using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] float Angle = 360.0f;
    [SerializeField] float LightRange = 10.0f;
    [SerializeField] bool DistanceDepend;
    
    void Start()
    {
        Light LinghtComponent = gameObject.GetComponent<Light>();
        if (LinghtComponent)
        {
            LightRange = LinghtComponent.range;
            Angle = LinghtComponent.type == LightType.Spot ?
                LinghtComponent.spotAngle/2.0f : 360.0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IlluminationController OtherIlluminationController = other.GetComponentInParent<IlluminationController>();
        if (OtherIlluminationController)
        {
            OtherIlluminationController.LightSources.Add(this);
            
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        IlluminationController OtherIlluminationController = other.GetComponentInParent<IlluminationController>();
        if (OtherIlluminationController)
        {
            OtherIlluminationController.LightSources.Remove(this);

        }
    }
    public float HowMuchObjShine(GameObject Object)
    {
        IlluminationController IlluContr = Object.GetComponentInParent<IlluminationController>();
        if (!IlluContr || !IlluContr.HeadObject)
        {
            return 0.0f;
        }







        if (LightRange >= (IlluContr.HeadObject.transform.position - gameObject.transform.position).magnitude)
        {
            if (Vector3.Angle(gameObject.transform.forward, (IlluContr.HeadObject.transform.position - gameObject.transform.position).normalized) <= Angle)
            {
                RaycastHit [] Hitresults = Physics.RaycastAll(gameObject.transform.position, IlluContr.HeadObject.transform.position - gameObject.transform.position);
                Debug.DrawLine(gameObject.transform.position, IlluContr.HeadObject.transform.position);
                foreach(RaycastHit Hitres in Hitresults)//Raycast from Light to Obj to check there is no obj between them
                {
                    if (Hitres.collider.gameObject.transform.root == gameObject.transform.root)
                    {
                        continue;
                    }
                    if (Hitres.collider.gameObject.transform.root == IlluContr.HeadObject.transform.root)
                    {
                        return DistanceDepend ?  Mathf.Clamp(1 - ( (gameObject.transform.position - IlluContr.HeadObject.transform.position).magnitude / LightRange ) ,0.0f, 1.0f) : 1.0f;
                    }
                    break;
                }
            }
        }
        return 0.0f;
    }
    void Update()
    {
        
    }
}