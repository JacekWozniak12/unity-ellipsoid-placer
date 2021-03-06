using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace EllipsePlacer.Editor
{
    public class CreatorView : EditorWindow
    {
        CreatorIO _creatorIO = new CreatorIO();
        List<IDisplayGUI> _viewElements = new List<IDisplayGUI>();
        CreatorSettingsSO _currentSettings = default;
        CreatorPropertyHandler _propertyHandler;
        CreatorPlacer _placer;

        [MenuItem(Settings.WINDOW_MENU_NAME)]
        static void Init()
        {
            CreatorView window = EditorWindow.GetWindow(
                typeof(CreatorView),
                true,
                Settings.NAME) as CreatorView;

            Setup(window);

            window.minSize = Settings.WINDOW_SIZE;
            window.Show();
        }

        static void Setup(CreatorView window)
        {
            CreatorDropView dropView = new CreatorDropView();
            window._placer = new CreatorPlacer();
            window._currentSettings = ScriptableObject.CreateInstance<CreatorSettingsSO>();
            window._propertyHandler = new CreatorPropertyHandler(window._currentSettings);
            dropView.EventHappened.AddListener(window.ImportProcedure);
            DefineGUIContent(window, dropView);
        }

        static void DefineGUIContent(CreatorView window, CreatorDropView dropView)
        {
            window._viewElements.AddRange(
                new IDisplayGUI[]{
                    new CreatorImportButton(window),
                    dropView,
                    new CreatorExportButton(window),
                    window._propertyHandler,
                    new CreatorGenerateButton(window)
                    }
               );
        }

        internal void OnGUI()
        {
            foreach (IDisplayGUI displayer in _viewElements)
                displayer.OnDisplayGUI();
        }

        public void Place()
        {
            Place(_propertyHandler.GetSerializedObject().targetObject as CreatorSettingsSO);
        }

        public void ImportProcedure()
        {
            CreatorSettingsSO temp = _creatorIO.ImportWithOpenFilePanel();
            ImportProcedure(temp);
        }

        public void ImportProcedure(CreatorSettingsSO so)
        {
            if (so != null)
            {
                _currentSettings = Instantiate(so);
                _propertyHandler.SetCreatorSettings(_currentSettings);
            }
        }

        public void ExportProcedure()
        {
            _currentSettings = ScriptableObject.CreateInstance<CreatorSettingsSO>();

            _propertyHandler.ApplyPropertiesFromWindowTo(_currentSettings);
            if (_creatorIO.ExportToFile(_currentSettings))
            {
                _propertyHandler.SetCreatorSettings(_currentSettings);
            }
        }

        internal void Place(CreatorSettingsSO settings)
        {
            GameObject parent = new GameObject("Group");
            parent.transform.position = settings.StartingPosition;

            GameObject temp = Instantiate(
                settings.Prefab,
                settings.StartingPosition,
                Quaternion.identity
                );

            UpdatePrefabVisuals(temp, settings);
            UpdatePrefabScale(temp, settings);
            _placer.PlaceOnScene(temp, parent, settings);
            DestroyImmediate(temp);
        }

        internal void UpdatePrefabVisuals(GameObject temp, CreatorSettingsSO settings)
        {
            Renderer renderer = temp.GetComponent<Renderer>();
            if (settings.Material != null)
            {
                renderer.sharedMaterial = settings.Material;
                if (settings.Material.color != settings.Color)
                {
                    if (EditorUtility.DisplayDialog(
                        "Change color of the material",
                        "Do you want to change color of the material that you selected?",
                        "Yes",
                        "No")
                        )
                    {
                        renderer.material.color = settings.Color;
                    }
                }
            }
        }

        internal void UpdatePrefabScale(GameObject temp, CreatorSettingsSO settings)
        {
            Transform transform = temp.GetComponent<Transform>();
            transform.localScale *= settings.Scale;
        }
    }
}