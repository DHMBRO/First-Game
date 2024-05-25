using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] float _currentSpeed = 0.0f;
    Vector3 _lastPosition;

    float _chekTime = 0.0f;
    float _delay = 1.0f;

    void Update()
    {
        if (Time.time >= _chekTime)
        {
            _chekTime = _delay + Time.time;
            
            _currentSpeed = (_lastPosition - transform.position).magnitude;
            //Debug.Log(_currentSpeed);
            _lastPosition = transform.position; 
            
        }

    }
}
