using UnityEngine;
public class SoundCreatorScript : MonoBehaviour
{
    [SerializeField] public float CurrentNoiceRadiuse = 0.0f;
    [SerializeField] bool DebugingTheWork = false;
    
    public void CreateNoise(float NewNoiceRadius)
    {
        CurrentNoiceRadiuse = NewNoiceRadius;
        if(NewNoiceRadius <= 0.0f)
        {
            return;
        }
        
        Collider[] Colliders;
        Colliders = Physics.OverlapSphere(gameObject.transform.position, CurrentNoiceRadiuse);
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
                        TakerPosition = BaseInfo.MyHeadScript.GetHeadPosition();
                    }
                    float DistanceToNoice = (gameObject.transform.position - BaseInfo.MyHeadScript.GetHeadPosition()).magnitude;
                    RaycastHit[] Hitres = Physics.RaycastAll(gameObject.transform.position, BaseInfo.MyHeadScript.GetHeadPosition());
                    foreach (RaycastHit obj in Hitres)
                    {
                        if (obj.collider.gameObject.transform.root != Colider.gameObject.transform.root && obj.collider.gameObject.isStatic)
                        {
                            DistanceToNoice = DistanceToNoice / 5;
                        }
                    }
                    if (DistanceToNoice >= NewNoiceRadius)
                    {
                        SoundScript.TakeSound(gameObject.transform.position);
                    }
                }
            }
        }
        
        if (DebugingTheWork)
        {
            Debug.Log("Sound Created " + NewNoiceRadius + " == Radius  ");

        }

    }
}

