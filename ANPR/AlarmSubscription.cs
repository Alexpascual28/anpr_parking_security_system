using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ANPR
{
	public class AlarmSubscription
	{
		public AlarmSubscription()
            {
            }

		public void Main()
		{
			int userId = 0;

			try
			{
				if (!CHCNetSDK.NET_DVR_Init())
				{
					var error = CHCNetSDK.NET_DVR_GetLastError();
					Console.WriteLine($"NET_DVR_Init error {error}");
					Console.ReadLine();
					return;
				}

				//Set connection time and reconnection time
				CHCNetSDK.NET_DVR_SetConnectTime(2000, 1);
				CHCNetSDK.NET_DVR_SetReconnect(10000, 1);

				var deviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
				string ipAddress = "192.168.1.64";
				short portNumber = 8000;
				string username = "admin";
				string password = "Labmanuser";

				userId = CHCNetSDK.NET_DVR_Login_V30(ipAddress, portNumber, username, password, ref deviceInfo);

				if (userId < 0)
				{
					var error = CHCNetSDK.NET_DVR_GetLastError();
					Console.WriteLine($"NET_DVR_Login_30 error {error}");
					Console.ReadLine();
					return;
				}

				HCNetSDK.NET_DVR_ALARMER alarmer1 = new HCNetSDK.NET_DVR_ALARMER();
				alarmer1.lUserID = userId;
				alarmer1.sSerialNumber = deviceInfo.sSerialNumber;

				var ipInBuffer = new NET_DVR_TRIGGER_COND()
				{
					dwSize = 1,
					dwChannel = 1,
					dwTriggerMode = ITC_TRIGGERMODE_TYPE.ITC_POST_PRS_TYPE,
					byDetSceneId = 1,
					byRes = new byte[1] { 0 }
				};

				HCNetSDK.MSGCallBack MessageCallbackNo1 = default;

				//Set alarm callback function
				HCNetSDK.NET_DVR_SetDVRMessageCallBack_V50(0, MessageCallbackNo1, IntPtr.Zero);

				//Enable arming
				HCNetSDK.NET_DVR_SETUPALARM_PARAM_V50 struSetupParam = new HCNetSDK.NET_DVR_SETUPALARM_PARAM_V50 { };
				// struSetupParam.dwSize = sizeof();

				//Alarm category to be uploaded
				struSetupParam.byAlarmInfoType = 1;

				//Arming level 
				struSetupParam.byLevel = 1;

				char[] szSubscribe = new char[1024];

				//The following code is for alarm subscription (subscribe all)
				string source = "<SubscribeEvent version=\"2.0\" xmlns=\"http://www.isapi.org / ver20 / XMLSchema\">\r\n<eventMode>all</eventMode>\r\n";
				Array.Copy(source.ToArray(), szSubscribe, source.Length);

				long lHandle = -1;

				if (szSubscribe.Length == 0)
				{
					//Arm
					lHandle = HCNetSDK.NET_DVR_SetupAlarmChan_V50(userId, ref struSetupParam, IntPtr.Zero, (uint)szSubscribe.Length);
				}
				else
				{
					//Subscribe
					// Assign memory address to pointer and store character array in this position 
					IntPtr pSubDestination = Marshal.AllocHGlobal(szSubscribe.Length);
					Marshal.StructureToPtr(szSubscribe, pSubDestination, true);

					// Pass subscription array as pointer to channel setup
					lHandle = HCNetSDK.NET_DVR_SetupAlarmChan_V50(userId, ref struSetupParam, pSubDestination, (uint)szSubscribe.Length);

					// Free pointer address memory
					Marshal.FreeHGlobal(pSubDestination);
				}

				if (lHandle < 0)
				{
					Console.WriteLine("NET_DVR_SetupAlarmChan_V30 error, %d\n", CHCNetSDK.NET_DVR_GetLastError());
					Console.ReadLine();
					return;
				}

				Thread.Sleep(2000);

				//Disarm the uploading channel
				if (!CHCNetSDK.NET_DVR_CloseAlarmChan_V30((int)lHandle))
				{
					Console.WriteLine("NET_DVR_CloseAlarmChan_V30 error, %d\n", CHCNetSDK.NET_DVR_GetLastError());
					Console.ReadLine();
					return;
				}
			}
			finally
			{
				CHCNetSDK.NET_DVR_Logout_V30(userId);
				CHCNetSDK.NET_DVR_Cleanup();
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NET_DVR_TRIGGER_COND
		{
			public uint dwSize;
			public uint dwChannel;
			public ITC_TRIGGERMODE_TYPE dwTriggerMode;
			public byte byDetSceneId;
			public byte[] byRes;
		}

		public enum ITC_TRIGGERMODE_TYPE : uint
		{
			ITC_POST_IOSPEED_TYPE = 0x1,
			ITC_POST_SINGLEIO_TYPE = 0x2,
			ITC_POST_RS485_TYPE = 0x4,
			ITC_POST_RS485_RADAR_TYPE = 0x8,
			ITC_POST_VIRTUALCOIL_TYPE = 0x10,
			ITC_POST_HVT_TYPE_V50 = 0x20,
			ITC_POST_MPR_TYPE = 0x40,
			ITC_POST_PRS_TYPE = 0x80,
			ITC_EPOLICE_IO_TRAFFICLIGHTS_TYPE = 0x100,
			ITC_EPOLICE_RS485_TYPE = 0x200,
			ITC_PE_RS485_TYPE = 0x10000,
			ITC_VIDEO_EPOLICE_TYPE = 0x20000,
			ITC_VIA_VIRTUALCOIL_TYPE = 0x40000,
			ITC_POST_IMT_TYPE = 0x80000,
			IPC_POST_HVT_TYPE = 0x100000,
			ITC_POST_MOBILE_TYPE = 0x200000,
			ITC_REDLIGHT_PEDESTRIAN_TYPE = 0x400000,
			ITC_NOCOMITY_PEDESTRIAN_TYPE = 0x800000
		}
	}
}
