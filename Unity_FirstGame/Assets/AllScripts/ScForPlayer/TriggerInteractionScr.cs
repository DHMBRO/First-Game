using UnityEngine;

public class TriggerInteractionScr : MonoBehaviour
{
    [SerializeField] PlayerToolsToInteraction ToolsToInteraction;

    void Start()
    {
        ToolsToInteraction = GetComponent<PlayerToolsToInteraction>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    void Update()
    {
        
    }
}
