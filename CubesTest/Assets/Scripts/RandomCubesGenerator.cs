using UnityEngine;
using System.Collections.Generic;

public class RandomCubesGenerator : MonoBehaviour
{
    // ������ ��� �������� ������ �����
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

    // ����� ��� ��������� ���������� ������ ����� � ���� 1
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

    // ����� ��� �������� ����� � ���� 3
    public void CreateZone3Cubes(GameObject cubePrefab, Transform zone3SpawnPoint, List<Cube> zone3Cubes, List<Color> randomCreatedColors, List<Vector3> cubePositionsZone1)
    {
        ClearZone(zone3SpawnPoint, "Zone3Cube");
        zone3Cubes.Clear();

        // �������� ����� � ���� 3
        foreach (Vector3 position in cubePositionsZone1)
        {
            Cube cube = CreateCube(cubePrefab, zone3SpawnPoint, position, "Zone3Cube");
            cube.OriginalColor = randomCreatedColors[0];
            randomCreatedColors.RemoveAt(0);
            zone3Cubes.Add(cube);
        }
    }

    private Cube CreateCube(GameObject cubePrefab, Transform zone, Vector3 position, string tag)
    {
        GameObject cube = Instantiate(cubePrefab, zone.position + position, Quaternion.identity, zone);
        cube.tag = tag;
        Cube newCube = cube.GetComponent<Cube>();
        return newCube;
    }

    // ����� ��� �������� ���������� ����
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

    // ����� ��� ������� ���� �� �����
    private void ClearZone(Transform zone, string tag)
    {
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(cube);
        }
    }
}

