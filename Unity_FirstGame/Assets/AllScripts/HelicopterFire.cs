using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HelicopterFire : MonoBehaviour
{
    [SerializeField]GameObject Ammo;
    float timer = 0.00f;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GameObject Amo;
        if (0.01 <= timer)
        {
            Amo = Instantiate(Ammo, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(Amo, 10f);
            timer = 0.00f;
        }
    }
}
