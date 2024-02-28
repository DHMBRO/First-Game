using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HeadInterface
{
    Vector3 GetHeadPosition();
    Vector3 GetSpeed();
}
public class HeadScript : MonoBehaviour, HeadInterface
{
    [SerializeField] public GameObject Head;

    PatrolScript Patrol;
    void Start()
    {
        Patrol = gameObject.GetComponent<PatrolScript>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Vector3 DirectionToHead()
    {
        return Head.transform.position;
    }
    public Vector3 GetHeadPosition()
    {
        return Head.transform.position;
    }
    public Vector3 GetSpeed()
    {
        return Patrol.ZombieNavMesh.velocity;
    }
}
