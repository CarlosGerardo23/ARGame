using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class SaveScore : MonoBehaviour
{
    [SerializeField] string keyTable;
    [SerializeField] Text name;
    [SerializeField] Text score;
    // Use this for initialization
    void Start()
    {
        HighScoreController.CreatTable(keyTable);
    }

    public void Save()
    {

        int scoreInt = GetInt(score.text);
        HighScoreController.SetPersistanceKey(keyTable);
        HighScoreController.AddElementToTable(keyTable, scoreInt, name.text);

    }

    int GetInt(string scoreText)
    {
        int result = 0;
        string scoreResult = "";
        for (int i = 0; i < scoreText.Length; i++)
        {
            if (Char.IsDigit(scoreText[i]))
                scoreResult += scoreText[i];
        }
        if (!Int32.TryParse(scoreResult, out result))
        {
            result = -1;
        }
        return result;
    }
}
