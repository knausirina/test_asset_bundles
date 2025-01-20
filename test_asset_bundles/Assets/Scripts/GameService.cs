using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class GameService
{
    private readonly Config _config;
    private AssetBundlesService _assetBundlesService;
    private DataDownloader _dataDownloader = new DataDownloader();

    public GameService(Config config)
    {
        _config = config;
        _assetBundlesService = new AssetBundlesService();
    }

    public async UniTask<Texture2D> LoadButtonImage(bool isNewLoad = false)
    {
        return await _assetBundlesService.LoadImage(_config.AssetBundlePath, isNewLoad);
    }

    public async UniTask<LocalSettingsData> GetSettingsData(CancellationToken token)
    {
        var settingsDataBytes = await _dataDownloader.DownloadFile(_config.SettingsFilePath,
            (error) => OnError("Settings", error), token);

        var stringValue = System.Text.Encoding.UTF8.GetString(settingsDataBytes);
        var settingsData = JsonUtility.FromJson<SettingsData>(stringValue);

        var localSettingsData = new LocalSettingsData(settingsData);
        return localSettingsData;
    }

    public async UniTask<LocalStringsData> GetStringsData(CancellationToken token)
    {
        var stringsDataBytes = await _dataDownloader.DownloadFile(_config.StringsFilePath,
            (error) => OnError("Strings", error), token);

        var stringValue = System.Text.Encoding.UTF8.GetString(stringsDataBytes);
        var stringsData = JsonUtility.FromJson<StringsData>(stringValue);

        var localStringsData = new LocalStringsData(stringsData);
        return localStringsData;
    }

    private void OnError(string fileName, string error)
    {
        Debug.Log($"Error fileName = {fileName} error = {error}");
    }
}