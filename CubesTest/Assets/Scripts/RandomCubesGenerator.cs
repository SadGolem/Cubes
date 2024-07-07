using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using System.Linq;

public class RandomCubesGenerator : NetworkBehaviour
{
    int count = 0;
    // ƒанные дл€ хранени€ набора кубов
    static bool isGenerated = false;


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

    // ћетод дл€ генерации случайного набора кубов в зоне 1
    public void GenerateZone1Cubes(List<GameObject> cubePrefab, Transform zone1Grid, List<Vector3> cubePositionsZone1, List<Color> randomCreatedColors)
    {
        ClearZone(zone1Grid, "Zone1Cube");
        cubePositionsZone1.Clear();
        randomCreatedColors.Clear();
        Debug.Log("генераци€ зоны 1");
        for (int i = 0; i < 9; i++)
        {
            CreateRandomCube(cubePrefab, zone1Grid, cubePositionsZone1, randomCreatedColors, "Zone1Cube");
        }

        randomCreatedColors.Reverse();
    }

    // ћетод дл€ создани€ кубов в зоне 3

    /*[ServerRpc]*/
    public void CreateZone3Cubes(List<GameObject> cubePrefab, Transform zone3SpawnPoint, List<Cube> zone3Cubes, List<Color> randomCreatedColors, List<Vector3> cubePositionsZone1)
    {
        if (isGenerated) { return; }
        ClearZone(zone3SpawnPoint, "Zone3Cube");
        zone3Cubes.Clear();
        Debug.Log("генераци€ зоны 3");
        // —оздание кубов в зоне 3
        foreach (Vector3 position in cubePositionsZone1)
        {
            Cube cube = CreateCube(cubePrefab, zone3SpawnPoint, position, "Zone3Cube");
            cube.OriginalColor = randomCreatedColors[0];
            randomCreatedColors.RemoveAt(0);
            zone3Cubes.Add(cube);
        }
        isGenerated = true;
    }

    private Cube CreateCube(List<GameObject> cubePrefab, Transform zone, Vector3 position, string tag)
    {
        GameObject cube = Instantiate(cubePrefab.ElementAt(Random.Range(0, cubePrefab.Count)), zone.position + position, Quaternion.identity, zone);
        cube.GetComponent<NetworkObject>().Spawn();

        cube.name = count++.ToString();
        if (count == 9) { count = 0; }
        cube.tag = tag;
        Cube newCube = cube.GetComponent<Cube>();
        return newCube;
    }

    // ћетод дл€ создани€ случайного куба
    private Cube CreateRandomCube(List<GameObject> cubePrefab, Transform zone, List<Vector3> cubePositions, List<Color> randomCreatedColors, string tag)
    {
        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector3 randomPosition = availablePositions[randomIndex];
        cubePositions.Add(randomPosition);
        availablePositions.RemoveAt(randomIndex);
        Cube cube = CreateCube(cubePrefab, zone, randomPosition, tag);

        
        randomCreatedColors.Add(cube.OriginalColor);
        cube.IsCanTouch = false;
        return cube;
    }

    private void ClearZone(Transform zone, string tag)
    {
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(cube);
        }
    }


}

