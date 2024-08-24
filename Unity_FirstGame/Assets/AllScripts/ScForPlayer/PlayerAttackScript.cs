using UnityEngine;


public class PlayerAttackScript : MonoBehaviour
{
  //  public bool CanInstantKill;
    
    [SerializeField] private float MaxKillDistance = 3.0f;
    [SerializeField] private float MinKillDistance = 0.5f;
    [SerializeField] private float MoveBackDistance = 0.0f;
    [SerializeField] private float TimeOfAnimation = 7.0f;
    [SerializeField] Transform Cube;
    public UpdateOnEvent EventDelegate;


    Collider[] Colliders;
    protected Animator PlayerAnimator;
    protected PlayerControler PlayerController;
    [SerializeField]  public GameObject PlayerHead;

    void Start()
    {
        
        PlayerAnimator = gameObject.GetComponentInChildren<Animator>();
        PlayerController = gameObject.GetComponent<PlayerControler>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
              
            float HalfExtents = (MaxKillDistance - MinKillDistance) / 2;

            Colliders = Physics.OverlapBox(gameObject.transform.position + 1.0f * gameObject.transform.forward, new Vector3(HalfExtents, HalfExtents, HalfExtents));
            foreach (Collider Collider in Colliders)
            {

                HpScript HpScript = Collider.gameObject.GetComponent<HpScript>();
                LocateScript ZombieLocateScript = Collider.gameObject.GetComponent<LocateScript>();
                InfScript InfoScript = Collider.gameObject.GetComponent<InfScript>();
                
                if (HpScript && ZombieLocateScript && InfoScript)
                {
                    if (InfoScript.IsObjectFromBehinde(gameObject))
                    {
                       if (ZombieLocateScript.WhatForvardToMe(PlayerHead) == Collider.gameObject)
                       {
                            PlayerController.StealthKilling = true;
                            HpScript.StelthKill = true;

                            StealthKill(Collider.gameObject);
                            Invoke("OnStealthAnimateEnd", TimeOfAnimation);                            
                            HpScript.Invoke("InstanceKill", TimeOfAnimation);
                            
                            HpScript.StelthKill = false;                            
                            break;
                       }
                    }
                }
            }
        }
    }
    
    public void SetPlayerAnimation(string TriggerName)
    {
        PlayerAnimator.SetTrigger(TriggerName);
    }

    public void StealthKill(GameObject Enemy)
    {
        InfScript InfScript = Enemy.GetComponent<InfScript>();

        gameObject.transform.eulerAngles = Enemy.transform.eulerAngles;
        gameObject.transform.position = Enemy.transform.position + -(Enemy.transform.forward * MoveBackDistance);
        
        SetPlayerAnimation("StealthKill");
        if (InfScript)
        {
            InfScript.StelthDead();
        }
        
    }
    void OnStealthAnimateEnd()
    {
        PlayerController.StealthKilling = false;
        
    }
}


