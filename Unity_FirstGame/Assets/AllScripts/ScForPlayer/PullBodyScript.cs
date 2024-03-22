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
        GetComponent<PlayerToolsToInteraction>().PlayerSetInteractionDelegat += ChekToInteraction;

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

    private void ChekToInteraction(Transform GivenReference)
    {
        if (GivenReference.GetComponent<HpScript>())
        {
            CanWork = true;
            LocalBody = GivenReference;
        }
        else
        {
            LocalBody = null;
            CanWork = false;
        }
    }

    private void PullBody(Transform LocalBody)
    {
        
        if (PlayerHingeJoint)
        {
            Destroy(PlayerHingeJoint);
            PlayerHingeJoint = null;
        }
        else if (LocalBody.GetComponent<HpScript>())
        {
            PlayerHingeJoint = gameObject.AddComponent<HingeJoint>();
            PlayerHingeJoint.connectedBody = LocalBody.GetComponent<Rigidbody>();
            PlayerHingeJoint.axis = new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (LocalBody && !LocalBody.GetComponent<HpScript>()) Debug.Log(LocalBody.name);
        
        
    }


}
