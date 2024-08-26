using UnityEngine;

public class ActiveWinPanel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerControler ControlerPlayer = other.GetComponent<PlayerControler>();

        if (!ControlerPlayer) return;

        ControlerPlayer.DisableAllSoundSources();
        ControlerPlayer.ControlerUi.SetPanelWin();

    }
}
