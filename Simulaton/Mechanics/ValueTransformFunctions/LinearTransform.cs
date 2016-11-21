using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics.ValueTransformFunctions
{
    class LinearTransform : TransformFunction<float, float>
    {

        SortedList<float, float> points = new SortedList<float, float>();

        public LinearTransform(params KeyValuePair<float, float>[] pointList)
        //: base(x => valueAtZero + x * (valueAtOne - valueAtZero))
        {
            Array.ForEach(pointList, point => this.points.Add(point.Key, point.Value));

        }

        public float Transform(float x)
        {

            float result = x;
            float keyBefore = 0;
            float keyAfter = 0;
            foreach (float key in points.Keys)
            {
                if (x <= key)
                {
                    keyAfter = key;
                    break;
                }
                keyBefore = key;
                keyAfter = key;
            }
            float valueBefore, valueAfter;
            points.TryGetValue(keyBefore, out valueBefore);
            points.TryGetValue(keyAfter, out valueAfter);

            float slope = (valueAfter - valueBefore)/(keyAfter - keyBefore);
            result = slope*(x - keyBefore) + valueBefore;

            return result;
        }
    }
}
