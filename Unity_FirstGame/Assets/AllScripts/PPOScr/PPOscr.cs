using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPOscr : MonoBehaviour, IInteractionWithObjects
{
    GameObject Turget;
    bool PPOIsOn = true;
    [SerializeField] float RSpeed;
    [SerializeField] GameObject BulletOrRocket;
    [SerializeField] GameObject FierFrom;
    enum HorisontalOrVertical
    {
        Horisontal,
        Vertical
    }
    [SerializeField] HorisontalOrVertical RotateTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool AuditToUse()
    {
        return true;
    }
    public void Interaction()
    {
        PPOIsOn = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Turget && PPOIsOn)
        {
            Quaternion ToTargetRotation = Quaternion.LookRotation(Turget.transform.position - gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, ToTargetRotation, RSpeed);
            if (RotateTo == HorisontalOrVertical.Horisontal)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, transform.localPosition.y, 0);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Euler(transform.localPosition.x, transform.localPosition.y, 0);
                GameObject bullet = Instantiate(BulletOrRocket, FierFrom.transform.position, gameObject.transform.rotation);
                Destroy(bullet, 10f);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Helicopter"))
        {
            Turget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Helicopter"))
        {
            Turget = null;
        }
    }
}
