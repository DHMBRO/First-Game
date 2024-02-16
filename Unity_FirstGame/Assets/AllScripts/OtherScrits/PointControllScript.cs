using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControllScript : MonoBehaviour
{
    [SerializeField] public GameObject[] Points;
    public int CurrentPointIndex = 0;
    public int SearchNextPosition(int CurrentPoint)
    {
        if (Points.Length > 0)
        {
            if(CurrentPoint +1 < Points.Length)
            {
                return  CurrentPoint +1;
            }
        }
        return 0;
    }
    public Vector3 GetPosByIndex(int PosIndex)
    {
        if (Points.Length < 1 || PosIndex < 0 || PosIndex > Points.Length)
        {
            return Vector3.zero;
        }
        return Points[PosIndex].transform.position;
    }

}
