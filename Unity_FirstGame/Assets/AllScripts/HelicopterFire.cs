using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HelicopterFire : MonoBehaviour
{
    [SerializeField]GameObject Ammo;
    float timer = 0.00f;
    [SerializeField] HelicopterScr Hscr; 
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Hscr.State == HelicopterScr.States.RoamAround)
        {
            timer += Time.deltaTime;
            //Debug.Log("HelicopterFire" + " " + timer);
            if (0.01f <= timer)
            {
                GameObject Amo = Instantiate(Ammo, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(Amo, 10f);
                timer = 0.00f;
            }
        }
    }
}
