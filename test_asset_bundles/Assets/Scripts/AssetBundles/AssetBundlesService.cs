using Cysharp.Threading.Tasks;
using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundlesService
{
    private async UniTask<AssetBundle> LoadBundle(string url)
    {
        var request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
        await request.SendWebRequest();
        AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
        return bundle;
    }

    public async UniTask<Texture2D> LoadImage(string url)
    {
        var bundle = await LoadBundle(url);

        var image = bundle.LoadAsset("image");
       
        var texture = (Texture2D)image;
        return texture;
    }
}
