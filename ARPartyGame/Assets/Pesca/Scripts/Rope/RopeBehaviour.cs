using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 pivot;
    [SerializeField] GameObject centerPoint;
    [SerializeField] GameObject endRope;
    public float pullRadius = 2;
    public float pullForce = 1;
    int x = 0;
    int z = 0;
   [SerializeField] Transform initialRotation;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = pivot;
        
    }
    public void FixedUpdate()
    {
        RopeMovement();
    }

    private void RopeMovement()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        float angleX = transform.localEulerAngles.x;
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        float angleZ = transform.localEulerAngles.z;
        angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

        Vector3 angles = new Vector3(angleX, 0, angleZ);
        float force = Vector3.Distance(angles, initialRotation.position);
        Vector3 forceDirection = initialRotation.position - angles;
        // Debug.Log(forceDirection);
        rb.AddTorque(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
    }


}

