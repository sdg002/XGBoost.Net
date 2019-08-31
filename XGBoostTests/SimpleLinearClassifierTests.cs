using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XGBoostTests
{
    [TestClass]
    public class SimpleLinearClassifierTests
    {
        /// <summary>
        /// Two classes of vectors - Class0 and Class1
        /// 
        /// Class0 - The vectors are centered around the point (+0.5,+0.5)
        /// Class1 - The vectors are centered around the point (-0.5,-0.5)
        /// </summary>
        [TestMethod]
        public void LinearClassification1()
        {
            var xgb = new XGBoost.XGBClassifier();
            float[][] vectorsTrain = new float[][]
            {
                new[] {0.5f,0.5f},
                new[] {0.6f,0.6f},
                new[] {0.6f,0.4f},
                new[] {0.4f,0.6f},
                new[] {0.4f,0.4f},

                new[] {-0.5f,-0.5f},
                new[] {-0.6f,-0.6f},
                new[] {-0.6f,-0.4f},
                new[] {-0.4f,-0.6f},
                new[] {-0.4f,-0.4f},
            };
            var lablesTrain = new[]
            {
                1.0f,
                1.0f,
                1.0f,
                1.0f,
                1.0f,

                0.0f,
                0.0f,
                0.0f,
                0.0f,
                0.0f,
            };
            ///
            /// Ensure count of training labels=count of training vectors
            ///
            Assert.AreEqual(vectorsTrain.Length, lablesTrain.Length);
            ///
            /// Train the model
            ///
            xgb.Fit(vectorsTrain, lablesTrain);
            ///
            /// Test the model using test vectors
            ///
            float[][] vectorsTest = new float[][]
            {
                new[] {0.55f,0.55f},
                new[] {0.55f,0.45f},
                new[] {0.45f,0.55f},
                new[] {0.45f,0.45f},

                new[] {-0.55f,-0.55f},
                new[] {-0.55f,-0.45f},
                new[] {-0.45f,-0.55f},
                new[] {-0.45f,-0.45f},
            };
            var labelsTestExpected = new[]
            {
                1.0f,
                1.0f,
                1.0f,
                1.0f,

                0.0f,
                0.0f,
                0.0f,
                0.0f,
            };
            float[] labelsTestPredicted = xgb.Predict(vectorsTest);
            ///
            /// Verify that predicted labels match the expected labels
            ///
            CollectionAssert.AreEqual(labelsTestPredicted, labelsTestExpected);
        }
        [TestMethod]
        public void LinearClassification2()
        {
            var xgb = new XGBoost.XGBClassifier();
            int countTrainingPoints = 20;
            entity.XGBArray trainClass1 = Util.GenerateRandom2dPoints(countTrainingPoints/ 2,    -1.0, 0.0,  0.0, 1.0, 0.0);//Top  left quadrant
            entity.XGBArray trainClass2 = Util.GenerateRandom2dPoints(countTrainingPoints / 2,   0.0,1.0,    -1.0,0.0, 1.0);//Bot right quadrant
            entity.XGBArray train_Class1_Class2 = Util.UnionOfXGBArray(trainClass1, trainClass2);
            xgb.Fit(train_Class1_Class2.Vectors, train_Class1_Class2.Labels);


            int countTestingPoints = 50;
            entity.XGBArray testClass1 = Util.GenerateRandom2dPoints(countTestingPoints / 2, -0.8, -0.2, 0.2, 0.8, 0.0);//Top  left quadrant
            entity.XGBArray testClass2 = Util.GenerateRandom2dPoints(countTestingPoints / 2, 0.2, 0.8, -0.8, -0.2, 1.0);//Bot right quadrant
            entity.XGBArray test_Class1_Class2 = Util.UnionOfXGBArray(testClass1, testClass2);
            var results = xgb.Predict(test_Class1_Class2.Vectors);
            CollectionAssert.AreEqual(results, test_Class1_Class2.Labels);
        }
    }
}
