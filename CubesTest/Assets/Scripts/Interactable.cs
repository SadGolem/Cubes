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
            // Прикрепляем объект к персонажу
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
