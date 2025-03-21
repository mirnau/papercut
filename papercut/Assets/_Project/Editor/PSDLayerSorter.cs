
/*  NOTE: Vibecode for editing later 
 * 
 * 

using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;

class PSDLayerSorting : AssetPostprocessor
{
    void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            if (assetPath.EndsWith(".psd") || assetPath.EndsWith(".psb"))
            {
                ApplySortingLayers(assetPath);
            }
        }
    }

    void ApplySortingLayers(string assetPath)
    {
        var spriteLib = AssetDatabase.LoadAssetAtPath<SpriteLibraryAsset>(assetPath);
        if (spriteLib == null) return;

        foreach (var category in spriteLib.GetCategoryNames())
        {
            GameObject spriteObj = new GameObject(category);
            SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();

            // Load sprite by category name (which is the folder name in PSD)
            sr.sprite = spriteLib.GetSprite(category);

            // Assign sorting layer based on folder name
            sr.sortingLayerName = GetSortingLayerFor(category);
        }
    }

    string GetSortingLayerFor(string folderName)
    {
        switch (folderName.ToLower())
        {
            case "background": return "Background";
            case "midground": return "Midground";
            case "foreground": return "Foreground";
            default: return "Default";
        }
    }
}

*/