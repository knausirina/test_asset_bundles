using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundlesService
{
    private readonly Dictionary<string, uint> _versionsByUrls = new Dictionary<string, uint>();

    private async UniTask<AssetBundle> LoadBundle(string url, bool isNewLoad)
    {
        _versionsByUrls.TryGetValue(url, out uint version);
        if (isNewLoad)
        {
            version++;
        }
        _versionsByUrls[url] = version;
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url, version, 0);
        await request.SendWebRequest();
        var bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
        return bundle;
    }

    public async UniTask<Texture2D> LoadImage(string url, bool isNewLoad)
    {
        var bundle = await LoadBundle(url, isNewLoad);

        var image = bundle.LoadAsset("image");

        bundle.Unload(false);

        var texture = (Texture2D)image;
        return texture;
    }
}
