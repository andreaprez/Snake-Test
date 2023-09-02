using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Asset Bundles Configuration")]
public class AssetBundlesConfiguration : ScriptableObject
{
    [Header("URLs")] 
    public string GameConfigURL = "https://drive.google.com/u/0/uc?id=1NIm22FaM9B-XA48eUAIIHjqPNnkAFD9m&export=download";
    public string TexturesURL = "https://drive.google.com/u/0/uc?id=1B9mE71wDs4gXjOfhNz9dRSZKDea_mVVT&export=download";
    public string SoundsURL = "https://drive.google.com/u/0/uc?id=1O6EiMBgyGAE2BeYV6B3dpvKPp1k45GrA&export=download";
}
