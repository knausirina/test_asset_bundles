using UnityEditor;
using UnityEngine;

public interface ISaverUserData
{
    public void SaveData(LocalSettingsData data);
    public UserData LoadData();
}