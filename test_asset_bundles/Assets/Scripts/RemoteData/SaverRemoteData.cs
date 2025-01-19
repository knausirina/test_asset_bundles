using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaverRemoteData : ISaverRemoteData
{
    private Config _config;

    public SaverRemoteData(Config config)
    {
        _config = config;
    }

    public void SaveData(LocalData localData)
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(GetPath());
        var data = new RemoteData();
        data.StartingNumber = localData.StartingNumber;
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public RemoteData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + _config.UserFileName))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(GetPath(), FileMode.Open);
            var remoteData = (RemoteData)binaryFormatter.Deserialize(file);
            file.Close();
            return remoteData;
        }
        return null;
    }

    private string GetPath()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + _config.UserFileName;
    }
}