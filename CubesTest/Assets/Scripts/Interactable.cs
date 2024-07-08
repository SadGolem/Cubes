
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour, IInterectable
{
    private bool isUsing = false;
    public bool isCanTouch = true;

    [ServerRpc/*(RequireOwnership = false)*/]
    public void UseServerRpc(PlayerController player)
    {
        if (!isCanTouch) return;
        if (!isUsing && player.CanUsingCube)
        {
            // Прикрепляем объект к персонажу
            if(IsServer)
                transform.GetComponent<NetworkObject>().ChangeOwnership(player.GetComponent<NetworkObject>().OwnerClientId);
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Debug.Log("Я взялся");
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
}
