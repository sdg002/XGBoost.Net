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
        internal static XGBArray ConvertToXGBArray<T>(XGVector<T>[] vectorsTrain)
        {
            var arr = new XGBArray();
            arr.Labels = vectorsTrain.Select(v => v.Label).ToArray();
            arr.Vectors = vectorsTrain.Select(v => v.Features).ToArray();
            return arr;
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
