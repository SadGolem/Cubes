using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CheckZoneController : MonoBehaviour
{
    public Transform zone2Grid;
    public List<GameObject> objectsInZone = new List<GameObject>();
    [SerializeField] private static float tolerance = 0.1f;

    private void Start()
    {
       /* StartCoroutine(CheckSolutionCoroutine());*/ // Запуск корутины для проверки раз в секунду
    }

    private IEnumerator CheckSolutionCoroutine()
    {
        while (true)
        {
            CheckCubes();
            yield return new WaitForSeconds(1f); // Пауза на 1 секунду
        }
    }

    public void CheckCubes()
    {
        Debug.Log("я проверяюсь");
        // Очистка предыдущих объектов
        objectsInZone.Clear();

        // Получение всех коллайдеров в зоне
        Collider[] colliders = Physics.OverlapSphere(zone2Grid.position, zone2Grid.localScale.x * 100);

        // Проверка каждого коллайдера
        foreach (Collider collider in colliders)
        {
            // Проверка, имеет ли объект тэг "Zone1Cube"
            if (collider.gameObject.CompareTag("Zone2Cube"))
            {
                // Получение объекта, связанного с коллайдером
                GameObject obj = collider.gameObject;

                // Добавление объекта в список
                objectsInZone.Add(obj);
                Debug.Log(obj + " добавлен");
            }
            if (objectsInZone.Count > 0)
                Debug.Log(GetObjectsPositions().First());
        }
    }

    // Метод для получения списка объектов и их позиций
    public List<Vector3> GetObjectsPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        // Перебор объектов в зоне
        foreach (GameObject obj in objectsInZone)
        {
            // Добавление позиции объекта в список
            positions.Add(obj.transform.localPosition - zone2Grid.position);
        }

        return positions;
    }

    public bool CheckPuzzleSolution(List<Vector3> zone1, List<Vector3> zone2)
    {
        // Проверка, что размер наборов одинаковый
        if (zone1.Count != zone2.Count)
        {
            return false;
        }

        /*// Сортировка наборов для сравнения
        cubePositionsZone1.Sort();
        cubePositionsZone2.Sort();*/

        // Проверка на совпадение с погрешностью
        for (int i = 0; i < zone1.Count; i++)
        {
            // Проверяем, что разница между позициями меньше заданного порога
            if (Vector3.Distance(zone1[i], zone2[i]) > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}
