using System.Collections.Generic;
using UnityEngine;

public class LocationManagerScript : MonoBehaviour
{
    SphereCollider ManagerColider;
    List<GameObject> AllNpcInLocation = new List<GameObject>();
    [SerializeField] BoxCollider Box;
    void Start()
    {
        Box = gameObject.GetComponent<BoxCollider>();
        ManagerColider = gameObject.GetComponent<SphereCollider>();
        Collider[] AllColiders = Physics.OverlapBox(gameObject.transform.position, Box.size); //Physics.o(gameObject.transform.position, ManagerColider.radius); 
        foreach (Collider colider in AllColiders)
        {
            InfScript ColiderInfo = colider.GetComponent<InfScript>();
            if (!ColiderInfo)
            {
                continue;
            }
            AllNpcInLocation.Add(colider.gameObject.transform.root.gameObject);
            colider.gameObject.transform.root.gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerControler PlayerControllerScr = other.gameObject.GetComponent<PlayerControler>();
        if (PlayerControllerScr)
        {
            SetActiveToAll(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerControler PlayerControllerScr = other.gameObject.GetComponent<PlayerControler>();
        if (PlayerControllerScr)
        { 
            SetActiveToAll(false);
        }
    }
    void SetActiveToAll(bool value)
    {
        foreach (GameObject Npc in AllNpcInLocation)
        {
            Npc.SetActive(value);
        }
    }           
    
}
