using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Config _config;
    [SerializeField] private GameObject _loaderGameObject;
    [SerializeField] private GameViewController _gameViewController;

    private DataDownloader dataDownloader = new DataDownloader();
    private GameService _gameService;
    private DataStorage _dataStorage;

    private void Awake()
    {
        _gameService = new GameService(_config);

        Load();
    }

    private async void Load()
    {
        ToggleLoader(true);

        var remoteData = _gameService.GetUserData();
        LocalSettingsData localSettingsData = null;

        var token = this.GetCancellationTokenOnDestroy();

        if (remoteData == null)
        {
            var settingsDataBytes = await dataDownloader.DownloadFile(_config.SettingsFilePath,
            (error) => OnError("Settings", error), token);

            var remoteSettingsData = _gameService.GetSettingsData(settingsDataBytes);

            localSettingsData = new LocalSettingsData(remoteSettingsData);
        }
        else
        {
            localSettingsData = new LocalSettingsData(remoteData.StartingNumber);
        }
       
        var stringsDataBytes = await dataDownloader.DownloadFile(_config.StringsFilePath,
            (error) => OnError("Strings", error), token);

        var stringsData = _gameService.GetStringsData(stringsDataBytes);
        var localStringsData = new LocalStringsData(stringsData);

        var buttonImage = await _gameService.LoadButtonImage();
        _dataStorage = new DataStorage(localSettingsData, localStringsData, buttonImage);
        _gameService.SetDataStorage(_dataStorage);

        _gameViewController.SetDataStorage(_dataStorage);

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

    private void OnDestroy()
    {
        _gameService.SaveUserData();
    }
}
