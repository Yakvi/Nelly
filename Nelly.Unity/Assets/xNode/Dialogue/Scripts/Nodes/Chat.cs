using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue {
    [NodeTint("#ddFFdd")]
    public class Chat : DialogueBaseNode {

        public CharacterInfo character;

        public Sprite Image;
        public Color ImageTint = Color.white;
        [TextArea]
        public string ImageText;

        [TextArea] public string text;
        
        [Output(instancePortList = true)] public List<Choice> choices = new List<Choice>();

        public Location PlayerPosition;

        [System.Serializable] public class Choice {
            public string ButtonText;
            public Location POI;

            public float CostInTime = 0f;
            public float CostInMoney = 0f;
        }

        public void AnswerQuestion(int index) {
            NodePort port = null;
            if (choices.Count == 0) {
                port = GetOutputPort("output");
            } else {
                if (choices.Count <= index) return;
                port = GetOutputPort("choices " + index);
            }

            if (port == null) return;
            for (int i = 0; i < port.ConnectionCount; i++) {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }
        }

        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }
    }
}