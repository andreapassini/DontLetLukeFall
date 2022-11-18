using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionsEditorScriptVisualizer))]
public class VisualizeActionsEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ActionsEditorScriptVisualizer myScript = (ActionsEditorScriptVisualizer) target;

        if (GUILayout.Button("Visualize actions"))
        {
            myScript.VisualizeActions();
        }
    }
    
}
