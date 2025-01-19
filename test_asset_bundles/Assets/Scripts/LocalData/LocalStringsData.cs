using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LocalStringsData
{
    private string _greetings;

    public string Greetings => _greetings;

    public LocalStringsData(StringsData stringsData)
    {
        _greetings = stringsData.Greetings;
    }
}