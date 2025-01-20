using UnityEngine;
using System;

public class GameViewController : MonoBehaviour
{
    public Action UpdateDataAction;

    [SerializeField] private GameView _gameView;

    private DataStorage _dataStorage;
    private GameService _gameService;
    private StartingCountRule _startingCountRule;

    public void Construct(GameService gameService, DataStorage dataStorage)
    {
        _gameService = gameService;
        _dataStorage = dataStorage;

        _startingCountRule = new StartingCountRule(_dataStorage);

        _gameView.SetStartingNumber(_dataStorage.LocalSettingsData.StartingNumber);
        _gameView.SetGreetings(_dataStorage.LocalStringsData.Greetings);
        _gameView.SetButtonImage(_dataStorage.ButtonImage);
    }

    private void Awake()
    {
        _gameView.IncrementStartingAction += OnIncrementStartingAction;
        _gameView.UpdateDataAction += OnUpdateDataAction;
    }

    private void OnUpdateDataAction()
    {
        UpdateDataAction?.Invoke();
    }

    private void OnDestroy()
    {
        _gameView.IncrementStartingAction -= OnIncrementStartingAction;
        _gameView.UpdateDataAction -= OnUpdateDataAction;
    }

    private void OnIncrementStartingAction()
    {
        _startingCountRule.RequestIncrementStartingCount();

        _gameView.SetStartingNumber(_dataStorage.LocalSettingsData.StartingNumber);
    }
}