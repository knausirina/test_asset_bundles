using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
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

        if (settingsDataBytes == null)
        {
            return null;
        }

        SettingsData settingsData;
        try
        {
            var stringValue = System.Text.Encoding.UTF8.GetString(settingsDataBytes);
            settingsData = JsonUtility.FromJson<SettingsData>(stringValue);
        }
        catch (Exception e)
        {
            return null;
        }

        var localSettingsData = new LocalSettingsData(settingsData);
        return localSettingsData;
    }

    public async UniTask<LocalStringsData> GetStringsData(CancellationToken token)
    {
        var stringsDataBytes = await _dataDownloader.DownloadFile(_config.StringsFilePath,
            (error) => OnError("Strings", error), token);

        if (stringsDataBytes == null)
        {
            return null;
        }

        StringsData stringsData;
        try
        {
            var stringValue = System.Text.Encoding.UTF8.GetString(stringsDataBytes);
            stringsData = JsonUtility.FromJson<StringsData>(stringValue);
        }
        catch (Exception e)
        {
            return null;
        }

        var localStringsData = new LocalStringsData(stringsData);
        return localStringsData;
    }

    private void OnError(string fileName, string error)
    {
        Debug.Log($"Error fileName = {fileName} error = {error}");
    }
}