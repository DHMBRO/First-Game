using UnityEngine;
using System.Collections.Generic;

public class RagdollControler : MonoBehaviour
{
    [SerializeField] private HpScript HpScriptPlayer;
    [SerializeField] private Animator MyAnimator;
    [SerializeField] public SetRagdoll SetRagdollDelegat;

    [SerializeField] List<GameObject> AllBoneObjects = new List<GameObject>();
    

    private void Start()
    {
        MyAnimator = gameObject.GetComponent<Animator>();

        for (int i = 0;i < AllBoneObjects.Count;i++)
        {
            RagdollEnDis LocalRagdollEnDis = AllBoneObjects[i].AddComponent<RagdollEnDis>();
            
            LocalRagdollEnDis.MyRagdollControler = GetComponent<RagdollControler>();
            LocalRagdollEnDis.GetReferences();

            if (i == AllBoneObjects.Count - 1) break;
        }
        
        DisebleRagdoll();

    }

    public void EnableRagdoll()
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            SetRagdollDelegat(true);
            MyAnimator.enabled = false;
        }
        else Debug.Log("RagdollControler desnt have some references " + gameObject.name);
    }

    public void DisebleRagdoll()
    {
        if (SetRagdollDelegat != null && MyAnimator)
        {
            SetRagdollDelegat(false);
            MyAnimator.enabled = true;
        }
        else Debug.Log("RagdollControler desnt have some references " + gameObject.name);
    }
}   
