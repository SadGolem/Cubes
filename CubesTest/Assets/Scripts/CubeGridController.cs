using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CubeGridController : MonoBehaviour
{
    [SerializeField] private RandomCubesGenerator randomCubesGenerator;
    [SerializeField] private CheckZoneController checkZoneController;
    [SerializeField] public List<GameObject> cubePrefab; // ������ ����
    [SerializeField] private Transform zone1Grid; // ��������� ����� 3x3 � ���� 1
    [SerializeField] private Transform zone2Grid; // ��������� ����� 3x3 � ���� 2
    [SerializeField] private Transform zone3SpawnPoint; // ��������� ����� ��������� ����� � ���� 3
    [SerializeField] private NetWorkUI netWorkUI;

    private List<Vector3> cubePositionsZone1 = new List<Vector3>(); // ������� ����� � ���� 1
    private List<Vector3> cubePositionsZone2 = new List<Vector3>(); // ������� ����� � ���� 1
    private List<Cube> zone3Cubes = new List<Cube>(); // ������ ����� � ���� 3
    private List<Color> randomCreatedColors = new List<Color>(); // ������ ��������� ������
    private List<Cube> cubes = new List<Cube>(); // ������ ��������� ������

    private void Start()
    {
        if (netWorkUI != null)
        {
            netWorkUI.isHosting += StartGame;
        }
    }

    void StartGame()
    {
        Debug.Log("������");
        randomCubesGenerator.GenerateZone1Cubes(cubePrefab, zone1Grid, cubePositionsZone1, cubes);
        randomCubesGenerator.CreateZone3Cubes(cubePrefab, zone3SpawnPoint, zone3Cubes, cubes, cubePositionsZone1);
    }

    public void SubscribeOnthePlayer(PlayerController playerController)
    {
        // ������������� �� ������� cubeIsPutOnTheFloor
        if (playerController != null)
        {
            playerController.cubeIsPutOnTheFloor += checkZoneController.CheckCubes;
        }
        Debug.Log("������������ ���������� �� �������");
    }

}
