using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Config/Assets Configuration")]
public class AssetsConfiguration : ScriptableObject {
    [Header("Everything in this file is set from the asset bundles when the game starts.")]
    [Header("No need to set these references.")]
    [Space]
    
    [Header("Prefabs")]
    public GameObject MainMenuPrefab;
    public GameObject FoodApplePrefab;
    public GameObject SnakeHeadPrefab;
    public GameObject SnakeBodyPrefab;
    public GameObject SoundPrefab;
    
    [Header("Sprites")]
    public Sprite GameplayBackgroundSprite;

    [Header("AudioClips")]
    public List<AudioClip> AudioClips;

}
    