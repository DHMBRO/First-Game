using UnityEngine;
using System.Collections.Generic;

public class SpawnerBotsInteractionActor : MonoBehaviour, IInteractionWithObjects
{
    [SerializeField] GameObject Bot = null;
    [SerializeField] Transform BotPositionSpawn = null;
    
    [SerializeField] float NextTimeToSpawn = 0.0f;
    [SerializeField] float DelayToSpawn = 1.0f;
    
    [SerializeField] int HaveToBeBots = 0;
    [SerializeField] int NowAliveBots = 0;
    [SerializeField] bool IsEnabled = false;

    public bool CheckToUse()
    {
        if (Bot == null || BotPositionSpawn == null || HaveToBeBots == 0)
        {
            Debug.Log(" (SpawnerBotsInteractionActor), canot work, check settings of component !");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Interaction()
    {
        IsEnabled = !IsEnabled; 
    }

    private void Update()
    {

        if(NowAliveBots == HaveToBeBots || !IsEnabled)
        {
            return;
        }
        
        if (Time.time >= NextTimeToSpawn)
        {
            NextTimeToSpawn = Time.time + DelayToSpawn;

            GameObject NewBot = Instantiate(Bot, BotPositionSpawn.position, BotPositionSpawn.rotation);
            NewBot.GetComponent<HpScript>().StateDelegate += UpdateCountOfAliveBots;
            
            NowAliveBots++;
        }

    }
    
    private void UpdateCountOfAliveBots(bool Alive)
    {
        if (!Alive)
        {
            NowAliveBots--;
            NextTimeToSpawn = Time.time + DelayToSpawn;
        }
    }

}
