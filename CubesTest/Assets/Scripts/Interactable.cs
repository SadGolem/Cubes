
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour, IInterectable
{
    private bool isUsing = false;
    private Vector3 offset;
    public bool isCanTouch = true;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerController playerOwner;

    /*[ServerRpc*//*(RequireOwnership = false)*//*]*/
    public void Use(PlayerController player)
    {
        if (!isCanTouch) return;
        if (!isUsing && player.CanUsingCube)
        {
            offset = transform.position - player.transform.position;
            playerOwner = player;
            rb.isKinematic = true;  // Make the object kinematic to ensure it follows the player's movement

            isUsing = true;
        }
        else
        {
            // Открепляем объект от персонажа
            transform.SetParent(null);

            Debug.Log("Я выбросился");
            isUsing = false;
        }
        
    }

    void Update()
    {
        if (isUsing)
        {
            transform.position = playerOwner.transform.position;
            transform.rotation = playerOwner.transform.rotation;
        }
    }
}
