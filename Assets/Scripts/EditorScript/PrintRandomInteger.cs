using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRandomInteger : MonoBehaviour
{
    
    [SerializeField] private Text _text;
    
    public void PrintRandomIntegerFunction()
    {
        _text.text = Random.Range(0, 100).ToString();
    }
    
}
