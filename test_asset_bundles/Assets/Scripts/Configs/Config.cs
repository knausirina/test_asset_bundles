using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
public class Config : ScriptableObject
{
    [SerializeField] private string _settingsFilePath;
    [SerializeField] private string _stringsFilePath;
    [SerializeField] private string _assetBundlePath;
    [SerializeField] private string _userFileName;

    public string SettingsFilePath => _settingsFilePath;
    public string StringsFilePath => _stringsFilePath;
    public string AssetBundlePath => _assetBundlePath;
    public string UserFileName => _userFileName;
}
