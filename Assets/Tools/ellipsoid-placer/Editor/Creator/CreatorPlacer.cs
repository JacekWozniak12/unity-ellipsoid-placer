using UnityEngine;

namespace EllipsePlacer.Editor
{
    public class CreatorPlacer
    {
        public void PlaceOnScene(GameObject obj, GameObject parent, CreatorSettingsSO settings)
        {
            Collider collider = obj.GetComponent<Collider>();

            float x_size = collider.bounds.extents.x * 2;
            PositionRotation[] places = new PositionRotation[0];

            switch (settings.Method)
            {
                case CreationMethod.AngleConst:
                    places = EllipseHandler.CalculatePositionsAngleConstMethod(
                        settings.StartingPosition, settings.Width, settings.Height,
                        x_size + settings.Between);
                    break;
                case CreationMethod.DistanceConstNaive:
                    places = EllipseHandler.CalculatePositionsAngleFromItemLengthNaive(
                        settings.StartingPosition, settings.Width, settings.Height,
                        x_size + settings.Between);
                    break;
                case CreationMethod.ItemBasedNaive:
                    places = EllipseHandler.CalcuatePositionsAngleFromItem(
                         settings.StartingPosition, settings.Width, settings.Height,
                         obj, settings.Between, settings.Precision);
                    break;
            }

            foreach (PositionRotation p in places)
            {
                GameObject copy = GameObject.Instantiate(obj, p.position, p.rotation);
                copy.name = "Item";
                copy.transform.parent = parent.transform;
            }
        }
    }
}