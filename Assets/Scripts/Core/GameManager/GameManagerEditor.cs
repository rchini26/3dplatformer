using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GameManager fsm = (GameManager)target;
        
        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: " + fsm.stateMachine.CurrentState.GetType().Name);
        }
        
        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
            if (fsm.stateMachine.dictionaryState != null)
            {
                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {values[i]}");
                }
            }
        }
    }
}
