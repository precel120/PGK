using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCollector : MonoBehaviour
{
    public static GemCollector instance;
    public TextMeshProUGUI text;
    int amount;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void ChangeNumber(int number)
    {
        amount += number;
        text.text = "Collected Gems: " + amount.ToString();
    }
}
