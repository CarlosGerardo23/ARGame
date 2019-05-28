using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController 
{
    private static string persistenceKey;
    public static void CreatTable(string keyTable)
    {
        if (!PlayerPrefs.HasKey(keyTable))
        {
            HighScoreTableData tableData = new HighScoreTableData();
            tableData.tableDataList = new List<HighScoreData>();
            string json = JsonUtility.ToJson(tableData);
            PlayerPrefs.SetString(keyTable, json);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Ya existe esta llave");
        }
    }

    public static void SetPersistanceKey(string key)
    {
        persistenceKey = key;
    }

    public static string GetPersistanceKey()
    {
        return persistenceKey;
    }

    public static void AddElementToTable(string keyTable, int score, string name)
    {
        HighScoreData tableData = new HighScoreData();
        tableData.score = score;
        tableData.name = name;

        string json = PlayerPrefs.GetString(keyTable);
        HighScoreTableData tableDataJsonUtility = JsonUtility.FromJson<HighScoreTableData>(json);
        tableDataJsonUtility.tableDataList.Add(tableData);

        json = JsonUtility.ToJson(tableDataJsonUtility);
        PlayerPrefs.SetString(keyTable, json);
        PlayerPrefs.Save();
    }

    public static void GetElementsList(string keyTable, int listIndex, out int score, out string name)
    {
        string json = PlayerPrefs.GetString(keyTable);
        HighScoreTableData tableDataJsonUtility = JsonUtility.FromJson<HighScoreTableData>(json);
        score = tableDataJsonUtility.tableDataList[listIndex].score;
        name = tableDataJsonUtility.tableDataList[listIndex].name;
        json = JsonUtility.ToJson(tableDataJsonUtility);
        PlayerPrefs.SetString(keyTable, json);
        PlayerPrefs.Save();

    }

    public static int GetLenghtTableList(string keyTable)
    {
        string json = PlayerPrefs.GetString(keyTable);
        HighScoreTableData tableDataJsonUtility = JsonUtility.FromJson<HighScoreTableData>(json);
        return tableDataJsonUtility.tableDataList.Count;
    }

    public static void DeleateTable(string keyTable)
    {
        PlayerPrefs.DeleteKey(keyTable);
    }

    public static void SortTable(string keyTable)
    {
        string json = PlayerPrefs.GetString(keyTable);
        HighScoreTableData tableDataJsonUtility = JsonUtility.FromJson<HighScoreTableData>(json);
        if (tableDataJsonUtility.tableDataList.Count > 1)
            tableDataJsonUtility.Sort();
        json = JsonUtility.ToJson(tableDataJsonUtility);
        PlayerPrefs.SetString(keyTable, json);
        PlayerPrefs.Save();

    }



    private class HighScoreTableData
    {
        public List<HighScoreData> tableDataList;

        public void Sort()
        {
            int n = tableDataList.Count;

            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(tableDataList, n, i);

            // One by one extract an element from heap 
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end 
                HighScoreData temp = tableDataList[0];
                tableDataList[0] = tableDataList[i];
                tableDataList[i] = temp;

                // call max heapify on the reduced heap 
                heapify(tableDataList, i, 0);
            }
        }

        private void heapify(List<HighScoreData> table, int n, int i)
        {
            int small = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            // If left child is larger than root 
            if (l < n && table[l].score < table[small].score)
                small = l;

            // If right child is larger than largest so far 
            if (r < n && table[r].score < table[small].score)
                small = r;

            // If largest is not root 
            if (small != i)
            {
                HighScoreData swap = table[i];
                table[i] = table[small];
                table[small] = swap;

                // Recursively heapify the affected sub-tree 
                heapify(table, n, small);
            }
        }
    }

    [System.Serializable]
    private class HighScoreData
    {
        public int score;
        public string name;
 
    }
}
