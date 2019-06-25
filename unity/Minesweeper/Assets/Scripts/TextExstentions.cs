
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(InputField))]
public class TextExstentions : MonoBehaviour
{

    public InputField inputfield;
    // Start is called before the first frame update
    void Start()
    {
        inputfield.GetComponent<InputField>();

        inputfield.onValueChanged.AddListener(OnValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(string In)
    {
        string inTester = "";
        foreach (var item in In)
        {
            if (char.IsNumber(item))
            {
                inTester += item;
            }

        }
        inputfield.text = inTester;
    }
}
