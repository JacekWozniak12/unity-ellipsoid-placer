using UnityEngine;

namespace EllipsePlacer.Editor
{
    public abstract class CreatorViewButton : IDisplayGUI
    {
        protected CreatorView _view;
        public CreatorViewButton(CreatorView view) => _view = view;
        public abstract void OnDisplayGUI();
    }

    public class CreatorImportButton : CreatorViewButton
    {
        public CreatorImportButton(CreatorView view) : base(view) { }

        public override void OnDisplayGUI()
        {
            if (GUILayout.Button("Import changes from file"))
                _view.ImportProcedure();
        }
    }

    public class CreatorExportButton : CreatorViewButton
    {
        public CreatorExportButton(CreatorView view) : base(view) { }

        public override void OnDisplayGUI()
        {
            if (GUILayout.Button("Export changes to new file"))
                _view.ExportProcedure();
        }
    }

    public class CreatorGenerateButton : CreatorViewButton
    {
        public CreatorGenerateButton(CreatorView view) : base(view){}

        public override void OnDisplayGUI()
        {
            if (GUILayout.Button("Generate"))
                _view.Place();
        }
    }
}
