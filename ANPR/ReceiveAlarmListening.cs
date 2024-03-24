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
      public class ReceiveAlarmListening
      {
            #region Enumeration

            enum VCA_PLATE_COLOR
            {
                  VCA_BLUE_PLATE = 0,
                  VCA_YELLOW_PLATE = 1,
                  VCA_WHITE_PLATE = 2,
                  VCA_BLACK_PLATE = 3,
                  VCA_GREEN_PLATE = 4,
                  VCA_BKAIR_PLATE = 5,
                  VCA_RED_PLATE,
                  VCA_ORANGE_PLATE,
                  VCA_OTHER = 0xff
            }

            #endregion

            int _iNum = 0;

            private void MessageCallback(int lCommand, ref HCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
            {
                  int i = 0;
                  string filename;
                  FileStream fSnapPic = null;
                  FileStream fSnapPicPlate = null;

                  //This sample code is for reference only. Actually, it is not recommended to process the data and save file in the callback function directly.
                  //You'd better process the data in the message response funcion via message mode(PostMessage).
                  switch (lCommand)
                  {
                        case HCNetSDK.COMM_ALARM:
                              HCNetSDK.NET_DVR_ALARMINFO struAlarmInfo = new HCNetSDK.NET_DVR_ALARMINFO { };
                              Marshal.PtrToStructure(pAlarmInfo, struAlarmInfo);

                              switch (struAlarmInfo.dwAlarmType)
                              {
                                    case 3: //Motion detection alarm
                                          for (i = 0; i < HCNetSDK.MAX_CHANNUM; i++) // #define MAX_CHANNUM 16 //The maximum number of channels
                                          {
                                                if (struAlarmInfo.dwChannel[i] == 1)
                                                {
                                                      Console.WriteLine("Channel Number with Motion Detection Alarm % d\n", i + 1);
                                                }
                                          }
                                          break;

                                    default:
                                          break;
                              }
                              break;

                        case HCNetSDK.COMM_UPLOAD_PLATE_RESULT:
                              HCNetSDK.NET_DVR_PLATE_RESULT struPlateResult = new HCNetSDK.NET_DVR_PLATE_RESULT();
                              Marshal.PtrToStructure(pAlarmInfo, struPlateResult);

                              Console.WriteLine("License Plate Number: %s\n", struPlateResult.struPlateInfo.sLicense); // License plate number

                              switch ((VCA_PLATE_COLOR)struPlateResult.struPlateInfo.byColor) // License plate color
                              {
                                    case VCA_PLATE_COLOR.VCA_BLUE_PLATE:
                                          Console.WriteLine("Vehicle Color: Blue\n");
                                          break;

                                    case VCA_PLATE_COLOR.VCA_YELLOW_PLATE:
                                          Console.WriteLine("Vehicle Color: Yellow\n");
                                          break;

                                    case VCA_PLATE_COLOR.VCA_WHITE_PLATE:
                                          Console.WriteLine("Vehicle Color: White\n");
                                          break;

                                    case VCA_PLATE_COLOR.VCA_BLACK_PLATE:
                                          Console.WriteLine("Vehicle Color: Black\n");
                                          break;

                                    default:
                                          break;
                              }

                              // Scene picture
                              if (struPlateResult.dwPicLen != 0 && struPlateResult.byResultType == 1)
                              {
                                    filename = $@"C://ANPR//testpic_{_iNum}.jpg";
                                    fSnapPic = File.Open(filename, FileMode.Create);

                                    byte[] bufferArray = Marshal.PtrToStructure<byte[]>(struPlateResult.pBuffer1);
                                    fSnapPic.Write(bufferArray, 0, (int)struPlateResult.dwPicLen);

                                    _iNum++;
                                    fSnapPic.Close();
                              }

                              // License plate picture
                              if (struPlateResult.dwPicPlateLen != 0 && struPlateResult.byResultType == 1)
                              {
                                    filename = $@"C://ANPR//testpic_{_iNum}.jpg";
                                    fSnapPicPlate = File.Open(filename, FileMode.Create);

                                    byte[] bufferArray = Marshal.PtrToStructure<byte[]>(struPlateResult.pBuffer1);
                                    fSnapPicPlate.Write(bufferArray, 0, (int)struPlateResult.dwPicLen);

                                    _iNum++;
                                    fSnapPicPlate.Close();
                              }

                              // Processing other data...
                              break;

                        case HCNetSDK.COMM_ITS_PLATE_RESULT:
                              HCNetSDK.NET_ITS_PLATE_RESULT struITSPlateResult = new HCNetSDK.NET_ITS_PLATE_RESULT();
                              Marshal.PtrToStructure(pAlarmInfo, struITSPlateResult);

                              for (i = 0; i < struITSPlateResult.dwPicNum; i++)
                              {
                                    Console.WriteLine("License Plate Number: %s\n", struITSPlateResult.struPlateInfo.sLicense); //License plate number

                                    switch ((VCA_PLATE_COLOR)struITSPlateResult.struPlateInfo.byColor) //License plate color
                                    {
                                          case VCA_PLATE_COLOR.VCA_BLUE_PLATE:
                                                Console.WriteLine("Vehicle Color: Blue\n");
                                                break;

                                          case VCA_PLATE_COLOR.VCA_YELLOW_PLATE:
                                                Console.WriteLine("Vehicle Color: Yellow\n");
                                                break;

                                          case VCA_PLATE_COLOR.VCA_WHITE_PLATE:
                                                Console.WriteLine("Vehicle Color: White\n");
                                                break;

                                          case VCA_PLATE_COLOR.VCA_BLACK_PLATE:
                                                Console.WriteLine("Vehicle Color: Black\n");
                                                break;

                                          default:
                                                break;
                                    }

                                    //Save scene picture
                                    if ((struITSPlateResult.struPicInfo[i].dwDataLen != 0) && (struITSPlateResult.struPicInfo[i].byType == 1) || (struITSPlateResult.struPicInfo[i].byType == 2))
                                    {
                                          filename = $@"C://ANPR//testITSpic{_iNum}_{i}.jpg";
                                          fSnapPic = File.Open(filename, FileMode.Create);

                                          byte[] bufferArray = Marshal.PtrToStructure<byte[]>(struITSPlateResult.struPicInfo[i].pBuffer);
                                          fSnapPic.Write(bufferArray, 0, (int)struITSPlateResult.struPicInfo[i].dwDataLen);

                                          _iNum++;
                                          fSnapPic.Close();
                                    }

                                    //License plate thumbnails
                                    if ((struITSPlateResult.struPicInfo[i].dwDataLen != 0) && (struITSPlateResult.struPicInfo[i].byType == 0))
                                    {
                                          filename = $@"C://ANPR//testITSpic{_iNum}_{i}.jpg";
                                          fSnapPicPlate = File.Open(filename, FileMode.Create);

                                          byte[] bufferArray = Marshal.PtrToStructure<byte[]>(struITSPlateResult.struPicInfo[i].pBuffer);
                                          fSnapPicPlate.Write(bufferArray, 0, (int)struITSPlateResult.struPicInfo[i].dwDataLen);

                                          _iNum++;
                                          fSnapPicPlate.Close();
                                    }
                                    //Processing other data...
                              }
                              break;

                        default:
                              break;
                  }
            }

            public void Main()
            {
                  //---------------------------------------
                  // Initialize
                  HCNetSDK.NET_DVR_Init();

                  //Set connection time and reconnection time
                  HCNetSDK.NET_DVR_SetConnectTime(2000, 1);
                  HCNetSDK.NET_DVR_SetReconnect(10000, 1);

                  //---------------------------------------
                  // Log in to device
                  int userID = -1;
                  HCNetSDK.NET_DVR_DEVICEINFO_V30 struDeviceInfo = new HCNetSDK.NET_DVR_DEVICEINFO_V30 { };
                  userID = HCNetSDK.NET_DVR_Login_V30("192.168.1.64", 8000, "admin", "Labmanuser", ref struDeviceInfo);

                  if (userID < 0)
                  {
                        Console.WriteLine($"Login error, {HCNetSDK.NET_DVR_GetLastError()}\n");
                        HCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  //Enable listening
                  int lHandle = -1;
                  lHandle = HCNetSDK.NET_DVR_StartListen_V30(null, 7200, MessageCallback, IntPtr.Zero);
                  if (lHandle < 0)
                  {
                        Console.WriteLine($"NET_DVR_StartListen_V30 error, {HCNetSDK.NET_DVR_GetLastError()}\n");
                        HCNetSDK.NET_DVR_Logout(userID);
                        HCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  Thread.Sleep(20000);

                  //Disable listening
                  if (!HCNetSDK.NET_DVR_StopListen_V30(lHandle))
                  {
                        Console.WriteLine($"NET_DVR_StopListen_V30 error, {HCNetSDK.NET_DVR_GetLastError()}\n");
                        HCNetSDK.NET_DVR_Logout(userID);
                        HCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  //Log out
                  HCNetSDK.NET_DVR_Logout(userID);

                  //Release SDK resource
                  HCNetSDK.NET_DVR_Cleanup();
                  return;
            }
      }
}
