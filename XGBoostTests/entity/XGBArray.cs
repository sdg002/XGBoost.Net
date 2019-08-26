using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGBoostTests.entity
{
    /// <summary>
    /// Represents an arrays of vectors and array of labels
    /// </summary>
    public class XGBArray
    {
        public float[][] Vectors { get; set; }
        public float[] Labels { get; set; }
    }
}
