using System.Collections.Generic;
using UnityEngine;
namespace ItemPlacer
{
    public static class EllipseHandler
    {
        public const int FULL_ANGLE = 360;
        public const int HALF_ANGLE = 180;
        public const int HOW_PRECISE = 1;

        public static PositionRotation[] CalculatePositionRotationFromItem(Vector3 center, float width, float height, GameObject item, float between, int precision = HOW_PRECISE)
        {
            List<PositionRotation> points = new List<PositionRotation>();

            GameObject i1 = GameObject.Instantiate(item, center, Quaternion.identity);
            Vector3 itemBoundsSize = item.GetComponent<Collider>().bounds.size + Vector3.one * between / 2;
            float itemDistance = itemBoundsSize.x + between;
            float angle = 0;
            float x0 = width;
            float y0 = 0;
            float x1 = 0;
            float y1 = 0;

            for (; angle < 360;)
            {
                float distance = 0;
                while (distance < MathL.GetLengthOfCubeFromAngle(itemDistance, angle))
                {
                    angle += 1 / precision;
                    x1 = width * Mathf.Sin(angle * Mathf.Deg2Rad);
                    y1 = height * Mathf.Cos(angle * Mathf.Deg2Rad);
                    distance += MathL.GetDistanceFromV2(x0, y0, x1, y1);
                    x0 = x1;
                    y0 = y1;
                    if (distance >= MathL.GetLengthOfCubeFromAngle(itemDistance, angle))
                    {
                        Vector3 pos = GetPositionFromRad(center, width, height, angle * Mathf.Deg2Rad);
                        Quaternion rot = GetRotationFromRad(angle * Mathf.Deg2Rad);
                        AddPositionToListIfNotOverlapping(points, i1, pos, rot, itemBoundsSize);
                    }
                }
            }
            GameObject.DestroyImmediate(i1);
            return points.ToArray();
        }

        public static PositionRotation[] CalculatePositionsAngleFromItemLength(Vector3 center, float width, float height, float itemDistance, int precision = HOW_PRECISE)
        {
            List<PositionRotation> points = new List<PositionRotation>();

            float angle = 0;
            float x0 = width;
            float y0 = 0;
            float x1 = 0;
            float y1 = 0;

            for (; angle < 360;)
            {
                float distance = 0;
                while (distance < MathL.GetLengthOfCubeFromAngle(itemDistance, angle))
                {
                    angle += 1 / precision;
                    x1 = width * Mathf.Sin(angle * Mathf.Deg2Rad);
                    y1 = height * Mathf.Cos(angle * Mathf.Deg2Rad);
                    distance += MathL.GetDistanceFromV2(x0, y0, x1, y1);
                    x0 = x1;
                    y0 = y1;
                    if (distance > MathL.GetLengthOfCubeFromAngle(itemDistance, angle))
                    {
                        points.Add(new PositionRotation(
                            GetPositionFromRad(center, width, height, angle * Mathf.Deg2Rad),
                            GetRotationFromRad(angle * Mathf.Deg2Rad)));
                    }
                }

            }
            return points.ToArray();
        }

        public static PositionRotation[] CalcuatePositionsBasedOnSymmetryWidth(Vector3 center, float width, float height, GameObject item, float itemDistance, int precision = HOW_PRECISE)
        {
            float itemLength = item.GetComponent<Collider>().bounds.size.x + itemDistance / 2;
            Vector3 itemBoundsSize = item.GetComponent<Collider>().bounds.size + Vector3.one * itemDistance / 2;
            GameObject i1 = GameObject.Instantiate(item, center, Quaternion.identity);

            int size = (int)(CalculateLength(width, height) / itemLength);
            List<PositionRotation> points = new List<PositionRotation>();

            for (int i = 0; i <= HALF_ANGLE * HOW_PRECISE - 2; i++)
            {
                float angle = i * Mathf.Deg2Rad / (float)precision;
                Vector3 pos = GetPositionFromRad(center, width, height, angle);
                Quaternion rot = GetRotationFromRad(angle);
                AddPositionToListIfNotOverlapping(points, i1, pos, rot, itemBoundsSize);
            }
            var part = points.ToArray();

            for (int i = 1; i < part.Length; i++)
            {
                var p = part[i];
                p.position = new Vector3(-p.position.x, p.position.y, p.position.z);
                p.rotation = Quaternion.Euler(p.rotation.eulerAngles * -1);
                part[i] = p;
            }
            points.AddRange(part);
            points.Add(new PositionRotation(
                GetPositionFromRad(center, width, height, 180 * Mathf.Deg2Rad), GetRotationFromRad(180 * Mathf.Deg2Rad)));
            GameObject.DestroyImmediate(i1);
            return points.ToArray();
        }

        // issue rotation to fix as bounds dont take it to the calc.
        // also some optimization would be suggested 
        public static PositionRotation[] CalcuatePositionsAngleFromItem(Vector3 center, float width, float height, GameObject item, float itemDistance, int precision = HOW_PRECISE)
        {
            List<PositionRotation> points = new List<PositionRotation>();
            Vector3 temp = Vector3.forward * height + center;
            Vector3 itemBoundsSize = item.GetComponent<Collider>().bounds.size + Vector3.one * itemDistance / 2;

            GameObject i1 = GameObject.Instantiate(item, temp, Quaternion.identity);

            for (int i = 0; i <= FULL_ANGLE * precision; i++)
            {
                float angle = i * Mathf.Deg2Rad / (float)precision;
                Vector3 pos = GetPositionFromRad(center, width, height, angle);
                Quaternion rot = GetRotationFromRad(angle);
                AddPositionToListIfNotOverlapping(points, i1, pos, rot, itemBoundsSize);
            }
            GameObject.DestroyImmediate(i1);

            return points.ToArray();
        }

        private static void AddPositionToListIfNotOverlapping(
            List<PositionRotation> points, GameObject i1, Vector3 pos, Quaternion rot, Vector3 itemBoundsSize)
        {
            var a = Physics.OverlapBox(pos, itemBoundsSize, rot);
            if (a.Length == 0)
            {
                i1.transform.position = pos;
                i1.transform.rotation = rot;
                points.Add(new PositionRotation(pos, rot));
            }
            Physics.SyncTransforms();
        }

        // inspired by: https://math.stackexchange.com/questions/433094/how-to-determine-the-arc-length-of-ellipse
        // and: https://stackoverflow.com/questions/40785545/is-there-a-shortcut-to-retrieve-the-angle-of-an-arc-of-an-ellipse-just-using-it
        public static PositionRotation[] CalculatePositionsAngleFromItemLengthNaive(Vector3 center, float width, float height, float itemLength)
        {
            int size = (int)(CalculateLength(width, height) / itemLength);
            List<PositionRotation> points = new List<PositionRotation>();
            Vector3 temp = Vector3.zero;

            for (int i = 0; i <= FULL_ANGLE * HOW_PRECISE; i++)
            {
                float angle = (float)(i * Mathf.Deg2Rad) / HOW_PRECISE;
                float px = Mathf.Sin(angle) * width;
                float pz = Mathf.Cos(angle) * height;

                Vector3 pos = GetPositionFromRad(center, width, height, angle);
                Quaternion rot = GetRotationFromRad(angle);

                if (Vector3.Distance(temp, pos) > itemLength)
                {
                    points.Add(new PositionRotation(pos, rot));
                    temp = pos;
                }
            }
            points.RemoveAt(points.Count - 1);
            return points.ToArray();
        }

        // inspired by: https://www.youtube.com/watch?v=mQKGRoV_jBc
        public static PositionRotation[] CalculatePositionsAngleConstMethod(Vector3 center, float width, float height, float itemLength)
        {
            int size = (int)(CalculateLength(width, height) / itemLength);
            PositionRotation[] points = new PositionRotation[size];
            for (int i = 0; i < points.Length; i++)
            {
                float angle = ((float)i / (float)size) * 360 * Mathf.Deg2Rad;

                Vector3 pos = GetPositionFromRad(center, width, height, angle);
                Quaternion rot = GetRotationFromRad(angle);

                points[i] = new PositionRotation(pos, rot);
            }
            return points;
        }

        private static Quaternion GetRotationFromRad(float radians) => Quaternion.AngleAxis((float)radians * Mathf.Rad2Deg, Vector3.up);

        private static Vector3 GetPositionFromRad(Vector3 center, float width, float height, float angle)
        {
            float px = Mathf.Sin(angle) * width;
            float pz = Mathf.Cos(angle) * height;
            Vector3 pos = new Vector3(center.x + px, center.y, center.z + pz);
            return pos;
        }

        // inspired by: https://www.youtube.com/watch?v=5nW3nJhBHL0
        /// <summary>
        /// Ramanujan first approximation of Ellipse Length. 
        /// </summary>
        /// <returns>PI{  3(a + b) - SQRT[ (3a + b) * (a + 3b) ]  }</returns>
        public static float CalculateLength(float a, float b)
        {
            // Ramanujan first approximation
            // Can be optimised (sqrt into part * part as multiplication is faster) 
            // but I don't see much need of it in this project
            // and I prefer clarity over optimization in this case
            return Mathf.PI * (3 * (a + b) - Mathf.Sqrt((3 * a + b) * (a + 3 * b)));
        }
    }
}