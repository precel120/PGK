using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text changingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextChange(string instruction)
    {
        changingText.text += '\n';
        changingText.text += '\n';
        changingText.text += instruction;
    }
}
