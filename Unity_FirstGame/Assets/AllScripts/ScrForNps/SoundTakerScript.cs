 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundTakerScript : MonoBehaviour
{
    protected PatrolScriptNavMesh ZombiePatrolScript;
    public bool IHearSomething;
    public Vector3 InerestPos;
    protected InfScript MyInfo;
    // Start is called before the first frame update
    void Start()
    {
        MyInfo = gameObject.GetComponent<InfScript>();
        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
    }
    // Update is called once per frame
    void Update()
    {


    }
    public void TakeSound(Vector3 SoundPosition)
    {
        //Debug.Log(gameObject.name + " Voice");
        if (ZombiePatrolScript && MyInfo && MyInfo.InterestPosition != Vector3.zero)
        {
            IHearSomething = true;
            MyInfo.InterestPosition = SoundPosition;
        }
    }
    public void NullInterest()
    {
        IHearSomething = false;
        MyInfo.InterestPosition = Vector3.zero;
    }
   
}
