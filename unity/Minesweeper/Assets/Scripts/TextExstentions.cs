
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(InputField))] // requires inputfield component to work
public class TextExstentions : MonoBehaviour
{

    public InputField inputfield;
    // Start is called before the first frame update

    /// <summary>
    /// adds inputfield component and on OnValueChanged
    /// </summary>
    void Start()
    {
        inputfield.GetComponent<InputField>();

        inputfield.onValueChanged.AddListener(OnValueChanged);
    }

    /// <summary>
    /// makes sure it only has numbers
    /// </summary>
    /// <param name="In"></param>
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
