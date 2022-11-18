using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrintRandomInteger))]
public class PrintRandomIntegerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PrintRandomInteger myScript = (PrintRandomInteger) target;

        if (GUILayout.Button("Print Random Integer"))
        {
            myScript.PrintRandomIntegerFunction();
        }
    }
    
}
