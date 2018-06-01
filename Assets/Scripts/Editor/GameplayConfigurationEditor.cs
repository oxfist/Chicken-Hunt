using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameplayConfiguration))]
public class GameplayConfigurationEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GameplayConfiguration script = (GameplayConfiguration)target;
        if (GUILayout.Button("Reset Configurations")) {
            script.ResetParameters();
        }
    }
}
