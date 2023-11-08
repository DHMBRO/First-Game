using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTakerScript : MonoBehaviour
{
    protected PatrolScriptNavMesh ZombiePatrolScript;
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
            // State Attacking && Chasing (не чую шумів);
            ZombiePatrolScript.CheckPosition(SoundPosition);
        }
    }
}
