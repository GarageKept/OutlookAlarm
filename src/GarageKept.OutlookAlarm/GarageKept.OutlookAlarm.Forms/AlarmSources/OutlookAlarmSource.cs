using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageKept.OutlookAlarm.Forms.Interfaces;

namespace GarageKept.OutlookAlarm.Forms.AlarmSources
{
    internal class OutlookAlarmSource : IAlarmSource
    {
        public List<IAlarm> GetAlarms(int hours)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
