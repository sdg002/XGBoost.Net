using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGBoostTests.entity;
using XGBoostTests.iris;

namespace XGBoostTests
{
    public class Util
    {
        /// <summary>
        /// Returns the path to the current unit testing project. This method uses the path of the executing assembly and traverses upwards
        /// </summary>
        /// <returns></returns>
        public static string GetProjectDir2()
        {
            string pathAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folderAssembly = System.IO.Path.GetDirectoryName(pathAssembly);
            if (folderAssembly.EndsWith("\\") == false) folderAssembly = folderAssembly + "\\";
            string folderProjectLevel = System.IO.Path.GetFullPath(folderAssembly + "..\\..\\");
            Trace.WriteLine($"Project directory:{folderProjectLevel}");
            return folderProjectLevel;
        }
        static Random _rnd = new Random(DateTime.Now.Second);
        /// <summary>
        /// Generates random number of points whose X,Y fall within the specified min,max ranges
        /// When to use this function? Use this for generating labelled training vectors
        /// </summary>
        /// <param name="count">No of points to generate</param>
        /// <param name="minX">Lower limit of the X value of points</param>
        /// <param name="maxX">Upper limit of the X value of points</param>
        /// <param name="minY">Lower limit of the Y value of points</param>
        /// <param name="maxY">Upper limit of the Y value of points</param>
        /// <param name="label">The numeric class label for these points</param>
        /// <returns></returns>
        internal static XGBArray GenerateRandom2dPoints(int count, double minX, double maxX, double minY, double maxY, double label)
        {
            var lstPoints = new List<Tuple<float, float>>();//Item1=X, Item2=Y
            var lstLabels = new List<float>();
            for(int i=0;i<count;i++)
            {
                float x = (float)(_rnd.NextDouble() * (maxX - minX) + minX);
                float y = (float)(_rnd.NextDouble() * (maxY - minY) + minY);
                var tPoint = new Tuple<float, float>(x, y);
                lstPoints.Add(tPoint);
            }
            var rs= new XGBArray
            {
                Labels=Enumerable.Repeat<float>((float)label,count).ToArray(),
                Vectors = lstPoints.Select(t=> new float[] { t.Item1,t.Item2 }).ToArray()
            };
            return rs;
        }
        internal static XGBArray ConvertToXGBArray<T>(XGVector<T>[] vectorsTrain)
        {
            var arr = new XGBArray();
            arr.Labels = vectorsTrain.Select(v => v.Label).ToArray();
            arr.Vectors = vectorsTrain.Select(v => v.Features).ToArray();
            return arr;
        }
        /// <summary>
        /// Combines the lables and vectors from array 1 and array 2
        /// </summary>
        /// <param name="arr1">First array of vectors and labels</param>
        /// <param name="arr2">Second array of vectors and labels</param>
        /// <returns></returns>
        internal static XGBArray UnionOfXGBArray(XGBArray arr1, XGBArray arr2)
        {
            var arrUnion = new XGBArray
            {
                Labels = Enumerable.Concat( arr1.Labels,arr2.Labels).ToArray(),
                Vectors = Enumerable.Concat( arr1.Vectors,arr2.Vectors).ToArray()
            };
            return arrUnion;
        }

        /// <summary>
        /// Returns the index of the item in the specified array which has the highest value
        /// </summary>
        /// <param name="arrResults"></param>
        /// <returns></returns>
        internal static int GetIndexWithMaxValue(float[] arrResults)
        {
            double lastKnownMax = double.MinValue;
            int result = -1;
            for(int index=0;index<arrResults.Length;index++)
            {
                if (arrResults[index] > lastKnownMax)
                {
                    lastKnownMax = arrResults[index];
                    result = index;
                }
            }
            return result;
        }
    }
}
