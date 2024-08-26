using UnityEngine;

public class OnDeadScript : MonoBehaviour
{
    [SerializeField] bool PlayerIsDead = false;

    //Main Scripts
    PlayerControler ControlerPlayer;
    SlotControler ControlerSlot;
    
    //Scripts From UI
    UiControler ControlerUi;
    
    //Scri[ts From Animations
    [SerializeField] IKControlerForPlayer ControlerIKForPlayer;
    
    void Start()
    {
        ControlerPlayer = GetComponent<PlayerControler>();
        ControlerSlot = ControlerPlayer.GetComponent<SlotControler>();
        
        ControlerUi = ControlerPlayer.ControlerUi;

        ControlerPlayer.GetComponent<HpScript>().UpdateOnEvenetDelegate += DeadPlayer;
        
    }

    public bool IsPlayerDead()
    {
        return PlayerIsDead;
    }

    private void DeadPlayer()
    {
        PlayerIsDead = true;

        if (ControlerUi && ControlerUi.ActiveSelfWinPanel())
        {
            ControlerUi.OpenOrCloseInventory(false);
            ControlerUi.DeleteNameOnTable();
            ControlerUi.SetPanelDied();
        }
        else
        {
            Debug.LogError("Not set UIControler !" + gameObject.name);
        }

        if (ControlerIKForPlayer)
        {
            ControlerIKForPlayer.SetupIKWeight(0.0f);        
        }
        else
        {
            Debug.LogError("Not set ControlerIKForPlayer !" + gameObject.name);
        }

        if (ControlerSlot)
        {
            ControlerSlot.PutAwayWeapon();
        }
        else
        {
            Debug.LogError("Not set ControlerSlot !" + gameObject.name);
        }

        if (ControlerPlayer)
        {
            ControlerPlayer.GetComponent<DivertAttention>().StopDropRock();

            ControlerPlayer.WhatPlayerDo = Player.Null;
            ControlerPlayer.StateCamera = CameraPlayer.Null;

            ControlerPlayer.WhatPlayerHandsDo = HandsPlayer.Null;
            ControlerPlayer.WhatPlayerHandsHave = HandsPlayerHave.Null;

            ControlerPlayer.WhatPlayerLegsDo = LegsPlayer.Null;
            ControlerPlayer.WhatSpeedPlayerLegs = SpeedLegsPlayer.Null;
        
        }
        else
        {
            Debug.LogError("Not set ControlerPLayer !" + gameObject.name);
        }

    }
}
