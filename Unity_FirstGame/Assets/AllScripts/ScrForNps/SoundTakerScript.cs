using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundTakerScript : MonoBehaviour
{
    protected PatrolScriptNavMesh ZombiePatrolScript;
    public bool IHearSomething;
    public Vector3 InerestPos;
    // Start is called before the first frame update
    void Start()
    {

        ZombiePatrolScript = gameObject.GetComponent<PatrolScriptNavMesh>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void TakeSound(Vector3 SoundPosition)
    {
        
        if (ZombiePatrolScript)
        {
            IHearSomething = true;
            InerestPos = SoundPosition;
        }
    }
    public void NullInterest()
    {
        IHearSomething = false;
    }
}
