using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DataStorage
{
    public LocalStringsData LocalStringsData;
    public LocalSettingsData LocalSettingsData;
    public Texture2D ButtonImage;

    public DataStorage(LocalSettingsData localSettingsData, LocalStringsData localStringsData, Texture2D buttonImage)
    {
        LocalStringsData = localStringsData;
        LocalSettingsData = localSettingsData;
        ButtonImage = buttonImage;
    }
}