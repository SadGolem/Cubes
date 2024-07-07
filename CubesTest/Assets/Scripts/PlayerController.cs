using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IPlayerController, INetworkSerializable
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private float positionRange = 5f;
    private bool isUsingCube = true;
    public bool CanUsingCube { get { return isUsingCube; } set { isUsingCube = value; } }
    public event Action cubeIsPutOnTheFloor;

    void Start()
    {
        CubeGridController cubeGridController = FindObjectOfType<CubeGridController>();
        cubeGridController.SubscribeOnthePlayer(this);
        Debug.Log("пользователь" + this + "зашел");
    }

/*    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();*/

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(positionRange, -positionRange), 0, UnityEngine.Random.Range(positionRange, -positionRange));
    }
    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdInteraction();
        }
    }

    void CmdInteraction()
    {
        DoInteractionServerRpc();
    }

    [ServerRpc]
    void DoInteractionServerRpc()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);

        foreach (Collider collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.UseServerRpc(this);
                isUsingCube = !isUsingCube;
                if (isUsingCube)
                    cubeIsPutOnTheFloor?.Invoke();
                break;
            }
        }
    }

    void Update()
    {
        Interaction();
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;
        Interaction();
        Move();
        // Передаем запрос на перемещение на сервер
        /*Move(movement);*/

    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        /*movement.Normalize();*/

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref isUsingCube);
    }
}
