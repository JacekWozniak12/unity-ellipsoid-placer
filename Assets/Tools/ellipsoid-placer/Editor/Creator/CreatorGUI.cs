using UnityEngine;
using UnityEditor;

namespace EllipsePlacer.Editor
{
    public partial class CreatorView : EditorWindow
    {
        internal void OnGUI()
        {
            serialized_object.Update();
            DisplayImportExport();
            DisplayProperties();
            DisplayCreate();
            serialized_object.ApplyModifiedProperties();
        }

        internal void DisplayImportExport()
        {
            if (GUILayout.Button("Import changes from file")) ImportProcedure();
            DropAreaGUI();
            if (GUILayout.Button("Export changes to new file")) ExportProcedure();
        }

        internal void DisplayCreate()
        {
            if (GUILayout.Button("Generate")) Place(serialized_object.targetObject as CreatorSettingsSO);
        }

        internal void DisplayProperties()
        {
            EditorGUILayout.PropertyField(prop_StartingPosition);
            EditorGUILayout.PropertyField(prop_Method);
            EditorGUILayout.PropertyField(prop_Scale);
            EditorGUILayout.PropertyField(prop_Prefab);
            EditorGUILayout.PropertyField(prop_Material);
            EditorGUILayout.PropertyField(prop_Color);
            EditorGUILayout.PropertyField(prop_Height);
            EditorGUILayout.PropertyField(prop_Width);
            EditorGUILayout.PropertyField(prop_Between);
            EditorGUILayout.PropertyField(prop_Precision);
        }

        internal void DropAreaGUI()
        {
            Event evt = Event.current;
            Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
            GUI.Box(drop_area, "\nDrag asset here");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!drop_area.Contains(evt.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        var dragged = DragAndDrop.objectReferences[0];
                        if (dragged is CreatorSettingsSO settingsSO)
                        {
                            SetSerializedObject(settingsSO);
                        }
                    }
                    break;
            }
        }
    }
}
