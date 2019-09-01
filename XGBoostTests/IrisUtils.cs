using Microsoft.VisualBasic.FileIO;
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
        ///
        ///The function LoadIris will read the specified file line by line and create an instance of the Iris POCO
        ///The class TextFieldParser from the assembly Microsoft.VisualBasic is being used here
        ///
        static internal Iris[] LoadIris(string filename)
        {
            string pathFull = System.IO.Path.Combine(Util.GetProjectDir2(), filename);
            List<Iris> records = new List<Iris>();
            using (var parser = new TextFieldParser(pathFull))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    Iris oRecord = new Iris();
                    oRecord.Col1 = float.Parse(fields[0]);
                    oRecord.Col2 = float.Parse(fields[1]);
                    oRecord.Col3 = float.Parse(fields[2]);
                    oRecord.Col4 = float.Parse(fields[3]);
                    oRecord.Petal = fields[4];
                    records.Add(oRecord);
                }
            }
            return records.ToArray();
        }
        /// <summary>
        /// Create XGBoost consumable feature vector from Iris POCO classes
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
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
