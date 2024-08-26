using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    
    [SerializeField] bool DebugingTheWork = false;
    
    public void CreateNoise(float NoiceRadius)
    {
        
        if(NoiceRadius <= 0.0f)
        {
            return;
        }
        
        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, NoiceRadius);
        foreach (Collider Colider in Colliders)
        {
            if (Colider.gameObject && Colider.gameObject != gameObject)
            {
                SoundTakerScript SoundScript = Colider.gameObject.GetComponentInParent<SoundTakerScript>();
                if (SoundScript)
                {
                    Vector3 TakerPosition = Colider.gameObject.transform.position;
                    BaseInformationScript BaseInfo = Colider.gameObject.GetComponentInParent<BaseInformationScript>();

                    if (BaseInfo)
                    {
                        if (BaseInfo.MyHeadScript == null)
                        {
                            //Debug.Log("Not set BaseInfo.MyHeadScript ! " + gameObject.name);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    TakerPosition = BaseInfo.MyHeadScript.GetHeadPosition();
                    
                    float DistanceToListener = (gameObject.transform.position - BaseInfo.MyHeadScript.GetHeadPosition()).magnitude;
                    float CurrentNoiceRadius = NoiceRadius;
                    RaycastHit[] Hitres = Physics.RaycastAll(gameObject.transform.position, BaseInfo.MyHeadScript.GetHeadPosition());
                    foreach (RaycastHit obj in Hitres)
                    {
                        if (obj.collider.gameObject.transform.root != Colider.gameObject.transform.root && obj.collider.gameObject.isStatic)
                        {
                            CurrentNoiceRadius /= 5;
                        }
                    }
                    if (DistanceToListener <= CurrentNoiceRadius)
                    {
                            SoundScript.TakeSound(gameObject.transform.position);
                    }
                }
            }
        }
        
        if (DebugingTheWork)
        {
           // Debug.Log("Sound Created " + NoiceRadius + " == Radius  ");

        }

    }
}

