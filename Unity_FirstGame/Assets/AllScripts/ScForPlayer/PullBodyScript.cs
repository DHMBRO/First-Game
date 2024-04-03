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
        if (PlayerHingeJoint || LocalBody)
        {
            CanWork = true;
        }
        else CanWork = false;

        return CanWork;

    }

    public void Work()
    {
        if(CanWork)
        {
            PullBody();
        }
    }

    private bool ChekToInteraction(Transform GivenReference)
    {
        if (GivenReference && GivenReference.GetComponent<BoneControler>())
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

    private void PullBody()
    {
        BoneControler LocalBoneControler = null;
        Transform LocalLimb = null;
        Rigidbody LimbRigidbody = null;


        if (PlayerHingeJoint)
        {
            Destroy(PlayerHingeJoint);
            PlayerHingeJoint = null;
        }
        else if (LocalBody)
        {
            LocalBoneControler = LocalBody.GetComponent<BoneControler>();
            LocalLimb = LocalBoneControler.ReturnLimb(this.transform);
            LimbRigidbody = LocalLimb.GetComponent<Rigidbody>();

            if (!LimbRigidbody) LimbRigidbody = LocalLimb.gameObject.AddComponent<Rigidbody>();
            PlayerHingeJoint = gameObject.AddComponent<HingeJoint>();

            PlayerHingeJoint.connectedBody = LimbRigidbody;
            PlayerHingeJoint.axis = new Vector3(0.0f, 1.0f, 0.0f);
        }
    }

}
