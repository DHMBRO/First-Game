using System.Collections.Generic;
using System.Xml.Serialization;
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
        if(!GivenReference)
        {
            return;
        }

        if (GivenReference.GetComponent<BoneControler>())
        {
            LocalBody = GivenReference;
            PullBody();

            if (PlayerHingeJoint)
            {
                ControlerPlayer.WhatPlayerHandsDo = HandsPlayer.CarryBody;
                ControlerPlayer.GetComponent<SlotControler>().PutAwayWeapon();
            }
            else
            {
                ControlerPlayer.WhatPlayerHandsDo = HandsPlayer.Null;
                ControlerPlayer.GetComponent<SlotControler>().ReturnWeaponInHand();
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
            StopPullingBody();
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
    
    private void StopPullingBody()
    {
        Transform ConectedBody = PlayerHingeJoint.connectedBody.GetComponent<Transform>();
        Transform MainParentObject = ConectedBody;
        Transform Other = null;

        while (MainParentObject.parent != null)
        {
            MainParentObject = MainParentObject.parent;   
            
            if(MainParentObject.name == "mixamorig:Hips")
            {
                Other = MainParentObject;
            }
        }

        MainParentObject.position = ConectedBody.position;
        ConectedBody.localPosition = Vector3.zero;
        Other.localPosition = Vector3.zero;

        Destroy(PlayerHingeJoint);
        PlayerHingeJoint = null;

    }

}
