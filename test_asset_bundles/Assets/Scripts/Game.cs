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
    private ISaverUserData _saverRemoteData;

    private void Awake()
    {
        _gameService = new GameService(_config);

        Load(false);

        _gameViewController.UpdateDataAction += UpdateDataAction;
    }

    private async void Load(bool isClearOldData)
    {
        ToggleLoader(true);

        if (isClearOldData)
        {
            Caching.ClearCache();
            _saverRemoteData.ClearData();
        }

        var remoteData = GetUserData();
        LocalSettingsData localSettingsData = null;

        var token = this.GetCancellationTokenOnDestroy();

        if (remoteData == null)
        {
            localSettingsData = await _gameService.GetSettingsData(token);
        }
        else
        {
            localSettingsData = new LocalSettingsData(remoteData.StartingNumber);
        }

        var localStringsData = await _gameService.GetStringsData(token);

        var buttonImage = await _gameService.LoadButtonImage(isClearOldData);
        _dataStorage = new DataStorage(localSettingsData, localStringsData, buttonImage);

        _gameViewController.Construct(_gameService, _dataStorage);

        await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale: false);

        ToggleLoader(false);
    }

    private void UpdateDataAction()
    {
        Load(true);
    }

    private void ToggleLoader(bool isShowLoader)
    {
        _loaderGameObject.SetActive(isShowLoader);
    }

    private void OnDestroy()
    {
        SaveData();
        _gameViewController.UpdateDataAction -= UpdateDataAction;
    }

    public UserData GetUserData()
    {
        _saverRemoteData = new SaverUserData(_config);
        var remoteData = _saverRemoteData.LoadData();
        return remoteData;
    }

    private void SaveData()
    {
        _saverRemoteData.SaveData(_dataStorage.LocalSettingsData);
    }
}
