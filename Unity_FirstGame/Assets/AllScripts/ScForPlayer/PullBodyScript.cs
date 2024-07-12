using UnityEngine;

public class PullBodyScript : MonoBehaviour
{
    PlayerControler ControlerPlayer;
    [SerializeField] public HingeJoint PlayerHingeJoint;
    [SerializeField] Transform LocalBody;
   
    private void Start()
    {
        //Setup references
        ControlerPlayer = GetComponent<PlayerControler>();
        PlayerHingeJoint = GetComponent<HingeJoint>();
        GetComponent<PlayerToolsToInteraction>().TryToInteractDelegate += TryToInteract;
        
        //All Debug
        if (PlayerHingeJoint) Destroy(PlayerHingeJoint);
    }

    private void TryToInteract(Transform GivenReference)
    {
        if (GivenReference.GetComponent<BoneControler>())
        {
            LocalBody = GivenReference;
            PullBody();

            if (PlayerHingeJoint)
            {
                ControlerPlayer.WhatPlayerHandsDo = HandsPlayer.CarryBody;
            }
            else
            {
                ControlerPlayer.WhatPlayerHandsDo = HandsPlayer.Null;
                ControlerPlayer.GetComponent<SlotControler>().UpWeapon();
            }
            if (ControlerPlayer.WhatPlayerLegsDo != LegsPlayer.SatDown)
            {
                ControlerPlayer.WhatPlayerLegsDo = LegsPlayer.SatDown;
                ControlerPlayer.GetComponent<MovePlayer>().ControlCapsuleColider(true);
            }
            
        }
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
