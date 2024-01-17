using System;
using System.Net.NetworkInformation;
using UnityEngine;

public class HelicopterScr : MonoBehaviour
{
    
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject FlightFrom;
    [SerializeField] GameObject FlightTo;
    [SerializeField] Vector3 PositionToLending;
    [SerializeField] int Speed;
    //[SerializeField] GameObject MGameOjct;
    //[SerializeField] GameObject M2GameOjct;
    //[SerializeField] GameObject FGameOjct;
    //SerializeField] GameObject SGameOjct;
    [SerializeField] GameObject PositionAraundFlight;
    float T = 0.00f;
    float AddToT;
    bool landing = false;
    bool DriveMRight;
    float T2 = 0.00f;
    float T3 = 0.00f;
    Vector3 PositionFrom;
    public float RandomPosition_X;
    public float RandomPosition_Z;
    public GameObject TargetToGun;

    bool NeedNewPosition = true;
    Vector3 S;
    Vector3 F;
    Vector3 M;
    Vector3 s;

    public enum States
    {

        Wait,
        FlightToPlayer,
        RoamAround,
        Landing,
        FlightOut
    }
    public enum StatesFlight
    {
        FromSToM,
        FromMToM,
        FromMToF
    }
    StatesFlight StatesFlght;
    public States State;
    // Start is called before the first frame update
    void Start()
    {
        State = States.Wait;
    }

    // Update is called once per frame
    private void Update()
    {
    }
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            int value = ((int)State);
            value++;
            if (Enum.IsDefined(typeof(States), value))
            {
                State = (States)value;
            }
        }
        switch (State)
        {
            case States.Wait:
                break;
            case States.FlightToPlayer:
                FlightToPlayer();
                //Debug.Log("FlightToPlayer");
                break;
            case States.RoamAround:
                RoamAround();
                //Debug.Log("RoamAround");
                break;
            case States.Landing:
                //Debug.Log("Landing");
                Landing();
                break;
            case States.FlightOut:
                //Debug.Log("FlightOut");
                FlightOut();
                break;
            default:
                break;
        }
    }
    public void NextState()
    {
        int value = ((int)State);
        value++;
        if (Enum.IsDefined(typeof(States), value))
{
            State = (States)value;
        }
    }
    public void FlightToPlayer()
    {
        gameObject.transform.LookAt(FlightTo.transform.position);
        AddToT = Time.fixedDeltaTime / (Vector3.Distance(FlightFrom.transform.position, FlightTo.transform.position) / Speed);
        gameObject.transform.position = Vector3.Lerp(FlightFrom.transform.position, FlightTo.transform.position, T);
        T += AddToT;
        if(T >= 1.00f)
        {
            State = States.RoamAround;
        }
        
    }
    public void RoamAround()
    {

        Vector3 RandomPOsition;
        if (NeedNewPosition)
        {
            RandomPOsition = GetNewRandomPosition(gameObject.transform.position, PositionAraundFlight.transform.position);
            S = gameObject.transform.position;
            F = RandomPOsition;
            if (DriveMRight)
            {
                M = new Vector3(S.x + Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2, gameObject.transform.position.y, Vector3.Distance(gameObject.transform.position, RandomPOsition) * 1f * -1);
            }
            else
            {
                M = new Vector3(S.x - Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2, gameObject.transform.position.y, Vector3.Distance(gameObject.transform.position, RandomPOsition) * 1f * 1);
            }
            DriveMRight = !DriveMRight;
            //SGameOjct.transform.position = S;
            //FGameOjct.transform.position = F;
            //MGameOjct.transform.position = M;
            NeedNewPosition = false;
        }

        Vector3 SM, MF;
        SM = Vector3.Lerp(S, M, T2);
        MF = Vector3.Lerp(M, F, T2);
        gameObject.transform.position = Vector3.Lerp(SM, MF, T2);
        //gameObject.transform.LookAt(F);
        AddToT = Time.fixedDeltaTime / (Vector3.Distance(S, M) / Speed);
        if (TargetToGun)
        {
            Gun.transform.LookAt(TargetToGun.transform.position + new Vector3(0, 1.5f, 0));
        }
        T2 += AddToT;
        gameObject.transform.LookAt(Vector3.Lerp(SM, MF, T2));
        if (T2 >= 1.00)
        {
            T2 = 0.00f;
            NeedNewPosition = true;
        }
    }
    public void Landing()
    {
        gameObject.transform.LookAt(PositionToLending * 10);
        //gameObject.transform.rotation = gameObject.transform.rotation + (Vector3(0, 90, 0));
            if (landing)
        {
            AddToT = Time.fixedDeltaTime / (Vector3.Distance(FlightFrom.transform.position, FlightTo.transform.position) / Speed);
            gameObject.transform.position =  Vector3.Lerp(PositionFrom, PositionToLending, T3);
            T3 += AddToT;
        }
        else
        {
            PositionFrom = gameObject.transform.position;
            landing = true;
        }
    }
    public void FlightOut()
    {
        AddToT = Time.fixedDeltaTime / (Vector3.Distance(FlightFrom.transform.position, PositionToLending) / Speed);
        gameObject.transform.position = Vector3.Lerp(FlightFrom.transform.position, PositionToLending, T);
        T -= AddToT;
    }

    Vector3 GetNewRandomPosition(Vector3 HelicopterPosition, Vector3 Origin)
    {
        Vector3 RandomPOsition;
        do
            {
                RandomPosition_X = UnityEngine.Random.Range(-50f, 50f);

                RandomPOsition = new Vector3(Origin.x + RandomPosition_X, Origin.y, Origin.z);
            }
            while (Vector3.Distance(HelicopterPosition, RandomPOsition) < 50f);
        return RandomPOsition;
    }
}
