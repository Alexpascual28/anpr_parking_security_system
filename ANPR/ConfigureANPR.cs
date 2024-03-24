using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ANPR
{
      // Chapter 2: Configure ANPR Alarm
      // If the vehicle appears in the monitoring image during a certain time period, and the recognition
      // parameters are configured the ANPR camera will capture the vehicle picture automatically Then
      // the camera analyzes the license plate and the ANPR alarm will be triggered.
      public class ConfigureANPR
      {
            const string XML_Desc_ITDeviceAbility = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                                <!--req, description of input parameter pInBuf for getting intelligent traffic
                                                capability-->
                                                <ITDeviceAbility version = ""2.0"" >
                                                 < channelNO >< !--req, xs:integer, channel No.--></channelNO> 
                                                 <ITCAbility/><!--opt, intelligent traffic capability--> 
                                                </ITDeviceAbility>";

            const int NET_DVR_GET_TRIGGEREX_CFG = 5074;
            const int NET_DVR_SET_TRIGGEREX_CFG = 5075;

            const uint _numberOfCameras = 1;

            private struct NET_DVR_TRIGGER_COND
            {
                  public uint dwSize;
                  public uint dwChannel;
                  public uint dwtriggerMode;
                  public byte byDetSceneID;
                  public byte[] byRes;
            }

            public ConfigureANPR()
            {
            }

            public void Main()
            {
                  // Before you start:
                  //---------------------------------------
                  // Initialize: Make sure you have called NET_DVR_Init to initialize the integration environment.
                  HCNetSDK.NET_DVR_Init();

                  //---------------------------------------
                  // Log in to device: Make sure you have called NET_DVR_Login_V40 to log in to the device.
                  HCNetSDK.NET_DVR_DEVICEINFO_V40 struDeviceInfo = new HCNetSDK.NET_DVR_DEVICEINFO_V40 { };
                  HCNetSDK.NET_DVR_USER_LOGIN_INFO loginInfo = new HCNetSDK.NET_DVR_USER_LOGIN_INFO
                  {
                        bUseAsynLogin = false,
                        sDeviceAddress = "192.168.1.64",
                        wPort = 8000,
                        sUserName = "admin",
                        sPassword = "Labmanuser",
                  };

                  int userID = -1;
                  userID = HCNetSDK.NET_DVR_Login_V40(ref loginInfo, ref struDeviceInfo);

                  if (userID < 0)
                  {
                        Console.Write("Login error, %d\n", HCNetSDK.NET_DVR_GetLastError());
                        HCNetSDK.NET_DVR_Cleanup();
                        return;
                  }

                  // 1. Optional: Call NET_DVR_GetDeviceAbility , set the capability type (dwAbilityType) to
                  // "DEVICE_ABILITY_INFO"(0x011), and set the input parameter pointer(pInbuf) to
                  // XML_Desc_ITDeviceAbility message for getting intelligent traffic capability to check if the
                  // following functions are supported.
                  // The intelligent traffic capability is returned in the message of XML_ITDeviceAbility, and the
                  // related node is < ITCAbility >.
                  IntPtr pInBuf = Marshal.StringToHGlobalUni(XML_Desc_ITDeviceAbility);
                  IntPtr pOutBuf = Marshal.AllocHGlobal(HCNetSDK.XML_ABILITY_OUT_LEN);

                  HCNetSDK.NET_DVR_GetDeviceAbility(userID, HCNetSDK.DEVICE_ABILITY_INFO, pInBuf, (uint)XML_Desc_ITDeviceAbility.Length, pOutBuf, HCNetSDK.XML_ABILITY_OUT_LEN);

                  // You can also call NET_DVR_STDXMLConfig to transmit the request URI: GET /ISAPI/ITC/capability
                  // to get the intelligent traffic capability and check whether the ANPR function is supported.
                  // The capability is returned in the message XML_ITCCap by lpOutBuffer of lpOutputParam.
                  string requestURI = "GET /ISAPI/ITC/capability";

                  HCNetSDK.NET_DVR_XML_CONFIG_INPUT configInput = new HCNetSDK.NET_DVR_XML_CONFIG_INPUT
                  {
                        lpRequestUrl = Marshal.StringToHGlobalUni(requestURI),
                        dwRequestUrlLen = (uint)requestURI.Length,
                  };

                  IntPtr pConfigInput = Marshal.AllocHGlobal(Marshal.SizeOf(configInput));
                  Marshal.StructureToPtr(configInput, pConfigInput, true);

                  HCNetSDK.NET_DVR_XML_CONFIG_OUTPUT configOutput = new HCNetSDK.NET_DVR_XML_CONFIG_OUTPUT { };
                  IntPtr pConfigOutput = Marshal.AllocHGlobal(Marshal.SizeOf(configOutput));
                  Marshal.StructureToPtr(configOutput, pConfigOutput, true);

                  HCNetSDK.NET_DVR_STDXMLConfig(userID, pConfigInput, pConfigOutput);

                  // 2. Optional: Call NET_DVR_GetDeviceConfig with "NET_DVR_GET_TRIGGEREX_CFG" (command
                  // No.: 5074) and set the input buffer (lpInBuffer) to the structure NET_DVR_TRIGGER_COND for
                  // getting the configured or existing triggering mode of ANPR alarm for reference.
                  // The triggering mode parameters are returned by the output buffer (lpOutBuffer) in the structure
                  // of NET_ITC_TRIGGERCFG.

                  //// NOT WORKING ////

                  /* NET_DVR_TRIGGER_COND triggerCondition = new NET_DVR_TRIGGER_COND { };
                  
                  IntPtr lpInBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(triggerCondition));
                  Marshal.StructureToPtr(triggerCondition, lpInBuffer, true);

                  HCNetSDK.NET_DVR_GetDeviceConfig(userID, NET_DVR_GET_TRIGGEREX_CFG, _numberOfCameras, lpInBuffer, Marshal.SizeOf(triggerCondition), ) */

                  //// NOT WORKING ////

                  // 3. Call NET_DVR_SetDeviceConfig with "NET_DVR_SET_TRIGGEREX_CFG"(command No.: 5075),
                  // set the input buffer (lpInBuffer) to the structure NET_DVR_TRIGGER_COND, and set the input
                  // parameter(lpInParamBuffer) to the structure NET_ITC_TRIGGERCFG for setting the triggering
                  // mode.

                  //// NOT WORKING ////

                  /*NET_DVR_TRIGGER_COND triggerCondition = new NET_DVR_TRIGGER_COND { };
                  IntPtr lpStatusList = Marshal.AllocHGlobal(4 * (int)_numberOfCameras);

                  HCNetSDK.NET_DVR_SetDeviceConfig(userID, NET_DVR_SET_TRIGGEREX_CFG, _numberOfCameras, triggerCondition, Marshal.SizeOf(triggerCondition), lpStatusList, )*/

                  //// NOT WORKING ////

                  // 4. Optional: Call NdͺsZͺGĞƚĞǀŝcĞŽnĮŐ with "NET_DVR_GET_GUARDCFG"(command No.:
                  // 3134) and set the input bƵīĞr(ůƉ / nBƵīĞƌ) to the structure NET_DVR_GUARD_COND for ŐĞƫnŐ
                  // the cŽnĮŐƵrĞĚ or ĞxŝƐtinŐ arming schedule of ANPR alarm for reference.
                  // The arming schedule parameters are returned by the output bƵīĞr(ůƉKƵƚBƵīĞƌ) in the
                  // structure of NET_DVR_GUARD_CFG.

                  // 5. Call NdͺsZͺ ^ ĞƚĞǀŝcĞŽnĮŐ with "NET_DVR_SET_GUARDCFG"(command No.: 3135), set the
                  // input bƵīĞr(ůƉ / nBƵīĞƌ) to the structure NET_DVR_GUARD_COND, and set the input parameter
                  // (ůƉ / nWĂƌĂmBƵīĞƌ) to the structure NET_DVR_GUARD_CFG for ƐĞƫnŐ arming schedule.

                  // 6. Optional: CŽnĮŐƵrĞ parameters to display license plate ŝnĨŽrmĂtiŽn on alarm picture.

                  // 1) KƉtiŽnĂů͗ Call NdͺsZͺGĞƚĞǀŝcĞŽnĮŐ with "NET_ITS_GET_OVERLAP_CFG_V50"
                  // (command No.: 5055) and set the input bƵīĞr(ůƉ / nBƵīĞƌ) to the structure
                  // NET_ITS_OVERLAPCFG_COND for ŐĞƫnŐ the cŽnĮŐƵrĞĚ or ĞxŝƐtinŐ overlay parameters for
                  // reference.
                  // The overlay parameters are returned by the output bƵīĞr(ůƉKƵƚBƵīĞƌ) in the structure of
                  // NET_ITS_OVERLAP_CFG_V50.

                  // 2) Call NdͺsZͺ ^ ĞƚĞǀŝcĞŽnĮŐ with "NET_ITS_SET_OVERLAP_CFG_V50"(command No.:
                  // 5056), set the input bƵīĞr(ůƉ/ nBƵīĞƌ) to the structure NET_ITS_OVERLAPCFG_COND, and
                  // set the input parameter(ůƉ/ nWĂƌĂmBƵīĞƌ) to the structure NET_ITS_OVERLAP_CFG_V50 for
                  // ƐĞƫnŐ the parameters to display license plate ŝnĨŽrmĂtiŽn on alarm picture.

                  // 7. Optional: Call NdͺsZͺ ^ dyM >ŽnĮŐ to transmit ͬ/ ^W /ͬdƌĂĸcͬcŚĂnnĞůƐͬф /хͬůŝcĞnƐĞWůĂƚĞͬ
                  // ĮůƚƌĂƟŽn͍ĨŽƌmĂƚсũƐŽn by PUT method and set lpInputParam to: ^KNͺFŝůƚƌĂƟŽn to ĮůƚĞr the
                  // duplicated license plates and receive the same alarm just for once.

                  // Note: To check whether the device supports ĮůƚĞrŝnŐ duplicated license plates, you can call
                  // NdͺsZͺ ^ dyM >ŽnĮŐ to transmit ͬ/ ^W /ͬdƌĂĸcͬcŚĂnnĞůƐͬф /хͬcĂƉĂbŝůŝƟĞƐ by GET
                  // method.The capability will be returned in the message :^KNͺFŝůƚƌĂƟŽn by lpOutputParam.If it
                  // supports, the node < ŝƐ ^ ƵƉƉŽƌƚFŝůƚƌĂƟŽn > will be in the capability message and its value is "true".

                  // 8. Optional: Receive ANPR alarm in arming mode(see Receive Alarm / Event in Arming Mode) or
                  // listening mode(see Receive Alarm / Event in Listening Mode) when alarm is triggered.

                  // Note: The command(lCommand) to receive ANPR alarms should be set to
                  // "COMM_ITS_PLATE_RESULT"(command No.: 0x3050) or "COMM_UPLOAD_PLATE_RESULT"
                  // (command No.: 0x2800) in the alarm callback ĨƵnctiŽn MSGCallBack.

                  // For alarm details, refer to yM > ͺǀĞnƚNŽƟĮcĂƟŽnůĞƌƚͺNWZ returned in the ĮĞůĚ pXmlBuf of
                  // NET_DVR_PLATE_INFO in the structure NET_ITS_PLATE_RESULT or NET_DVR_PLATE_RESULT.

                  // What to do next: Call NET_DVR_Logout and NET_DVR_Cleanup to log out from device and release resources.
            }

      }
}
