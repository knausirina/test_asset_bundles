using UnityEditor;
using UnityEngine;

public class StartingCountRule
{
    private DataStorage _dataStorage;

    private const int INCREMENT_VALUE = 1;

    public StartingCountRule(DataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }

    public void RequestIncrementStartingCount()
    {
        _dataStorage.LocalSettingsData.StartingNumber += INCREMENT_VALUE;
    }
}