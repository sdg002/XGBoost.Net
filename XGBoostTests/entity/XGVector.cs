using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XGBoostTests.entity
{
    /// <summary>
    /// Represent a vector that can be used by XGBoost
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XGVector<T>
    {
        /// <summary>
        /// The original object
        /// </summary>
        public T Original { get; set; }
        /// <summary>
        /// Attributes of the feature vector
        /// </summary>
        public float[] Features { get; set; }
        public float Label { get; set; }
        public override string ToString()
        {
            try
            {
                string sFeatures = (Features==null)? "null":string.Join("|", Features);
                string sLabel = Label.ToString();
                string sOriginal = (this.Original == null)? "null":this.Original.ToString();
                return $"Features={sFeatures} Label={sLabel} Original={sOriginal}";
            }
            catch
            {
                return "-error--";
            }
        }
    }
}
