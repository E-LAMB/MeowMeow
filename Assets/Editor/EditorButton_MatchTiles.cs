using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MatchPuzzleTile))]
public class EditorButton_MatchTiles : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MatchPuzzleTile updater = (MatchPuzzleTile)target;

        if (GUILayout.Button("Interact"))
        {
            updater.FlipMe();
        }
    }
}
