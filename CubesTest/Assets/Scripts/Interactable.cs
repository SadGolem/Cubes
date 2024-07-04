using UnityEngine;

public class Interactable : MonoBehaviour, IInterectable
{
    private bool isUsing = false;
    public bool isCanTouch = true;

    public void Use(PlayerController player)
    {
        if (!isCanTouch) return;
        if (!isUsing && player.CanUsingCube)
        {
            // ����������� ������ � ���������
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
