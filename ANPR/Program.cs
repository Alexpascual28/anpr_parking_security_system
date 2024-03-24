using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ANPR
{
	class Program
	{
		static void Main(string[] args)
		{
			// ReceiveAlarmArming msgCallback = new ReceiveAlarmArming();
			// AlarmSubscription alarmSub = new AlarmSubscription();
			ReceiveAlarmListening alarmListen = new ReceiveAlarmListening();

			// alarmSub.Main();
			alarmListen.Main();
		}
	}
}
