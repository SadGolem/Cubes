using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSolution : MonoBehaviour
{
    [SerializeField] private float tolerance = 0.1f;
    // ћетод дл€ проверки совпадени€ наборов кубов в зонах 1 и 2
    public bool CheckPuzzleSolution(List<Vector3> zone1, List<Vector3> zone2)
    {
        // ѕроверка, что размер наборов одинаковый
        if (zone1.Count != zone2.Count)
        {
            return false;
        }

        /*// —ортировка наборов дл€ сравнени€
        cubePositionsZone1.Sort();
        cubePositionsZone2.Sort();*/

        // ѕроверка на совпадение с погрешностью
        for (int i = 0; i < zone1.Count; i++)
        {
            // ѕровер€ем, что разница между позици€ми меньше заданного порога
            if (Vector3.Distance(zone1[i], zone2[i]) > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}
