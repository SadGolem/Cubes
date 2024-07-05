using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckZoneController : MonoBehaviour
{
    public Transform zone2Grid;
    public Transform zone1Grid;
    public List<GameObject> objectsInZone2 = new List<GameObject>();
    public List<GameObject> objectsInZone3 = new List<GameObject>();
    [SerializeField] private static float tolerance = 6f;

    public void CheckCubes()
    {
        Debug.Log("я проверяюсь");
        // Очистка предыдущих объектов
        objectsInZone2.Clear();
        objectsInZone3.Clear();

        // Получение всех коллайдеров в зонах 2 и 3
        Collider[] collidersZone2 = Physics.OverlapSphere(zone2Grid.position, zone2Grid.localScale.x * 100);
        Collider[] collidersZone1 = Physics.OverlapSphere(zone1Grid.position, zone1Grid.localScale.x * 100);

        // Проверка каждого коллайдера в зоне 2
        foreach (Collider collider in collidersZone2)
        {
            if (collider.gameObject.CompareTag("Zone3Cube"))
            {
                objectsInZone2.Add(collider.gameObject);
                Debug.Log(collider.gameObject + " добавлен в зону 2");
            }
        }

        // Проверка каждого коллайдера в зоне 1
        foreach (Collider collider in collidersZone1)
        {
            if (collider.gameObject.CompareTag("Zone1Cube"))
            {
                objectsInZone3.Add(collider.gameObject);
                Debug.Log(collider.gameObject + " добавлен в зону 1");
            }
        }

        // Проверка решения
        if (objectsInZone2.Count > 0 && objectsInZone3.Count > 0)
        {
            if (CheckColorOrder(objectsInZone2, objectsInZone3))
            {
                Debug.Log("Пазл решен!");
                // Добавьте код для обработки решения пазла
            }
        }
    }

    // Метод для получения списка объектов и их позиций
    private List<Vector3> GetObjectsPositions(List<GameObject> objects, Transform referenceZone)
    {
        List<Vector3> positions = new List<Vector3>();

        foreach (GameObject obj in objects)
        {
            // Calculate position relative to the referenceZone
            Vector3 position = obj.transform.position - referenceZone.position;
            positions.Add(position);
        }

        return positions;
    }


    private bool CheckColorOrder(List<GameObject> zone2Objects, List<GameObject> zone3Objects)
    {
        // Check if the number of objects in zones 2 and 3 is the same
        if (zone2Objects.Count != zone3Objects.Count)
        {
            Debug.Log("Количество объектов не совпадает");
            return false;
        }

        Debug.Log("Проверка цветов");

        for (int i = 0; i < zone2Objects.Count; i++)
        {
            GameObject cubeZone2 = zone2Objects[i];

            Vector3 positionZone2 = cubeZone2.transform.position - zone2Grid.position;


            GameObject cubeZone3 = zone3Objects.FirstOrDefault(obj =>
                Vector3.Distance((obj.transform.position - zone1Grid.position), positionZone2) <= tolerance);

            if (cubeZone3 == null)
            {
                Debug.Log("Corresponding cube not found for " + cubeZone2.name);
                return false;
            }

            // Check if the original colors of the cubes match
            if (cubeZone2.GetComponent<Cube>().OriginalColor != cubeZone3.GetComponent<Cube>().OriginalColor)
            {
                Debug.Log("Colors of cubes do not match for " + cubeZone2.name + " and " + cubeZone3.name);
                return false;
            }
        }

        Debug.Log("Colors match based on object positions");
        return true;
    }
}


