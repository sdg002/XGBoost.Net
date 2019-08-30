using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XGBoostTests
{
    [TestClass]
    public class SimpleLinearClassifierTests
    {
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
