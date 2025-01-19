using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;

public class GameViewController : MonoBehaviour
{
    [SerializeField] private GameView _gameView;

    private DataStorage _dataStorage;
    private StartingCountRule _startingCountRule;

    public void SetDataStorage(DataStorage dataStorage)
    {
        _dataStorage = dataStorage;

        _startingCountRule = new StartingCountRule(_dataStorage);

        _gameView.SetStartingNumber(_dataStorage.LocalSettingsData.StartingNumber);
        _gameView.SetGreetings(_dataStorage.LocalStringsData.Greetings);
        _gameView.SetButtonImage(_dataStorage.ButtonImage);
    }

    private void Awake()
    {
        _gameView.IncrementStartingAction += OnIncrementStartingAction;
    }

    private void OnDestroy()
    {
        _gameView.IncrementStartingAction -= OnIncrementStartingAction;
    }

    private void OnIncrementStartingAction()
    {
        _startingCountRule.RequestIncrementStartingCount();

        _gameView.SetStartingNumber(_dataStorage.LocalSettingsData.StartingNumber);
    }
}