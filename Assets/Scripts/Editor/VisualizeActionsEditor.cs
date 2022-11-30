using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionsEditorScriptVisualizer))]
public class VisualizeActionsEditor : Editor
// This script enable two buttons for the editor script
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ActionsEditorScriptVisualizer myScript = (ActionsEditorScriptVisualizer) target;

        if (GUILayout.Button("Visualize actions")) // Button to visualize actions (or update actions) in the editor (editor script) while creating levels
        {
            myScript.VisualizeActions();
        }
        
        if (GUILayout.Button("Remove actions")) // Button to remove action you was visualizing with the other button
        {
            myScript.RemoveActions();
        }
        
    }
    
}
