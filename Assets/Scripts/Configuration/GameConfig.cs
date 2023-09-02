
using UnityEngine;

public static class GameConfig {
    private static AssetBundlesConfiguration assetBundlesConfiguration;
    private static AssetsConfiguration assetsConfiguration;
    private static GameplayConfiguration gameplayConfiguration;
    
    public static AssetBundlesConfiguration GetAssetBundlesConfiguration() {
        if (assetBundlesConfiguration == null) {
            assetBundlesConfiguration = Resources.Load(nameof(AssetBundlesConfiguration)) as AssetBundlesConfiguration;
        }
        return assetBundlesConfiguration;
    }
    
    public static AssetsConfiguration GetAssetsConfiguration() {
        if (assetsConfiguration == null) {
            assetsConfiguration = Resources.Load(nameof(AssetsConfiguration)) as AssetsConfiguration;
        }
        return assetsConfiguration;
    }
    
    public static GameplayConfiguration GetGameplayConfiguration() {
        if (gameplayConfiguration == null) {
            gameplayConfiguration = Resources.Load(nameof(GameplayConfiguration)) as GameplayConfiguration;
        }
        return gameplayConfiguration;
    }
    
    public static void OverrideGameplayConfiguration(GameplayConfiguration newConfig)
    {
        gameplayConfiguration = newConfig;
    }
}
