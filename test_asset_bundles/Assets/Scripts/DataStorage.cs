using UnityEditor;
using UnityEngine;

public class DataStorage
{
    public LocalStringsData LocalStringsData;
    public LocalSettingsData LocalSettingsData;

    public DataStorage(LocalSettingsData localSettingsData, LocalStringsData localStringsData)
    {
        LocalStringsData = localStringsData;
        LocalSettingsData = localSettingsData;
    }
}