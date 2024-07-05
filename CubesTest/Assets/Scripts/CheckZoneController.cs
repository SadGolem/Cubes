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
       /* StartCoroutine(CheckSolutionCoroutine());*/ // ������ �������� ��� �������� ��� � �������
    }

    private IEnumerator CheckSolutionCoroutine()
    {
        while (true)
        {
            CheckCubes();
            yield return new WaitForSeconds(1f); // ����� �� 1 �������
        }
    }

    public void CheckCubes()
    {
        Debug.Log("� ����������");
        // ������� ���������� ��������
        objectsInZone.Clear();

        // ��������� ���� ����������� � ����
        Collider[] colliders = Physics.OverlapSphere(zone2Grid.position, zone2Grid.localScale.x * 100);

        // �������� ������� ����������
        foreach (Collider collider in colliders)
        {
            // ��������, ����� �� ������ ��� "Zone1Cube"
            if (collider.gameObject.CompareTag("Zone2Cube"))
            {
                // ��������� �������, ���������� � �����������
                GameObject obj = collider.gameObject;

                // ���������� ������� � ������
                objectsInZone.Add(obj);
                Debug.Log(obj + " ��������");
            }
            if (objectsInZone.Count > 0)
                Debug.Log(GetObjectsPositions().First());
        }
    }

    // ����� ��� ��������� ������ �������� � �� �������
    public List<Vector3> GetObjectsPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        // ������� �������� � ����
        foreach (GameObject obj in objectsInZone)
        {
            // ���������� ������� ������� � ������
            positions.Add(obj.transform.localPosition - zone2Grid.position);
        }

        return positions;
    }

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
