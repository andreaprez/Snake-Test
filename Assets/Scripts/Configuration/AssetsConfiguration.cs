using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Assets Configuration")]
public class AssetsConfiguration : ScriptableObject {
    [Header("AssetBundleURLs")] 
    public string TexturesURL = "https://drive.google.com/u/0/uc?id=16qrrsKkJRKuX8Jx6d3QWoDhQwgAmdWBb&export=download";
    public string SoundsURL = "https://drive.google.com/u/0/uc?id=1qEvwIJOLT18EG9lg50dFiwU0V4_G9Z9R&export=download";

    [Space]
    [Header("Everything below is set from the asset bundles when the game starts. No need to set these references.")]
    
    [Header("Prefabs")]
    public GameObject MainMenuPrefab;
    public GameObject FoodApplePrefab;
    public GameObject SnakeHeadPrefab;
    public GameObject SnakeBodyPrefab;
    
    [Header("Sprites")]
    public Sprite GameplayBackgroundSprite;

    [Header("AudioClips")]
    public List<AudioClip> AudioClips;
}
    