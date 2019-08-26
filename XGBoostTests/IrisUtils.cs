using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGBoostTests.entity;
using XGBoostTests.iris;

namespace XGBoostTests
{
    public class IrisUtils
    {
        internal static XGVector<Iris>[] ConvertFromIrisToFeatureVectors(Iris[] records)
        {
            List<XGVector<Iris>> vectors = new List<XGVector<Iris>>();
            foreach (var rec in records)
            {
                XGVector<Iris> newVector = new XGVector<Iris>();
                newVector.Original = rec;
                newVector.Features = new float[]
                {
                    rec.Col1, rec.Col2,rec.Col3,rec.Col4
                };
                newVector.Label = ConvertLabelFromStringToNumeric(rec.Petal);
                vectors.Add(newVector);
            }
            return vectors.ToArray();
        }
        /// <summary>
        /// Converts the string based name of the petal to a numeric representation
        /// </summary>
        /// <param name="petal"></param>
        /// <returns></returns>
        internal static float ConvertLabelFromStringToNumeric(string petal)
        {
            if (petal.Contains("setosa"))
            {
                return 0;
            }
            else if (petal.Contains("versicolor"))
            {
                return 1.0f;
            }
            else if (petal.Contains("virginica"))
            {
                return 2.0f;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal static string ConvertLabelFromNumericToString(float v)
        {
            if (v == 0f)
            {
                return "Iris-setosa";
            }
            else if (v == 1.0f)
            {
                return "Iris-versicolor";
            }
            else if (v == 2.0f)
            {
                return "Iris-virginica";
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
