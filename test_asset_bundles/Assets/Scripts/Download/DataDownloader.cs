using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class DataDownloader
{
    public async UniTask<byte[]> DownloadFile(string filePath,
        Action<string> errorCallback,
        CancellationToken cancellationToken)
    {
        var unityWebRequest = new UnityWebRequest(filePath);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        await unityWebRequest.SendWebRequest().ToUniTask(null, PlayerLoopTiming.Update, cancellationToken);

        if (unityWebRequest.result != UnityWebRequest.Result.Success)
        {
            errorCallback.Invoke(unityWebRequest.error);
            return null;
        }

        Debug.Log("result " + unityWebRequest.downloadHandler.text);

        var results = unityWebRequest.downloadHandler.data;

        string result = System.Text.Encoding.UTF8.GetString(results);
        return results;
    }
}