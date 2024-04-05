using UnityEngine;
using System.Collections.Generic;

public class RagdollControler : MonoBehaviour
{
    [SerializeField] private Animator MyAnimator;

    [SerializeField] private BoxCollider ColiderToInteraction;
    [SerializeField] private BoneControler ControlerBone;

    [SerializeField] public SetRagdoll SetRagdollDelegat;

    [SerializeField] List<Transform> AllBones = new List<Transform>();
    
    private void Start()
    {
        MyAnimator = gameObject.GetComponent<Animator>();
        List<Rigidbody> AllBonesObject = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());
        

        for (int i = 0;i < AllBonesObject.Count;i++)
        {
            if (AllBonesObject[i].tag != "Another")
            {
                RagdollEnDis LocalRagdollEnDis = AllBonesObject[i].gameObject.AddComponent<RagdollEnDis>();

                LocalRagdollEnDis.gameObject.layer = LayerMask.NameToLayer("Bone");
                LocalRagdollEnDis.GetReferences(this);
                AllBones.Add(AllBonesObject[i].transform);
                
            }
        }

        SetRagdol(false);
    }

    public void SetRagdol(bool Enable)
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            ColiderToInteraction.enabled = Enable;
            ColiderToInteraction.enabled = Enable;
            
            SetRagdollDelegat(Enable);
            MyAnimator.enabled = !Enable;
        }
        else Debug.Log("RagdollControler doesnt have some references " + gameObject.name);
    }


}   
