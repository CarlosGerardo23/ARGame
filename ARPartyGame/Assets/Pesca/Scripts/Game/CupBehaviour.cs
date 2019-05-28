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
        if (chopstick.letGoSushi)
        {
            for (int i = 0; i < idSushi.Count; i++)
            {
                if (idSushi[i] == chopstick.sushi.GetComponent<SushiData>().id)
                {
                    suhi.Add(chopstick.sushi);
                    foundSushi = true;
                    break;
                }
            }
            if (!foundSushi)
            {
                ClearCup();
            }
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
        if(suhi.Count>=3)
        {
            score += 1;
            suhi.Clear();
            SetIDSushi();
            sushiUi.UpdateUI();
        }                
    }
    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
}
