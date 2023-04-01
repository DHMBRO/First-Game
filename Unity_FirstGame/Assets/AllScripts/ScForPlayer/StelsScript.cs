using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StelsScript : MonoBehaviour
{
    public bool StelsOn;
    public MovePlayer MovePlayer;
    private bool ButtonActive = false;
    void Start()
    {
        MovePlayer = GetComponent<MovePlayer>();      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bach") && ButtonActive == true)
        {
            Debug.Log("Stels.On");
            StelsOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StelsOn = false;
    }
    void Update()
    {
       
        ButonActive();
    }
    bool ButonActive()
    {
        if (ButtonActive = false && Input.GetKeyDown(KeyCode.Z))
        {
            ButtonActive = true;
        }
        if (ButtonActive = true && Input.GetKeyDown(KeyCode.Z))
        {
            ButtonActive = false;
        }
        return ButtonActive;
    }
}
