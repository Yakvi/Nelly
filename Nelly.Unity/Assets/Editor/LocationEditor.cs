using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Location))]
public class LocationEditor : Editor
{
    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        var location = target as Location;
        if (location)
        {
            var pos = location.Position;

            using(var cc = new EditorGUI.ChangeCheckScope())
            {
                Handles.color = Color.black;
                var newPos = Handles.FreeMoveHandle(new Vector3(pos.x, pos.y, 0), Quaternion.identity, 1, Vector3.one * 0.5f, Handles.RectangleHandleCap);
                Handles.DrawSolidDisc(newPos, Vector3.forward, 0.5f);

                if (cc.changed)
                {
                    Undo.RecordObject(location, "Position Change");
                    location.Position = new Vector2(newPos.x, newPos.y);
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // TODO: Custom inspector
    }

}
