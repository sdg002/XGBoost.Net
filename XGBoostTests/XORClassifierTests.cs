using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XGBoostTests
{
    [TestClass]
    public class XORClassifierTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var xgb = new XGBoost.XGBClassifier();
            ///
            /// Generate training vectors
            ///
            int countTrainingPoints = 50;
            entity.XGBArray trainClass_0_1 = Util.GenerateRandom2dPoints(countTrainingPoints / 2, 
                0.0, 0.5,
                0.5, 1.0, 1.0);//0,1
            entity.XGBArray trainClass_1_0 = Util.GenerateRandom2dPoints(countTrainingPoints / 2,
                0.5, 1.0,
                0.0, 0.5, 1.0);//1,0
            entity.XGBArray trainClass_0_0 = Util.GenerateRandom2dPoints(countTrainingPoints / 2,
                0.0, 0.5,
                0.0, 0.5, 0.0);//0,0
            entity.XGBArray trainClass_1_1 = Util.GenerateRandom2dPoints(countTrainingPoints / 2,
                0.5, 1.0,
                0.5, 1.0, 0.0);//1,1
            ///
            /// Train the model
            ///
            entity.XGBArray allVectorsTraining = Util.UnionOfXGBArrays(trainClass_0_1,trainClass_1_0,trainClass_0_0,trainClass_1_1);
            xgb.Fit(allVectorsTraining.Vectors, allVectorsTraining.Labels);
            ///
            /// Test the model
            ///
            int countTestingPoints = 10;
            entity.XGBArray testClass_0_1 = Util.GenerateRandom2dPoints(countTestingPoints ,
                0.1, 0.4,
                0.6, 0.9, 1.0);//0,1
            entity.XGBArray testClass_1_0 = Util.GenerateRandom2dPoints(countTestingPoints,
                0.6, 0.9,
                0.1, 0.4, 1.0);//1,0
            entity.XGBArray testClass_0_0 = Util.GenerateRandom2dPoints(countTestingPoints,
                0.1, 0.4,
                0.1, 0.4, 0.0);//0,0
            entity.XGBArray testClass_1_1 = Util.GenerateRandom2dPoints(countTestingPoints,
                0.6, 0.9,
                0.6, 0.9, 0.0);//1,1
            entity.XGBArray allVectorsTest = Util.UnionOfXGBArrays(testClass_0_1, testClass_1_0,testClass_0_0,testClass_1_1);
            var resultsActual = xgb.Predict(allVectorsTest.Vectors);
            CollectionAssert.AreEqual(resultsActual, allVectorsTest.Labels);

        }
    }
}
