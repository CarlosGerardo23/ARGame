using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISushi : MonoBehaviour
{
    [SerializeField] CupBehaviour cup;
    [SerializeField] SushiController sushi;
    [SerializeField] List<GameObject> mySushi;
    [SerializeField] List<Transform> position;

   
    public void UpdateUI()
    {
        DisbaleSushi();
        for (int i = 0; i < cup.idSushi.Count; i++)
        {
            mySushi[cup.idSushi[i]].SetActive(true);
            mySushi[cup.idSushi[i]].transform.position = position[i].position;
        }
    }
    private void DisbaleSushi()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mySushi.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
