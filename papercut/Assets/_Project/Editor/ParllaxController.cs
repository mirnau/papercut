using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ParallaxController))]
public class ParallaxControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Recalculate Layers"))
        {
            var controller = (ParallaxController)target;
            controller.ApplyFogToLayers();
        }
    }
}
