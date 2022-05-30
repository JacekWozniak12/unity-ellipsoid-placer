namespace ItemPlacer
{
    using UnityEngine;

    public static class Extensions
    {
        public static void ApplyValuesFromTransform(this Transform t, Transform arg)
        {
            t.position = arg.position;
            t.rotation = arg.rotation;
            t.localScale = arg.localScale;
        }
    }

    public static class MathL
    {
        public static float SquareRootNewton(float number, float tolerance)
        {
            float x = number;
            float root;
            int count = 0;

            while (true)
            {
                count++;
                root = 0.5f * (x + (number / x));
                if (Mathf.Abs(root - x) < tolerance) break;
                x = root;
            }
            return root;
        }

        public static float GetDistanceFromV2(float x1, float y1, float x2, float y2)
        {
            return Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static float GetLengthOfCubeFromAngle(float LengthOnAngle0, float Angle)
        {
            float a = LengthOnAngle0;
            float c = Mathf.Sqrt(a * a + a * a);
            float t = Angle;
            while(t >= 45)
            {
                t -= 90;
            }              
            return Mathf.Lerp(a, c, Mathf.Clamp01(t / 45));
            
        }
    }
}