using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQ
{
    class Sensors
    {
        double dVal;
        int sId;
        Random rSenVal;
        public Sensors(int id)
        {
            sId = id;
            rSenVal = new Random((sId+1)+DateTime.Now.Millisecond);
            dVal = 0.0F;
        }
        public double GetValue()
        {
            dVal = rSenVal.NextDouble();
            return dVal;
        }
        public int GetSensId()
        {
            return sId;
        }

    }
}
