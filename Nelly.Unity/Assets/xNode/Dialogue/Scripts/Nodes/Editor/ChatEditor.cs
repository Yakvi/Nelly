using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Dialogue
{
    [CustomNodeEditor(typeof(Chat))]
    public class ChatEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            serializedObject.Update();

            Chat node = target as Chat;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("character"), GUIContent.none);
            if (node.choices.Count == 0)
            {
                GUILayout.BeginHorizontal();
                {
                    NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
                    NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"));
            }
            GUILayout.Space(-20);

            var label = new GUIContent();
            var image = serializedObject.FindProperty("Image").objectReferenceValue;
            if (image != null) label.image = ((Sprite) image).texture;
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Image"), label);
            label.image = null;
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ImageTint"), GUIContent.none);

            label.text = label.tooltip = "Image text";
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ImageText"), label);
            label.text = label.tooltip = "Dialog text";
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("text"), label);

            NodeEditorGUILayout.InstancePortList("choices", typeof(DialogueBaseNode), serializedObject, NodePort.IO.Output, Node.ConnectionType.Override);

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth()
        {
            return 300;
        }

        public override Color GetTint()
        {
            Chat node = target as Chat;
            if (node.character == null) return base.GetTint();
            else
            {
                Color col = node.character.color;
                col.a = 1;
                return col;
            }
        }
    }
}
