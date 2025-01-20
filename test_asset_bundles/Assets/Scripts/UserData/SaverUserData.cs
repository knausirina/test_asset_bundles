using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaverUserData : ISaverUserData
{
    private Config _config;

    public SaverUserData(Config config)
    {
        _config = config;
    }

    public void SaveData(LocalSettingsData settingsData)
    {
        var binaryFormatter = new BinaryFormatter();
        var file = File.Create(GetPath());
        var data = new UserData();
        data.StartingNumber = settingsData.StartingNumber;
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public UserData LoadData()
    {
        var path = GetPath();
        if (File.Exists(path))
        {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(GetPath(), FileMode.Open);
            var remoteData = (UserData)binaryFormatter.Deserialize(file);
            file.Close();
            return remoteData;
        }
        return null;
    }

    public void ClearData()
    {
        File.Delete(GetPath());
    }

    private string GetPath()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + _config.UserFileName;
    }
}