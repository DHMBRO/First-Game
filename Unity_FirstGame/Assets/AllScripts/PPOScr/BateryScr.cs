using UnityEngine;

public class BateryScr : MonoBehaviour
{
    [SerializeField] InteractioWithObject Computer;
    [SerializeField] PPOscr ScrPPO;

    void Start()
    {
        if(!Computer || !ScrPPO)
        {
            Debug.Log("Not set ScrPPO or Computer");
            return;
        }
    }

    
}
