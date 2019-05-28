using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TablePlayer : MonoBehaviour
{
    [SerializeField] Text name;
    [SerializeField] Text rank;
    [SerializeField] Text score;

    public void SetValue(string n, string r, string s)
    {
        name.text = n;
        rank.text = r;
        score.text = s;
    }
}
