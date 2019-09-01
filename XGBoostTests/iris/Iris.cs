using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGBoostTests.iris
{
    ///
    ///The C# class Iris will be used for capturing a single data row
    ///
    public class Iris
    {
        public float Col1 { get; set; }
        public float Col2 { get; set; }
        public float Col3 { get; set; }
        public float Col4 { get; set; }
        public string Petal { get; set; }
        public override string ToString()
        {
            return $"Col1={Col1}  Col2={Col2}  Col3={Col3}  Col4={Col4}  Petal={Petal}";
        }
    }
}
