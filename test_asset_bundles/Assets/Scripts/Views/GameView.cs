using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Action IncrementStartingAction;
    public Action UpateDataAction;

    [SerializeField] private TMP_Text _startingNumberField;
    [SerializeField] private TMP_Text _greetingsField;
    [SerializeField] private Button _startingButton;
    [SerializeField] private Button _updateButton;

    private void Awake()
    {
        _startingButton.onClick.AddListener(OnStartingButton);
        _updateButton.onClick.AddListener(OnUpdateButton);
    }

    public void SetStartingNumber(int startingNumber)
    {
        _startingNumberField.text = startingNumber.ToString();
    }

    public void SetGreetings(string greetings)
    {
        _greetingsField.text = greetings;
    }

    public void SetData(SettingsData data)
    {
        _startingNumberField.text = data.StartingNumber.ToString();
    }

    private void OnStartingButton()
    {
        IncrementStartingAction?.Invoke();
    }

    private void OnUpdateButton()
    {
        UpateDataAction?.Invoke();
    }

    private void OnDestroy()
    {
        _startingButton.onClick.RemoveListener(OnStartingButton);
        _updateButton.onClick.RemoveListener(OnUpdateButton);
    }
}