using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePosition : MonoBehaviour
{
    [SerializeField] GameObject gameHolder;
    // Update is called once per frame
    void Start()
    {
        gameHolder.transform.parent = transform;
    }
}
