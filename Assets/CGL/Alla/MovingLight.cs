using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    public Vector3 rotationAxis;
    Vector3 startRotation;
    private void Start()
    {
        
        startRotation = transform.eulerAngles;
    }
    public void SetOffeset(float offset)
    {
        transform.rotation = Quaternion.Euler(startRotation + rotationAxis * (1 - offset));
    }
}
