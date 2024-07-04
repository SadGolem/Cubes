using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CheckZoneController : MonoBehaviour
{
    public Transform zone2Grid;
    public List<GameObject> objectsInZone = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(CheckSolutionCoroutine()); // ������ �������� ��� �������� ��� � �������
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
}
