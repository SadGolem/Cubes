
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
            // ����������� ������ � ���������
            if(IsServer)
                transform.GetComponent<NetworkObject>().ChangeOwnership(player.GetComponent<NetworkObject>().OwnerClientId);
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;

            Debug.Log("� ������");
            isUsing = true;
        }
        else
        {
            // ���������� ������ �� ���������
            transform.SetParent(null);

            Debug.Log("� ����������");
            isUsing = false;
        }      
    }
}
