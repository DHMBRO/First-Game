using UnityEngine;
using System.Collections.Generic;

public class BoneControler : MonoBehaviour
{
    [SerializeField] List<Transform> AllBonesScr = new List<Transform>();

    public Transform ReturnLimb(Transform Player)
    {
        float SmalestDistance = (this.transform.position - Player.position).magnitude;
        int CountBestBone = 0;

        for (int i = 0;i <  AllBonesScr.Count;i++)
        {
            float CurrentDistance = (AllBonesScr[i].transform.position - Player.position).magnitude;
            
            if (CurrentDistance <= SmalestDistance)
            {
                SmalestDistance = CurrentDistance;
                CountBestBone = i;
            }
        }
        
        return AllBonesScr[CountBestBone].transform;
    }

}
