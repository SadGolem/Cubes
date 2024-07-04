using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    // Данные для хранения набора кубов

    private List<Vector3> availablePositions = new List<Vector3>() // Лист векторов для расположения на зоне
        {
            new Vector3(-15, 10, -15),
            new Vector3(0, 10, -15),
            new Vector3(15, 10, -15),
            new Vector3(-15, 10, 0),
            new Vector3(0, 10, 0),
            new Vector3(15, 10, 0),
            new Vector3(-15, 10, 15),
            new Vector3(0, 10, 15),
            new Vector3(15, 10, 15)
        };

    // Метод для генерации случайного набора кубов в зоне 1
    public void GenerateZone1Cubes(GameObject cubePrefab, Transform zone1Grid, List<Vector3> cubePositionsZone1, List<Color> randomCreatedColors)
    {
        // Очистка предыдущего набора кубов
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("Zone1Cube"))
        {
            Destroy(cube);
        }
        cubePositionsZone1.Clear();
        randomCreatedColors.Clear();

        for (int i = 0; i < 9; i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 randomPosition = availablePositions[randomIndex];
            cubePositionsZone1.Add(randomPosition);
            availablePositions.RemoveAt(randomIndex); // Удаляем позицию из списка, чтобы избежать дублирования

            // Создание куба на выбранной позиции
            GameObject cube = Instantiate(cubePrefab, zone1Grid.position + randomPosition, Quaternion.identity);
            cube.transform.SetParent(zone1Grid);
            cube.tag = "Zone1Cube";

            // Присваиваем случайный цвет кубу
            Color color = Random.ColorHSV();
            randomCreatedColors.Add(color);
            cube.GetComponent<Renderer>().material.color = color;
            cube.GetComponent<Interactable>().isCanTouch = false;
        }
        randomCreatedColors.Reverse();
    }  

    // Метод для создания кубов в зоне 3
    public void CreateZone3Cubes(GameObject cubePrefab, Transform zone3SpawnPoint, List<GameObject> zone3Cubes, List<Color> randomCreatedColors, List<Vector3> cubePositionsZone1)
    {
        // Очистка предыдущих кубов
        foreach (GameObject cube in zone3Cubes)
        {
            Destroy(cube);
        }
        zone3Cubes.Clear();

        // Создание кубов в зоне 3
        foreach (Vector3 position in cubePositionsZone1)
        {
            GameObject cube = Instantiate(cubePrefab, zone3SpawnPoint.position + position, Quaternion.identity);
            cube.GetComponent<Renderer>().material.color = randomCreatedColors[0];
            cube.tag = "Zone2Cube";
            randomCreatedColors.RemoveAt(0);
            zone3Cubes.Add(cube);
        }
    }
}

