using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class EnvironmentLayerAssignerEditor
{
    // NOTE: This script enforces visual sorting of layers in the current open scene during development
    // it's purpose is to enable efficent composition in scenes

    static EnvironmentLayerAssignerEditor() => EditorApplication.hierarchyChanged += OnHierarchyChanged;

    private static void OnHierarchyChanged()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        int order = 0;

        for (int i = rootObjects.Length - 1; i >= 0; i--)
        {
            AssignLayerOrder(rootObjects[i].transform, ref order);
        }
    }

    private static void AssignLayerOrder(Transform parent, ref int order)
    {
        if (parent.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.sortingOrder = order;
            order++;
        }

        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            AssignLayerOrder(parent.GetChild(i), ref order);
        }
    }
}