using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiSpawner : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

   [SerializeField] GameObject currentSushi;
    GameObject newSushi = null;
    // Start is called before the first frame update

    private void Start()
    {
        startPoint = transform;
        StartCoroutine(CreateSushi());
    }
    private void Update()
    {
        
    }

    IEnumerator CreateSushi()
    {
        yield return new WaitForSeconds(.5f);
        GameObject sushi = Instantiate(currentSushi, startPoint.position, Quaternion.identity);
        sushi.transform.parent = transform;
        sushi.transform.localScale = new Vector3(.2f, .2f, .2f);
        sushi.GetComponent<SushiBehaviour>().EndPoint = endPoint.position;
        sushi.GetComponent<SushiBehaviour>().MoveTo(endPoint.position, 4f);
    }
}
