using UnityEngine;

public class PullBodyScript : MonoBehaviour
{
    [SerializeField] public HingeJoint PlayerHingeJoint;

    [SerializeField] Transform LocalBody;
    [SerializeField] bool CanWork = false;

    private void Start()
    {
        //Setup references
        PlayerHingeJoint = GetComponent<HingeJoint>();
        GetComponent<PlayerToolsToInteraction>().PlayerChekToInteractionDelegat += ChekToInteraction;

        //All Debug
        if (PlayerHingeJoint) Destroy(PlayerHingeJoint);
    }

    public bool CanEnable()
    {
        if (LocalBody)
        {
            CanWork = true;
        }
        else CanWork = false;

        return CanWork;

    }

    public void Work()
    {
        if(CanWork && LocalBody)
        {
            PullBody(LocalBody);
        }
    }

    private bool ChekToInteraction(Transform GivenReference)
    {
        if (GivenReference.GetComponent<BoneControler>())
        {
            CanWork = true;
            LocalBody = GivenReference;
        }
        else
        {
            LocalBody = null;
            CanWork = false;
        }

        return LocalBody != null;
    }

    private void PullBody(Transform LocalBody)
    {
        BoneControler LocalBoneControler = LocalBody.GetComponent<BoneControler>();
        
        Transform LocalLimb = LocalBoneControler.ReturnLimb(this.transform);
        Rigidbody LimbRigidbody = LocalLimb.GetComponent<Rigidbody>();

        if (PlayerHingeJoint)
        {
            Destroy(PlayerHingeJoint);
            PlayerHingeJoint = null;
        }
        else if (LocalLimb)
        {
            if (!LimbRigidbody) LimbRigidbody = LocalLimb.gameObject.AddComponent<Rigidbody>();
            PlayerHingeJoint = gameObject.AddComponent<HingeJoint>();

            PlayerHingeJoint.connectedBody = LocalLimb.GetComponent<Rigidbody>();
            PlayerHingeJoint.axis = new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (!LocalLimb) Debug.Log(LocalBody.name);
        

    }


}
