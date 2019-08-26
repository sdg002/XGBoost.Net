﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XGBoostTests.iris;

namespace XGBoostTests
{
    [TestClass]
    public class IrisUnitTest
    {
        string _fileModelIris = "Iris\\Iris.Trained.model";
        [TestMethod]
        public void BasicLoadData()
        {
            string filename = "Iris\\Iris.train.data";
            iris.Iris[] records = LoadIris(filename);
            entity.XGVector<iris.Iris>[] vectors = IrisUtils.ConvertFromIrisToFeatureVectors(records);
        }
        [TestMethod]
        public void TrainAndTestIris()
        {
            ///
            /// Load training vectors
            ///
            string filenameTrain = "Iris\\Iris.train.data";
            iris.Iris[] recordsTrain = LoadIris(filenameTrain);
            entity.XGVector<iris.Iris>[] vectorsTrain = IrisUtils.ConvertFromIrisToFeatureVectors(recordsTrain);
            ///
            /// Load testingvectors
            ///
            string filenameTest = "Iris\\Iris.test.data";
            iris.Iris[] recordsTest = LoadIris(filenameTest);
            entity.XGVector<iris.Iris>[] vectorsTest = IrisUtils.ConvertFromIrisToFeatureVectors(recordsTest);

            int noOfClasses = 3;
            var xgbc = new XGBoost.XGBClassifier(objective: "multi:softprob", numClass:3);
            entity.XGBArray arrTrain = Util.ConvertToXGBArray(vectorsTrain);
            entity.XGBArray arrTest = Util.ConvertToXGBArray(vectorsTest);
            xgbc.Fit(arrTrain.Vectors, arrTrain.Labels);
            var outcomeTest=xgbc.Predict(arrTest.Vectors);
            for(int index=0;index<arrTest.Vectors.Length;index++)
            {
                string sExpected = IrisUtils.ConvertLabelFromNumericToString(arrTest.Labels[index]);
                float[] arrResults = new float[]
                {
                    outcomeTest[index*noOfClasses +0],
                    outcomeTest[index*noOfClasses +1],
                    outcomeTest[index*noOfClasses +2]
                };
                float max = arrResults.Max();
                int indexWithMaxValue = Util.GetIndexWithMaxValue(arrResults);
                string sActualClass = IrisUtils.ConvertLabelFromNumericToString((float)indexWithMaxValue);
                Trace.WriteLine($"{index}       Expected={sExpected}        Actual={sActualClass}");
            }
            string pathFull = System.IO.Path.Combine(Util.GetProjectDir2(), _fileModelIris);
            xgbc.SaveModelToFile(pathFull);
        }
        [TestMethod]
        public void LoadModelFromFile()
        {
            string pathFull = System.IO.Path.Combine(Util.GetProjectDir2(), _fileModelIris);
            var xgbc = XGBoost.XGBClassifier.LoadClassifierFromFile(pathFull);
            ///
            /// Load testingvectors
            ///
            int noOfClasses = 3;
            string filenameTest = "Iris\\Iris.test.data";
            iris.Iris[] recordsTest = LoadIris(filenameTest);
            entity.XGVector<iris.Iris>[] vectorsTest = IrisUtils.ConvertFromIrisToFeatureVectors(recordsTest);
            entity.XGBArray arrTest = Util.ConvertToXGBArray(vectorsTest);
            var outcomeTest = xgbc.Predict(arrTest.Vectors);
            for (int index = 0; index < arrTest.Vectors.Length; index++)
            {
                string sExpected = IrisUtils.ConvertLabelFromNumericToString(arrTest.Labels[index]);
                float[] arrResults = new float[]
                {
                    outcomeTest[index*noOfClasses +0],
                    outcomeTest[index*noOfClasses +1],
                    outcomeTest[index*noOfClasses +2]
                };
                float max = arrResults.Max();
                int indexWithMaxValue = Util.GetIndexWithMaxValue(arrResults);
                string sActualClass = IrisUtils.ConvertLabelFromNumericToString((float)indexWithMaxValue);
                Trace.WriteLine($"{index}       Expected={sExpected}        Actual={sActualClass}");
            }
        }
        private Iris[] LoadIris(string filename)
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
    }
}
