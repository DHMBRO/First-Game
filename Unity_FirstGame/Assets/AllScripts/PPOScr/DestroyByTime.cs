using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] float TimeToDestroy;
    void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
