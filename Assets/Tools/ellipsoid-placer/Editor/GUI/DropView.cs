using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace EllipsePlacer.Editor
{
    public abstract class DropView<T> : IDisplayGUI, IEventNotifier<T>
    {
        public UnityEvent<T> EventHappened
        {
            get;
            private set;
        } 
        = new UnityEvent<T>();

        public void OnDisplayGUI() => DropAreaGUI();

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
                        if (dragged is T obj)
                        {
                            EventHappened?.Invoke(obj);
                            // SetSerializedObject(settingsSO);
                        }
                    }
                    break;
            }
        }
    }
}