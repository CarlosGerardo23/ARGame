using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBehaviour : MonoBehaviour
{
    [SerializeField] ChopstickData chopstick;
    [SerializeField] SushiController controller;
    [SerializeField] UISushi sushiUi;
    [SerializeField] List<GameObject> suhi;
    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] UnityEngine.UI.Text currentsSushi;
    public List<int> idSushi;


    public float score = 0;

    Renderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        SetIDSushi();
        sushiUi.UpdateUI();
    }

    private void Update()
    {
        if (myRenderer.isVisible)
        {
            CheckNewSushi();
        }
        CheckScore();
        UpdateScore();
    }

    private void ClearCup()
    {
        for (int i = 0; i < suhi.Count; i++)
        {
            Destroy(suhi[i]);
        }
        suhi.Clear();
        SetIDSushi();
        sushiUi.UpdateUI();

    }

    private void CheckNewSushi()
    {
        bool foundSushi = false;
        int indexNewSuhi = -1; ;
        if (chopstick.letGoSushi)
        {
            for (int i = 0; i < idSushi.Count; i++)
            {
                if (idSushi[i] == chopstick.sushi.GetComponent<SushiData>().id)
                {
                    score += 3;
                    chopstick.letGoSushi = false;
                    return;
                   // suhi.Add(chopstick.sushi);
                    //foundSushi = true;
                    //currentsSushi.text = (suhi.Count).ToString();
                    //break;
                }
            }
            score+= 1;
            //if (!foundSushi)
            //{
            //    ClearCup();
            //}




            chopstick.letGoSushi = false;
        }
    }

    private void SetIDSushi()
    {
        idSushi = new List<int>();
        while (idSushi.Count < 3)
        {
            int newId = Random.Range(0, controller.sushiPrefabs.Count);
            bool sameNumer = false;
            for (int i = 0; i < idSushi.Count; i++)
            {
                if (idSushi[i] == newId)
                {
                    sameNumer = true;
                    break;
                }
            }
            if (sameNumer)
                continue;
            idSushi.Add(newId);
        }
    }
    void CheckScore()
    {
        //if (suhi.Count >= 3)
        //{
        //    score += 1;
        //    ClearCup();
        //    currentsSushi.text = "0";
        //}
    }
    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
