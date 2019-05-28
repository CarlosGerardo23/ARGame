using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] string keyTable;
    [SerializeField] GameObject prefab;
    List<GameObject> listObject;
    int lenght;

    private void Start()
    {
        lenght = HighScoreController.GetLenghtTableList(keyTable);
        listObject = new List<GameObject>();
        AddElemnts();

    }
    private void AddElemnts()
    {
        for (int i = 0; i < lenght; i++)
        {
            GameObject player = Instantiate(prefab, transform);
            int score = 0;
            string name = "";
            player.SetActive(true);
            HighScoreController.GetElementsList(keyTable, i, out score, out name);
            player.GetComponent<TablePlayer>().SetValue(name, (i+1).ToString(), score.ToString());
            listObject.Add(player);
        }
    }
}
