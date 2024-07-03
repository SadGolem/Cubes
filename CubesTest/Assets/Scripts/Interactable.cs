using UnityEngine;

public class Interactable : MonoBehaviour, IInterectable
{
    private bool isUsing = false;

    public void Use(Transform playerTransform)
    {
        if (!isUsing)
        {
            // Прикрепляем объект к персонажу
            transform.SetParent(playerTransform);
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
