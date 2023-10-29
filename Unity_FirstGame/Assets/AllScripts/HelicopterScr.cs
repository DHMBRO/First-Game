using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class HelicopterScr : MonoBehaviour
{
    [SerializeField] GameObject FlightFrom;
    [SerializeField] GameObject FlightTo;
    [SerializeField] Vector3 PositionToLending;
    [SerializeField] int Speed;
    [SerializeField] GameObject MGameOjct;
    [SerializeField] GameObject M2GameOjct;
    [SerializeField] GameObject FGameOjct;
    [SerializeField] GameObject SGameOjct;
    [SerializeField] Vector3 PositionAraundFlight;
    float T = 0.00f;
    float AddToT;
    bool landing = false;
    float T2 = 0.00f;
    float T3 = 0.00f;
    Vector3 PositionFrom;
    public float RandomPosition_X;
    public float RandomPosition_Z;
    Vector3 RandomPOsition;
    bool NeedNewPosition = true;
    Vector3 S;
    Vector3 F;
    Vector3 M;

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
    States State;
    // Start is called before the first frame update
    void Start()
    {
        State = States.FlightToPlayer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
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
                Debug.Log("FlightToPlayer");
                break;
            case States.RoamAround:
                RoamAround();
                Debug.Log("RoamAround");
                break;
            case States.Landing:
                Debug.Log("Landing");
                Landing();
                break;
            case States.FlightOut:
                Debug.Log("FlightOut");
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
        if (NeedNewPosition)
        {
            do
            {
                RandomPosition_X = UnityEngine.Random.Range(0f, 100f);

                RandomPOsition = new Vector3(RandomPosition_X, gameObject.transform.position.y, gameObject.transform.position.z);

            }
            while (Vector3.Distance(gameObject.transform.position, RandomPOsition) < 30f);
            S = gameObject.transform.position;
            F = RandomPOsition;
            if (RandomPosition_X < 0)
            {
                M = new Vector3(Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2, gameObject.transform.position.y, Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2 * -1);
            }
            else
            {
                M = new Vector3(Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2, gameObject.transform.position.y, Vector3.Distance(gameObject.transform.position, RandomPOsition) / 2 * 1);
            }
            SGameOjct.transform.position = S;
            FGameOjct.transform.position = F;
            MGameOjct.transform.position = M;
            NeedNewPosition = false;
                
        }
        else
        {
            switch (StatesFlght)
            {
                case StatesFlight.FromSToM:
                    AddToT = Time.fixedDeltaTime / (Vector3.Distance(S, M) / Speed);
                    gameObject.transform.position = gameObject.transform.position + Vector3.Lerp(S, M, T2);
                    T2 += AddToT;
                    if (T2 >= 1.00)
                    {
                        T2 = 0.00f;
                        StatesFlght = StatesFlight.FromMToM;
                    }
                    break;
                case StatesFlight.FromMToM:
                    AddToT = Time.fixedDeltaTime / (Vector3.Distance(M, M) / Speed);
                    gameObject.transform.position = gameObject.transform.position + Vector3.Lerp(M, M, T2);
                    T2 += AddToT;
                    if (T2 >= 1.00)
                    {
                        T2 = 0.00f;
                        StatesFlght = StatesFlight.FromMToF;
                    }
                    break;
                case StatesFlight.FromMToF:
                    AddToT = Time.fixedDeltaTime / (Vector3.Distance(M, F) / Speed);
                    gameObject.transform.position = gameObject.transform.position + Vector3.Lerp(M, F, T2);
                    T2 += AddToT;
                    if (T2 >= 1.00)
                    {
                        T2 = 0.00f;
                        StatesFlght = StatesFlight.FromSToM;
                        NeedNewPosition = true;
                    }
                    break;

            }
        }
    }
    public void Landing()
    {
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
}