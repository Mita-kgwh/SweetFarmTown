using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CropsContainer))]
public class CropsContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CropsContainer container = (CropsContainer)target;
        if (GUILayout.Button("Clear container"))
        {
            container.crops = new List<CropTile>();
        }
        // Show default inspector property editor
        DrawDefaultInspector();
    }
}
