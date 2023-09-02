using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class AssetBundleHelper {
    
    [MenuItem("Assets/Create Asset Bundles")]
    private static void BuildAssetBundles() {
        string assetBundleDirectoryPath = Application.dataPath + "/../GeneratedAssetBundles";
        
        if (Directory.Exists(assetBundleDirectoryPath)) {
            FileUtil.DeleteFileOrDirectory(assetBundleDirectoryPath);
        }
        Directory.CreateDirectory(assetBundleDirectoryPath);
        AssetDatabase.Refresh();
        
        try {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e) {
            Debug.LogWarning(e);
        }
    }
}
