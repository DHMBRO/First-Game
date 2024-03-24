using UnityEngine;
using System.Collections.Generic;

public class RagdollControler : MonoBehaviour
{
    [SerializeField] private Animator MyAnimator;
    [SerializeField] public SetRagdoll SetRagdollDelegat;

    [SerializeField] bool Enable = false;

    private void Start()
    {
        MyAnimator = gameObject.GetComponent<Animator>();
        List<Rigidbody> AllBonesObject = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());
        
        for (int i = 0;i < AllBonesObject.Count;i++)
        {
            RagdollEnDis LocalRagdollEnDis = AllBonesObject[i].gameObject.AddComponent<RagdollEnDis>();
            LocalRagdollEnDis.GetReferences(this);
        }

        SetRagdol(false);
    }

    private void Update()
    {
        //SetRagdol(Enable);
        
    }

    public void SetRagdol(bool Enable)
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            SetRagdollDelegat(Enable);
            MyAnimator.enabled = !Enable;

            GetComponent<BoxCollider>().enabled = Enable;
            GetComponent<BoneControler>().enabled = Enable;
            GetComponent<ScrForAllLoot>().enabled = Enable;
        }
        else Debug.Log("RagdollControler doesnt have some references " + gameObject.name);
    }


}   
