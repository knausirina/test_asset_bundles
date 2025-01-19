using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameService
{
    private readonly Config _config;
    private DataStorage _dataStorage;
    private ISaverUserData _saverRemoteData;
    private AssetBundlesService _assetBundlesService;

    public GameService(Config config)
    {
        _config = config;
        _assetBundlesService = new AssetBundlesService();
    }

    public void SetDataStorage(DataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public async UniTask<Texture2D> LoadButtonImage()
    {
        return await _assetBundlesService.LoadImage(_config.AssetBundlePath);
    }

    public UserData GetUserData()
    {
        _saverRemoteData = new SaverUserData(_config);
        var remoteData = _saverRemoteData.LoadData();
        return remoteData;
    }

    public void SaveUserData()
    {
        _saverRemoteData.SaveData(_dataStorage.LocalSettingsData);
    }

    public SettingsData GetSettingsData(byte[] data)
    {
        var stringValue = System.Text.Encoding.UTF8.GetString(data);
        var settingsData = JsonUtility.FromJson<SettingsData>(stringValue);
        return settingsData;
    }

    public StringsData GetStringsData(byte[] data)
    {
        var stringValue = System.Text.Encoding.UTF8.GetString(data);
        var stringsData = JsonUtility.FromJson<StringsData>(stringValue);
        return stringsData;
    }
}