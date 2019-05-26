using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiGameManager : MonoBehaviour
{
    [SerializeField] GameObject sushiPrefab;
    [SerializeField] GameObject startPointsHolder;
    [SerializeField] GameObject endPointsHolder;

    [SerializeField] List<GameObject> sushiGame;
    List<Transform> startPoints;
    List<Transform> endPoints;

    // Start is called before the first frame update
    void Start()
    {
        sushiGame = new List<GameObject>();
        startPoints = new List<Transform>();
        endPoints = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
