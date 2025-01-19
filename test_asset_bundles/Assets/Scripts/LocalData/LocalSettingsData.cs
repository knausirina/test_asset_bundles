using UnityEditor;
using UnityEngine;

public class LocalSettingsData
{
    public int StartingNumber;

    public LocalSettingsData(SettingsData settingsData)
    {
        StartingNumber = settingsData.StartingNumber;
    }

    public LocalSettingsData(int startingNumber)
    {
        StartingNumber = startingNumber;
    }
}