using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiBehaviour : MonoBehaviour
{
    Vector3 endPoint;

    public Vector3 EndPoint { get { return endPoint; } set { endPoint = value; } }

    private void Update()
    {
        CheckSushiFinsih();
    }
    public void CheckSushiFinsih()
    {
        float distance = Vector3.Distance(endPoint, transform.position);

        if(distance<0.2)
        {
            Debug.Log("Reach");
            Destroy(gameObject);
        }
    }
    public void MoveTo(Vector3 to, float time)
    {
        iTween.MoveTo(gameObject, to, time);
    }
}
