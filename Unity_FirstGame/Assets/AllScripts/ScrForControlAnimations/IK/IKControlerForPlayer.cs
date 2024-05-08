using UnityEngine;

public class IKControlerForPlayer : MonoBehaviour
{
    PlayerControler ControlerPlayer;
    ControlerAnimationsPlayer PlayerControlerAnimations;

    void Start()
    {
        ControlerPlayer = GetComponentInParent<PlayerControler>();
        PlayerControlerAnimations = GetComponent<ControlerAnimationsPlayer>(); ;
    }

    void Update()
    {
        
    }
    
}
