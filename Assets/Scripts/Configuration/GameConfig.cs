
using UnityEngine;

public static class GameConfig {
    private static AssetsConfiguration assetsConfiguration;
    
    public static AssetsConfiguration GetAssetsConfiguration() {
        if (assetsConfiguration == null) {
            assetsConfiguration = Resources.Load(nameof(AssetsConfiguration)) as AssetsConfiguration;
        }
        return assetsConfiguration;
    }
}
