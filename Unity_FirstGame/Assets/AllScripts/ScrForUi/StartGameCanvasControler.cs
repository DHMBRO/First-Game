using System.Collections.Generic;
using UnityEngine;

public class StartGameCanvasControler : MonoBehaviour
{    
    [SerializeField] List<GameObject> AllPanels = new List<GameObject>();


    private void Start()
    {
        for (int i = 0;i < AllPanels.Count;i++)
        {
            if (i == 0)
            {
                AllPanels[i].SetActive(true);
            }
            else
            {
                AllPanels[i].SetActive(false);
            }
        }
    }

    public void SetPanel(int CountOfPanel)
    {
        for (int i = 0;i < AllPanels.Count;i++)
        {
            if(i == CountOfPanel)
            {
                AllPanels[i].SetActive(true);
            }
            else
            {
                AllPanels[i].SetActive(false);
            }
        }
    }

    

}
