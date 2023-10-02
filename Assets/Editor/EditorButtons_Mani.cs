/**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LookMani))]
public class EditorButtons_Mani: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LookMani updater = (LookMani)target;

        if (GUILayout.Button("Interact"))
        {
            updater.Interacted();
        }
    }
}
/**/

