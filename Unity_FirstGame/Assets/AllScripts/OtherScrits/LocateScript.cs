using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateScript : MonoBehaviour
{    
    [SerializeField] public bool Agr;
    [SerializeField] public Transform Head;    

    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject PosTargetForRotate;
    [SerializeField] public StelsScript StelsScript;

    [SerializeField] private float SpeedForMove = 0.01f;
    [SerializeField] private float MaxDistance = 14.0f;

    
    void Start()
    {        
        if (Target)
        {
            StelsScript = Target.GetComponent<StelsScript>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {              
        if (other.gameObject.CompareTag("Player01"))
        {
            Target = other.gameObject;
            PosTargetForRotate = Target;
            StelsScript = Target.gameObject.GetComponent<StelsScript>();

            if (StelsScript && !StelsScript.StelsOn)
            {
                Agr = true;                
            }                                    
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            Target = null;
            StelsScript = null;

            Agr = false;
        }
    }

    void MoveTo()
    {               
        gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;

    }
    
    void Update()
    {
        Vector3 target = Target.transform.position - gameObject.transform.position;
        Ray ForwardZombie = new Ray(Head.position, Head.forward);

        if (Physics.Raycast(transform.position, Target.transform.position * MaxDistance))
        {
            Debug.DrawLine(transform.position, Target.transform.position, Color.yellow);
            transform.rotation = Quaternion.LookRotation(target);


        }
        if (Physics.Raycast(ForwardZombie, out RaycastHit HitResult01, MaxDistance))
        {
            Debug.DrawLine(Head.position, Head.forward * MaxDistance + Head.position, Color.green);

            if (Agr && HitResult01.collider.gameObject.tag == "Player01" && !StelsScript.StelsOn)
            {
                gameObject.transform.localPosition += gameObject.transform.forward * SpeedForMove;
                Debug.Log("1");

            }
            else if (HitResult01.collider.gameObject.tag != "Player01")
            {
                StelsScript = null;
                Agr = false;
            }
            Debug.Log(HitResult01.collider.gameObject.tag);
        }
    }
}

