using UnityEditor;
using UnityEngine;

// [CustomEditor(typeof(Location))]
[CustomEditor(typeof(LocationSetter))]
public class LocationEditor : Editor
{
    void OnSceneGUI()
    {
        var locationSetter = target as LocationSetter;
        var location = locationSetter.location;
        // var location = target as Location;
        if (location)
        {
            var pos = location.Position;

            using(var cc = new EditorGUI.ChangeCheckScope())
            {
                var newPos = Handles.PositionHandle(new Vector3(pos.x, pos.y, 0), Quaternion.identity);

                if (cc.changed)
                {
                    Undo.RecordObject(location, "Position Change");
                    location.Position = new Vector2(newPos.x, newPos.y);
                }
            }
        }

    }
}
