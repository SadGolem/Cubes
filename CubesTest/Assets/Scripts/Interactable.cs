using UnityEngine;

public class Interactable : MonoBehaviour, IInterectable
{
    private bool isUsing = false;

    public void Use(Transform playerTransform)
    {
        if (!isUsing)
        {
            // ����������� ������ � ���������
            transform.SetParent(playerTransform);
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
