using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField] private GameObject LoadingGO;
    [SerializeField] private Transform MainMenuContainer;

    private bool textureAssetsLoaded = false;
    private bool soundAssetsLoaded = false;
    
    private void Awake() {
        Debug.Log("[AssetBundles] Loading asset bundles");
        LoadingGO.SetActive(true);
        
        StartCoroutine(DownloadAssetBundle(AssetBundleType.Textures, HandleTextureAssets));
        StartCoroutine(DownloadAssetBundle(AssetBundleType.Sounds, HandleSoundAssets));
    }

    private IEnumerator DownloadAssetBundle(AssetBundleType bundleType, Action<List<UnityEngine.Object>> successCallback) {
        string url = "";
        switch (bundleType) {
            case AssetBundleType.Textures:
                url = GameConfig.GetAssetsConfiguration().TexturesURL;
                break;
            case AssetBundleType.Sounds:
                url = GameConfig.GetAssetsConfiguration().SoundsURL;
                break;
        }
        
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url)) {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogWarning("[AssetBundles] The request at url: " + url + " failed with error: " + www.error);
            }
            else {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                if (bundle == null || bundle.GetAllAssetNames().Length == 0) {
                    Debug.LogWarning("[AssetBundles]  bundle downloaded from url: " + url + " is null or empty.");
                }
                else
                {
                    List<UnityEngine.Object> bundleAssets = new List<UnityEngine.Object>(bundle.LoadAllAssets());
                    successCallback?.Invoke(bundleAssets);
                }
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
    }
    
    private void HandleTextureAssets(List<UnityEngine.Object> assets) {
        foreach (var asset in assets) {
            switch (asset.name) {
                case "SnakeHeadGO":
                    GameConfig.GetAssetsConfiguration().SnakeHeadPrefab = (GameObject)asset;
                    break;
                case "SnakeBodyGO":
                    GameConfig.GetAssetsConfiguration().SnakeBodyPrefab = (GameObject)asset;
                    break;
                case "FoodAppleGO":
                    GameConfig.GetAssetsConfiguration().FoodApplePrefab = (GameObject)asset;
                    break;
                case "MainMenu":
                    GameConfig.GetAssetsConfiguration().MainMenuPrefab = (GameObject)asset;
                    break;
                case "White_1x1":
                    GameConfig.GetAssetsConfiguration().GameplayBackgroundSprite = (Sprite)asset;
                    break;
            }
        }
        
        Debug.Log("[AssetBundles] Textures asset bundle finished loading.");
        textureAssetsLoaded = true;

        if (soundAssetsLoaded) {
            StartGame();            
        }
    }

    private void HandleSoundAssets(List<UnityEngine.Object> assets) {
        GameConfig.GetAssetsConfiguration().AudioClips = new List<AudioClip>();
        foreach (var asset in assets) {
            GameConfig.GetAssetsConfiguration().AudioClips.Add((AudioClip)asset);
        }
        
        Debug.Log("[AssetBundles] Sounds asset bundle finished loading.");
        soundAssetsLoaded = true;
        
        if (textureAssetsLoaded) {
            StartGame();            
        }
    }

    private void StartGame()
    {
        Debug.Log("[AssetBundles] Finished loading all asset bundles. Starting game.");
        LoadingGO.SetActive(false);
        Instantiate(GameConfig.GetAssetsConfiguration().MainMenuPrefab, MainMenuContainer);
    }

    private enum AssetBundleType
    {
        Textures,
        Sounds
    }
}
