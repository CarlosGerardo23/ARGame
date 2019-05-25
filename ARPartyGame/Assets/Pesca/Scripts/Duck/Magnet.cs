using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] RopeBehaviour rope;
    [SerializeField] GameObject point;
    [SerializeField] float distance;
    private void Start()
    {
        
    }

    public void Update()
    {
        Cathed();

    }

    public  void Cathed()
    {
        if(Vector3.Distance(point.transform.position,transform.position)<distance)
        {
            iTween.MoveTo(gameObject, point.transform.position, .1f);
            StartCoroutine(DestroyObject());
        }
           
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }

}
