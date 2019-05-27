using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State { CENTER, RIGHT, LEFT };

public class ChopstickData : MonoBehaviour
{
  
    [SerializeField] float speed;
    [SerializeField] GameObject plate;
    [SerializeField] Camera cam;
    public State chopstickState;

    bool haveSushi = false;
    GameObject sushi;
    // Start is called before the first frame update
    void Start()
    {
        chopstickState = State.CENTER;
    }

    public void MoveTo(Vector3 endPoint)
    {
        Vector3 direction = endPoint - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public bool CheckFinish(Vector3 endPoint, float pointDistance)
    {
        float distance = Vector3.Distance(transform.position, endPoint);

        if (distance < pointDistance)
            return true;

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Sushi")
        {
            other.gameObject.transform.parent = transform;
            sushi = other.gameObject;
            transform.parent = cam.gameObject.transform;
            Debug.Log("Detecto sushi");
            haveSushi = true;
        }
    }

    public void ReleaseSushi()
    {
        if(haveSushi)
        {
           sushi.transform.parent = plate.transform;
            sushi.GetComponent<Rigidbody>().useGravity=true;
        }
    }
}
