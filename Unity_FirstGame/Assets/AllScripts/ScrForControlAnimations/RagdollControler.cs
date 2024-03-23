using UnityEngine;
using System.Collections.Generic;

public class RagdollControler : MonoBehaviour
{
    [SerializeField] private HpScript HpScriptPlayer;
    [SerializeField] private Animator MyAnimator;
    [SerializeField] public SetRagdoll SetRagdollDelegat;

    private void Start()
    {
        
        MyAnimator = gameObject.GetComponent<Animator>();
        List<Rigidbody> AllBonesObject = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());
        
        for (int i = 0;i < AllBonesObject.Count;i++)
        {
            RagdollEnDis LocalRagdollEnDis = AllBonesObject[i].gameObject.AddComponent<RagdollEnDis>();
            
            LocalRagdollEnDis.MyRagdollControler = this;
            LocalRagdollEnDis.GetReferences();

        }

        //EnableRagdoll();
        DisebleRagdoll();
    }

    /*
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.P))
        {
            EnableRagdoll();
        }
        else if (Input.GetKey(KeyCode.O))
        {
            DisebleRagdoll();
        }  
    }
    */

    public void EnableRagdoll()
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            SetRagdollDelegat(true);
            MyAnimator.enabled = false;
        }
        else Debug.Log("RagdollControler doesnt have some references " + gameObject.name);
    }

    public void DisebleRagdoll()
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            SetRagdollDelegat(false);
            MyAnimator.enabled = true;
        }
        else Debug.Log("RagdollControler doesnt have some references " + gameObject.name);
    }
}   
