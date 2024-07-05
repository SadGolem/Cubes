using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float interactionDistance = 3f;
    private bool isUsingCube = true;
    public bool CanUsingCube { get { return isUsingCube; } set { isUsingCube = value; } }
    public event Action cubeIsPutOnTheFloor;

    void Start()
    {
        CubeGridController cubeGridController = FindObjectOfType<CubeGridController>();
        cubeGridController.SubscribeOnthePlayer(this);
    }

    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ����� �������� � ������� ��������������
           Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);

            foreach (Collider collider in colliders)
            {
                // ��������, �������� �� ������ �����������������
                Interactable interactable = collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    // �������������� � ��������
                    interactable.Use(this);
                    isUsingCube = !isUsingCube;
                    if (isUsingCube)
                        cubeIsPutOnTheFloor?.Invoke(); // FFFFFFFFFFFFFFFFFFFFFFFFFFFFF
                    break;
                }
            }   
        }
    }

    public void Move()
    {
        // ��������� ����� �� ������������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        transform.position += movement * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Interaction();
    }
}
