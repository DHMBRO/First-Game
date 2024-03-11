using UnityEngine;

public class PullBodyScript : MonoBehaviour
{
    [SerializeField] PickUp PickUpPlayer;
    [SerializeField] public HingeJoint PlayerHingeJoint;

    private void Start()
    {
        //Setup references
        PickUpPlayer = GetComponent<PickUp>();
        PlayerHingeJoint = GetComponent<HingeJoint>();

        //All Debug
        if (PlayerHingeJoint) Destroy(PlayerHingeJoint);
    }

    public void Working()
    {
        if (!PickUpPlayer)
        {
            Debug.Log("Not set PickUpPlayer");
            return;
        }

        if (PlayerHingeJoint)
        {
            Destroy(PlayerHingeJoint);
            PlayerHingeJoint = null;
        }
        else if (PickUpPlayer.ObjectToBeLifted && PickUpPlayer.ObjectToBeLifted.GetComponent<HpScript>())
        {
            PlayerHingeJoint = gameObject.AddComponent<HingeJoint>();
            PlayerHingeJoint.connectedBody = PickUpPlayer.ObjectToBeLifted.GetComponent<Rigidbody>();
            PlayerHingeJoint.axis = new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (PickUpPlayer.ObjectToBeLifted && !PickUpPlayer.ObjectToBeLifted.GetComponent<HpScript>()) Debug.Log(PickUpPlayer.ObjectToBeLifted.name);
        
    }


}
