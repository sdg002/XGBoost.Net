using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XGBoostTests
{
    [TestClass]
    public class MyUnitTest
    {
        [TestMethod]
        public void TestXGBClassifierPredictProba()
        {
            var xgb = new XGBoost.XGBClassifier();
            var X = new[] 
            {
                new[] {1f, 2f, 3f, 4f, 5f},
                new[] {1f, 2f, 3f, 4f, 5f}
            };
            var y = new[] { .5f, .5f };
            xgb.Fit(X, y);
            var h = xgb.PredictProba(X);
            CollectionAssert.AreEqual(y, h[0]);
        }
        /// <summary>
        /// Simple classification of points in 2 clusters
        /// </summary>
        [TestMethod]
        public void SauMethod()
        {
            var xgb = new XGBoost.XGBClassifier();
            var train = new[]
            {
                new[] {4.0f,4.0f},
                new[] {4.1f,4.1f},
                new[] {4.1f,4.0f},
                new[] {4.0f,3.9f},
                new[] {3.9f,3.9f},

                new[] {8.0f,8.0f},
                new[] {8.1f,8.0f},
                new[] {8.1f,7.9f},
                new[] {8.0f,7.9f},
                new[] {7.9f,7.9f},
                new[] {7.9f,8.0f},

            };
            var label = new[] 
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
                0.0f,
            };
            Assert.AreEqual(train.Length, label.Length);
            var test = new[]
            {
                new[] {4.05f,4.0f},
                new[] {4.05f,4.05f},

                new[] {7.95f,8.05f},
                new[] {7.90f,8.0f},
            };
            xgb.Fit(train, label);
            var h = xgb.Predict(train);
            var hTest = xgb.Predict(test);
        }
        [TestMethod]
        public void XOR_WORK_IN_PROGRESS()
        {
            throw new NotImplementedException();
        }
    }
}
