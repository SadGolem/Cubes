using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSolution : MonoBehaviour
{
    [SerializeField] private float tolerance = 0.1f;
    // ����� ��� �������� ���������� ������� ����� � ����� 1 � 2
    public bool CheckPuzzleSolution(List<Vector3> zone1, List<Vector3> zone2)
    {
        // ��������, ��� ������ ������� ����������
        if (zone1.Count != zone2.Count)
        {
            return false;
        }

        /*// ���������� ������� ��� ���������
        cubePositionsZone1.Sort();
        cubePositionsZone2.Sort();*/

        // �������� �� ���������� � ������������
        for (int i = 0; i < zone1.Count; i++)
        {
            // ���������, ��� ������� ����� ��������� ������ ��������� ������
            if (Vector3.Distance(zone1[i], zone2[i]) > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}
