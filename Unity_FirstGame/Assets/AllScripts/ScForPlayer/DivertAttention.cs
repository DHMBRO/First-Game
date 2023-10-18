using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivertAttention : MethodsFromDevelopers
{
    [SerializeField] private GameObject RockForDiverAttention;
    [SerializeField] private SlotControler ControlerSlots;
    [SerializeField] private Transform CameraPlayer;
    [SerializeField] private Transform HandPlayer;
    

    [SerializeField] float SpeedToDrop;
    [SerializeField] float HeidhtToDrop;
    [SerializeField] int Count;

    [SerializeField] bool CanDivrtAttention;

    [SerializeField] GameObject Rock;


    private void Start()
    {
        ControlerSlots = GetComponent<SlotControler>();
        if (ControlerSlots && ControlerSlots.SlotHand) HandPlayer = ControlerSlots.SlotHand;
        else Debug.Log("Not set ControlerSlots");

    }

    public void SpawnRock()
    {
        if (!HandPlayer && ControlerSlots && !this.Rock) return;

        GameObject Rock;

        ControlerSlots.PutObjectInHand();

        Rock = Instantiate(RockForDiverAttention);
        this.Rock = Rock;
        PutObjects(Rock.transform, HandPlayer);

    }


    public void AimingToDrop()
    {
        Rock.transform.forward = CameraPlayer.forward;

    }


    public void DropRock()
    {
        Vector3 Trajectory;
        Rigidbody RIGRock;

        SoundCreatorScript SoundScript;
        ExecutoreScript ExecutorScript;

        Rock.transform.SetParent(null);
        RIGRock = Rock.AddComponent<Rigidbody>();

        SoundScript = Rock.AddComponent<SoundCreatorScript>();
        ExecutorScript = Rock.AddComponent<ExecutoreScript>();

        Trajectory = new Vector3(0.0f, HeidhtToDrop, SpeedToDrop);

        RIGRock.AddRelativeForce(Trajectory, ForceMode.Force);
        
        Destroy(Rock, 10.0f);
        ControlerSlots.GetObjectInHand();
        
    }
       
    

    
    
}
