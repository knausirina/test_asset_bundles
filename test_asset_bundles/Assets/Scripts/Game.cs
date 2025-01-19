using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private GameObject _loaderGameObject;

    private DataDownloader dataDownloader = new DataDownloader();
    private ISaverRemoteData saverRemoteData;

    private void Awake()
    {
        Load();
    }

    private async void Load()
    {
        ToggleLoader(true);


        saverRemoteData = new SaverRemoteData(_config);
        var remoteData = saverRemoteData.LoadData();

        var token = this.GetCancellationTokenOnDestroy();
        var loadSettingsTask = dataDownloader.DownloadFile(_config.SettingsFilePath,
            (error) => OnError("Settings", error),
            token);
        var loadStringsTask = dataDownloader.DownloadFile(_config.StringsFilePath,
            (error) => OnError("Strings", error),
            token);

        var (settingsTask, stringsTask) = await UniTask.WhenAll(loadSettingsTask, loadStringsTask);

        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);

        ToggleLoader(false);
    }

    private void ToggleLoader(bool isShowLoader)
    {
        _loaderGameObject.SetActive(isShowLoader);
    }

    private void OnError(string fileName, string error)
    {
        Debug.Log($"Error fileName = {fileName} error = {error}");
    }
}
