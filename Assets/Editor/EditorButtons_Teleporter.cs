/**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TeleportPad))]
public class EditorButtons_Teleporter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TeleportPad updater = (TeleportPad)target;

        if (GUILayout.Button("Interact"))
        {
            updater.TakeMe();
        }
    }
}
/**/