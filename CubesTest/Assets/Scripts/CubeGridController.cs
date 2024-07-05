using System.Collections.Generic;
using UnityEngine;

public class CubeGridController : MonoBehaviour
{
    [SerializeField] private CheckSolution checkSolution;
    [SerializeField] private RandomCubesGenerator randomCubesGenerator;
    [SerializeField] private GameObject cubePrefab; // Префаб куба
    [SerializeField] private Transform zone1Grid; // Трансформ сетки 3x3 в зоне 1
    [SerializeField] private Transform zone2Grid; // Трансформ сетки 3x3 в зоне 2
    [SerializeField] private Transform zone3SpawnPoint; // Трансформ точки появления кубов в зоне 3

    private List<Vector3> cubePositionsZone1 = new List<Vector3>(); // Позиции кубов в зоне 1
    private List<Vector3> cubePositionsZone2 = new List<Vector3>(); // Позиции кубов в зоне 1
    private List<Cube> zone3Cubes = new List<Cube>(); // Список кубов в зоне 3
    private List<Color> randomCreatedColors = new List<Color>(); // Список созданных цветов


    private void Start()
    {
        randomCubesGenerator.GenerateZone1Cubes(cubePrefab, zone1Grid, cubePositionsZone1, randomCreatedColors);
        randomCubesGenerator.CreateZone3Cubes(cubePrefab, zone3SpawnPoint, zone3Cubes, randomCreatedColors, cubePositionsZone1);
    }

    void Check()
    {
        checkSolution.CheckPuzzleSolution(cubePositionsZone1, cubePositionsZone2);
    }
}
