using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics.ValueTransformFunctions
{
    class LinearTransform : TransformFunction<float, float>
    {
        SortedList<float, Func<float, float>> points = new SortedList<float, Func<float, float>>();

        public LinearTransform(params float[] evenlySpacedValues)
        {
            int pointCount = evenlySpacedValues.Count();
            KeyValuePair<float, float>[] pointList = new KeyValuePair<float, float>[pointCount];
            float keyDistance = 1.0f / pointCount;
            float pointKey = 0;
            for (int i = 0; i < pointCount - 1; i++)
            {
                pointList[i] = new KeyValuePair<float, float>(pointKey, evenlySpacedValues[i]);
                pointKey += keyDistance;
            }
            pointList[pointCount - 1] = new KeyValuePair<float, float>(1, evenlySpacedValues[pointCount - 1]);
            ConvertPointsToFunctions(pointList);
        }

        public LinearTransform(params KeyValuePair<float, float>[] pointList)
        {
            if (pointList.First().Key != 0) throw new ArgumentException("First point must be (0,y)");
            if (pointList.Last().Key != 1) throw new ArgumentException("Last point must be (1,y)");
            ConvertPointsToFunctions(pointList);
        }

        private void ConvertPointsToFunctions(KeyValuePair<float, float>[] pointList)
        {
            for (int i = 0; i < pointList.Count() - 1; i++)
            {
                float differenceY = (pointList[i + 1].Value - pointList[i].Value);
                float differenceX = (pointList[i + 1].Key - pointList[i].Key);
                if (differenceX == 0) throw new ArgumentException("Stepfunction not supported, X(n) and X(n+1) equal");
                float slope = differenceY / differenceX;
                points.Add(pointList[i].Key, x => slope * (x - pointList[i].Key) + pointList[i].Value);
            }
        }

        public float Transform(float x)
        {
            float result = x;
            for (int i = points.Count; i >= 0; i--)
            {
                if (x >= points.Keys[i])
                {
                    return points[points.Keys[i]](x);
                }
            }
            return 0;
        }
    }
}
