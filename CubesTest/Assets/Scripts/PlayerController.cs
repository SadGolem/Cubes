using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float interactionDistance = 3f;
    private bool isCanUsingCube = true;
    public bool CanUsingCube { get { return isCanUsingCube; } set { isCanUsingCube = value; } }
    public event System.Action cubeIsPutOnTheFloor;
    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ����� �������� � ������� ��������������
           Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);

            // �������� ������� �������
            foreach (Collider collider in colliders)
            {
                // ��������, �������� �� ������ �����������������
                Interactable interactable = collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    // �������������� � ��������
                    interactable.Use(this);
                    isCanUsingCube = !isCanUsingCube;
                    if (!isCanUsingCube)
                        cubeIsPutOnTheFloor?.Invoke();
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
