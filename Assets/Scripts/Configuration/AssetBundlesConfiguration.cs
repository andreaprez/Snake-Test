using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Asset Bundles Configuration")]
public class AssetBundlesConfiguration : ScriptableObject
{
    [Header("URLs")] 
    public string GameConfigURL = "https://drive.google.com/u/0/uc?id=1RdMq9I-LbOdU70mlW5iJvmVX3sQWG1g0&export=download";
    public string TexturesURL = "https://drive.google.com/u/0/uc?id=1lERPo2h9VG9S55OEOsaTjZW4BSnIWIM5&export=download";
    public string SoundsURL = "https://drive.google.com/u/0/uc?id=1MF5T87F5kQ9Lsu32vRZQ_lWMtO5Jo4uY&export=download";
}
