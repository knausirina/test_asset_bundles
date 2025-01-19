using UnityEditor;
using UnityEngine;

public interface ISaverRemoteData
{
    public void SaveData(LocalData data);
    public RemoteData LoadData();
}