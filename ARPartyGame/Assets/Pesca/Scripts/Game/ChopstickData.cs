using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum State { CENTER, RIGHT, LEFT };

public class ChopstickData : MonoBehaviour
{

    [SerializeField] bool vuforiaActive;
    [SerializeField] float speed;
    [SerializeField] float VuforiaSpeed;
    
    [SerializeField] GameObject plate;
    [SerializeField] Camera cam;
    [SerializeField] Transform camReference;
    [SerializeField] UnityEngine.UI.Button grab;
    public State chopstickState;
    public bool haveSushi = false;
    public bool letGoSushi = false;
    public GameObject sushi;
    // Start is called before the first frame update
    void Start()
    {
        chopstickState = State.CENTER;
        grab.interactable = false;
    }

    private void Update()
    {
        if (haveSushi)
            MoveToCamera();
    }
    public void MoveTo(Vector3 endPoint)
    {
        float speedResult = 0;
        if (vuforiaActive)
            speedResult = VuforiaSpeed;
        else
            speedResult = speed;
        Vector3 direction = endPoint - transform.position;
        transform.Translate(direction.normalized * speedResult * Time.deltaTime);
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
            grab.interactable = true;
            sushi = other.gameObject;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sushi")
        {
            grab.interactable = false;
            sushi = null;

        }
    }

    public void GrabSushi()
    {
        sushi.transform.parent = transform;
        transform.parent = cam.gameObject.transform;
        haveSushi = true;
        letGoSushi = false;
    }

    public void MoveToCamera()
    {
        if(!CheckFinish(camReference.position,.1f))
        {
            MoveTo(camReference.position);
        }     
    }

    public void ReleaseSushi()
    {
        if(haveSushi)
        {
            sushi.transform.parent = plate.transform;
            sushi.GetComponent<Rigidbody>().useGravity=true;
            haveSushi = false;
            grab.interactable = false;
            letGoSushi = true;
        }
    }
}
