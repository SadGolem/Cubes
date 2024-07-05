using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class RandomCubesGenerator : MonoBehaviour
{
    int count = 0;
    // Данные для хранения набора кубов
    private List<Vector3> availablePositions = new List<Vector3>()
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
        ClearZone(zone1Grid, "Zone1Cube");
        cubePositionsZone1.Clear();
        randomCreatedColors.Clear();

        for (int i = 0; i < 9; i++)
        {
            CreateRandomCube(cubePrefab, zone1Grid, cubePositionsZone1, randomCreatedColors, "Zone1Cube");
        }

        randomCreatedColors.Reverse();
    }

    // Метод для создания кубов в зоне 3
    public void CreateZone3Cubes(GameObject cubePrefab, Transform zone3SpawnPoint, List<Cube> zone3Cubes, List<Color> randomCreatedColors, List<Vector3> cubePositionsZone1)
    {
        ClearZone(zone3SpawnPoint, "Zone3Cube");
        zone3Cubes.Clear();

        // Создание кубов в зоне 3
        foreach (Vector3 position in cubePositionsZone1)
        {
            Cube cube = CreateCube(cubePrefab, zone3SpawnPoint, position, "Zone3Cube");
            cube.OriginalColor = randomCreatedColors[0];
            randomCreatedColors.RemoveAt(0);
            zone3Cubes.Add(cube);
        }
    }

    [ClientRpc]
    private Cube CreateCube(GameObject cubePrefab, Transform zone, Vector3 position, string tag)
    {
        GameObject cube = Instantiate(cubePrefab, zone.position + position, Quaternion.identity, zone);
        cube.GetComponent<NetworkObject>().Spawn();
        /*NetworkServer.Spawn(cube);*/
        cube.name = count++.ToString();
        if (count == 9) { count = 0; }
        cube.tag = tag;
        Cube newCube = cube.GetComponent<Cube>();
        return newCube;
    }

    // Метод для создания случайного куба
    private Cube CreateRandomCube(GameObject cubePrefab, Transform zone, List<Vector3> cubePositions, List<Color> randomCreatedColors, string tag)
    {
        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector3 randomPosition = availablePositions[randomIndex];
        cubePositions.Add(randomPosition);
        availablePositions.RemoveAt(randomIndex);

        Cube cube = CreateCube(cubePrefab, zone, randomPosition, tag);

        Color color = Random.ColorHSV();
        randomCreatedColors.Add(color);
        cube.OriginalColor = color;
        cube.IsCanTouch = false;

        return cube;
    }

    // Метод для очистки зоны от кубов
    [ClientRpc]
    private void ClearZone(Transform zone, string tag)
    {
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(cube);
        }
    }
}

