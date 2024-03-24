using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Threading;

namespace ARAD_ANPR.UserInterface.ViewModels
{
      public class MainWindowViewModel : INotifyPropertyChanged
      {
            #region Constructor

            public MainWindowViewModel()
            {
                  Init();

                  Task t = new Task(() => Main());
                  t.Start();
            }

            #endregion

            #region Fields

            private string _deviceIP;
            private string _port;
            private string _user;
            private string _password;
            private WriteableBitmap _previewImage;
            private WriteableBitmap _licensePlateImage;
            private List<ImageProcessing.LicensePlateInfo> _detectedPlatesInfo;

            #endregion

            #region Properties

            /// <summary>
            /// IP Address of Device
            /// </summary>
            public string DeviceIP
            {
                  get => _deviceIP;
                  set
                  {
                        _deviceIP = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// Port Address of Device
            /// </summary>
            public string Port
            {
                  get => _port;
                  set
                  {
                        _port = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// Username to log into device
            /// </summary>
            public string User
            {
                  get => _user;
                  set
                  {
                        _user = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// IP Address of Device
            /// </summary>
            public string Password
            {
                  get => _password;
                  set
                  {
                        _password = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// Image being displayed
            /// </summary>
            public WriteableBitmap PreviewImage
            {
                  get => _previewImage;
                  set
                  {
                        _previewImage = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// Image being displayed
            /// </summary>
            public WriteableBitmap LicensePlateImage
            {
                  get => _licensePlateImage;
                  set
                  {
                        _licensePlateImage = value;
                        OnPropertyChanged();
                  }
            }

            /// <summary>
            /// Image being displayed
            /// </summary>
            public List<ImageProcessing.LicensePlateInfo> DetectedPlatesInfo
            {
                  get => _detectedPlatesInfo;
                  set
                  {
                        _detectedPlatesInfo = value;
                        OnPropertyChanged();
                  }
            }


            // Declare the OnPropertyChanged event
            public event PropertyChangedEventHandler PropertyChanged;

            // Create the OnPropertyChanged method to raise the event
            // The calling member's name will be used as the parameter.
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }

            #endregion

            #region Methods

            private void Init()
            {
                  // Log in to device
                  int userID = 0;

                  try
                  {
                        // Initialize
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

                        userID = CHCNetSDK.NET_DVR_Login_V30(ipAddress, portNumber, username, password, ref deviceInfo);

                        if (userID < 0)
                        {
                              var error = CHCNetSDK.NET_DVR_GetLastError();
                              Console.WriteLine($"NET_DVR_Login_30 error {error}");
                              Console.ReadLine();
                              return;
                        }
                  }
                  finally
                  {
                        CHCNetSDK.NET_DVR_Logout_V30(userID);
                        CHCNetSDK.NET_DVR_Cleanup();
                  }

                  #region SampleCode

                  /*// Initialize
                  CHCNetSDK.NET_DVR_Init()

                  //Set connection time and reconnection time
                  CHCNetSDK.NET_DVR_SetConnectTime(2000, 1);
                  CHCNetSDK.NET_DVR_SetReconnect(10000, 1);

                  // Log in to device
                  long lUserID;

                  //Login parameters, including device IP address, user name, password, and so on.
                  CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLoginInfo = default;
                  struLoginInfo.bUseAsynLogin = false; //Synchronous login mode
                  struLoginInfo.sDeviceAddress = "192.168.1.64"; //Device IP address
                  struLoginInfo.wPort = 554; //Service port No.
                  struLoginInfo.sUserName = "admin"; //User name
                  struLoginInfo.sPassword = "Labmanuser"; //Password



                  //Device information, output parameter 
                  CHCNetSDK.NET_DVR_DEVICEINFO_V40 struDeviceInfoV40 = default;
                  lUserID = CHCNetSDK.NET_DVR_Login_V40(ref struLoginInfo, ref struDeviceInfoV40);

                  if (lUserID < 0)
                  {
                        Console.WriteLine($"Login failed, error code: {CHCNetSDK.NET_DVR_GetLastError()}\n");
                        CHCNetSDK.NET_DVR_Cleanup();
                        throw new Exception($"Login failed, error code: {CHCNetSDK.NET_DVR_GetLastError()}\n");
                  }

                  CHCNetSDK.MSGCallBack MessageCallbackNo1 = default;
                  CHCNetSDK.MSGCallBack MessageCallbackNo2 = default;

                  //Set alarm callback function
                  CHCNetSDK.NET_DVR_SetDVRMessageCallBack_V50(0, MessageCallbackNo1, IntPtr.Zero);
                  CHCNetSDK.NET_DVR_SetDVRMessageCallBack_V50(1, MessageCallbackNo2, IntPtr.Zero);

                  //Enable arming
                  CHCNetSDK.NET_DVR_SETUPALARM_PARAM_V50 struSetupParamV50 = default;
                  struSetupParamV50.dwSize = (uint)CHCNetSDK.NET_DVR_SetupAlarmChan_V50((int)lUserID, ref struSetupParamV50, IntPtr.Zero, struSetupParamV50.byLevel);

                  //Alarm category to be uploaded
                  struSetupParamV50.byAlarmInfoType = 1;

                  //Arming level 
                  struSetupParamV50.byLevel = 1;

                  char[] szSubscribe = new char[1024];

                  //The following code is for alarm subscription (subscribe all)
                  Array.Copy("<SubscribeEvent version=\"2.0\" xmlns=\"http://www.isapi.org / ver20 / XMLSchema\">\r\n<eventMode>all</eventMode>\r\n".ToArray(), szSubscribe, 1024);

                  long lHandle = -1;

                  if (szSubscribe.Length == 0)
                  {
                        //Arm
                        lHandle = CHCNetSDK.NET_DVR_SetupAlarmChan_V50((int)lUserID, ref struSetupParamV50, IntPtr.Zero, (uint)szSubscribe.Length);
                  }
                  else
                  {
                        //Subscribe
                        lHandle = CHCNetSDK.NET_DVR_SetupAlarmChan_V50((int)lUserID, ref struSetupParamV50, (IntPtr)Convert.ToChar(szSubscribe), (uint)szSubscribe.Length);
                  }

                  if (lHandle < 0)
                  {
                        Console.WriteLine("NET_DVR_SetupAlarmChan_V50 error, %d\n", CHCNetSDK.NET_DVR_GetLastError());
                        CHCNetSDK.NET_DVR_Logout((int)lUserID);
                        CHCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  Thread.Sleep(20000);

                  //Disarm the uploading channel
                  if (!CHCNetSDK.NET_DVR_CloseAlarmChan_V30((int)lHandle))
                  {
                        Console.WriteLine("NET_DVR_CloseAlarmChan_V30 error, %d\n", CHCNetSDK.NET_DVR_GetLastError());
                        CHCNetSDK.NET_DVR_Logout((int)lUserID);
                        CHCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  //Log out
                  CHCNetSDK.NET_DVR_Logout((int)lUserID);

                  //Release resources 
                  CHCNetSDK.NET_DVR_Cleanup();
                  return;*/

                  #endregion
            }

            private void Main()
            {
                  while (true)
                  {

                  }
            }

            #endregion
      }
}
