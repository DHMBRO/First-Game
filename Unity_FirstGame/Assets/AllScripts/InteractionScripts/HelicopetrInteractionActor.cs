using UnityEngine;

public class HelicopetrInteractionActor : SingleInteractionActor
{
    [SerializeField] HelicopterScr ScrHelicopter;
    [SerializeField] HelicopterScr.States HelicopterStates = HelicopterScr.States.Wait;

    private void Start()
    {
        base.Start();    
        if (!ScrHelicopter) ScrHelicopter = GetComponent<HelicopterScr>();
    
    }

    override protected void CustomInteraction()
    {
        switch (HelicopterStates)
        {
            case HelicopterScr.States.Wait:
                ScrHelicopter.FlightToPlayer();
                break;
            case HelicopterScr.States.FlightToPlayer:
                ScrHelicopter.SetStateFlightToPlayer();
                break;
            case HelicopterScr.States.RoamAround:
                ScrHelicopter.SetStateRoamAround();
                break;
            case HelicopterScr.States.Landing:
                ScrHelicopter.SetStateLanding();
                break;
            case HelicopterScr.States.FlightOut:
                ScrHelicopter.FlightOut();
                break;
            default:
                break;
        }
    }

}
