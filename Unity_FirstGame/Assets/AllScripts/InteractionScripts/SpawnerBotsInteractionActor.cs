using UnityEngine;

public class SpawnerBotsInteractionActor : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] GameObject Bot;
    [SerializeField] int CountOfAliveBots;
    //[SerializeField]


    void Start()
    {
        
    }

    public bool CheckToUse()
    {
        return true;
    }

    public void Interaction()
    {

    }
}
