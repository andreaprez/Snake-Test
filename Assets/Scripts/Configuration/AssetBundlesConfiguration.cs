using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Asset Bundles Configuration")]
public class AssetBundlesConfiguration : ScriptableObject
{
    [Header("URLs")] 
    public string GameConfigURL = "https://drive.google.com/u/0/uc?id=13VxjhNRJ-n9HBQo4xjoeKM2bLykJTkLz&export=download";
    public string TexturesURL = "https://drive.google.com/u/0/uc?id=1ksirfcflTCm90ZxKtstIeJyMThkG7fwY&export=download";
    public string SoundsURL = "https://drive.google.com/u/0/uc?id=1O6EiMBgyGAE2BeYV6B3dpvKPp1k45GrA&export=download";
}
