using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SushiData : MonoBehaviour
{
    public int id;
    public bool canMove;
    public float speed;
    public bool destoyObject=false;

    Rigidbody rb;
    Timer time;
    private void Start()
    {
        time = new Timer();
        rb = transform.GetComponentInChildren<Rigidbody>();
        time.SetTime(3f);
    }

    private void Update()
    {
        if(time.StartTimer)
        time.UpdateTime(Time.deltaTime);

        if(destoyObject)
        {
            if (!time.StartTimer)
                Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 endPoint)
    {
        if(canMove)
        {
            Vector3 direction = endPoint - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        
    }

    public bool CheckFinish(Vector3 endPoint,float pointDistance)
    {
        float distance = Vector3.Distance(transform.position, endPoint);

        if(distance< pointDistance)
            return true;
        
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sushi")
        {
            Debug.Log("soy sushi y detecto sushi");
            rb.AddForce(new Vector3(0, 1, -1)*2000, ForceMode.Force);
            time.StartTimer = true;
            destoyObject = true;
        }
    }

}
