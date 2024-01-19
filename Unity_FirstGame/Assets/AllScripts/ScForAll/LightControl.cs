using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] float Angle = 360.0f;
    [SerializeField] float LightRange = 10.0f;
    void Start()
    {
        Light LinghtComponent = gameObject.GetComponent<Light>();
        if (LinghtComponent)
        {
            LightRange = LinghtComponent.range;
            Angle = LinghtComponent.type == LightType.Spot ?
                LinghtComponent.spotAngle : 360.0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IlluminationController OtherIlluminationController = other.GetComponent<IlluminationController>();
        if (OtherIlluminationController)
        {
            OtherIlluminationController.LightSources.Add(this);

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        IlluminationController OtherIlluminationController = other.GetComponent<IlluminationController>();
        if (OtherIlluminationController)
        {
            OtherIlluminationController.LightSources.Remove(this);

        }
    }
    public float IsObjShine(GameObject Object)
    {
        if (LightRange >= (Object.transform.position - gameObject.transform.position).magnitude)
        {
            if (Vector3.Angle(gameObject.transform.forward, (Object.transform.position - gameObject.transform.position).normalized) <= Angle)
            {
                return 1.0f;//коефіцієнт від дист до обєкта
            }
        }
        return 0.0f;
    }
    void Update()
    {
        
    }
}
