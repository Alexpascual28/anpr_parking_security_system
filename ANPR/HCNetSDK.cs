using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace ANPR
{
    /// <summary>
    /// CHCNetSDK 的摘要说明。
    /// </summary>
    public class HCNetSDK
    {

        public HCNetSDK()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 公共参数
        public static string ProtocolType;

        #endregion

        /************************************视频综合平台(begin)***************************************/
        public const int MAX_SUBSYSTEM_NUM = 80;   //一个矩阵系统中最多子系统数量
        public const int MAX_SERIALLEN = 36;  //最大序列号长度

        public const int MAX_LOOPPLANNUM = 16;  //最大计划切换组
        public const int DECODE_TIMESEGMENT = 4;     //计划解码每天时间段数

        public const int MAX_DOMAIN_NAME = 64;  /* 最大域名长度 */
        public const int MAX_DISKNUM_V30 = 33; //9000设备最大硬盘数/* 最多33个硬盘(包括16个内置SATA硬盘、1个eSATA硬盘和16个NFS盘) */
        public const int MAX_DAYS = 7;       //每周天数

        public const uint DEVICE_ABILITY_INFO = 0x011;     //设备事件能力（V2.0）


        public const int MAX_ID_NUM_LEN = 32;  //最大身份证号长度
        public const int MAX_ID_NAME_LEN = 128;   //最大姓名长度
        public const int MAX_ID_ADDR_LEN = 280;   //最大住址长度
        public const int MAX_ID_ISSUING_AUTHORITY_LEN = 128; //最大签发机关长度

        public const int MAX_ID_LEN = 48;
        public const int MAX_PARKNO_LEN = 16;
        public const int MAX_HUMAN_BIRTHDATE_LEN = 10;
        public const int CID_CODE_LEN = 4;
        public const int ACCOUNTNUM_LEN = 6;
        public const int ACCOUNTNUM_LEN_32 = 32;

        public const int MAX_CATEGORY_LEN = 8; //车牌附加信息最大字符

        public const int MAX_LEN_XML = 10 * 1024 * 1024; //xml最大长度

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct NET_DVR_SUBSYSTEMINFO
        {
            public byte bySubSystemType;//子系统类型，1-解码用子系统，2-编码用子系统，0-NULL（此参数只能获取）
            public byte byChan;//子系统通道数（此参数只能获取）
            public byte byLoginType;//注册类型，1-直连，2-DNS，3-花生壳
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struSubSystemIP;/*IP地址（可修改）*/
            public ushort wSubSystemPort;//子系统端口号（可修改）
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;

            public NET_DVR_IPADDR struSubSystemIPMask;//子网掩码
            public NET_DVR_IPADDR struGatewayIpAddr;    /* 网关地址*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 （此参数只能获取）*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*密码（此参数只能获取）*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDomainName;//域名(可修改)
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDnsAddress;/*DNS域名或IP地址*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号（此参数只能获取）
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALLSUBSYSTEMINFO
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_SUBSYSTEM_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SUBSYSTEMINFO[] struSubSystemInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_SUBCFG
        {
            public uint dwSize;
            public uint dwPoolTime; /*轮询间隔，单位：秒*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struChanConInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_ARRAYCFG
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_LOOPPLANNUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_LOOPPLAN_SUBCFG[] struLoopPlanSubCfg;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALARMMODECFG
        {
            public uint dwSize;
            public byte byAlarmMode;//报警触发类型，1-轮询，2-保持 
            public ushort wLoopTime;//轮询时间, 单位：秒 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESYSTEMINFO
        {
            public byte bySerialNum;//子系统序号
            public byte byChan;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERINFO
        {
            public uint dwSize;
            public NET_DVR_IPADDR struIP;/*码分器IP地址*/
            public ushort wPort;//码分器端口号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*密码 */
            public byte byChan;//码分器485号
            public byte by485Port;//485口地址
            public ushort wBaudRate;// 波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;//数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位：0－1位，1－2位;
            public byte byParity;// 校验：0－无校验；1－奇校验；2－偶校验;
            public byte byFlowControl;// 0－无；1－软流控；2-硬流控
            public ushort wDecoderType;// 解码器类型, 从0开始，与获取的解码器协议列表中的下标相对应
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERCFG
        {
            public uint dwSize;
            public NET_DVR_CODESYSTEMINFO struCodeSubsystemInfo;//编码子系统对应信息
            public NET_DVR_CODESPLITTERINFO struCodeSplitterInfo;//码分器信息
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ASSOCIATECFG
        {
            public byte byAssociateType;//关联类型，1-报警
            public ushort wAlarmDelay;//报警延时，0－5秒；1－10秒；2－30秒；3－1分钟；4－2分钟；5－5分钟；6－10分钟；
            public byte byAlarmNum;//报警号，具体的值由应用赋，相同的报警赋相同的值
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DYNAMICDECODE
        {
            public uint dwSize;
            public NET_DVR_ASSOCIATECFG struAssociateCfg;//触发动态解码关联结构
            public NET_DVR_PU_STREAM_CFG struPuStreamCfg;//动态解码结构
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DECODESCHED
        {
            public NET_DVR_SCHEDTIME struSchedTime;
            public byte byDecodeType;/*0-无，1-轮询解码，2-动态解码*/
            public byte byLoopGroup;//轮询组号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PU_STREAM_CFG struDynamicDec;//动态解码
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PLANDECODE
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_DAYS * DECODE_TIMESEGMENT, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_DECODESCHED[] struDecodeSched;//周一作为开始，和9000一致
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byres;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOPLATFORM_ABILITY
        {
            public uint dwSize;
            public byte byCodeSubSystemNums;//编码子系统数量
            public byte byDecodeSubSystemNums;//解码子系统数量
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byWindowMode; /*显示通道支持的窗口模式*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int VIDEOPLATFORM_ABILITY = 0x210; //视频综合平台能力集
        /************************************视频综合平台(end)***************************************/

        //SDK类型
        public const int SDK_PLAYMPEG4 = 1;//播放库
        public const int SDK_HCNETSDK = 2;//网络库

        //数据库操作NVR信息
        //数据库操作
        public const int INSERTTYPE = 0;        //代表插入
        public const int MODIFYTYPE = 1;        //代表修改
        public const int DELETETYPE = 2;        //代表删除
        /****************************************日志操作******************************************/
        //操作类型
        public const int DEF_OPE_PREVIEW = 1;   //预览
        public const int DEF_OPE_TALK = 2;  //对讲
        public const int DEF_OPE_SETALARM = 3;   //布防
        public const int DEF_OPE_PTZCTRL = 4;   //云台控制
        public const int DEF_OPE_VIDEOPARAM = 5;   //视频参数设置
        public const int DEF_OPE_PLAYBACK = 6;   //回放
        public const int DEF_OPE_REMOTECFG = 7;   //远程配置
        public const int DEF_OPE_GETSERVSTATE = 8;   //获取设备状态
        public const int DEF_OPE_CHECKTIME = 9;   //校时



        //操作日志内容    
        public const int DEF_OPE_PRE_STARTPREVIEW = 1;   //开始预览
        public const int DEF_OPE_PRE_STOPPREVIEW = 2;   //停止预览
        public const int DEF_OPE_PRE_STRATCYCPLAY = 3;   //开始循环播放
        public const int DEF_OPE_PRE_STOPCYCPLAY = 4;   //停止循环播放
        public const int DEF_OPE_PRE_STARTRECORD = 5;   //开始录像
        public const int DEF_OPE_PRE_STOPRECORD = 6;   //停止录像
        public const int DEF_OPE_PRE_CAPTURE = 7;   //抓图
        public const int DEF_OPE_PRE_OPENSOUND = 8;   //打开声音
        public const int DEF_OPE_PRE_CLOSESOUND = 9;   //关闭声音

        //对讲
        public const int DEF_OPE_TALK_STARTTALK = 1;   //开始对讲
        public const int DEF_OPE_TALK_STOPTALK = 2;   //停止对讲

        public const int DEF_OPE_ALARM_SETALARM = 1;   //布防
        public const int DEF_OPE_ALARM_WITHDRAWALARM = 2;   //撤防

        public const int DEF_OPE_PTZ_PTZCTRL = 1;   //云台控制

        public const int DEF_OPE_VIDEOPARAM_SET = 1;   //视频参数

        //回放
        public const int DEF_OPE_PLAYBACK_LOCALSEARCH = 1;   //本地回放查询文件
        public const int DEF_OPE_PLAYBACK_LOCALPLAY = 2;   //本地回放文件
        public const int DEF_OPE_PLAYBACK_LOCALDOWNLOAD = 3;   //本地回放下载文件
        public const int DEF_OPE_PLAYBACK_REMOTESEARCH = 4;   //远程回放查询文件
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYFILE = 5;   //远程按文件回放
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYTIME = 6;   //远程按时间回放
        public const int DEF_OPE_PLAYBACK_REMOTEDOWNLOAD = 7;   //远程回放下载文件

        public const int DEF_OPE_REMOTE_REMOTECFG = 1;   //远程参数配置

        public const int DEF_OPE_STATE_GETSERVSTATE = 1;//获取设备状态

        public const int DEF_OPE_CHECKT_CHECKTIME = 1;//校时

        //报警类型
        public const int DEF_ALARM_IO = 1;   //信号量报警
        public const int DEF_ALARM_HARDFULL = 2;   //硬盘满报警
        public const int DEF_ALARM_VL = 3;  //视频信号丢失报警
        public const int DEF_ALARM_MV = 4;     //移动侦测报警
        public const int DEF_ALARM_HARDFORMAT = 5;   //硬盘未格式化报警
        public const int DEF_ALARM_HARDERROR = 6;   //硬盘错报警
        public const int DEF_ALARM_VH = 7;     //遮挡报警
        public const int DEF_ALARM_NOPATCH = 8;   //制式不匹配报警
        public const int DEF_ALARM_ERRORVISIT = 9;   //非法访问报警
        public const int DEF_ALARM_EXCEPTION = 10;  //巡检异常
        public const int DEF_ALARM_RECERROR = 11;  //巡检异常

        //系统日志类型
        public const int DEF_SYS_LOGIN = 1;   //登陆 
        public const int DEF_SYS_LOGOUT = 2;   //注销
        public const int DEF_SYS_LOCALCFG = 3;   //本地配置

        /****************************************日志操作******************************************/


        public const int NAME_LEN = 32;//用户名长度
        public const int PASSWD_LEN = 16;//密码长度
        public const int MAX_NAMELEN = 16;//DVR本地登陆名
        public const int MAX_RIGHT = 32;//设备支持的权限（1-12表示本地权限，13-32表示远程权限）
        public const int SERIALNO_LEN = 48;//序列号长度
        public const int MACADDR_LEN = 6;//mac地址长度
        public const int MAX_ETHERNET = 2;//设备可配以太网络
        public const int PATHNAME_LEN = 128;//路径长度

        public const int MAX_TIMESEGMENT_V30 = 8;//9000设备最大时间段数
        public const int MAX_TIMESEGMENT = 4;//8000设备最大时间段数

        public const int MAX_SHELTERNUM = 4;//8000设备最大遮挡区域数
        public const int PHONENUMBER_LEN = 32;//pppoe拨号号码最大长度

        public const int MAX_DISKNUM = 16;//8000设备最大硬盘数
        public const int MAX_DISKNUM_V10 = 8;//1.2版本之前版本

        public const int MAX_WINDOW_V30 = 32;//9000设备本地显示最大播放窗口数
        public const int MAX_WINDOW = 16;//8000设备最大硬盘数
        public const int MAX_VGA_V30 = 4;//9000设备最大可接VGA数
        public const int MAX_VGA = 1;//8000设备最大可接VGA数

        public const int MAX_USERNUM_V30 = 32;//9000设备最大用户数
        public const int MAX_USERNUM = 16;//8000设备最大用户数
        public const int MAX_EXCEPTIONNUM_V30 = 32;//9000设备最大异常处理数
        public const int MAX_EXCEPTIONNUM = 16;//8000设备最大异常处理数
        public const int MAX_LINK = 6;//8000设备单通道最大视频流连接数

        public const int MAX_DECPOOLNUM = 4;//单路解码器每个解码通道最大可循环解码数
        public const int MAX_DECNUM = 4;//单路解码器的最大解码通道数（实际只有一个，其他三个保留）
        public const int MAX_TRANSPARENTNUM = 2;//单路解码器可配置最大透明通道数
        public const int MAX_CYCLE_CHAN = 16; //单路解码器最大轮循通道数
        public const int MAX_CYCLE_CHAN_V30 = 64;//最大轮询通道数（扩展）
        public const int MAX_DIRNAME_LENGTH = 80;//最大目录长度
        public const int MAX_WINDOWS = 16;//最大窗口数

        public const int MAX_STRINGNUM_V30 = 8;//9000设备最大OSD字符行数数
        public const int MAX_STRINGNUM = 4;//8000设备最大OSD字符行数数
        public const int MAX_STRINGNUM_EX = 8;//8000定制扩展
        public const int MAX_AUXOUT_V30 = 16;//9000设备最大辅助输出数
        public const int MAX_AUXOUT = 4;//8000设备最大辅助输出数
        public const int MAX_HD_GROUP = 16;//9000设备最大硬盘组数
        public const int MAX_NFS_DISK = 8; //8000设备最大NFS硬盘数

        public const int IW_ESSID_MAX_SIZE = 32;//WIFI的SSID号长度
        public const int IW_ENCODING_TOKEN_MAX = 32;//WIFI密锁最大字节数
        public const int MAX_SERIAL_NUM = 64;//最多支持的透明通道路数
        public const int MAX_DDNS_NUMS = 10;//9000设备最大可配ddns数
        public const int MAX_EMAIL_ADDR_LEN = 48;//最大email地址长度
        public const int MAX_EMAIL_PWD_LEN = 32;//最大email密码长度

        public const int MAXPROGRESS = 100;//回放时的最大百分率
        public const int MAX_SERIALNUM = 2;//8000设备支持的串口数 1-232， 2-485
        public const int CARDNUM_LEN = 20;//卡号长度
        public const int MAX_VIDEOOUT_V30 = 4;//9000设备的视频输出数
        public const int MAX_VIDEOOUT = 2;//8000设备的视频输出数

        public const int MAX_PRESET_V30 = 256;// 9000设备支持的云台预置点数
        public const int MAX_TRACK_V30 = 256;// 9000设备支持的云台轨迹数
        public const int MAX_CRUISE_V30 = 256;// 9000设备支持的云台巡航数
        public const int MAX_PRESET = 128;// 8000设备支持的云台预置点数 
        public const int MAX_TRACK = 128;// 8000设备支持的云台轨迹数
        public const int MAX_CRUISE = 128;// 8000设备支持的云台巡航数 

        public const int CRUISE_MAX_PRESET_NUMS = 32;// 一条巡航最多的巡航点 

        public const int MAX_SERIAL_PORT = 8;//9000设备支持232串口数
        public const int MAX_PREVIEW_MODE = 8;// 设备支持最大预览模式数目 1画面,4画面,9画面,16画面.... 
        public const int MAX_MATRIXOUT = 16;// 最大模拟矩阵输出个数 
        public const int LOG_INFO_LEN = 11840; // 日志附加信息 
        public const int DESC_LEN = 16;// 云台描述字符串长度 
        public const int PTZ_PROTOCOL_NUM = 200;// 9000最大支持的云台协议数 

        public const int MAX_AUDIO = 1;//8000语音对讲通道数
        public const int MAX_AUDIO_V30 = 2;//9000语音对讲通道数
        public const int MAX_CHANNUM = 16;//8000设备最大通道数
        public const int MAX_ALARMIN = 16;//8000设备最大报警输入数
        public const int MAX_ALARMOUT = 4;//8000设备最大报警输出数
        //9000 IPC接入
        public const int MAX_ANALOG_CHANNUM = 32;//最大32个模拟通道
        public const int MAX_ANALOG_ALARMOUT = 32; //最大32路模拟报警输出 
        public const int MAX_ANALOG_ALARMIN = 32;//最大32路模拟报警输入

        public const int MAX_IP_DEVICE = 32;//允许接入的最大IP设备数
        public const int MAX_IP_CHANNEL = 32;//允许加入的最多IP通道数
        public const int MAX_IP_ALARMIN = 128;//允许加入的最多报警输入数
        public const int MAX_IP_ALARMOUT = 64;//允许加入的最多报警输出数

        //SDK_V31 ATM
        public const int MAX_ATM_NUM = 1;
        public const int MAX_ACTION_TYPE = 12;
        public const int ATM_FRAMETYPE_NUM = 4;
        public const int MAX_ATM_PROTOCOL_NUM = 1025;
        public const int ATM_PROTOCOL_SORT = 4;
        public const int ATM_DESC_LEN = 32;
        // SDK_V31 ATM

        /* 最大支持的通道数 最大模拟加上最大IP支持 */
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160

        public const int MAX_INTERVAL_NUM = 4;

        //码流连接方式
        public const int NORMALCONNECT = 1;
        public const int MEDIACONNECT = 2;

        //设备型号(大类)
        public const int HCDVR = 1;
        public const int MEDVR = 2;
        public const int PCDVR = 3;
        public const int HC_9000 = 4;
        public const int HF_I = 5;
        public const int PCNVR = 6;
        public const int HC_76NVR = 8;

        //NVR类型
        public const int DS8000HC_NVR = 0;
        public const int DS9000HC_NVR = 1;
        public const int DS8000ME_NVR = 2;

        //在线测温报警
        public const int THERMOMETRY_ALARMRULE_NUM = 40;           //热成像报警规则数
        public const int MAX_THERMOMETRY_REGION_NUM = 40;         //热度图检测区域最大支持数
        public const int MAX_THERMOMETRY_DIFFCOMPARISON_NUM = 40; //热成像温差报警规则数
        public const int NET_SDK_MAX_THERMOMETRYALGNAME = 128; //测温算法库版本最大长度
        public const int NET_SDK_MAX_SHIPSALGNAME = 128; //船只算法库版本最大长度
        public const int NET_SDK_MAX_FIRESALGNAME = 128; //火点算法库版本最大长度

        public const int ISAPI_STATUS_LEN = 4096 * 4;
        public const int ISAPI_DATA_LEN = 4096 * 4;

        /*******************全局错误码 begin**********************/
        public const int NET_DVR_NOERROR = 0;//没有错误
        public const int NET_DVR_PASSWORD_ERROR = 1;//用户名密码错误
        public const int NET_DVR_NOENOUGHPRI = 2;//权限不足
        public const int NET_DVR_NOINIT = 3;//没有初始化
        public const int NET_DVR_CHANNEL_ERROR = 4;//通道号错误
        public const int NET_DVR_OVER_MAXLINK = 5;//连接到DVR的客户端个数超过最大
        public const int NET_DVR_VERSIONNOMATCH = 6;//版本不匹配
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;//连接服务器失败
        public const int NET_DVR_NETWORK_SEND_ERROR = 8;//向服务器发送失败
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;//从服务器接收数据失败
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;//从服务器接收数据超时
        public const int NET_DVR_NETWORK_ERRORDATA = 11;//传送的数据有误
        public const int NET_DVR_ORDER_ERROR = 12;//调用次序错误
        public const int NET_DVR_OPERNOPERMIT = 13;//无此权限
        public const int NET_DVR_COMMANDTIMEOUT = 14;//DVR命令执行超时
        public const int NET_DVR_ERRORSERIALPORT = 15;//串口号错误
        public const int NET_DVR_ERRORALARMPORT = 16;//报警端口错误
        public const int NET_DVR_PARAMETER_ERROR = 17;//参数错误
        public const int NET_DVR_CHAN_EXCEPTION = 18;//服务器通道处于错误状态
        public const int NET_DVR_NODISK = 19;//没有硬盘
        public const int NET_DVR_ERRORDISKNUM = 20;//硬盘号错误
        public const int NET_DVR_DISK_FULL = 21;//服务器硬盘满
        public const int NET_DVR_DISK_ERROR = 22;//服务器硬盘出错
        public const int NET_DVR_NOSUPPORT = 23;//服务器不支持
        public const int NET_DVR_BUSY = 24;//服务器忙
        public const int NET_DVR_MODIFY_FAIL = 25;//服务器修改不成功
        public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;//密码输入格式不正确
        public const int NET_DVR_DISK_FORMATING = 27;//硬盘正在格式化，不能启动操作
        public const int NET_DVR_DVRNORESOURCE = 28;//DVR资源不足
        public const int NET_DVR_DVROPRATEFAILED = 29;//DVR操作失败
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;//打开PC声音失败
        public const int NET_DVR_DVRVOICEOPENED = 31;//服务器语音对讲被占用
        public const int NET_DVR_TIMEINPUTERROR = 32;//时间输入不正确
        public const int NET_DVR_NOSPECFILE = 33;//回放时服务器没有指定的文件
        public const int NET_DVR_CREATEFILE_ERROR = 34;//创建文件出错
        public const int NET_DVR_FILEOPENFAIL = 35;//打开文件出错
        public const int NET_DVR_OPERNOTFINISH = 36; //上次的操作还没有完成
        public const int NET_DVR_GETPLAYTIMEFAIL = 37;//获取当前播放的时间出错
        public const int NET_DVR_PLAYFAIL = 38;//播放出错
        public const int NET_DVR_FILEFORMAT_ERROR = 39;//文件格式不正确
        public const int NET_DVR_DIR_ERROR = 40;//路径错误
        public const int NET_DVR_ALLOC_RESOURCE_ERROR = 41;//资源分配错误
        public const int NET_DVR_AUDIO_MODE_ERROR = 42;//声卡模式错误
        public const int NET_DVR_NOENOUGH_BUF = 43;//缓冲区太小
        public const int NET_DVR_CREATESOCKET_ERROR = 44;//创建SOCKET出错
        public const int NET_DVR_SETSOCKET_ERROR = 45;//设置SOCKET出错
        public const int NET_DVR_MAX_NUM = 46;//个数达到最大
        public const int NET_DVR_USERNOTEXIST = 47;//用户不存在
        public const int NET_DVR_WRITEFLASHERROR = 48;//写FLASH出错
        public const int NET_DVR_UPGRADEFAIL = 49;//DVR升级失败
        public const int NET_DVR_CARDHAVEINIT = 50;//解码卡已经初始化过
        public const int NET_DVR_PLAYERFAILED = 51;//调用播放库中某个函数失败
        public const int NET_DVR_MAX_USERNUM = 52;//设备端用户数达到最大
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;//获得客户端的IP地址或物理地址失败
        public const int NET_DVR_NOENCODEING = 54;//该通道没有编码
        public const int NET_DVR_IPMISMATCH = 55;//IP地址不匹配
        public const int NET_DVR_MACMISMATCH = 56;//MAC地址不匹配
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;//升级文件语言不匹配
        public const int NET_DVR_MAX_PLAYERPORT = 58;//播放器路数达到最大
        public const int NET_DVR_NOSPACEBACKUP = 59;//备份设备中没有足够空间进行备份
        public const int NET_DVR_NODEVICEBACKUP = 60;//没有找到指定的备份设备
        public const int NET_DVR_PICTURE_BITS_ERROR = 61;//图像素位数不符，限24色
        public const int NET_DVR_PICTURE_DIMENSION_ERROR = 62;//图片高*宽超限， 限128*256
        public const int NET_DVR_PICTURE_SIZ_ERROR = 63;//图片大小超限，限100K
        public const int NET_DVR_LOADPLAYERSDKFAILED = 64;//载入当前目录下Player Sdk出错
        public const int NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//找不到Player Sdk中某个函数入口
        public const int NET_DVR_LOADDSSDKFAILED = 66;//载入当前目录下DSsdk出错
        public const int NET_DVR_LOADDSSDKPROC_ERROR = 67;//找不到DsSdk中某个函数入口
        public const int NET_DVR_DSSDK_ERROR = 68;//调用硬解码库DsSdk中某个函数失败
        public const int NET_DVR_VOICEMONOPOLIZE = 69;//声卡被独占
        public const int NET_DVR_JOINMULTICASTFAILED = 70;//加入多播组失败
        public const int NET_DVR_CREATEDIR_ERROR = 71;//建立日志文件目录失败
        public const int NET_DVR_BINDSOCKET_ERROR = 72;//绑定套接字失败
        public const int NET_DVR_SOCKETCLOSE_ERROR = 73;//socket连接中断，此错误通常是由于连接中断或目的地不可达
        public const int NET_DVR_USERID_ISUSING = 74;//注销时用户ID正在进行某操作
        public const int NET_DVR_SOCKETLISTEN_ERROR = 75;//监听失败
        public const int NET_DVR_PROGRAM_EXCEPTION = 76;//程序异常
        public const int NET_DVR_WRITEFILE_FAILED = 77;//写文件失败
        public const int NET_DVR_FORMAT_READONLY = 78;//禁止格式化只读硬盘
        public const int NET_DVR_WITHSAMEUSERNAME = 79;//用户配置结构中存在相同的用户名
        public const int NET_DVR_DEVICETYPE_ERROR = 80;//导入参数时设备型号不匹配
        public const int NET_DVR_LANGUAGE_ERROR = 81;//导入参数时语言不匹配
        public const int NET_DVR_PARAVERSION_ERROR = 82;//导入参数时软件版本不匹配
        public const int NET_DVR_IPCHAN_NOTALIVE = 83; //预览时外接IP通道不在线
        public const int NET_DVR_RTSP_SDK_ERROR = 84;//加载高清IPC通讯库StreamTransClient.dll失败
        public const int NET_DVR_CONVERT_SDK_ERROR = 85;//加载转码库失败
        public const int NET_DVR_IPC_COUNT_OVERFLOW = 86;//超出最大的ip接入通道数

        public const int NET_PLAYM4_NOERROR = 500;//no error
        public const int NET_PLAYM4_PARA_OVER = 501;//input parameter is invalid
        public const int NET_PLAYM4_ORDER_ERROR = 502;//The order of the function to be called is error
        public const int NET_PLAYM4_TIMER_ERROR = 503;//Create multimedia clock failed
        public const int NET_PLAYM4_DEC_VIDEO_ERROR = 504;//Decode video data failed
        public const int NET_PLAYM4_DEC_AUDIO_ERROR = 505;//Decode audio data failed
        public const int NET_PLAYM4_ALLOC_MEMORY_ERROR = 506;//Allocate memory failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR = 507;//Open the file failed
        public const int NET_PLAYM4_CREATE_OBJ_ERROR = 508;//Create thread or event failed
        public const int NET_PLAYM4_CREATE_DDRAW_ERROR = 509;//Create DirectDraw object failed
        public const int NET_PLAYM4_CREATE_OFFSCREEN_ERROR = 510;//failed when creating off-screen surface
        public const int NET_PLAYM4_BUF_OVER = 511;//buffer is overflow
        public const int NET_PLAYM4_CREATE_SOUND_ERROR = 512;//failed when creating audio device
        public const int NET_PLAYM4_SET_VOLUME_ERROR = 513;//Set volume failed
        public const int NET_PLAYM4_SUPPORT_FILE_ONLY = 514;//The function only support play file
        public const int NET_PLAYM4_SUPPORT_STREAM_ONLY = 515;//The function only support play stream
        public const int NET_PLAYM4_SYS_NOT_SUPPORT = 516;//System not support
        public const int NET_PLAYM4_FILEHEADER_UNKNOWN = 517;//No file header
        public const int NET_PLAYM4_VERSION_INCORRECT = 518;//The version of decoder and encoder is not adapted
        public const int NET_PALYM4_INIT_DECODER_ERROR = 519;//Initialize decoder failed
        public const int NET_PLAYM4_CHECK_FILE_ERROR = 520;//The file data is unknown
        public const int NET_PLAYM4_INIT_TIMER_ERROR = 521;//Initialize multimedia clock failed
        public const int NET_PLAYM4_BLT_ERROR = 522;//Blt failed
        public const int NET_PLAYM4_UPDATE_ERROR = 523;//Update failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR_MULTI = 524;//openfile error, streamtype is multi
        public const int NET_PLAYM4_OPEN_FILE_ERROR_VIDEO = 525;//openfile error, streamtype is video
        public const int NET_PLAYM4_JPEG_COMPRESS_ERROR = 526;//JPEG compress error
        public const int NET_PLAYM4_EXTRACT_NOT_SUPPORT = 527;//Don't support the version of this file
        public const int NET_PLAYM4_EXTRACT_DATA_ERROR = 528;//extract video data failed
        /*******************全局错误码 end**********************/

        /*************************************************
        NET_DVR_IsSupport()返回值
        1－9位分别表示以下信息（位与是TRUE)表示支持；
        **************************************************/
        public const int NET_DVR_SUPPORT_DDRAW = 1;//支持DIRECTDRAW，如果不支持，则播放器不能工作
        public const int NET_DVR_SUPPORT_BLT = 2;//显卡支持BLT操作，如果不支持，则播放器不能工作
        public const int NET_DVR_SUPPORT_BLTFOURCC = 4;//显卡BLT支持颜色转换，如果不支持，播放器会用软件方法作RGB转换
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 8;//显卡BLT支持X轴缩小；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 16;//显卡BLT支持Y轴缩小；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 32;//显卡BLT支持X轴放大；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 64;//显卡BLT支持Y轴放大；如果不支持，系统会用软件方法转换
        public const int NET_DVR_SUPPORT_SSE = 128;//CPU支持SSE指令，Intel Pentium3以上支持SSE指令
        public const int NET_DVR_SUPPORT_MMX = 256;//CPU支持MMX指令集，Intel Pentium3以上支持SSE指令

        /**********************云台控制命令 begin*************************/
        public const int LIGHT_PWRON = 2;// 接通灯光电源
        public const int WIPER_PWRON = 3;// 接通雨刷开关 
        public const int FAN_PWRON = 4;// 接通风扇开关
        public const int HEATER_PWRON = 5;// 接通加热器开关
        public const int AUX_PWRON1 = 6;// 接通辅助设备开关
        public const int AUX_PWRON2 = 7;// 接通辅助设备开关 
        public const int SET_PRESET = 8;// 设置预置点 
        public const int CLE_PRESET = 9;// 清除预置点 

        public const int ZOOM_IN = 11;// 焦距以速度SS变大(倍率变大)
        public const int ZOOM_OUT = 12;// 焦距以速度SS变小(倍率变小)
        public const int FOCUS_NEAR = 13;// 焦点以速度SS前调 
        public const int FOCUS_FAR = 14;// 焦点以速度SS后调
        public const int IRIS_OPEN = 15;// 光圈以速度SS扩大
        public const int IRIS_CLOSE = 16;// 光圈以速度SS缩小 

        public const int TILT_UP = 21;/* 云台以SS的速度上仰 */
        public const int TILT_DOWN = 22;/* 云台以SS的速度下俯 */
        public const int PAN_LEFT = 23;/* 云台以SS的速度左转 */
        public const int PAN_RIGHT = 24;/* 云台以SS的速度右转 */
        public const int UP_LEFT = 25;/* 云台以SS的速度上仰和左转 */
        public const int UP_RIGHT = 26;/* 云台以SS的速度上仰和右转 */
        public const int DOWN_LEFT = 27;/* 云台以SS的速度下俯和左转 */
        public const int DOWN_RIGHT = 28;/* 云台以SS的速度下俯和右转 */
        public const int PAN_AUTO = 29;/* 云台以SS的速度左右自动扫描 */

        public const int FILL_PRE_SEQ = 30;/* 将预置点加入巡航序列 */
        public const int SET_SEQ_DWELL = 31;/* 设置巡航点停顿时间 */
        public const int SET_SEQ_SPEED = 32;/* 设置巡航速度 */
        public const int CLE_PRE_SEQ = 33;/* 将预置点从巡航序列中删除 */
        public const int STA_MEM_CRUISE = 34;/* 开始记录轨迹 */
        public const int STO_MEM_CRUISE = 35;/* 停止记录轨迹 */
        public const int RUN_CRUISE = 36;/* 开始轨迹 */
        public const int RUN_SEQ = 37;/* 开始巡航 */
        public const int STOP_SEQ = 38;/* 停止巡航 */
        public const int GOTO_PRESET = 39;/* 快球转到预置点 */
        public const int DEL_SEQ = 43; /* 删除巡航路径 */
        /**********************云台控制命令 end*************************/
        /*************************************************
        回放时播放控制命令宏定义 
        NET_DVR_PlayBackControl
        NET_DVR_PlayControlLocDisplay
        NET_DVR_DecPlayBackCtrl的宏定义
        具体支持查看函数说明和代码
        **************************************************/
        public const int NET_DVR_PLAYSTART = 1;//开始播放
        public const int NET_DVR_PLAYSTOP = 2;//停止播放
        public const int NET_DVR_PLAYPAUSE = 3;//暂停播放
        public const int NET_DVR_PLAYRESTART = 4;//恢复播放
        public const int NET_DVR_PLAYFAST = 5;//快放
        public const int NET_DVR_PLAYSLOW = 6;//慢放
        public const int NET_DVR_PLAYNORMAL = 7;//正常速度
        public const int NET_DVR_PLAYFRAME = 8;//单帧放
        public const int NET_DVR_PLAYSTARTAUDIO = 9;//打开声音
        public const int NET_DVR_PLAYSTOPAUDIO = 10;//关闭声音
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;//调节音量
        public const int NET_DVR_PLAYSETPOS = 12;//改变文件回放的进度
        public const int NET_DVR_PLAYGETPOS = 13;//获取文件回放的进度
        public const int NET_DVR_PLAYGETTIME = 14;//获取当前已经播放的时间(按文件回放的时候有效)
        public const int NET_DVR_PLAYGETFRAME = 15;//获取当前已经播放的帧数(按文件回放的时候有效)
        public const int NET_DVR_GETTOTALFRAMES = 16;//获取当前播放文件总的帧数(按文件回放的时候有效)
        public const int NET_DVR_GETTOTALTIME = 17;//获取当前播放文件总的时间(按文件回放的时候有效)
        public const int NET_DVR_THROWBFRAME = 20;//丢B帧
        public const int NET_DVR_SETSPEED = 24;//设置码流速度
        public const int NET_DVR_KEEPALIVE = 25;//保持与设备的心跳(如果回调阻塞，建议2秒发送一次)
        public const int NET_DVR_PLAYSETTIME = 26;//按绝对时间定位
        public const int NET_DVR_PLAYGETTOTALLEN = 27;//获取按时间回放对应时间段内的所有文件的总长度
        public const int NET_DVR_PLAY_FORWARD = 29; //倒放切换为正放
        public const int NET_DVR_PLAY_REVERSE = 30; //正放切换为倒放
        public const int NET_DVR_SET_DECODEFFRAMETYPE = 31;
        public const int NET_DVR_SET_TRANS_TYPE = 32; //设置转码格式
        public const int NET_DVR_PLAY_CONVERT = 33; //回放转码
        public const int NET_DVR_START_DRAWFRAME = 34; //开始抽帧回放
        public const int NET_DVR_STOP_DRAWFRAME = 35; //停止抽帧回放

        //远程按键定义如下：
        /* key value send to CONFIG program */
        public const int KEY_CODE_1 = 1;
        public const int KEY_CODE_2 = 2;
        public const int KEY_CODE_3 = 3;
        public const int KEY_CODE_4 = 4;
        public const int KEY_CODE_5 = 5;
        public const int KEY_CODE_6 = 6;
        public const int KEY_CODE_7 = 7;
        public const int KEY_CODE_8 = 8;
        public const int KEY_CODE_9 = 9;
        public const int KEY_CODE_0 = 10;
        public const int KEY_CODE_POWER = 11;
        public const int KEY_CODE_MENU = 12;
        public const int KEY_CODE_ENTER = 13;
        public const int KEY_CODE_CANCEL = 14;
        public const int KEY_CODE_UP = 15;
        public const int KEY_CODE_DOWN = 16;
        public const int KEY_CODE_LEFT = 17;
        public const int KEY_CODE_RIGHT = 18;
        public const int KEY_CODE_EDIT = 19;
        public const int KEY_CODE_ADD = 20;
        public const int KEY_CODE_MINUS = 21;
        public const int KEY_CODE_PLAY = 22;
        public const int KEY_CODE_REC = 23;
        public const int KEY_CODE_PAN = 24;
        public const int KEY_CODE_M = 25;
        public const int KEY_CODE_A = 26;
        public const int KEY_CODE_F1 = 27;
        public const int KEY_CODE_F2 = 28;

        /* for PTZ control */
        public const int KEY_PTZ_UP_START = KEY_CODE_UP;
        public const int KEY_PTZ_UP_STOP = 32;

        public const int KEY_PTZ_DOWN_START = KEY_CODE_DOWN;
        public const int KEY_PTZ_DOWN_STOP = 33;


        public const int KEY_PTZ_LEFT_START = KEY_CODE_LEFT;
        public const int KEY_PTZ_LEFT_STOP = 34;

        public const int KEY_PTZ_RIGHT_START = KEY_CODE_RIGHT;
        public const int KEY_PTZ_RIGHT_STOP = 35;

        public const int KEY_PTZ_AP1_START = KEY_CODE_EDIT;/* 光圈+ */
        public const int KEY_PTZ_AP1_STOP = 36;

        public const int KEY_PTZ_AP2_START = KEY_CODE_PAN;/* 光圈- */
        public const int KEY_PTZ_AP2_STOP = 37;

        public const int KEY_PTZ_FOCUS1_START = KEY_CODE_A;/* 聚焦+ */
        public const int KEY_PTZ_FOCUS1_STOP = 38;

        public const int KEY_PTZ_FOCUS2_START = KEY_CODE_M;/* 聚焦- */
        public const int KEY_PTZ_FOCUS2_STOP = 39;

        public const int KEY_PTZ_B1_START = 40;/* 变倍+ */
        public const int KEY_PTZ_B1_STOP = 41;

        public const int KEY_PTZ_B2_START = 42;/* 变倍- */
        public const int KEY_PTZ_B2_STOP = 43;

        //9000新增
        public const int KEY_CODE_11 = 44;
        public const int KEY_CODE_12 = 45;
        public const int KEY_CODE_13 = 46;
        public const int KEY_CODE_14 = 47;
        public const int KEY_CODE_15 = 48;
        public const int KEY_CODE_16 = 49;

        /*************************参数配置命令 begin*******************************/
        //用于NET_DVR_SetDVRConfig和NET_DVR_GetDVRConfig,注意其对应的配置结构
        public const int NET_DVR_GET_DEVICECFG = 100;//获取设备参数
        public const int NET_DVR_SET_DEVICECFG = 101;//设置设备参数
        public const int NET_DVR_GET_NETCFG = 102;//获取网络参数
        public const int NET_DVR_SET_NETCFG = 103;//设置网络参数
        public const int NET_DVR_GET_PICCFG = 104;//获取图象参数
        public const int NET_DVR_SET_PICCFG = 105;//设置图象参数
        public const int NET_DVR_GET_COMPRESSCFG = 106;//获取压缩参数
        public const int NET_DVR_SET_COMPRESSCFG = 107;//设置压缩参数
        public const int NET_DVR_GET_RECORDCFG = 108;//获取录像时间参数
        public const int NET_DVR_SET_RECORDCFG = 109;//设置录像时间参数
        public const int NET_DVR_GET_DECODERCFG = 110;//获取解码器参数
        public const int NET_DVR_SET_DECODERCFG = 111;//设置解码器参数
        public const int NET_DVR_GET_RS232CFG = 112;//获取232串口参数
        public const int NET_DVR_SET_RS232CFG = 113;//设置232串口参数
        public const int NET_DVR_GET_ALARMINCFG = 114;//获取报警输入参数
        public const int NET_DVR_SET_ALARMINCFG = 115;//设置报警输入参数
        public const int NET_DVR_GET_ALARMOUTCFG = 116;//获取报警输出参数
        public const int NET_DVR_SET_ALARMOUTCFG = 117;//设置报警输出参数
        public const int NET_DVR_GET_TIMECFG = 118;//获取DVR时间
        public const int NET_DVR_SET_TIMECFG = 119;//设置DVR时间
        public const int NET_DVR_GET_PREVIEWCFG = 120;//获取预览参数
        public const int NET_DVR_SET_PREVIEWCFG = 121;//设置预览参数
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;//获取视频输出参数
        public const int NET_DVR_SET_VIDEOOUTCFG = 123;//设置视频输出参数
        public const int NET_DVR_GET_USERCFG = 124;//获取用户参数
        public const int NET_DVR_SET_USERCFG = 125;//设置用户参数
        public const int NET_DVR_GET_EXCEPTIONCFG = 126;//获取异常参数
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;//设置异常参数
        public const int NET_DVR_GET_ZONEANDDST = 128;//获取时区和夏时制参数
        public const int NET_DVR_SET_ZONEANDDST = 129;//设置时区和夏时制参数
        public const int NET_DVR_GET_SHOWSTRING = 130;//获取叠加字符参数
        public const int NET_DVR_SET_SHOWSTRING = 131;//设置叠加字符参数
        public const int NET_DVR_GET_EVENTCOMPCFG = 132;//获取事件触发录像参数
        public const int NET_DVR_SET_EVENTCOMPCFG = 133;//设置事件触发录像参数

        public const int NET_DVR_GET_AUXOUTCFG = 140;//获取报警触发辅助输出设置(HS设备辅助输出2006-02-28)
        public const int NET_DVR_SET_AUXOUTCFG = 141;//设置报警触发辅助输出设置(HS设备辅助输出2006-02-28)
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142;//获取-s系列双输出预览参数(-s系列双输出2006-04-13)
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143;//设置-s系列双输出预览参数(-s系列双输出2006-04-13)

        public const int NET_DVR_GET_PICCFG_EX = 200;//获取图象参数(SDK_V14扩展命令)
        public const int NET_DVR_SET_PICCFG_EX = 201;//设置图象参数(SDK_V14扩展命令)
        public const int NET_DVR_GET_USERCFG_EX = 202;//获取用户参数(SDK_V15扩展命令)
        public const int NET_DVR_SET_USERCFG_EX = 203;//设置用户参数(SDK_V15扩展命令)
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204;//获取压缩参数(SDK_V15扩展命令2006-05-15)
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205;//设置压缩参数(SDK_V15扩展命令2006-05-15)

        public const int NET_DVR_GET_NETAPPCFG = 222;//获取网络应用参数 NTP/DDNS/EMAIL
        public const int NET_DVR_SET_NETAPPCFG = 223;//设置网络应用参数 NTP/DDNS/EMAIL
        public const int NET_DVR_GET_NTPCFG = 224;//获取网络应用参数 NTP
        public const int NET_DVR_SET_NTPCFG = 225;//设置网络应用参数 NTP
        public const int NET_DVR_GET_DDNSCFG = 226;//获取网络应用参数 DDNS
        public const int NET_DVR_SET_DDNSCFG = 227;//设置网络应用参数 DDNS
        //对应NET_DVR_EMAILPARA
        public const int NET_DVR_GET_EMAILCFG = 228;//获取网络应用参数 EMAIL
        public const int NET_DVR_SET_EMAILCFG = 229;//设置网络应用参数 EMAIL

        public const int NET_DVR_GET_NFSCFG = 230;/* NFS disk config */
        public const int NET_DVR_SET_NFSCFG = 231;/* NFS disk config */

        public const int NET_DVR_GET_SHOWSTRING_EX = 238;//获取叠加字符参数扩展(支持8条字符)
        public const int NET_DVR_SET_SHOWSTRING_EX = 239;//设置叠加字符参数扩展(支持8条字符)
        public const int NET_DVR_GET_NETCFG_OTHER = 244;//获取网络参数
        public const int NET_DVR_SET_NETCFG_OTHER = 245;//设置网络参数

        //对应NET_DVR_EMAILCFG结构
        public const int NET_DVR_GET_EMAILPARACFG = 250;//Get EMAIL parameters
        public const int NET_DVR_SET_EMAILPARACFG = 251;//Setup EMAIL parameters

        public const int NET_DVR_GET_DDNSCFG_EX = 274;//获取扩展DDNS参数
        public const int NET_DVR_SET_DDNSCFG_EX = 275;//设置扩展DDNS参数

        public const int NET_DVR_SET_PTZPOS = 292;//云台设置PTZ位置
        public const int NET_DVR_GET_PTZPOS = 293;//云台获取PTZ位置
        public const int NET_DVR_GET_PTZSCOPE = 294;//云台获取PTZ范围

        /***************************DS9000新增命令(_V30) begin *****************************/
        //网络(NET_DVR_NETCFG_V30结构)
        public const int NET_DVR_GET_NETCFG_V30 = 1000;//获取网络参数
        public const int NET_DVR_SET_NETCFG_V30 = 1001;//设置网络参数

        //图象(NET_DVR_PICCFG_V30结构)
        public const int NET_DVR_GET_PICCFG_V30 = 1002;//获取图象参数
        public const int NET_DVR_SET_PICCFG_V30 = 1003;//设置图象参数

        //录像时间(NET_DVR_RECORD_V30结构)
        public const int NET_DVR_GET_RECORDCFG_V30 = 1004;//获取录像参数
        public const int NET_DVR_SET_RECORDCFG_V30 = 1005;//设置录像参数

        //用户(NET_DVR_USER_V30结构)
        public const int NET_DVR_GET_USERCFG_V30 = 1006;//获取用户参数
        public const int NET_DVR_SET_USERCFG_V30 = 1007;//设置用户参数

        //9000DDNS参数配置(NET_DVR_DDNSPARA_V30结构)
        public const int NET_DVR_GET_DDNSCFG_V30 = 1010;//获取DDNS(9000扩展)
        public const int NET_DVR_SET_DDNSCFG_V30 = 1011;//设置DDNS(9000扩展)

        //EMAIL功能(NET_DVR_EMAILCFG_V30结构)
        public const int NET_DVR_GET_EMAILCFG_V30 = 1012;//获取EMAIL参数 
        public const int NET_DVR_SET_EMAILCFG_V30 = 1013;//设置EMAIL参数 

        //巡航参数 (NET_DVR_CRUISE_PARA结构)
        public const int NET_DVR_GET_CRUISE = 1020;
        public const int NET_DVR_SET_CRUISE = 1021;

        //报警输入结构参数 (NET_DVR_ALARMINCFG_V30结构)
        public const int NET_DVR_GET_ALARMINCFG_V30 = 1024;
        public const int NET_DVR_SET_ALARMINCFG_V30 = 1025;

        //报警输出结构参数 (NET_DVR_ALARMOUTCFG_V30结构)
        public const int NET_DVR_GET_ALARMOUTCFG_V30 = 1026;
        public const int NET_DVR_SET_ALARMOUTCFG_V30 = 1027;

        //视频输出结构参数 (NET_DVR_VIDEOOUT_V30结构)
        public const int NET_DVR_GET_VIDEOOUTCFG_V30 = 1028;
        public const int NET_DVR_SET_VIDEOOUTCFG_V30 = 1029;

        //叠加字符结构参数 (NET_DVR_SHOWSTRING_V30结构)
        public const int NET_DVR_GET_SHOWSTRING_V30 = 1030;
        public const int NET_DVR_SET_SHOWSTRING_V30 = 1031;

        //异常结构参数 (NET_DVR_EXCEPTION_V30结构)
        public const int NET_DVR_GET_EXCEPTIONCFG_V30 = 1034;
        public const int NET_DVR_SET_EXCEPTIONCFG_V30 = 1035;

        //串口232结构参数 (NET_DVR_RS232CFG_V30结构)
        public const int NET_DVR_GET_RS232CFG_V30 = 1036;
        public const int NET_DVR_SET_RS232CFG_V30 = 1037;

        //网络硬盘接入结构参数 (NET_DVR_NET_DISKCFG结构)
        public const int NET_DVR_GET_NET_DISKCFG = 1038;//网络硬盘接入获取
        public const int NET_DVR_SET_NET_DISKCFG = 1039;//网络硬盘接入设置

        //压缩参数 (NET_DVR_COMPRESSIONCFG_V30结构)
        public const int NET_DVR_GET_COMPRESSCFG_V30 = 1040;
        public const int NET_DVR_SET_COMPRESSCFG_V30 = 1041;

        //获取485解码器参数 (NET_DVR_DECODERCFG_V30结构)
        public const int NET_DVR_GET_DECODERCFG_V30 = 1042;//获取解码器参数
        public const int NET_DVR_SET_DECODERCFG_V30 = 1043;//设置解码器参数

        //获取预览参数 (NET_DVR_PREVIEWCFG_V30结构)
        public const int NET_DVR_GET_PREVIEWCFG_V30 = 1044;//获取预览参数
        public const int NET_DVR_SET_PREVIEWCFG_V30 = 1045;//设置预览参数

        //辅助预览参数 (NET_DVR_PREVIEWCFG_AUX_V30结构)
        public const int NET_DVR_GET_PREVIEWCFG_AUX_V30 = 1046;//获取辅助预览参数
        public const int NET_DVR_SET_PREVIEWCFG_AUX_V30 = 1047;//设置辅助预览参数

        //IP接入配置参数 （NET_DVR_IPPARACFG结构）
        public const int NET_DVR_GET_IPPARACFG = 1048; //获取IP接入配置信息 
        public const int NET_DVR_SET_IPPARACFG = 1049;//设置IP接入配置信息

        //IP报警输入接入配置参数 （NET_DVR_IPALARMINCFG结构）
        public const int NET_DVR_GET_IPALARMINCFG = 1050; //获取IP报警输入接入配置信息 
        public const int NET_DVR_SET_IPALARMINCFG = 1051; //设置IP报警输入接入配置信息

        //IP报警输出接入配置参数 （NET_DVR_IPALARMOUTCFG结构）
        public const int NET_DVR_GET_IPALARMOUTCFG = 1052;//获取IP报警输出接入配置信息 
        public const int NET_DVR_SET_IPALARMOUTCFG = 1053;//设置IP报警输出接入配置信息

        //硬盘管理的参数获取 (NET_DVR_HDCFG结构)
        public const int NET_DVR_GET_HDCFG = 1054;//获取硬盘管理配置参数
        public const int NET_DVR_SET_HDCFG = 1055;//设置硬盘管理配置参数

        //盘组管理的参数获取 (NET_DVR_HDGROUP_CFG结构)
        public const int NET_DVR_GET_HDGROUP_CFG = 1056;//获取盘组管理配置参数
        public const int NET_DVR_SET_HDGROUP_CFG = 1057;//设置盘组管理配置参数

        //设备编码类型配置(NET_DVR_COMPRESSION_AUDIO结构)
        public const int NET_DVR_GET_COMPRESSCFG_AUD = 1058;//获取设备语音对讲编码参数
        public const int NET_DVR_SET_COMPRESSCFG_AUD = 1059;//设置设备语音对讲编码参数

        //IP接入配置参数 （NET_DVR_IPPARACFG_V31结构）
        public const int NET_DVR_GET_IPPARACFG_V31 = 1060;//获取IP接入配置信息 
        public const int NET_DVR_SET_IPPARACFG_V31 = 1061; //设置IP接入配置信息

        //远程控制命令
        public const int NET_DVR_BARRIERGATE_CTRL = 3128; //道闸控制

        //进入区域单个区域配置
        public const int NET_DVR_GET_REGION_ENTR_REGION = 3505;    //获取进入区域的单个区域配置
        public const int NET_DVR_SET_REGION_ENTR_REGION = 3506;   //设置进入区域的单个区域配置

        //离开区域单个区域配置
        public const int NET_DVR_GET_REGION_EXITING_REGION = 3514; //获取离开区域的单个区域配置
        public const int NET_DVR_SET_REGION_EXITING_REGION = 3515; //设置离开区域的单个区域配置

        //越界侦测配置
        public const int NET_DVR_GET_TRAVERSE_PLANE_DETECTION = 3360 ; //获取越界侦测配置
        public const int NET_DVR_SET_TRAVERSE_PLANE_DETECTION = 3361; //设置越界侦测配置

        //区域侦测配置
        public const int NET_DVR_GET_FIELD_DETECTION = 3362; //获取区域侦测配置
        public const int NET_DVR_SET_FIELD_DETECTION = 3363; //设置区域侦测配置
        /***************************DS9000新增命令(_V30) end *****************************/

        /*************************参数配置命令 end*******************************/

        /*******************查找文件和日志函数返回值*************************/
        public const int NET_DVR_FILE_SUCCESS = 1000;//获得文件信息
        public const int NET_DVR_FILE_NOFIND = 1001;//没有文件
        public const int NET_DVR_ISFINDING = 1002;//正在查找文件
        public const int NET_DVR_NOMOREFILE = 1003;//查找文件时没有更多的文件
        public const int NET_DVR_FILE_EXCEPTION = 1004;//查找文件时异常

        /*********************回调函数类型 begin************************/
        public const int COMM_ALARM_RULE_CALC = 0x1110;  //行为统计报警上传(人员密度)
        public const int COMM_ALARM = 0x1100;//8000报警信息主动上传，对应NET_DVR_ALARMINFO
        public const int COMM_ALARM_RULE = 0x1102;//行为分析报警信息，对应NET_VCA_RULE_ALARM
        public const int COMM_ALARM_PDC = 0x1103;//人流量统计报警上传，对应NET_DVR_PDC_ALRAM_INFO
        public const int COMM_ALARM_ALARMHOST = 0x1105;//网络报警主机报警上传，对应NET_DVR_ALARMHOST_ALARMINFO
        public const int COMM_ALARM_FACE = 0x1106;//人脸检测识别报警信息，对应NET_DVR_FACEDETECT_ALARM
        public const int COMM_RULE_INFO_UPLOAD = 0x1107;  // 事件数据信息上传
        public const int COMM_ALARM_AID = 0x1110;  //交通事件报警信息
        public const int COMM_ALARM_TPS = 0x1111;  //交通参数统计报警信息
        public const int COMM_UPLOAD_FACESNAP_RESULT = 0x1112;  //人脸识别结果上传
        public const int COMM_ALARM_FACE_DETECTION = 0x4010; //人脸侦测报警信息
        public const int COMM_ALARM_TFS = 0x1113;  //交通取证报警信息
        public const int COMM_ALARM_TPS_V41 = 0x1114;  //交通参数统计报警信息扩展
        public const int COMM_ALARM_AID_V41 = 0x1115;  //交通事件报警信息扩展
        public const int COMM_ALARM_VQD_EX = 0x1116;     //视频质量诊断报警
        public const int COMM_SENSOR_VALUE_UPLOAD = 0x1120;  //模拟量数据实时上传
        public const int COMM_SENSOR_ALARM = 0x1121;  //模拟量报警上传
        public const int COMM_SWITCH_ALARM = 0x1122;     //开关量报警
        public const int COMM_ALARMHOST_EXCEPTION = 0x1123; //报警主机故障报警
        public const int COMM_ALARMHOST_OPERATEEVENT_ALARM = 0x1124;  //操作事件报警上传
        public const int COMM_ALARMHOST_SAFETYCABINSTATE = 0x1125;     //防护舱状态
        public const int COMM_ALARMHOST_ALARMOUTSTATUS = 0x1126;     //报警输出口/警号状态
        public const int COMM_ALARMHOST_CID_ALARM = 0x1127;     //CID报告报警上传
        public const int COMM_ALARMHOST_EXTERNAL_DEVICE_ALARM = 0x1128;     //报警主机外接设备报警上传
        public const int COMM_ALARMHOST_DATA_UPLOAD = 0x1129;     //报警数据上传
        public const int COMM_ALARM_AUDIOEXCEPTION = 0x1150;     //声音报警信息
        public const int COMM_ALARM_DEFOCUS = 0x1151;     //虚焦报警信息
        public const int COMM_ALARM_BUTTON_DOWN_EXCEPTION = 0x1152;     //按钮按下报警信息
        public const int COMM_ALARM_ALARMGPS = 0x1202; //GPS报警信息上传
        public const int COMM_TRADEINFO = 0x1500;  //ATMDVR主动上传交易信息
        public const int COMM_UPLOAD_PLATE_RESULT = 0x2800;     //上传车牌信息
        public const int COMM_ITC_STATUS_DETECT_RESULT = 0x2810;  //实时状态检测结果上传(智能高清IPC)
        public const int COMM_IPC_AUXALARM_RESULT = 0x2820;  //PIR报警、无线报警、呼救报警上传
        public const int COMM_UPLOAD_PICTUREINFO = 0x2900;     //上传图片信息
        public const int COMM_SNAP_MATCH_ALARM = 0x2902;  
        public const int COMM_ITS_PLATE_RESULT = 0x3050;  //终端图片上传
        public const int COMM_ITS_TRAFFIC_COLLECT = 0x3051;  //终端统计数据上传
        public const int COMM_ITS_GATE_VEHICLE = 0x3052;  //出入口车辆抓拍数据上传
        public const int COMM_ITS_GATE_FACE = 0x3053; //出入口人脸抓拍数据上传
        public const int COMM_ITS_GATE_COSTITEM = 0x3054;  //出入口过车收费明细 2013-11-19
        public const int COMM_ITS_GATE_HANDOVER = 0x3055; //出入口交接班数据 2013-11-19
        public const int COMM_ITS_PARK_VEHICLE = 0x3056;  //停车场数据上传
        public const int COMM_ITS_BLOCKLIST_ALARM = 0x3057;  
        public const int COMM_ALARM_V30 = 0x4000;     //9000报警信息主动上传
        public const int COMM_IPCCFG = 0x4001;     //9000设备IPC接入配置改变报警信息主动上传
        public const int COMM_IPCCFG_V31 = 0x4002;     //9000设备IPC接入配置改变报警信息主动上传扩展 9000_1.1
        public const int COMM_IPCCFG_V40 = 0x4003; // IVMS 2000 编码服务器 NVR IPC接入配置改变时报警信息上传
        public const int COMM_ALARM_DEVICE = 0x4004;  //设备报警内容，由于通道值大于256而扩展
        public const int COMM_ALARM_CVR = 0x4005;  //CVR 2.0.X外部报警类型
        public const int COMM_ALARM_HOT_SPARE = 0x4006;  //热备异常报警（N+1模式异常报警）
        public const int COMM_ALARM_V40 = 0x4007;    //移动侦测，视频丢失，遮挡，IO信号量等报警信息主动上传，报警数据为可变长

        public const int COMM_ITS_ROAD_EXCEPTION = 0x4500;     //路口设备异常报警
        public const int COMM_ITS_EXTERNAL_CONTROL_ALARM = 0x4520;  //外控报警
        public const int COMM_SCREEN_ALARM = 0x5000;  //多屏控制器报警类型
        public const int COMM_DVCS_STATE_ALARM = 0x5001;  //分布式大屏控制器报警上传
        public const int COMM_ALARM_VQD = 0x6000;  //VQD主动报警上传 
        public const int COMM_PUSH_UPDATE_RECORD_INFO = 0x6001;  //推模式录像信息上传
        public const int COMM_DIAGNOSIS_UPLOAD = 0x5100;  //诊断服务器VQD报警上传
        //public const int COMM_ALARM_ACS = 0x5002;  //门禁主机报警
        public const int COMM_ID_INFO_ALARM = 0x5200;  //身份证信息上传
        public const int COMM_PASSNUM_INFO_ALARM = 0x5201;  //通行人数上报

        public const int COMM_UPLOAD_AIOP_VIDEO =  0x4021; //设备支持AI开放平台接入，上传视频检测数据 对应的结构体(NET_AIOP_VIDEO_HEAD)
        public const int COMM_UPLOAD_AIOP_PICTURE = 0x4022; //设备支持AI开放平台接入，上传图片检测数据 对应的结构体(NET_AIOP_PICTURE_HEAD)
        public const int COMM_UPLOAD_AIOP_POLLING_SNAP = 0x4023; //设备支持AI开放平台接入，上传轮询抓图图片检测数据 对应的结构体(NET_AIOP_POLLING_PICTURE_HEAD)
        public const int COMM_UPLOAD_AIOP_POLLING_VIDEO = 0x4024; //设备支持AI开放平台接入，上传轮询视频检测数据 对应的结构体(NET_AIOP_POLLING_VIDEO_HEAD)


        /*************操作异常类型(消息方式, 回调方式(保留))****************/
        public const int EXCEPTION_EXCHANGE = 32768;//用户交互时异常
        public const int EXCEPTION_AUDIOEXCHANGE = 32769;//语音对讲异常
        public const int EXCEPTION_ALARM = 32770;//报警异常
        public const int EXCEPTION_PREVIEW = 32771;//网络预览异常
        public const int EXCEPTION_SERIAL = 32772;//透明通道异常
        public const int EXCEPTION_RECONNECT = 32773;//预览时重连
        public const int EXCEPTION_ALARMRECONNECT = 32774;//报警时重连
        public const int EXCEPTION_SERIALRECONNECT = 32775;//透明通道重连
        public const int EXCEPTION_PLAYBACK = 32784;//回放异常
        public const int EXCEPTION_DISKFMT = 32785;//硬盘格式化

        /********************预览回调函数*********************/
        public const int NET_DVR_SYSHEAD = 1;//系统头数据
        public const int NET_DVR_STREAMDATA = 2;//视频流数据（包括复合流和音视频分开的视频流数据）
        public const int NET_DVR_AUDIOSTREAMDATA = 3;//音频流数据
        public const int NET_DVR_STD_VIDEODATA = 4;//标准视频流数据
        public const int NET_DVR_STD_AUDIODATA = 5;//标准音频流数据

        //回调预览中的状态和消息
        public const int NET_DVR_REALPLAYEXCEPTION = 111;//预览异常
        public const int NET_DVR_REALPLAYNETCLOSE = 112;//预览时连接断开
        public const int NET_DVR_REALPLAY5SNODATA = 113;//预览5s没有收到数据
        public const int NET_DVR_REALPLAYRECONNECT = 114;//预览重连

        /********************回放回调函数*********************/
        public const int NET_DVR_PLAYBACKOVER = 101;//回放数据播放完毕
        public const int NET_DVR_PLAYBACKEXCEPTION = 102;//回放异常
        public const int NET_DVR_PLAYBACKNETCLOSE = 103;//回放时候连接断开
        public const int NET_DVR_PLAYBACK5SNODATA = 104;//回放5s没有收到数据

        /*********************回调函数类型 end************************/
        //设备型号(DVR类型)
        /* 设备类型 */
        public const int DVR = 1;/*对尚未定义的dvr类型返回NETRET_DVR*/
        public const int ATMDVR = 2;/*atm dvr*/
        public const int DVS = 3;/*DVS*/
        public const int DEC = 4;/* 6001D */
        public const int ENC_DEC = 5;/* 6001F */
        public const int DVR_HC = 6;/*8000HC*/
        public const int DVR_HT = 7;/*8000HT*/
        public const int DVR_HF = 8;/*8000HF*/
        public const int DVR_HS = 9;/* 8000HS DVR(no audio) */
        public const int DVR_HTS = 10; /* 8016HTS DVR(no audio) */
        public const int DVR_HB = 11; /* HB DVR(SATA HD) */
        public const int DVR_HCS = 12; /* 8000HCS DVR */
        public const int DVS_A = 13; /* 带ATA硬盘的DVS */
        public const int DVR_HC_S = 14; /* 8000HC-S */
        public const int DVR_HT_S = 15;/* 8000HT-S */
        public const int DVR_HF_S = 16;/* 8000HF-S */
        public const int DVR_HS_S = 17; /* 8000HS-S */
        public const int ATMDVR_S = 18;/* ATM-S */
        public const int LOWCOST_DVR = 19;/*7000H系列*/
        public const int DEC_MAT = 20; /*多路解码器*/
        public const int DVR_MOBILE = 21;/* mobile DVR */
        public const int DVR_HD_S = 22;   /* 8000HD-S */
        public const int DVR_HD_SL = 23;/* 8000HD-SL */
        public const int DVR_HC_SL = 24;/* 8000HC-SL */
        public const int DVR_HS_ST = 25;/* 8000HS_ST */
        public const int DVS_HW = 26; /* 6000HW */
        public const int DS630X_D = 27; /* 多路解码器 */
        public const int IPCAM = 30;/*IP 摄像机*/
        public const int MEGA_IPCAM = 31;/*X52MF系列,752MF,852MF*/
        public const int IPCAM_X62MF = 32;/*X62MF系列可接入9000设备,762MF,862MF*/
        public const int IPDOME = 40; /*IP 标清球机*/
        public const int IPDOME_MEGA200 = 41;/*IP 200万高清球机*/
        public const int IPDOME_MEGA130 = 42;/*IP 130万高清球机*/
        public const int IPMOD = 50;/*IP 模块*/
        public const int DS71XX_H = 71;/* DS71XXH_S */
        public const int DS72XX_H_S = 72;/* DS72XXH_S */
        public const int DS73XX_H_S = 73;/* DS73XXH_S */
        public const int DS76XX_H_S = 76;/* DS76XX_H_S */
        public const int DS81XX_HS_S = 81;/* DS81XX_HS_S */
        public const int DS81XX_HL_S = 82;/* DS81XX_HL_S */
        public const int DS81XX_HC_S = 83;/* DS81XX_HC_S */
        public const int DS81XX_HD_S = 84;/* DS81XX_HD_S */
        public const int DS81XX_HE_S = 85;/* DS81XX_HE_S */
        public const int DS81XX_HF_S = 86;/* DS81XX_HF_S */
        public const int DS81XX_AH_S = 87;/* DS81XX_AH_S */
        public const int DS81XX_AHF_S = 88;/* DS81XX_AHF_S */
        public const int DS90XX_HF_S = 90;  /*DS90XX_HF_S*/
        public const int DS91XX_HF_S = 91;  /*DS91XX_HF_S*/
        public const int DS91XX_HD_S = 92; /*91XXHD-S(MD)*/
        /**********************设备类型 end***********************/

        /*************************************************
        参数配置结构、参数(其中_V30为9000新增)
        **************************************************/
        //校时结构参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME
        {
            public int dwYear;
            public int dwMonth;
            public int dwDay;
            public int dwHour;
            public int dwMinute;
            public int dwSecond;
        }

        //时间段(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCHEDTIME
        {
            public byte byStartHour;//开始时间
            public byte byStartMin;//开始时间
            public byte byStopHour;//结束时间
            public byte byStopMin;//结束时间
        }

        /*设备报警和异常处理方式*/
        public const int NOACTION = 0;/*无响应*/
        public const int WARNONMONITOR = 1;/*监视器上警告*/
        public const int WARNONAUDIOOUT = 2;/*声音警告*/
        public const int UPTOCENTER = 4;/*上传中心*/
        public const int TRIGGERALARMOUT = 8;/*触发报警输出*/

        //报警和异常处理结构(子结构)(多处使用)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V30
        {
            public uint dwHandleType;/*处理方式,处理方式的"或"结果*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//报警触发的输出通道,报警触发的输出,为1表示触发该输出
        }

        //报警和异常处理结构(子结构)(多处使用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION
        {
            public uint dwHandleType;/*处理方式,处理方式的"或"结果*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//报警触发的输出通道,报警触发的输出,为1表示触发该输出
        }

        //DVR设备参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;//DVR名称
            public uint dwDVRID;//DVR ID,用于遥控器 //V1.4(0-99), V1.5(0-255)
            public uint dwRecycleRecord;//是否循环录像,0:不是; 1:是
            //以下不可更改
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public uint dwSoftwareVersion;//软件版本号,高16位是主版本,低16位是次版本
            public uint dwSoftwareBuildDate;//软件生成日期,0xYYYYMMDD
            public uint dwDSPSoftwareVersion;//DSP软件版本,高16位是主版本,低16位是次版本
            public uint dwDSPSoftwareBuildDate;// DSP软件生成日期,0xYYYYMMDD
            public uint dwPanelVersion;// 前面板版本,高16位是主版本,低16位是次版本
            public uint dwHardwareVersion;// 硬件版本,高16位是主版本,低16位是次版本
            public byte byAlarmInPortNum;//DVR报警输入个数
            public byte byAlarmOutPortNum;//DVR报警输出个数
            public byte byRS232Num;//DVR 232串口个数
            public byte byRS485Num;//DVR 485串口个数
            public byte byNetworkPortNum;//网络口个数
            public byte byDiskCtrlNum;//DVR 硬盘控制器个数
            public byte byDiskNum;//DVR 硬盘个数
            public byte byDVRType;//DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR 通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
            public byte byDecordChans;//DVR 解码路数
            public byte byVGANum;//VGA口的个数
            public byte byUSBNum;//USB口的个数
            public byte byAuxoutNum;//辅口的个数
            public byte byAudioNum;//语音口的个数
            public byte byIPChanNum;//最大数字通道数
        }

        /*IP地址*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_IPADDR
        {

            /// char[16]
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpV4;

            /// BYTE[128]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[128];
            }
        }

        /*网络数据结构(子结构)(9000扩展)*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ETHERNET_V30
        {
            public NET_DVR_IPADDR struDVRIP;//DVR IP地址
            public NET_DVR_IPADDR struDVRIPMask;//DVR IP地址掩码
            public uint dwNetInterface;//网络接口1-10MBase-T 2-10MBase-T全双工 3-100MBase-TX 4-100M全双工 5-10M/100M自适应
            public ushort wDVRPort;//端口号
            public ushort wMTU;//增加MTU设置，默认1500。
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;// 物理地址
        }

        /*网络数据结构(子结构)*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;//DVR IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIPMask;//DVR IP地址掩码
            public uint dwNetInterface;//网络接口 1-10MBase-T 2-10MBase-T全双工 3-100MBase-TX 4-100M全双工 5-10M/100M自适应
            public ushort wDVRPort;//端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;//服务器的物理地址
        }

        //pppoe结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPOECFG
        {
            public uint dwPPPOE;//0-不启用,1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE密码
            public NET_DVR_IPADDR struPPPoEIP;//PPPoE IP地址
        }

        //网络配置结构(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NETCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET_V30[] struEtherNet;//以太网口
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struRes1;/*保留*/
            public NET_DVR_IPADDR struAlarmHostIpAddr;/* 报警主机IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U2)]
            public ushort[] wRes2;
            public ushort wAlarmHostIpPort;
            public byte byUseDhcp;
            public byte byIPv6Mode;//IPv6分配方式，0-路由公告，1-手动设置，2-启用DHCP分配
            public NET_DVR_IPADDR struDnsServer1IpAddr;/* 域名服务器1的IP地址 */
            public NET_DVR_IPADDR struDnsServer2IpAddr;/* 域名服务器2的IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byIpResolver;
            public ushort wIpResolverPort;
            public ushort wHttpPortNo;
            public NET_DVR_IPADDR struMulticastIpAddr;/* 多播组地址 */
            public NET_DVR_IPADDR struGatewayIpAddr;/* 网关地址 */
            public NET_DVR_PPPOECFG struPPPoE;
            public byte byEnablePrivateMulticastDiscovery;  //私有多播搜索，0~默认，1~启用，2-禁用
            public byte byEnableOnvifMulticastDiscovery;  //Onvif多播搜索，0~默认，1~启用，2-禁用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                struEtherNet = new NET_DVR_ETHERNET_V30[MAX_ETHERNET];
                struAlarmHostIpAddr = new NET_DVR_IPADDR();
                struDnsServer1IpAddr = new NET_DVR_IPADDR();
                struDnsServer2IpAddr = new NET_DVR_IPADDR();
                byIpResolver = new byte[MAX_DOMAIN_NAME];
                struMulticastIpAddr = new NET_DVR_IPADDR();
                struGatewayIpAddr = new NET_DVR_IPADDR();
                struPPPoE = new NET_DVR_PPPOECFG();
            }
        }

        //网络配置结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET[] struEtherNet;/* 以太网口 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sManageHostIP;//远程管理主机地址
            public ushort wManageHostPort;//远程管理主机端口号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIPServerIP;//IPServer服务器地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sMultiCastIP;//多播组地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIP;//网关地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNFSIP;//NFS主机IP地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNFSDirectory;//NFS目录
            public uint dwPPPOE;//0-不启用,1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE密码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sPPPoEIP;//PPPoE IP地址(只读)
            public ushort wHttpPort;//HTTP端口号
        }

        //通道图象结构
        //移动侦测(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MOTION_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96 * 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*侦测区域,0-96位,表示64行,共有96*64个小宏块,为1表示是移动侦测区域,0-表示不是*/
            public byte byMotionSensitive;/*移动侦测灵敏度, 0 - 5,越高越灵敏,oxff关闭*/
            public byte byEnableHandleMotion;/* 是否处理移动侦测 0－否 1－是*/
            public byte byPrecision;/* 移动侦测算法的进度: 0--16*16, 1--32*32, 2--64*64 ... */
            public byte reservedData;
            public NET_DVR_HANDLEEXCEPTION_V30 struMotionHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;/*布防时间*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;/* 报警触发的录象通道*/
        }

        //移动侦测(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MOTION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 396, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*侦测区域,共有22*18个小宏块,为1表示改宏块是移动侦测区域,0-表示不是*/
            public byte byMotionSensitive;/*移动侦测灵敏度, 0 - 5,越高越灵敏,0xff关闭*/
            public byte byEnableHandleMotion;/* 是否处理移动侦测 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string reservedData;
            public NET_DVR_HANDLEEXCEPTION strMotionHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //遮挡报警(子结构)(9000扩展)  区域大小704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM_V30
        {
            public uint dwEnableHideAlarm;/* 是否启动遮挡报警 ,0-否,1-低灵敏度 2-中灵敏度 3-高灵敏度*/
            public ushort wHideAlarmAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAlarmAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAlarmAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAlarmAreaHeight;/*遮挡区域的高*/
            public NET_DVR_HANDLEEXCEPTION_V30 strHideAlarmHandleType;    /* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //遮挡报警(子结构)  区域大小704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM
        {
            public uint dwEnableHideAlarm;/* 是否启动遮挡报警 ,0-否,1-低灵敏度 2-中灵敏度 3-高灵敏度*/
            public ushort wHideAlarmAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAlarmAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAlarmAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAlarmAreaHeight;/*遮挡区域的高*/
            public NET_DVR_HANDLEEXCEPTION strHideAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //信号丢失报警(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST_V30
        {
            public byte byEnableHandleVILost;/* 是否处理信号丢失报警 */
            public NET_DVR_HANDLEEXCEPTION_V30 strVILostHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //信号丢失报警(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST
        {
            public byte byEnableHandleVILost;/* 是否处理信号丢失报警 */
            public NET_DVR_HANDLEEXCEPTION strVILostHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
        }

        //遮挡区域(子结构)
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_SHELTER
        {
            public ushort wHideAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAreaHeight;/*遮挡区域的高*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COLOR
        {
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255*/
            public byte byHue;/*色调,0-255*/
        }

        //通道图象结构(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PICCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            //  [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public string sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public NET_DVR_COLOR struColor;//    图像参数
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string reservedData;/*保留*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //视频信号丢失报警
            public NET_DVR_VILOST_V30 struVILost;
            public NET_DVR_VILOST_V30 struRes;/*保留*/
            //移动侦测
            public NET_DVR_MOTION_V30 struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM_V30 struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte byHourOSDType;/* OSD小时制:0-24小时制,1-12小时制 */
            public byte byFontSize;//字体大小，16*16(中)/8*16(英)，1-32*32(中)/16*32(英)，2-64*64(中)/32*64(英)  3-48*48(中)/24*48(英) 0xff-自适应(adaptive)
            public byte byOSDColorType;    //0-默认（黑白）；1-自定义
            public byte byAlignment;//对齐方式 0-自适应，1-右对齐, 2-左对齐
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 61, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        //通道图象结构SDK_V14扩展
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255 */
            public byte byHue;/*色调,0-255*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //信号丢失报警
            public NET_DVR_VILOST struVILost;
            //移动侦测
            public NET_DVR_MOTION struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte byHourOsdType;/* OSD小时制:0-24小时制,1-12小时制 */
        }

        //通道图象结构(SDK_V13及之前版本)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* 只读 视频制式 1-NTSC 2-PAL*/
            public byte byBrightness;/*亮度,0-255*/
            public byte byContrast;/*对比度,0-255*/
            public byte bySaturation;/*饱和度,0-255 */
            public byte byHue;/*色调,0-255*/
            //显示通道名
            public uint dwShowChanName;// 预览的图象上是否显示通道名称,0-不显示,1-显示 区域大小704*576
            public ushort wShowNameTopLeftX;/* 通道名称显示位置的x坐标 */
            public ushort wShowNameTopLeftY;/* 通道名称显示位置的y坐标 */
            //信号丢失报警
            public NET_DVR_VILOST struVILost;
            //移动侦测
            public NET_DVR_MOTION struMotion;
            //遮挡报警
            public NET_DVR_HIDEALARM struHideAlarm;
            //遮挡  区域大小704*576
            public uint dwEnableHide;/* 是否启动遮挡 ,0-否,1-是*/
            public ushort wHideAreaTopLeftX;/* 遮挡区域的x坐标 */
            public ushort wHideAreaTopLeftY;/* 遮挡区域的y坐标 */
            public ushort wHideAreaWidth;/* 遮挡区域的宽 */
            public ushort wHideAreaHeight;/*遮挡区域的高*/
            //OSD
            public uint dwShowOsd;// 预览的图象上是否显示OSD,0-不显示,1-显示 区域大小704*576
            public ushort wOSDTopLeftX;/* OSD的x坐标 */
            public ushort wOSDTopLeftY;/* OSD的y坐标 */
            public byte byOSDType;/* OSD类型(主要是年月日格式) */
            /* 0: XXXX-XX-XX 年月日 */
            /* 1: XX-XX-XXXX 月日年 */
            /* 2: XXXX年XX月XX日 */
            /* 3: XX月XX日XXXX年 */
            /* 4: XX-XX-XXXX 日月年*/
            /* 5: XX日XX月XXXX年 */
            public byte byDispWeek;/* 是否显示星期 */
            public byte byOSDAttrib;/* OSD属性:透明，闪烁 */
            /* 0: 不显示OSD */
            /* 1: 透明,闪烁 */
            /* 2: 透明,不闪烁 */
            /* 3: 闪烁,不透明 */
            /* 4: 不透明,不闪烁 */
            public byte reservedData2;
        }

        //码流压缩参数(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_V30
        {
            public byte byStreamType;//码流类型 0-视频流, 1-复合流, 表示事件压缩参数时最高位表示是否启用压缩参数
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF 5（保留）16-VGA（640*480） 17-UXGA（1600*1200） 18-SVGA （800*600）19-HD720p（1280*720）20-XVGA  21-HD900p
            public byte byBitrateType;//码率类型 0:变码率, 1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate;//视频码率 0-保留 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20; V2.0版本中新加14-15; 15-18; 16-22;
            public ushort wIntervalFrameI;//I帧间隔
            //2006-08-11 增加单P帧的配置接口，可以改善实时流延时问题
            public byte byIntervalBPFrame;//0-BBP帧; 1-BP帧; 2-单P帧
            public byte byres1; //保留
            public byte byVideoEncType;//视频编码类型 0 hik264;1标准h264; 2标准mpeg4;
            public byte byAudioEncType; //音频编码类型 0－OggVorbis
            public byte byVideoEncComplexity; //视频编码复杂度，0-低，1-中，2高,0xfe:自动，和源一致
            public byte byEnableSvc; //0 - 不启用SVC功能；1- 启用SVC功能; 2-自动启用SVC功能
            public byte byFormatType; //封装类型，1-裸流，2-RTP封装，3-PS封装，4-TS封装，5-私有，6-FLV，7-ASF，8-3GP,9-RTP+PS（国标：GB28181），0xff-无效
            public byte byAudioBitRate; //音频码率 参考 BITRATE_ENCODE_INDEX
            public byte byStreamSmooth;//码流平滑 1～100（1等级表示清晰(Clear)，100表示平滑(Smooth)）
            public byte byAudioSamplingRate;//音频采样率0-默认,1- 16kHZ, 2-32kHZ, 3-48kHZ, 4- 44.1kHZ,5-8kHZ
            public byte bySmartCodec;//高性能编码 0-关闭，1-打开
            public byte byres;
            //平均码率（在SmartCodec使能开启下生效）, 0-0K 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K 24-2560K 25-3072K 26-4096K 27-5120K 28-6144K 29-7168K 30-8192K
            //最高位(15位)置成1表示是自定义码流, 0-14位表示码流值(MIN- 0 K)。
            public ushort wAverageVideoBitrate;
        }

        //通道压缩参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_V30
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_V30 struNormHighRecordPara;//录像 对应8000的普通
            public NET_DVR_COMPRESSION_INFO_V30 struRes;//保留 char reserveData[28];
            public NET_DVR_COMPRESSION_INFO_V30 struEventRecordPara;//事件触发压缩参数
            public NET_DVR_COMPRESSION_INFO_V30 struNetPara;//网传(子码流)
        }

        //码流压缩参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO
        {
            public byte byStreamType;//码流类型0-视频流,1-复合流,表示压缩参数时最高位表示是否启用压缩参数
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(车载专用)
            public byte byBitrateType;//码率类型0:变码率，1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate; //视频码率 0-保留 1-16K(保留) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值(MIN-32K MAX-8192K)。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20;
        }

        //通道压缩参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO struRecordPara;//录像/事件触发录像
            public NET_DVR_COMPRESSION_INFO struNetPara;//网传/保留
        }

        //码流压缩参数(子结构)(扩展) 增加I帧间隔
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_EX
        {
            public byte byStreamType;//码流类型0-视频流, 1-复合流
            public byte byResolution;//分辨率0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(车载专用)
            public byte byBitrateType;//码率类型0:变码率，1:定码率
            public byte byPicQuality;//图象质量 0-最好 1-次好 2-较好 3-一般 4-较差 5-差
            public uint dwVideoBitrate;//视频码率 0-保留 1-16K(保留) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //最高位(31位)置成1表示是自定义码流, 0-30位表示码流值(MIN-32K MAX-8192K)。
            public uint dwVideoFrameRate;//帧率 0-全部; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20, //V2.0增加14-15, 15-18, 16-22;
            public ushort wIntervalFrameI;//I帧间隔
            //2006-08-11 增加单P帧的配置接口，可以改善实时流延时问题
            public byte byIntervalBPFrame;//0-BBP帧; 1-BP帧; 2-单P帧
            public byte byRes;
        }

        //通道压缩参数(扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_EX
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struRecordPara;//录像
            public NET_DVR_COMPRESSION_INFO_EX struNetPara;//网传
        }

        //时间段录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RECORDSCHED
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            public byte byRecordType;//0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警, 5:命令触发, 6: 智能录像
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string reservedData;
        }

        //全天录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY
        {
            public ushort wAllDayRecord;/* 是否全天录像 0-否 1-是*/
            public byte byRecordType;/* 录象类型 0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 6: 智能录像*/
            public byte reservedData;
        }

        //通道录像参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V30
        {
            public uint dwSize;
            public uint dwRecord;/*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* 录象延时长度 0-5秒， 1-20秒， 2-30秒， 3-1分钟， 4-2分钟， 5-5分钟， 6-10分钟*/
            public uint dwPreRecordTime;/* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
            public uint dwRecorderDuration;/* 录像保存的最长时间 */
            public byte byRedundancyRec;/*是否冗余录像,重要数据双备份：0/1*/
            public byte byAudioRec;/*录像时复合流编码时是否记录音频数据：国外有此法规*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;
        }

        //通道录像参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD
        {
            public uint dwSize;
            public uint dwRecord;/*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* 录象时间长度 */
            public uint dwPreRecordTime;/* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
        }

        //云台协议表结构配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZ_PROTOCOL
        {
            public uint dwType;/*解码器类型值，从1开始连续递增*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DESC_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDescribe;/*解码器的描述符，和8000中的一致*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PTZ_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PTZ_PROTOCOL[] struPtz;/*最大200中PTZ协议*/
            public uint dwPtzNum;/*有效的ptz协议数目，从0开始(即计算时加1)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /***************************云台类型(end)******************************/

        //通道解码器(云台)参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG_V30
        {
            public uint dwSize;
            public uint dwBaudRate;//波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;// 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位
            public byte byParity;// 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public ushort wDecoderType;//解码器类型, 从0开始，对应ptz协议列表
            public ushort wDecoderAddress;/*解码器地址:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* 预置点是否设置,0-没有设置,1-设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* 巡航是否设置: 0-没有设置,1-设置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* 轨迹是否设置,0-没有设置,1-设置*/
        }

        //通道解码器(云台)参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG
        {
            public uint dwSize;
            public uint dwBaudRate; //波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit; // 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位;
            public byte byParity; // 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public ushort wDecoderType;//解码器类型, 0－YouLi，1－LiLin-1016，2－LiLin-820，3－Pelco-p，4－DM DynaColor，5－HD600，6－JC-4116，7－Pelco-d WX，8－Pelco-d PICO
            public ushort wDecoderAddress;/*解码器地址:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* 预置点是否设置,0-没有设置,1-设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* 巡航是否设置: 0-没有设置,1-设置 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* 轨迹是否设置,0-没有设置,1-设置*/
        }

        //ppp参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG_V30
        {
            public NET_DVR_IPADDR struRemoteIP;//远端IP地址
            public NET_DVR_IPADDR struLocalIP;//本地IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//本地IP地址掩码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byPPPMode;//PPP模式, 0－主动，1－被动
            public byte byRedial;//是否回拨 ：0-否,1-是
            public byte byRedialMode;//回拨模式,0-由拨入者指定,1-预置回拨号码
            public byte byDataEncrypt;//数据加密,0-否,1-是
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//电话号码
        }

        //ppp参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteIP;//远端IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIP;//本地IP地址
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//本地IP地址掩码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byPPPMode;//PPP模式, 0－主动，1－被动
            public byte byRedial;//是否回拨 ：0-否,1-是
            public byte byRedialMode;//回拨模式,0-由拨入者指定,1-预置回拨号码
            public byte byDataEncrypt;//数据加密,0-否,1-是
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//电话号码
        }

        //RS232串口参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_RS232
        {
            public uint dwBaudRate;/*波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;*/
            public byte byDataBit;/* 数据有几位 0－5位，1－6位，2－7位，3－8位 */
            public byte byStopBit;/* 停止位 0－1位，1－2位 */
            public byte byParity;/* 校验 0－无校验，1－奇校验，2－偶校验 */
            public byte byFlowcontrol;/* 0－无，1－软流控,2-硬流控 */
            public uint dwWorkMode; /* 工作模式，0－232串口用于PPP拨号，1－232串口用于参数控制，2－透明通道 */
        }

        //RS232串口参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG_V30
        {
            public uint dwSize;
            public NET_DVR_SINGLE_RS232 struRs232;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 84, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PPPCFG_V30 struPPPConfig;
        }

        //RS232串口参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG
        {
            public uint dwSize;
            public uint dwBaudRate;//波特率(bps)，0－50，1－75，2－110，3－150，4－300，5－600，6－1200，7－2400，8－4800，9－9600，10－19200， 11－38400，12－57600，13－76800，14－115.2k;
            public byte byDataBit;// 数据有几位 0－5位，1－6位，2－7位，3－8位;
            public byte byStopBit;// 停止位 0－1位，1－2位;
            public byte byParity;// 校验 0－无校验，1－奇校验，2－偶校验;
            public byte byFlowcontrol;// 0－无，1－软流控,2-硬流控
            public uint dwWorkMode;// 工作模式，0－窄带传输(232串口用于PPP拨号)，1－控制台(232串口用于参数控制)，2－透明通道
            public NET_DVR_PPPCFG struPPPConfig;
        }

        //报警输入参数配置(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* 名称 */
            public byte byAlarmType; //报警器类型,0：常开,1：常闭
            public byte byAlarmInHandle; /* 是否处理 0-不处理 1-处理*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_HANDLEEXCEPTION_V30 struAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* 是否调用预置点 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点。*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*保留*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* 是否调用巡航 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* 巡航 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* 是否调用轨迹 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* 调用的云台的轨迹序号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        //报警输入参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* 名称 */
            public byte byAlarmType;//报警器类型,0：常开,1：常闭
            public byte byAlarmInHandle;/* 是否处理 0-不处理 1-处理*/
            public NET_DVR_HANDLEEXCEPTION struAlarmHandleType;/* 处理方式 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* 是否调用预置点 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点。*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* 是否调用巡航 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* 巡航 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* 是否调用轨迹 0-否,1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* 调用的云台的轨迹序号 */
        }

        //上传报警信息(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V30
        {
            public int dwAlarmType;/*0-信号量报警,1-硬盘满,2-信号丢失,3－移动侦测,4－硬盘未格式化,5-读写硬盘出错,6-遮挡报警,7-制式不匹配, 8-非法访问, 0xa-GPS定位信息(车载定制)*/
            public int dwAlarmInputNumber;/*报警输入端口*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutputNumber;/*触发的输出端口，为1表示对应输出*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmRelateChannel;/*触发的录像通道，为1表示对应录像, dwAlarmRelateChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;/*dwAlarmType为2或3,6时，表示哪个通道，dwChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byDiskNumber;/*dwAlarmType为1,4,5时,表示哪个硬盘, dwDiskNumber[0]对应第1个硬盘*/
            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                byAlarmRelateChannel = new byte[MAX_CHANNUM_V30];
                byChannel = new byte[MAX_CHANNUM_V30];
                byAlarmOutputNumber = new byte[MAX_ALARMOUT_V30];
                byDiskNumber = new byte[MAX_DISKNUM_V30];
                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }
            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }
        }

        //信号量报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_TRIGER_ALARM
        {
            //报警固定部分
            public uint dwAlarmType;//报警类型,仅为0-信号量报警
            public NET_DVR_TIME_EX struAlarmTime;    //发生报警的时间
            public uint dwAlarmInputNo;        //发生报警的报警输入通道号，一次只有一个
            public uint dwTrigerAlarmOutNum;    /*触发的报警输出个数，用于后面计算变长数据部分中所有触发的报警输出通道号，四字节表示一个*/
            public uint dwTrigerRecordChanNum;    /*触发的录像通道个数，用于后面计算变长数据部分中所有触发的录像通道号，四字节表示一个*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
            public IntPtr pAlarmData;    //报警可变部分内容
        }

        //移动侦测相关报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_CHAN_ALARM
        {
            //报警固定部分
            public uint dwAlarmType;//报警类型,2-信号丢失，3－移动侦测,6-遮挡报警，9-视频信号异常，10-录像异常，13-前端/录像分辨率不匹配
            public NET_DVR_TIME_EX struAlarmTime;    //发生报警的时间
            public uint dwAlarmChanNum;    /*发生报警通道数据个数，用于后面计算变长数据部分中所有发生的报警通道号，四字节表示一个*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
            public IntPtr pAlarmData;    //报警可变部分内容
        }

        //硬盘相关报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_HARDDISK_ALARM
        {
            //报警固定部分
            public uint dwAlarmType;//报警类型,1-硬盘满，4－硬盘未格式化,5-写硬盘出错,
            public NET_DVR_TIME_EX struAlarmTime;    //发生报警的时间
            public uint dwAlarmHardDiskNum;    /*发生报警的硬盘数据长度，用于后面计算变长数据部分中所有发生报警的硬盘号，四节表示一个*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
            public IntPtr pAlarmData;    //报警可变部分内容
        }

        //录播主机相关报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_RECORDHOST_ALARM
        {
            //报警固定部分
            public uint dwAlarmType;//报警类型,17-录播主机报警,
            public NET_DVR_TIME_EX struAlarmTime;    //发生报警的时间
            public byte bySubAlarmType;  //报警子类型，1-一键延迟录像； 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;/* 保留 */
            public NET_DVR_TIME_EX struRecordEndTime; //录播结束时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
            public IntPtr pAlarmData;    //报警可变部分内容
        }

        //其他报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_OTHER_ALARM
        {
            //报警固定部分
            public uint dwAlarmType;//报警类型,7-制式不匹配, 8-非法访问，11-智能场景变化，12-阵列异常，14-申请解码资源失败,15-智能侦测报警, 16-热备异常，18-语音对讲请求报警
            public NET_DVR_TIME_EX struAlarmTime;    //发生报警的时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
            public IntPtr pAlarmData;    //报警可变部分内容
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO
        {
            public int dwAlarmType;/*0-信号量报警,1-硬盘满,2-信号丢失,3－移动侦测,4－硬盘未格式化,5-读写硬盘出错,6-遮挡报警,7-制式不匹配, 8-非法访问, 9-串口状态, 0xa-GPS定位信息(车载定制)*/
            public int dwAlarmInputNumber;/*报警输入端口, 当报警类型为9时该变量表示串口状态0表示正常， -1表示错误*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmOutputNumber;/*触发的输出端口，哪一位为1表示对应哪一个输出*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmRelateChannel;/*触发的录像通道，哪一位为1表示对应哪一路录像, dwAlarmRelateChannel[0]对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwChannel;/*dwAlarmType为2或3,6时，表示哪个通道，dwChannel[0]位对应第1个通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwDiskNumber;/*dwAlarmType为1,4,5时,表示哪个硬盘, dwDiskNumber[0]位对应第1个硬盘*/
            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                dwAlarmOutputNumber = new int[MAX_ALARMOUT];
                dwAlarmRelateChannel = new int[MAX_CHANNUM];
                dwChannel = new int[MAX_CHANNUM];
                dwDiskNumber = new int[MAX_DISKNUM];
                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }
            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //IPC接入参数配置
        /* IP设备结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO
        {
            public uint dwEnable;/* 该IP设备是否启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword; /* 密码 */
            public NET_DVR_IPADDR struIP;/* IP地址 */
            public ushort wDVRPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */

            public void Init()
            {
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byRes = new byte[34];
            }
        }

        //ipc接入设备信息扩展，支持ip设备的域名添加
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO_V31
        {
            public byte byEnable;//该IP设备是否有效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留字段，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;//用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;//密码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byDomain;//设备域名
            public NET_DVR_IPADDR struIP;//IP地址
            public ushort wDVRPort;// 端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留字段，置0
        }

        /* IP通道匹配参数 */
        //sizeof(NET_DVR_IPCHANINFO) == 492
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO
        {
            public byte byEnable;/* 该通道是否在线 */
            public byte byIPID;/* IP设备ID 取值1- MAX_IP_DEVICE */
            public byte byChannel;/* 通道号 */
            public byte byIPIDHigh;//IP设备ID的高8位
            public byte byProType;//协议类型，0-海康协议(default)，1-松下协议，2-索尼
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 487, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留,置0
        }

        /* IP接入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;/* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
        }

        /* 扩展IP接入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V31
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，从低到高表示1-32通道，0表示无效 1有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
        }

        /* 报警输出参数 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO
        {
            public byte byIPID;/* IP设备ID取值1- MAX_IP_DEVICE */
            public byte byAlarmOut;/* 报警输出号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */

            public void Init()
            {
                byRes = new byte[18];
            }
        }

        /* IP报警输出配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG
        {
            public uint dwSize; /* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */

            public void Init()
            {
                struIPAlarmOutInfo = new NET_DVR_IPALARMOUTINFO[MAX_IP_ALARMOUT];
                for (int i = 0; i < MAX_IP_ALARMOUT; i++)
                {
                    struIPAlarmOutInfo[i].Init();
                }
            }
        }

        /* 报警输入参数 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO
        {
            public byte byIPID;/* IP设备ID取值1- MAX_IP_DEVICE */
            public byte byAlarmIn;/* 报警输入号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* 保留 */
        }

        /* IP报警输入配置结构 */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG
        {
            public uint dwSize;/* 结构大小 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP报警输入 */
        }

        //ipc alarm info
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* 模拟通道是否启用，0-未启用 1-启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP报警输入 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */
        }

        //ipc配置改变报警信息扩展 9000_1.1
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO_V31
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP设备 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;/* 模拟通道是否启用，0-未启用 1-启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IP通道 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo; /* IP报警输入 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP报警输出 */
        }

        public enum HD_STAT
        {
            HD_STAT_OK = 0,/* 正常 */
            HD_STAT_UNFORMATTED = 1,/* 未格式化 */
            HD_STAT_ERROR = 2,/* 错误 */
            HD_STAT_SMART_FAILED = 3,/* SMART状态 */
            HD_STAT_MISMATCH = 4,/* 不匹配 */
            HD_STAT_IDLE = 5, /* 休眠*/
            NET_HD_STAT_OFFLINE = 6,/*网络盘处于未连接状态 */
        }

        //本地硬盘信息配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HD
        {
            public uint dwHDNo;/*硬盘号, 取值0~MAX_DISKNUM_V30-1*/
            public uint dwCapacity;/*硬盘容量(不可设置)*/
            public uint dwFreeSpace;/*硬盘剩余空间(不可设置)*/
            public uint dwHdStatus;/*硬盘状态(不可设置) HD_STAT*/
            public byte byHDAttr;/*0-默认, 1-冗余; 2-只读*/
            public byte byHDType;/*0-本地硬盘,1-ESATA硬盘,2-NAS硬盘*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwHdGroup; /*属于哪个盘组 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDCFG
        {
            public uint dwSize;
            public uint dwHDCount;/*硬盘数(不可设置)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HD[] struHDInfo;//硬盘相关操作都需要重启才能生效；
        }

        //本地盘组信息配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HDGROUP
        {
            public uint dwHDGroupNo;/*盘组号(不可设置) 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byHDGroupChans;/*盘组对应的录像通道, 0-表示该通道不录象到该盘组，1-表示录象到该盘组*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDGROUP_CFG
        {
            public uint dwSize;
            public uint dwHDGroupCount;/*盘组总数(不可设置)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HD_GROUP, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HDGROUP[] struHDGroupAttr;//硬盘相关操作都需要重启才能生效
        }

        //配置缩放参数的结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCALECFG
        {
            public uint dwSize;
            public uint dwMajorScale;/* 主显示 0-不缩放，1-缩放*/
            public uint dwMinorScale;/* 辅显示 0-不缩放，1-缩放*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //DVR报警输出(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* 名称 */
            public uint dwAlarmOutDelay;/* 输出保持时间(-1为无限，手动关闭) */
            //0-5秒,1-10秒,2-30秒,3-1分钟,4-2分钟,5-5分钟,6-10分钟,7-手动
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* 报警输出激活时间段 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR报警输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* 名称 */
            public uint dwAlarmOutDelay;/* 输出保持时间(-1为无限，手动关闭) */
            //0-5秒,1-10秒,2-30秒,3-1分钟,4-2分钟,5-5分钟,6-10分钟,7-手动
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* 报警输出激活时间段 */
        }

        //DVR本地预览参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG_V30
        {
            public uint dwSize;
            public byte byPreviewNumber;//预览数目,0-1画面,1-4画面,2-9画面,3-16画面,0xff:最大画面
            public byte byEnableAudio;//是否声音预览,0-不预览,1-预览
            public ushort wSwitchTime;//切换时间,0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PREVIEW_MODE * MAX_WINDOW_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//切换顺序,如果lSwitchSeq[i]为 0xff表示不用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR本地预览参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG
        {
            public uint dwSize;
            public byte byPreviewNumber;//预览数目,0-1画面,1-4画面,2-9画面,3-16画面,0xff:最大画面
            public byte byEnableAudio;//是否声音预览,0-不预览,1-预览
            public ushort wSwitchTime;//切换时间,0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//切换顺序,如果lSwitchSeq[i]为 0xff表示不用
        }

        //DVR视频输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VGAPARA
        {
            public ushort wResolution;/* 分辨率 */
            public ushort wFreq;/* 刷新频率 */
            public uint dwBrightness;/* 亮度 */
        }

        //MATRIX输出参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.U2)]
            public ushort[] wOrder;/* 预览顺序, 0xff表示相应的窗口不预览 */
            public ushort wSwitchTime;// 预览切换时间 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA
        {
            public ushort wDisplayLogo;/* 显示视频通道号 */
            public ushort wDisplayOsd;/* 显示时间 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VOOUT
        {
            public byte byVideoFormat;/* 输出制式,0-PAL,1-NTSC */
            public byte byMenuAlphaValue;/* 菜单与背景图象对比度 */
            public ushort wScreenSaveTime;/* 屏幕保护时间 0-从不,1-1分钟,2-2分钟,3-5分钟,4-10分钟,5-20分钟,6-30分钟 */
            public ushort wVOffset;/* 视频输出偏移 */
            public ushort wBrightness;/* 视频输出亮度 */
            public byte byStartMode;/* 启动后视频输出模式(0:菜单,1:预览)*/
            public byte byEnableScaler;/* 是否启动缩放 (0-不启动, 1-启动)*/
        }

        //DVR视频输出(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;/* VGA参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MATRIXOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIXPARA_V30[] struMatrixPara;/* MATRIX参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR视频输出
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;    /* VGA参数 */
            public NET_DVR_MATRIXPARA struMatrixPara;/* MATRIX参数 */
        }

        //单用户参数(子结构)(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_INFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRight;/* 本地权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            /*数组6: 本地查看参数 */
            /*数组7: 本地管理模拟和IP camera */
            /*数组8: 本地备份 */
            /*数组9: 本地关机/重启 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemoteRight;/* 远程权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            /*数组11: 远程查看参数 */
            /*数组12: 远程管理模拟和IP camera */
            /*数组13: 远程关机/重启 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPreviewRight;/* 远程可以预览的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPlaybackRight;/* 本地可以回放的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPlaybackRight;/* 远程可以回放的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRecordRight;/* 本地可以录像的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetRecordRight;/* 远程可以录像的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPTZRight;/* 本地可以PTZ的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPTZRight;/* 远程可以PTZ的通道 0-有权限，1-无权限*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalBackupRight;/* 本地备份权限通道 0-有权限，1-无权限*/
            public NET_DVR_IPADDR struUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
            public byte byPriority;/* 优先级，0xff-无，0--低，1--中，2--高 */
            /*
            无……表示不支持优先级的设置
            低……默认权限:包括本地和远程回放,本地和远程查看日志和状态,本地和远程关机/重启
            中……包括本地和远程控制云台,本地和远程手动录像,本地和远程回放,语音对讲和远程预览
                  本地备份,本地/远程关机/重启
            高……管理员
            */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //单用户参数(SDK_V15扩展)(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO_EX
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* 权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            public uint dwLocalPlaybackRight;/* 本地可以回放的通道 bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* 权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            public uint dwNetPreviewRight;/* 远程可以预览的通道 bit0 -- channel 1*/
            public uint dwNetPlaybackRight;/* 远程可以回放的通道 bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
        }

        //单用户参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* 权限 */
            /*数组0: 本地控制云台*/
            /*数组1: 本地手动录象*/
            /*数组2: 本地回放*/
            /*数组3: 本地设置参数*/
            /*数组4: 本地查看状态、日志*/
            /*数组5: 本地高级操作(升级，格式化，重启，关机)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* 权限 */
            /*数组0: 远程控制云台*/
            /*数组1: 远程手动录象*/
            /*数组2: 远程回放 */
            /*数组3: 远程设置参数*/
            /*数组4: 远程查看状态、日志*/
            /*数组5: 远程高级操作(升级，格式化，重启，关机)*/
            /*数组6: 远程发起语音对讲*/
            /*数组7: 远程预览*/
            /*数组8: 远程请求报警上传、报警输出*/
            /*数组9: 远程控制，本地输出*/
            /*数组10: 远程控制串口*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* 用户IP地址(为0时表示允许任何地址) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* 物理地址 */
        }

        //DVR用户参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_V30[] struUser;
        }

        //DVR用户参数(SDK_V15扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_EX[] struUser;
        }

        //DVR用户参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO[] struUser;
        }

        //DVR异常参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION_V30[] struExceptionHandleType;
            /*数组0-盘满,1- 硬盘出错,2-网线断,3-局域网内IP 地址冲突, 4-非法访问, 5-输入/输出视频制式不匹配, 6-视频信号异常, 7-录像异常*/
        }

        //DVR异常参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION[] struExceptionHandleType;
            /*数组0-盘满,1- 硬盘出错,2-网线断,3-局域网内IP 地址冲突,4-非法访问, 5-输入/输出视频制式不匹配, 6-视频信号异常*/
        }

        //通道状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE_V30
        {
            public byte byRecordStatic;//通道是否在录像,0-不录像,1-录像
            public byte bySignalStatic;//连接的信号状态,0-正常,1-信号丢失
            public byte byHardwareStatic;//通道硬件状态,0-正常,1-异常,例如DSP死掉
            public byte byRes1;//保留
            public uint dwBitRate;//实际码率
            public uint dwLinkNum;//客户端连接的个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struClientIP;//客户端的IP地址
            public uint dwIPLinkNum;//如果该通道为IP接入，那么表示IP接入当前的连接数
            public byte byExceedMaxLink;//是否超出了单路6路连接数 0 - 未超出, 1-超出 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uint dwAllBitRate;//所有客户端和该通道连接的实际码率之和 
            public uint dwChannelNo;//当前的通道号，0xffffffff表示当前及后续通道信息无效
        }

        //通道状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE
        {
            public byte byRecordStatic;//通道是否在录像,0-不录像,1-录像
            public byte bySignalStatic;//连接的信号状态,0-正常,1-信号丢失
            public byte byHardwareStatic;//通道硬件状态,0-正常,1-异常,例如DSP死掉
            public byte reservedData;//保留
            public uint dwBitRate;//实际码率
            public uint dwLinkNum;//客户端连接的个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.U4)]
            public uint[] dwClientIP;//客户端的IP地址
        }

        //硬盘状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISKSTATE
        {
            public uint dwVolume;//硬盘的容量
            public uint dwFreeSpace;//硬盘的剩余空间
            public uint dwHardDiskStatic;//硬盘的状态,0-活动,1-休眠,2-不正常
        }

        //DVR工作状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V30
        {
            public uint dwDeviceStatic;//设备的状态,0-正常,1-CPU占用率太高,超过85%,2-硬件错误,例如串口死掉
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;//通道的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//报警端口的状态,0-没有报警,1-有报警
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//报警输出端口的状态,0-没有输出,1-有报警输出
            public uint dwLocalDisplay;//本地显示状态,0-正常,1-不正常
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;//表示语音通道的状态 0-未使用，1-使用中, 0xff无效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR工作状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE
        {
            public uint dwDeviceStatic;//设备的状态,0-正常,1-CPU占用率太高,超过85%,2-硬件错误,例如串口死掉
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE[] struChanStatic;//通道的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//报警端口的状态,0-没有报警,1-有报警
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//报警输出端口的状态,0-没有输出,1-有报警输出
            public uint dwLocalDisplay;//本地显示状态,0-正常,1-不正常

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM];
                struChanStatic = new NET_DVR_CHANNELSTATE[MAX_CHANNUM];
                byAlarmInStatic = new byte[MAX_ALARMIN];
                byAlarmOutStatic = new byte[MAX_ALARMOUT];
            }
        }

        /************************DVR日志 begin***************************/
        /* 报警 */
        //主类型
        public const int MAJOR_ALARM = 1;
        //次类型
        public const int MINOR_ALARM_IN = 1;/* 报警输入 */
        public const int MINOR_ALARM_OUT = 2;/* 报警输出 */
        public const int MINOR_MOTDET_START = 3; /* 移动侦测报警开始 */
        public const int MINOR_MOTDET_STOP = 4; /* 移动侦测报警结束 */
        public const int MINOR_HIDE_ALARM_START = 5;/* 遮挡报警开始 */
        public const int MINOR_HIDE_ALARM_STOP = 6;/* 遮挡报警结束 */
        public const int MINOR_VCA_ALARM_START = 7;/*智能报警开始*/
        public const int MINOR_VCA_ALARM_STOP = 8;/*智能报警停止*/
        public const int MINOR_ITS_ALARM_START = 0x09; // 交通事件报警开始 
        public const int MINOR_ITS_ALARM_STOP = 0x0a; // 交通事件报警结束 
        public const int MINOR_NETALARM_START = 0x0b; // 网络报警开始 
        public const int MINOR_NETALARM_STOP = 0x0c; // 网络报警结束 
        public const int MINOR_NETALARM_RESUME = 0x0d; // 网络报警恢复 
        public const int MINOR_WIRELESS_ALARM_START = 0x0e; // 无线报警开始 
        public const int MINOR_WIRELESS_ALARM_STOP = 0x0f; // 无线报警结束 
        public const int MINOR_PIR_ALARM_START = 0x10; // 人体感应报警开始 
        public const int MINOR_PIR_ALARM_STOP = 0x11; // 人体感应报警结束 
        public const int MINOR_CALLHELP_ALARM_START = 0x12; // 呼救报警开始 
        public const int MINOR_CALLHELP_ALARM_STOP = 0x13; // 呼救报警结束 
        public const int MINOR_DETECTFACE_ALARM_START = 0x16; // 人脸侦测报警开始 
        public const int MINOR_DETECTFACE_ALARM_STOP = 0x17; // 人脸侦测报警结束 
        public const int MINOR_VQD_ALARM_START = 0x18; //VQD报警 
        public const int MINOR_VQD_ALARM_STOP = 0x19; //VQD报警结束 
        public const int MINOR_VCA_SECNECHANGE_DETECTION = 0x1a; // 场景侦测报警 
        public const int MINOR_SMART_REGION_EXITING_BEGIN = 0x1b; // 离开区域侦测开始 
        public const int MINOR_SMART_REGION_EXITING_END = 0x1c; // 离开区域侦测结束 
        public const int MINOR_SMART_LOITERING_BEGIN = 0x1d; // 徘徊侦测开始 
        public const int MINOR_SMART_LOITERING_END = 0x1e; // 徘徊侦测结束 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_BEGIN = 0x20; // 越界侦测开始 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_END = 0x21; // 越界侦测结束 
        public const int MINOR_VCA_ALARM_INTRUDE_BEGIN = 0x22; // 区域入侵侦测开始 
        public const int MINOR_VCA_ALARM_INTRUDE_END = 0x23; // 区域入侵侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOINPUT = 0x24; // 音频丢失侦测 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL = 0x25; // 音频异常侦测 
        public const int MINOR_VCA_DEFOCUS_DETECTION_BEGIN = 0x26; // 虚焦侦测开始 
        public const int MINOR_VCA_DEFOCUS_DETECTION_END = 0x27; // 虚焦侦测结束
        public const int MINOR_EXT_ALARM = 0x28; // IPC外部报警
        public const int MINOR_VCA_FACE_ALARM_BEGIN = 0x29; // 人脸侦测开始 
        public const int MINOR_SMART_REGION_ENTRANCE_BEGIN = 0x2a; // 进入区域侦测开始 
        public const int MINOR_SMART_REGION_ENTRANCE_END = 0x2b; // 进入区域侦测结束 
        public const int MINOR_SMART_PEOPLE_GATHERING_BEGIN = 0x2c; // 人员聚集侦测开始 
        public const int MINOR_SMART_PEOPLE_GATHERING_END = 0x2d; // 人员聚集侦测结束 
        public const int MINOR_SMART_FAST_MOVING_BEGIN = 0x2e; // 快速运动侦测开始 
        public const int MINOR_SMART_FAST_MOVING_END = 0x2f; // 快速运动侦测结束 
        public const int MINOR_VCA_FACE_ALARM_END = 0x30; // 人脸侦测结束 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_BEGIN = 0x31; // 场景变更侦测开始 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_END = 0x32; // 场景变更侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_BEGIN = 0x33; // 音频丢失侦测开始 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_END = 0x34; // 音频丢失侦测结束 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_BEGIN = 0x35; // 声强突变侦测开始 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_END = 0x36; // 声强突变侦测结束 

        public const int MINOR_VCA_LECTURE_DETECTION_BEGIN = 0x37;  //授课侦测开始报警
        public const int MINOR_VCA_LECTURE_DETECTION_END = 0x38;  //授课侦测结束报警
        public const int MINOR_VCA_ALARM_AUDIOSTEEPDROP = 0x39;  //声强陡降 2014-03-21
        public const int MINOR_VCA_ANSWER_DETECTION_BEGIN = 0x3a;  //回答问题侦测开始报警
        public const int MINOR_VCA_ANSWER_DETECTION_END = 0x3b;  //回答问题侦测结束报警

        public const int MINOR_SMART_PARKING_BEGIN = 0x3c; // 停车侦测开始 
        public const int MINOR_SMART_PARKING_END = 0x3d; // 停车侦测结束 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_BEGIN = 0x3e; // 物品遗留侦测开始 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_END = 0x3f; // 物品遗留侦测结束 
        public const int MINOR_SMART_OBJECT_REMOVAL_BEGIN = 0x40; // 物品拿取侦测开始 
        public const int MINOR_SMART_OBJECT_REMOVAL_END = 0x41; // 物品拿取侦测结束 
        public const int MINOR_SMART_VEHICLE_ALARM_START = 0x46;   //车牌检测开始
        public const int MINOR_SMART_VEHICLE_ALARM_STOP = 0x47;   //车牌检测结束
        public const int MINOR_THERMAL_FIREDETECTION = 0x48;   //热成像火点检测侦测开始
        public const int MINOR_THERMAL_FIREDETECTION_END = 0x49;   //热成像火点检测侦测结束
        public const int MINOR_SMART_VANDALPROOF_BEGIN = 0x50;   //防破坏检测开始
        public const int MINOR_SMART_VANDALPROOF_END = 0x51; //防破坏检测结束

        public const int MINOR_ALARMIN_SHORT_CIRCUIT = 0x400; // 防区短路报警 
        public const int MINOR_ALARMIN_BROKEN_CIRCUIT = 0x401; // 防区断路报警 
        public const int MINOR_ALARMIN_EXCEPTION = 0x402; // 防区异常报警 
        public const int MINOR_ALARMIN_RESUME = 0x403; // 防区报警恢复 
        public const int MINOR_HOST_DESMANTLE_ALARM = 0x404; //防区防拆报警  
        public const int MINOR_HOST_DESMANTLE_RESUME = 0x405; // 防区防拆恢复 
        public const int MINOR_CARD_READER_DESMANTLE_ALARM = 0x406; //读卡器防拆报警 
        public const int MINOR_CARD_READER_DESMANTLE_RESUME = 0x407; // 读卡器防拆恢复  
        public const int MINOR_CASE_SENSOR_ALARM = 0x408; // 事件输入报警 
        public const int MINOR_CASE_SENSOR_RESUME = 0x409; // 事件输入恢复 
        public const int MINOR_STRESS_ALARM = 0x40a; // 胁迫报警 
        public const int MINOR_OFFLINE_ECENT_NEARLY_FULL = 0x40b; // 离线事件满90%报警 
        public const int MINOR_CARD_MAX_AUTHENTICATE_FAIL = 0x40c; // 卡号认证失败超次报警 
        public const int MINOR_SD_CARD_FULL = 0x40d;  //SD卡存储满报警
        public const int MINOR_LINKAGE_CAPTURE_PIC = 0x40e;  //联动抓拍事件报警

        /* 异常 */
        //主类型
        public const int MAJOR_EXCEPTION = 2;
        //次类型
        public const int MINOR_VI_LOST = 33;/* 视频信号丢失 */
        public const int MINOR_ILLEGAL_ACCESS = 34;/* 非法访问 */
        public const int MINOR_HD_FULL = 35;/* 硬盘满 */
        public const int MINOR_HD_ERROR = 36;/* 硬盘错误 */
        public const int MINOR_DCD_LOST = 37;/* MODEM 掉线(保留不使用) */
        public const int MINOR_IP_CONFLICT = 38;/* IP地址冲突 */
        public const int MINOR_NET_BROKEN = 39;/* 网络断开*/
        public const int MINOR_REC_ERROR = 40;/* 录像出错 */
        public const int MINOR_IPC_NO_LINK = 41;/* IPC连接异常 */
        public const int MINOR_VI_EXCEPTION = 42;/* 视频输入异常(只针对模拟通道) */
        public const int MINOR_IPC_IP_CONFLICT = 43;/*ipc ip 地址 冲突*/
        public const int MINOR_RAID_ERROR = 0x20; // 阵列异常  
        public const int MINOR_SENCE_EXCEPTION = 0x2c; // 场景异常 
        public const int MINOR_PIC_REC_ERROR = 0x2d; // 抓图出错,获取图片文件失败 
        public const int MINOR_VI_MISMATCH = 0x2e; // 视频制式不匹配 
        public const int MINOR_RESOLUTION_MISMATCH = 0x2f; // 编码分辨率和前端分辨率不匹配 

        //2010-01-22 增加视频综合平台异常日志次类型
        public const int MINOR_NET_ABNORMAL = 0x35; /*网络状态异常*/
        public const int MINOR_MEM_ABNORMAL = 0x36; /*内存状态异常*/
        public const int MINOR_FILE_ABNORMAL = 0x37; /*文件状态异常*/
        public const int MINOR_PANEL_ABNORMAL = 0x38; /*前面板连接异常*/
        public const int MINOR_PANEL_RESUME = 0x39; /*前面板恢复正常*/
        public const int MINOR_RS485_DEVICE_ABNORMAL = 0x3a; /*RS485连接状态异常*/
        public const int MINOR_RS485_DEVICE_REVERT = 0x3b; /*RS485连接状态异常恢复*/
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALREBOOT = 0x3c; // 子板异常启动 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALINSERT = 0x3d; // 子板插入 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALPULLOUT = 0x3e; // 子板拔出 
        public const int MINOR_SCREEN_ABNARMALTEMPERATURE = 0x3f; // 温度异常 
        public const int MINOR_RECORD_OVERFLOW = 0x41; // 缓冲区溢出 
        public const int MINOR_DSP_ABNORMAL = 0x42; // DSP异常 
        public const int MINOR_ANR_RECORD_FAIED = 0x43; // ANR录像失败 
        public const int MINOR_SPARE_WORK_DEVICE_EXCEPT = 0x44; // 热备设备工作机异常 
        public const int MINOR_START_IPC_MAS_FAILED = 0x45; // 开启IPC MAS失败 
        public const int MINOR_IPCM_CRASH = 0x46; // IPCM异常重启 
        public const int MINOR_POE_POWER_EXCEPTION = 0x47; // POE供电异常 
        public const int MINOR_UPLOAD_DATA_CS_EXCEPTION = 0x48; // 云存储数据上传失败 
        public const int MINOR_DIAL_EXCEPTION = 0x49;         /*拨号异常*/
        public const int MINOR_DEV_EXCEPTION_OFFLINE = 0x50;  //设备异常下线
        public const int MINOR_UPGRADEFAIL = 0x51; //远程升级设备失败
        public const int MINOR_AI_LOST = 0x52; /* 音频信号丢失 */

        public const int MINOR_DEV_POWER_ON = 0x400; // 设备上电启动 
        public const int MINOR_DEV_POWER_OFF = 0x401; // 设备掉电关闭 
        public const int MINOR_WATCH_DOG_RESET = 0x402; // 看门狗复位 
        public const int MINOR_LOW_BATTERY = 0x403; // 蓄电池电压低 
        public const int MINOR_BATTERY_RESUME = 0x404; // 蓄电池电压恢复正常 
        public const int MINOR_AC_OFF = 0x405; // 交流电断电 
        public const int MINOR_AC_RESUME = 0x406; // 交流电恢复 
        public const int MINOR_NET_RESUME = 0x407; // 网络恢复 
        public const int MINOR_FLASH_ABNORMAL = 0x408; // FLASH读写异常 
        public const int MINOR_CARD_READER_OFFLINE = 0x409; // 读卡器掉线 
        public const int MINOR_CARD_READER_RESUME = 0x40a; // 读卡器掉线恢复 
        public const int MINOR_SUBSYSTEM_IP_CONFLICT = 0x4000; // 子板IP冲突 
        public const int MINOR_SUBSYSTEM_NET_BROKEN = 0x4001; // 子板断网 
        public const int MINOR_FAN_ABNORMAL = 0x4002; // 风扇异常 
        public const int MINOR_BACKPANEL_TEMPERATURE_ABNORMAL = 0x4003; // 背板温度异常 

        //视频综合平台
        public const int MINOR_FANABNORMAL = 49;/* 视频综合平台：风扇状态异常 */
        public const int MINOR_FANRESUME = 50;/* 视频综合平台：风扇状态恢复正常 */
        public const int MINOR_SUBSYSTEM_ABNORMALREBOOT = 51;/* 视频综合平台：6467异常重启 */
        public const int MINOR_MATRIX_STARTBUZZER = 52;/* 视频综合平台：dm6467异常，启动蜂鸣器 */

        /* 操作 */
        //主类型
        public const int MAJOR_OPERATION = 3;

        //次类型
        public const int MINOR_VCA_MOTIONEXCEPTION = 0x29; //智能侦测异常
        public const int MINOR_START_DVR = 0x41; // 开机 
        public const int MINOR_STOP_DVR = 0x42; // 关机 
        public const int MINOR_STOP_ABNORMAL = 0x43; // 异常关机 
        public const int MINOR_REBOOT_DVR = 0x44; // 本地重启设备 

        public const int MINOR_LOCAL_LOGIN = 0x50; // 本地登陆 
        public const int MINOR_LOCAL_LOGOUT = 0x51; // 本地注销登陆 
        public const int MINOR_LOCAL_CFG_PARM = 0x52; // 本地配置参数 
        public const int MINOR_LOCAL_PLAYBYFILE = 0x53; // 本地按文件回放或下载 
        public const int MINOR_LOCAL_PLAYBYTIME = 0x54; // 本地按时间回放或下载 
        public const int MINOR_LOCAL_START_REC = 0x55; // 本地开始录像 
        public const int MINOR_LOCAL_STOP_REC = 0x56; // 本地停止录像 
        public const int MINOR_LOCAL_PTZCTRL = 0x57; // 本地云台控制 
        public const int MINOR_LOCAL_PREVIEW = 0x58; // 本地预览(保留不使用) 
        public const int MINOR_LOCAL_MODIFY_TIME = 0x59; // 本地修改时间(保留不使用) 
        public const int MINOR_LOCAL_UPGRADE = 0x5a; // 本地升级 
        public const int MINOR_LOCAL_RECFILE_OUTPUT = 0x5b; // 本地备份录象文件 
        public const int MINOR_LOCAL_FORMAT_HDD = 0x5c; // 本地初始化硬盘 
        public const int MINOR_LOCAL_CFGFILE_OUTPUT = 0x5d; // 导出本地配置文件 
        public const int MINOR_LOCAL_CFGFILE_INPUT = 0x5e; // 导入本地配置文件 
        public const int MINOR_LOCAL_COPYFILE = 0x5f; // 本地备份文件 
        public const int MINOR_LOCAL_LOCKFILE = 0x60; // 本地锁定录像文件 
        public const int MINOR_LOCAL_UNLOCKFILE = 0x61; // 本地解锁录像文件 
        public const int MINOR_LOCAL_DVR_ALARM = 0x62; // 本地手动清除和触发报警 
        public const int MINOR_IPC_ADD = 0x63; // 本地添加IPC 
        public const int MINOR_IPC_DEL = 0x64; // 本地删除IPC 
        public const int MINOR_IPC_SET = 0x65; // 本地设置IPC 
        public const int MINOR_LOCAL_START_BACKUP = 0x66; // 本地开始备份 
        public const int MINOR_LOCAL_STOP_BACKUP = 0x67; // 本地停止备份 
        public const int MINOR_LOCAL_COPYFILE_START_TIME = 0x68; // 本地备份开始时间 
        public const int MINOR_LOCAL_COPYFILE_END_TIME = 0x69; // 本地备份结束时间 
        public const int MINOR_LOCAL_ADD_NAS = 0x6a; // 本地添加网络硬盘 
        public const int MINOR_LOCAL_DEL_NAS = 0x6b; // 本地删除NAS盘 
        public const int MINOR_LOCAL_SET_NAS = 0x6c; // 本地设置NAS盘 
        public const int MINOR_LOCAL_RESET_PASSWD = 0x6d; /* 本地恢复管理员默认密码*/

        public const int MINOR_REMOTE_LOGIN = 0x70; // 远程登录 
        public const int MINOR_REMOTE_LOGOUT = 0x71; // 远程注销登陆 
        public const int MINOR_REMOTE_START_REC = 0x72; // 远程开始录像 
        public const int MINOR_REMOTE_STOP_REC = 0x73; // 远程停止录像 
        public const int MINOR_START_TRANS_CHAN = 0x74; // 开始透明传输 
        public const int MINOR_STOP_TRANS_CHAN = 0x75; // 停止透明传输 
        public const int MINOR_REMOTE_GET_PARM = 0x76; // 远程获取参数 
        public const int MINOR_REMOTE_CFG_PARM = 0x77; // 远程配置参数 
        public const int MINOR_REMOTE_GET_STATUS = 0x78; // 远程获取状态 
        public const int MINOR_REMOTE_ARM = 0x79; // 远程布防 
        public const int MINOR_REMOTE_DISARM = 0x7a; // 远程撤防 
        public const int MINOR_REMOTE_REBOOT = 0x7b; // 远程重启 
        public const int MINOR_START_VT = 0x7c; // 开始语音对讲 
        public const int MINOR_STOP_VT = 0x7d; // 停止语音对讲 
        public const int MINOR_REMOTE_UPGRADE = 0x7e; // 远程升级 
        public const int MINOR_REMOTE_PLAYBYFILE = 0x7f; // 远程按文件回放 
        public const int MINOR_REMOTE_PLAYBYTIME = 0x80; // 远程按时间回放 
        public const int MINOR_REMOTE_PTZCTRL = 0x81; // 远程云台控制 
        public const int MINOR_REMOTE_FORMAT_HDD = 0x82; // 远程格式化硬盘 
        public const int MINOR_REMOTE_STOP = 0x83; // 远程关机 
        public const int MINOR_REMOTE_LOCKFILE = 0x84; // 远程锁定文件 
        public const int MINOR_REMOTE_UNLOCKFILE = 0x85; // 远程解锁文件 
        public const int MINOR_REMOTE_CFGFILE_OUTPUT = 0x86; // 远程导出配置文件 
        public const int MINOR_REMOTE_CFGFILE_INTPUT = 0x87; // 远程导入配置文件 
        public const int MINOR_REMOTE_RECFILE_OUTPUT = 0x88; // 远程导出录象文件 
        public const int MINOR_REMOTE_DVR_ALARM = 0x89; // 远程手动清除和触发报警 
        public const int MINOR_REMOTE_IPC_ADD = 0x8a; // 远程添加IPC 
        public const int MINOR_REMOTE_IPC_DEL = 0x8b; // 远程删除IPC 
        public const int MINOR_REMOTE_IPC_SET = 0x8c; // 远程设置IPC 
        public const int MINOR_REBOOT_VCA_LIB = 0x8d; // 重启智能库 
        public const int MINOR_REMOTE_ADD_NAS = 0x8e; // 远程添加NAS盘 
        public const int MINOR_REMOTE_DEL_NAS = 0x8f; // 远程删除NAS盘 

        public const int MINOR_REMOTE_SET_NAS = 0x90; // 远程设置NAS盘 
        public const int MINOR_LOCAL_START_REC_CDRW = 0x91; // 本地开始刻录 
        public const int MINOR_LOCAL_STOP_REC_CDRW = 0x92; // 本地停止刻录 
        public const int MINOR_REMOTE_START_REC_CDRW = 0x93; // 远程开始刻录 
        public const int MINOR_REMOTE_STOP_REC_CDRW = 0x94; // 远程停止刻录 
        public const int MINOR_LOCAL_PIC_OUTPUT = 0x95; // 本地备份图片文件 
        public const int MINOR_REMOTE_PIC_OUTPUT = 0x96; // 远程备份图片文件 
        public const int MINOR_LOCAL_INQUEST_RESUME = 0x97; // 本地恢复审讯事件 
        public const int MINOR_REMOTE_INQUEST_RESUME = 0x98; // 远程恢复审讯事件 
        public const int MINOR_LOCAL_ADD_FILE = 0x99; // 本地导入文件 
        public const int MINOR_REMOTE_DELETE_HDISK = 0x9a; // 远程删除异常不存在的硬盘 
        public const int MINOR_REMOTE_LOAD_HDISK = 0x9b; // 远程加载硬盘 
        public const int MINOR_REMOTE_UNLOAD_HDISK = 0x9c; // 远程卸载硬盘 
        public const int MINOR_LOCAL_OPERATE_LOCK = 0x9d; // 本地操作锁定 
        public const int MINOR_LOCAL_OPERATE_UNLOCK = 0x9e; // 本地操作解除锁定 
        public const int MINOR_LOCAL_DEL_FILE = 0x9f; // 本地删除审讯文件 

        public const int MINOR_SUBSYSTEMREBOOT = 0xa0; /*视频综合平台：dm6467 正常重启*/
        public const int MINOR_MATRIX_STARTTRANSFERVIDEO = 0xa1; /*视频综合平台：矩阵切换开始传输图像*/
        public const int MINOR_MATRIX_STOPTRANSFERVIDEO = 0xa2; /*视频综合平台：矩阵切换停止传输图像*/
        public const int MINOR_REMOTE_SET_ALLSUBSYSTEM = 0xa3; /*视频综合平台：设置所有6467子系统信息*/
        public const int MINOR_REMOTE_GET_ALLSUBSYSTEM = 0xa4; /*视频综合平台：获取所有6467子系统信息*/
        public const int MINOR_REMOTE_SET_PLANARRAY = 0xa5; /*视频综合平台：设置计划轮巡组*/
        public const int MINOR_REMOTE_GET_PLANARRAY = 0xa6; /*视频综合平台：获取计划轮巡组*/
        public const int MINOR_MATRIX_STARTTRANSFERAUDIO = 0xa7; /*视频综合平台：矩阵切换开始传输音频*/
        public const int MINOR_MATRIX_STOPRANSFERAUDIO = 0xa8; /*视频综合平台：矩阵切换停止传输音频*/
        public const int MINOR_LOGON_CODESPITTER = 0xa9; /*视频综合平台：登陆码分器*/
        public const int MINOR_LOGOFF_CODESPITTER = 0xaa; /*视频综合平台：退出码分器*/

        //KY2013 3.0.0
        public const int MINOR_LOCAL_MAIN_AUXILIARY_PORT_SWITCH = 0X302; //本地主辅口切换
        public const int MINOR_LOCAL_HARD_DISK_CHECK = 0x303; //本地物理硬盘自检
        //2010-01-22 增加视频综合平台中解码器操作日志
        public const int MINOR_START_DYNAMIC_DECODE = 0xb; /*开始动态解码*/
        public const int MINOR_STOP_DYNAMIC_DECODE = 0xb1; /*停止动态解码*/
        public const int MINOR_GET_CYC_CFG = 0xb2; /*获取解码器通道轮巡配置*/
        public const int MINOR_SET_CYC_CFG = 0xb3; /*设置解码通道轮巡配置*/
        public const int MINOR_START_CYC_DECODE = 0xb4; /*开始轮巡解码*/
        public const int MINOR_STOP_CYC_DECODE = 0xb5; /*停止轮巡解码*/
        public const int MINOR_GET_DECCHAN_STATUS = 0xb6; /*获取解码通道状态*/
        public const int MINOR_GET_DECCHAN_INFO = 0xb7; /*获取解码通道当前信息*/
        public const int MINOR_START_PASSIVE_DEC = 0xb8; /*开始被动解码*/
        public const int MINOR_STOP_PASSIVE_DEC = 0xb9; /*停止被动解码*/
        public const int MINOR_CTRL_PASSIVE_DEC = 0xba; /*控制被动解码*/
        public const int MINOR_RECON_PASSIVE_DEC = 0xbb; /*被动解码重连*/
        public const int MINOR_GET_DEC_CHAN_SW = 0xbc; /*获取解码通道总开关*/
        public const int MINOR_SET_DEC_CHAN_SW = 0xbd; /*设置解码通道总开关*/
        public const int MINOR_CTRL_DEC_CHAN_SCALE = 0xbe; /*解码通道缩放控制*/
        public const int MINOR_SET_REMOTE_REPLAY = 0xbf; /*设置远程回放*/
        public const int MINOR_GET_REMOTE_REPLAY = 0xc0; /*获取远程回放状态*/
        public const int MINOR_CTRL_REMOTE_REPLAY = 0xc1; /*远程回放控制*/
        public const int MINOR_SET_DISP_CFG = 0xc2; /*设置显示通道*/
        public const int MINOR_GET_DISP_CFG = 0xc3; /*获取显示通道设置*/
        public const int MINOR_SET_PLANTABLE = 0xc4; /*设置计划轮巡表*/
        public const int MINOR_GET_PLANTABLE = 0xc5;/*获取计划轮巡表*/
        public const int MINOR_START_PPPPOE = 0xc6; /*开始PPPoE连接*/
        public const int MINOR_STOP_PPPPOE = 0xc7; /*结束PPPoE连接*/
        public const int MINOR_UPLOAD_LOGO = 0xc8; /*上传LOGO*/

        //推模式操作日志
        public const int MINOR_LOCAL_PIN = 0xc9; /* 本地PIN功能操作 */
        public const int MINOR_LOCAL_DIAL = 0xca; /* 本地手动启动断开拨号 */
        public const int MINOR_SMS_CONTROL = 0xcb; /* 短信控制上下线 */
        public const int MINOR_CALL_ONLINE = 0xc; /* 呼叫控制上线 */
        public const int MINOR_REMOTE_PIN = 0xcd; /* 远程PIN功能操作 */

        public const int MINOR_REMOTE_BYPASS = 0xd0; // 远程旁路 
        public const int MINOR_REMOTE_UNBYPASS = 0xd1; // 远程旁路恢复 
        public const int MINOR_REMOTE_SET_ALARMIN_CFG = 0xd2; // 远程设置报警输入参数 
        public const int MINOR_REMOTE_GET_ALARMIN_CFG = 0xd3; // 远程获取报警输入参数 
        public const int MINOR_REMOTE_SET_ALARMOUT_CFG = 0xd4; // 远程设置报警输出参数 
        public const int MINOR_REMOTE_GET_ALARMOUT_CFG = 0xd5; // 远程获取报警输出参数 
        public const int MINOR_REMOTE_ALARMOUT_OPEN_MAN = 0xd6; // 远程手动开启报警输出 
        public const int MINOR_REMOTE_ALARMOUT_CLOSE_MAN = 0xd7; // 远程手动关闭报警输出 
        public const int MINOR_REMOTE_ALARM_ENABLE_CFG = 0xd8; // 远程设置报警主机的RS485串口使能状态 
        public const int MINOR_DBDATA_OUTPUT = 0xd9; // 导出数据库记录 
        public const int MINOR_DBDATA_INPUT = 0xda; // 导入数据库记录 
        public const int MINOR_MU_SWITCH = 0xdb; // 级联切换 
        public const int MINOR_MU_PTZ = 0xdc; // 级联PTZ控制
        public const int MINOR_DELETE_LOGO = 0xdd; /* 删除logo */

        public const int MINOR_LOCAL_CONF_REB_RAID = 0x101; // 本地配置自动重建 
        public const int MINOR_LOCAL_CONF_SPARE = 0x102; // 本地配置热备 
        public const int MINOR_LOCAL_ADD_RAID = 0x103; // 本地创建阵列 
        public const int MINOR_LOCAL_DEL_RAID = 0x104; // 本地删除阵列 
        public const int MINOR_LOCAL_MIG_RAID = 0x105; // 本地迁移阵列 
        public const int MINOR_LOCAL_REB_RAID = 0x106; // 本地手动重建阵列 
        public const int MINOR_LOCAL_QUICK_CONF_RAID = 0x107; // 本地一键配置 
        public const int MINOR_LOCAL_ADD_VD = 0x108; // 本地创建虚拟磁盘 
        public const int MINOR_LOCAL_DEL_VD = 0x109; // 本地删除虚拟磁盘 
        public const int MINOR_LOCAL_RP_VD = 0x10a; // 本地修复虚拟磁盘 
        public const int MINOR_LOCAL_FORMAT_EXPANDVD = 0x10b; // 本地扩展虚拟磁盘扩容 
        public const int MINOR_LOCAL_RAID_UPGRADE = 0x10c; // 本地raid卡升级 
        public const int MINOR_LOCAL_STOP_RAID = 0x10d; // 本地暂停RAID操作(即安全拔盘) 
        public const int MINOR_REMOTE_CONF_REB_RAID = 0x111; // 远程配置自动重建 
        public const int MINOR_REMOTE_CONF_SPARE = 0x112; // 远程配置热备 
        public const int MINOR_REMOTE_ADD_RAID = 0x113; // 远程创建阵列 
        public const int MINOR_REMOTE_DEL_RAID = 0x114; // 远程删除阵列 
        public const int MINOR_REMOTE_MIG_RAID = 0x115; // 远程迁移阵列 
        public const int MINOR_REMOTE_REB_RAID = 0x116; // 远程手动重建阵列 
        public const int MINOR_REMOTE_QUICK_CONF_RAID = 0x117; // 远程一键配置 
        public const int MINOR_REMOTE_ADD_VD = 0x118; // 远程创建虚拟磁盘 
        public const int MINOR_REMOTE_DEL_VD = 0x119; // 远程删除虚拟磁盘 
        public const int MINOR_REMOTE_RP_VD = 0x11a; // 远程修复虚拟磁盘 
        public const int MINOR_REMOTE_FORMAT_EXPANDVD = 0x11b; // 远程虚拟磁盘扩容 
        public const int MINOR_REMOTE_RAID_UPGRADE = 0x11c; // 远程raid卡升级 
        public const int MINOR_REMOTE_STOP_RAID = 0x11d; // 远程暂停RAID操作(即安全拔盘) 
        public const int MINOR_LOCAL_START_PIC_REC = 0x121; // 本地开始抓图 
        public const int MINOR_LOCAL_STOP_PIC_REC = 0x122; // 本地停止抓图 
        public const int MINOR_LOCAL_SET_SNMP = 0x125; // 本地配置SNMP 
        public const int MINOR_LOCAL_TAG_OPT = 0x126; // 本地标签操作 
        public const int MINOR_REMOTE_START_PIC_REC = 0x131; // 远程开始抓图 
        public const int MINOR_REMOTE_STOP_PIC_REC = 0x132; // 远程停止抓图 
        public const int MINOR_REMOTE_SET_SNMP = 0x135; // 远程配置SNMP 
        public const int MINOR_REMOTE_TAG_OPT = 0x136; // 远程标签操作 

        public const int MINOR_LOCAL_VOUT_SWITCH = 0x140; // 本地输出口切换操作 
        public const int MINOR_STREAM_CABAC = 0x141; // 码流压缩性能选项配置操作 

        public const int MINOR_LOCAL_SPARE_OPT = 0x142;   /*本地N+1 热备相关操作*/
        public const int MINOR_REMOTE_SPARE_OPT = 0x143;   /*远程N+1 热备相关操作*/
        public const int MINOR_LOCAL_IPCCFGFILE_OUTPUT = 0x144;      /* 本地导出ipc配置文件*/
        public const int MINOR_LOCAL_IPCCFGFILE_INPUT = 0x145;   /* 本地导入ipc配置文件 */
        public const int MINOR_LOCAL_IPC_UPGRADE = 0x146;   /* 本地升级IPC */
        public const int MINOR_REMOTE_IPCCFGFILE_OUTPUT = 0x147;   /* 远程导出ipc配置文件*/
        public const int MINOR_REMOTE_IPCCFGFILE_INPUT = 0x148;   /* 远程导入ipc配置文件*/
        public const int MINOR_REMOTE_IPC_UPGRADE = 0x149;   /* 远程升级IPC */

        public const int MINOR_SET_MULTI_MASTER = 0x201; // 设置大屏主屏 
        public const int MINOR_SET_MULTI_SLAVE = 0x202; // 设置大屏子屏 
        public const int MINOR_CANCEL_MULTI_MASTER = 0x203; // 取消大屏主屏 
        public const int MINOR_CANCEL_MULTI_SLAVE = 0x204; // 取消大屏子屏 

        public const int MINOR_DISPLAY_LOGO = 0x205;    /*显示LOGO*/
        public const int MINOR_HIDE_LOGO = 0x206;    /*隐藏LOGO*/
        public const int MINOR_SET_DEC_DELAY_LEVEL = 0x207;    /*解码通道延时级别设置*/
        public const int MINOR_SET_BIGSCREEN_DIPLAY_AREA = 0x208;    /*设置大屏显示区域*/
        public const int MINOR_CUT_VIDEO_SOURCE = 0x209;    /*大屏视频源切割设置*/
        public const int MINOR_SET_BASEMAP_AREA = 0x210;    /*大屏底图区域设置*/
        public const int MINOR_DOWNLOAD_BASEMAP = 0x211;    /*下载大屏底图*/
        public const int MINOR_CUT_BASEMAP = 0x212;    /*底图切割配置*/
        public const int MINOR_CONTROL_ELEC_ENLARGE = 0x213;    /*电子放大操作(放大或还原)*/
        public const int MINOR_SET_OUTPUT_RESOLUTION = 0x214;    /*显示输出分辨率设置*/
        public const int MINOR_SET_TRANCSPARENCY = 0X215;    /*图层透明度设置*/
        public const int MINOR_SET_OSD = 0x216;    /*显示OSD设置*/
        public const int MINOR_RESTORE_DEC_STATUS = 0x217;    /*恢复初始状态(场景切换时，解码恢复初始状态)*/

        public const int MINOR_SCREEN_SET_INPUT = 0x251; // 修改输入源 
        public const int MINOR_SCREEN_SET_OUTPUT = 0x252; // 修改输出通道 
        public const int MINOR_SCREEN_SET_OSD = 0x253; // 修改虚拟LED 
        public const int MINOR_SCREEN_SET_LOGO = 0x254; // 修改LOGO 
        public const int MINOR_SCREEN_SET_LAYOUT = 0x255; // 设置场景 
        public const int MINOR_SCREEN_PICTUREPREVIEW = 0x256; // 回显操作 

        public const int MINOR_SCREEN_GET_OSD = 0x257; // 获取虚拟LED 
        public const int MINOR_SCREEN_GET_LAYOUT = 0x258; // 获取场景 
        public const int MINOR_SCREEN_LAYOUT_CTRL = 0x259; // 场景控制 
        public const int MINOR_GET_ALL_VALID_WND = 0x260; // 获取所有有效窗口 
        public const int MINOR_GET_SIGNAL_WND = 0x261; // 获取单个窗口信息 
        public const int MINOR_WINDOW_CTRL = 0x262; // 窗口控制 
        public const int MINOR_GET_LAYOUT_LIST = 0x263; // 获取场景列表 
        public const int MINOR_LAYOUT_CTRL = 0x264; // 场景控制 
        public const int MINOR_SET_LAYOUT = 0x265; // 设置单个场景 
        public const int MINOR_GET_SIGNAL_LIST = 0x266; // 获取输入信号源列表 
        public const int MINOR_GET_PLAN_LIST = 0x267; // 获取预案列表 
        public const int MINOR_SET_PLAN = 0x268; // 修改预案 
        public const int MINOR_CTRL_PLAN = 0x269; // 控制预案 
        public const int MINOR_CTRL_SCREEN = 0x270; // 屏幕控制 
        public const int MINOR_ADD_NETSIG = 0x271; // 添加信号源 
        public const int MINOR_SET_NETSIG = 0x272; // 修改信号源 
        public const int MINOR_SET_DECBDCFG = 0x273; // 设置解码板参数 
        public const int MINOR_GET_DECBDCFG = 0x274; // 获取解码板参数 
        public const int MINOR_GET_DEVICE_STATUS = 0x275; // 获取设备信息 
        public const int MINOR_UPLOAD_PICTURE = 0x276; // 底图上传 
        public const int MINOR_SET_USERPWD = 0x277; // 设置用户密码 
        public const int MINOR_ADD_LAYOUT = 0x278; // 添加场景 
        public const int MINOR_DEL_LAYOUT = 0x279; // 删除场景 
        public const int MINOR_DEL_NETSIG = 0x280; // 删除信号源 
        public const int MINOR_ADD_PLAN = 0x281; // 添加预案 
        public const int MINOR_DEL_PLAN = 0x282; // 删除预案 
        public const int MINOR_GET_EXTERNAL_MATRIX_CFG = 0x283; // 获取外接矩阵配置 
        public const int MINOR_SET_EXTERNAL_MATRIX_CFG = 0x284; // 设置外接矩阵配置 
        public const int MINOR_GET_USER_CFG = 0x285; // 获取用户配置 
        public const int MINOR_SET_USER_CFG = 0x286; // 设置用户配置 
        public const int MINOR_GET_DISPLAY_PANEL_LINK_CFG = 0x287; // 获取显示墙连接配置 
        public const int MINOR_SET_DISPLAY_PANEL_LINK_CFG = 0x288; // 设置显示墙连接配置 

        public const int MINOR_GET_WALLSCENE_PARAM = 0x289; // 获取电视墙场景 
        public const int MINOR_SET_WALLSCENE_PARAM = 0x28a; // 设置电视墙场景 
        public const int MINOR_GET_CURRENT_WALLSCENE = 0x28b; // 获取当前使用场景 
        public const int MINOR_SWITCH_WALLSCENE = 0x28c; // 场景切换 
        public const int MINOR_SIP_LOGIN = 0x28d; //SIP注册成功
        public const int MINOR_VOIP_START = 0x28e; //VOIP对讲开始
        public const int MINOR_VOIP_STOP = 0x28f; //VOIP对讲停止
        public const int MINOR_WIN_TOP = 0x290; //电视墙窗口置顶
        public const int MINOR_WIN_BOTTOM = 0x291; //电视墙窗口置底

        // Netra 2.2.2
        public const int MINOR_LOCAL_LOAD_HDISK = 0x300; // 本地加载硬盘 
        public const int MINOR_LOCAL_DELETE_HDISK = 0x301; // 本地删除异常不存在的硬盘

        //Netra3.1.0
        public const int MINOR_LOCAL_CFG_DEVICE_TYPE = 0x310; //本地配置设备类型
        public const int MINOR_REMOTE_CFG_DEVICE_TYPE = 0x311; //远程配置设备类型
        public const int MINOR_LOCAL_CFG_WORK_HOT_SERVER = 0x312; //本地配置工作机热备服务器
        public const int MINOR_REMOTE_CFG_WORK_HOT_SERVER = 0x313; //远程配置工作机热备服务器
        public const int MINOR_LOCAL_DELETE_WORK = 0x314; //本地删除工作机
        public const int MINOR_REMOTE_DELETE_WORK = 0x315; //远程删除工作机
        public const int MINOR_LOCAL_ADD_WORK = 0x316; //本地添加工作机
        public const int MINOR_REMOTE_ADD_WORK = 0x317; //远程添加工作机
        public const int MINOR_LOCAL_IPCHEATMAP_OUTPUT = 0x318; /* 本地导出热度图文件      */
        public const int MINOR_LOCAL_IPCHEATFLOW_OUTPUT = 0x319; /* 本地导出热度流量文件      */
        public const int MINOR_REMOTE_SMS_SEND = 0x350; /*远程发送短信*/
        public const int MINOR_LOCAL_SMS_SEND = 0x351; /*本地发送短信*/
        public const int MINOR_ALARM_SMS_SEND = 0x352; /*发送短信报警*/
        public const int MINOR_SMS_RECV = 0x353; /*接收短信*/
        //（备注：0x350、0x351是指人工在GUI或IE控件上编辑并发送短信）
        public const int MINOR_LOCAL_SMS_SEARCH = 0x354; /*本地搜索短信*/
        public const int MINOR_REMOTE_SMS_SEARCH = 0x355; /*远程搜索短信*/
        public const int MINOR_LOCAL_SMS_READ = 0x356; /*本地查看短信*/
        public const int MINOR_REMOTE_SMS_READ = 0x357; /*远程查看短信*/
        public const int MINOR_REMOTE_DIAL_CONNECT = 0x358; /*远程开启手动拨号*/
        public const int MINOR_REMOTE_DIAL_DISCONN = 0x359; /*远程停止手动拨号*/
        public const int MINOR_LOCAL_ALLOWLIST_SET = 0x35A; /*本地配置白名单*/
        public const int MINOR_REMOTE_ALLOWLIST_SET = 0x35B; /*远程配置白名单*/
        public const int MINOR_LOCAL_DIAL_PARA_SET = 0x35C; /*本地配置拨号参数*/
        public const int MINOR_REMOTE_DIAL_PARA_SET = 0x35D; /*远程配置拨号参数*/
        public const int MINOR_LOCAL_DIAL_SCHEDULE_SET = 0x35E; /*本地配置拨号计划*/
        public const int MINOR_REMOTE_DIAL_SCHEDULE_SET = 0x35F; /*远程配置拨号计划*/
        public const int MINOR_PLAT_OPER = 0x36; /* 平台操作*/

        public const int MINOR_REMOTE_OPEN_DOOR = 0x400; // 远程开门 
        public const int MINOR_REMOTE_CLOSE_DOOR = 0x401; // 远程关门 
        public const int MINOR_REMOTE_ALWAYS_OPEN = 0x402; // 远程常开 
        public const int MINOR_REMOTE_ALWAYS_CLOSE = 0x403; // 远程常关 
        public const int MINOR_REMOTE_CHECK_TIME = 0x404; // 远程手动校时 
        public const int MINOR_NTP_CHECK_TIME = 0x405; // NTP自动校时 
        public const int MINOR_REMOTE_CLEAR_CARD = 0x406; // 远程清空卡号 
        public const int MINOR_REMOTE_RESTORE_CFG = 0x407; // 远程恢复默认参数 
        public const int MINOR_ALARMIN_ARM = 0x408; // 防区布防 
        public const int MINOR_ALARMIN_DISARM = 0x409; // 防区撤防 
        public const int MINOR_LOCAL_RESTORE_CFG = 0x40a; // 本地恢复默认参数 
        public const int MINOR_REMOTE_CAPTURE_PIC = 0x40b; //远程抓拍
        public const int MINOR_MOD_NET_REPORT_CFG = 0x40c; //修改网络中心参数配置
        public const int MINOR_MOD_GPRS_REPORT_PARAM = 0x40d; //修改GPRS中心参数配置
        public const int MINOR_MOD_REPORT_GROUP_PARAM = 0x40e; //修改中心组参数配置
        public const int MINOR_UNLOCK_PASSWORD_OPEN_DOOR = 0x40f; //解除码输入

        public const int MINOR_SET_TRIGGERMODE_CFG = 0x1001; // 设置触发模式参数 
        public const int MINOR_GET_TRIGGERMODE_CFG = 0x1002; // 获取触发模式参数 
        public const int MINOR_SET_IOOUT_CFG = 0x1003; // 设置IO输出参数 
        public const int MINOR_GET_IOOUT_CFG = 0x1004; // 获取IO输出参数 
        public const int MINOR_GET_TRIGGERMODE_DEFAULT = 0x1005; // 获取触发模式推荐参数 
        public const int MINOR_GET_ITCSTATUS = 0x1006; // 获取状态检测参数 
        public const int MINOR_SET_STATUS_DETECT_CFG = 0x1007; // 设置状态检测参数 
        public const int MINOR_GET_STATUS_DETECT_CFG = 0x1008; // 获取状态检测参数 
        public const int MINOR_GET_VIDEO_TRIGGERMODE_CFG = 0x1009; // 获取视频电警模式参数 
        public const int MINOR_SET_VIDEO_TRIGGERMODE_CFG = 0x100a; // 设置视频电警模式参数 

        public const int MINOR_LOCAL_ADD_CAR_INFO = 0x2001; // 本地添加车辆信息 
        public const int MINOR_LOCAL_MOD_CAR_INFO = 0x2002; // 本地修改车辆信息 
        public const int MINOR_LOCAL_DEL_CAR_INFO = 0x2003; // 本地删除车辆信息 
        public const int MINOR_LOCAL_FIND_CAR_INFO = 0x2004; // 本地查找车辆信息 
        public const int MINOR_LOCAL_ADD_MONITOR_INFO = 0x2005; // 本地添加布控信息 
        public const int MINOR_LOCAL_MOD_MONITOR_INFO = 0x2006; // 本地修改布控信息 
        public const int MINOR_LOCAL_DEL_MONITOR_INFO = 0x2007; // 本地删除布控信息 
        public const int MINOR_LOCAL_FIND_MONITOR_INFO = 0x2008; // 本地查询布控信息 
        public const int MINOR_LOCAL_FIND_NORMAL_PASS_INFO = 0x2009; // 本地查询正常通行信息 
        public const int MINOR_LOCAL_FIND_ABNORMAL_PASS_INFO = 0x200a; // 本地查询异常通行信息 
        public const int MINOR_LOCAL_FIND_PEDESTRIAN_PASS_INFO = 0x200b; // 本地查询正常通行信息 
        public const int MINOR_LOCAL_PIC_PREVIEW = 0x200c; // 本地图片预览 
        public const int MINOR_LOCAL_SET_GATE_PARM_CFG = 0x200d; // 设置本地配置出入口参数 
        public const int MINOR_LOCAL_GET_GATE_PARM_CFG = 0x200e; // 获取本地配置出入口参数 
        public const int MINOR_LOCAL_SET_DATAUPLOAD_PARM_CFG = 0x200f; // 设置本地配置数据上传参数 
        public const int MINOR_LOCAL_GET_DATAUPLOAD_PARM_CFG = 0x2010; // 获取本地配置数据上传参数 

        public const int MINOR_LOCAL_DEVICE_CONTROL = 0x2011; // 本地设备控制(本地开关闸) 
        public const int MINOR_LOCAL_ADD_EXTERNAL_DEVICE_INFO = 0x2012; // 本地添加外接设备信息 
        public const int MINOR_LOCAL_MOD_EXTERNAL_DEVICE_INFO = 0x2013; // 本地修改外接设备信息 
        public const int MINOR_LOCAL_DEL_EXTERNAL_DEVICE_INFO = 0x2014; // 本地删除外接设备信息 
        public const int MINOR_LOCAL_FIND_EXTERNAL_DEVICE_INFO = 0x2015; // 本地查询外接设备信息 
        public const int MINOR_LOCAL_ADD_CHARGE_RULE = 0x2016; // 本地添加收费规则 
        public const int MINOR_LOCAL_MOD_CHARGE_RULE = 0x2017; // 本地修改收费规则 
        public const int MINOR_LOCAL_DEL_CHARGE_RULE = 0x2018; // 本地删除收费规则 
        public const int MINOR_LOCAL_FIND_CHARGE_RULE = 0x2019; // 本地查询收费规则 
        public const int MINOR_LOCAL_COUNT_NORMAL_CURRENTINFO = 0x2020; // 本地统计正常通行信息 
        public const int MINOR_LOCAL_EXPORT_NORMAL_CURRENTINFO_REPORT = 0x2021; // 本地导出正常通行信息统计报表 
        public const int MINOR_LOCAL_COUNT_ABNORMAL_CURRENTINFO = 0x2022; // 本地统计异常通行信息 
        public const int MINOR_LOCAL_EXPORT_ABNORMAL_CURRENTINFO_REPORT = 0x2023; // 本地导出异常通行信息统计报表 
        public const int MINOR_LOCAL_COUNT_PEDESTRIAN_CURRENTINFO = 0x2024; // 本地统计行人通行信息 
        public const int MINOR_LOCAL_EXPORT_PEDESTRIAN_CURRENTINFO_REPORT = 0x2025; // 本地导出行人通行信息统计报表 
        public const int MINOR_LOCAL_FIND_CAR_CHARGEINFO = 0x2026; // 本地查询过车收费信息 
        public const int MINOR_LOCAL_COUNT_CAR_CHARGEINFO = 0x2027; // 本地统计过车收费信息 
        public const int MINOR_LOCAL_EXPORT_CAR_CHARGEINFO_REPORT = 0x2028; // 本地导出过车收费信息统计报表 
        public const int MINOR_LOCAL_FIND_SHIFTINFO = 0x2029; // 本地查询交接班信息 
        public const int MINOR_LOCAL_FIND_CARDINFO = 0x2030; // 本地查询卡片信息 
        public const int MINOR_LOCAL_ADD_RELIEF_RULE = 0x2031; // 本地添加减免规则 
        public const int MINOR_LOCAL_MOD_RELIEF_RULE = 0x2032; // 本地修改减免规则 
        public const int MINOR_LOCAL_DEL_RELIEF_RULE = 0x2033; // 本地删除减免规则 
        public const int MINOR_LOCAL_FIND_RELIEF_RULE = 0x2034; // 本地查询减免规则 
        public const int MINOR_LOCAL_GET_ENDETCFG = 0x2035; // 本地获取出入口控制机离线检测配置 
        public const int MINOR_LOCAL_SET_ENDETCFG = 0x2036; // 本地设置出入口控制机离线检测配置 
        public const int MINOR_LOCAL_SET_ENDEV_ISSUEDDATA = 0x2037; // 本地设置出入口控制机下发卡片信息 
        public const int MINOR_LOCAL_DEL_ENDEV_ISSUEDDATA = 0x2038; // 本地清空出入口控制机下发卡片信息 
        public const int MINOR_REMOTE_DEVICE_CONTROL = 0x2101; // 远程设备控制 
        public const int MINOR_REMOTE_SET_GATE_PARM_CFG = 0x2102; // 设置远程配置出入口参数 
        public const int MINOR_REMOTE_GET_GATE_PARM_CFG = 0x2103; // 获取远程配置出入口参数 
        public const int MINOR_REMOTE_SET_DATAUPLOAD_PARM_CFG = 0x2104; // 设置远程配置数据上传参数 
        public const int MINOR_REMOTE_GET_DATAUPLOAD_PARM_CFG = 0x2105; // 获取远程配置数据上传参数 
        public const int MINOR_REMOTE_GET_BASE_INFO = 0x2106; // 远程获取终端基本信息 
        public const int MINOR_REMOTE_GET_OVERLAP_CFG = 0x2107; // 远程获取字符叠加参数配置 
        public const int MINOR_REMOTE_SET_OVERLAP_CFG = 0x2108; // 远程设置字符叠加参数配置 
        public const int MINOR_REMOTE_GET_ROAD_INFO = 0x2109; // 远程获取路口信息 
        public const int MINOR_REMOTE_START_TRANSCHAN = 0x210a; // 远程建立同步数据服务器 
        public const int MINOR_REMOTE_GET_ECTWORKSTATE = 0x210b; // 远程获取出入口终端工作状态 
        public const int MINOR_REMOTE_GET_ECTCHANINFO = 0x210c; // 远程获取出入口终端通道状态 
        public const int MINOR_REMOTE_ADD_EXTERNAL_DEVICE_INFO = 0x210d; // 远程添加外接设备信息 
        public const int MINOR_REMOTE_MOD_EXTERNAL_DEVICE_INFO = 0x210e; // 远程修改外接设备信息 
        public const int MINOR_REMOTE_GET_ENDETCFG = 0x210f; // 远程获取出入口控制机离线检测配置 
        public const int MINOR_REMOTE_SET_ENDETCFG = 0x2110; // 远程设置出入口控制机离线检测配置 
        public const int MINOR_REMOTE_ENDEV_ISSUEDDATA = 0x2111; // 远程设置出入口控制机下发卡片信息 
        public const int MINOR_REMOTE_DEL_ENDEV_ISSUEDDATA = 0x2112; // 远程清空出入口控制机下发卡片信息 

        public const int MINOR_REMOTE_ON_CTRL_LAMP = 0x2115; // 开启远程控制车位指示灯 
        public const int MINOR_REMOTE_OFF_CTRL_LAMP = 0x2116; // 关闭远程控制车位指示灯 
        public const int MINOR_SET_VOICE_LEVEL_PARAM = 0x2117; // 设置音量大小 
        public const int MINOR_SET_VOICE_INTERCOM_PARAM = 0x2118; // 设置音量录音 
        public const int MINOR_SET_INTELLIGENT_PARAM = 0x2119; // 智能配置 
        public const int MINOR_LOCAL_SET_RAID_SPEED = 0x211a; // 本地设置raid速度 
        public const int MINOR_REMOTE_SET_RAID_SPEED = 0x211b; // 远程设置raid速度 
        public const int MINOR_REMOTE_CREATE_STORAGE_POOL = 0x211c; // 远程添加存储池 
        public const int MINOR_REMOTE_DEL_STORAGE_POOL = 0x211d; // 远程删除存储池 

        public const int MINOR_REMOTE_DEL_PIC = 0x2120; // 远程删除图片数据 
        public const int MINOR_REMOTE_DEL_RECORD = 0x2121; // 远程删除录像数据 
        public const int MINOR_REMOTE_CLOUD_ENABLE = 0x2123; // 远程设置云存储启用 
        public const int MINOR_REMOTE_CLOUD_DISABLE = 0x2124; // 远程设置云存储禁用 
        public const int MINOR_REMOTE_CLOUD_MODIFY_PARAM = 0x2125; // 远程修改云存储池参数 
        public const int MINOR_REMOTE_CLOUD_MODIFY_VOLUME = 0x2126; // 远程修改云存储池容量 
        public const int MINOR_REMOTE_GET_GB28181_SERVICE_PARAM = 0x2127; //远程获取GB28181服务参数
        public const int MINOR_REMOTE_SET_GB28181_SERVICE_PARAM = 0x2128; //远程设置GB28181服务参数
        public const int MINOR_LOCAL_GET_GB28181_SERVICE_PARAM = 0x2129; //本地获取GB28181服务参数
        public const int MINOR_LOCAL_SET_GB28181_SERVICE_PARAM = 0x212a; //本地配置B28181服务参数
        public const int MINOR_REMOTE_SET_SIP_SERVER = 0x212b; //远程配置SIP SERVER
        public const int MINOR_LOCAL_SET_SIP_SERVER = 0x212c; //本地配置SIP SERVER
        public const int MINOR_LOCAL_BLOCKALLOWFILE_OUTPUT = 0x212d; 
        public const int MINOR_LOCAL_BLOCKALLOWFILE_INPUT = 0x212e; 
        public const int MINOR_REMOTE_BALCKALLOWCFGFILE_OUTPUT = 0x212f; 
        public const int MINOR_REMOTE_BALCKALLOWCFGFILE_INPUT = 0x2130; 

        public const int MINOR_REMOTE_CREATE_MOD_VIEWLIB_SPACE = 0x2200; // 远程创建/修改视图库空间 
        public const int MINOR_REMOTE_DELETE_VIEWLIB_FILE = 0x2201; // 远程删除视图库文件 
        public const int MINOR_REMOTE_DOWNLOAD_VIEWLIB_FILE = 0x2202; // 远程下载视图库文件 
        public const int MINOR_REMOTE_UPLOAD_VIEWLIB_FILE = 0x2203; // 远程上传视图库文件 
        public const int MINOR_LOCAL_CREATE_MOD_VIEWLIB_SPACE = 0x2204; // 本地创建/修改视图库空间 

        public const int MINOR_LOCAL_SET_DEVICE_ACTIVE = 0x3000; //本地激活设备
        public const int MINOR_REMOTE_SET_DEVICE_ACTIVE = 0x3001; //远程激活设备
        public const int MINOR_LOCAL_PARA_FACTORY_DEFAULT = 0x3002; //本地回复出厂设置
        public const int MINOR_REMOTE_PARA_FACTORY_DEFAULT = 0x3003; //远程恢复出厂设置

        // 测温命令添加
        public const int NET_DVR_GET_THERMOMETRY_BASICPARAM_CAPABILITIES = 3620; //获取测温配置能力
        public const int NET_DVR_GET_THERMOMETRY_BASICPARAM = 3621;    //获取测温配置参数
        public const int NET_DVR_SET_THERMOMETRY_BASICPARAM = 3622;    //设置测温配置参数
        public const int NET_DVR_GET_THERMOMETRY_SCENE_CAPABILITIES = 3623; //获取测温预置点关联配置能力
        public const int NET_DVR_GET_THERMOMETRY_PRESETINFO = 3624;    //获取测温预置点关联配置参数
        public const int NET_DVR_SET_THERMOMETRY_PRESETINFO = 3625;    //设置测温预置点关联配置参数
        public const int NET_DVR_GET_THERMOMETRY_ALARMRULE_CAPABILITIES = 3626;//获取测温报警方式配置能力
        public const int NET_DVR_GET_THERMOMETRY_ALARMRULE = 3627;    //获取测温预置点报警规则配置参数
        public const int NET_DVR_SET_THERMOMETRY_ALARMRULE = 3628;    //设置测温预置点报警规则配置参数
        public const int NET_DVR_GET_REALTIME_THERMOMETRY = 3629;    //实时温度检测
        public const int NET_DVR_GET_THERMOMETRY_DIFFCOMPARISON = 3630;    //获取测温预置点温差规则配置参数
        public const int NET_DVR_SET_THERMOMETRY_DIFFCOMPARISON = 3631;    //设置测温预置点温差规则配置参数
        public const int NET_DVR_GET_THERMOMETRY_TRIGGER = 3632;    //获取测温联动配置
        public const int NET_DVR_SET_THERMOMETRY_TRIGGER = 3633;    //设置测温联动配置

        public const int NET_DVR_GET_THERMAL_CAPABILITIES = 3634;    //获取热成像（Thermal）能力
        public const int NET_DVR_GET_FIREDETECTION_CAPABILITIES = 3635;    //获取火点检测配置能力
        public const int NET_DVR_GET_FIREDETECTION = 3636;    //获取火点检测参数
        public const int NET_DVR_SET_FIREDETECTION = 3637;    //设置火点检测参数
        public const int NET_DVR_GET_FIREDETECTION_TRIGGER = 3638;  //获取火点检测联动配置
        public const int NET_DVR_SET_FIREDETECTION_TRIGGER = 3639;    //设置火点检测联动配置
        public const int NET_DVR_GET_THERMOMETRY_SCHEDULE_CAPABILITIES = 6750;     //获取测温检测布防时间能力
        public const int NET_DVR_GET_THERMOMETRY_SCHEDULE = 6751;     //获取测温检测布防时间配置
        public const int NET_DVR_SET_THERMOMETRY_SCHEDULE = 6752;     //设置测温检测布防时间配置
        public const int NET_DVR_GET_TEMPERTURE_SCHEDULE_CAPABILITIES = 6753;     //获取温差布防时间能力
        public const int NET_DVR_GET_TEMPERTURE_SCHEDULE = 6754;     //获取温差布防时间配置
        public const int NET_DVR_SET_TEMPERTURE_SCHEDULE = 6755;     //设置温差布防时间配置
        public const int NET_DVR_GET_SEARCH_LOG_CAPABILITIES = 6756;  //日志类型支持能力
        public const int NET_DVR_GET_VEHICLEFLOW = 6758;    //获取车流量数据
        public const int NET_DVR_GET_IPADDR_FILTERCFG_V50 = 6759;     //获取IP地址过滤参数扩展
        public const int NET_DVR_SET_IPADDR_FILTERCFG_V50 = 6760;     //设置IP地址过滤参数扩展
        public const int NET_DVR_GET_TEMPHUMSENSOR_CAPABILITIES = 6761;     //获取温湿度传感器的能力
        public const int NET_DVR_GET_TEMPHUMSENSOR = 6762;     //获取温湿度传感器配置协议
        public const int NET_DVR_SET_TEMPHUMSENSOR = 6763;     //设置温湿度传感器配置协议

        public const int NET_DVR_GET_THERMOMETRY_MODE_CAPABILITIES = 6764; //获取测温模式能力
        public const int NET_DVR_GET_THERMOMETRY_MODE = 6765; //获取测温模式参数
        public const int NET_DVR_SET_THERMOMETRY_MODE = 6766;  //设置测温模式参数

        public const int NET_DVR_GET_THERMAL_PIP_CAPABILITIES = 6767;    //获取热成像画中画配置能力
        public const int NET_DVR_GET_THERMAL_PIP = 6768;    //获取热成像画中画配置参数
        public const int NET_DVR_SET_THERMAL_PIP = 6769;    //设置热成像画中画配置参数
        public const int NET_DVR_GET_THERMAL_INTELRULEDISPLAY_CAPABILITIES = 6770;    //获取热成像智能规则显示能力
        public const int NET_DVR_GET_THERMAL_INTELRULE_DISPLAY = 6771;    //获取热成像智能规则显示参数
        public const int NET_DVR_SET_THERMAL_INTELRULE_DISPLAY = 6772;    //设置热成像智能规则显示参数
        public const int NET_DVR_GET_THERMAL_ALGVERSION = 6773;   //获取热成像相关算法库版本
        public const int NET_DVR_GET_CURRENT_LOCK_CAPABILITIES = 6774;    //获取电流锁定配置能力
        public const int NET_DVR_GET_CURRENT_LOCK = 6775;    //获取电流锁定配置参数
        public const int NET_DVR_SET_CURRENT_LOCK = 6776;    //设置电流锁定配置参数

        public const int NET_DVR_SET_MANUALTHERM_BASICPARAM = 6716;  //设置手动测温基本参数
        public const int NET_DVR_GET_MANUALTHERM_BASICPARAM = 6717;  //获取手动测温基本参数

        public const int NET_DVR_GET_MANUALTHERM_INFO = 6706;   //手动测温实时获取
        public const int NET_DVR_SET_MANUALTHERM = 6708; //设置手动测温数据设置
        public const int NET_DVR_DEL_MANUALTHERM_RULE = 6778;     //删除手动测温规则

        public const int NET_DVR_GET_THERMOMETRYRULE_TEMPERATURE_INFO = 23001;    //手动获取测温规则温度信息

        public const int  NET_DVR_GET_BAREDATAOVERLAY_CAPABILITIES  = 6660;   //获取裸数据叠加能力
        public const int   NET_DVR_SET_BAREDATAOVERLAY_CFG  =  6661;    //设置裸数据叠加
        public const int NET_DVR_GET_BAREDATAOVERLAY_CFG = 6662;    //获取裸数据叠加

        /********************************智能人脸识别****************************/
        public const int NET_DVR_GET_FACESNAPCFG = 5001;//获取人脸抓拍参数
        public const int NET_DVR_SET_FACESNAPCFG = 5002;//设置人脸抓拍参数
        public const int NET_DVR_GET_DEVACCESS_CFG = 5005;//获取接入设备参数
        public const int NET_DVR_SET_DEVACCESS_CFG = 5006;//设置接入设备参数
        public const int NET_DVR_GET_SAVE_PATH_CFG = 5007;//获取存储信息参数
        public const int NET_DVR_SET_SAVE_PATH_CFG = 5008;//设置存储信息参数
        public const int NET_VCA_GET_RULECFG_V41 = 5011;//获取行为分析参数(扩展)
        public const int NET_VCA_SET_RULECFG_V41 = 5012;//设置行为分析参数(扩展)
        public const int NET_DVR_GET_AID_RULECFG_V41 = 5013;//获取交通事件规则参数
        public const int NET_DVR_SET_AID_RULECFG_V41 = 5014;//设置交通事件规则参数
        public const int NET_DVR_GET_TPS_RULECFG_V41 = 5015;//获取交通统计规则参数(扩展)
        public const int NET_DVR_SET_TPS_RULECFG_V41 = 5016;//设置交通统计规则参数(扩展)
        public const int NET_VCA_GET_FACEDETECT_RULECFG_V41 = 5017;//获取ATM人脸检测规则(扩展) 
        public const int NET_VCA_SET_FACEDETECT_RULECFG_V41 = 5018;//设置ATM人脸检测规则(扩展)
        public const int NET_DVR_GET_PDC_RULECFG_V41 = 5019;//设置人流量统计规则(扩展)
        public const int NET_DVR_SET_PDC_RULECFG_V41 = 5020;//获取人流量统计规则(扩展)
        public const int NET_DVR_GET_TRIAL_VERSION_CFG = 5021;//获取试用版信息
        public const int NET_DVR_GET_VCA_CTRLINFO_CFG = 5022;//批量获取智能控制参数
        public const int NET_DVR_SET_VCA_CTRLINFO_CFG = 5023;//批量设置智能控制参数
        public const int NET_DVR_SYN_CHANNEL_NAME = 5024;//同步通道名
        public const int NET_DVR_GET_RESET_COUNTER = 5025;//获取统计数据清零参数（人流量、交通统计）
        public const int NET_DVR_SET_RESET_COUNTER = 5026;//设置统计数据清零参数（人流量、交通统计）
        public const int NET_DVR_GET_OBJECT_COLOR = 5027;//获取物体颜色属性
        public const int NET_DVR_SET_OBJECT_COLOR = 5028;//设置物体颜色属性
        public const int NET_DVR_GET_AUX_AREA = 5029;//获取辅助区域
        public const int NET_DVR_SET_AUX_AREA = 5030;//设置辅助区域
        public const int NET_DVR_GET_CHAN_WORKMODE = 5031;//获取通道工作模式
        public const int NET_DVR_SET_CHAN_WORKMODE = 5032;//设置通道工作模式
        public const int NET_DVR_GET_SLAVE_CHANNEL = 5033;//获取从通道参数
        public const int NET_DVR_SET_SLAVE_CHANNEL = 5034;//设置从通道参数
        public const int NET_DVR_GET_VQD_EVENT_RULE = 5035;//获取视频质量诊断事件规则
        public const int NET_DVR_SET_VQD_EVENT_RULE = 5036;//设置视频质量诊断事件规则
        public const int NET_DVR_GET_BASELINE_SCENE = 5037;//获取基准场景参数
        public const int NET_DVR_SET_BASELINE_SCENE = 5038;//设置基准场景参数
        public const int NET_DVR_CONTROL_BASELINE_SCENE = 5039;//基准场景操作
        public const int NET_DVR_SET_VCA_DETION_CFG = 5040;//设置智能移动参数配置
        public const int NET_DVR_GET_VCA_DETION_CFG = 5041;//获取智能移动参数配置
        public const int NET_DVR_GET_STREAM_ATTACHINFO_CFG = 5042;//获取码流附加信息配置
        public const int NET_DVR_SET_STREAM_ATTACHINFO_CFG = 5043;//设置码流附加信息配置

        public const int NET_DVR_GET_BV_CALIB_TYPE = 5044;//获取双目标定类型
        public const int NET_DVR_CONTROL_BV_SAMPLE_CALIB = 5045;//双目样本标定
        public const int NET_DVR_GET_BV_SAMPLE_CALIB_CFG = 5046;//获取双目标定参数
        public const int NET_DVR_GET_RULECFG_V42 = 5049;//获取行为分析参数(支持16条规则扩展)
        public const int NET_DVR_SET_RULECFG_V42 = 5050;//设置行为分析参数(支持16条规则扩展)
        public const int NET_DVR_SET_VCA_DETION_CFG_V40 = 5051;//设置智能移动参数配置
        public const int NET_DVR_GET_VCA_DETION_CFG_V40 = 5052;//获取智能移动参数配置
        public const int NET_DVR_SET_FLASH_CFG = 5110;//写入数据到Flash 测试使用
        /********************************智能人脸识别 end****************************/


        /*日志附加信息*/
        //主类型
        public const int MAJOR_INFORMATION = 4;/*附加信息*/
        //次类型
        public const int MINOR_HDD_INFO = 0xa1; // 硬盘信息 
        public const int MINOR_SMART_INFO = 0xa2; // S.M.A.R.T信息 
        public const int MINOR_REC_START = 0xa3; // 开始录像 
        public const int MINOR_REC_STOP = 0xa4; // 停止录像 
        public const int MINOR_REC_OVERDUE = 0xa5; // 过期录像删除 
        public const int MINOR_LINK_START = 0xa6; // 连接前端设备 
        public const int MINOR_LINK_STOP = 0xa7; // 断开前端设备 
        public const int MINOR_NET_DISK_INFO = 0xa8; // 网络硬盘信息 
        public const int MINOR_RAID_INFO = 0xa9; // raid相关信息 
        public const int MINOR_RUN_STATUS_INFO = 0xaa; // 系统运行状态信息 
        public const int MINOR_SPARE_START_BACKUP = 0xab; // 热备系统开始备份指定工作机 
        public const int MINOR_SPARE_STOP_BACKUP = 0xac; // 热备系统停止备份指定工作机 
        public const int MINOR_SPARE_CLIENT_INFO = 0xad; // 热备客户机信息 
        public const int MINOR_ANR_RECORD_START = 0xae; // ANR录像开始 
        public const int MINOR_ANR_RECORD_END = 0xaf; // ANR录像结束 
        public const int MINOR_ANR_ADD_TIME_QUANTUM = 0xb0; // ANR添加时间段 
        public const int MINOR_ANR_DEL_TIME_QUANTUM = 0xb1; // ANR删除时间段 
        public const int MINOR_PIC_REC_START = 0xb3; // 开始抓图 
        public const int MINOR_PIC_REC_STOP = 0xb4; // 停止抓图 
        public const int MINOR_PIC_REC_OVERDUE = 0xb5; //过期图片文件删除 
        public const int MINOR_CLIENT_LOGIN = 0xb6; // 登录服务器成功 
        public const int MINOR_CLIENT_RELOGIN = 0xb7; // 重新登录服务器 
        public const int MINOR_CLIENT_LOGOUT = 0xb8; // 退出服务器成功 
        public const int MINOR_CLIENT_SYNC_START = 0xb9; // 录像同步开始 
        public const int MINOR_CLIENT_SYNC_STOP = 0xba; // 录像同步终止 
        public const int MINOR_CLIENT_SYNC_SUCC = 0xbb; // 录像同步成功 
        public const int MINOR_CLIENT_SYNC_EXCP = 0xbc; // 录像同步异常 
        public const int MINOR_GLOBAL_RECORD_ERR_INFO = 0xbd; // 全局错误记录信息 
        public const int MINOR_BUFFER_STATE = 0xbe; // 缓冲区状态日志记录 
        public const int MINOR_DISK_ERRORINFO_V2 = 0xbf; // 硬盘错误详细信息V2 
        public const int MINOR_UNLOCK_RECORD = 0xc3; // 开锁记录 
        public const int MINOR_VIS_ALARM = 0xc4; // 防区报警 
        public const int MINOR_TALK_RECORD = 0xc5; // 通话记录 

        /*日志附加信息*/
        //主类型
        public const int MAJOR_EVENT = 0x5;/*事件*/
        //次类型
        public const int MINOR_LEGAL_CARD_PASS = 0x01; // 合法卡认证通过 
        public const int MINOR_CARD_AND_PSW_PASS = 0x02; // 刷卡加密码认证通过 
        public const int MINOR_CARD_AND_PSW_FAIL = 0x03; // 刷卡加密码认证失败 
        public const int MINOR_CARD_AND_PSW_TIMEOUT = 0x04; // 数卡加密码认证超时 
        public const int MINOR_CARD_AND_PSW_OVER_TIME = 0x05; // 刷卡加密码超次 
        public const int MINOR_CARD_NO_RIGHT = 0x06; // 未分配权限 
        public const int MINOR_CARD_INVALID_PERIOD = 0x07; // 无效时段 
        public const int MINOR_CARD_OUT_OF_DATE = 0x08; // 卡号过期 
        public const int MINOR_INVALID_CARD = 0x09; // 无此卡号 
        public const int MINOR_ANTI_SNEAK_FAIL = 0x0a; // 反潜回认证失败 
        public const int MINOR_INTERLOCK_DOOR_NOT_CLOSE = 0x0b; // 互锁门未关闭 
        public const int MINOR_NOT_BELONG_MULTI_GROUP = 0x0c; // 卡不属于多重认证群组 
        public const int MINOR_INVALID_MULTI_VERIFY_PERIOD = 0x0d; // 卡不在多重认证时间段内 
        public const int MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL = 0x0e; // 多重认证模式超级权限认证失败 
        public const int MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL = 0x0f; // 多重认证模式远程认证失败 
        public const int MINOR_MULTI_VERIFY_SUCCESS = 0x10; // 多重认证成功 
        public const int MINOR_LEADER_CARD_OPEN_BEGIN = 0x11; // 首卡开门开始 
        public const int MINOR_LEADER_CARD_OPEN_END = 0x12; // 首卡开门结束 
        public const int MINOR_ALWAYS_OPEN_BEGIN = 0x13; // 常开状态开始 
        public const int MINOR_ALWAYS_OPEN_END = 0x14; // 常开状态结束 
        public const int MINOR_LOCK_OPEN = 0x15; // 门锁打开 
        public const int MINOR_LOCK_CLOSE = 0x16; // 门锁关闭 
        public const int MINOR_DOOR_BUTTON_PRESS = 0x17; // 开门按钮打开 
        public const int MINOR_DOOR_BUTTON_RELEASE = 0x18; // 开门按钮放开 
        public const int MINOR_DOOR_OPEN_NORMAL = 0x19; // 正常开门（门磁） 
        public const int MINOR_DOOR_CLOSE_NORMAL = 0x1a; // 正常关门（门磁） 
        public const int MINOR_DOOR_OPEN_ABNORMAL = 0x1b; // 门异常打开（门磁） 
        public const int MINOR_DOOR_OPEN_TIMEOUT = 0x1c; // 门打开超时（门磁）  
        public const int MINOR_ALARMOUT_ON = 0x1d; // 报警输出打开 
        public const int MINOR_ALARMOUT_OFF = 0x1e; // 报警输出关闭 
        public const int MINOR_ALWAYS_CLOSE_BEGIN = 0x1f; // 常关状态开始 
        public const int MINOR_ALWAYS_CLOSE_END = 0x20; // 常关状态结束 
        public const int MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN = 0x21; // 多重多重认证需要远程开门 
        public const int MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS = 0x22; // 多重认证超级密码认证成功事件 
        public const int MINOR_MULTI_VERIFY_REPEAT_VERIFY = 0x23; // 多重认证重复认证事件 
        public const int MINOR_MULTI_VERIFY_TIMEOUT = 0x24; // 多重认证重复认证事件 
        public const int MINOR_DOORBELL_RINGING = 0x25; // 门铃响
        public const int MINOR_FINGERPRINT_COMPARE_PASS = 0x26; // 指纹比对通过
        public const int MINOR_FINGERPRINT_COMPARE_FAIL = 0x27; // 指纹比对失败
        public const int MINOR_CARD_FINGERPRINT_VERIFY_PASS = 0x28; // 刷卡加指纹认证通过
        public const int MINOR_CARD_FINGERPRINT_VERIFY_FAIL = 0x29; // 刷卡加指纹认证失败
        public const int MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT = 0x2a; // 刷卡加指纹认证超时
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2b; // 刷卡加指纹加密码认证通过
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2c; // 刷卡加指纹加密码认证失败
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x2d; // 刷卡加指纹加密码认证超时
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2e; // 指纹加密码认证通过
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2f; // 指纹加密码认证失败
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x30; // 指纹加密码认证超时
        public const int MINOR_FINGERPRINT_INEXISTENCE = 0x31; // 指纹不存在

        //当日志的主类型为MAJOR_OPERATION=03，次类型为MINOR_LOCAL_CFG_PARM=0x52或者MINOR_REMOTE_GET_PARM=0x76或者MINOR_REMOTE_CFG_PARM=0x77时，dwParaType:参数类型有效，其含义如下：
        public const int PARA_VIDEOOUT = 1;
        public const int PARA_IMAGE = 2;
        public const int PARA_ENCODE = 4;
        public const int PARA_NETWORK = 8;
        public const int PARA_ALARM = 16;
        public const int PARA_EXCEPTION = 32;
        public const int PARA_DECODER = 64;/*解码器*/
        public const int PARA_RS232 = 128;
        public const int PARA_PREVIEW = 256;
        public const int PARA_SECURITY = 512;
        public const int PARA_DATETIME = 1024;
        public const int PARA_FRAMETYPE = 2048;/*帧格式*/

        //vca
        //        public const int PARA_VCA_RULE = 4096;//行为规则

        public const int PARA_DETECTION = 0x1000;   //侦测配置
        public const int PARA_VCA_RULE = 0x1001;  //行为规则 
        public const int PARA_VCA_CTRL = 0x1002;  //配置智能控制信息
        public const int PARA_VCA_PLATE = 0x1003; // 车牌识别

        public const int PARA_CODESPLITTER = 0x2000; /*码分器参数*/
        //2010-01-22 增加视频综合平台日志信息次类型
        public const int PARA_RS485 = 0x2001; /* RS485配置信息*/
        public const int PARA_DEVICE = 0x2002; /* 设备配置信息*/
        public const int PARA_HARDDISK = 0x2003; /* 硬盘配置信息 */
        public const int PARA_AUTOBOOT = 0x2004; /* 自动重启配置信息*/
        public const int PARA_HOLIDAY = 0x2005; /* 节假日配置信息*/
        public const int PARA_IPC = 0x2006; /* IP通道配置 */




        //日志信息(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG_V30
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//主类型 1-报警; 2-异常; 3-操作; 0xff-全部
            public uint dwMinorType;//次类型 0-全部;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//操作面板的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//网络操作的用户名
            public NET_DVR_IPADDR struRemoteHostAddr;//远程主机地址
            public uint dwParaType;//参数类型
            public uint dwChannel;//通道号
            public uint dwDiskNumber;//硬盘号
            public uint dwAlarmInPort;//报警输入端口
            public uint dwAlarmOutPort;//报警输出端口
            public uint dwInfoLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN)]
            public string sInfo;
        }

        //日志信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//主类型 1-报警; 2-异常; 3-操作; 0xff-全部
            public uint dwMinorType;//次类型 0-全部;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//操作面板的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//网络操作的用户名
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteHostAddr;//远程主机地址
            public uint dwParaType;//参数类型
            public uint dwChannel;//通道号
            public uint dwDiskNumber;//硬盘号
            public uint dwAlarmInPort;//报警输入端口
            public uint dwAlarmOutPort;//报警输出端口
        }

        /************************DVR日志 end***************************/

        //报警输出状态(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;

            public void Init()
            {
                Output = new byte[MAX_ALARMOUT_V30];
            }
        }

        //报警输出状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;
        }

        //交易信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_TRADEINFO
        {
            public ushort m_Year;
            public ushort m_Month;
            public ushort m_Day;
            public ushort m_Hour;
            public ushort m_Minute;
            public ushort m_Second;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] DeviceName;//设备名称
            public uint dwChannelNumer;//通道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] CardNumber;//卡号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string cTradeType;//交易类型
            public uint dwCash;//交易金额
        }

        //ATM专用
        /****************************ATM(begin)***************************/
        public const int NCR = 0;
        public const int DIEBOLD = 1;
        public const int WINCOR_NIXDORF = 2;
        public const int SIEMENS = 3;
        public const int OLIVETTI = 4;
        public const int FUJITSU = 5;
        public const int HITACHI = 6;
        public const int SMI = 7;
        public const int IBM = 8;
        public const int BULL = 9;
        public const int YiHua = 10;
        public const int LiDe = 11;
        public const int GDYT = 12;
        public const int Mini_Banl = 13;
        public const int GuangLi = 14;
        public const int DongXin = 15;
        public const int ChenTong = 16;
        public const int NanTian = 17;
        public const int XiaoXing = 18;
        public const int GZYY = 19;
        public const int QHTLT = 20;
        public const int DRS918 = 21;
        public const int KALATEL = 22;
        public const int NCR_2 = 23;
        public const int NXS = 24;




        ///*************操作异常类型(消息方式, 回调方式(保留))****************/
        //public const int EXCEPTION_EXCHANGE = 32768;//用户交互时异常
        //public const int EXCEPTION_AUDIOEXCHANGE = 32769;//语音对讲异常
        //public const int EXCEPTION_ALARM = 32770;//报警异常
        //public const int EXCEPTION_PREVIEW = 32771;//网络预览异常
        //public const int EXCEPTION_SERIAL = 32772;//透明通道异常
        //public const int EXCEPTION_RECONNECT = 32773;//预览时重连
        //public const int EXCEPTION_ALARMRECONNECT = 32774;//报警时重连
        //public const int EXCEPTION_SERIALRECONNECT = 32775;//透明通道重连
        //public const int EXCEPTION_PLAYBACK = 32784;//回放异常
        //public const int EXCEPTION_DISKFMT = 32785;//硬盘格式化


        /*帧格式*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMETYPECODE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] code;/* 代码 */
        }

        //ATM参数(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMEFORMAT_V30
        {
            public uint dwSize;
            public NET_DVR_IPADDR struATMIP;/* ATM IP地址 */
            public uint dwATMType;/* ATM类型 */
            public uint dwInputMode;/* 输入方式    0-网络侦听 1-网络接收 2-串口直接输入 3-串口ATM命令输入*/
            public uint dwFrameSignBeginPos;/* 报文标志位的起始位置*/
            public uint dwFrameSignLength;/* 报文标志位的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* 报文标志位的内容 */
            public uint dwCardLengthInfoBeginPos;/* 卡号长度信息的起始位置 */
            public uint dwCardLengthInfoLength;/* 卡号长度信息的长度 */
            public uint dwCardNumberInfoBeginPos;/* 卡号信息的起始位置 */
            public uint dwCardNumberInfoLength;/* 卡号信息的长度 */
            public uint dwBusinessTypeBeginPos;/* 交易类型的起始位置 */
            public uint dwBusinessTypeLength;/* 交易类型的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* 类型 */
            public ushort wATMPort;/* 卡号捕捉端口号(网络协议方式) */
            public ushort wProtocolType;/* 网络协议类型 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ATM参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FRAMEFORMAT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sATMIP;/* ATM IP地址 */
            public uint dwATMType;/* ATM类型 */
            public uint dwInputMode;/* 输入方式    0-网络侦听 1-网络接收 2-串口直接输入 3-串口ATM命令输入*/
            public uint dwFrameSignBeginPos;/* 报文标志位的起始位置*/
            public uint dwFrameSignLength;/* 报文标志位的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* 报文标志位的内容 */
            public uint dwCardLengthInfoBeginPos;/* 卡号长度信息的起始位置 */
            public uint dwCardLengthInfoLength;/* 卡号长度信息的长度 */
            public uint dwCardNumberInfoBeginPos;/* 卡号信息的起始位置 */
            public uint dwCardNumberInfoLength;/* 卡号信息的长度 */
            public uint dwBusinessTypeBeginPos;/* 交易类型的起始位置 */
            public uint dwBusinessTypeLength;/* 交易类型的长度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* 类型 */
        }

        //SDK_V31 ATM
        /*过滤设置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FILTER
        {
            public byte byEnable;/*0,不启用;1,启用*/
            public byte byMode;/*0,ASCII;1,HEX*/
            public byte byFrameBeginPos;// 报文标志位的起始位置     
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byFilterText;/*过滤字符串*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*起始标识设置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IDENTIFICAT
        {
            public byte byStartMode;/*0,ASCII;1,HEX*/
            public byte byEndMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_FRAMETYPECODE struStartCode;/*起始字符*/
            public NET_DVR_FRAMETYPECODE struEndCode;/*结束字符*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        /*报文信息位置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LOCATION
        {
            public byte byOffsetMode;/*0,token;1,fix*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwOffsetPos;/*mode为1的时候使用*/
            public NET_DVR_FRAMETYPECODE struTokenCode;/*标志位*/
            public byte byMultiplierValue;/*标志位多少次出现*/
            public byte byEternOffset;/*附加的偏移量*/
            public byte byCodeMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*报文信息长度*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LENGTH
        {
            public byte byLengthMode;/*长度类型，0,variable;1,fix;2,get from package(设置卡号长度使用)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwFixLength;/*mode为1的时候使用*/
            public uint dwMaxLength;
            public uint dwMinLength;
            public byte byEndMode;/*终结符0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_FRAMETYPECODE struEndCode;/*终结符*/
            public uint dwLengthPos;/*lengthMode为2的时候使用，卡号长度在报文中的位置*/
            public uint dwLengthLen;/*lengthMode为2的时候使用，卡号长度的长度*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        /*OSD 叠加的位置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OSD_POSITION
        {
            public byte byPositionMode;/*叠加风格，共2种；0，不显示；1，Custom*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPos_x;/*x坐标，positionmode为Custom时使用*/
            public uint dwPos_y;/*y坐标，positionmode为Custom时使用*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*日期显示格式*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_DATE_FORMAT
        {
            public byte byItem1;/*Month,0.mm;1.mmm;2.mmmm*/
            public byte byItem2;/*Day,0.dd;*/
            public byte byItem3;/*Year,0.yy;1.yyyy*/
            public byte byDateForm;/*0~5，3个item的排列组合*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;/*分隔符*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*显示分隔符*/
            public byte byDisplayForm;/*0~5，3个item的排列组合*///lili mode by lili
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        /*时间显示格式*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVRT_TIME_FORMAT
        {
            public byte byTimeForm;/*1. HH MM SS;0. HH MM*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byHourMode;/*0,12;1,24*/ //lili mode
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;/*报文分隔符，暂时没用*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*显示分隔符*/
            public byte byDisplayForm;/*0~5，3个item的排列组合*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public byte byDisplayHourMode;/*0,12;1,24*/ //lili mode
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 19, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OVERLAY_CHANNEL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;/*叠加的通道*/
            public uint dwDelayTime;/*叠加延时时间*/
            public byte byEnableDelayTime;/*是否启用叠加延时，在无退卡命令时启用*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_ACTION
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struActionCode;/*交易类型等对应的码*/
            public NET_DVR_FRAMETYPECODE struPreCode;/*叠加字符前的字符*/
            public byte byActionCodeMode;/*交易类型等对应的码0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_DATE
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_DATE_FORMAT struDateForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_TIME
        {
            public tagNET_DVR_PACKAGE_LOCATION location;
            public tagNET_DVRT_TIME_FORMAT struTimeForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_OTHERS
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_PACKAGE_LENGTH struPackageLength;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struPreCode;/*叠加字符前的字符*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_FRAMETYPE_NEW
        {
            public byte byEnable;/*是否启用0,不启用;1,启用*/
            public byte byInputMode;/*输入方式:网络监听、串口监听、网络协议、串口协议、串口服务器*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAtmName;/*ATM 名称*/
            public NET_DVR_IPADDR struAtmIp;/*ATM 机IP  */
            public ushort wAtmPort;/* 卡号捕捉端口号(网络协议方式) 或串口服务器端口号*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwAtmType;/*ATM 机类型*/
            public tagNET_DVR_IDENTIFICAT struIdentification;/*报文标志*/
            public tagNET_DVR_FILTER struFilter;/*过滤设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struCardNoPara;/*叠加卡号设置*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ACTION_TYPE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PACKAGE_ACTION[] struTradeActionPara;/*叠加交易行为设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struAmountPara;/*叠加交易金额设置*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struSerialNoPara;/*叠加交易序号设置*/
            public tagNET_DVR_OVERLAY_CHANNEL struOverlayChan;/*叠加通道设置*/
            public tagNET_DVR_ATM_PACKAGE_DATE byRes4;/*叠加日期设置，暂时保留*/
            public tagNET_DVR_ATM_PACKAGE_TIME byRes5;/*叠加时间设置，暂时保留*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FRAMEFORMAT_V31
        {
            public uint dwSize;
            public tagNET_DVR_ATM_FRAMETYPE_NEW struAtmFrameTypeNew;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_FRAMETYPE_NEW[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_ATM_PROTOIDX
        {
            public uint dwAtmType;/*ATM协议类型，同时作为索引序号*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = ATM_DESC_LEN)]
            public string chDesc;/*ATM协议简单描述*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PROTOCOL
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ATM_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PROTOIDX[] struAtmProtoidx;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ATM_PROTOCOL_SORT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAtmNumPerSort;/*每段协议数*/
        }

        /*****************************DS-6001D/F(begin)***************************/
        //DS-6001D Decoder
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//解码设备连接的服务器IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//解码设备连接的服务器的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//解码设备连接的服务器的密码
            public byte bySendMode;//解码设备连接服务器的连接模式
            public byte byEncoderChannel;//解码设备连接的服务器的通道号
            public ushort wEncoderPort;//解码设备连接的服务器的端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERSTATE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//解码设备连接的服务器IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//解码设备连接的服务器的用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//解码设备连接的服务器的密码
            public byte byEncoderChannel;//解码设备连接的服务器的通道号
            public byte bySendMode;//解码设备连接的服务器的连接模式
            public ushort wEncoderPort;//解码设备连接的服务器的端口号
            public uint dwConnectState;//解码设备连接服务器的状态
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//保留
        }

        /*解码设备控制码定义*/
        public const int NET_DEC_STARTDEC = 1;
        public const int NET_DEC_STOPDEC = 2;
        public const int NET_DEC_STOPCYCLE = 3;
        public const int NET_DEC_CONTINUECYCLE = 4;

        /*连接的通道配置*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public byte byChannel;/* 通道号 */
            public byte byLinkMode;/* 连接模式 */
            public byte byLinkType;/* 连接类型 0－主码流 1－子码流 */
        }

        /*每个解码通道的配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECINFO
        {
            public byte byPoolChans;/*每路解码通道上的循环通道数量, 最多4通道 0表示没有解码*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECPOOLNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANINFO[] struchanConInfo;
            public byte byEnablePoll;/*是否轮巡 0-否 1-是*/
            public byte byPoolTime;/*轮巡时间 0-保留 1-10秒 2-15秒 3-20秒 4-30秒 5-45秒 6-1分钟 7-2分钟 8-5分钟 */
        }

        /*整个设备解码配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECCFG
        {
            public uint dwSize;
            public uint dwDecChanNum;/*解码通道的数量*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECINFO[] struDecInfo;
        }

        //2005-08-01
        /* 解码设备透明通道设置 */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PORTINFO
        {
            public uint dwEnableTransPort;/* 是否启动透明通道 0－不启用 1－启用*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP地址 */
            public ushort wDecoderPort;/* 端口号 */
            public ushort wDVRTransPort;/* 配置前端DVR是从485/232输出，1表示232串口,2表示485串口 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string cReserve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PORTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRANSPARENTNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PORTINFO[] struTransPortInfo;/* 数组0表示232 数组1表示485 */
        }

        /* 控制网络文件回放 */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PLAYREMOTEFILE
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP地址 */
            public ushort wDecoderPort;/* 端口号 */
            public ushort wLoadMode;/* 回放下载模式 1－按名字 2－按时间 */

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct mode_size
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                [FieldOffsetAttribute(0)]
                public byte[] byFile;/* 回放的文件名 */

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct bytime
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    public NET_DVR_TIME struStartTime;/* 按时间回放的开始时间 */
                    public NET_DVR_TIME struStopTime;/* 按时间回放的结束时间 */
                }
            }
        }

        /*当前设备解码连接状态*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANSTATUS
        {
            public uint dwWorkType;/*工作方式：1：轮巡、2：动态连接解码、3：文件回放下载 4：按时间回放下载*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/*连接的设备ip*/
            public ushort wDVRPort;/*连接端口号*/
            public byte byChannel;/* 通道号 */
            public byte byLinkMode;/* 连接模式 */
            public uint dwLinkType;/*连接类型 0－主码流 1－子码流*/

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct objectInfo
            {
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct userInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 52)]
                    public string cReserve;
                }

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct fileInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                    public byte[] fileName;
                }
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct timeInfo
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*请求视频用户名*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* 密码 */
                    public NET_DVR_TIME struStartTime;/* 按时间回放的开始时间 */
                    public NET_DVR_TIME struStopTime;/* 按时间回放的结束时间 */
                }
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECSTATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANSTATUS[] struTransPortInfo;
        }

        //单字符参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SHOWSTRINGINFO
        {
            public ushort wShowString;// 预览的图象上是否显示字符,0-不显示,1-显示 区域大小704*576,单个字符的大小为32*32
            public ushort wStringSize;/* 该行字符的长度，不能大于44个字符 */
            public ushort wShowStringTopLeftX;/* 字符显示位置的x坐标 */
            public ushort wShowStringTopLeftY;/* 字符名称显示位置的y坐标 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 44)]
            public string sString;/* 要显示的字符内容 */
        }

        //叠加字符(9000扩展)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */

            public void Init()
            {
                struStringInfo = new NET_DVR_SHOWSTRINGINFO[MAX_STRINGNUM_V30];
            }
        }

        //叠加字符扩展(8条字符)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_EX, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */
        }

        //叠加字符
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* 要显示的字符内容 */
        }

        /****************************DS9000新增结构(begin)******************************/

        /*EMAIL参数结构*/
        //与原结构体有差异
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struReceiver
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sName;/* 收件人姓名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAddress;/* 收件人地址 */
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAccount;/* 账号*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_PWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/*密码 */

            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct struSender
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sName;/* 发件人姓名 */
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sAddress;/* 发件人地址 */
            }

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;/* smtp服务器 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;/* pop3服务器 */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public struReceiver[] struStringInfo;/* 最多可以设置3个收件人 */

            public byte byAttachment;/* 是否带附件 */
            public byte bySmtpServerVerify;/* 发送服务器要求身份验证 */
            public byte byMailInterval;/* mail interval */
            public byte byEnableSSL;//ssl是否启用9000_1.1
            public ushort wSmtpPort;//gmail的465，普通的为25  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 74, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        /*DVR实现巡航数据结构*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_PARA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* 预置点号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseSpeed;/* 巡航速度 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.U2)]
            public ushort[] wDwellTime;/* 停留时间 */
            public byte byEnableThisCruise;/* 是否启用 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
        /****************************DS9000新增结构(end)******************************/

        //时间点
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIMEPOINT
        {
            public uint dwMonth;//月 0-11表示1-12个月
            public uint dwWeekNo;//第几周 0－第1周 1－第2周 2－第3周 3－第4周 4－最后一周
            public uint dwWeekDate;//星期几 0－星期日 1－星期一 2－星期二 3－星期三 4－星期四 5－星期五 6－星期六
            public uint dwHour;//小时    开始时间0－23 结束时间1－23
            public uint dwMin;//分    0－59
        }

        //夏令时参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ZONEANDDST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwEnableDST;//是否启用夏时制 0－不启用 1－启用
            public byte byDSTBias;//夏令时偏移值，30min, 60min, 90min, 120min, 以分钟计，传递原始数值
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_TIMEPOINT struBeginPoint;//夏时制开始时间
            public NET_DVR_TIMEPOINT struEndPoint;//夏时制停止时间
        }

        //图片质量
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_JPEGPARA
        {
            /*注意：当图像压缩分辨率为VGA时，支持0=CIF, 1=QCIF, 2=D1抓图，
            当分辨率为3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA,7=XVGA, 8=HD900p
            仅支持当前分辨率的抓图*/
            public ushort wPicSize;/* 0=CIF, 1=QCIF, 2=D1 3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA*/
            public ushort wPicQuality;/* 图片质量系数 0-最好 1-较好 2-一般 */
        }

        //全屏测温
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_JPEGPICTURE_WITHAPPENDDATA
        {
            public uint dwSize;
            public uint dwChannel;
            public uint dwJPEGPicLen;
            public IntPtr pJPEGPicBuff;
            public uint dwJPEGPicWidth;
            public uint dwJPEGPicHeight;
            public uint dwP2PDataLen;
            public IntPtr pP2PDataBuff;
            public byte byIsFreezedata;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 255, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //测温模式配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_MODE
        {
            public uint dwSize;
            public byte byMode;  // 测温模式：0- 普通模式，1- 专家模式 
            public byte byThermometryROIEnabled; //测温ROI使能 0-保留 1-不开启 2-开启（基于互斥兼容考虑）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        /* aux video out parameter */
        //辅助输出参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUXOUTCFG
        {
            public uint dwSize;
            public uint dwAlarmOutChan;/* 选择报警弹出大报警通道切换时间：1画面的输出通道: 0:主输出/1:辅1/2:辅2/3:辅3/4:辅4 */
            public uint dwAlarmChanSwitchTime;/* :1秒 - 10:10秒 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAuxSwitchTime;/* 辅助输出切换时间: 0-不切换,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT * MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] byAuxOrder;/* 辅助输出预览顺序, 0xff表示相应的窗口不预览 */
        }

        //ntp
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NTPPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sNTPServer;/* Domain Name or IP addr of NTP server */
            public ushort wInterval;/* adjust time interval(hours) */
            public byte byEnableNTP;/* enable NPT client 0-no，1-yes*/
            public byte cTimeDifferenceH;/* 与国际标准时间的 小时偏移-12 ... +13 */
            public byte cTimeDifferenceM;/* 与国际标准时间的 分钟偏移0, 30, 45*/
            public byte res1;
            public ushort wNtpPort; /* ntp server port 9000新增 设备默认为123*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
        }

        //ddns
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS账号用户名/密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName; /* 域名 */
            public byte byEnableDDNS;/*是否应用 0-否，1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_EX
        {
            public byte byHostIndex;/* 0-Hikvision DNS 1－Dyndns 2－PeanutHull(花生壳)*/
            public byte byEnableDDNS;/*是否应用DDNS 0-否，1-是*/
            public ushort wDDNSPort;/* DDNS端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS用户名*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* DDNS密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* 设备配备的域名地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNS 对应的服务器地址，可以是IP地址或域名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //9000扩展
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struDDNS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS账号用户名*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* 设备配备的域名地址 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNS协议对应的服务器地址，可以是IP地址或域名 */
            public ushort wDDNSPort;/* 端口号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_V30
        {
            public byte byEnableDDNS;
            public byte byHostIndex;/* 0-Hikvision DNS(保留) 1－Dyndns 2－PeanutHull(花生壳)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DDNS_NUMS, ArraySubType = UnmanagedType.Struct)]
            public struDDNS[] struDDNS;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //email
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* 邮件账号/密码 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sMailAddr;/* email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr1;/* 上传报警/异常等的email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //网络参数配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETAPPCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDNSIp; /* DNS服务器地址 */
            public NET_DVR_NTPPARA struNtpClientParam;/* NTP参数 */
            public NET_DVR_DDNSPARA struDDNSClientParam;/* DDNS参数 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 464, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* 保留 */
        }

        //nfs结构配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SINGLE_NFS
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNfsHostIPAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNfsDirectory;

            public void Init()
            {
                this.sNfsDirectory = new byte[PATHNAME_LEN];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NFSCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NFS_DISK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_NFS[] struNfsDiskParam;

            public void Init()
            {
                this.struNfsDiskParam = new NET_DVR_SINGLE_NFS[MAX_NFS_DISK];

                for (int i = 0; i < MAX_NFS_DISK; i++)
                {
                    struNfsDiskParam[i].Init();
                }
            }
        }

        //巡航点配置(HIK IP快球专用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_POINT
        {
            public byte PresetNum;//预置点
            public byte Dwell;//停留时间
            public byte Speed;//速度
            public byte Reserve;//保留

            public void Init()
            {
                PresetNum = 0;
                Dwell = 0;
                Speed = 0;
                Reserve = 0;
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_RET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CRUISE_POINT[] struCruisePoint;//最大支持32个巡航点

            public void Init()
            {
                struCruisePoint = new NET_DVR_CRUISE_POINT[32];
                for (int i = 0; i < 32; i++)
                {
                    struCruisePoint[i].Init();
                }
            }
        }

        /************************************多路解码器(begin)***************************************/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG_OTHER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DECINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byTransProtocol;/* 传输协议类型 0-TCP, 1-UDP */
            public byte byTransMode;/* 传输码流模式 0－主码流 1－子码流*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 监控主机登陆帐号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 监控主机密码 */
        }

        //启动/停止动态解码
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DYNAMIC_DEC
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 动态解码通道信息 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
        {
            public uint dwSize;
            public uint dwIsLinked;/* 解码通道状态 0－休眠 1－正在连接 2－已连接 3-正在解码 */
            public uint dwStreamCpRate;/* Stream copy rate, X kbits/second */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cRes;/* 保留 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 解码通道信息 */
            public uint dwDecState;/* 0-动态解码 1－循环解码 2－按时间回放 3－按文件回放 */
            public NET_DVR_TIME StartTime;/* 按时间回放开始时间 */
            public NET_DVR_TIME StopTime;/* 按时间回放停止时间 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* 按文件回放文件名 */
        }

        //连接的通道配置 2007-11-05
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DECCHANINFO
        {
            public uint dwEnable;/* 是否启用 0－否 1－启用*/
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* 轮循解码通道信息 */
        }

        //2007-11-05 新增每个解码通道的配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_LOOP_DECINFO
        {
            public uint dwSize;
            public uint dwPoolTime;/*轮巡时间 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_DECCHANINFO[] struchanConInfo;
        }

        //2007-12-22
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TTY_CONFIG
        {
            public byte baudrate;/* 波特率 */
            public byte databits;/* 数据位 */
            public byte stopbits;/* 停止位 */
            public byte parity;/* 奇偶校验位 */
            public byte flowcontrol;/* 流控 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* 当前透明通道是否打开 0：关闭 1：打开 */
            /*
             *    多路解码器本地有1个485串口，1个232串口都可以作为透明通道,设备号分配如下：
             *    0 RS485
             *    1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *    远程串口输出还是两个,一个RS232，一个RS485
             *    1表示232串口
             *    2表示485串口
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte res1;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;/* 保留 */
            public TTY_CONFIG RemoteSerialDevCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;/* 设置哪路232透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* 设置哪路485透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_TRAN_CHAN_INFO[] struTranInfo;/*同时支持建立MAX_SERIAL_NUM个透明通道*/
        }

        //2007-12-24 Merry Christmas Eve...
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byReserve;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 用户名 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 密码 */
            public uint dwPlayMode;/* 0－按文件 1－按时间*/
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_CONTROL
        {
            public uint dwSize;
            public uint dwPlayCmd;/* 播放命令 见文件播放命令*/
            public uint dwCmdParam;/* 播放命令参数 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS
        {
            public uint dwSize;
            public uint dwCurMediaFileLen;/* 当前播放的媒体文件长度 */
            public uint dwCurMediaFilePosition;/* 当前播放文件的播放位置 */
            public uint dwCurMediaFileDuration;/* 当前播放文件的总时间 */
            public uint dwCurPlayTime;/* 当前已经播放的时间 */
            public uint dwCurMediaFIleFrames;/* 当前播放文件的总帧数 */
            public uint dwCurDataType;/* 当前传输的数据类型，19-文件头，20-流数据， 21-播放结束标志 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 72, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //2009-4-11 added by likui 多路解码器new
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_MATRIX_PASSIVEMODE
        {
            public ushort wTransProtol;//传输协议，0-TCP, 1-UDP, 2-MCAST
            public ushort wPassivePort;//UDP端口, TCP时默认
            // char    sMcastIP[16];  //TCP,UDP时无效, MCAST时为多播地址
            public NET_DVR_IPADDR struMcastIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagDEV_CHAN_INFO
        {
            public NET_DVR_IPADDR struIP;/* DVR IP地址 */
            public ushort wDVRPort;/* 端口号 */
            public byte byChannel;/* 通道号 */
            public byte byTransProtocol;/* 传输协议类型0-TCP，1-UDP */
            public byte byTransMode;/* 传输码流模式 0－主码流 1－子码流*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 71, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* 监控主机登陆帐号 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* 监控主机密码 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* 当前透明通道是否打开 0：关闭 1：打开 */
            /*
             *    多路解码器本地有1个485串口，1个232串口都可以作为透明通道,设备号分配如下：
             *    0 RS485
             *    1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *    远程串口输出还是两个,一个RS232，一个RS485
             *    1表示232串口
             *    2表示485串口
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte byRes1;/* 保留 */
            public NET_DVR_IPADDR struRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            public byte byIsEstablished;/* 透明通道建立成功标志，0-没有成功，1-建立成功 */
            public byte byRes2;/* 保留 */
            public TTY_CONFIG RemoteSerialDevCfg;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUsername;/* 32BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassword;/* 16BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;/* 设置哪路232透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* 设置哪路485透明通道是全双工的 取值1到MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] vyRes;/* 保留 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagMATRIX_TRAN_CHAN_INFO[] struTranInfo;/*同时支持建立MAX_SERIAL_NUM个透明通道*/
        }

        /*流媒体服务器基本配置*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MEDIA_SERVER_CFG
        {
            public byte byValid;/*是否启用流媒体服务器取流,0表示无效，非0表示有效*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struDevIP;
            public ushort wDevPort;/*流媒体服务器端口*/
            public byte byTransmitType;/*传输协议类型 0-TCP，1-UDP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 69, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_CFG
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG struStreamMediaSvrCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_INFO_V30
        {
            public uint dwEnable;/* 是否启用 0－否 1－启用*/
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;/* 轮循解码通道信息 */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_LOOP_DECINFO_V30
        {
            public uint dwSize;
            public uint dwPoolTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struchanConInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagDEC_MATRIX_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;/*流媒体服务器配置*/
            public tagDEV_CHAN_INFO struDevChanInfo;/* 解码通道信息 */
            public uint dwDecState;/* 0-动态解码 1－循环解码 2－按时间回放 3－按文件回放 */
            public NET_DVR_TIME StartTime;/* 按时间回放开始时间 */
            public NET_DVR_TIME StopTime;/* 按时间回放停止时间 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* 按文件回放文件名 */
            public uint dwGetStreamMode;/*取流模式:1-主动，2-被动*/
            public tagNET_MATRIX_PASSIVEMODE struPassiveMode;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_MATRIX_ABILITY
        {
            public uint dwSize;
            public byte byDecNums;
            public byte byStartChan;
            public byte byVGANums;
            public byte byBNCNums;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byVGAWindowMode;/*VGA支持的窗口模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byBNCWindowMode;/*BNC支持的窗口模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //上传logo结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_DISP_LOGOCFG
        {
            public uint dwCorordinateX;//图片显示区域X坐标
            public uint dwCorordinateY;//图片显示区域Y坐标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byFlash;//是否闪烁1-闪烁，0-不闪烁
            public byte byTranslucent;//是否半透明1-半透明，0-不半透明
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
            public uint dwLogoSize;//LOGO大小，包括BMP的文件头
        }

        /*编码类型*/
        public const int NET_DVR_ENCODER_UNKOWN = 0;/*未知编码格式*/
        public const int NET_DVR_ENCODER_H264 = 1;/*HIK 264*/
        public const int NET_DVR_ENCODER_S264 = 2;/*Standard H264*/
        public const int NET_DVR_ENCODER_MPEG4 = 3;/*MPEG4*/
        /* 打包格式 */
        public const int NET_DVR_STREAM_TYPE_UNKOWN = 0;/*未知打包格式*/
        public const int NET_DVR_STREAM_TYPE_HIKPRIVT = 1; /*海康自定义打包格式*/
        public const int NET_DVR_STREAM_TYPE_TS = 7;/* TS打包 */
        public const int NET_DVR_STREAM_TYPE_PS = 8;/* PS打包 */
        public const int NET_DVR_STREAM_TYPE_RTP = 9;/* RTP打包 */

        /*解码通道状态*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_STATUS
        {
            public byte byDecodeStatus;/*当前状态:0:未启动，1：启动解码*/
            public byte byStreamType;/*码流类型*/
            public byte byPacketType;/*打包方式*/
            public byte byRecvBufUsage;/*接收缓冲使用率*/
            public byte byDecBufUsage;/*解码缓冲使用率*/
            public byte byFpsDecV;/*视频解码帧率*/
            public byte byFpsDecA;/*音频解码帧率*/
            public byte byCpuLoad;/*DSP CPU使用率*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDecodedV;/*解码的视频帧*/
            public uint dwDecodedA;/*解码的音频帧*/
            public ushort wImgW;/*解码器当前的图像大小,宽*/
            public ushort wImgH; //高
            public byte byVideoFormat;/*视频制式:0-NON,NTSC--1,PAL--2*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*显示通道状态*/
        public const int NET_DVR_MAX_DISPREGION = 16;         /*每个显示通道最多可以显示的窗口*/
        //VGA分辨率，目前能用的是：VGA_THS8200_MODE_XGA_60HZ、VGA_THS8200_MODE_SXGA_60HZ、
        //
        public enum VGA_MODE
        {
            VGA_NOT_AVALIABLE,
            VGA_THS8200_MODE_SVGA_60HZ,//（800*600）
            VGA_THS8200_MODE_SVGA_75HZ, //（800*600）
            VGA_THS8200_MODE_XGA_60HZ,//（1024*768）
            VGA_THS8200_MODE_XGA_70HZ, //（1024*768）
            VGA_THS8200_MODE_SXGA_60HZ,//（1280*1024）
            VGA_THS8200_MODE_720P_60HZ,//（1280*720 ）
            VGA_THS8200_MODE_1080i_60HZ,//（1920*1080）
            VGA_THS8200_MODE_1080P_30HZ,//（1920*1080）
            VGA_THS8200_MODE_1080P_25HZ,//（1920*1080）
            VGA_THS8200_MODE_UXGA_30HZ,//（1600*1200）
        }

        /*视频制式标准*/
        public enum VIDEO_STANDARD
        {
            VS_NON = 0,
            VS_NTSC = 1,
            VS_PAL = 2,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_VGA_DISP_CHAN_CFG
        {
            public uint dwSize;
            public byte byAudio;/*音频是否开启,0-否，1-是*/
            public byte byAudioWindowIdx;/*音频开启子窗口*/
            public byte byVgaResolution;/*VGA的分辨率*/
            public byte byVedioFormat;/*视频制式，1:NTSC,2:PAL,0-NON*/
            public uint dwWindowMode;/*画面模式，从能力集里获取，目前支持1,2,4,9,16*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*各个子窗口关联的解码通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS
        {
            public byte byDispStatus;/*显示状态：0：未显示，1：启动显示*/
            public byte byBVGA; /*VGA/BNC*/
            public byte byVideoFormat;/*视频制式:1:NTSC,2:PAL,0-NON*/
            public byte byWindowMode;/*当前画面模式*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*各个子窗口关联的解码通道*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_DVR_MAX_DISPREGION, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;/*每个子画面的显示帧率*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int MAX_DECODECHANNUM = 32;//多路解码器最大解码通道数
        public const int MAX_DISPCHANNUM = 24;//多路解码器最大显示通道数

        /*解码器设备状态*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR__DECODER_WORK_STATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;/*解码通道状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISPCHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS[] struDispChanStatus;/*显示通道状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;/*报警输入状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;/*报警输出状态*/
            public byte byAudioInChanStatus;/*语音对讲状态*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /************************************多路解码器(end)***************************************/

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_EMAILCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sPassWord;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sFromName;/* Sender *///字符串中的第一个字符和最后一个字符不能是"@",并且字符串中要有"@"字符
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sFromAddr;/* Sender address */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName1;/* Receiver1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName2;/* Receiver2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr1;/* Receiver address1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr2;/* Receiver address2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sEmailServer;/* Email server address */
            public byte byServerType;/* Email server type: 0-SMTP, 1-POP, 2-IMTP…*/
            public byte byUseAuthen;/* Email server authentication method: 1-enable, 0-disable */
            public byte byAttachment;/* enable attachment */
            public byte byMailinterval;/* mail interval 0-2s, 1-3s, 2-4s. 3-5s*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_NEW
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struLowCompression;//定时录像
            public NET_DVR_COMPRESSION_INFO_EX struEventCompression;//事件触发录像
        }

        //球机位置信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZPOS
        {
            public ushort wAction;//获取时该字段无效
            public ushort wPanPos;//水平参数
            public ushort wTiltPos;//垂直参数
            public ushort wZoomPos;//变倍参数
        }

        //球机范围信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZSCOPE
        {
            public ushort wPanPosMin;//水平参数min
            public ushort wPanPosMax;//水平参数max
            public ushort wTiltPosMin;//垂直参数min
            public ushort wTiltPosMax;//垂直参数max
            public ushort wZoomPosMin;//变倍参数min
            public ushort wZoomPosMax;//变倍参数max
        }

        //rtsp配置 ipcamera专用
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RTSPCFG
        {
            public uint dwSize;//长度
            public ushort wPort;//rtsp服务器侦听端口
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 54, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;//预留
        }

        /********************************接口参数结构(begin)*********************************/

        //NET_DVR_Login()参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public byte byAlarmInPortNum;//DVR报警输入个数
            public byte byAlarmOutPortNum;//DVR报警输出个数
            public byte byDiskNum;//DVR硬盘个数
            public byte byDVRType;//DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR 通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
        }

        //NET_DVR_Login_V30()参数结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            public byte byAlarmInPortNum;//报警输入个数
            public byte byAlarmOutPortNum;//报警输出个数
            public byte byDiskNum;//硬盘个数
            public byte byDVRType;//设备类型, 1:DVR 2:ATM DVR 3:DVS ...
            public byte byChanNum;//模拟通道个数
            public byte byStartChan;//起始通道号,例如DVS-1,DVR - 1
            public byte byAudioChanNum;//语音通道数
            public byte byIPChanNum;//最大数字通道个数  
            public byte byZeroChanNum;      //零通道编码个数 //2010-01-16
            public byte byMainProto;      //主码流传输协议类型 0-private, 1-rtsp,2-同时支持private和rtsp
            public byte bySubProto;    //子码流传输协议类型0-private, 1-rtsp,2-同时支持private和rtsp
            public byte bySupport;        //能力，位与结果为0表示不支持，1表示支持，
            //bySupport & 0x1, 表示是否支持智能搜索
            //bySupport & 0x2, 表示是否支持备份
            //bySupport & 0x4, 表示是否支持压缩参数能力获取
            //bySupport & 0x8, 表示是否支持多网卡
            //bySupport & 0x10, 表示支持远程SADP
            //bySupport & 0x20, 表示支持Raid卡功能
            //bySupport & 0x40, 表示支持IPSAN 目录查找
            //bySupport & 0x80, 表示支持rtp over rtsp
            public byte bySupport1;        // 能力集扩充，位与结果为0表示不支持，1表示支持
            //bySupport1 & 0x1, 表示是否支持snmp v30
            //bySupport1 & 0x2, 支持区分回放和下载
            //bySupport1 & 0x4, 是否支持布防优先级    
            //bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
            //bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
            //bySupport1 & 0x20, 表示是否支持rtsp over http    
            //bySupport1 & 0x80, 表示是否支持车牌新报警信息2012-9-28, 且还表示是否支持NET_DVR_IPPARACFG_V40结构体
            public byte bySupport2; /*能力，位与结果为0表示不支持，非0表示支持          
                     bySupport2 & 0x1, 表示解码器是否支持通过URL取流解码
                     bySupport2 & 0x2,  表示支持FTPV40
                     bySupport2 & 0x4,  表示支持ANR
                     bySupport2 & 0x8,  表示支持CCD的通道参数配置
                     bySupport2 & 0x10,  表示支持布防报警回传信息（仅支持抓拍机报警 新老报警结构）
                     bySupport2 & 0x20,  表示是否支持单独获取设备状态子项
            bySupport2 & 0x40,  表示是否是码流加密设备*/
            public ushort wDevType;              //设备型号
            public byte bySupport3; //能力集扩展，位与结果为0表示不支持，1表示支持
            //bySupport3 & 0x1, 表示是否多码流
            // bySupport3 & 0x4 表示支持按组配置， 具体包含 通道图像参数、报警输入参数、IP报警输入、输出接入参数、
            // 用户参数、设备工作状态、JPEG抓图、定时和时间抓图、硬盘盘组管理 
            //bySupport3 & 0x8为1 表示支持使用TCP预览、UDP预览、多播预览中的"延时预览"字段来请求延时预览（后续都将使用这种方式请求延时预览）。而当bySupport3 & 0x8为0时，将使用 "私有延时预览"协议。
            //bySupport3 & 0x10 表示支持"获取报警主机主要状态（V40）"。
            //bySupport3 & 0x20 表示是否支持通过DDNS域名解析取流
            public byte byMultiStreamProto;//是否支持多码流,按位表示,0-不支持,1-支持,bit1-码流3,bit2-码流4,bit7-主码流，bit-8子码流
            public byte byStartDChan;  //起始数字通道号,0表示无效
            public byte byStartDTalkChan;    //起始数字对讲通道号，区别于模拟对讲通道号，0表示无效
            public byte byHighDChanNum;  //数字通道个数，高位
            public byte bySupport4;        //能力集扩展，位与结果为0表示不支持，1表示支持
            //bySupport4 & 0x4表示是否支持拼控统一接口
            // bySupport4 & 0x80 支持设备上传中心报警使能。表示判断调用接口是 NET_DVR_PDC_RULE_CFG_V42还是 NET_DVR_PDC_RULE_CFG_V41
            public byte byLanguageType;// 支持语种能力,按位表示,每一位0-不支持,1-支持  
            //  byLanguageType 等于0 表示 老设备
            //  byLanguageType & 0x1表示支持中文
            //  byLanguageType & 0x2表示支持英文
            public byte byVoiceInChanNum;   //音频输入通道数 
            public byte byStartVoiceInChanNo; //音频输入起始通道号 0表示无效
            public byte bySupport5;  //按位表示,0-不支持,1-支持,bit0-支持多码流
            //bySupport5 &0x01表示支持wEventTypeEx ,兼容dwEventType 的事件类型（支持行为事件扩展）--先占住，防止冲突
            //bySupport5 &0x04表示是否支持使用扩展的场景模式接口
            /*
               bySupport5 &0x08 设备返回该值表示是否支持计划录像类型V40接口协议(DVR_SET_RECORDCFG_V40/ DVR_GET_RECORDCFG_V40)(在该协议中设备支持类型类型13的配置)
               之前的部分发布的设备，支持录像类型13，则配置录像类型13。如果不支持，统一转换成录像类型3兼容处理。SDK通过命令探测处理)
            */
            public byte bySupport6;   //能力，按位表示，0-不支持,1-支持
            //bySupport6 0x1  表示设备是否支持压缩 
            //bySupport6 0x2 表示是否支持流ID方式配置流来源扩展命令，DVR_SET_STREAM_SRC_INFO_V40
            //bySupport6 0x4 表示是否支持事件搜索V40接口
            //bySupport6 0x8 表示是否支持扩展智能侦测配置命令
            //bySupport6 0x40表示图片查询结果V40扩展
            public byte byMirrorChanNum;    //镜像通道个数，<录播主机中用于表示导播通道>
            public ushort wStartMirrorChanNo;  //起始镜像通道号
            public byte bySupport7;   //能力,按位表示,0-不支持,1-支持
            // bySupport7 & 0x1  表示设备是否支持 INTER_VCA_RULECFG_V42 扩展
            // bySupport7 & 0x2  表示设备是否支持 IPC HVT 模式扩展
            // bySupport7 & 0x04  表示设备是否支持 返回锁定时间
            // bySupport7 & 0x08  表示设置云台PTZ位置时，是否支持带通道号
            // bySupport7 & 0x10  表示设备是否支持双系统升级备份
            // bySupport7 & 0x20  表示设备是否支持 OSD字符叠加 V50
            // bySupport7 & 0x40  表示设备是否支持 主从跟踪（从摄像机）
            // bySupport7 & 0x80  表示设备是否支持 报文加密
            public byte byRes2;
        }

        //sdk网络环境枚举变量，用于远程升级
        public enum SDK_NETWORK_ENVIRONMENT
        {
            LOCAL_AREA_NETWORK = 0,
            WIDE_AREA_NETWORK,
        }

        //显示模式
        public enum DISPLAY_MODE
        {
            NORMALMODE = 0,
            OVERLAYMODE
        }

        //发送模式
        public enum SEND_MODE
        {
            PTOPTCPMODE = 0,
            PTOPUDPMODE,
            MULTIMODE,
            RTPMODE,
            RESERVEDMODE
        }

        //抓图模式
        public enum CAPTURE_MODE
        {
            BMP_MODE = 0,  //BMP模式
            JPEG_MODE = 1  //JPEG模式 
        }

        //实时声音模式
        public enum REALSOUND_MODE
        {
            MONOPOLIZE_MODE = 1,//独占模式
            SHARE_MODE = 2  //共享模式
        }



        //SDK状态信息(9000新增)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKSTATE
        {
            public uint dwTotalLoginNum;//当前login用户数
            public uint dwTotalRealPlayNum;//当前realplay路数
            public uint dwTotalPlayBackNum;//当前回放或下载路数
            public uint dwTotalAlarmChanNum;//当前建立报警通道路数
            public uint dwTotalFormatNum;//当前硬盘格式化路数
            public uint dwTotalFileSearchNum;//当前日志或文件搜索路数
            public uint dwTotalLogSearchNum;//当前日志或文件搜索路数
            public uint dwTotalSerialNum;//当前透明通道路数
            public uint dwTotalUpgradeNum;//当前升级路数
            public uint dwTotalVoiceComNum;//当前语音转发路数
            public uint dwTotalBroadCastNum;//当前语音广播路数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //SDK功能支持信息(9000新增)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKABL
        {
            public uint dwMaxLoginNum;//最大login用户数 MAX_LOGIN_USERS
            public uint dwMaxRealPlayNum;//最大realplay路数 WATCH_NUM
            public uint dwMaxPlayBackNum;//最大回放或下载路数 WATCH_NUM
            public uint dwMaxAlarmChanNum;//最大建立报警通道路数 ALARM_NUM
            public uint dwMaxFormatNum;//最大硬盘格式化路数 SERVER_NUM
            public uint dwMaxFileSearchNum;//最大文件搜索路数 SERVER_NUM
            public uint dwMaxLogSearchNum;//最大日志搜索路数 SERVER_NUM
            public uint dwMaxSerialNum;//最大透明通道路数 SERVER_NUM
            public uint dwMaxUpgradeNum;//最大升级路数 SERVER_NUM
            public uint dwMaxVoiceComNum;//最大语音转发路数 SERVER_NUM
            public uint dwMaxBroadCastNum;//最大语音广播路数 MAX_CASTNUM
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //报警设备信息
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            public byte byUserIDValid;/* userid是否有效 0-无效，1-有效 */
            public byte bySerialValid;/* 序列号是否有效 0-无效，1-有效 */
            public byte byVersionValid;/* 版本号是否有效 0-无效，1-有效 */
            public byte byDeviceNameValid;/* 设备名字是否有效 0-无效，1-有效 */
            public byte byMacAddrValid; /* MAC地址是否有效 0-无效，1-有效 */
            public byte byLinkPortValid;/* login端口是否有效 0-无效，1-有效 */
            public byte byDeviceIPValid;/* 设备IP是否有效 0-无效，1-有效 */
            public byte bySocketIPValid;/* socket ip是否有效 0-无效，1-有效 */
            public int lUserID; /* NET_DVR_Login()返回值, 布防时有效 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;/* 序列号 */
            public uint dwDeviceVersion;/* 版本信息 高16位表示主版本，低16位表示次版本*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;/* 设备名字 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;/* MAC地址 */
            public ushort wLinkPort; /* link port */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;/* IP地址 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;/* 报警主动上传时的socket IP地址 */
            public byte byIpProtocol; /* Ip协议 0-IPV4, 1-IPV6 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SYSTEM_TIME
        {
            public ushort  wYear;           //年
            public ushort  wMonth;          //月
            public ushort  wDay;            //日
            public ushort  wHour;           //时
            public ushort  wMinute;      //分
            public ushort  wSecond;      //秒
            public ushort  wMilliSec;    //毫秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_AIOP_VIDEO_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //设备分析通道的通道号；
            public NET_DVR_SYSTEM_TIME struTime; //时间
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string  szTaskID;     //视频任务ID，来自于视频任务派发
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize; //对应分析图片长度
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //检测模型包ID，用于匹配AIOP的检测数据解析；可以通过URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)获取当前设备加载的模型包的label description信息；
            public IntPtr pBufferAIOPData;  //AIOPDdata数据
            public IntPtr pBufferPicture;//对应分析图片数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_PICTURE_HEAD
        {
            public uint dwSize;//dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public NET_DVR_SYSTEM_TIME struTime;  //时间
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string  szPID;//透传下发的图片ID，来自于图片任务派发
            public uint dwAIOPDataSize;//dwSize = sizeof(AIOP_DATA)
            public byte  byStatus;//状态值：0-成功，1-图片大小错误
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;//检测模型包ID，用于匹配AIOP的检测数据解析；
            public IntPtr pBufferAIOPData;//AIOPDdata数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_POLLING_SNAP_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //设备分析通道的通道号；
            public NET_DVR_SYSTEM_TIME struTime;     //时间
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szTaskID;     //视频任务ID，来自于视频任务派发
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize;    //对应分析图片长度
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //检测模型包ID，用于匹配AIOP的检测数据解析；可以通过URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)获取当前设备加载的模型包的label description信息；
            public IntPtr pBufferAIOPData;  //AIOPDdata数据
            public IntPtr pBufferPicture;//对应分析图片数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_POLLING_VIDEO_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //设备分析通道的通道号；
            public NET_DVR_SYSTEM_TIME struTime;     //时间
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szTaskID;     //视频任务ID，来自于视频任务派发
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize;    //对应分析图片长度
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //检测模型包ID，用于匹配AIOP的检测数据解析；可以通过URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)获取当前设备加载的模型包的label description信息；
            public IntPtr pBufferAIOPData;  //AIOPDdata数据
            public IntPtr pBufferPicture;//对应分析图片数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //硬解码显示区域参数(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISPLAY_PARA
        {
            public int bToScreen;
            public int bToVideoOut;
            public int nLeft;
            public int nTop;
            public int nWidth;
            public int nHeight;
            public int nReserved;
        }

        //硬解码预览参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARDINFO
        {
            public int lChannel;//通道号
            public int lLinkMode;//最高位(31)为0表示主码流，为1表示子，0－30位表示码流连接方式:0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-电话线，5－128k宽带，6－256k宽带，7－384k宽带，8－512k宽带；
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string sMultiCastIP;
            public NET_DVR_DISPLAY_PARA struDisplayPara;
        }

        //录象文件参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FIND_DATA
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
        }

        //录象文件参数(9000)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;//9000设备支持,1表示此文件已经被锁定,0表示正常的文件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //录象文件参数(带卡号)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_CARD
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
        }

        //录象文件查找条件结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND
        {
            public int lChannel;//通道号
            public uint dwFileType;//录象文件类型0xff－全部，0－定时录像,1-移动侦测 ，2－报警触发，
            //3-报警|移动侦测 4-报警&移动侦测 5-命令触发 6-手动录像
            public uint dwIsLocked;//是否锁定 0-正常文件,1-锁定文件, 0xff表示所有文件
            public uint dwUseCardNo;//是否使用卡号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;//卡号
            public NET_DVR_TIME struStartTime;//开始时间
            public NET_DVR_TIME struStopTime;//结束时间
        }

        //云台区域选择放大缩小(HIK 快球专用)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_POINT_FRAME
        {
            public int xTop;//方框起始点的x坐标
            public int yTop;//方框结束点的y坐标
            public int xBottom;//方框结束点的x坐标
            public int yBottom;//方框结束点的y坐标
            public int bCounter;//保留
        }

        //语音对讲参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_COMPRESSION_AUDIO
        {
            public byte byAudioEncType;//音频编码类型 0-G722; 1-G711
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byres;//这里保留音频的压缩参数 
        }




        ////////////////////////////////////////////////////////////////////////////////////////
        ///抓拍机
        ///
        public const int MAX_OVERLAP_ITEM_NUM = 50;       //最大字符叠加种数
        public const int NET_ITS_GET_OVERLAP_CFG = 5072;//获取字符叠加参数配置（相机或ITS终端）
        public const int NET_ITS_SET_OVERLAP_CFG = 5073;//设置字符叠加参数配置（相机或ITS终端）

        //车牌识别结果子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_INFO
        {
            public byte byPlateType; //车牌类型
            public byte byColor; //车牌颜色
            public byte byBright; //车牌亮度
            public byte byLicenseLen;    //车牌字符个数
            public byte byEntireBelieve;//整个车牌的置信度，0-100
            public byte byRegion;                       // 区域索引值 0-保留，1-欧洲(EU)，2-俄语区域(ER)，3-欧洲&俄罗斯(EU&CIS) ,4-中东(ME),0xff-所有
            public byte byCountry;                      // 国家索引值，参照枚举COUNTRY_INDEX（不支持"COUNTRY_ALL = 0xff, //ALL  全部"）
            public byte byArea;                         //区域（省份），各国家内部区域枚举，阿联酋参照 EMI_AREA
            public byte byPlateSize;                    //车牌尺寸，0~未知，1~long, 2~short(中东车牌使用)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;                       //保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CATEGORY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPlateCategory;//车牌附加信息, 即中东车牌中车牌号码旁边的小字信息，(目前只有中东地区支持)
            public uint dwXmlLen;                        //XML报警信息长度
            public IntPtr pXmlBuf;                      // XML报警信息指针,报警类型为 COMM_ITS_PLATE_RESUL时有效，其XML对应到EventNotificationAlert XML Block
            public NET_VCA_RECT struPlateRect;    //车牌位置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sLicense;    //车牌号码 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBelieve; //各个识别字符的置信度，如检测到车牌"浙A12345", 置信度为,20,30,40,50,60,70，则表示"浙"字正确的可能性只有%，"A"字的正确的可能性是%
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VEHICLE_INFO
        {
            public uint dwIndex;
            public byte byVehicleType;
            public byte byColorDepth;
            public byte byColor;
            public byte byRadarState;
            public ushort wSpeed;
            public ushort wLength;
            public byte byIllegalType;
            public byte byVehicleLogoRecog; //参考枚举类型 VLR_VEHICLE_CLASS
            public byte byVehicleSubLogoRecog; //车辆品牌子类型识别；参考VSB_VOLKSWAGEN_CLASS等子类型枚举。
            public byte byVehicleModel; //车辆子品牌年款，0-未知，参考"车辆子品牌年款.xlsx"
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byCustomInfo;  //自定义信息
            public uint wVehicleLogoRecog;  //车辆主品牌，参考"车辆主品牌.xlsx" (该字段兼容byVehicleLogoRecog);
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[12];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_RESULT
        {
            public uint dwSize;
            public byte byResultType;
            public byte byChanIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwRelativeTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;
            public uint dwPicLen;
            public uint dwPicPlateLen;
            public uint dwVideoLen;
            public byte byTrafficLight;
            public byte byPicNum;
            public byte byDriveChan;
            public byte byRes2;
            public uint dwBinPicLen;
            public uint dwCarPicLen;
            public uint dwFarCarPicLen;
            public IntPtr pBuffer3;
            public IntPtr pBuffer4;
            public IntPtr pBuffer5;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public NET_DVR_PLATE_INFO struPlateInfo;
            public NET_DVR_VEHICLE_INFO struVehicleInfo;
            public IntPtr pBuffer1;
            public IntPtr pBuffer2;

            public void Init()
            {
                byRes1 = new byte[2];
                byAbsTime = new byte[32];
                byRes3 = new byte[8];
            }
        }

        //人脸抓拍结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TARGET_INFO
        {
            public uint dwID;//目标ID ,人员密度过高报警时为0
            public NET_VCA_RECT struRect; //目标边界框 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;//前端设备地址，
            public ushort wPort;//前端设备端口号， 
            public byte byChannel;//前端设备通道，
            public byte byIvmsChannel;// Ivms 通道 
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_FEATURE
        {
            public byte byAgeGroup;           //年龄段,参见 HUMAN_AGE_GROUP_ENUM
            public byte bySex;                //性别, 1 – 男 , 2 – 女
            public byte byEyeGlass;           //是否戴眼镜 1 – 不戴, 2 – 戴
            public byte byAge;           //年龄 0-表示“未知”（算法不支持）,0xff-算法支持，但是没有识别出来
            public byte byAgeDeviation;           //年龄误差值
            public byte byRes0;           //是否戴眼镜 1 – 不戴, 2 – 戴
            public byte byMask;           //是否戴口罩 0-表示“未知”（算法不支持）,1 – 不戴, 2 – 戴, 0xff-算法支持，但是没有识别出来
            public byte bySmile;           //是否微笑 0-表示“未知”（算法不支持）,1 – 不微笑, 2 – 微笑, 0xff-算法支持，但是没有识别出来
            public byte byFaceExpression;           // 表情,参见FACE_EXPRESSION_GROUP_ENUM
            public byte byRes1;
            public byte byRes2;           //是否戴眼镜 1 – 不戴, 2 – 戴
            public byte byHat;           //帽子, 0-不支持,1-不戴帽子,2-戴帽子, 3-头盔 0xff-unknow表示未知,算法支持未检出
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           //保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_ADDINFO
        {
            NET_VCA_RECT struFacePicRect;//人脸矩形框,该坐标为人脸小图(头肩照)中人脸的坐标
            public int iSwingAngle;//旋转角, -90~90度
            public int iTiltAngle;//俯仰角, -90~90度
            public uint dwPupilDistance;//瞳距,范围为：最小值为10像素,最大值为当前分辨率宽度/1.6
            public byte byBlockingState;//目标遮挡状态， 0-表示“未知”（算法不支持）,1~无遮挡,2~瞬时轻度遮挡，3~持续轻度遮挡，4~严重遮挡
            public byte byFaceSnapThermometryEnabled;//人脸抓拍测温使能 1-开启 0-关闭
            public byte byIsAbnomalTemperature;//人脸抓拍测温是否温度异常 1-是 0-否
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)
            NET_DVR_TIME_EX struEnterTime;   // 最佳抓拍下进入时间
            NET_DVR_TIME_EX struExitTime;    // 最佳抓拍下离开时间
            public float fFaceTemperature; // 人脸温度（ - 20.0℃~150.0℃，精确到小数点后1位）
            public float fAlarmTemperature;// 测温报警警阈值（精确到小数点后1位）
            public uint dwThermalPicLen;//热成像图片长度
            public IntPtr pThermalPicBuff;// 热成像图片指针
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 65, ArraySubType = UnmanagedType.I1)]
            public byte[] szCustomChanID;// 自定义监控点通道号  string  max.len = 64
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 399, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;// 保留字节
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_RESULT
        {
            public uint dwSize;             // 结构大小
            public uint dwRelativeTime;     // 相对时标
            public uint dwAbsTime;            // 绝对时标
            public uint dwFacePicID;       //人脸图ID
            public uint dwFaceScore;        //人脸评分,0-100
            public NET_VCA_TARGET_INFO struTargetInfo;    //报警目标信息
            public NET_VCA_RECT struRect;      //人脸子图区域
            public NET_VCA_DEV_INFO struDevInfo;        //前端设备信息
            public uint dwFacePicLen;        //人脸子图的长度，为0表示没有图片，大于0表示有图片
            public uint dwBackgroundPicLen; //背景图的长度，为0表示没有图片，大于0表示有图片
            public byte bySmart;             //0- IDS设备返回0,1-Smart设备返回
            public byte byAlarmEndMark;//报警结束标记0-保留，1-结束标记（该字段结合人脸ID字段使用，表示该ID对应的下报警结束，主要提供给NVR使用，用于判断报警结束，提取识别图片数据中，清晰度最高的图片）
            public byte byRepeatTimes;   //重复报警次数，0-无意义
            public byte byUploadEventDataType;//人脸图片数据长传方式：0-二进制数据，1-URL
            public NET_VCA_HUMAN_FEATURE struFeature; //人体属性
            public float fStayDuration;     //停留画面中时间(单位:秒)
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sStorageIP;        //存储服务IP地址
            public ushort wStoragePort;            //存储服务端口号
            public ushort wDevInfoIvmsChannelEx;            //与NET_VCA_DEV_INFO里的byIvmsChannel含义相同，能表示更大的值。老客户端用byIvmsChannel能继续兼容，但是最大到255。新客户端版本请使用wDevInfoIvmsChannelEx。
            /*人脸子图图片质量评估等级，0-低等质量,1-中等质量,2-高等质量,
            新增人脸抓拍质量评分机制与原有的人脸评分区别：
            原有的人脸评分是在人脸抓拍中实现，目的是确保一次人脸抓拍中获取最好的图像效果，是一个相对图像质量的评分；新增人脸抓拍质量评分机制是针对已经抓拍完成的人脸图片，使用图像算法对图像中人脸，人眼，肩宽等相对重要的信息进行分析，按照统一的标准对人脸图片效果质量进行评分*/
            public byte byFacePicQuality;
            public byte byUIDLen;        //上传报警的标识长度
            public byte byLivenessDetectionStatus;   // 活体检测状态：0-保留，1-未知(检测失败)，2-非真人人脸，3-真人人脸，4-未开启活体检测
            public byte byAddInfo;      //附加信息标识位（即是否有NET_VCA_FACESNAP_ADDINFO结构体）,0-无附加信息, 1-有附加信息
            public IntPtr pUIDBuffer;   //标识指针
            public IntPtr pAddInfoBuffer;   //附加信息指针,指向NET_VCA_FACESNAP_ADDINFO结构体
            public byte byTimeDiffFlag;   //时差字段是否有效  0-时差无效， 1-时差有效
            public byte cTimeDifferenceH;   //与UTC的时差（小时），-12 ... +14， +表示东区,，byTimeDiffFlag为1时有效
            public byte cTimeDifferenceM;   //与UTC的时差（分钟），-30, 30, 45， +表示东区，byTimeDiffFlag为1时有效
            public byte byBrokenNetHttp;   //断网续传标志位，0-不是重传数据，1-重传数据
            public IntPtr pBuffer1;          //人脸子图的图片数据
            public IntPtr pBuffer2;          //背景图的图片数据
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_V30
        {
            public ushort wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
            public ushort wMilliSec;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PICTURE_INFO
        {
            public uint dwDataLen;              //媒体数据长度
            public byte byType;                           // 0:车牌图;1:场景图;2:合成图;3:码流
            public byte byDataType;                       // 0-数据直接上传; 1-云存储服务器URL(3.7Ver)原先的图片数据变成URL数据，图片长度变成URL长度
            public byte byCloseUpType;                    //特写图类型，0-保留,1-非机动车,2-行人
            public byte byPicRecogMode;                   //图片背向识别：0-正向车牌识别，1-背向识别(尾牌识别)
            public uint dwRedLightTime;                   //经过的红灯时间  （s）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;                 //绝对时间点,yyyymmddhhmmssxxx,e.g.20090810235959999  最后三位为毫秒数
            public NET_VCA_RECT struPlateRect;         //车牌位置
            public NET_VCA_RECT struPlateRecgRect;   //牌识区域坐标
            public IntPtr pBuffer;     //数据指针
            public uint dwUTCTime;//UTC时间定义
            public byte byCompatibleAblity; //兼容能力字段 0表示无效，1表示有效; bit0-表示dwUTCTime字段有效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;              //保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PLATE_RESULT
        {
            public uint dwSize;
            public uint dwMatchNo;
            public byte byGroupNum;
            public byte byPicNo;
            public byte bySecondCam;    //是否第二相机抓拍（如远近景抓拍的远景相机，或前后抓拍的后相机，特殊项目中会用到）
            public byte byFeaturePicNo; //闯红灯电警，取第几张图作为特写图,0xff-表示不取
            public byte byDriveChan;                //触发车道号
            public byte byVehicleType;     //0- 未知，1-客车，2-货车，3-轿车，4-面包车，5-小货车
            public byte byDetSceneID;//检测场景号[1,4], IPC默认是0
            public byte byVehicleAttribute;//车辆属性，按位表示，0- 无附加属性(普通车)，bit1- 黄标车(类似年检的标志)，bit2- 危险品车辆，值：0- 否，1- 是                      //保留
            public ushort wIllegalType;       //违章类型采用国标定义
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byIllegalSubType;   //违章子类型
            public byte byPostPicNo;    //违章时取第几张图片作为卡口图,0xff-表示不取
            public byte byChanIndex;                //通道号（保留）
            public ushort wSpeedLimit;            //限速上限（超速时有效）
            public byte byChanIndexEx;      //byChanIndexEx*256+byChanIndex表示真实通道号。
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_PLATE_INFO struPlateInfo;       //车牌信息结构
            public NET_DVR_VEHICLE_INFO struVehicleInfo;        //车辆信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSiteID;          //监测点编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byDeviceID;                                   //设备编号
            public byte byDir;                //监测方向，1-上行，2-下行，3-双向，4-由东向西，5-由南向北,6-由西向东，7-由北向南，8-其它
            public byte byDetectType;    //检测方式,1-地感触发，2-视频触发，3-多帧识别，4-雷达触发
            public byte byRelaLaneDirectionType;
            public byte byCarDirectionType; //车辆具体行驶的方向，0表示从上往下，1表示从下往上（根据实际车辆的行驶方向来的区分）
            //当wIllegalType参数为空时，使用该参数。若wIllegalType参数为有值时，以wIllegalType参数为准，该参数无效。
            public uint dwCustomIllegalType; //违章类型定义(用户自定义)
            /*为0~数字格式时，为老的违章类型，wIllegalType、dwCustomIllegalType参数生效，赋值国标违法代码。
              为1~字符格式时，pIllegalInfoBuf参数生效。老的违章类型，wIllegalType、dwCustomIllegalType参数依然赋值国标违法代码*/
            public IntPtr pIllegalInfoBuf;    //违法代码字符信息结构体指针；指向NET_ITS_ILLEGAL_INFO 
            public byte byIllegalFromatType; //违章信息格式类型； 0~数字格式， 1~字符格式
            public byte byPendant;// 0-表示未知,1-车窗有悬挂物，2-车窗无悬挂物
            public byte byDataAnalysis;            //0-数据未分析, 1-数据已分析
            public byte byYellowLabelCar;        //0-表示未知, 1-非黄标车,2-黄标车
            public byte byDangerousVehicles;    //0-表示未知, 1-非危险品车,2-危险品车
            //以下字段包含Pilot字符均为主驾驶，含Copilot字符均为副驾驶
            public byte byPilotSafebelt;//0-表示未知,1-系安全带,2-不系安全带
            public byte byCopilotSafebelt;//0-表示未知,1-系安全带,2-不系安全带
            public byte byPilotSunVisor;//0-表示未知,1-不打开遮阳板,2-打开遮阳板
            public byte byCopilotSunVisor;//0-表示未知, 1-不打开遮阳板,2-打开遮阳板
            public byte byPilotCall;// 0-表示未知, 1-不打电话,2-打电话
            //0-开闸，1-未开闸 ()
            public byte byBarrierGateCtrlType;
            public byte byAlarmDataType;//0-实时数据，1-历史数据
            public NET_DVR_TIME_V30 struSnapFirstPicTime;//端点时间(ms)（抓拍第一张图片的时间）
            public uint dwIllegalTime;//违法持续时间（ms） = 抓拍最后一张图片的时间 - 抓拍第一张图片的时间
            public uint dwPicNum;            //图片数量（与picGroupNum不同，代表本条信息附带的图片数量，图片信息由struVehicleInfoEx定义   
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_PICTURE_INFO[] struPicInfo;                //图片信息,单张回调，最多6张图，由序号区分            
        }

        //字符叠加配置条件参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAPCFG_COND
        {
            public uint dwSize;
            public uint dwChannel;//通道号 
            public uint dwConfigMode;//配置模式：0- 终端，1- 前端(直连前端或终端接前端)
            public byte byPicModeType;//0-表示小图(独立图)，1-表示大图(合成图)
            /*
            0表示关联 抓拍MPR模式（多帧触发抓拍 IPC使用）
            1 表示关联 抓拍 HVT 模式（混卡IPC使用）
*/
            public byte byRelateType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        public enum ITS_OVERLAP_ITEM_TYPE
        {
            OVERLAP_ITEM_NULL = 0,          //0-未知
            OVERLAP_ITEM_SITE,                //1-地点
            OVERLAP_ITEM_ROADNUM,             //2-路口编号
            OVERLAP_ITEM_INSTRUMENTNUM,       //3-设备编号
            OVERLAP_ITEM_DIRECTION,           //4-方向编号
            OVERLAP_ITEM_DIRECTIONDESC,       //5-方向
            OVERLAP_ITEM_LANENUM,             //6-车道号
            OVERLAP_ITEM_LANEDES,             //7-车道
            OVERLAP_ITEM_CAPTIME,             //8-抓拍时间(不带毫秒)
            OVERLAP_ITEM_CAPTIME_MILLSECOND,  //9-抓拍时间(带毫秒)
            OVERLAP_ITEM_PLATENUM,            //10-车牌号
            OVERLAP_ITEM_CARCOLOR,            //11-车身颜色
            OVERLAP_ITEM_CARTYPE,             //12-车辆类型
            OVERLAP_ITEM_CARBRAND,            //13-车辆品牌
            OVERLAP_ITEM_CARSPEED,            //14-车辆速度
            OVERLAP_ITEM_SPEEDLIMIT,          //15-限速标志
            OVERLAP_ITEM_CARLENGTH,           //16-车辆长度1~99m
            OVERLAP_ITEM_ILLEGALNUM,          //17-违法代码(违法代码叠加应该没用的，应该直接叠加违法信息，比如正常、低速、超速、逆行、闯红灯、占道、压黄线等)
            OVERLAP_ITEM_MONITOR_INFO,      //18-监测点信息
            OVERLAP_ITEM_ILLEGALDES,          //19-违法行为
            OVERLAP_ITEM_OVERSPEED_PERCENT,    //20-超速比
            OVERLAP_ITEM_RED_STARTTIME,           //21-红灯开始时间
            OVERLAP_ITEM_RED_STOPTIME,        //22-红灯结束时间
            OVERLAP_ITEM_RED_DURATION,        //23-红灯已亮时间
            OVERLAP_ITEM_SECUNITY_CODE,        //24-防伪码
            OVERLAP_ITEM_CAP_CODE,        //25-抓拍编号
            OVERLAP_ITEM_SEATBELT,      //26-安全带  
            OVERLAP_ITEM_MONITOR_ID,    //27-监测点编号
            OVERLAP_ITEM_SUN_VISOR,     //28-遮阳板 
            OVERLAP_ITEM_LANE_DIRECTION,  //29-车道行驶方向 
            OVERLAP_ITEM_LICENSE_PLATE_COLOR,  // 30-车牌颜色
            OVERLAP_ITEM_SCENE_NUMBER,  //31-场景编号
            OVERLAP_ITEM_SCENE_NAME,   //32-场景名称
            OVERLAP_ITEM_YELLOW_SIGN_CAR,  //33-黄标车
            OVERLAP_ITEM_DANGEROUS_CAR,    //34-危险品车
            OVERLAP_ITEM_CAR_SUBBRAND,  //35-车辆子品牌
            OVERLAP_ITEM_CAR_DIRECTION,  //36-车辆方向
            OVERLAP_ITEM_PENDANT,  //37-车窗悬挂物
            OVERLAP_ITEM_CALL,  //38-打电话
            OVERLAP_ITEM_CAR_VALIDITY  //39-置信度
        }


        //单条字符叠加信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_SINGLE_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public byte byItemType;//类型
            public byte byChangeLineNum;//叠加项后的换行数，取值范围：[0,10]，默认：0 
            public byte bySpaceNum;//叠加项后的空格数，取值范围：[0-255]，默认：0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //字符串参数配置结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_OVERLAP_ITEM_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_OVERLAP_SINGLE_ITEM_PARAM[] struSingleItem;//字符串内容信息
            public uint dwLinePercent;
            public uint dwItemsStlye;
            public ushort wStartPosTop;
            public ushort wStartPosLeft;
            public ushort wCharStyle;
            public ushort wCharSize;
            public ushort wCharInterval;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwForeClorRGB;//前景色的RGB值，bit0~bit7: B，bit8~bit15: G，bit16~bit23: R，默认：x00FFFFFF-白
            public uint dwBackClorRGB;//背景色的RGB值，只对图片外叠加有效，bit0~bit7: B，bit8~bit15: G，bit16~bit23: R，默认：x00000000-黑 
            public byte byColorAdapt;//颜色是否自适应：0-否，1-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //字符叠加内容信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_INFO_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] bySite;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRoadNum;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byInstrumentNum;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirection;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirectionDesc;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byLaneDes;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//这里保留音频的压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite1;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite2;//这里保留音频的压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        //字符叠加配置条件参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_CFG
        {
            public uint dwSize;
            public byte byEnable;//是否启用：0- 不启用，1- 启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//这里保留音频的压缩参数
            public NET_ITS_OVERLAP_ITEM_PARAM struOverLapItem;//字符串参数
            public NET_ITS_OVERLAP_INFO_PARAM struOverLapInfo;//字符串内容信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        //报警布防参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM
        {
            public uint dwSize;
            public byte byLevel;//布防优先级：0- 一等级（高），1- 二等级（中），2- 三等级（低，保留）
            public byte byAlarmInfoType;//上传报警信息类型（智能交通摄像机支持）：0- 老报警信息（NET_DVR_PLATE_RESULT），1- 新报警信息(NET_ITS_PLATE_RESULT) 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//这里保留音频的压缩参数 
            public byte byFaceAlarmDetection;//1-表示人脸侦测报警扩展(INTER_FACE_DETECTION),0-表示原先支持结构(INTER_FACESNAP_RESULT)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//这里保留音频的压缩参数 
        }

        // 报警布防参数V50结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM_V50
        {
            public uint dwSize;
            public byte byLevel; //布防优先级，0-一等级（高），1-二等级（中），2-三等级（低）
            public byte byAlarmInfoType; //上传报警信息类型（抓拍机支持），0-老报警信息（NET_DVR_PLATE_RESULT），1-新报警信息(NET_ITS_PLATE_RESULT)2012-9-28
            public byte byRetAlarmTypeV40; //0--返回NET_DVR_ALARMINFO_V30或NET_DVR_ALARMINFO, 1--设备支持NET_DVR_ALARMINFO_V40则返回NET_DVR_ALARMINFO_V40，不支持则返回NET_DVR_ALARMINFO_V30或NET_DVR_ALARMINFO
            public byte byRetDevInfoVersion; //CVR上传报警信息回调结构体版本号 0-COMM_ALARM_DEVICE， 1-COMM_ALARM_DEVICE_V40
            public byte byRetVQDAlarmType; //VQD报警上传类型，0-上传报报警NET_DVR_VQD_DIAGNOSE_INFO，1-上传报警NET_DVR_VQD_ALARM
            //1-表示人脸侦测报警扩展(INTER_FACE_DETECTION),0-表示原先支持结构(INTER_FACESNAP_RESULT)
            public byte byFaceAlarmDetection;
            //Bit0- 表示二级布防是否上传图片: 0-上传，1-不上传
            //Bit1- 表示开启数据上传确认机制；0-不开启，1-开启
            //Bit6- 表示雷达检测报警(eventType:radarDetection)是否开启实时上传；0-不开启，1-开启（用于web插件实时显示雷达目标轨迹）
            public byte bySupport;
            //断网续传类型 
            //bit0-车牌检测（IPC） （0-不续传，1-续传）
            //bit1-客流统计（IPC）  （0-不续传，1-续传）
            //bit2-热度图统计（IPC） （0-不续传，1-续传）
            //bit3-人脸抓拍（IPC） （0-不续传，1-续传）
            //bit4-人脸对比（IPC） （0-不续传，1-续传）
            //bit5-JSON报警透传（IPC） （0-不续传，1-续传）
            //bit6-热度图按人员停留时间统计数据上传事件（0-不续传，1-续传）
            //bit7-热度图按人数统计数据上传事件的确认机制（0-不续传，1-续传）
            public byte byBrokenNetHttp;
            public ushort wTaskNo;    //任务处理号 和 (上传数据NET_DVR_VEHICLE_RECOG_RESULT中的字段dwTaskNo对应 同时 下发任务结构 NET_DVR_VEHICLE_RECOG_COND中的字段dwTaskNo对应)
            public byte byDeployType;    //布防类型：0-客户端布防，1-实时布防
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byAlarmTypeURL;//bit0-表示人脸抓拍报警上传（INTER_FACESNAP_RESULT）；0-表示二进制传输，1-表示URL传输（设备支持的情况下，设备支持能力根据具体报警能力集判断,同时设备需要支持URL的相关服务，当前是”云存储“）
            //bit1-表示EVENT_JSON中图片数据长传类型；0-表示二进制传输，1-表示URL传输（设备支持的情况下，设备支持能力根据具体报警能力集判断）
            //bit2 - 人脸比对(报警类型为COMM_SNAP_MATCH_ALARM)中图片数据上传类型：0 - 二进制传输，1 - URL传输
            //bit3 - 行为分析(报警类型为COMM_ALARM_RULE)中图片数据上传类型：0 - 二进制传输，1 - URL传输，本字段设备是否支持，对应软硬件能力集中<isSupportBehaviorUploadByCloudStorageURL>节点是否返回且为true
            public byte byCustomCtrl;//Bit0- 表示支持副驾驶人脸子图上传: 0-不上传,1-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
        }

        //道闸控制参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_CFG
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byLaneNo;
            public byte byBarrierGateCtrl;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int MAX_RELAY_NUM = 12;
        public const int MAX_IOIN_NUM = 8;
        public const int MAX_VEHICLE_TYPE_NUM = 8;

        public const int NET_DVR_GET_ENTRANCE_PARAMCFG = 3126; //获取出入口控制参数
        public const int NET_DVR_SET_ENTRANCE_PARAMCFG = 3127; //设置出入口控制参数

        //出入口控制条件
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_COND
        {
            public byte byLaneNo;//车道号：0- 表示无效值(设备需要做有效值判断)，1- 车道1
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //继电器关联配置
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RELAY_PARAM
        {
            public byte byAccessDevInfo;//0-不接入设备，1-开道闸、2-关道闸、3-停道闸、4-报警信号、5-常亮灯
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //车辆信息管控参数
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_VEHICLE_CONTROL
        {
            public byte byGateOperateType;//操作类型：0- 无操作，1- 开道闸
            public byte byRes1;
            public ushort wAlarmOperateType; //报警处理类型：0- 无操作，bit0- 继电器输出报警，bit1- 布防上传报警，bit3- 告警主机上传，值：0-表示关，1-表示开，可复选
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //出入口控制参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ENTRANCE_CFG
        {
            public uint dwSize;
            public byte byEnable;
            public byte byBarrierGateCtrlMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwRelateTriggerMode;
            public uint dwMatchContent;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RELAY_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RELAY_PARAM[] struRelayRelateInfo;//继电器关联配置信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IOIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byGateSingleIO;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLE_TYPE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VEHICLE_CONTROL[] struVehicleCtrl;//车辆信息管控
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MANUALSNAP
        {
            public byte byOSDEnable;
            public byte byLaneNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }




        /********************************接口参数结构(end)*********************************/


        /********************************SDK接口函数声明*********************************/

        /*********************************************************
        Function:    NET_DVR_Init
        Desc:  初始化SDK，调用其他SDK函数的前提。
        Input:    
        Output:    
        Return:    TRUE表示成功，FALSE表示失败。
        **********************************************************/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();

        /*********************************************************
        Function:    NET_DVR_Cleanup
        Desc:  释放SDK资源，在结束之前最后调用
        Input:    
        Output:    
        Return:    TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);

        /*********************************************************
        Function:    EXCEPYIONCALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void EXCEPYIONCALLBACK(uint dwType, int lUserID, int lHandle, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetExceptionCallBack_V30(uint nMessage, IntPtr hWnd, EXCEPYIONCALLBACK fExceptionCallBack, IntPtr pUser);


        /*********************************************************
        Function:    MESSCALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACK(int lCommand, string sDVRIP, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack(MESSCALLBACK fMessCallBack);

        /*********************************************************
        Function:    MESSCALLBACKEX
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACKEX(int iCommand, int iUserID, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_EX(MESSCALLBACKEX fMessCallBack_EX);

        /*********************************************************
        Function:    MESSCALLBACKNEW
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACKNEW(int lCommand, string sDVRIP, string pBuf, uint dwBufLen, ushort dwLinkDVRPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(MESSCALLBACKNEW fMessCallBack_NEW);

        /*********************************************************
        Function:    MESSAGECALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSAGECALLBACK(int lCommand, System.IntPtr sDVRIP, System.IntPtr pBuf, uint dwBufLen, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack(MESSAGECALLBACK fMessageCallBack, uint dwUser);


        /*********************************************************
        Function:    MSGCallBack
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void MSGCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V30(MSGCallBack fMessageCallBack, IntPtr pUser);
        public delegate bool MSGCallBack_V31(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V31(MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V50(int iIndex, MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V51(int iIndex, MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetReconnect(uint dwInterval, int bEnableRecon);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKBuildVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_IsSupport();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartListen(string sLocalIP, ushort wLocalPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartListen_V30(String sLocalIP, ushort wLocalPort, MSGCallBack DataCallback, IntPtr pUserData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen_V30(Int32 lListenHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO lpDeviceInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(int iUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetLastError();

        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_GetErrorMsg(ref int pErrorNo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetShowMode(uint dwShowType, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr(string sServerIP, ushort wServerPort, string sDVRName, ushort wDVRNameLen, string sDVRSerialNumber, ushort wDVRSerialLen, IntPtr pGetIP);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr_EX(string sServerIP, ushort wServerPort, ref byte sDVRName, ushort wDVRNameLen, ref byte sDVRSerialNumber, ushort wDVRSerialLen, string sGetIP, ref uint dwPort);

        //预览相关接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_RealPlay(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_SDK_RealPlay(int iUserLogID, ref NET_SDK_CLIENTINFO lpDVRClientInfo);
        /*********************************************************
        Function:    REALDATACALLBACK
        Desc:  预览回调
        Input:    lRealHandle 当前的预览句柄 
                dwDataType 数据类型
                pBuffer 存放数据的缓冲区指针 
                dwBufSize 缓冲区大小 
                pUser 用户数据 
        Output:    
        Return:    void
        **********************************************************/
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]

        /*********************************************************
        Function:    NET_DVR_RealPlay_V30
        Desc:  实时预览。
        Input:    lUserID [in] NET_DVR_Login()或NET_DVR_Login_V30()的返回值 
                lpClientInfo [in] 预览参数 
                cbRealDataCallBack [in] 码流数据回调函数 
                pUser [in] 用户数据 
                bBlocked [in] 请求码流过程是否阻塞：0－否；1－是 
        Output:    
        Return:    1表示失败，其他值作为NET_DVR_StopRealPlay等函数的句柄参数
        **********************************************************/
        public static extern int NET_DVR_RealPlay_V30(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser, UInt32 bBlocked);

        /*********************************************************
        Function:    NET_DVR_RealPlay_V40
        Desc:  实时预览。
        Input:    lUserID [in] NET_DVR_Login()或NET_DVR_Login_V30()的返回值 
                lpClientInfo [in] 预览参数 
                cbRealDataCallBack [in] 码流数据回调函数 
                pUser [in] 用户数据 
                bBlocked [in] 请求码流过程是否阻塞：0－否；1－是 
        Output:    
        Return:    1表示失败，其他值作为NET_DVR_StopRealPlay等函数的句柄参数
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_V40(int iUserID, ref NET_DVR_PREVIEWINFO lpPreviewInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);

        /*********************************************************
        Function:    NET_DVR_StopRealPlay
        Desc:  停止预览。
        Input:    lRealHandle [in] 预览句柄，NET_DVR_RealPlay或者NET_DVR_RealPlay_V30的返回值 
        Output:    
        Return:    
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int iRealHandle);

        /*********************************************************
        Function:    DRAWFUN
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void DRAWFUN(int lRealHandle, IntPtr hDc, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_CaptureJPEGPicture_WithAppendData(int m_lUserID, int lChannel, IntPtr struJpegWithAppendData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayerBufNumber(Int32 lRealHandle, uint dwBufNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ThrowBFrame(Int32 lRealHandle, uint dwNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(uint dwMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(Int32 lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData(Int32 lRealHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopSaveRealData(Int32 lRealHandle);

        /*********************************************************
        Function:    REALDATACALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void SETREALDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(int lRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint dwUser);

        /*********************************************************
        Function:    STDDATACALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void STDDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetStandardDataCallBack(int lRealHandle, STDDATACALLBACK fStdDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_STDControl(int lUserID, uint dwCommand, ref NET_DVR_STD_CONTROL lpControlParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture(Int32 lRealHandle, string sPicFileName);

        //动态生成I帧
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(Int32 lUserID, Int32 lChannel);//主码流

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrameSub(Int32 lUserID, Int32 lChannel);//子码流

        //云台控制相关接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl_Other(Int32 lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(Int32 lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(Int32 lUserID, Int32 lChannel, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(Int32 lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(int lUserID, int lChannel, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_EX(int lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_EX(int lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_EX(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_EX(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_EX(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, int dwPTZCommand, int dwStop, int dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_EX(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCruise(int lUserID, int lChannel, int lCruiseRoute, ref NET_DVR_CRUISE_RET lpCruiseRet);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_EX(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);

        //文件查找与回放
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile(int lFindHandle, ref NET_DVR_FIND_DATA lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(int lFindHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_V30(int lFindHandle, ref NET_DVR_FINDDATA_V30 lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_V30(int lUserID, ref NET_DVR_FILECOND pFindCond);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose_V30(int lFindHandle);

        //2007-04-16增加查询结果带卡号的文件查找
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_Card(int lFindHandle, ref NET_DVR_FINDDATA_CARD lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_Card(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockFileByName(int lUserID, string sLockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnlockFileByName(int lUserID, string sUnlockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, System.IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackReverseByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

        /*********************************************************
        Function:    PLAYDATACALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void PLAYDATACALLBACK(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(int lPlayHandle, PLAYDATACALLBACK fPlayDataCallBack, uint dwUser);

        public delegate void PLAYDATACALLBACK_V40(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack_V40(int lPlayHandle, PLAYDATACALLBACK_V40 fPlayDataCallBack_V40, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(int lPlayHandle, ref NET_DVR_TIME lpOsdTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPos(int lPlayHandle);

        //升级
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeProgress(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetNetworkEnvironment(uint dwEnvironmentLevel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_AdapterUpgrade(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_VcalibUpgrade(int lUserID, int Channel, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
         public static extern int NET_DVR_Upgrade_V40(int lUserID, int dwUpgradeType, string sFileName, IntPtr pInbuffer, int dwBufferLen);



        //远程格式化硬盘
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, ref int pCurrentFormatDisk, ref int pCurrentDiskPos, ref int pFormatStatic);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);

        //报警
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan(int lAlarmHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V30(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID, ref NET_DVR_SETUPALARM_PARAM lpSetupParam);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V50(int lUserID, ref NET_DVR_SETUPALARM_PARAM_V50 lpSetupParam, IntPtr pSub, uint dwSubSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);

        //语音对讲
        /*********************************************************
        Function:    VOICEDATACALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void VOICEDATACALLBACK(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        /*********************************************************
        Function:    VOICEDATACALLBACKV30
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void VOICEDATACALLBACKV30(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_V30(int lUserID, uint dwVoiceChan, bool bNeedCBNoEncData, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(int lVoiceComHandle);

        //语音转发
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR_V30(int lUserID, uint dwVoiceChan, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, string pSendBuf, uint dwBufSize);

        //语音广播
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();

        /*********************************************************
        Function:    VOICEAUDIOSTART
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void VOICEAUDIOSTART(string pRecvDataBuffer, uint dwBufSize, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart_V30(VOICEAUDIOSTART fVoiceAudioStart, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AddDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_AddDVR_V30(int lUserID, uint dwVoiceChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR_V30(int lVoiceHandle);


        //透明通道设置
        /*********************************************************
        Function:    SERIALDATACALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void SERIALDATACALLBACK(int lSerialHandle, string pRecvDataBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStart(int lUserID, int lSerialPort, SERIALDATACALLBACK fSerialDataCallBack, uint dwUser);

        //485作为透明通道时，需要指明通道号，因为不同通道号485的设置可以不同(比如波特率)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendToSerialPort(int lUserID, uint dwSerialPort, uint dwSerialIndex, string pSendBuf, uint dwBufSize);

        //解码 nBitrate = 16000
        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_InitG722Decoder(int nBitrate);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Decoder(IntPtr pDecHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG722Frame(IntPtr pDecHandle, ref byte pInBuffer, ref byte pOutBuffer);

        //编码
        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Encoder();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG722Frame(IntPtr pEncodeHandle, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Encoder(IntPtr pEncodeHandle);

        //远程控制本地显示
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(int lUserID, int lKeyIndex);

        //远程控制设备端手动录像
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID, int lChannel, int lRecordType);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID, int lChannel);

        //解码卡
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDevice_Card(ref int pDeviceTotalChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDevice_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDraw_Card(IntPtr hParent, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDraw_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_Card(int lUserID, ref NET_DVR_CARDINFO lpCardInfo, int lChannelNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetPara_Card(int lRealHandle, ref NET_DVR_DISPLAY_PARA lpDisplayPara);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClearSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVolume_Card(int lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AudioPreview_Card(int lRealHandle, int bEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetCardLastError_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_GetChanHandle_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture_Card(int lRealHandle, string sPicFileName);

        //获取解码卡序列号此接口无效，改用GetBoardDetail接口获得(2005-12-08支持)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSerialNum_Card(int lChannelNum, ref uint pDeviceSerialNo);

        //日志
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog(int lLogHandle, ref NET_DVR_LOG lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose(int lLogHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_V30(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, bool bOnlySmart);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_V30(int lLogHandle, ref NET_DVR_LOG_V30 lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose_V30(int lLogHandle);

        //截止2004年8月5日,共113个接口
        //ATM DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByCard(int lUserID, int lChannel, uint dwFileType, int nFindType, ref byte sCardNumber, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);


        //2005-09-15
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sPicFileName);

        //JPEG抓图到内存
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sJpegPicBuffer, uint dwPicSize, ref uint lpSizeReturned);

        //2006-02-16
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetRealPlayerIndex(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPlayerIndex(int lPlayHandle);

        //2006-08-28 704-640 缩放配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG(int lUserID, uint dwScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG(int lUserID, ref uint lpOutScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        //2006-08-28 ATM机端口设置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetATMPortCFG(int lUserID, ushort wATMPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetATMPortCFG(int lUserID, ref ushort LPOutATMPort);

        //2006-11-10 支持显卡辅助输出
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDDrawDeviceTotalNums();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDDrawDevice(int lPlayPort, uint nDeviceNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn(int lRealHandle, ref NET_DVR_POINT_FRAME pStruPointFrame);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn_EX(int lUserID, int lChannel, ref NET_DVR_POINT_FRAME pStruPointFrame);

        //解码设备DS-6001D/DS-6001F
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecode(int lUserID, int lChannel, ref NET_DVR_DECODERINFO lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecode(int lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecoderState(int lUserID, int lChannel, ref NET_DVR_DECODERSTATE lpDecoderState);

        //2005-08-01
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecPlayBackCtrl(int lUserID, int lChannel, uint dwControlCode, uint dwInValue, ref uint LPOutValue, ref NET_DVR_PLAYREMOTEFILE lpRemoteFileInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlDec(int lUserID, int lChannel, uint dwControlCode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlScreen(int lUserID, int lChannel, uint dwControl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecCurLinkStatus(int lUserID, int lChannel, ref NET_DVR_DECSTATUS lpDecStatus);

        //多路解码器
        //2007-11-30 V211支持以下接口 //11
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DYNAMIC_DEC lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopDynamic(int lUserID, uint dwDecChanNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecEnable(int lUserID, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_STATUS lpInter);

        //2007-12-22 增加支持接口 //18
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlay(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlayControl(int lUserID, uint dwDecChanNum, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetRemotePlayStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS lpOuter);

        //2009-4-13 新增
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic_V30(int lUserID, uint dwDecChanNum, ref NET_DVR_PU_STREAM_CFG lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagDEC_MATRIX_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);


        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_MatrixStartPassiveDecode(int lUserID, uint dwDecChanNum, ref tagNET_MATRIX_PASSIVEMODE lpPassiveMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSendData(int lPassiveHandle, System.IntPtr pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopPassiveDecode(int lPassiveHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadLogo(int lUserID, uint dwDispChanNum, ref tagNET_DVR_DISP_LOGOCFG lpDispLogoCfg, System.IntPtr sLogoBuffer);

        public const int NET_DVR_SHOWLOGO = 1;/*显示LOGO*/
        public const int NET_DVR_HIDELOGO = 2;/*隐藏LOGO*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LogoSwitch(int lUserID, uint dwDecChan, uint dwLogoSwitch);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus(int lUserID, ref tagNET_DVR__DECODER_WORK_STATUS lpDecoderCfg);

        /*显示通道命令码定义*/
        //上海世博 定制
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterPlayBackDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);

        public const int DISP_CMD_ENLARGE_WINDOW = 1;    /*显示通道放大某个窗口*/
        public const int DISP_CMD_RENEW_WINDOW = 2;    /*显示通道窗口还原*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixDiaplayControl(int lUserID, uint dwDispChanNum, uint dwDispChanCmd, uint dwCmdParam);

        //end
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshPlay(int lPlayHandle);

        //恢复默认值
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);

        //保存参数
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(int lUserID);

        //重启
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);

        //关闭DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);

        //参数配置 begin
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, System.IntPtr lpInBuffer, uint dwInBufferSize);

        //报警主机设备用户配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        //批量参数配置
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpOutBuffer, uint dwOutBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpInParamBuffer, uint dwInParamBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RemoteControl(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState_V30(int lUserID, IntPtr pWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState(int lUserID, ref NET_DVR_WORKSTATE lpWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVideoEffect(int lUserID, int lChannel, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVideoEffect(int lUserID, int lChannel, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Getframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Setframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAtmProtocol(int lUserID, ref tagNET_DVR_ATM_PROTOCOL lpAtmProtocol);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut_V30(int lUserID, IntPtr lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut(int lUserID, ref NET_DVR_ALARMOUTSTATUS lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);

        //视频参数调节
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(int lRealHandle, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        //配置文件
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_V30(int lUserID, string sOutBuffer, uint dwOutSize, ref uint pReturnSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_EX(int lUserID, string sOutBuffer, uint dwOutSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile_EX(int lUserID, string sInBuffer, uint dwInSize);

        //启用日志文件写入接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLogToFile(int bLogEnable, string strLogDir, bool bAutoDel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKState(ref NET_DVR_SDKSTATE pSDKState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKAbility(ref NET_DVR_SDKABL pSDKAbl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZProtocol(int lUserID, ref NET_DVR_PTZCFG pPtzcfg);

        //前面板锁定
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnLockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpOutBuffer, uint dwOutBufferSize);

		[DllImport(@"HCNetSDK.dll")]
        public static extern long NET_DVR_Upgrade_V50(int lUserID, IntPtr lpUpgradeParam);
        [DllImport(@"HCNetSDK.dll")]
        public static extern long NET_DVR_GetUpgradeStep(int lUpgradeHandle, IntPtr pSubProgress);

        //SDK_V222
        //智能设备类型
        public const int DS6001_HF_B = 60;//行为分析：DS6001-HF/B
        public const int DS6001_HF_P = 61;//车牌识别：DS6001-HF/P
        public const int DS6002_HF_B = 62;//双机跟踪：DS6002-HF/B
        public const int DS6101_HF_B = 63;//行为分析：DS6101-HF/B
        public const int IDS52XX = 64;//智能分析仪IVMS
        public const int DS9000_IVS = 65;//9000系列智能DVR
        public const int DS8004_AHL_A = 66;//智能ATM, DS8004AHL-S/A
        public const int DS6101_HF_P = 67;//车牌识别：DS6101-HF/P

        //能力获取命令
        public const int VCA_DEV_ABILITY = 256;//设备智能分析的总能力
        public const int VCA_CHAN_ABILITY = 272;//行为分析能力
        public const int MATRIXDECODER_ABILITY = 512;//多路解码器显示、解码能力
        //获取/设置大接口参数配置命令
        //车牌识别（NET_VCA_PLATE_CFG）
        public const int NET_DVR_SET_PLATECFG = 150;//设置车牌识别参数
        public const int NET_DVR_GET_PLATECFG = 151;//获取车牌识别参数
        //行为对应（NET_VCA_RULECFG）
        public const int NET_DVR_SET_RULECFG = 152;//设置行为分析规则
        public const int NET_DVR_GET_RULECFG = 153;//获取行为分析规则

        //双摄像机标定参数（NET_DVR_LF_CFG）
        public const int NET_DVR_SET_LF_CFG = 160;//设置双摄像机的配置参数
        public const int NET_DVR_GET_LF_CFG = 161;//获取双摄像机的配置参数

        //智能分析仪取流配置结构
        public const int NET_DVR_SET_IVMS_STREAMCFG = 162;//设置智能分析仪取流参数
        public const int NET_DVR_GET_IVMS_STREAMCFG = 163;//获取智能分析仪取流参数

        //智能控制参数结构
        public const int NET_DVR_SET_VCA_CTRLCFG = 164;//设置智能控制参数
        public const int NET_DVR_GET_VCA_CTRLCFG = 165;//获取智能控制参数

        //屏蔽区域NET_VCA_MASK_REGION_LIST
        public const int NET_DVR_SET_VCA_MASK_REGION = 166;//设置屏蔽区域参数
        public const int NET_DVR_GET_VCA_MASK_REGION = 167;//获取屏蔽区域参数

        //ATM进入区域 NET_VCA_ENTER_REGION
        public const int NET_DVR_SET_VCA_ENTER_REGION = 168;//设置进入区域参数
        public const int NET_DVR_GET_VCA_ENTER_REGION = 169;//获取进入区域参数

        //标定线配置NET_VCA_LINE_SEGMENT_LIST
        public const int NET_DVR_SET_VCA_LINE_SEGMENT = 170;//设置标定线
        public const int NET_DVR_GET_VCA_LINE_SEGMENT = 171;//获取标定线

        // ivms屏蔽区域NET_IVMS_MASK_REGION_LIST
        public const int NET_DVR_SET_IVMS_MASK_REGION = 172;//设置IVMS屏蔽区域参数
        public const int NET_DVR_GET_IVMS_MASK_REGION = 173;//获取IVMS屏蔽区域参数
        // ivms进入检测区域NET_IVMS_ENTER_REGION
        public const int NET_DVR_SET_IVMS_ENTER_REGION = 174;//设置IVMS进入区域参数
        public const int NET_DVR_GET_IVMS_ENTER_REGION = 175;//获取IVMS进入区域参数

        public const int NET_DVR_SET_IVMS_BEHAVIORCFG = 176;//设置智能分析仪行为规则参数
        public const int NET_DVR_GET_IVMS_BEHAVIORCFG = 177;//获取智能分析仪行为规则参数

        // IVMS 回放检索
        public const int NET_DVR_IVMS_SET_SEARCHCFG = 178;//设置IVMS回放检索参数
        public const int NET_DVR_IVMS_GET_SEARCHCFG = 179;//获取IVMS回放检索参数

        //报警回调命令
        //对应NET_VCA_PLATE_RESULT
        public const int COMM_ALARM_PLATE = 4353;//车牌识别报警信息

        //结构参数宏定义 
        public const int VCA_MAX_POLYGON_POINT_NUM = 10;//检测区域最多支持10个点的多边形
        public const int MAX_RULE_NUM = 8;//最多规则条数
        public const int MAX_TARGET_NUM = 30;//最多目标个数
        public const int MAX_CALIB_PT = 6;//最大标定点个数
        public const int MIN_CALIB_PT = 4;//最小标定点个数
        public const int MAX_TIMESEGMENT_2 = 2;//最大时间段数
        public const int MAX_LICENSE_LEN = 16;//车牌号最大长度
        public const int MAX_PLATE_NUM = 3;//车牌个数
        public const int MAX_MASK_REGION_NUM = 4;//最多四个屏蔽区域
        public const int MAX_SEGMENT_NUM = 6;//摄像机标定最大样本线数目
        public const int MIN_SEGMENT_NUM = 3;//摄像机标定最小样本线数目

        //智能控制信息
        public const int MAX_VCA_CHAN = 16;//最大智能通道数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLINFO
        {
            public byte byVCAEnable;//是否开启智能
            public byte byVCAType;//智能能力类型，VCA_CHAN_ABILITY_TYPE 
            public byte byStreamWithVCA;//码流中是否带智能信息
            public byte byMode;//模式，VCA_CHAN_MODE_TYPE ,atm能力的时候需要配置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0 
        }

        //智能控制信息结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VCA_CHAN, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_CTRLINFO[] struCtrlInfo;//控制信息,数组0对应设备的起始通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //智能设备能力集
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_ABILITY
        {
            public uint dwSize;//结构长度
            public byte byVCAChanNum;//智能通道个数
            public byte byPlateChanNum;//车牌通道个数
            public byte byBBaseChanNum;//行为基本版个数
            public byte byBAdvanceChanNum;//行为高级版个数
            public byte byBFullChanNum;//行为完整版个数
            public byte byATMChanNum;//智能ATM个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //行为分析能力类型
        public enum VCA_ABILITY_TYPE
        {
            TRAVERSE_PLANE_ABILITY = 1,//穿越警戒面
            ENTER_AREA_ABILITY = 2,//进入区域
            EXIT_AREA_ABILITY = 4,//离开区域
            INTRUSION_ABILITY = 8,//入侵
            LOITER_ABILITY = 16,//徘徊
            LEFT_TAKE_ABILITY = 32,//丢包捡包
            PARKING_ABILITY = 64,//停车
            RUN_ABILITY = 128,//奔跑
            HIGH_DENSITY_ABILITY = 256,//内人员密度
            LF_TRACK_ABILITY = 512,//双摄像机跟踪
            STICK_UP_ABILITY = 1073741824,//贴纸条
            INSTALL_SCANNER_ABILITY = -2147483648,//安装读卡器
        }

        //智能通道类型
        public enum VCA_CHAN_ABILITY_TYPE
        {
            VCA_BEHAVIOR_BASE = 1,//行为分析基本版
            VCA_BEHAVIOR_ADVANCE = 2,//行为分析高级版
            VCA_BEHAVIOR_FULL = 3,//行为分析完整版
            VCA_PLATE = 4,//车牌能力
            VCA_ATM = 5,//ATM能力
        }

        //智能ATM模式类型(ATM能力特有)
        public enum VCA_CHAN_MODE_TYPE
        {
            VCA_ATM_PANEL = 0,//ATM面板
            VCA_ATM_SURROUND = 1,//ATM环境
        }

        //通道能力输入参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CHAN_IN_PARAM
        {
            public byte byVCAType;//VCA_CHAN_ABILITY_TYPE枚举值
            public byte byMode;//模式，VCA_CHAN_MODE_TYPE ,atm能力的时候需要配置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0 
        }

        //行为能力集结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_BEHAVIOR_ABILITY
        {
            public uint dwSize;//结构长度
            public uint dwAbilityType;//支持的能力类型，按位表示，见VCA_ABILITY_TYPE定义
            public byte byMaxRuleNum;//最大规则数
            public byte byMaxTargetNum;//最大目标数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，设置为0
        }

        /*********************************************************
        Function:    NET_DVR_GetDeviceAbility
        Desc:  
        Input:    
        Output:    
        Return:    TRUE表示成功，FALSE表示失败。
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);


        //智能共用结构
        //坐标值归一化,浮点数值为当前画面的百分比大小, 精度为小数点后三位
        //点坐标结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POINT
        {
            public float fX;// X轴坐标, 0.001~1
            public float fY;//Y轴坐标, 0.001~1
        }

        public const int DRAG_PTZ = 51;//拖动PTZ
        public const int NET_DVR_FISHEYE_CFG = 3244; //鱼眼长连接配置

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DRAG_POS_PARAM
        {
            public uint dwChannel;  //设备通道号
            public uint dwPtzChannel;  //dwChannel所指通道模式为FISHEYE_STREAM_MODE_FISHEYE（鱼眼模式时），此值为其拖动所联动的ptz通道；dwChannel通道模式为非鱼眼模式时，置为0即可
            public tagNET_VCA_POINT struToPoint;  //拖动画面要跳转到的目标点，目标点位置相对于预览画面的左上角
            public tagNET_VCA_POINT struOriPoint;  //拖动操作起始点，此点为当次拖动操作开始时，鼠标指针相对于预览画面左上角的位置 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，设置为0
        }
        public enum FISHEYE_STREAM_OUTPUT_MODE
        {
            FISHEYE_STREAM_MODE_FISHEYE = 1,
            FISHEYE_STREAM_MODE_PTZ = 2,
            FISHEYE_STREAM_MODE_PANORAMA = 3
        }

        public enum CALLBACK_TYPE_DATA_ENUM
        {
            ENUM_FISHEYE_STREAM_STATUS = 1,  //鱼眼码流输出状态
            ENUM_FISHEYE_PTZPOS = 2,  //ptz通道当前所处鱼眼模式下的坐标
            ENUM_FISHEYE_REALTIME_OUTPUT = 3   //实时输出模式
        }

        //鱼眼码流状态
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FISHEYE_STREAM_STATUS
        {
            public uint dwSize;
            public byte byStreamMode;    //码流输出模式，参见FISHEYE_STREAM_OUTPUT_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，设置为0
        }

        //长连接回调数据结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CALLBACK_TYPE_DATA
        {
            public uint dwChannel;      //通道号
            public uint dwDataType;     //参见CALLBACK_TYPE_DATA_ENUM
            public uint dwDataLen;      //数据长度
            public IntPtr pData;          //数据，当dwTypeData为 ENUM_FISHEYE_STREAM_STATUS，其对应为NET_DVR_FISHEYE_STREAM_STATUS
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，设置为0
        }

        //区域框结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RECT
        {
            public float fX;//边界框左上角点的X轴坐标, 0.001~1
            public float fY;//边界框左上角点的Y轴坐标, 0.001~1
            public float fWidth;//边界框的宽度, 0.001~1
            public float fHeight;//边界框的高度, 0.001~1
        }

        //行为分析事件类型
        public enum VCA_EVENT_TYPE
        {
            VCA_TRAVERSE_PLANE = 1,//穿越警戒面
            VCA_ENTER_AREA = 2,//目标进入区域,支持区域规则
            VCA_EXIT_AREA = 4,//目标离开区域,支持区域规则
            VCA_INTRUSION = 8,//周界入侵,支持区域规则
            VCA_LOITER = 16,//徘徊,支持区域规则
            VCA_LEFT_TAKE = 32,//丢包捡包,支持区域规则
            VCA_PARKING = 64,//停车,支持区域规则
            VCA_RUN = 128,//奔跑,支持区域规则
            VCA_HIGH_DENSITY = 256,//区域内人员密度,支持区域规则
            VCA_STICK_UP = 1073741824,//贴纸条,支持区域规则
            VCA_INSTALL_SCANNER = -2147483648,//安装读卡器,支持区域规则
        }

        //警戒面穿越方向类型
        public enum VCA_CROSS_DIRECTION
        {
            VCA_BOTH_DIRECTION,// 双向 
            VCA_LEFT_GO_RIGHT,// 由左至右 
            VCA_RIGHT_GO_LEFT,// 由右至左 
        }

        //线结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE
        {
            public tagNET_VCA_POINT struStart;//起点 
            public tagNET_VCA_POINT struEnd; //终点

            //             public void init()
            //             {
            //                 struStart = new tagNET_VCA_POINT();
            //                 struEnd = new tagNET_VCA_POINT();
            //             }
        }

        //该结构会导致xaml界面出不来！！！！！！！！！？？问题暂时还没有找到  
        //暂时屏蔽结构先
        //多边型结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POLYGON
        {
            /// DWORD->unsigned int
            public uint dwPointNum;

            /// NET_VCA_POINT[10]
            //             [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            //             public tagNET_VCA_POINT[] struPos;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TRAVERSE_PLANE
        {
            public tagNET_VCA_LINE struPlaneBottom;//警戒面底边
            public VCA_CROSS_DIRECTION dwCrossDirection;//穿越方向: 0-双向，1-从左到右，2-从右到左
            public byte byRes1;//保留
            public byte byPlaneHeight;//警戒面高度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 38, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new tagNET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }

        //进入/离开区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_AREA
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //根据报警延迟时间来标识报警中带图片，报警间隔和IO报警一致，1秒发送一个。
        //入侵参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_INTRUSION
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//报警延迟时间: 1-120秒，建议5秒，判断是有效报警的时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //徘徊参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARAM_LOITER
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发徘徊报警的持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //丢包/捡包参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TAKE_LEFT
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发丢包/捡包报警的持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //停车参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARKING
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//触发停车报警持续时间：1-120秒，建议10秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //奔跑参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RUN
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public float fRunDistance;//人奔跑最大距离, 范围: [0.1, 1.00]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //人员聚集参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_HIGH_DENSITY
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public float fDensity;//密度比率, 范围: [0.1, 1.0]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //贴纸条参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_STICK_UP
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//报警持续时间：10-60秒，建议10秒
            public byte bySensitivity;//灵敏度参数，范围[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //读卡器参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SCANNER
        {
            public tagNET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//读卡持续时间：10-60秒
            public byte bySensitivity;//灵敏度参数，范围[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //警戒事件参数
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct tagNET_VCA_EVENT_UNION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            [FieldOffsetAttribute(0)]
            public uint[] uLen;//参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TRAVERSE_PLANE struTraversePlane;//穿越警戒面参数 
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_AREA struArea;//进入/离开区域参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_INTRUSION struIntrusion;//入侵参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARAM_LOITER struLoiter;//徘徊参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TAKE_LEFT struTakeTeft;//丢包/捡包参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARKING struParking;//停车参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_RUN struRun;//奔跑参数
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_HIGH_DENSITY struHighDensity;//人员聚集参数  
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_STICK_UP struStickUp;//贴纸条
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_SCANNER struScanner;//读卡器参数 

            //             public void init()
            //             {
            //                 uLen = new uint[23];
            //                 struTraversePlane = new tagNET_VCA_TRAVERSE_PLANE();
            //                 struTraversePlane.init();
            //                 struArea = new tagNET_VCA_AREA();
            //                 struArea.init();
            //                 struIntrusion = new tagNET_VCA_INTRUSION();
            //                 struIntrusion.init();
            //                 struLoiter = new tagNET_VCA_PARAM_LOITER();
            //                 struLoiter.init();
            //                 struTakeTeft = new tagNET_VCA_TAKE_LEFT();
            //                 struTakeTeft.init();
            //                 struParking = new tagNET_VCA_PARKING();
            //                 struParking.init();
            //                 struRun = new tagNET_VCA_RUN();
            //                 struRun.init();
            //                 struHighDensity = new tagNET_VCA_HIGH_DENSITY();
            //                 struHighDensity.init();
            //                 struStickUp = new tagNET_VCA_STICK_UP();
            //                 struStickUp.init();
            //                 struScanner = new tagNET_VCA_SCANNER();
            //                 struScanner.init();
            //             }
        }

        // 尺寸过滤器类型
        public enum SIZE_FILTER_MODE
        {
            IMAGE_PIX_MODE,//根据像素大小设置
            REAL_WORLD_MODE,//根据实际大小设置
        }

        //尺寸过滤器
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SIZE_FILTER
        {
            public byte byActive;//是否激活尺寸过滤器 0-否 非0-是
            public byte byMode;//过滤器模式SIZE_FILTER_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            public NET_VCA_RECT struMiniRect;//最小目标框,全0表示不设置
            public NET_VCA_RECT struMaxRect;//最大目标框,全0表示不设置
        }

        //警戒规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ONE_RULE
        {
            public byte byActive;//是否激活规则,0-否,非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0字段
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//行为分析事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//行为分析事件参数
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//尺寸过滤器
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//处理方式 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //行为分析配置结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULECFG
        {
            public uint dwSize;//结构长度
            public byte byPicProType;//报警时图片处理方式 0-不处理 非0-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPictureParam;//图片规格结构
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ONE_RULE[] struRule;//规则数组
        }

        //简化目标结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TARGET_INFO
        {
            public uint dwID;//目标ID ,人员密度过高报警时为0
            public NET_VCA_RECT struRect; //目标边界框 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        //简化的规则信息, 包含规则的基本信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_INFO
        {
            public byte byRuleID;//规则ID,0-7
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//警戒事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//事件参数
        }

        //前端设备地址信息，智能分析仪表示的是前端设备的地址信息，其他设备表示本机的地址
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;//前端设备地址，
            public ushort wPort;//前端设备端口号， 
            public byte byChannel;//前端设备通道，
            public byte byRes;// 保留字节
        }

        //行为分析结果上报结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_ALARM
        {
            public uint dwSize;//结构长度
            public uint dwRelativeTime;//相对时标
            public uint dwAbsTime;//绝对时标
            public tagNET_VCA_RULE_INFO struRuleInfo;//事件规则信息
            public tagNET_VCA_TARGET_INFO struTargetInfo;//报警目标信息
            public tagNET_VCA_DEV_INFO struDevInfo;//前端设备信息
            public uint dwPicDataLen;//返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//保留，设置为0
            public IntPtr pImage;//指向图片的指针
        }

        //参数关键字
        public enum IVS_PARAM_KEY
        {
            OBJECT_DETECT_SENSITIVE = 1,//目标检测灵敏度
            BACKGROUND_UPDATE_RATE = 2,//背景更新速度
            SCENE_CHANGE_RATIO = 3,//场景变化检测最小值
            SUPPRESS_LAMP = 4,//是否抑制车头灯
            MIN_OBJECT_SIZE = 5,//能检测出的最小目标大小
            OBJECT_GENERATE_RATE = 6,//目标生成速度
            MISSING_OBJECT_HOLD = 7,//目标消失后继续跟踪时间
            MAX_MISSING_DISTANCE = 8,//目标消失后继续跟踪距离
            OBJECT_MERGE_SPEED = 9,//多个目标交错时，目标的融合速度
            REPEATED_MOTION_SUPPRESS = 10,//重复运动抑制
            ILLUMINATION_CHANGE = 11,//光影变化抑制开关
            TRACK_OUTPUT_MODE = 12,//轨迹输出模式：0-输出目标的中心，1-输出目标的底部中心
            ENTER_CHANGE_HOLD = 13,//检测区域变化阈值
            RESUME_DEFAULT_PARAM = 255,//恢复默认关键字参数
        }

        //设置/获取参数关键字
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, int nValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, ref int pValue);

        //行为分析规则DSP信息叠加结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;//编码是否叠加目标
            public byte byDspAddRule;//编码是否叠加规则
            public byte byDspPicAddTarget;//抓图是否叠加目标
            public byte byDspPicAddRule;//抓图是否叠加规则
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //获取/设置行为分析目标叠加接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        //标定点子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CB_POINT
        {
            public tagNET_VCA_POINT struPoint;//标定点，主摄像机（枪机）
            public NET_DVR_PTZPOS struPtzPos;//球机输入的PTZ坐标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //标定参数配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CALIBRATION_PARAM
        {
            public byte byPointNum;//有效标定点个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CALIB_PT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_CB_POINT[] struCBPoint;//标定点组
        }

        //LF双摄像机配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CFG
        {
            public uint dwSize;//结构长度    
            public byte byEnable;//标定使能
            public byte byFollowChan;// 被控制的从通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public tagNET_DVR_LF_CALIBRATION_PARAM struCalParam;//标定点组
        }

        //L/F跟踪模式
        public enum TRACK_MODE
        {
            MANUAL_CTRL = 0,//手动跟踪
            ALARM_TRACK,//报警触发跟踪
            TARGET_TRACK,//目标跟踪
        }

        //L/F手动控制结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_MANUAL_CTRL_INFO
        {
            public tagNET_VCA_POINT struCtrlPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //L/F目标跟踪结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_TARGET_INFO
        {
            public uint dwTargetID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_MODE
        {
            public uint dwSize;//结构长度
            public byte byTrackMode;//跟踪模式
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct uModeParam
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
                [FieldOffsetAttribute(0)]
                public uint[] dwULen;
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_MANUAL_CTRL_INFO struManualCtrl;//手动跟踪结构
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_TRACK_TARGET_INFO struTargetTrack;//目标跟踪结构
            }
        }

        //双摄像机跟踪模式设置接口
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        //识别场景
        public enum VCA_RECOGNIZE_SCENE
        {
            VCA_LOW_SPEED_SCENE = 0,//低速通过场景（收费站、小区门口、停车场）
            VCA_HIGH_SPEED_SCENE = 1,//高速通过场景（卡口、高速公路、移动稽查)
            VCA_MOBILE_CAMERA_SCENE = 2,//移动摄像机应用） 
        }

        //识别结果标志
        public enum VCA_RECOGNIZE_RESULT
        {
            VCA_RECOGNIZE_FAILURE = 0,//识别失败
            VCA_IMAGE_RECOGNIZE_SUCCESS,//图像识别成功
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_BEST_LICENSE,//视频识别更优结果
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_NEW_LICENSE,//视频识别到新的车牌
            VCA_VIDEO_RECOGNIZE_FINISH_OF_CUR_LICENSE,//视频识别车牌结束
        }

        //车牌颜色
        public enum VCA_PLATE_COLOR
        {
            VCA_BLUE_PLATE = 0,//蓝色车牌
            VCA_YELLOW_PLATE,//黄色车牌
            VCA_WHITE_PLATE,//白色车牌
            VCA_BLACK_PLATE,       //黑色车牌
            VCA_GREEN_PLATE,       //绿色车牌
            VCA_BKAIR_PLATE,       //民航黑色车牌
            VCA_OTHER = 0xff       //其他
        }

        //车牌类型
        public enum VCA_PLATE_TYPE
        {
            VCA_STANDARD92_PLATE = 0,    //标准民用车与军车
            VCA_STANDARD02_PLATE,  //02式民用车牌 
            VCA_WJPOLICE_PLATE,      //武警车 
            VCA_JINGCHE_PLATE,      //警车
            STANDARD92_BACK_PLATE,         //民用车双行尾牌
            VCA_SHIGUAN_PLATE,          //使馆车牌
            VCA_NONGYONG_PLATE,         //农用车
            VCA_MOTO_PLATE              //摩托车
        }

        //视频识别触发类型
        public enum VCA_TRIGGER_TYPE
        {
            INTER_TRIGGER = 0,// 模块内部触发识别
            EXTER_TRIGGER = 1,// 外部物理信号触发：线圈、雷达、手动触发信号；
        }

        public const int MAX_CHINESE_CHAR_NUM = 64;    // 最大汉字类别数量
        //车牌可动态修改参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_PARAM
        {
            public NET_VCA_RECT struSearchRect;//搜索区域(归一化)
            public NET_VCA_RECT struInvalidateRect;//无效区域，在搜索区域内部 (归一化)
            public ushort wMinPlateWidth;//车牌最小宽度
            public ushort wTriggerDuration;//触发持续帧数
            public byte byTriggerType;//触发模式, VCA_TRIGGER_TYPE
            public byte bySensitivity;//灵敏度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byCharPriority;// 汉字优先级
        }

        /*wMinPlateWidth:该参数默认配置为80像素；该参数的配置对于车牌海康威视车牌识别说明文档 
        识别有影响，如果设置过大，那么如果场景中出现小车牌就会漏识别；如果场景中车牌宽度普遍较大，可以把该参数设置稍大，便于减少对虚假车牌的处理。在标清情况下建议设置为80， 在高清情况下建议设置为120
        wTriggerDuration － 外部触发信号持续帧数量，其含义是从触发信号开始识别的帧数量。该值在低速场景建议设置为50～100；高速场景建议设置为15～25；移动识别时如果也有外部触发，设置为15～25；具体可以根据现场情况进行配置
        */
        //车牌识别参数子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATEINFO
        {
            public VCA_RECOGNIZE_SCENE eRecogniseScene;//识别场景(低速和高速)
            public tagNET_VCA_PLATE_PARAM struModifyParam;//车牌可动态修改参数
        }

        //车牌识别配置参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATECFG
        {
            public uint dwSize;
            public byte byPicProType;//报警时图片处理方式 0-不处理 1-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，设置为0
            public NET_DVR_JPEGPARA struPictureParam;//图片规格结构
            public tagNET_VCA_PLATEINFO struPlateInfo;//车牌信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//处理方式
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//报警触发的录象通道,为1表示触发该通道
        }

        //车牌识别结果子结构
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_VCA_PLATE_INFO
        {
            public VCA_RECOGNIZE_RESULT eResultFlag;//识别结果标志 
            public VCA_PLATE_TYPE ePlateType;//车牌类型
            public VCA_PLATE_COLOR ePlateColor;//车牌颜色
            public NET_VCA_RECT struPlateRect;//车牌位置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//保留，设置为0 
            public uint dwLicenseLen;//车牌长度
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense;//车牌号码 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sBelieve;//各个识别字符的置信度，如检测到车牌"浙A12345", 置信度为10,20,30,40,50,60,70，则表示"浙"字正确的可能性只有10%，"A"字的正确的可能性是20%
        }

        //车牌检测结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_RESULT
        {
            public uint dwSize;//结构长度
            public uint dwRelativeTime;//相对时标
            public uint dwAbsTime;//绝对时标
            public byte byPlateNum;//车牌个数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PLATE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_PLATE_INFO[] struPlateInfo;//车牌信息结构
            public uint dwPicDataLen;//返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes2;//保留，设置为0 图片的高宽
            public System.IntPtr pImage;//指向图片的指针
        }

        //分析仪行为分析规则结构
        //警戒规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ONE_RULE_
        {
            public byte byActive;/* 是否激活规则,0-否, 非0-是 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留，设置为0字段
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//行为分析事件类型
            public tagNET_VCA_EVENT_UNION uEventParam;//行为分析事件参数
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//尺寸过滤器
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*保留，设置为0*/
        }

        // 分析仪规则结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_RULECFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_ONE_RULE_[] struRule; //规则数组
        }

        // IVMS行为分析配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_BEHAVIORCFG
        {
            public uint dwSize;
            public byte byPicProType;//报警时图片处理方式 0-不处理 非0-上传
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;//图片规格结构
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_RULECFG[] struRuleCfg;//每个时间段对应规则
        }

        //智能分析仪取流计划子结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_DEVSCHED
        {
            public NET_DVR_SCHEDTIME struTime;//时间参数
            public NET_DVR_PU_STREAM_CFG struPUStream;//前端取流参数
        }

        //智能分析仪参数配置结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_STREAMCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_DEVSCHED[] struDevSched;//按时间段配置前端取流以及规则信息
        }

        //屏蔽区域
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION
        {
            public byte byEnable;//是否激活, 0-否，非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            public tagNET_VCA_POLYGON struPolygon;//屏蔽多边形
        }

        //屏蔽区域链表结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION_LIST
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MASK_REGION_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION[] struMask;//屏蔽区域数组
        }

        //ATM进入区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ENTER_REGION
        {
            public uint dwSize;
            public byte byEnable;//是否激活，0-否，非0-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public tagNET_VCA_POLYGON struPolygon;//进入区域
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

		 public struct NET_DVR_UPGRADE_PARAM
        {
            public int dwUpgradeType;
            public IntPtr sFileName;
            public IntPtr pInbuffer;
            public int dwBufferLen;
            [MarshalAsAttribute(UnmanagedType.LPStruct, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public IntPtr[] pUnitIdList;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 112, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
		
        //    重启智能库
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_VCA_RestartLib(int lUserID, int lChannel);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEGMENT
        {
            public tagNET_VCA_POINT struStartPoint;//表示高度线时，表示头部点
            public tagNET_VCA_POINT struEndPoint;//表示高度线时，表示脚部点
            public float fValue;//高度值，单位米
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //标定线链表
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEG_LIST
        {
            public uint dwSize;//结构长度
            public byte bySegNum;//标定线条数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;//保留，置0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SEGMENT_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_LINE_SEGMENT[] struSeg;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealHeight(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpHeight);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealLength(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpLength);

        //IVMS屏蔽区域链表
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_MASK_REGION_LIST
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION_LIST[] struList;
        }

        //IVMS的ATM进入区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ENTER_REGION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ENTER_REGION[] struEnter;//进入区域
        }

        // ivms 报警图片上传结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ALARM_JPEG
        {
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;
        }

        // IVMS 后检索配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_SEARCHCFG
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DEC_REMOTE_PLAY struRemotePlay;// 远程回放
            public tagNET_IVMS_ALARM_JPEG struAlarmJpeg;// 报警上传图片配置
            public tagNET_IVMS_RULECFG struRuleCfg;//IVMS 行为规则配置
        }

        //2009-7-22
        public const int NET_DVR_GET_AP_INFO_LIST = 305;//获取无线网络资源参数
        public const int NET_DVR_SET_WIFI_CFG = 306;//设置IP监控设备无线参数
        public const int NET_DVR_GET_WIFI_CFG = 307;//获取IP监控设备无线参数
        public const int NET_DVR_SET_WIFI_WORKMODE = 308;//设置IP监控设备网口工作模式参数
        public const int NET_DVR_GET_WIFI_WORKMODE = 309;//获取IP监控设备网口工作模式参数

        //public const int IW_ESSID_MAX_SIZE = 32;
        public const int WIFI_WEP_MAX_KEY_COUNT = 4;
        public const int WIFI_WEP_MAX_KEY_LENGTH = 33;
        public const int WIFI_WPA_PSK_MAX_KEY_LENGTH = 63;
        public const int WIFI_WPA_PSK_MIN_KEY_LENGTH = 8;
        public const int WIFI_MAX_AP_COUNT = 20;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_AP_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sSsid;
            public uint dwMode;/* 0 mange 模式;1 ad-hoc模式，参见NICMODE */
            public uint dwSecurity;  /*0 不加密；1 wep加密；2 wpa-psk;3 wpa-Enterprise，参见WIFISECURITY*/
            public uint dwChannel;/*1-11表示11个通道*/
            public uint dwSignalStrength;/*0-100信号由最弱变为最强*/
            public uint dwSpeed;/*速率,单位是0.01mbps*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_AP_INFO_LIST
        {
            public uint dwSize;
            public uint dwCount;/*无线AP数量，不超过20*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = WIFI_MAX_AP_COUNT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_AP_INFO[] struApInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_WIFIETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpAddress;/*IP地址*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpMask;/*掩码*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/*物理地址，只用来显示*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes;
            public uint dwEnableDhcp;/*是否启动dhcp  0不启动 1启动*/
            public uint dwAutoDns;/*如果启动dhcp是否自动获取dns,0不自动获取 1自动获取；对于有线如果启动dhcp目前自动获取dns*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDns; /*第一个dns域名*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDns;/*第二个dns域名*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIpAddr;/* 网关地址*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR__WIFI_CFG_EX
        {
            public tagNET_DVR_WIFIETHERNET struEtherNet;/*wifi网口*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sEssid;/*SSID*/
            public uint dwMode;/* 0 mange 模式;1 ad-hoc模式，参见*/
            public uint dwSecurity;/*0 不加密；1 wep加密；2 wpa-psk; */
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct key
            {
                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wep
                {
                    public uint dwAuthentication;/*0 -开放式 1-共享式*/
                    public uint dwKeyLength;/* 0 -64位；1- 128位；2-152位*/
                    public uint dwKeyType;/*0 16进制;1 ASCI */
                    public uint dwActive;/*0 索引：0---3表示用哪一个密钥*/
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WEP_MAX_KEY_COUNT * WIFI_WEP_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                }

                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wpa_psk
                {
                    public uint dwKeyLength;/*8-63个ASCII字符*/
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WPA_PSK_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                    public byte sRes;
                }
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_CFG
        {
            public uint dwSize;
            public tagNET_DVR__WIFI_CFG_EX struWifiCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_WORKMODE
        {
            public uint dwSize;
            public uint dwNetworkInterfaceMode;/*0 自动切换模式　1 有线模式*/
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData_V30(int lRealHandle, uint dwTransType, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        //2009-7-22 end

        //SDK 9000_1.1
        //网络硬盘结构配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SINGLE_NET_DISK_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public NET_DVR_IPADDR struNetDiskAddr;//网络硬盘地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDirectory;// PATHNAME_LEN = 128
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }

        public const int MAX_NET_DISK = 16;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_NET_DISKCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NET_DISK, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_SINGLE_NET_DISK_INFO[] struNetDiskParam;
        }

        //事件类型
        //主类型
        public enum MAIN_EVENT_TYPE
        {
            EVENT_MOT_DET = 0,//移动侦测
            EVENT_ALARM_IN = 1,//报警输入
            EVENT_VCA_BEHAVIOR = 2,//行为分析
        }

        //行为分析主类型对应的此类型， 0xffff表示全部
        public enum BEHAVIOR_MINOR_TYPE
        {
            EVENT_TRAVERSE_PLANE = 0,// 穿越警戒面,
            EVENT_ENTER_AREA,//目标进入区域,支持区域规则
            EVENT_EXIT_AREA,//目标离开区域,支持区域规则
            EVENT_INTRUSION,// 周界入侵,支持区域规则
            EVENT_LOITER,//徘徊,支持区域规则
            EVENT_LEFT_TAKE,//丢包捡包,支持区域规则
            EVENT_PARKING,//停车,支持区域规则
            EVENT_RUN,//奔跑,支持区域规则
            EVENT_HIGH_DENSITY,//区域内人员密度,支持区域规则
            EVENT_STICK_UP,//贴纸条,支持区域规则
            EVENT_INSTALL_SCANNER,//安装读卡器,支持区域规则
        }

        //事件搜索条件 200-04-07 9000_1.1
        public const int SEARCH_EVENT_INFO_LEN = 300;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        //报警输入
        public struct struAlarmParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInNo;//报警输入号，byAlarmInNo[0]若置1则表示查找由报警输入1触发的事件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byAlarmInNo = new byte[MAX_ALARMIN_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //移动侦测
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotDetChanNo;//移动侦测通道，byMotDetChanNo[0]若置1则表示查找由通道1发生移动侦测触发的事件
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byMotDetChanNo = new byte[MAX_CHANNUM_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //行为分析
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChanNo;//触发事件的通道
            public byte byRuleID;//规则ID，0xff表示全部
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 43, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留

            public void init()
            {
                byChanNo = new byte[MAX_CHANNUM_V30];
                byRes1 = new byte[43];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorParam
        {
            //             [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            //             public byte[] byLen;
            [FieldOffset(0)]
            public struMotionParam struMotionPara;
            [FieldOffset(0)]
            public struAlarmParam struAlarmPara;

            //             public struVcaParam struVcaPara;

            public void init()
            {
                //                 byLen = new byte[SEARCH_EVENT_INFO_LEN];
                struAlarmPara = new struAlarmParam();
                struAlarmPara.init();
                //                 struMotionPara = new struMotionParam();
                //                 struMotionPara.init();
                //                 struVcaPara = new struVcaParam();
                //                 struVcaPara.init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_PARAM
        {
            public ushort wMajorType;//0-移动侦测，1-报警输入, 2-智能事件
            public ushort wMinorType;//搜索次类型- 根据主类型变化，0xffff表示全部
            public NET_DVR_TIME struStartTime;//搜索的开始时间，停止时间: 同时为(0, 0) 表示从最早的时间开始，到最后，最前面的4000个事件
            public NET_DVR_TIME struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public uSeniorParam uSeniorPara;

            public void init()
            {
                byRes = new byte[132];
                uSeniorPara = new uSeniorParam();
                uSeniorPara.init();
            }
        }

        //报警输入结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struAlarmRet
        {
            public uint dwAlarmInNo;//报警输入号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //移动侦测结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionRet
        {
            public uint dwMotDetNo;//移动侦测通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //行为分析结果 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaRet
        {
            public uint dwChanNo;//触发事件的通道号
            public byte byRuleID;//规则ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public tagNET_VCA_EVENT_UNION uEvent;//行为事件参数，wMinorType = VCA_EVENT_TYPE决定事件类型

            public void init()
            {
                byRes1 = new byte[3];
                byRuleName = new byte[NAME_LEN];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorRet
        {
            [FieldOffset(0)]
            public struAlarmRet struAlarmRe;
            [FieldOffset(0)]
            public struMotionRet struMotionRe;
            //             public struVcaRet struVcaRe;

            public void init()
            {
                struAlarmRe = new struAlarmRet();
                struAlarmRe.init();
                //                 struVcaRe = new struVcaRet();
                //                 struVcaRe.init();
            }
        }
        //查找返回结果
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_RET
        {
            public ushort wMajorType;//主类型MA
            public ushort wMinorType;//次类型
            public NET_DVR_TIME struStartTime;//事件开始的时间
            public NET_DVR_TIME struEndTime;//事件停止的时间，脉冲事件时和开始时间一样
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uSeniorRet uSeniorRe;

            public void init()
            {
                byChan = new byte[MAX_CHANNUM_V30];
                byRes = new byte[36];
                uSeniorRe = new uSeniorRet();
                uSeniorRe.init();
            }
        }


        //邮件服务测试 9000_1.1
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EmailTest(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByEvent(int lUserID, ref tagNET_DVR_SEARCH_EVENT_PARAM lpSearchEventParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextEvent(int lSearchHandle, ref tagNET_DVR_SEARCH_EVENT_RET lpSearchEventRet);


        //2009-8-18 抓拍机
        public const int PLATE_INFO_LEN = 1024;
        public const int PLATE_NUM_LEN = 16;
        public const int FILE_NAME_LEN = 256;

        // 车牌颜色
        public enum Anonymous_26594f67_851c_4f7d_bec4_094765b7ff83
        {
            BLUE_PLATE, // 蓝色车牌 
            YELLOW_PLATE, // 黄色车牌
            WHITE_PLATE,// 白色车牌
            BLACK_PLATE,// 黑色车牌
        }

        //liscense plate result
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PLATE_RET
        {
            public uint dwSize;//结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPlateNum;//车牌号
            public byte byVehicleType;// 车类型
            public byte byTrafficLight;//0-绿灯；1-红灯
            public byte byPlateColor;//车牌颜色
            public byte byDriveChan;//触发车道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byTimeInfo;/*时间信息*///plate_172.6.113.64_20090724155526948_197170484 
            //目前是17位，精确到ms:20090724155526948
            public byte byCarSpeed;/*单位km/h*/
            public byte byCarSpeedH;/*cm/s高8位*/
            public byte byCarSpeedL;/*cm/s低8位*/
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_INFO_LEN - 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byInfo;
            public uint dwPicLen;
        }
        /*注：后面紧跟 dwPicLen 长度的 图片 信息*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_INVOKE_PLATE_RECOGNIZE(int lUserID, int lChannel, string pPicFileName, ref tagNET_DVR_PLATE_RET pPlateRet, string pPicBuf, uint dwPicBufLen);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CCD_CFG
        {
            public uint dwSize;//结构长度
            public byte byBlc;/*背光补偿0-off; 1-on*/
            public byte byBlcMode;/*blc类型0-自定义1-上；2-下；3-左；4-右；5-中；注：此项在blc为 on 时才起效*/
            public byte byAwb;/*自动白平衡0-自动1; 1-自动2; 2-自动控制*/
            public byte byAgc;/*自动增益0-关; 1-低; 2-中; 3-高*/
            public byte byDayNight;/*日夜转换；0 彩色；1黑白；2自动*/
            public byte byMirror;/*镜像0-关;1-左右;2-上下;3-中心*/
            public byte byShutter;/*快门0-自动; 1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k;*/
            public byte byIrCutTime;/*IRCUT切换时间，5, 10, 15, 20, 25*/
            public byte byLensType;/*镜头类型0-电子光圈; 1-自动光圈*/
            public byte byEnVideoTrig;/*视频触发使能：1-支持；0-不支持。视频触发模式下视频快门速度按照byShutter速度，抓拍图片的快门速度按照byCapShutter速度，抓拍完成后会自动调节回视频模式*/
            public byte byCapShutter;/*抓拍时的快门速度，1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k; 11-1/150; 12-1/200*/
            public byte byEnRecognise;/*1-支持识别；0-不支持识别*/
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagCAMERAPARAMCFG
        {
            public uint dwSize;
            public uint dwPowerLineFrequencyMode;/*0-50HZ; 1-60HZ*/
            public uint dwWhiteBalanceMode;/*0手动白平衡; 1自动白平衡1（范围小）; 2 自动白平衡2（范围宽，2200K-15000K）;3自动控制3*/
            public uint dwWhiteBalanceModeRGain;/*手动白平衡时有效，手动白平衡 R增益*/
            public uint dwWhiteBalanceModeBGain;/*手动白平衡时有效，手动白平衡 B增益*/
            public uint dwExposureMode;/*0 手动曝光 1自动曝光*/
            public uint dwExposureSet;/* 0-USERSET, 1-自动x2，2-自动4，3-自动81/25, 4-1/50, 5-1/100, 6-1/250, 7-1/500, 8-1/750, 9-1/1000, 10-1/2000, 11-1/4000,12-1/10,000; 13-1/100,000*/
            public uint dwExposureUserSet;/* 自动自定义曝光时间*/
            public uint dwExposureTarget;/*手动曝光时间 范围（Manumal有效，微秒）*/
            public uint dwIrisMode;/*0 自动光圈 1手动光圈*/
            public uint dwGainLevel;/*增益：0-100*/
            public uint dwBrightnessLevel;/*0-100*/
            public uint dwContrastLevel;/*0-100*/
            public uint dwSharpnessLevel;/*0-100*/
            public uint dwSaturationLevel;/*0-100*/
            public uint dwHueLevel;/*0-100，（保留）*/
            public uint dwGammaCorrectionEnabled;/*0 dsibale  1 enable*/
            public uint dwGammaCorrectionLevel;/*0-100*/
            public uint dwWDREnabled;/*宽动态：0 dsibale  1 enable*/
            public uint dwWDRLevel1;/*0-F*/
            public uint dwWDRLevel2;/*0-F*/
            public uint dwWDRContrastLevel;/*0-100*/
            public uint dwDayNightFilterType;/*日夜切换：0 day,1 night,2 auto */
            public uint dwSwitchScheduleEnabled;/*0 dsibale  1 enable,(保留)*/
            //模式1(保留)
            public uint dwBeginTime;    /*0-100*/
            public uint dwEndTime;/*0-100*/
            //模式2
            public uint dwDayToNightFilterLevel;//0-7
            public uint dwNightToDayFilterLevel;//0-7
            public uint dwDayNightFilterTime;//(60秒)
            public uint dwBacklightMode;/*背光补偿:0 USERSET 1 UP、2 DOWN、3 LEFT、4 RIGHT、5MIDDLE*/
            public uint dwPositionX1;//（X坐标1）
            public uint dwPositionY1;//（Y坐标1）
            public uint dwPositionX2;//（X坐标2）
            public uint dwPositionY2;//（Y坐标2）
            public uint dwBacklightLevel;/*0x0-0xF*/
            public uint dwDigitalNoiseRemoveEnable; /*数字去噪：0 dsibale  1 enable*/
            public uint dwDigitalNoiseRemoveLevel;/*0x0-0xF*/
            public uint dwMirror; /* 镜像：0 Left;1 Right,;2 Up;3Down */
            public uint dwDigitalZoom;/*数字缩放:0 dsibale  1 enable*/
            public uint dwDeadPixelDetect;/*坏点检测,0 dsibale  1 enable*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        public const int NET_DVR_GET_CCDPARAMCFG = 1067;       //IPC获取CCD参数配置
        public const int NET_DVR_SET_CCDPARAMCFG = 1068;      //IPC设置CCD参数配置

        //图像增强仪
        //图像增强去燥区域配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEREGION
        {
            public uint dwSize;//总的结构长度
            public ushort wImageRegionTopLeftX;/* 图像增强去燥的左上x坐标 */
            public ushort wImageRegionTopLeftY;/* 图像增强去燥的左上y坐标 */
            public ushort wImageRegionWidth;/* 图像增强去燥区域的宽 */
            public ushort wImageRegionHeight;/*图像增强去燥区域的高*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //图像增强、去噪级别及稳定性使能配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGESUBPARAM
        {
            public NET_DVR_SCHEDTIME struImageStatusTime;//图像状态时间段
            public byte byImageEnhancementLevel;//图像增强的级别，0-7，0表示关闭
            public byte byImageDenoiseLevel;//图像去噪的级别，0-7，0表示关闭
            public byte byImageStableEnable;//图像稳定性使能，0表示关闭，1表示打开
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_IMAGEREGION = 1062;       //图像增强仪图像增强去燥区域获取
        public const int NET_DVR_SET_IMAGEREGION = 1063;       //图像增强仪图像增强去燥区域获取
        public const int NET_DVR_GET_IMAGEPARAM = 1064;       // 图像增强仪图像参数(去噪、增强级别，稳定性使能)获取
        public const int NET_DVR_SET_IMAGEPARAM = 1065;       // 图像增强仪图像参数(去噪、增强级别，稳定性使能)设置

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEPARAM
        {
            public uint dwSize;
            //图像增强时间段参数配置，周日开始    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagIMAGESUBPARAM[] struImageParamSched;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetParamSetMode(int lUserID, ref uint dwParamSetMode);

        public struct NET_DVR_CLIENTINFO
        {
            public Int32 lChannel;//通道号
            public uint lLinkMode;//最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public IntPtr hPlayWnd;//播放窗口的句柄,为NULL表示不播放图象
            public string sMultiCastIP;//多播组地址
        }

        public struct NET_SDK_CLIENTINFO
        {
            public Int32 lChannel;//通道号
            public Int32 lLinkType; //连接sdk的方式，是否通过流媒体的标志
            public Int32 lLinkMode;//最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public IntPtr hPlayWnd;//播放窗口的句柄,为NULL表示不播放图象
            public string sMultiCastIP;//多播组地址
            public Int32 iMediaSrvNum;
            public System.IntPtr pMediaSrvDir;
        }

        public struct NET_DVR_PREVIEWINFO
        {
            public Int32 lChannel;
            public uint dwStreamType;
            public uint dwLinkMode;
            public IntPtr hPlayWnd;
            public bool bBlocked;
            public bool bPassbackRecord;
            public Byte byPreviewMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            public Byte byProtoType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint DisplayBufNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 216, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /*********************************************************
        Function:    NET_DVR_Login_V30
        Desc:  
        Input:    sDVRIP [in] 设备IP地址 
                wServerPort [in] 设备端口号 
                sUserName [in] 登录的用户名 
                sPassword [in] 用户密码 
        Output:    lpDeviceInfo [out] 设备信息 
        Return:    -1表示失败，其他值表示返回的用户ID值
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        /*********************************************************
        Function:    NET_DVR_Logout_V30
        Desc:  用户注册设备。
        Input:    lUserID [in] 用户ID号
        Output:    
        Return:    TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout_V30(Int32 lUserID);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SNAPCFG
        {
            public uint dwSize;
            public byte byRelatedDriveWay;
            public byte bySnapTimes;
            public ushort wSnapWaitTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
            public ushort[] wIntervalTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*********************************************************
        Function:    NET_DVR_ContinuousShoot
        Desc:  手动触发连拍。
        Input:        lUserID [in] 用户ID号
                    lpInter [in] 手动连拍参数结构
        Output:    
        Return:    TRUE表示成功，FALSE表示失败
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ContinuousShoot(Int32 lUserID, ref NET_DVR_SNAPCFG lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ManualSnap(Int32 lUserID, ref NET_DVR_MANUALSNAP lpInter, ref NET_DVR_PLATE_RESULT lpOuter);

        #region  取流模块相关结构与接口

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PLAY_INFO
        {
            public int iUserID;      //注册用户ID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strDeviceIP;
            public int iDevicePort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string strDevAdmin;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string strDevPsd;
            public int iChannel;      //播放通道号(从0开始)
            public int iLinkMode;   //最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
            public bool bUseMedia;     //是否启用流媒体
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strMediaIP; //流媒体IP地址
            public int iMediaPort;   //流媒体端口号
        }


        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_Init();

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_UnInit();


        [DllImport("GetStream.dll")]
        public static extern int CLIENT_SDK_GetStream(PLAY_INFO lpPlayInfo); //

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SetRealDataCallBack(int iRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint lUser); //

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_StopStream(int iRealHandle);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_GetVideoEffect(int iRealHandle, ref int iBrightValue, ref int iContrastValue, ref int iSaturationValue, ref int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_SetVideoEffect(int iRealHandle, int iBrightValue, int iContrastValue, int iSaturationValue, int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_MakeKeyFrame(int iRealHandle);

        #endregion


        #region VOD点播放库

        public const int WM_NETERROR = 0x0400 + 102;          //网络异常消息
        public const int WM_STREAMEND = 0x0400 + 103;    //文件播放结束

        public const int FILE_HEAD = 0;      //文件头
        public const int VIDEO_I_FRAME = 1;  //视频I帧
        public const int VIDEO_B_FRAME = 2;  //视频B帧
        public const int VIDEO_P_FRAME = 3;  //视频P帧
        public const int VIDEO_BP_FRAME = 4; //视频BP帧
        public const int VIDEO_BBP_FRAME = 5; //视频B帧B帧P帧
        public const int AUDIO_PACKET = 10;   //音频包

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BLOCKTIME
        {
            public ushort wYear;
            public byte bMonth;
            public byte bDay;
            public byte bHour;
            public byte bMinute;
            public byte bSecond;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODSEARCHPARAM
        {
            public IntPtr sessionHandle;                                    //[in]VOD客户端句柄
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //    [in]DVR的网络地址
            public uint dvrPort;                                            //    [in]DVR的端口地址
            public uint channelNum;                                         //  [in]DVR的通道号
            public BLOCKTIME startTime;                                     //    [in]查询的开始时间
            public BLOCKTIME stopTime;                                      //    [in]查询的结束时间
            public bool bUseIPServer;                                       //  [in]是否使用IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]设备的序列号
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SECTIONLIST
        {
            public BLOCKTIME startTime;
            public BLOCKTIME stopTime;
            public byte byRecType;
            public IntPtr pNext;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODOPENPARAM
        {
            public IntPtr sessionHandle;                                    //[in]VOD客户端句柄
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //    [in]DVR的网络地址
            public uint dvrPort;                                            //    [in]DVR的端口地址
            public uint channelNum;                                         //  [in]DVR的通道号
            public BLOCKTIME startTime;                                     //    [in]查询的开始时间
            public BLOCKTIME stopTime;                                      //    [in]查询的结束时间
            public uint uiUser;
            public bool bUseIPServer;                                       //  [in]是否使用IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]设备的序列号

            public VodStreamFrameData streamFrameData;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CONNPARAM
        {
            public uint uiUser;
            public ErrorCallback errorCB;
        }


        // 异常回调函数
        public delegate void ErrorCallback(System.IntPtr hSession, uint dwUser, int lErrorType);
        //帧数据回调函数
        public delegate void VodStreamFrameData(System.IntPtr hStream, uint dwUser, int lFrameType, System.IntPtr pBuffer, uint dwSize);

        //模块初始化
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerConnect(string strServerIp, uint uiServerPort, ref IntPtr hSession, ref CONNPARAM struConn, IntPtr hWnd);

        //模块销毁
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerDisconnect(IntPtr hSession);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStreamSearch(IntPtr pSearchParam, ref IntPtr pSecList);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODDeleteSectionList(IntPtr pSecList);

        // 根据ID、时间段打开流获取流句柄
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenStream(IntPtr pOpenParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseStream(IntPtr hStream);

        //根据ID、时间段打开批量下载
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenDownloadStream(ref VODOPENPARAM struVodParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseDownloadStream(IntPtr hStream);

        // 开始流解析，发送数据帧
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStartStreamData(IntPtr phStream);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODPauseStreamData(IntPtr hStream, bool bPause);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStopStreamData(IntPtr hStream);

        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSeekStreamData(IntPtr hStream, IntPtr pStartTime);


        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSetStreamSpeed(IntPtr hStream, int iSpeed);

        // 根据时间定位
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODGetStreamCurrentTime(IntPtr hStream, ref BLOCKTIME pCurrentTime);

        #endregion


        #region 帧分析库


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PACKET_INFO
        {
            public int nPacketType;     // packet type
            // 0:  file head
            // 1:  video I frame
            // 2:  video B frame
            // 3:  video P frame
            // 10: audio frame
            // 11: private frame only for PS


            //      [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public IntPtr pPacketBuffer;
            public uint dwPacketSize;
            public int nYear;
            public int nMonth;
            public int nDay;
            public int nHour;
            public int nMinute;
            public int nSecond;
            public uint dwTimeStamp;
        }



        /******************************************************************************
        * function：get a empty port number
        * parameters：
        * return： 0 - 499 : empty port number
        *          -1      : server is full        
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetSafeHandle();


        /******************************************************************************
        * function：open standard stream data for analyzing
        * parameters：lHandle - working port number
        *             pHeader - pointer to file header or info header
        * return：TRUE or FALSE
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataOpenStreamEx(int iHandle, byte[] pFileHead);


        /******************************************************************************
        * function：close analyzing
        * parameters：lHandle - working port number
        * return：
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataClose(int iHandle);


        /******************************************************************************
        * function：input stream data
        * parameters：lHandle  - working port number
        *        pBuffer  - data pointer
        *        dwBuffersize    - data size
        * return：TRUE or FALSE
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataInputData(int iHandle, IntPtr pBuffer, uint uiSize); //byte []


        /******************************************************************************
        * function：get analyzed packet
        * parameters：lHandle  - working port number
        *        pPacketInfo    - returned structure
        * return：-1 : error
        *          0 : succeed
        *     1 : failed
        *     2 : file end (only in file mode)    
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetPacket(int iHandle, ref PACKET_INFO pPacketInfo);  //要把pPacketInfo转换成PACKET_INFO结构


        /******************************************************************************
        * function：get remain data from input buffer
        * parameters：lHandle  - working port number
        *        pBuf            - pointer to the mem which stored remain data
        *             dwSize        - size of remain data  
        * return： TRUE or FALSE    
        * comment：
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataGetTail(int iHandle, ref IntPtr pBuffer, ref uint uiSize);


        [DllImport("AnalyzeData.dll")]
        public static extern uint AnalyzeDataGetLastError(int iHandle);

        #endregion


        #region 录像库

        public const int DATASTREAM_HEAD = 0;  //数据头
        public const int DATASTREAM_BITBLOCK = 1;  //字节数据
        public const int DATASTREAM_KEYFRAME = 2;  //关键帧数据
        public const int DATASTREAM_NORMALFRAME = 3;  //非关键帧数据


        public const int MESSAGEVALUE_DISKFULL = 0x01;
        public const int MESSAGEVALUE_SWITCHDISK = 0x02;
        public const int MESSAGEVALUE_CREATEFILE = 0x03;
        public const int MESSAGEVALUE_DELETEFILE = 0x04;
        public const int MESSAGEVALUE_SWITCHFILE = 0x05;




        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct STOREINFO
        {
            public int iMaxChannels;
            public int iDiskGroup;
            public int iStreamType;
            public bool bAnalyze;
            public bool bCycWrite;
            public uint uiFileSize;

            public CALLBACKFUN_MESSAGE funCallback;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CREATEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileCreateTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CLOSEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileSwitchTime;
        }



        public delegate int CALLBACKFUN_MESSAGE(int iMessageType, System.IntPtr pBuf, int iBufLen);


        [DllImport("RecordDLL.dll")]
        public static extern int Initialize(STOREINFO struStoreInfo);

        [DllImport("RecordDLL.dll")]
        public static extern int Release();

        [DllImport("RecordDLL.dll")]
        public static extern int OpenChannelRecord(string strCameraid, IntPtr pHead, uint dwHeadLength);

        [DllImport("RecordDLL.dll")]
        public static extern bool CloseChannelRecord(int iRecordHandle);

        [DllImport("RecordDLL.dll")]
        public static extern int GetData(int iHandle, int iDataType, IntPtr pBuf, uint uiSize);

        #endregion

        //设备区域设置
        public const int REGIONTYPE = 0;//代表区域
        public const int MATRIXTYPE = 11;//矩阵节点
        public const int DEVICETYPE = 2;//代表设备
        public const int CHANNELTYPE = 3;//代表通道
        public const int USERTYPE = 5;//代表用户

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_LOG_MATRIX
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;
            public uint dwMinorType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr;
            public uint dwParaType;
            public uint dwChannel;
            public uint dwDiskNumber;
            public uint dwAlarmInPort;
            public uint dwAlarmOutPort;
            public uint dwInfoLen;
            public byte byDevSequence;//槽位号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC地址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN - SERIALNO_LEN - MACADDR_LEN - 1)]
            public string sInfo;
        }

        //视频综合平台软件
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagVEDIOPLATLOG
        {
            public byte bySearchCondition;//搜索条件，0-按槽位号搜索，1-按序列号搜索 2-按MAC地址进行搜索
            public byte byDevSequence;//槽位号，0-79：对应子系统的槽位号；
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC地址
        }

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_MATRIX(int iLogHandle, ref NET_DVR_LOG_MATRIX lpLogData);


        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_Matrix(int iUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref tagVEDIOPLATLOG lpVedioPlatLog, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);




        /*************************************************
        启动远程配置宏定义 
        NET_DVR_StartRemoteConfig
        具体支持查看函数说明和代码
        **************************************************/
        public const int MAX_CARDNO_LEN = 48;
        public const int MAX_OPERATE_INDEX_LEN = 32;
        public const int NET_DVR_GET_ALL_VEHICLE_CONTROL_LIST = 3124;
        public const int NET_DVR_VEHICLE_DELINFO_CTRL = 3125; 
        public const int NET_DVR_VEHICLELIST_CTRL_START = 3133;

        /*********************************************************
        Function:    REMOTECONFIGCALLBACK
        Desc:  (回调函数)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void REMOTECONFIGCALLBACK(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_StartRemoteConfig(Int32 lUserID, uint dwCommand, IntPtr lpInBuffer, Int32 dwInBufferLen, REMOTECONFIGCALLBACK cbStateCallback, IntPtr pUserData);

        // 建立发送长连接数据与关闭长连接配置接口所创建的句柄，释放资源
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(Int32 lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(Int32 lHandle);

        // 逐个获取查找到的车辆数据信息。
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_GetNextRemoteConfig(Int32 lHandle, IntPtr lpOutBuff, uint dwOutBuffSize);


        // 智能交通操作类型
        public enum VCA_OPERATE_TYPE
        {
            VCA_LICENSE_TYPE = 0x1,  //车牌号码
            VCA_PLATECOLOR_TYPE = 0x2,  //车牌颜色
            VCA_CARDNO_TYPE = 0x4,  //卡号
            VCA_PLATETYPE_TYPE = 0x8,  //车牌类型
            VCA_LISTTYPE_TYPE = 0x10,  //车辆名单类型
            VCA_INDEX_TYPE = 0x20,  //数据流水号 2014-02-25
            VCA_OPERATE_INDEX_TYPE = 0x40  //操作数 2014-03-03
        }
        // NET_DVR_StartRemoteConfig CallBack 返回类型
        public enum NET_SDK_CALLBACK_TYPE
        {
            NET_SDK_CALLBACK_TYPE_STATUS = 0,
            NET_SDK_CALLBACK_TYPE_PROGRESS,
            NET_SDK_CALLBACK_TYPE_DATA
        }

        // NET_DVR_StartRemoteConfig CallBack调用设备返回的状态值
        public enum NET_SDK_CALLBACK_STATUS_NORMAL
        {
            NET_SDK_CALLBACK_STATUS_SUCCESS = 1000,  // 成功
            NET_SDK_CALLBACK_STATUS_PROCESSING,      // 处理中
            NET_SDK_CALLBACK_STATUS_FAILED,    // 失败
            NET_SDK_CALLBACK_STATUS_EXCEPTION,      // 异常
            NET_SDK_CALLBACK_STATUS_LANGUAGE_MISMATCH,    //（IPC配置文件导入）语言不匹配
            NET_SDK_CALLBACK_STATUS_DEV_TYPE_MISMATCH,    //（IPC配置文件导入）设备类型不匹配
            NET_DVR_CALLBACK_STATUS_SEND_WAIT           // 发送等待
        }

        // 数据全部获取接口 （长连接获取）
        public struct NET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;
            public uint dwOperateType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo;
            public byte byListType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        // NET_DVR_GetNextRemoteConfig 设备返回状态
        public enum NET_SDK_GET_NEXT_STATUS
        {
            NET_SDK_GET_NEXT_STATUS_SUCCESS = 1000,    // 成功读取到数据，客户端处理完本次数据后需要再次调用NET_DVR_RemoteConfigGetNext获取下一条数据
            NET_SDK_GET_NETX_STATUS_NEED_WAIT,  // 需等待设备发送数据，继续调用NET_DVR_RemoteConfigGetNext函数
            NET_SDK_GET_NEXT_STATUS_FINISH,      // 数据全部取完，此时客户端可调用NET_DVR_StopRemoteConfig结束长连接
            NET_SDK_GET_NEXT_STATUS_FAILED,      // 出现异常，客户端可调用NET_DVR_StopRemoteConfig结束长连接
        }


        public struct tagNET_DVR_VEHICLE_CONTROL_LIST_INFO
        {
            public uint dwSize;
            public uint dwChannel;//通道号0xff - 全部通道（ITC 默认是1）
            public uint dwDataIndex;//数据流水号（平台维护的数据唯一值，客户端操作的时候，该值不会起效。该值主要用于数据增量同步）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //车牌号码
            public byte byListType;//名单属性
            public byte byPlateType;    //车牌类型
            public byte byPlateColor;    //车牌颜色
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 21, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // 卡号
            public NET_DVR_TIME_V30 struStartTime;//有效开始时间
            public NET_DVR_TIME_V30 struStopTime;//有效结束时间
            //操作数（平台同步表流水号不会重复，用于增量更新，代表同步到同步表的某一条记录了，存在相机内存，重启后会清0）2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 224, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; // 保留字节
        }

        // 数据全部获取接口 （长连接获取）
        public struct tagNET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;//通道号0xffffffff - 全部通道（ITC 默认是1）
            public uint dwOperateType;//操作类型，参照VCA_OPERATE _TYPE。（可复选）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //车牌号码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // 卡号
            public byte byListType;//名单属性
            //2014-02-25
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;//数据流水号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        public struct NET_DVR_VEHICLE_CONTROL_DELINFO
        {
            public uint dwSize;
            public uint dwDelType;//删除条件类型，删除条件类型，参照VCA_OPERATE _TYPE。（可复选）
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public String sLicense; //车牌号码
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public String sCardNo; // 卡号 
            public byte byPlateType;    //车牌类型
            public byte byPlateColor;    //车牌颜色
            public byte byOperateType;    //删除操作类型(0-条件删除,0xff-删除全部)
            //2014-02-25
            public byte byListType;//名单属性
            public uint dwDataIndex;//数据流水号     
            //操作数（平台同步表流水号不会重复，用于增量更新，代表同步到同步表的某一条记录了，存在相机内存，重启后会清0）2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //本地配置 报警监听
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLocalIP(byte[] strIP, ref Int32 pValidNum, ref bool pEnableBind);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetValidIP(uint dwIPIndex, bool bEnableBind);

        // 门参数配置结构体
        public const int DOOR_NAME_LEN = 32;
        public const int STRESS_PASSWORD_LEN = 8;
        public const int SUPER_PASSWORD_LEN = 8;
        public const int UNLOCK_PASSWORD_LEN = 8;
        public const int NET_DVR_GET_DOOR_CFG = 2108; // 获取门参数
        public const int NET_DVR_SET_DOOR_CFG = 2109; // 设置门参数
        public const int COMM_ALARM_ACS = 0x5002; // 门禁主机报警
        public const int ACS_CARD_NO_LEN = 32; // 门禁卡号长度
        public const int MAX_DOOR_NUM = 32;
        public const int MAX_GROUP_NUM = 32;
        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;
        public const int CARD_PASSWORD_LEN = 8;

        public struct NET_DVR_DOOR_CFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DOOR_NAME_LEN)]
            public String byDoorName;
            public byte byMagneticType;
            public byte byOpenButtonType;
            public byte byOpenDuration;
            public byte byAccessibleOpenDuration;
            public byte byMagneticAlarmTimeout;
            public byte byEnableDoorLock;
            public byte byEnableLeaderCard;
            public byte byRes1;
            public uint dwLeaderCardOpenDuration;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = STRESS_PASSWORD_LEN)]
            public String byStressPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = SUPER_PASSWORD_LEN)]
            public String bySuperPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = UNLOCK_PASSWORD_LEN)]
            public String byUnlockPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 56)]
            public String byRes2;
        }

        // 门禁主机报警信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_ALARM_INFO
        {
            public uint dwSize;
            public uint dwMajor; //报警主类型，参考宏定义
            public uint dwMinor; //报警次类型，参考宏定义
            public NET_DVR_TIME struTime; //时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//网络操作的用户名
            public NET_DVR_IPADDR struRemoteHostAddr;//远程主机地址
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo; //详细参数
            public uint dwPicDataLen;   //图片数据大小，不为0是表示后面带数据
            public IntPtr pPicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 门禁主机事件信息
        public struct NET_DVR_ACS_EVENT_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号，为0无效
            public byte byCardType; 
            public byte byAllowListNo; 
            public byte byReportChannel; //报告上传通道，1-布防上传，2-中心组1上传，3-中心组2上传，为0无效
            public byte byCardReaderKind; //读卡器属于哪一类，0-无效，1-IT读卡器，2-身份证读卡器，3-二维码读卡器
            public uint dwCardReaderNo; //读卡器编号，为0无效
            public uint dwDoorNo; //门编号，为0无效
            public uint dwVerifyNo; //多重卡认证序号，为0无效
            public uint dwAlarmInNo; //报警输入号，为0无效
            public uint dwAlarmOutNo; //报警输出号，为0无效
            public uint dwCaseSensorNo; //事件触发器编号
            public uint dwRs485No; //RS485通道号，为0无效
            public uint dwMultiCardGroupNo; //群组编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //批量参数配置
        public const int ZERO_CHAN_INDEX = 500;
        public const int MIRROR_CHAN_INDEX = 400;
        public const int STREAM_ID_LEN = 32;
        public const int MAX_CHANNUM_V40 = 512;

        //compression parameter
        public const int NORM_HIGH_STREAM_COMPRESSION = 0; //主码流图像压缩参数,压缩能力强,性能可以更高
        public const int SUB_STREAM_COMPRESSION = 1; //子码流图像压缩参数
        public const int EVENT_INVOKE_COMPRESSION = 2; //事件触发图像压缩参数,一些参数相对固定
        public const int THIRD_STREAM_COMPRESSION = 3;  //第三码流
        public const int TRANS_STREAM_COMPRESSION = 4;  //转码码流

        public const int NET_DVR_GET_AUDIO_INPUT = 3201; //获取音频输入参数
        public const int NET_DVR_SET_AUDIO_INPUT = 3202; //设置音频输入参数
        public const int NET_DVR_GET_MULTI_STREAM_COMPRESSIONCFG = 3216; //远程获取多码流压缩参数
        public const int NET_DVR_SET_MULTI_STREAM_COMPRESSIONCFG = 3217; //远程设置多码流压缩参数

        public struct NET_DVR_VALID_PERIOD_CFG
        {
            public byte byEnable; //使能有效期，0-不使能，1使能
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struBeginTime; //有效期起始时间
            public NET_DVR_TIME_EX struEndTime; //有效期结束时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        public struct NET_DVR_TIME_EX
        {
            public Int16 wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
        }
        // 流信息 - 72字节长
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byID;
            public uint dwChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //多码流压缩参数配置条件结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG_COND
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struStreamInfo;
            public uint dwStreamType; //码流类型，0-主码流，1-子码流，2-事件类型，3-码流3，……
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //多码流压缩参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG
        {
            public uint dwSize;
            public uint dwStreamType; //码流类型，0-主码流，1-子码流，2-事件类型，3-码流3，……
            public NET_DVR_COMPRESSION_INFO_V30 struStreamPara; //码流压缩参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUDIO_INPUT_PARAM
        {
            public byte byAudioInputType;  //音频输入类型，0-mic in，1-line in
            public byte byVolume; //volume,[0-100]
            public byte byEnableNoiseFilter; //是否开启声音过滤-关，-开
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //时间段录像参数配置(子结构)
        public struct NET_DVR_RECORDSCHED_V40
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            /*录像类型，0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 
            6-智能报警录像，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件,14-智能交通事件, 
            15-越界侦测,16-区域入侵,17-声音异常,18-场景变更侦测,
            19-智能侦测(越界侦测|区域入侵|人脸侦测|声音异常|场景变更侦测),20－人脸侦测,21-POS录像,
            22-进入区域侦测, 23-离开区域侦测,24-徘徊侦测,25-人员聚集侦测,26-快速运动侦测,27-停车侦测,
            28-物品遗留侦测,29-物品拿取侦测,30-火点检测，31-防破坏检测*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //全天录像参数配置(子结构)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY_V40
        {
            public byte byAllDayRecord;/* 是否全天录像 0-否 1-是*/
            /*录像类型，0:定时录像，1:移动侦测，2:报警录像，3:动测|报警，4:动测&报警 5:命令触发, 
            6-智能报警录像，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件,14-智能交通事件, 
            15-越界侦测,16-区域入侵,17-声音异常,18-场景变更侦测,
            19-智能侦测(越界侦测|区域入侵|人脸侦测|声音异常|场景变更侦测),20－人脸侦测,21-POS录像,
            22-进入区域侦测, 23-离开区域侦测,24-徘徊侦测,25-人员聚集侦测,26-快速运动侦测,27-停车侦测,
            28-物品遗留侦测,29-物品拿取侦测,30-火点检测，31-防破坏检测*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V40
        {
            public uint dwSize;
            public uint dwRecord; /*是否录像 0-否 1-是*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY_V40[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED_V40[] struRecordSched;
            public uint dwRecordTime; /* 录象延时长度 0-5秒， 1-10秒， 2-30秒， 3-1分钟， 4-2分钟， 5-5分钟， 6-10分钟*/
            public uint dwPreRecordTime; /* 预录时间 0-不预录 1-5秒 2-10秒 3-15秒 4-20秒 5-25秒 6-30秒 7-0xffffffff(尽可能预录) */
            public uint dwRecorderDuration; /* 录像保存的最长时间 */
            public byte byRedundancyRec; /*是否冗余录像,重要数据双备份：0/1*/
            public byte byAudioRec; /*录像时复合流编码时是否记录音频数据：国外有此法规*/
            public byte byStreamType;  // 0-主码流，1-子码流，2-主子码流同时 3-三码流
            public byte byPassbackRecord;  // 0:不回传录像 1：回传录像
            public ushort wLockDuration;  // 录像锁定时长，单位小时 0表示不锁定，0xffff表示永久锁定，录像段的时长大于锁定的持续时长的录像，将不会锁定
            public byte byRecordBackup;  // 0:录像不存档 1：录像存档
            public byte bySVCLevel;    //SVC抽帧类型：0-不抽，1-抽二分之一 2-抽四分之三
            public byte byRecordManage;   //录像调度，0-启用， 1-不启用; 启用时进行定时录像；不启用时不进行定时录像，但是录像计划仍在使用，比如移动侦测，回传都还在按这条录像计划进行
            public byte byExtraSaveAudio;//音频单独存储
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_CARD_CFG = 2116;    //获取卡参数
        public const int NET_DVR_SET_CARD_CFG = 2117;    //设置卡参数

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG
        {
            public uint dwSize;
            public uint dwModifyParamType;
            // 需要修改的卡参数，设置卡参数时有效，按位表示，每位代表一种参数，1为需要修改，0为不修改
            // #define CARD_PARAM_CARD_VALID       0x00000001 //卡是否有效参数
            // #define CARD_PARAM_VALID            0x00000002  //有效期参数
            // #define CARD_PARAM_CARD_TYPE        0x00000004  //卡类型参数
            // #define CARD_PARAM_DOOR_RIGHT       0x00000008  //门权限参数
            // #define CARD_PARAM_LEADER_CARD      0x00000010  //首卡参数
            // #define CARD_PARAM_SWIPE_NUM        0x00000020  //最大刷卡次数参数
            // #define CARD_PARAM_GROUP            0x00000040  //所属群组参数
            // #define CARD_PARAM_PASSWORD         0x00000080  //卡密码参数
            // #define CARD_PARAM_RIGHT_PLAN       0x00000100  //卡权限计划参数
            // #define CARD_PARAM_SWIPED_NUM       0x00000200  //已刷卡次数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号
            public byte byCardValid; //卡是否有效，0-无效，1-有效（用于删除卡，设置时置为0进行删除，获取时此字段始终为1）
            public byte byCardType; 
            public byte byLeaderCard; //是否为首卡，1-是，0-否
            public byte byRes1;
            public uint dwDoorRight; //门权限，按位表示，1为有权限，0为无权限，从低位到高位表示对门1-N是否有权限
            public NET_DVR_VALID_PERIOD_CFG struValid; //有效期参数
            public uint dwBelongGroup; //所属群组，按位表示，1-属于，0-不属于，从低位到高位表示是否从属群组1-N
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARD_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardPassword; //卡密码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM * MAX_CARD_RIGHT_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardRightPlan; //卡权限计划，取值为计划模板编号，同个门不同计划模板采用权限或的方式处理
            public uint dwMaxSwipeTime; //最大刷卡次数，0为无次数限制
            public uint dwSwipeTime; //已刷卡次数
            public ushort wRoomNumber; //房间号 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_COND
        {
            public uint dwSize;
            public uint dwCardNum; //设置或获取卡数量，获取时置为0xffffffff表示获取所有卡信息
            public byte byCheckCardNo; //设备是否进行卡号校验，0-不校验，1-校验
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_SEND_DATA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //卡号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        public const int NET_DVR_GET_GROUP_CFG = 2112;   //获取群组参数
        public const int NET_DVR_SET_GROUP_CFG = 2113;    //设置群组参数

        //新增结构体（群组）
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_VALID_PERIOD_CFG struValidPeriodCfg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byGroupName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARM_DEVICE_USER
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] sUserName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] sPassword;
            public NET_DVR_IPADDR struUserIP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byAMCAddr;
            public byte byUserType;
            public byte byAlarmOnRight;
            public byte byAlarmOffRight;
            public byte byBypassRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byOtherRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPreviewRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetRecordRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPlaybackRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPTZRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] byRes2;

        }

        public const int NET_DVR_GET_FINGERPRINT_CFG = 2150;    //获取指纹参数
        public const int NET_DVR_SET_FINGERPRINT_CFG = 2151;    //设置指纹参数
        public const int NET_DVR_DEL_FINGERPRINT_CFG = 2152;    //删除指纹参数
        public const int MAX_FINGER_PRINT_LEN = 768;            //最大指纹长度

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;  //指纹关联卡号
            public uint dwFingerPrintLen; //指纹数据长度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCardReader; //需要下发指纹的读卡器，按数组表示，0-不下发该读卡器，1-下发到该读卡器
            public byte byFingerPrintID;     //指纹编号，有效值范围为1-10
            public byte byFingerType;       //指纹类型  0-普通指纹，1-胁迫指纹
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FINGER_PRINT_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byFingerData;        //指纹数据内容
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct StrucTEST
        {
            public uint dwSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_STATUS
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byCardReaderRecvStatus;   //指纹读卡器状态，按数组表示
            public byte byFingerPrintID;  //指纹编号，有效值范围为1-10
            public byte byFingerType;   //指纹类型  0-普通指纹，1-胁迫指纹
            public byte byTotalStatus;  //下发总的状态，0-当前指纹未下完所有读卡器，1-已下完所有读卡器(这里的所有指的是门禁主机往所有的读卡器下发了，不管成功与否)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 61)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_COND
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //指纹的读卡器信息，按数组表示
            public uint dwFingerPrintNum; //设置或获取卡数量，获取时置为0xffffffff表示获取所有卡信息
            public byte byFingerPrintID;  //指纹编号，有效值范围为-10   0xff表示该卡所有指纹
            public byte byCallbackMode;   //设备回调方式，0-设备所有读卡器下完了范围，1-在时间段内下了部分也返回
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYCARD
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //指纹的读卡器信息，按数组表示
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] byFingerPrintID;    //需要获取的指纹信息，按数组下标，值表示0-不删除，1-删除该指纹
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34)]
            public byte[] byRes1;

            //public void init()
            //{
            //    byCardNo = new byte[32];
            //    byEnableCardReader = new byte[512];
            //    byFingerPrintID = new byte[10];
            //    byRes1 = new byte[34];
            //}
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYREADER
        {
            public uint dwCardReaderNo;  //按值表示，指纹读卡器编号
            public byte byClearAllCard;  //是否删除所有卡的指纹信息，0-按卡号删除指纹信息，1-删除所有卡的指纹信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //指纹关联的卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 548)]
            public byte[] byRes;

            //public void init()
            //{
            //    byRes1 = new byte[3];
            //    byCardNo = new byte[32];
            //    byRes = new byte[548];
            //}
        }

        //public const int DEL_FINGER_PRINT_MODE_LEN = 588; //联合体大小
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_DEL_FINGER_PRINT_MODE
        //{
        //    public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //按卡号的方式删除
        //    public NET_DVR_FINGER_PRINT_BYREADER struByReader; //按读卡器的方式删除

        //    //public void init()
        //    //{
        //    //    struByCard = new NET_DVR_FINGER_PRINT_BYCARD();
        //    //    struByCard.init();
        //    //}
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYCARD
        {
            public uint dwSize;
            public byte byMode;          //删除方式，0-按卡号方式删除，1-按读卡器删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //按卡号的方式删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYREADER
        {
            public uint dwSize;
            public byte byMode;          //删除方式，0-按卡号方式删除，1-按读卡器删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYREADER struByReader; //按读卡器的方式删除
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        public const int NET_DVR_GET_CARD_READER_CFG = 2140;      //获取读卡器参数
        public const int NET_DVR_SET_CARD_READER_CFG = 2141;      //设置读卡器参数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_CFG
        {
            public uint dwSize;
            public byte byEnable; //是否使能，1-使能，0-不使能
            public byte byCardReaderType; //读卡器类型，1-DS-K110XM/MK/C/CK，2-DS-K192AM/AMP，3-DS-K192BM/BMP，4-DS-K182AM/AMP，5-DS-K182BM/BMP，6-DS-K182AMF/ACF，7-韦根或485不在线,8- DS-K1101M/MK，9- DS-K1101C/CK，10- DS-K1102M/MK/M-A
            //11- DS-K1102C/CK，12- DS-K1103M/MK，13- DS-K1103C/CK，14- DS-K1104M/MK，15- DS-K1104C/CK，16- DS-K1102S/SK/S-A，17- DS-K1102G/GK，18- DS-K1100S-B，19- DS-K1102EM/EMK，20- DS-K1102E/EK，
            //21- DS-K1200EF，22- DS-K1200MF，23- DS-K1200CF，24- DS-K1300EF，25- DS-K1300MF，26- DS-K1300CF，27- DS-K1105E，28- DS-K1105M，29- DS-K1105C，30- DS-K182AMF，31- DS-K196AMF，32-DS-K194AMP
            //33-DS-K1T200EF/EF-C/MF/MF-C/CF/CF-C,34-DS-K1T300EF/EF-C/MF/MF-C/CF/CF-C，35-DS-K1T105E/E-C/M/M-C/C/C-C
            public byte byOkLedPolarity; //OK LED极性，0-阴极，1-阳极
            public byte byErrorLedPolarity; //Error LED极性，0-阴极，1-阳极
            public byte byBuzzerPolarity; //蜂鸣器极性，0-阴极，1-阳极
            public byte bySwipeInterval; //重复刷卡间隔时间，单位：秒
            public byte byPressTimeout;  //按键超时时间，单位：秒
            public byte byEnableFailAlarm; //是否启用读卡失败超次报警，0-不启用，1-启用
            public byte byMaxReadCardFailNum; //最大读卡失败次数
            public byte byEnableTamperCheck;  //是否支持防拆检测，0-disable ，1-enable
            public byte byOfflineCheckTime;  //掉线检测时间 单位秒
            public byte byFingerPrintCheckLevel;   //指纹识别等级，1-1/10误认率，2-1/100误认率，3-1/1000误认率，4-1/10000误认率，5-1/100000误认率，6-1/1000000误认率，7-1/10000000误认率，8-1/100000000误认率，9-3/100误认率，10-3/1000误认率，11-3/10000误认率，12-3/100000误认率，13-3/1000000误认率，14-3/10000000误认率，15-3/100000000误认率，16-Automatic Normal,17-Automatic Secure,18-Automatic More Secure
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_WEEK_PLAN_CFG = 2100; //获取门状态周计划参数
        public const int NET_DVR_SET_WEEK_PLAN_CFG = 2101; //设置门状态周计划参数
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_PLAN = 2102; //获取门状态假日计划参数
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_PLAN = 2103; //设置门状态假日计划参数
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_GROUP = 2104; //获取门状态假日组参数
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_GROUP = 2105; //设置门状态假日组参数
        public const int NET_DVR_GET_DOOR_STATUS_PLAN_TEMPLATE = 2106; //获取门状态计划模板参数
        public const int NET_DVR_SET_DOOR_STATUS_PLAN_TEMPLATE = 2107; //设置门状态计划模板参数
        public const int NET_DVR_GET_DOOR_STATUS_PLAN = 2110; //获取门状态计划参数
        public const int NET_DVR_SET_DOOR_STATUS_PLAN = 2111; //设置门状态计划参数
        public const int NET_DVR_CLEAR_ACS_PARAM = 2118; //清空门禁主机参数
        public const int NET_DVR_GET_VERIFY_WEEK_PLAN = 2124; //获取读卡器验证方式周计划参数
        public const int NET_DVR_SET_VERIFY_WEEK_PLAN = 2125; //设置读卡器验证方式周计划参数
        public const int NET_DVR_GET_CARD_RIGHT_WEEK_PLAN = 2126; //获取卡权限周计划参数
        public const int NET_DVR_SET_CARD_RIGHT_WEEK_PLAN = 2127; //设置卡权限周计划参数
        public const int NET_DVR_GET_VERIFY_HOLIDAY_PLAN = 2128; //获取读卡器验证方式假日计划参数
        public const int NET_DVR_SET_VERIFY_HOLIDAY_PLAN = 2129; //设置读卡器验证方式假日计划参数
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN = 2130; //获取卡权限假日计划参数
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN = 2131; //设置卡权限假日计划参数
        public const int NET_DVR_GET_VERIFY_HOLIDAY_GROUP = 2132; //获取读卡器验证方式假日组参数
        public const int NET_DVR_SET_VERIFY_HOLIDAY_GROUP = 2133; //设置读卡器验证方式假日组参数
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_GROUP = 2134; //获取卡权限假日组参数
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_GROUP = 2135; //设置卡权限假日组参数
        public const int NET_DVR_GET_VERIFY_PLAN_TEMPLATE = 2136; //获取读卡器验证方式计划模板参数
        public const int NET_DVR_SET_VERIFY_PLAN_TEMPLATE = 2137; //设置读卡器验证方式计划模板参数
        public const int NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE = 2138; //获取卡权限计划模板参数
        public const int NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE = 2139; //设置卡权限计划模板参数
        public const int NET_DVR_GET_CARD_READER_PLAN = 2142; //获取读卡器验证计划参数
        public const int NET_DVR_SET_CARD_READER_PLAN = 2143; //设置读卡器验证计划参数
        public const int NET_DVR_GET_CARD_USERINFO_CFG = 2163; //获取卡号关联用户信息参数
        public const int NET_DVR_SET_CARD_USERINFO_CFG = 2164; //设置卡号关联用户信息参数

        public const int NET_DVR_CONTROL_PTZ_MANUALTRACE = 3316; //手动跟踪定位
        public const int NET_DVR_GET_SMARTTRACKCFG = 3293; //获取智能运动跟踪配置信息    
        public const int NET_DVR_SET_SMARTTRACKCFG = 3294; //设置智能运动跟踪配置信息

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DATE
        {
            public ushort wYear; //年
            public byte byMonth; //月
            public byte byDay; //日
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SIMPLE_DAYTIME
        {
            public byte byHour; //时
            public byte byMinute; //分
            public byte bySecond; //秒
            public byte byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_SEGMENT
        {
            public NET_DVR_SIMPLE_DAYTIME struBeginTime; //开始时间点
            public NET_DVR_SIMPLE_DAYTIME struEndTime;   //结束时间点
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_PLAN_SEGMENT
        {
            public byte byEnable; //是否使能，1-使能，0-不使能
            public byte byDoorStatus; //门状态模式，0-无效，1-常开状态，2-常闭状态，3-普通状态（门状态计划使用）
            public byte byVerifyMode; //验证方式，0-无效，1-刷卡，2-刷卡+密码(读卡器验证方式计划使用)，3-刷卡,4-刷卡或密码(读卡器验证方式计划使用), 5-指纹，6-指纹+密码，7-指纹或刷卡，8-指纹+刷卡，9-指纹+刷卡+密码（无先后顺序）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
            public NET_DVR_TIME_SEGMENT struTimeSegment; //时间段参数
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WEEK_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //周计划参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public CHCNetSDK.NET_DVR_DATE struBeginDate; //假日开始日期
            public CHCNetSDK.NET_DVR_DATE struEndDate; //假日结束日期
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public HCNetSDK.NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //时间段参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        public const int TEMPLATE_NAME_LEN = 32; //计划模板名称长度
        public const int MAX_HOLIDAY_GROUP_NUM = 16; //计划模板最大假日组数
        public const int HOLIDAY_GROUP_NAME_LEN = 32; //假日组名称长度
        public const int MAX_HOLIDAY_PLAN_NUM = 16; //假日组最大假日计划数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;  //是否使能，1-使能，0-不使能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.HOLIDAY_GROUP_NAME_LEN)]
            public byte[] byGroupName; //假日组名称
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayPlanNo; //假日组编号，就前填充，遇0无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PLAN_TEMPLATE
        {
            public uint dwSize;
            public byte byEnable; //是否启用，1-启用，0-不启用
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.TEMPLATE_NAME_LEN)]
            public byte[] byTemplateName; //模板名称
            public uint dwWeekPlanNo; //周计划编号，0为无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayGroupNo; //假日组编号，就前填充，遇0无效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DOOR_STATUS_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //计划模板编号，为0表示取消关联，恢复默认状态（普通状态）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //计划模板编号，为0表示取消关联，恢复默认状态（刷卡开门）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        public const int ACS_PARAM_DOOR_STATUS_WEEK_PLAN = 0x00000001;      //门状态周计划参数
        public const int ACS_PARAM_VERIFY_WEEK_PALN = 0x00000002;           //读卡器周计划参数
        public const int ACS_PARAM_CARD_RIGHT_WEEK_PLAN = 0x00000004;       //卡权限周计划参数
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN = 0x00000008;   //门状态假日计划参数
        public const int ACS_PARAM_VERIFY_HOLIDAY_PALN = 0x00000010;        //读卡器假日计划参数
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN = 0x00000020;    //卡权限假日计划参数
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP = 0x00000040;  //门状态假日组参数
        public const int ACS_PARAM_VERIFY_HOLIDAY_GROUP = 0x00000080;       //读卡器验证方式假日组参数
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP = 0x00000100;   //卡权限假日组参数
        public const int ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE = 0x00000200;  //门状态计划模板参数
        public const int ACS_PARAM_VERIFY_PALN_TEMPLATE = 0x00000400;       //读卡器验证方式计划模板参数
        public const int ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE = 0x00000800;   //卡权限计划模板参数
        public const int ACS_PARAM_CARD = 0x00001000;                       //卡参数
        public const int ACS_PARAM_GROUP = 0x00002000;                      //群组参数
        public const int ACS_PARAM_ANTI_SNEAK_CFG = 0x00004000;             //反潜回参数
        public const int ACS_PAPAM_EVENT_CARD_LINKAGE = 0x00008000;         //事件及卡号联动参数
        public const int ACS_PAPAM_CARD_PASSWD_CFG = 0x00010000;            //密码开门使能参数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_PARAM_TYPE
        {
            public uint dwSize;
            public uint dwParamType;
            //参数类型，按位表示
            //#define ACS_PARAM_DOOR_STATUS_WEEK_PLAN        0x00000001 //门状态周计划参数
            //#define ACS_PARAM_VERIFY_WEEK_PALN             0x00000002 //读卡器周计划参数
            //#define ACS_PARAM_CARD_RIGHT_WEEK_PLAN         0x00000004 //卡权限周计划参数
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN     0x00000008 //门状态假日计划参数
            //#define ACS_PARAM_VERIFY_HOLIDAY_PALN          0x00000010 //读卡器假日计划参数
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN      0x00000020 //卡权限假日计划参数
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP    0x00000040 //门状态假日组参数
            //#define ACS_PARAM_VERIFY_HOLIDAY_GROUP         0x00000080 //读卡器验证方式假日组参数
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP     0x00000100 //卡权限假日组参数
            //#define ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE    0x00000200 //门状态计划模板参数
            //#define ACS_PARAM_VERIFY_PALN_TEMPLATE         0x00000400 //读卡器验证方式计划模板参数
            //#define ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE     0x00000800 //卡权限计划模板参数
            //#define ACS_PARAM_CARD                         0x00001000 //卡参数
            //#define ACS_PARAM_GROUP                        0x00002000 //群组参数
            //#define ACS_PARAM_ANTI_SNEAK_CFG           0x00004000 //反潜回参数
            //#define ACS_PAPAM_EVENT_CARD_LINKAGE           0x00008000 //事件及卡号联动参数
            //#define ACS_PAPAM_CARD_PASSWD_CFG              0x00010000 //密码开门使能参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_USER_INFO_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] byUsername;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes2;
        }


        //视频参数配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOEFFECT
        {
            public byte byBrightnessLevel; /*0-100*/
            public byte byContrastLevel; /*0-100*/
            public byte bySharpnessLevel; /*0-100*/
            public byte bySaturationLevel; /*0-100*/
            public byte byHueLevel; /*0-100,（保留）*/
            public byte byEnableFunc; //使能，按位表示，bit0-SMART IR(防过曝)，bit1-低照度,bit2-强光抑制使能，0-否，1-是
            public byte byLightInhibitLevel; //强光抑制等级，[1-3]表示等级
            public byte byGrayLevel; //灰度值域，0-[0-255]，1-[16-235]
        }

        //增益配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAIN
        {
            public byte byGainLevel; /*增益：0-100*/
            public byte byGainUserSet; /*用户自定义增益；0-100，对于抓拍机，是CCD模式下的抓拍增益*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwMaxGainValue;/*最大增益值，单位dB*/
        }

        //白平衡配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WHITEBALANCE
        {
            public byte byWhiteBalanceMode; /*0-手动白平衡（MWB）,1-自动白平衡1（AWB1）,2-自动白平衡2 (AWB2),3-自动控制改名为锁定白平衡(Locked WB)，
                                            4-室外(Indoor)，5-室内(Outdoor)6-日光灯(Fluorescent Lamp)，7-钠灯(Sodium Lamp)，
                                            8-自动跟踪(Auto-Track)9-一次白平衡(One Push)，10-室外自动(Auto-Outdoor)，
                                            11-钠灯自动 (Auto-Sodiumlight)，12-水银灯(Mercury Lamp)，13-自动白平衡(Auto)，
                                            14-白炽灯 (IncandescentLamp)，15-暖光灯(Warm Light Lamp)，16-自然光(Natural Light) */
            public byte byWhiteBalanceModeRGain; /*手动白平衡时有效，手动白平衡 R增益*/
            public byte byWhiteBalanceModeBGain; /*手动白平衡时有效，手动白平衡 B增益*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        //曝光控制
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_EXPOSURE
        {
            public byte byExposureMode; /*0 手动曝光 1自动曝光*/
            public byte byAutoApertureLevel; /* 自动光圈灵敏度, 0-10 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwVideoExposureSet; /* 自定义视频曝光时间（单位us）*//*注:自动曝光时该值为曝光最慢值 新增20-1s(1000000us)*/
            public uint dwExposureUserSet; /* 自定义曝光时间,在抓拍机上应用时，CCD模式时是抓拍快门速度*/
            public uint dwRes;
        }

        //Gamma校正
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAMMACORRECT
        {
            public byte byGammaCorrectionEnabled; /*0 dsibale  1 enable*/
            public byte byGammaCorrectionLevel; /*0-100*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //宽动态配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WDR
        {
            public byte byWDREnabled; /*宽动态：0 dsibale  1 enable 2 auto*/
            public byte byWDRLevel1; /*0-F*/
            public byte byWDRLevel2; /*0-F*/
            public byte byWDRContrastLevel; /*0-100*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes;
        }

        //日夜转换功能配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DAYNIGHT
        {
            public byte byDayNightFilterType; /*日夜切换：0-白天，1-夜晚，2-自动，3-定时，4-报警输入触发*/
            public byte bySwitchScheduleEnabled; /*0 dsibale  1 enable,(保留)*/
            //定时模式参数
            public byte byBeginTime; /*开始时间（小时），0-23*/
            public byte byEndTime; /*结束时间（小时），0-23*/
            //模式2
            public byte byDayToNightFilterLevel; //0-7
            public byte byNightToDayFilterLevel; //0-7
            public byte byDayNightFilterTime;//(60秒)
            //定时模式参数
            public byte byBeginTimeMin; //开始时间（分），0-59
            public byte byBeginTimeSec; //开始时间（秒），0-59
            public byte byEndTimeMin; //结束时间（分），0-59
            public byte byEndTimeSec; //结束时间（秒），0-59
            //报警输入触发模式参数
            public byte byAlarmTrigState; //报警输入触发状态，0-白天，1-夜晚
        }

        //背光补偿配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_BACKLIGHT
        {
            public byte byBacklightMode; /*背光补偿:0 off 1 UP、2 DOWN、3 LEFT、4 RIGHT、5MIDDLE、6自定义，10-开，11-自动，12-多区域背光补偿*/
            public byte byBacklightLevel; /*0x0-0xF*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes1;
            public uint dwPositionX1; //（X坐标1）
            public uint dwPositionY1; //（Y坐标1）
            public uint dwPositionX2; //（X坐标2）
            public uint dwPositionY2; //（Y坐标2）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        //数字降噪功能
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_NOISEREMOVE
        {
            public byte byDigitalNoiseRemoveEnable; /*0-不启用，1-普通模式数字降噪，2-专家模式数字降噪*/
            public byte byDigitalNoiseRemoveLevel; /*普通模式数字降噪级别：0x0-0xF*/
            public byte bySpectralLevel;       /*专家模式下空域强度：0-100*/
            public byte byTemporalLevel;   /*专家模式下时域强度：0-100*/
            public byte byDigitalNoiseRemove2DEnable;         /* 抓拍帧2D降噪，0-不启用，1-启用 */
            public byte byDigitalNoiseRemove2DLevel;            /* 抓拍帧2D降噪级别，0-100 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
        }

        //CMOS模式下前端镜头配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CMOSMODECFG
        {
            public byte byCaptureMod;   //抓拍模式：0-抓拍模式1；1-抓拍模式2
            public byte byBrightnessGate;//亮度阈值
            public byte byCaptureGain1;   //抓拍增益1,0-100
            public byte byCaptureGain2;   //抓拍增益2,0-100
            public uint dwCaptureShutterSpeed1;//抓拍快门速度1
            public uint dwCaptureShutterSpeed2;//抓拍快门速度2
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes;
        }

        //透雾
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEFOGCFG
        {
            public byte byMode; //模式，0-不启用，1-自动模式，2-常开模式
            public byte byLevel; //等级，0-100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //电子防抖
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ELECTRONICSTABILIZATION
        {
            public byte byEnable;//使能 0- 不启用，1- 启用
            public byte byLevel; //等级，0-100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //走廊模式
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CORRIDOR_MODE_CCD
        {
            public byte byEnableCorridorMode; //是否启用走廊模式 0～不启用， 1～启用
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] byRes;
        }

        // SMART IR(防过曝)配置参数
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SMARTIR_PARAM
        {
            public byte byMode;//0～手动，1～自动
            public byte byIRDistance;//红外距离等级(等级，距离正比例)level:1~100 默认:50（手动模式下增加）
            public byte byShortIRDistance;// 近光灯距离等级(1~100)
            public byte byInt32IRDistance;// 远光灯距离等级(1~100)
        }

        //在byIrisMode 为P-Iris1时生效，配置红外光圈大小等级，配置模式
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PIRIS_PARAM
        {
            public byte byMode;//0-自动，1-手动
            public byte byPIrisAperture;//红外光圈大小等级(等级,光圈大小正比例)level:1~100 默认:50（手动模式下增加）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //激光参数配置 2014-02-25
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_LASER_PARAM_CFG
        {
            //Length = 16
            public byte byControlMode;  //控制模式      0-无效，1-自动，2-手动 默认自动
            public byte bySensitivity;  //激光灯灵敏度  0-100 默认50
            public byte byTriggerMode;  //激光灯触发模式    0-无效，1-机芯触发，2-光敏触发 默认机芯触发
            public byte byBrightness;  //控制模式为手动模式下有效；激光灯亮度  0-255 默认100
            public byte byAngle;      //激光灯角度  0-无效，范围1-36  默认12，激光灯照射范围为一个圆圈，调节激光角度是调节这个圆的半径的大小
            public byte byLimitBrightness;  //控制模式为自动模式下有效；激光灯亮度限制 0~100 （新增）2014-01-26
            public byte byEnabled;         //手动控制激光灯使能 0-关闭，1-启动
            public byte byIllumination;     //激光灯强度配置0~100
            public byte byLightAngle;       //补光角度 0~100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FFC_PARAM
        {
            //1-Schedule Mode,2-Temperature Mode, 3-Off 
            public byte byMode;
            //（时间:按能力显示，单位分钟，选项有10,20,30,40,50,60,120,180,240）
            public byte byRes1;
            public ushort wCompensateTime; //定时模式下生效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDE_PARAM   //在sensor中完成
        {
            public byte byMode;//1-Off,2-Normal Mode,3-Expert Mode
            public byte byNormalLevel;//普通模式等级范围[1,100]，普通模式下生效
            public byte byExpertLevel;//专家模式等级范围[1,100]，专家模式下生效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_AGC_PARAM
        {
            public byte bySceneType;//1-Normal Sence,2-Highlight Sence,3-Manual Sence
            public byte byLightLevel;//亮度等级[1,100]；手动模式下生效
            public byte byGainLevel; //增益等级[1,100]；手动模式下生效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        //抓拍机CCD参数 共64字节
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SNAP_CAMERAPARAMCFG
        {
            public byte byWDRMode;   // 宽动态模式;0~关闭，1~数字宽动态 2~宽动态
            public byte byWDRType;    // 宽动态切换模式; 0~强制启用，1~按时间启用，2~按亮度启用
            public byte byWDRLevel;   // 宽动态等级，0~6索引对应1-7，默认索引2（即3级）；
            public byte byRes1;
            public NET_DVR_TIME_EX struStartTime; //开始宽动态时间
            public NET_DVR_TIME_EX struEndTime; //结束宽动态时间
            public byte byDayNightBrightness; //日夜转换亮度阈值，0-100，默认50；
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
            public byte[] byRes;
        }

        //光学透雾参数
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_OPTICAL_DEHAZE
        {
            public byte byEnable; //0~不启用光学透雾，1~启用光学透雾
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_ISP_CAMERAPARAMCFG = 3255; //获取ISP前端参数配置
        public const int NET_DVR_SET_ISP_CAMERAPARAMCFG = 3256; //设置ISP前端参数配置
        public const int NET_DVR_GET_CCDPARAMCFG_EX = 3368; //获取CCD参数配置
        public const int NET_DVR_SET_CCDPARAMCFG_EX = 3369; //设置CCD参数配置

        //前端参数配置
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CAMERAPARAMCFG_EX
        {
            public uint dwsize;
            public NET_DVR_VIDEOEFFECT struVideoEffect;/*亮度、对比度、饱和度、锐度、色调配置*/
            public NET_DVR_GAIN struGain;/*自动增益*/
            public NET_DVR_WHITEBALANCE struWhiteBalance;/*白平衡*/
            public NET_DVR_EXPOSURE struExposure; /*曝光控制*/
            public NET_DVR_GAMMACORRECT struGammaCorrect;/*Gamma校正*/
            public NET_DVR_WDR struWdr;/*宽动态*/
            public NET_DVR_DAYNIGHT struDayNight;/*日夜转换*/
            public NET_DVR_BACKLIGHT struBackLight;/*背光补偿*/
            public NET_DVR_NOISEREMOVE struNoiseRemove;/*数字降噪*/
            public byte byPowerLineFrequencyMode; /*0-50HZ; 1-60HZ*/
            public byte byIrisMode; /*0-自动光圈 1-手动光圈, 2-P-Iris1, 3-Union 3-9mm F1.6-2.7 (T5280-PQ1), 4-Union 2.8-12mm F1.6-2.7 (T5289-PQ1),5-HIK 3.8-16mm F1.5（HV3816P-8MPIR）*/
            public byte byMirror;  /* 镜像：0 off，1- leftright，2- updown，3-center 4-Auto*/
            public byte byDigitalZoom;  /*数字缩放:0 dsibale  1 enable*/
            public byte byDeadPixelDetect;   /*坏点检测,0 dsibale  1 enable*/
            public byte byBlackPwl;/*黑电平补偿 ,  0-255*/
            public byte byEptzGate;// EPTZ开关变量:0-不启用电子云台，1-启用电子云台
            public byte byLocalOutputGate;//本地输出开关变量0-本地输出关闭1-本地BNC输出打开 2-HDMI输出关闭  
            //20-HDMI_720P50输出开
            //21-HDMI_720P60输出开
            //22-HDMI_1080I60输出开
            //23-HDMI_1080I50输出开
            //24-HDMI_1080P24输出开
            //25-HDMI_1080P25输出开
            //26-HDMI_1080P30输出开
            //27-HDMI_1080P50输出开
            //28-HDMI_1080P60输出开
            public byte byCoderOutputMode;//编码器fpga输出模式0直通3像素搬家
            public byte byLineCoding; //是否开启行编码：0-否，1-是
            public byte byDimmerMode; //调光模式：0-半自动，1-自动
            public byte byPaletteMode; //调色板：0-白热，1-黑热，2-调色板2，…，8-调色板8, 9-融合1,10-彩虹,11-融合2,12-铁红1,13-铁红2,14-深褐色,15-色彩1,16-色彩2,17-冰火,18-雨,19-红热,20-绿热
            public byte byEnhancedMode; //增强方式（探测物体周边）：0-不增强，1-1，2-2，3-3，4-4
            public byte byDynamicContrastEN;    //动态对比度增强 0-1
            public byte byDynamicContrast;    //动态对比度 0-100
            public byte byJPEGQuality;    //JPEG图像质量 0-100
            public NET_DVR_CMOSMODECFG struCmosModeCfg;//CMOS模式下前端参数配置，镜头模式从能力集获取
            public byte byFilterSwitch; //滤波开关：0-不启用，1-启用
            public byte byFocusSpeed; //镜头调焦速度：0-10
            public byte byAutoCompensationInterval; //定时自动快门补偿：1-120，单位：分钟
            public byte bySceneMode;  //场景模式：0-室外，1-室内，2-默认，3-弱光
            public NET_DVR_DEFOGCFG struDefogCfg;//透雾参数
            public NET_DVR_ELECTRONICSTABILIZATION struElectronicStabilization;//电子防抖
            public NET_DVR_CORRIDOR_MODE_CCD struCorridorMode;//走廊模式
            public byte byExposureSegmentEnable; //0~不启用,1~启用  曝光时间和增益呈阶梯状调整，比如曝光往上调整时，先提高曝光时间到中间值，然后提高增益到中间值，再提高曝光到最大值，最后提高增益到最大值
            public byte byBrightCompensate;//亮度增强 [0~100]
            /*
             0-关闭、1-640*480@25fps、2-640*480@30ps、3-704*576@25fps、4-704*480@30fps、5-1280*720@25fps、6-1280*720@30fps、
             7-1280*720@50fps、8-1280*720@60fps、9-1280*960@15fps、10-1280*960@25fps、11-1280*960@30fps、
             12-1280*1024@25fps、13--1280*1024@30fps、14-1600*900@15fps、15-1600*1200@15fps、16-1920*1080@15fps、
             17-1920*1080@25fps、18-1920*1080@30fps、19-1920*1080@50fps、20-1920*1080@60fps、21-2048*1536@15fps、22-2048*1536@20fps、
             23-2048*1536@24fps、24-2048*1536@25fps、25-2048*1536@30fps、26-2560*2048@25fps、27-2560*2048@30fps、
             28-2560*1920@7.5fps、29-3072*2048@25fps、30-3072*2048@30fps、31-2048*1536@12.5、32-2560*1920@6.25、
             33-1600*1200@25、34-1600*1200@30、35-1600*1200@12.5、36-1600*900@12.5、37-1280*960@12.5fps、38-800*600@25fps、39-800*600@30fps40、
             4000*3000@12.5fps、41-4000*3000@15fps、42-4096*2160@20fps、43-3840x2160@20fps 、44-960*576@25fps、45-960*480@30fps、46-752*582@25fps、
             47-768*494@30fps、48-2560*1440@25fps、49-2560*1440@30fps 、50-720P@100fps、51-720P@120fps、52-2048*1536@50fps、53-2048*1536@60fps、
             54-3840*2160@25fps、55-3840*2160@30fps、56-4096*2160@25fps、57-4096*2160@30fps 、58-1280*1024@50fps、59-1280*1024@60fps、
             60-3072*2048@50fps、61-3072*2048@60fps、62-3072*1728@25fps、63-3072*1728@30fps、64-3072*1728@50fps、65-3072*1728@60fps、66-336*256@50fps、67-336*256@60fps、
             68-384*288@50fps、69-384*288@60fps 、70- 640 * 512@50fps 、71- 640 * 512@60fps、72-2592*1944@25fps、73-2592*1944@30fps、74-2688*1536@25fps、75-2688*1536@30fps 
             76-2592*1944@20fps、77-2592*1944@15fps、78-2688*1520@20fps、79-2688*1520@15fps、80-2688*1520@25fps、81-2688*1520@30fps、82- 2720*2048@25fps、 83- 2720*2048@30fps、
             84-336*256@25fps、85- 384*288@25fps、86-640*512@25fps、87-1280*960@50fps、88-1280*960@60fps、89-1280*960@100fps、90-1280*960@120fps、91-4000*3000@20fps、
             92-1920*1200@25fps、93-1920*1200@30fps、94-2560*1920@25fps、95-2560*1920@20fps、96-2560*1920@30fps、97-1280*1920@25fps、98-1280*1920@30fps
             99-4000*3000@24fps、100-4000*3000@25fps、101-4000*3000@10fps、102- 384*288@30fps、103-2560*1920@15fps、104-2400*3840@25fps、105-1200*1920@25fps
             106-4096*1800@30fps、107-3840*1680@30fps、108-2560*1120@30fps、109-704*320@30fps、110-1280*560@30fps、111-4096*1800@25fps、112-3840*1680@25fps
             113-2560*1120@25fps、114-704*320@25fps、115-1280*560@25fps、116-2400*3840@24fps、117-3840*2400@24fps、118-3840*2400@25fps、119-2560*1920@12.5fps
             120-2560*2048@12fps、121-2560*2048@15fps、122-2560*1536@25fps、123-2560*1536@30fps
            */
            public byte byCaptureModeN; //视频输入模式（N制）
            public byte byCaptureModeP; //视频输入模式（P制）
            public NET_DVR_SMARTIR_PARAM struSmartIRParam; //红外放过爆配置信息
            public NET_DVR_PIRIS_PARAM struPIrisParam;//PIris配置信息对应byIrisMode字段从2-PIris1开始生效
            //2014-02-25 新增参数
            public NET_DVR_LASER_PARAM_CFG struLaserParam;    //激光参数
            public NET_DVR_FFC_PARAM struFFCParam;
            public NET_DVR_DDE_PARAM struDDEParam;
            public NET_DVR_AGC_PARAM struAGCParam;
            public byte byLensDistortionCorrection;//镜头畸变校正 0-关闭,1-开启
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_SNAP_CAMERAPARAMCFG struSnapCCD; //抓拍机CCD参数，只用于抓拍机
            public NET_DVR_OPTICAL_DEHAZE struOpticalDehaze;//光学透雾参数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
            public byte[] byRes2;
        }

        public const int NET_DVR_GET_FOCUSMODECFG = 3305; //获取快球聚焦模式信息
        public const int NET_DVR_SET_FOCUSMODECFG = 3306; //设置快球聚焦模式信息

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FOCUSMODE_CFG
        {
            public uint dwSize;
            public byte byFocusMode;  /* 聚焦模式，0-自动，1-手动，2-半自动 */
            public byte byAutoFocusMode; /* 自动聚焦模式，0-关，1-模式A，2-模式B，3-模式AB，4-模式C 自动聚焦模式，需要在聚焦模式为自动时才显示*/
            public ushort wMinFocusDistance; /* 最小聚焦距离，单位CM,  0-自动，0xffff-无穷远 */
            public byte byZoomSpeedLevel;  /* 变倍速度，为实际取值，1-3 */
            public byte byFocusSpeedLevel; /* 聚焦速度，为实际取值，1-3 */
            public byte byOpticalZoom;  /* 光学变倍，0-255 */
            public byte byDigtitalZoom;  /* 数字变倍，0-255 */
            public Single fOpticalZoomLevel; /* 光学变倍(倍率值) [1,32], 最小间隔0.5 ，内部设备交互的时候*1000 */
            public uint dwFocusPos;/* dwFocusPos 是focus值（聚焦值），范围为[0x1000,0xC000]，这个值是sony坐标值，使用这个值是为了对外统一，保证不同的镜头对外focus值都转换在这个范围内 (手动聚焦模式下下应用)*/
            public byte byFocusDefinitionDisplay;// 聚焦清晰度显示，0~不显示，1~显示, 开启会在码流上显示当前镜头目标的清晰度值，用于帮助客户调焦使相机抓拍能够达到最清晰的效果，该清晰度越大代表着越清晰，清晰度范围为：0~100.0000
            public byte byFocusSensitivity; //聚焦灵敏度，范围[0,2]，聚焦模式为自动、半自动时生效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes1;
            public uint dwRelativeFocusPos;//相对focus值，范围是0-4000
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] byRes;
        }

        //前端参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CAMERAPARAMCFG
        {
            public uint dwSize;
            public NET_DVR_VIDEOEFFECT struVideoEffect;/*亮度、对比度、饱和度、锐度、色调配置*/
            public NET_DVR_GAIN struGain;/*自动增益*/
            public NET_DVR_WHITEBALANCE struWhiteBalance;/*白平衡*/
            public NET_DVR_EXPOSURE struExposure; /*曝光控制*/
            public NET_DVR_GAMMACORRECT struGammaCorrect;/*Gamma校正*/
            public NET_DVR_WDR struWdr;/*宽动态*/
            public NET_DVR_DAYNIGHT struDayNight;/*日夜转换*/
            public NET_DVR_BACKLIGHT struBackLight;/*背光补偿*/
            public NET_DVR_NOISEREMOVE struNoiseRemove;/*数字降噪*/
            public byte byPowerLineFrequencyMode; /*0-50HZ; 1-60HZ*/
            public byte byIrisMode; /*0 自动光圈 1手动光圈*/
            public byte byMirror;  /* 镜像：0 off，1- leftright，2- updown，3-center */
            public byte byDigitalZoom;  /*数字缩放:0 dsibale  1 enable*/
            public byte byDeadPixelDetect;   /*坏点检测,0 dsibale  1 enable*/
            public byte byBlackPwl;/*黑电平补偿 ,  0-255*/
            public byte byEptzGate;// EPTZ开关变量:0-不启用电子云台，1-启用电子云台
            public byte byLocalOutputGate;//本地输出开关变量0-本地输出关闭1-本地BNC输出打开 2-HDMI输出关闭  
            //20-HDMI_720P50输出开
            //21-HDMI_720P60输出开
            //22-HDMI_1080I60输出开
            //23-HDMI_1080I50输出开
            //24-HDMI_1080P24输出开
            //25-HDMI_1080P25输出开
            //26-HDMI_1080P30输出开
            //27-HDMI_1080P50输出开
            //28-HDMI_1080P60输出开
            //40-SDI_720P50,
            //41-SDI_720P60,
            //42-SDI_1080I50,
            //43-SDI_1080I60,
            //44-SDI_1080P24,
            //45-SDI_1080P25,
            //46-SDI_1080P30,
            //47-SDI_1080P50,
            //48-SDI_1080P60
            public byte byCoderOutputMode;//编码器fpga输出模式0直通3像素搬家
            public byte byLineCoding; //是否开启行编码：0-否，1-是
            public byte byDimmerMode; //调光模式：0-半自动，1-自动
            public byte byPaletteMode; //调色板：0-白热，1-黑热，2-调色板2，…，8-调色板8
            public byte byEnhancedMode; //增强方式（探测物体周边）：0-不增强，1-1，2-2，3-3，4-4
            public byte byDynamicContrastEN;    //动态对比度增强 0-1
            public byte byDynamicContrast;    //动态对比度 0-100
            public byte byJPEGQuality;    //JPEG图像质量 0-100
            public NET_DVR_CMOSMODECFG struCmosModeCfg;//CMOS模式下前端参数配置，镜头模式从能力集获取
            public byte byFilterSwitch; //滤波开关：0-不启用，1-启用
            public byte byFocusSpeed; //镜头调焦速度：0-10
            public byte byAutoCompensationInterval; //定时自动快门补偿：1-120，单位：分钟
            public byte bySceneMode;  //场景模式：0-室外，1-室内，2-默认，3-弱光
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ATMFINDINFO
        {
            public byte byTransactionType;       //交易类型 0-全部，1-查询， 2-取款， 3-存款， 4-修改密码，5-转账， 6-无卡查询 7-无卡存款， 8-吞钞 9-吞卡 10-自定义
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes;
            public uint dwTransationAmount;     //交易金额 ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SPECIAL_FINDINFO_UNION
        {
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            //public byte[] byLenth;
            public NET_DVR_ATMFINDINFO struATMFindInfo;           //ATM查询
        }

        public const int CARDNUM_LEN_OUT = 32;//外部结构体卡号长度
        public const int GUID_LEN = 16;//GUID长度

        //[DllImport(@"HCNetSDK.dll")]
        //public static extern bool NET_DVR_GetDiskList(int lUserID, )

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_FindFile_V40(int lUserID, ref NET_DVR_FILECOND_V40 pFindCond);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_FindNextFile_V40(int lFindHandle, ref NET_DVR_FINDDATA_V40 lpFindData);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND_V40
        {
            public int lChannel;
            public uint dwFileType;//不带卡号录象文件类型0xff－全部，0－定时录像,1-移动侦测 ，2－报警触发，3-报警|移动侦测 4-报警&移动侦测 5-命令触发 6-手动录像，7-智能录像，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件，14-智能交通事件
            //带卡号录像类型： 0xff－全部，0－定时录像，1-移动侦测，2－接近报警，3－出钞报警，4－进钞报警，5-命令触发，6－手动录像，7－震动报警，8-环境报警，9-智能报警，10-PIR报警，11-无线报警，12-呼救报警，13-全部事件，14-智能交通事件
            //15-越界侦测，16-区域入侵，17-声音异常，18-场景变更侦测，19-智能侦测（越界侦测|区域入侵|进入区域|离开区域|人脸识别）20-人脸侦测  21-信号量、22-回传、23-回迁录像、24-遮挡 25-pos录像
            public uint dwIsLocked;
            public uint dwUseCardNo;//是否带ATM信息进行查询：0-不带ATM信息，1-按交易卡号查询，2-按交易类型查询，3-按交易金额查询，4-按卡号、交易类型及交易金额的组合查询 5-按课程名称查找，此时卡号表示课程名称
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CARDNUM_LEN_OUT)]
            public byte[] sCardNumber;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public byte byDrawFrame; //0:不抽帧，1：抽帧
            public byte byFindType; //0:查询普通卷，1：查询存档卷
            public byte byQuickSearch; //0:普通查询，1：快速（日历）查询
            public byte bySpecialFindInfoType;    //专有查询条件类型 0-无效， 1-带ATM查询条件  
            public uint dwVolumeNum;  //存档卷号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = GUID_LEN)]
            public byte[] byWorkingDeviceGUID;//工作机GUID，通过获取N+1得到
            public NET_DVR_SPECIAL_FINDINFO_UNION uSpecialFindInfo;   //专有查询条件
            public byte byStreamType;    //0-主码流，1-子码流，2-三码流，0xff-全部
            public byte byAudioFile;    //音频文件 0-非音频文件，1-音频文件
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public byte[] byRes2;
        }

        //录象文件参数(cvr)
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINDDATA_V40
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//文件名
            public NET_DVR_TIME struStartTime;//文件的开始时间
            public NET_DVR_TIME struStopTime;//文件的结束时间
            public uint dwFileSize;//文件的大小
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;//9000设备支持,1表示此文件已经被锁定,0表示正常的文件
            public byte byFileType;  //文件类型:0－定时录像,1-移动侦测 ，2－报警触发，
            //3-报警|移动侦测 4-报警&移动侦测 5-命令触发 6-手动录像,7－震动报警，8-环境报警，9-智能报警，10-PIR报警，11-无线报警，12-呼救报警,14-智能交通事件
            public byte byQuickSearch; //0:普通查询结果，1：快速（日历）查询结果
            public byte byRes;
            public uint dwFileIndex; //文件索引号
            public byte byStreamType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            public byte[] byRes1;
        }

        public const int DEV_TYPE_NAME_LEN = 24;

        //DVR设备参数
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG_V40
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] sDVRName;//DVR名称
            public uint dwDVRID;    //DVR ID,用于遥控器 //V1.4(0-99), V1.5(0-255)
            public uint dwRecycleRecord;  //是否循环录像,0:不是; 1:是
            //以下不可更改
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN)]
            public byte[] sSerialNumber;//序列号
            public uint dwSoftwareVersion;      //软件版本号,高16位是主版本,低16位是次版本
            public uint dwSoftwareBuildDate;      //软件生成日期,0xYYYYMMDD
            public uint dwDSPSoftwareVersion;      //DSP软件版本,高16位是主版本,低16位是次版本
            public uint dwDSPSoftwareBuildDate;  // DSP软件生成日期,0xYYYYMMDD
            public uint dwPanelVersion;    // 前面板版本,高16位是主版本,低16位是次版本
            public uint dwHardwareVersion;    // 硬件版本,高16位是主版本,低16位是次版本
            public byte byAlarmInPortNum;  //DVR报警输入个数
            public byte byAlarmOutPortNum;  //DVR报警输出个数
            public byte byRS232Num;      //DVR 232串口个数
            public byte byRS485Num;      //DVR 485串口个数 
            public byte byNetworkPortNum;  //网络口个数
            public byte byDiskCtrlNum;      //DVR 硬盘控制器个数
            public byte byDiskNum;    //DVR 硬盘个数
            public byte byDVRType;    //DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;    //DVR 通道个数
            public byte byStartChan;      //起始通道号,例如DVS-1,DVR - 1
            public byte byDecordChans;      //DVR 解码路数
            public byte byVGANum;    //VGA口的个数 
            public byte byUSBNum;    //USB口的个数
            public byte byAuxoutNum;      //辅口的个数
            public byte byAudioNum;      //语音口的个数
            public byte byIPChanNum;      //最大数字通道数 低8位，高8位见byHighIPChanNum 
            public byte byZeroChanNum;      //零通道编码个数
            public byte bySupport;        //能力，位与结果为0表示不支持，1表示支持，
            //bySupport & 0x1, 表示是否支持智能搜索
            //bySupport & 0x2, 表示是否支持备份
            //bySupport & 0x4, 表示是否支持压缩参数能力获取
            //bySupport & 0x8, 表示是否支持多网卡
            //bySupport & 0x10, 表示支持远程SADP
            //bySupport & 0x20, 表示支持Raid卡功能
            //bySupport & 0x40, 表示支持IPSAN搜索
            //bySupport & 0x80, 表示支持rtp over rtsp
            public byte byEsataUseage;  //Esata的默认用途，0-默认备份，1-默认录像
            public byte byIPCPlug;      //0-关闭即插即用，1-打开即插即用
            public byte byStorageMode;  //0-盘组模式,1-磁盘配额, 2抽帧模式
            public byte bySupport1;  //能力，位与结果为0表示不支持，1表示支持
            //bySupport1 & 0x1, 表示是否支持snmp v30
            //bySupport1 & 0x2, 支持区分回放和下载
            //bySupport1 & 0x4, 是否支持布防优先级    
            //bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
            //bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
            //bySupport1 & 0x20, 表示是否支持rtsp over http    
            public ushort wDevType;//设备型号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DEV_TYPE_NAME_LEN)]
            public byte[] byDevTypeName;//设备型号名称 
            public byte bySupport2; //能力集扩展，位与结果为0表示不支持，1表示支持
            //bySupport2 & 0x1, 表示是否支持扩展的OSD字符叠加(终端和抓拍机扩展区分)
            public byte byAnalogAlarmInPortNum; //模拟报警输入个数
            public byte byStartAlarmInNo;    //模拟报警输入起始号
            public byte byStartAlarmOutNo;  //模拟报警输出起始号
            public byte byStartIPAlarmInNo;  //IP报警输入起始号
            public byte byStartIPAlarmOutNo; //IP报警输出起始号
            public byte byHighIPChanNum;      //数字通道个数，高8位 
            public byte byEnableRemotePowerOn;//是否启用在设备休眠的状态下远程开机功能，0-不启用，1-启用
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            public byte bySupportLock;        //设备支持锁定功能，该字段由SDK根据设备返回值来赋值的。bySupportLock为1时，dwSurplusLockTime和byRetryLoginTime有效
            public byte byRetryLoginTime;        //剩余可尝试登陆的次数，用户名，密码错误时，此参数有效
            public byte byPasswordLevel;      //admin密码安全等级0-无效，1-默认密码，2-有效密码,3-风险较高的密码。当用户的密码为出厂默认密码（12345）或者风险较高的密码时，上层客户端需要提示用户更改密码。      
            public byte byRes1;
            public uint dwSurplusLockTime;    //剩余时间，单位秒，用户锁定时，此参数有效
            public byte byCharEncodeType;     //字符编码类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
            public byte[] byRes2;
        }

        public const int NET_DVR_DEV_ADDRESS_MAX_LEN = 129;
        public const int NET_DVR_LOGIN_USERNAME_MAX_LEN = 64;
        public const int NET_DVR_LOGIN_PASSWD_MAX_LEN = 64;

        public delegate void LOGINRESULTCALLBACK(Int32 lUserID, UInt32 dwResult, IntPtr lpDeviceInfo, IntPtr pUser);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_USER_LOGIN_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_DEV_ADDRESS_MAX_LEN)]
            public string sDeviceAddress;
            public byte byRes1;
            public ushort wPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_USERNAME_MAX_LEN)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_PASSWD_MAX_LEN)]
            public string sPassword;
            public LOGINRESULTCALLBACK cbLoginResult;
            public IntPtr pUser;
            public bool bUseAsynLogin;
            public byte byProxyType;
            public byte byUseUTCTime;    //0-不进行转换，默认,1-接口上输入输出全部使用UTC时间,SDK完成UTC时间与设备时区的转换,2-接口上输入输出全部使用平台本地时间，SDK完成平台本地时间与设备时区的转换
            public byte byRes3;
            public byte byHttps;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 124)]
            public byte[] byRes2;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Login_V40(ref NET_DVR_USER_LOGIN_INFO pLoginInfo, ref NET_DVR_DEVICEINFO_V40 lpDeviceInfo);

        public const int XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STD_ABILITY
        {
            public IntPtr lpCondBuffer;    //[in]条件参数(码字格式),例如通道号等.可以为NULL
            public uint dwCondSize;        //[in] dwCondSize指向的内存大小
            public IntPtr lpOutBuffer;    //[out]输出参数(XML格式),不为NULL
            public uint dwOutSize;        //[in] lpOutBuffer指向的内存大小
            public IntPtr lpStatusBuffer;    //[out]返回的状态参数(XML格式),获取成功时不会赋值,如果不需要,可以置NULL
            public uint dwStatusSize;    //[in] lpStatusBuffer指向的内存大小
            public uint dwRetSize;        //[out]获取到的数据长度(lpOutBuffer或者lpStatusBuffer指向的实际数据长度)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //保留字节
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSTDAbility(int lUserID, uint dwAbilityType, IntPtr lpAbilityParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_INPUT
        {
            public uint dwSize;                    //结构体大小
            public IntPtr lpRequestUrl;                //请求信令，字符串格式
            public uint dwRequestUrlLen;            //请求信令长度，字符串长度
            public IntPtr lpInBuffer;                //输入参数缓冲区，XML格式
            public uint dwInBufferSize;            //输入参数缓冲区大小
            public uint dwRecvTimeOut;                //接收超时时间，单位：ms，填0则使用默认超时5s
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //保留字节
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_OUTPUT
        {
            public uint dwSize;                    //结构体大小
            public IntPtr lpOutBuffer;                //输出参数缓冲区，XML格式
            public uint dwOutBufferSize;            //输出参数缓冲区大小(内存大小)
            public uint dwReturnedXMLSize;            //实际输出的XML内容大小
            public IntPtr lpStatusBuffer;            //返回的状态参数(XML格式),获取命令成功时不会赋值,如果不需要,可以置NULL
            public uint dwStatusSize;                //状态缓冲区大小(内存大小)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //保留字节
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_STDXMLConfig(int lUserID, IntPtr lpInputParam, IntPtr lpOutputParam);

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_STD_CONFIG
        {
            public IntPtr lpCondBuffer;    //[in]条件参数(结构体格式),例如通道号等.可以为NULL
            public uint dwCondSize;        //[in] dwCondSize指向的内存大小
            public IntPtr lpInBuffer;        //[in]byDataType = 0时有效,输入参数(结构体格式),设置时不为NULL，获取时为NULL
            public uint dwInSize;        //[in] lpInBuffer指向的内存大小
            public IntPtr lpOutBuffer;    //[out]byDataType = 0时有效,输出参数(结构体格式),获取时不为NULL,设置时为NULL
            public uint dwOutSize;        //[in] lpOutBuffer指向的内存大小
            public IntPtr lpStatusBuffer;    //[out]返回的状态参数(XML格式),获取成功时不会赋值,如果不需要,可以置NULL
            public uint dwStatusSize;    //[in] lpStatusBuffer指向的内存大小
            public IntPtr lpXmlBuffer;    //[in/out]byDataType = 1时有效,xml格式数据
            public uint dwXmlSize;      //[in/out]lpXmlBuffer指向的内存大小,获取时同时作为输入和输出参数，获取成功后会修改会实际长度，设置时表示实际长度，而不是整个内存大小
            public byte byDataType;     //[in]输入/输出参数类型,0-使用结构体类型lpInBuffer/lpOutBuffer有效,1-使用XML类型lpXmlBuffer有效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 23)]
            public byte[] byRes1;        //保留字节
            //              uint            dwRetSize;        //获取到的数据长度(lpOutBuffer或者lpStatusBuffer指向的实际数据长度)
            //              [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            //              public byte[]   byRes2;        //保留字节
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSTDConfig(int lUserID, uint dwCommand, IntPtr lpConfigParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSTDConfig(int lUserID, uint dwCommand, IntPtr lpConfigParam);

        public const int ALARM_INFO_T = 0;
        public const int OPERATION_SUCC_T = 1;
        public const int OPERATION_FAIL_T = 2;
        public const int PLAY_SUCC_T = 3;
        public const int PLAY_FAIL_T = 4;

        public const int MAX_AUDIO_V40 = 8;
        public const int NET_DVR_USER_LOCKED = 153;//用户被锁定

        public enum DEMO_CHANNEL_TYPE
        {
            DEMO_CHANNEL_TYPE_INVALID = -1,
            DEMO_CHANNEL_TYPE_ANALOG = 0,
            DEMO_CHANNEL_TYPE_IP = 1,
            DEMO_CHANNEL_TYPE_MIRROR = 2
        }

        //device index info
        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_CHANNEL_INFO
        {
            public int iDeviceIndex;      //device index
            public int iChanIndex;      //channel index
            public DEMO_CHANNEL_TYPE iChanType;
            public int iChannelNO;         //channel NO.       
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chChanName;         //channel name
            public uint dwProtocol;      //network protocol
            public uint dwStreamType; //码流类型，0-主码流，1-子码流，2-码流3，
            public uint dwLinkMode;//码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-RTP/RTSP,5-RSTP/HTTP
            public bool bPassbackRecord; //0-不启用录像回传,1启用录像回传
            public uint dwPreviewMode;  //预览模式 0-正常模式 1-延时预览
            public int iPicResolution;    //resolution
            public int iPicQuality;    //image quality
            public Int32 lRealHandle;          //preview handle
            public bool bLocalManualRec;     //manual record
            public bool bAlarm;    //alarm
            public bool bEnable;      //enable
            public uint dwImageType;  //channel status icon
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string chAccessChanIP;   //ip addr of IP channel
            public uint nPreviewProtocolType;    //取流协议类型 0-私有协议 1-RTSP协议
            public IntPtr pNext;

            public void init()
            {
                iDeviceIndex = -1;
                iChanIndex = -1;
                iChannelNO = -1;
                iChanType = DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_INVALID;
                chChanName = null;
                dwProtocol = 0;

                dwStreamType = 0;
                dwLinkMode = 0;

                iPicResolution = 0;
                iPicQuality = 2;

                lRealHandle = -1;
                bLocalManualRec = false;
                bAlarm = false;
                bEnable = false;
                dwImageType = CHAN_ORIGINAL;
                chAccessChanIP = null;
                pNext = IntPtr.Zero;
                dwPreviewMode = 0;
                bPassbackRecord = false;
                nPreviewProtocolType = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MODE
        {
            public byte byGetStreamType; //取流方式GET_STREAM_TYPE，0-直接从设备取流，1-从流媒体取流、2-通过IPServer获得ip地址后取流,3.通过IPServer找到设备，再通过流媒体去设备的流
            //4-通过流媒体由URL去取流,5-通过hkDDNS取流，6-直接从设备取流(扩展)，使用NET_DVR_IPCHANINFO_V40结构, 7-通过RTSP协议方式进行取流
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_GET_STREAM_UNION uGetStream;    // 不同取流方式结构体
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPSERVER_STREAM
        {
            public byte byEnable;   // 是否在线
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struIPServer;    //IPServer 地址
            public ushort wPort;                  //IPServer 端口
            public ushort wDvrNameLen;            // DVR 名称长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRName;              // DVR名称
            public ushort wDVRSerialLen;          // 序列号长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;      // DVR序列号长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;             // DVR 登陆用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord;             // DVR登陆密码
            public byte byChannel;              // DVR 通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public ushort[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDNS_STREAM_CFG
        {
            public byte byEnable;   // 是否启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struStreamServer;
            public ushort wStreamServerPort;           //流媒体服务器端口   
            public byte byStreamServerTransmitType;  //流媒体传输协议类型 0-TCP，1-UDP
            public byte byRes2;
            public NET_DVR_IPADDR struIPServer;          //IPSERVER地址
            public ushort wIPServerPort;        //IPserver端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;             //DVR名称 
            public ushort wDVRNameLen;            // DVR名称长度
            public ushort wDVRSerialLen;          // 序列号长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRSerialNumber;       // DVR序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;              // DVR 登陆用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassWord;              // DVR登陆密码
            public ushort wDVRPort;   //DVR端口号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
            public byte byChannel;              // DVR 通道
            public byte byTransProtocol; //传输协议类型0-TCP，1-UDP
            public byte byTransMode; //传输码流模式 0－主码流 1－子码流
            public byte byFactoryType; //前端设备厂家类型,通过接口获取
        }

        public const int URL_LEN = 240;  //URL长度

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_URL
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = URL_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] strURL;
            public byte byTransPortocol; // 传输协议类型 0-tcp  1-UDP
            public ushort wIPID;  //设备ID号，wIPID = iDevInfoIndex + iGroupNO*64 +1
            public byte byChannel;  //通道号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HKDDNS_STREAM
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byDDNSDomain;   // hiDDNS服务器
            public ushort wPort;                  // hiDDNS 端口
            public ushort wAliasLen;              // 别名长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlias;              // 别名
            public ushort wDVRSerialLen;          // 序列号长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;     // DVR序列号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;              // DVR 登陆用户名
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord;
            public byte byChannel;              // DVR通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO_V40
        {
            public byte byEnable;
            public byte byRes1;
            public ushort wIPID;                  //IP设备ID
            public uint dwChannel;    //通道号
            public byte byTransProtocol;  //传输协议类型0-TCP，1-UDP
            public byte byTransMode;      //传输码流模式 0－主码流 1－子码流
            public byte byFactoryType;      /*前端设备厂家类型,通过接口获取*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 241, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GET_STREAM_UNION
        {
            public NET_DVR_IPCHANINFO struChanInfo;    /*IP通道信息*/
            //public NET_DVR_IPSERVER_STREAM struIPServerStream;  // IPServer去流
            //public NET_DVR_PU_STREAM_CFG struPUStream;     //  通过前端设备获取流媒体去流
            //public NET_DVR_DDNS_STREAM_CFG struDDNSStream;     //通过IPServer和流媒体取流
            //public NET_DVR_PU_STREAM_URL struStreamUrl;        //通过流媒体到url取流
            //public NET_DVR_HKDDNS_STREAM struHkDDNSStream;   //通过hiDDNS去取流
            //public NET_DVR_IPCHANINFO_V40 struIPChan; //直接从设备取流（扩展）
        }

        public const int MAX_IP_DEVICE_V40 = 64;// 允许接入的最大IP设备数 最多可添加64个 IVMS 2000等新设备
        // 通道资源配置 (NET_DVR_IPPARACFG_V40结构)
        public const int NET_DVR_GET_IPPARACFG_V40 = 1062;// 获取IP接入配置
        public const int NET_DVR_SET_IPPARACFG_V40 = 1063;// 设置IP接入配置

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V40
        {
            public uint dwSize;
            public uint dwGroupNum;        //     设备支持的总组数    
            public uint dwAChanNum;        //最大模拟通道个数
            public uint dwDChanNum;                  //数字通道个数
            public uint dwStartDChan;              //起始数字通道
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30)]
            public byte[] byAnalogChanEnable;       /* 模拟通道是否启用，从低到高表示1-64通道，0表示无效 1有效 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE_V40)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30)]
            public NET_DVR_STREAM_MODE[] struStreamMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO_V40
        {
            public uint dwIPID;        /* IP设备ID */
            public uint dwAlarmIn;    /* IP设备ID对应的报警输入号 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        public const int MAX_IP_ALARMIN_V40 = 4096;//允许加入的最多报警输入数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG_V40
        {
            public uint dwSize;  //结构体长度
            public uint dwCurIPAlarmInNum;  //当前报警输入口数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN_V40)]
            public NET_DVR_IPALARMININFO_V40[] struIPAlarmInInfo; /* IP报警输入*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO_V40
        {
            public uint dwIPID;                 /* IP设备ID */
            public uint dwAlarmOut;    /* IP设备ID对应的报警输出号 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }
        public const int NET_DVR_CONTROL_PTZ_PATTERN = 3313;
        public const int DELETE_CRUISE = 45;
        public const int DELETE_ALL_CRUISE = 46;
        public const int STOP_CRUISE = 44;
        public struct NET_DVR_PTZ_PATTERN
        {
            public uint dwSize;
            public uint dwChannel; //通道号
            public uint dwPatternCmd; //云台轨迹操作命令码,详见下面定义
            public uint dwPatternID; //云台轨迹ID（删除所有轨迹时无效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        // 辅助设备升级条件结构体
        public struct NET_DVR_AUXILIARY_DEV_UPGRADE_PARAM
        {
            public uint dwSize;
            public uint dwDevNo;
            public byte byDevType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 131, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        public enum ENUM_UPGRADE_TYPE
        {
            ENUM_UPGRADE_DVR = 0, //普通设备升级
            ENUM_UPGRADE_ADAPTER = 1, //DVR适配器升级
            ENUM_UPGRADE_VCALIB = 2,  //智能库升级
            ENUM_UPGRADE_OPTICAL = 3, //光端机升级
            ENUM_UPGRADE_ACS = 4, //门禁系统升级
            ENUM_UPGRADE_AUXILIARY_DEV = 5,//辅助设备升级
        }
        public const int MAX_IP_ALARMOUT_V40 = 4096;//允许加入的最多报警输出数

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG_V40
        {
            public uint dwSize;  //结构体长度
            public uint dwCurIPAlarmOutNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT_V40)]
            public NET_DVR_IPALARMOUTINFO_V40[] struIPAlarmOutInfo; /* IP报警输出*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        //2011-12-16 被动解码结构 silujie
        [StructLayout(LayoutKind.Sequential)]
        public struct PASSIVEDECODE_CHANINFO
        {
            public Int32 lPassiveHandle;
            public Int32 lRealHandle;
            public Int32 lUserID;
            public Int32 lSel;
            public IntPtr hFileThread;
            public IntPtr hFileHandle;
            public IntPtr hExitThread;
            public IntPtr hThreadExit;
            public string strRecordFilePath;

            public void init()
            {
                lPassiveHandle = -1;
                lRealHandle = -1;
                lUserID = -1;
                lSel = -1;
                hFileThread = IntPtr.Zero;
                hFileHandle = IntPtr.Zero;
                hExitThread = IntPtr.Zero;
                hThreadExit = IntPtr.Zero;
                strRecordFilePath = null;
            }
        }

        public const int NET_DVR_GET_DEVICECFG_V40 = 1100;//获取扩展设备参数 
        public const int NET_DVR_SET_DEVICECFG_V40 = 1101;//设置扩展设备参数
        public const int NET_DVR_GET_IPALARMINCFG_V40 = 6183;//获取IP报警输入接入配置信息

        public const int MAX_DEVICES = 512;//max device number
        //bmp status
        public const int TREE_ALL = 0;//device list    
        public const int DEVICE_LOGOUT = 1;//device not log in
        public const int DEVICE_LOGIN = 2;//device login
        public const int DEVICE_FORTIFY = 3;//on guard
        public const int DEVICE_ALARM = 4;//alarm on device
        public const int DEVICE_FORTIFY_ALARM = 5;//onguard & alarm on device
        public const int CHAN_ORIGINAL = 6;//no preview, no record
        public const int CHAN_PLAY = 7;   //preview
        public const int CHAN_RECORD = 8;   //record
        public const int CHAN_PLAY_RECORD = 9;   //preview and record

        public const int CHAN_ALARM = 10;   //no preview, no record, only alarm
        public const int CHAN_PLAY_ALARM = 11;   //review, no record, with alarm info
        public const int CHAN_PLAY_RECORD_ALARM = 12;   //preview & record & alarm
        public const int CHAN_OFF_LINE = 13;     //channel off-line

        [StructLayout(LayoutKind.Sequential)]
        public struct PREVIEW_IFNO
        {
            public int iDeviceIndex;      //device index
            public int iChanIndex;      //channel index
            public byte PanelNo;
            public int lRealHandle;
            public IntPtr hPlayWnd;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_DEVICE_INFO
        {
            public int iDeviceIndex;      //device index
            public Int32 lLoginID;    //ID
            public uint dwDevSoftVer;      //erserved
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chLocalNodeName;      //local device node
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chDeviceName;         //device name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceIP;           //device IP: IP,pppoe address, or network gate address, etc
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceIPInFileName; //if chDeviceIP includes ':'(IPV6),change it to '.', for '.'is not allowed in window's file name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string chLoginUserName;      //login user name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string chLoginPwd;           //password
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceMultiIP;      //multi-cast group address
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string chSerialNumber;       //SN
            public int iDeviceChanNum;      //channel numder  (analog + ip)
            public int iStartChan;    //start channel number
            public int iStartDChan;    //start IP channel number
            public int iDeviceType;      //device type
            public int iDiskNum;    //HD number
            public Int32 lDevicePort;      //port number
            public byte byLoginMode;
            public int iAlarmInNum;      //alarm in number
            public int iAlarmOutNum;      //alarm out number
            public int iAudioNum;    //voice talk number
            public int iAnalogChanNum;      //analog channel number
            public int iIPChanNum;    //IP channel number
            public int iGroupNO;               //IP Group NO.
            public bool bCycle;        //if this device is on cycle recording
            public bool bPlayDevice;      //will be added later
            public bool bVoiceTalk;    //on voice talkor not
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public Int32[] lAudioHandle;         //voicebroadcast handle
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public bool[] bCheckBroadcast;      //Add to broad cast group
            public Int32 lFortifyHandle;      //on guard handle
            public bool bAlarm;        //whether there is alarm
            public int iDeviceLoginType;  //register mode  0 - fix IP   1- IPSERVER mode    2 -  domain name 
            public uint dwImageType;      //device status icon
            public bool bIPRet;        //support IP conection
            public int iEnableChanNum;      //valid channel number
            public bool bDVRLocalAllRec;  //local recording
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public Int32[] lVoiceCom;            //voice transmit
            public Int32 lFirstEnableChanIndex;      //first enabled channel index
            public Int32 lTranHandle;    //232 transparent channel handle
            public byte byZeroChanNum;  //Zero channel number
            public byte byMainProto;      //main stream protocol type 0-Private, 1-rtp/tcp, 2-rtp/rtsp
            public byte bySubProto;    //sub stream protocol type 0-Private, 1-rtp/tcp, 2-rtp/rtsp
            public byte bySupport;             //ability
            public byte byStartDTalkChan;
            public byte byAudioInputChanNum;
            public byte byStartAudioInputChanNo;
            public byte byLanguageType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_CHANNUM_V40)]
            public STRU_CHANNEL_INFO[] pStruChanInfo; //channel structure
            public NET_DVR_IPPARACFG_V40[] pStruIPParaCfgV40;    //IP channel parameters
            public NET_DVR_IPALARMINCFG struAlarmInCfg;
            public NET_DVR_IPALARMINCFG_V40 pStruIPAlarmInCfgV40;  // IP alarm In parameters
            public NET_DVR_IPALARMOUTCFG_V40 pStruIPAlarmOutCfgV40; // IP alarm Out parameters
            public NET_DVR_IPALARMOUTCFG struAlarmOutCfg;
            public IntPtr pNext;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public STRU_CHANNEL_INFO[] struZeroChan;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecretKey;
            public int iAudioEncType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public PASSIVEDECODE_CHANINFO[] struPassiveDecode;
            public byte bySupport1;    //能力，位与结果为0表示不支持，1表示支持
            // bySupport1 & 0x1, 表示是否支持snmp v30
            //bySupport1 & 0x2, 支持区分回放和下载    
            //bySupport1 & 0x4, 是否支持布防优先级    
            //bySupport1 & 0x8, 智能设备是否支持布防时间段扩展
            //bySupport1 & 0x10, 表示是否支持多磁盘数（超过33个）
            //bySupport1 & 0x40 表示是否支持延迟预览
            public byte bySupport2; //能力集扩展，位与结果为0表示不支持，1表示支持
            //bySupport2 & 0x1, 表示解码器是否支持通过URL取流解码
            //bySupport2 & 0x2,  表示支持FTPV40
            //bySupport2 & 0x4,  表示支持ANR(断网录像)
            public byte byStartIPAlarmOutNo;  //起始IP报警输出号
            public byte byMirrorChanNum; //镜像通道个数，<录播主机中用于表示导播通道>
            public ushort wStartMirrorChanNo;  //起始镜像通道号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public STRU_CHANNEL_INFO[] struMirrorChan;
            public byte bySupport5;
            public byte bySupport7;// bySupport7 & 0x2  表示设备是否支持 IPC HVT 模式扩展
            public byte byCharaterEncodeType;
            public bool bInit;
            public byte byPanelNo; //
            public bool bHttps;

            public void Init()
            {
                iGroupNO = -1;
                iDeviceIndex = -1;
                lLoginID = -1;
                dwDevSoftVer = 0;
                chLocalNodeName = null;
                chDeviceName = null;
                chDeviceIP = null;
                chDeviceIPInFileName = null;
                //chDevNetCard1IP[0]    = '\0';
                chLoginUserName = null;
                chLoginPwd = null;
                chDeviceMultiIP = null;
                chSerialNumber = null;
                iDeviceChanNum = -1;
                iStartChan = 0;
                iDeviceType = 0;
                iDiskNum = 0;
                lDevicePort = 8000;
                iAlarmInNum = 0;
                iAlarmOutNum = 0;
                iAnalogChanNum = 0;
                iIPChanNum = 0;
                byAudioInputChanNum = 0;
                byStartAudioInputChanNo = 0;
                bCycle = false;
                bPlayDevice = false;
                bVoiceTalk = false;
                lAudioHandle = new Int32[MAX_AUDIO_V40];
                bCheckBroadcast = new bool[MAX_AUDIO_V40];
                lFortifyHandle = -1;
                bAlarm = false;
                iDeviceLoginType = 0;
                dwImageType = DEVICE_LOGOUT;
                bIPRet = false;
                pNext = IntPtr.Zero;
                lVoiceCom = new Int32[MAX_AUDIO_V40];
                for (int i = 0; i < lVoiceCom.Length; i++)
                {
                    lVoiceCom[i] = -1;
                }
                lFirstEnableChanIndex = 0;
                lTranHandle = -1;
                byZeroChanNum = 0;
                lAudioHandle[0] = -1;
                lAudioHandle[1] = -1;
                struAlarmInCfg = new NET_DVR_IPALARMINCFG();
                struAlarmOutCfg = new NET_DVR_IPALARMOUTCFG();
                sSecretKey = "StreamNotEncrypt";
                iAudioEncType = 0;
                bySupport1 = 0;
                bySupport2 = 0;
                bySupport5 = 0;
                bySupport7 = 0;
                byStartDTalkChan = 0;
                byLanguageType = 0;
                byCharaterEncodeType = 0;
                bInit = true;
                byPanelNo = 4;
                bHttps = false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SERIALSTART_V40   //透明通道
        {
            public uint dwSize;         //结构体大小
            public uint dwSerialType;    //串口号（1-232串口，2-485串口）
            public byte bySerialNum;   //串口编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 255, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public delegate void CallBackSerialData(int lSerialHandle, int lCHannel, IntPtr pRecvDataBuffer, uint dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SerialStart_V40(int lUserID, IntPtr lpInBuffer, int dwInBufferSize, CallBackSerialData fSerialDataCallBack, IntPtr pUser);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl_V40(int lPlayHandle, int dwControlCode,
            IntPtr lpInBuffer, uint dwInLen, IntPtr lpOutBuffer, IntPtr lpOutLen);


        public delegate bool CHARENCODECONVERT(string strInput, uint dwInputLen, uint dwInEncodeType, string strOutput, uint dwOutputLen, uint dwOutEncodeType);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_LOCAL_BYTE_ENCODE_CONVERT
        {
            CHARENCODECONVERT fnCharConvertCallBack;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public enum CHAR_ENCODE_TYPE
        {
            ENUM_MEM_CHAR_ENCODE_ERR = -1,         //Error   
            ENUM_MEM_CHAR_ENCODE_NO = 0,          //Don't know.
            ENUM_MEM_CHAR_ENCODE_CN = 1,          //EUC-CN, GB2312
            ENUM_MEM_CHAR_ENCODE_GBK = 2,          //GBK
            ENUM_MEM_CHAR_ENCODE_BIG5 = 3,          //BIG5
            ENUM_MEM_CHAR_ENCODE_JP = 4,          //JISX0208-1, EUC-JP
            ENUM_MEM_CHAR_ENCODE_KR = 5,          //EUC-KR
            ENUM_MEM_CHAR_ENCODE_UTF8 = 6,          //UTF-8
            ENUM_MEM_CHAR_ENCODE_ISO8859_1 = 7,    //ISO-8859-n: ENUM_MEM_CHAR_ENCODE_ISO8859_1 + n -1
        }

        [DllImport("kernel32.dll")]
        public static extern int MultiByteToWideChar(int CodePage, int dwFlags, string lpMultiByteStr, int cchMultiByte, [MarshalAs(UnmanagedType.LPWStr)]string lpWideCharStr, int cchWideChar);

        [DllImport("Kernel32.dll")]
        public static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, [In, MarshalAs(UnmanagedType.LPWStr)]string lpWideCharStr, int cchWideChar,
        [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder lpMultiByteStr, int cbMultiByte, IntPtr lpDefaultChar, // Defined as IntPtr because in most cases is better to pass
        IntPtr lpUsedDefaultChar);


        public enum NET_SDK_LOCAL_CFG_TYPE
        {
            NET_SDK_LOCAL_CFG_TYPE_TCP_PORT_BIND = 0,  //本地TCP端口绑定配置，对应结构体NET_DVR_LOCAL_TCP_PORT_BIND_CFG
            NET_SDK_LOCAL_CFG_TYPE_UDP_PORT_BIND,      //本地UDP端口绑定配置，对应结构体NET_DVR_LOCAL_UDP_PORT_BIND_CFG
            NET_SDK_LOCAL_CFG_TYPE_MEM_POOL,    //内存池本地配置，对应结构体NET_DVR_LOCAL_MEM_POOL_CFG
            NET_SDK_LOCAL_CFG_TYPE_MODULE_RECV_TIMEOUT,  //按模块配置超时时间，对应结构体NET_DVR_LOCAL_MODULE_RECV_TIMEOUT_CFG
            NET_SDK_LOCAL_CFG_TYPE_ABILITY_PARSE,      //是否使用能力集解析库，对应结构体NET_DVR_LOCAL_ABILITY_PARSE_CFG
            NET_SDK_LOCAL_CFG_TYPE_TALK_MODE,    //对讲模式，对应结构体NET_DVR_LOCAL_TALK_MODE_CFG
            NET_SDK_LOCAL_CFG_TYPE_PROTECT_KEY,    //密钥设置，对应结构体NET_DVR_LOCAL_PROTECT_KEY_CFG
            NET_SDK_LOCAL_CFG_TYPE_CFG_VERSION,             //用于测试版本头的设备端兼容情况, 只有在设置参数时才起作用。
            NET_SDK_LOCAL_CFG_TYPE_RTSP_PARAMS,    //rtsp参数配置，对于结构体NET_DVR_RTSP_PARAMS_CFG
            NET_SDK_LOCAL_CFG_TYPE_SIMXML_LOGIN,            //在登录时使用模拟能力补充support字段, 对应结构NET_DVR_SIMXML_LOGIN
            NET_SDK_LOCAL_CFG_TYPE_CHECK_DEV,                //心跳交互间隔时间
            NET_SDK_LOCAL_CFG_TYPE_SECURITY,                  //SDK本次安全配置，
            NET_SDK_LOCAL_CFG_TYPE_EZVIZLIB_PATH,            //配置萤石云通信库地址，
            NET_SDK_LOCAL_CFG_TYPE_CHAR_ENCODE,               //13.配置字符编码相关处理回调
            NET_SDK_LOCAL_CFG_TYPE_PROXYS                     //设置获取代
        }
        //设置SDK本地参数
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSDKLocalCfg(NET_SDK_LOCAL_CFG_TYPE enumType, IntPtr lpInBuff);

        public const int CP_UTF8 = 65001;
        public const int CP_ACP = 0;

        // 美分定制菜单输出模式外部命令
        public const int NET_DVR_GET_MEMU_OUTPUT_MODE = 155649;// 获取菜单输出模式
        public const int NET_DVR_SET_MEMU_OUTPUT_MODE = 155650;// 设置菜单输出模式

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MENU_OUTPUT_MODE
        {
            public uint dwSize;
            public byte byMenuOutputMode; //非同源设备：0-Auto 1-主CVBS 2-HDMI 3-VGA 同源设备：0-Auto 1-主CVBS 2-HDMI/VGA
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //default image parameter
        public const int DEFAULT_BRIGHTNESS = 6;        //default brightness
        public const int DEFAULT_CONTRAST = 6;            //default contrast
        public const int DEFAULT_SATURATION = 6;        //default saturation
        public const int DEFAULT_HUE = 6;                //default hue
        public const int DEFAULT_SHARPNESS = 6;            //default sharpness
        public const int DEFAULT_DENOISING = 6;            //default denoising
        public const int DEFAULT_VOLUME = 50;           //default volume
        public const int MAX_OUTPUTS = 512;

        [StructLayout(LayoutKind.Sequential)]
        public struct VIDEO_INFO
        {
            public uint m_iBrightness;                //1-10
            public uint m_iContrast;                //1-10
            public uint m_iSaturation;                //1-10
            public uint m_iHue;                        //1-10
            public uint m_iSharpness;
            public uint m_iDenoising;
            public void Init()
            {
                m_iBrightness = DEFAULT_BRIGHTNESS;
                m_iContrast = DEFAULT_CONTRAST;
                m_iSaturation = DEFAULT_SATURATION;
                m_iHue = DEFAULT_HUE;
                m_iSharpness = DEFAULT_SHARPNESS;
                m_iDenoising = DEFAULT_DENOISING;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LOCAL_RECORD_TIME
        {
            public ushort iStartHour;
            public ushort iStartMinute;
            public ushort iStopHour;
            public ushort iStopMinute;
            public ushort iStartTime;
            public ushort iStopTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StruDomain
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] szDomain;//服务器地址，域名 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StruAddrIP
        {
            public NET_DVR_IPADDR struIp;/*IP地址*/        //IPv4 IPv6地址, 144字节
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UnionServer
        {
            public StruDomain struDomain;
            public StruAddrIP stryAddrIP;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SLAVECAMERA_CFG
        {
            public uint dwSize;
            public byte byAddressType;   //0-实际ipv4 ipv6地址 1-域名
            // 控制unionServer是使用实际地址还是域名
            public ushort wPort;            /*端口*/
            public byte byLoginStatus; /*从设备的登陆状态 0-logout,1-login*/
            public UnionServer unionServer;//使用联合体结构，通过byAddressType字段表示是IP地址还是域名 64    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szUserName;/*用户名*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szPassWord;/*密码*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_LOCAL_PARAM
        {
            public bool bReconnect;                 //reconnect
            public bool bCyclePlay;                    //cycle play
            public int iCycleTime;                    //cycle time, default 20
            public bool bUseCard;                    //hadrware decode
            public bool bNTSC;                        //hardware decode mode,FALSE,PAL;TRUE,NTSC;default as pal
            public bool bAutoRecord;                //auto record;
            public bool bCycleRecord;                //cycle record
            public int iStartRecordDriver;            //client record starting HD dirve
            public int iEndRecordDriver;            //client record stop HD drive
            public int iRecFileSize;                //record file size
            public int iRecordFileInterval;            //record file interval
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chDownLoadPath;           //remote file download directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chPictureSavePath;        //image capture directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chRemoteCfgSavePath;        //remote config file saving directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chClientRecordPath;        //client record path
            public bool bAutoCheckDeviceTime;        //check time with device
            public Int32 lCheckDeviceTime;            //check time interval
            public int iAlarmDelayTime;                //alarm delay time
            public int iAlarmListenPort;
            public bool bAutoSaveLog;                //auto save local log info
            public bool bAlarmInfo;                    //display alarm info on log list
            public bool bSuccLog;                    //display log access on log list
            public bool bFailLog;                    //display filaure operation on log list
            public bool bAllDiskFull;                //HD full

            //preview
            public bool bPlaying;                    //on playing
            public bool bCycling;                    //cycle playing
            public bool bPaused;                    //cycle pause
            public bool bNextPage;                    //next page
            public bool bFrontPage;                    //previous page
            public bool bEnlarged;                    //enlarge image
            public bool bFullScreen;                //full screen
            public bool bMultiScreen;                //multi-split-window full screen
            public bool bNoDecode;                    //soft decode or not
            public bool bPreviewBlock;                //preview block or not

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_OUTPUTS)]
            public VIDEO_INFO[] struVideoInfo;                //video parameter
            public int iVolume;                        //volume
            public bool bBroadCast;                    //voice broadcast
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public string chIPServerIP;
            public bool bOutputDebugString;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
            public LOCAL_RECORD_TIME[] struLocalRecordTime;
            public uint dwBFrameNum;//throw B frame number
            public uint nLogLevel;
            public bool bCycleWriteLog;
            public uint nTimeout;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public NET_DVR_SLAVECAMERA_CFG[] struSlaveCameraCfg;
            public bool bInit;
            public void Init()
            {
                bReconnect = true;
                bCyclePlay = false;
                iCycleTime = 20;
                bUseCard = false;
                bNTSC = false;
                bAutoRecord = false;
                bCycleRecord = false;
                iStartRecordDriver = 0;
                iEndRecordDriver = 0;
                iRecFileSize = 1;
                iRecordFileInterval = 60;
                chDownLoadPath = "C:\\DownLoad";
                chPictureSavePath = "C:\\Picture";
                chRemoteCfgSavePath = "C:\\SaveRemoteCfgFile";
                chClientRecordPath = "C:\\mpeg4record\\2008-04-30";
                chIPServerIP = "172.7.28.123";

                bAutoCheckDeviceTime = false;
                lCheckDeviceTime = 0;

                iAlarmDelayTime = 10;
                iAlarmListenPort = 7200;

                bAutoSaveLog = true;
                bAlarmInfo = true;
                bSuccLog = true;
                bFailLog = true;

                bAllDiskFull = false;
                bPlaying = false;
                bCycling = false;
                bPaused = false;
                bNextPage = false;
                bFrontPage = false;
                bEnlarged = false;
                bFullScreen = false;
                bMultiScreen = false;
                iVolume = DEFAULT_VOLUME;
                bBroadCast = false;
                bNoDecode = false;
                bPreviewBlock = true;
                bOutputDebugString = false;
                dwBFrameNum = 0;
                nLogLevel = 3;
                bCycleWriteLog = false;
                nTimeout = 5000;
                struVideoInfo = new VIDEO_INFO[MAX_OUTPUTS];
                for (int i = 0; i < MAX_OUTPUTS; i++)
                {
                    struVideoInfo[i].Init();
                }
                struLocalRecordTime = new LOCAL_RECORD_TIME[28];
                struSlaveCameraCfg = new NET_DVR_SLAVECAMERA_CFG[4];
                bInit = true;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VOD_PARA
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struIDInfo;
            public NET_DVR_TIME struBeginTime;
            public NET_DVR_TIME struEndTime;
            public IntPtr hWnd;
            public byte byDrawFrame; //0:不抽帧，1：抽帧
            public byte byVolumeType;  //0-普通录像卷  1-存档卷
            public byte byVolumeNum;  //卷号，目前指存档卷号
            public byte byStreamType;   //码流类型 0-主码流， 1-子码流，2-码流三
            public uint dwFileIndex;      //存档卷上的录像文件索引，搜索存档卷录像时返回的值
            public byte byAudioFile;    //音频文件0-否，1-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public void Init()
            {
                struBeginTime = new NET_DVR_TIME();
                struEndTime = new NET_DVR_TIME();
                struIDInfo = new NET_DVR_STREAM_INFO();
                struIDInfo.byID = new byte[STREAM_ID_LEN];
                hWnd = IntPtr.Zero;
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLAYCOND
        {
            public uint dwChannel;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public byte byDrawFrame;  //0:不抽帧，1：抽帧
            public byte byStreamType; //码流类型，0-主码流 1-子码流 2-码流三
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;    //保留
        }


        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_PlayBackByTime_V40(uint lUserID, ref NET_DVR_VOD_PARA pVodPara);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_PlayBackReverseByTime_V40(uint lUserID, IntPtr hWnd, ref CHCNetSDK.NET_DVR_PLAYCOND pPlayCond);

        public const int NET_DVR_GET_WORK_STATUS = 6189; //Get device status

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GETWORKSTATE_COND
        {
            public uint dwSize;
            public byte byFindHardByCond;
            public byte byFindChanByCond;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;  //保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindHardStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindChanNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  //保留
        }

        public const int MAX_ALARMIN_V40 = 4128;
        public const int MAX_ALARMOUT_V40 = 4128;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V40
        {
            public uint dwChannel;
            public uint dwDeviceStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmInStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmOutStatic;
            public uint dwLocalDisplay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioInChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  //保留
        }

        //温度检测报警
        public const int COMM_THERMOMETRY_ALARM = 0x5212;  //温度报警上传  5210应改为ox5212,手册上是5212
        public const int COMM_THERMOMETRY_DIFF_ALARM = 0x5211;  //温差报警上传

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_PTZ_INFO
        {
            public float fPan;
            public float fTilt;
            public float fZoom;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        //坐标值归一化,浮点数值为当前画面的百分比大小, 精度为小数点后三位
        //点坐标结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POINT
        {
            public float fX;                                // X轴坐标, 0.001~1
            public float fY;                                //Y轴坐标, 0.001~1
        }

        //多边型结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POLYGON
        {
            public uint dwPointNum;                                  //有效点 大于等于3，若是3点在一条线上认为是无效区域，线交叉认为是无效区域 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = CHCNetSDK.VCA_MAX_POLYGON_POINT_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPos; //多边形边界点,最多十个 
        }

        //温度报警（检测温度和配置温度比较报警）
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_ALARM
        {
            public uint dwSize;
            public uint dwChannel;//通道号
            public byte byRuleID;//规则ID
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)
            public ushort wPresetNo; //预置点号
            public NET_PTZ_INFO struPtzInfo;//ptz坐标信息
            public byte byAlarmLevel;//0-预警 1-报警
            public byte byAlarmType;/*报警类型 0-最高温度 1-最低温度 2-平均温度 3-温差*/
            public byte byAlarmRule;//0-大于，1-小于
            public byte byRuleCalibType;//规则标定类型 0-点，1-框，2线
            public NET_VCA_POINT struPoint;//点测温坐标（当规则标定类型为点的时候生效）
            public NET_VCA_POLYGON struRegion;//区域（当规则标定类型为框的时候生效）
            public float fRuleTemperature;/*配置规则温度,精确到小数点后一位(-40-1000),（浮点数+100） */
            public float fCurrTemperature;/*当前温度,精确到小数点后一位(-40-1000),（浮点数+100） */
            public uint dwPicLen;//可见光图片长度
            public uint dwThermalPicLen;//热成像图片长度
            public uint dwThermalInfoLen;//热成像附加信息长度
            public IntPtr pPicBuff; ///可见光图片指针
            public IntPtr pThermalPicBuff;// 热成像图片指针
            public IntPtr pThermalInfoBuff; //热成像附加信息指针
            public NET_VCA_POINT struHighestPoint;//线、框测温最高温度位置坐标（当规则标定类型为线、框的时候生效）
            public Single fHighestTemperature;//线、框测温最高温度（当规则标定类型为线、框的时候生效）,精确到小数点后一位(-40-1000),（浮点数+100）
            public NET_VCA_POINT struLowestPoint;//线、框测温最低温度位置坐标（当规则标定类型为线、框的时候生效）
            public Single fLowestTemperature;//线、框测温最低温度（当规则标定类型为线、框的时候生效）,精确到小数点后一位(-40-1000),（浮点数+100）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TEMPERATURE_COLOR
        {
            /*
            选择0~高温报警类型时，<highTemperature>字段生效,当高于该温度值时，会有进行颜色标注，
            选择1~低温报警类型时, <lowTemperature>字段生效,当低于该温度值时，会有进行颜色标注。
            选择2~区间报警类型时，<highTemperature>、<lowTemperature>字段生效，当在温度在该温度区间时，会有进行颜色标注。
            选择3~保温报警类型时，<highTemperature>、<lowTemperature>字段生效，当温度不在该温度区间时，会有进行颜色标注。 
            选择4~为无报警类型，<nullAlarm>字段生效，关闭报警，*/
            public byte byType;//测温报警颜色控制类型，0~无报警类型（关闭），1~高温报警类型，2~低温报警类型，3~区间报警类型，4~保温报警类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint iHighTemperature;//高温值，-273~10000
            public uint iLowTemperature;//低温值，-273~10000
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_BASICPARAM
        {
            public uint dwSize;//结构体大小
            public byte byEnabled;  //是否使能：0- 否，1- 是
            public byte byStreamOverlay; //码流叠加温度信息：0- 否，1- 是
            public byte byPictureOverlay;//抓图叠加温度信息：0- 否，1- 是
            public byte byThermometryRange;//测温范围(): 0-默认值,1-(-20~150),2-(0~550)（这里以摄氏度为单位计算）,3-(摄氏度:0-650℃；华氏温度:32-1200℉),4-（摄氏度: -40-150℃）,5-(摄氏度: 0~1200℃)（这里以摄氏度为单位计算，根据测温单位设定不同测温范围的显示）
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)。
            public byte byThermometryCurve;//测温曲线模式显示方式，0-关闭，1-模式1（横向温度趋势线模式），2-模式2（纵向温度趋势线模式）
            public byte byFireImageModea;
            public byte byShowTempStripEnable;//显示温度条使能：0- 否，1- 是
            public float fEmissivity;//发射率(发射率 精确到小数点后两位)[0.01, 1.00](即：物体向外辐射能量的本领)
            public byte byDistanceUnit;//距离单位: 0-米（m），1-英尺（feet）。
            public byte byEnviroHumidity;//环境相对湿度，取值范围：0~100%
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            NET_DVR_TEMPERATURE_COLOR struTempColor;//测温报警颜色
            public uint iEnviroTemperature;//环境温度，取值范围：-273~10000摄氏度
            public uint iCorrectionVolume;//测温修正量，取值范围：-100~100
            /*bit0-中心点测温：0-不显示，1-显示；
            bit1-最高点测温：0-不显示，1-显示；
            bit2-最低点测温：0-不显示，1-显示；
            */
            public byte bySpecialPointThermType;// 特殊测温点显示
            public byte byReflectiveEnabled;//反射温度使能：0- 否，1- 是
            public ushort wDistance;//距离(m)[0, 10000]
            public float fReflectiveTemperature;//反射温度 精确到小数后一位
            public float fAlert;//预警温度阈值，-100.0-1000.0度（精确到小数点后一位）
            public float fAlarm;//报警温度阈值，-100.0-1000.0度（精确到小数点后一位）
            public float fThermalOpticalTransmittance;// 光学透过率, 精确到小数点后3位，范围0.001-1.000，默认1.000
            public float fExternalOpticsWindowCorrection;//外部光学温度，默认值20℃，范围为-40.0~80.0℃，实际显示单位以界面显示为准

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_COND
        {
            public uint dwSize;//结构体大小
            public uint dwChannel;
            public ushort wPresetNo;//0-保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_THERMOMETRY_PRESETINFO_PARAM
        {
            public byte byEnabled;  //是否使能：0- 否，1- 是
            public byte byRuleID;//规则ID 0-表示无效，从1开始 （list内部判断数据有效性）
            public ushort wDistance;//距离(m)[0, 10000]
            public float fEmissivity;//发射率(发射率 精确到小数点后两位)[0.01, 1.00](即：物体向外辐射能量的本领)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public byte byReflectiveEnabled;//反射温度使能：0- 否，1- 是
            public float fReflectiveTemperature;//反射温度 精确到小数后一位
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//规则名称    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byRuleCalibType;//规则标定类型 0-点，1-框，2-线
            public NET_VCA_POINT struPoint;//点测温坐标（当规则标定类型为"点"的时候生效）
            public NET_VCA_POLYGON struRegion;//区域、线（当规则标定类型为"框"或者"线"的时候生效）
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_THERMOMETRY_PRESETINFO
        {
            public uint dwSize;//结构体大小
            public ushort wPresetNo;//0-保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_THERMOMETRY_REGION_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_PRESETINFO_PARAM[] struPresetInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential/*, CharSet = CharSet.Unicode*/)]
        public struct NET_DVR_THERMOMETRY_ALARMRULE_PARAM
        {
            public byte byEnable;
            public byte byRuleID;//规则ID
            public byte byRule;//报警温度比较方式 0-高温大于,1-高温小于,2-低温大于,3-低温小于,4-平均温大于,5-平均温小于,6-温差大于,7-温差小于    
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//规则名称
            public float fAlert;//预警温度
            public float fAlarm;//报警温度
            public float fThreshold;//门限温度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential/*, CharSet = CharSet.Unicode*/)]
        public struct NET_DVR_THERMOMETRY_ALARMRULE
        {
            public uint dwSize;//结构体大小
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = THERMOMETRY_ALARMRULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_ALARMRULE_PARAM[] struThermometryAlarmRuleParam;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //预置点温差报警规则配置结构体
        public struct NET_DVR_THERMOMETRY_DIFFCOMPARISON_PARAM
        {
            public byte byEnabled;
            public byte byRuleID;
            public byte byAlarmID1;
            public byte byAlarmID2;
            public byte byRule;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public float fTemperatureDiff;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        //温差报警配置
        public struct NET_DVR_THERMOMETRY_DIFFCOMPARISON
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = THERMOMETRY_ALARMRULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_DIFFCOMPARISON_PARAM[] struDiffComparison;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //测温布防时间配置
        public struct NET_DVR_EVENT_SCHEDULE
        {
            public uint dwSize;//结构体大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime; /*布防时间*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayAlarmTime; /*假日布防时间*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //测温联动配置条件结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_TRIGGER_COND
        {
            public uint dwSize;//结构体大小
            public uint dwChannel;
            public ushort wPresetNo;//0-保留
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //手动测温基本参数配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUALTHERM_BASICPARAM
        {
            public uint dwSize;
            public ushort wDistance;//距离(m)[0, 10000]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; //保留
            public float fEmissivity;//发射率(发射率 精确到小数点后两位)[0.01, 1.00](即：物体向外辐射能量的本领)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_POINT_THERMOMETRY
        {
            public float fPointTemperature;/*点测温当前温度, 当标定为0-点时生效。精确到小数点后一位(-40-1000),（浮点数+100）*10 */
            public NET_VCA_POINT struPoint;//点测温坐标（当规则标定类型为“点”的时候生效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_REGION_THERMOMETRY
        {
            public float fMaxTemperature;//最高温度,精确到小数点后一位(-40-1000),（浮点数+100）*10 */
            public float fMinTemperature;//最低温度,精确到小数点后一位(-40-1000),（浮点数+100）*10 */
            public float fAverageTemperature;//平均温度,精确到小数点后一位(-40-1000),（浮点数+100）*10 */
            public float fTemperatureDiff;//温差,精确到小数点后一位(-40-1000),（浮点数+100）*10 */
            public NET_VCA_POLYGON struRegion;//区域、线（当规则标定类型为“框”或者“线”的时候生效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUALTHERM_RULE
        {
            public byte byRuleID;//规则ID 0-表示无效，从1开始 （list内部判断数据有效性）
            public byte byEnable;//是否启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//规则名称
            public byte byRuleCalibType;//规则标定类型 0-点，1-框，2-线
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_SDK_POINT_THERMOMETRY struPointTherm;//点测温，当标定为0-点时生效
            public NET_SDK_REGION_THERMOMETRY struRegionTherm; //区域测温，当标定为1-框、2-线时生效。
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 手动测温
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUAL_THERMOMETRY
        {
            public uint dwSize;//结构体大小
            public uint dwChannel;//通道号
            public uint dwRelativeTime; // 相对时标（只读）
            public uint dwAbsTime;      // 绝对时标（只读）
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)
            public byte byDataType;//数据状态类型:0-检测中，1-开始，2-结束（只读）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_SDK_MANUALTHERM_RULE struRuleInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 手动获取规则温度信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRYRULE_TEMPERATURE_INFO
        {
            public float fMaxTemperature;//最高温，精确到小数点后一位
            public float fMinTemperature;//最低温，精确到小数点后一位
            public float fAverageTemperature;//平均温，精确到小数点后一位
            public NET_VCA_POINT struHighestPoint; //最高温度位置坐标
            public NET_VCA_POINT struLowestPoint; //最低温度位置坐标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //热成像智能规则
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMAL_INTELRULE_DISPLAY
        {
            public uint dwSize;//结构体大小
            /*
            fontSizeType:为字体大小倍率索引，播放库会根据该倍率以及预览窗口的宽度动态改变字体的大小。公式为：具体倍率值/8*(0.01*预览窗口宽度)
            倍率索引对应如下：
            0~8倍率（小）
            1~12倍率（标准）
            2~16倍率（大）
            3~20倍率（超大）
            4~24倍率（特大）
            */
            public byte byFontSizeType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_RULESLINE_CFG struNormalRulesLineCfg;//正常规则线相关属性参数
            public NET_DVR_RULESLINE_CFG struAlertRulesLineCfg;//预警规则线相关属性参数
            public NET_DVR_RULESLINE_CFG struAlarmRulesLineCfg;//报警规则线相关属性参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 640, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RULESLINE_CFG
        {
            public NET_DVR_RGB_COLOR struRGB;// RGB参数：R（红色），G（绿色），B（蓝色） 范围0-255
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RGB_COLOR
        {
            public byte byRed;        //RGB颜色三分量中的红色
            public byte byGreen;    //RGB颜色三分量中的绿色
            public byte byBlue;    //RGB颜色三分量中的蓝色
            public byte byRes;        //保留
        }

        //热成像相关算法库版本获取
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMAL_ALGINFO
        {
            public uint dwSize;//结构体大小
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_THERMOMETRYALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sThermometryAlgName;//测温算法库版本名称
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_SHIPSALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sShipsAlgName;//船只算法库版本名称
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_FIRESALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sFireAlgName;//火点检测算法库版本名称
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 768, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_BAREDATAOVERLAY_CFG
        {
            public uint dwSize;
            public byte byEnable;//使能
            public byte byIntervalTime;// 上传的时间间隔可配置：1 2 3 4 5.单位为秒，默认为3秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 258, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V41
        {
            public uint dwHandleType;/*处理方式,处理方式的"或"结果*/
            /*0x00: 无响应*/
            /*0x01: 监视器上警告*/
            /*0x02: 声音警告*/
            /*0x04: 上传中心*/
            /*0x08: 触发报警输出*/
            /*0x10: 触发JPRG抓图并上传Email*/
            /*0x20: 无线声光报警器联动*/
            /*0x40: 联动电子地图(目前只有PCNVR支持)*/
            /*0x200: 抓图并上传FTP*/
            public uint dwMaxRelAlarmOutChanNum; //触发的报警输出通道数（只读）最大支持数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelAlarmOut; //触发报警通道      
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           //保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PRESETCHAN_INFO
        {
            public uint dwEnablePresetChan;    /*启用预置点的通道*/
            public uint dwPresetPointNo;        /*调用预置点通道对应的预置点序号, 0xfffffff表示不调用预置点。*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISECHAN_INFO
        {
            public uint dwEnableCruiseChan;    /*启用巡航的通道*/
            public uint dwCruiseNo;        /*巡航通道对应的巡航编号, 0xfffffff表示无效*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZTRACKCHAN_INFO
        {
            public uint dwEnablePtzTrackChan;    /*启用云台轨迹的通道*/
            public uint dwPtzTrackNo;        /*云台轨迹通道对应的编号, 0xfffffff表示无效*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EVENT_TRIGGER
        {
            public uint dwSize;
            public NET_DVR_HANDLEEXCEPTION_V41 struHandleException;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PRESETCHAN_INFO[] struPresetChanInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CRUISECHAN_INFO[] struCruiseChanInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PTZTRACKCHAN_INFO[] struPtzTrackInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //温差报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_DIFF_ALARM
        {
            public uint dwSize;
            public uint dwChannel;//通道号
            public byte byAlarmID1;//规则AlarmID1
            public byte byAlarmID2;//规则AlarmID2
            public ushort wPresetNo; //预置点号
            public byte byAlarmLevel;//0-预警 1-报警
            public byte byAlarmType;/*报警类型 0-最高温度 1-最低温度 2-平均温度*/
            public byte byAlarmRule;//0-大于，1-小于
            public byte byRuleCalibType;//规则标定类型 0-点，1-框，2线
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPoint;//点测温坐标（当规则标定类型为点的时候生效）数组下标0代表着AlarmID1，数组下标1代表着AlarmID2.
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POLYGON[] struRegion;//区域（当规则标定类型为框的时候生效）数组下标0代表着AlarmID1，数组下标1代表着AlarmID2.
            public float fRuleTemperatureDiff;/*配置规则温差,精确到小数点后一位(-40-1000),（浮点数+100）*/
            public float fCurTemperatureDiff;/*当前温差,精确到小数点后一位(-40-1000),（浮点数+100） */
            public NET_PTZ_INFO struPtzInfo;//ptz坐标信息
            public uint dwPicLen;//可见光图片长度
            public uint dwThermalPicLen;//热成像图片长度
            public uint dwThermalInfoLen;//热成像附加信息长度
            public IntPtr pPicBuff; ///可见光图片指针
            public IntPtr pThermalPicBuff;// 热成像图片指针
            public IntPtr pThermalInfoBuff; //热成像附加信息指针
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        //实时温度检测条件结构
        public struct NET_DVR_REALTIME_THERMOMETRY_COND
        {
            public uint dwSize;
            public uint dwChan;
            public byte byRuleID; //规则ID 0-代表获取全部规则，具体规则ID从1开始 
            /*
            1-定时模式：设备每隔一秒上传各个规则测温数据的最高温、最低温和平均温度值、温差
            2-温差模式：若上一秒与下一秒的最高温或者最低温或者平均温或者温差值的温差大于等于2摄氏度，则上传最高温、最低温和平均温度值。若大于等于一个小时温差值均小于2摄氏度，则上传最高温、最低温、平均温和温差值
            */
            public byte byMode; //长连接模式， 0-保留（为兼容老设备），1-定时模式，2-温差模式

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
        }

        public struct NET_DVR_POINT_THERM_CFG
        {
            public float fTemperature;//当前温度
            public NET_VCA_POINT struPoint;//点测温坐标（当规则标定类型为点的时候生效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
        }

        public struct NET_DVR_LINEPOLYGON_THERM_CFG
        {
            public float fMaxTemperature;//最高温
            public float fMinTemperature;//最低温
            public float fAverageTemperature;//平均温
            public float fTemperatureDiff;//温差
            public NET_VCA_POLYGON struRegion;//区域（当规则标定类型为框/线的时候生效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
        }

        public struct NET_DVR_THERMOMETRY_UPLOAD
        {
            public uint dwSize;
            public uint dwRelativeTime;     // 相对时标
            public uint dwAbsTime;            // 绝对时标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public char[] szRuleName;//规则名称
            public byte byRuleID;//规则ID号
            public byte byRuleCalibType;//规则标定类型 0-点，1-框，2-线
            public ushort wPresetNo; //预置点号
            public NET_DVR_POINT_THERM_CFG struPointThermCfg;
            public NET_DVR_LINEPOLYGON_THERM_CFG struLinePolygonThermCfg;
            public byte byThermometryUnit;//测温单位: 0-摄氏度（℃），1-华氏度（℉），2-开尔文(K)
            public byte byDataType;//数据状态类型:0-检测中，1-开始，2-结束
            public byte byRes1;
            public byte bySpecialPointThermType;
            public Single fCenterPointTemperature;
            public Single fHighestPointTemperature;
            public Single fLowestPointTemperature;
            public NET_VCA_POINT struHighestPoint;//线、框测温最高温度位置坐标（当规则标定类型为线、框的时候生效）
            public NET_VCA_POINT struLowestPoint;//线、框测温最低温度位置坐标（当规则标定类型为线、框的时候生效）

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留

            public void Init()
            {
                struPointThermCfg = new HCNetSDK.NET_DVR_POINT_THERM_CFG();
                struLinePolygonThermCfg = new HCNetSDK.NET_DVR_LINEPOLYGON_THERM_CFG();
            }
        }
        // 相对时标:一般涉及到时区，比如传递的东八区时间、GMT时间。
        // 绝对时标：即UTC时间，不带时区，比如1970年1月1日0时0分0秒到现在的秒数，相机现在一般传递的绝对时标都是这个。
        //  
        // 比如一个当前相机东八区的time_t值1400252410 
        // 相对时标的话应该是：2014-05-16 23:00:10
        // 绝对时标的话应该是：2014-05-16 15:00:10

        public const int NET_DVR_GET_FIGURE = 6640;

        public struct NET_DVR_GET_FIGURE_COND
        {
            public uint dwLength;         //结构长度
            public uint dwChannel;         //通道
            public NET_DVR_TIME_V30 struTimePoint;    //时间点
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_FIGURE_INFO
        {
            public uint dwPicLen;     //图片长度
            public IntPtr pPicBuf;     //图片数据
        }

        public struct NET_DVR_PTZ_MANUALTRACE
        {
            public uint dwSize;
            public uint dwChannel;      // 相对时标
            public NET_VCA_POINT struPoint; //定位坐标(启示坐标)
            public byte byTrackType;    //跟踪类型（手动跟踪默认是0） 0、非自动取证(普通取证) 1、高速道路跟踪 2、城市道路跟踪（手动跟踪取证）3、静态取证
            public byte byLinkageType;//联动动作: 0-手动跟踪 1-联动不跟踪
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
            public NET_VCA_POINT struPointEnd;  //定位坐标(启示坐标)
            public NET_DVR_TIME_V30 struTime;   //手动跟踪定位，当前时间
            public uint dwSerialNo;         //序号;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; //保留
        }

        public struct NET_DVR_SMARTTRACKCFG
        {
            public uint dwSize;
            public byte byEnable;//启动使能 0-否，1-是
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public uint dwDuration;//持续时间：0--300秒，默认300秒
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
        }

        //获取单个通道属性数据
        public const int NET_DVR_GET_SINGLE_CHANNELINFO = 4360;

        //人脸侦测配置
        public const int NET_DVR_GET_FACE_DETECT = 3352;
        public const int NET_DVR_SET_FACE_DETECT = 3353;

        public struct NET_DVR_CHANNEL_GROUP
        {
            public uint dwSize;
            public uint dwChannel; //通道号
            public uint dwGroup;  //组号 
            public byte byID;//设备区域设置ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public uint dwPositionNo;//场景位置索引号,IPC是0，IPD是从1开始
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }

        public struct NET_DVR_HANDLEEXCEPTION_V40
        {
            public uint dwHandleType;//异常处理,异常处理方式的"或"结果   
            /*0x00: 无响应*/
            /*0x01: 监视器上警告*/
            /*0x02: 声音警告*/
            /*0x04: 上传中心*/
            /*0x08: 触发报警输出*/
            /*0x10: 触发JPRG抓图并上传Email*/
            /*0x20: 无线声光报警器联动*/
            /*0x40: 联动电子地图(目前只有PCNVR支持)*/
            /*0x80: 报警触发录像(目前只有PCNVR支持) */
            /*0x100: 报警触发云台预置点 (目前只有PCNVR支持)*/
            /*0x200: 抓图并上传FTP*/
            /*0x400: 虚交侦测 联动 聚焦模式（提供可配置项，原先设备自动完成）IPC5.1.0*/
            /*0x800: PTZ联动跟踪(球机跟踪目标)*/
            /*0x1000:抓图上传到云*/
            public uint dwMaxRelAlarmOutChanNum;//触发的报警输出通道数（只读）最大支持数量
            public uint dwRelAlarmOutChanNum;//触发的报警输出通道数 实际支持数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelAlarmOut;//触发报警通道
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;//保留
        }

        public struct NET_DVR_DETECT_FACE
        {
            public uint dwSize;
            public byte byEnableDetectFace;//是否启用 0～不启用， 1～启用
            public byte byDetectSensitive;//灵敏度 10个等级 1～10
            public byte byEnableDisplay;/*启用移动侦测高亮显示，0-否，1-是*/
            public byte byRes;
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType;  /*处理方式*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;  /*布防时间*/
            public uint dwMaxRelRecordChanNum;//报警触发的录象通道 数（只读）最大支持数量
            public uint dwRelRecordChanNum;//报警触发的录象通道数 实际支持数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//报警触发的录象通道
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;  /*假日布防时间*/
            public ushort wDuration;//报警持续时间 5s 能力集
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes1;//保留
        }

        public const int MAX_FACE_PIC_NUM = 30;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_DETECTION
        {
            public uint dwSize;
            public uint dwRelativeTime; //相对时标
            public uint dwAbsTime; //绝对时标
            public uint dwBackgroundPicLen; //背景图的长度，为0表示没有图片，大于0表示有图片
            public NET_VCA_DEV_INFO struDevInfo;   //前端设备信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_FACE_PIC_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_RECT[] struFacePic;   //人脸子图区域
            public byte byFacePicNum;//子图数量
            public byte byRes1;
            public ushort wDevInfoIvmsChannelEx;//与NET_VCA_DEV_INFO里的byIvmsChannel含义相同，能表示更大的值。老客户端用byIvmsChannel能继续兼容，但是最大到255。新客户端版本请使用wDevInfoIvmsChannelEx。
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 252, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public IntPtr pBackgroundPicpBuffer;//背景图的图片数据
        }



        public const int NET_DVR_GET_PDC_RESULT = 5089;//客流量数据查询 
        public const int NET_DVR_REMOVE_FLASHSTORAGE = 3756;    //客流数据清除操作
        public const int NET_DVR_GET_PDC_RULECFG_V42 = 3405;  //设置人流量统计规则(扩展)
        public const int NET_DVR_SET_PDC_RULECFG_V42 = 3406;  //获取人流量统计规则(扩展)


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_QUERY_COND
        {
            public uint dwSize;
            public uint dwChannel; //通道号
            public NET_DVR_TIME_EX struStartTime; //开始时间
            public NET_DVR_TIME_EX struEndTime; //结束时间
            public byte byReportType; //0-无效值，1-日报表，2-周报表，3-月报表，4-年报表
            public byte byEnableProgramStatistics; //是否按节目统计，0-否，1-是
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPlayScheduleNo; //按节目统计时关联的日程号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PROGRAM_INFO
        {
            public uint dwProgramNo; //节目编号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sProgramName; //节目名称
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_RESULT
        {
            public uint dwSize;
            public NET_DVR_TIME_EX struStartTime;/*开始时间*/
            public NET_DVR_TIME_EX struEndTime;/*结束时间*/
            public uint dwEnterNum;   //进入人数
            public uint dwLeaveNum;  //离开人数
            public NET_DVR_PROGRAM_INFO struProgramInfo;        //节目信息
            public uint dwPeoplePassing;        //经过人数
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 200, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_ALRAM_INFO
        {
            public uint dwSize;          // PDC人流量报警上传结构体大小
            public byte byMode;          // 0 单帧统计结果 1最小时间段统计结果  
            public byte byChannel;       // 报警上传通道号
            /********* IPC5.1.7 新增参数 Begin 2014-03-21***********/
            public byte bySmart;         //专业智能返回0，Smart 返回 1
            public byte byRes1;          // 保留字节    
            /********* IPC5.1.7 新增参数 End 2014-03-21***********/
            public NET_VCA_DEV_INFO struDevInfo;             //前端设备信息
            public NET_MODE_PARAM uStatModeParam;
            public uint dwLeaveNum;        // 离开人数
            public uint dwEnterNum;        // 进入人数
            public byte byBrokenNetHttp;     //断网续传标志位，0-不是重传数据，1-重传数据
            public byte byRes3;
            public short wDevInfoIvmsChannelEx;     //与NET_VCA_DEV_INFO里的byIvmsChannel含义相同，能表示更大的值。老客户端用byIvmsChannel能继续兼容，但是最大到255。新客户端版本请使用wDevInfoIvmsChannelEx
            public uint dwPassingNum;        // 经过人数（进入区域后徘徊没有触发进入、离开的人数）
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        }

        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_MODE_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.U1)]
            [FieldOffsetAttribute(0)]
            public byte[] byLen;//参数
            [FieldOffsetAttribute(0)]
            public NET_MODE_FRAME struStatFrame;
            [FieldOffsetAttribute(0)]
            public NET_MODE_START_TIME struStatTime;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_FRAME
        {
            public uint dwRelativeTime;     // 相对时标
            public uint dwAbsTime;          // 绝对时标
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_START_TIME
        {
            public NET_DVR_TIME tmStart; // 统计起始时间 
            public NET_DVR_TIME tmEnd;  //  统计结束时间 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        }

        public struct NET_DVR_FLASHSTORAGE_REMOVE
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byPDCRemoveEnable;//清除客流数据使能 0-不清除，1-清除
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节;
        }

        public struct NET_DVR_AI_ALGORITHM_MODEL
        {
            public uint dwSize;
            public uint dwDescribeLength;
            public IntPtr pDescribeBuffer;//清除客流数据使能 0-不清除，1-清除
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节;
        }

        public struct NET_DVR_STD_CONTROL
        {
            public IntPtr lpCondBuffer;    //[in]条件参数(码字格式),例如通道号等.可以为NULL
            public uint dwCondSize;        //[in] dwCondSize指向的内存大小
            public IntPtr lpStatusBuffer;    //[out]返回的状态参数(XML格式),获取成功时不会赋值,如果不需要,可以置NULL
            public uint dwStatusSize;    //[in] lpStatusBuffer指向的内存大小
            public IntPtr lpXmlBuffer;    //[in/out]byDataType = 1时有效,xml格式数据
            public uint dwXmlSize;      //[in/out]lpXmlBuffer指向的内存大小,获取时同时作为输入和输出参数，获取成功后会修改会实际长度，设置时表示实际长度，而不是整个内存大小
            public byte byDataType;     //[in]输入/输出参数类型,0-使用结构体类型lpCondBuffer有效,1-使用XML类型lpXmlBuffer有效
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 55, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        };

        public const int NET_DVR_GET_TRAFFIC_CAP = 6630;
        public const int UPLOAD_VEHICLE_BLOCKALLOWLIST_FILE = 13; 
        public const int UPLOAD_THERMOMETRIC_FILE = 22; // 上传测温标定文件
        public const int UPLOAD_AI_ALGORITHM_MODEL = 52; // AI开放平台，主动上传算法模型到设备
        public const int UPLOAD_FD_DATA = 35;//导入人脸数据到人脸库
        public const int UPLOAD_FACE_DATA = 36;//导入人脸图片数据到人脸库
        public const int IMPORT_DATA_TO_FACELIB = 39;//导入人脸数据（人脸图片+图片附件信息 到设备人脸库）

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_UploadFile_V40(int lUserID, uint dwUploadType, IntPtr lpInBuffer, uint dwInBufferSize, string sFileName, IntPtr lpOutBuffer, uint dwOutBufferSize);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUploadState(int lUploadHandle, IntPtr pProgress);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StopUploadFile(int lFileHandle);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetUploadResult(int lUploadHandle, IntPtr lpOutBuffer, uint dwOutBufferSize);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadClose(int lUploadHandle);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_UploadSend(int lUploadHandle, IntPtr pstruSendParamIN, IntPtr lpOutBuffer);

        public const int NET_SDK_DOWNLOAD_VEHICLE_BLOCKALLOWLIST_FILE = 8;
        public const int NET_SDK_DOWNLOAD_THERMOMETRIC_FILE = 15; // 下载测温标定文件


        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartDownload(int lUserID, uint dwDownloadType, IntPtr lpInBuffer, uint dwInBufferSize, string sFileName);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadState(int lFileHandle, IntPtr pProgress);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadStateInfo(int lFileHandle, IntPtr pStatusInfo);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDownload(int lFileHandle);

        public const int NET_DVR_GET_GUARDCFG = 3134;//获取车牌识别检测计划 
        public const int NET_DVR_SET_GUARDCFG = 3135;//设置车牌识别检测计划

        //抓拍触发模式支持关联布防时间段和上传中心条件
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_COND
        {
            public uint dwSize;
            public uint dwChannel; //通道号
            /*
            0表示无效， 
            1表示关联 抓拍VIA模式（视频触发抓拍 IPC使用）;
            2 表示关联 抓拍 HVT 模式 （混卡IPC使用）
            */
            public byte byRelateType;
            public byte byGroupNo;  //组号
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
            public byte[] byRes;
        };

        public struct NET_DVR_TIME_DETECTION
        {
            public NET_DVR_SCHEDTIME struSchedTime; //时间
            public byte byDetSceneID;//检测场景号[1,4],IPC默认是0
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
            public byte[] byRes;
        };

        //抓拍触发模式支持关联布防时间段和上传中心
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_CFG
        {
            public uint dwSize;
            //布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_TIME_DETECTION[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //异常处理方式
            public uint dwMaxRelRecordChanNum;  //报警触发的录象通道 数（只读）最大支持数量
            public uint dwRelRecordChanNum;     //本组内实际触发的录象通道数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;    //报警触发的录象通道(0xffff ffff表示后续无效）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            NET_DVR_TIME_DETECTION[] struHolidayTime; //假日布防时间 
            public byte byDirection;//触发方向：0-保留；1-全部；2-正向；3-反向
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 87, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        };

        public const int NET_DVR_GET_EVENT_TRIGGERS_CAPABILITIES = 3501;

        #region  读取初始化参数文件
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            //检查文件是否存在
            if (File.Exists(iniFilePath))
            {
                //                 StringBuilder temp = new StringBuilder(1024);
                //                 //读取配置文件中某个key的值
                //                 GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                //                 return temp.ToString();
                /*                ReadIniData("DataBase", "DB_SHARE_User", m_DB_SHARE_User, inifile);*/
                byte[] byteAr = new byte[1024];
                uint resultSize = GetPrivateProfileStringByByteArray(Section, Key, NoText, byteAr, (uint)byteAr.Length, iniFilePath);
                string strall = Encoding.Unicode.GetString(byteAr, 0, (int)resultSize * 2);
                return strall;
            }
            else
            {
                //不存在则返回空
                return String.Empty;
            }
        }
        #endregion


        //流量统计方向结构体
        public struct NET_DVR_PDC_ENTER_DIRECTION
        {
            public NET_VCA_POINT struStartPoint; //流量统计方向起始点
            public NET_VCA_POINT struEndPoint;    // 流量统计方向结束点 
        }

        public struct NET_DVR_PDC_RULE_CFG_V41
        {
            public uint dwSize;              //结构大小
            public byte byEnable;             // 是否激活规则;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;    //保留字节
            public NET_VCA_POLYGON struPolygon;            // 多边形
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection;    // 流量进入方向
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_TIME_EX struDayStartTime; //白天开始时间，时分秒有效
            public NET_DVR_TIME_EX struNightStartTime; //夜晚开始时间，时分秒有效
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节
        }

        public struct NET_DVR_PDC_RULE_COND
        {
            public uint dwSize; //结构大小
            public uint dwChannel; //通道号
            public uint dwID; //场景ID，兼容球机多场景概念，兼容老版本SDK配置新设备时，保留字节为0的情况，所以这个字节为0时，也默认为场景1 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节 
        }

        //线结构
        public struct NET_VCA_LINE
        {
            public NET_VCA_POINT struStart;    //起点 
            public NET_VCA_POINT struEnd;      //终点
        }


        //配置结构
        public struct NET_DVR_PDC_RULE_CFG_V42
        {
            public uint dwSize; //结构大小
            public byte byEnable; //是否激活规则;
            public byte byOSDEnable;//客流统计OSD显示是否启用,  0-否（0-无），1-是（0-进入和离开）， 2-进入， 3-离开   对于老设备的不启用OSD叠加对应（无），启用对应（进入/离开）。
            public byte byCurDetectType;//当前检测区域类型，0-多边形，1-检测线
            public byte byInterferenceSuppression; //干扰抑制，按位表示,0-未勾选,1-勾选，bit0-阴影，bit1-徘徊，bit2-推车
            NET_VCA_POINT struOSDPoint;//客流统计显示OSD显示左上角坐标
            //客流量检测数据上传周期（0-15、1-1、2-5、3-10、4-20、5-30、6-60）单位：分钟
            public byte byDataUploadCycle;
            //每秒上传机制使能（0-关闭，1-开启）
            public byte bySECUploadEnable;
            public byte byEmailDayReport;//客流日报表使能 0-关闭，1-开启
            public byte byEmailWeekReport;//客流周报表使能 0-关闭，1-开启
            public byte byEmailMonthReport;//客流月报表使能0-关闭，1-开启
            public byte byEmailYearReport;//客流年报表使能0-关闭，1-开启
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;    //保留字节 
            public NET_VCA_POLYGON struPolygon; // 多边形
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection; // 流量进入方向
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//布防时间
            public NET_DVR_TIME_EX struDayStartTime; //白天开始时间，时分秒有效
            public NET_DVR_TIME_EX struNightStartTime; //夜晚开始时间，时分秒有效
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType; /*处理方式 仅支持上传中心*/
            public byte byDetecteSensitivity;//目标检测灵敏度：范围1-100，默认50
            public byte byGenerateSpeedSpace;//目标生成速度（空域）：范围1-100，默认50
            public byte byGenerateSpeedTime;// 目标生成速度（时域）：范围1-100，默认50
            public byte byCountSpeed;// 计数速度：范围1-100，默认50
            public byte byDetecteType;// 目标检测类型：0-自动，1-人头，2-头肩，默认0-自动，自动模式下DSP调整算法的参数配置给算法库
            public byte byTargetSizeCorrect;//目标尺寸修正：范围1-100，默认50
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes3;    //保留字节 
            public NET_VCA_LINE struLine;//检测线
            public byte byHeightFilterEnable;//高度过滤是否开启，0-关闭，1-开启
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes4;    //保留字节 
            float fHeightFilter;//过滤高度，单位：厘米，默认值：120厘米，范围：40-200厘米。byHeightFilterEnable为1是才有效
            public byte byCalibrateType;//标定类型，0-未进行标定，1-自动标定，2-手动标定
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes6;    //保留字节 
            float fTiltAngle;//俯仰角,单位：度；俯仰角默认值：0；俯仰角范围：0-180度,只读
            float fHeelAngle;//倾斜角,单位：度；倾斜角默认值：0；倾斜角范围：-90-90度,只读
            float fHeight;//高度，高度单位：厘米；高度默认值300厘米：高度范围：200-500厘米,当byCalibrateType为2时设置有效，其余时只读
            NET_VCA_POLYGON struCountPolygon;//计数区域,只读区域
            NET_VCA_POLYGON struAutoCalibPolygon;//标定区域，当byCalibrateType为1时有效
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //保留字节 
        }

        public const int NET_DVR_SET_POS_INFO_OVERLAY = 3913;//设置Pos信息码流叠加控制
        public const int NET_DVR_GET_POS_INFO_OVERLAY = 3914;//获取Pos信息码流叠加控制
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_POS_INFO_OVERLAY
        {
            public uint dwSize; //结构大小
            public byte byPosInfoOverlayEnable;// Pos信息码流叠加控制，0-不叠加，1-叠加
            public byte byOverlayType;//0-叠加进入和离开，1-叠加进入、离开、PASS、ID、高度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;    //保留字节 
        }
        //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]

        //单帧统计结果时使用
        [StructLayoutAttribute(LayoutKind.Sequential)]
        // [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct UNION_STATFRAME
        {
            public uint dwRelativeTime;     // 相对时标
            public uint dwAbsTime;          // 绝对时标
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct UNION_STATTIME
        {
            public NET_DVR_TIME tmStart; // 统计起始时间 
            public NET_DVR_TIME tmEnd;  //  统计结束时间 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //身份证信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ID_CARD_INFO
        {
            public uint dwSize;        //结构长度
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName;   //姓名
            public NET_DVR_DATE struBirth; //出生日期
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAddr;  //住址
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIDNum;   //身份证号码
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_ISSUING_AUTHORITY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIssuingAuthority;  //签发机关
            public NET_DVR_DATE struStartDate;  //有效开始日期
            public NET_DVR_DATE struEndDate;  //有效截止日期
            public byte byTermOfValidity;  //是否长期有效， 0-否，1-是（有效截止日期无效）
            public byte bySex;  //性别，1-男，2-女
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 101, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //身份证信息报警
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ID_CARD_INFO_ALARM
        {
            public uint dwSize;        //结构长度
            public NET_DVR_ID_CARD_INFO struIDCardCfg;//身份证信息
            public uint dwMajor; //报警主类型，参考宏定义
            public uint dwMinor; //报警次类型，参考宏定义
            public NET_DVR_TIME_V30 struSwipeTime; //时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetUser;//网络操作的用户名
            public NET_DVR_IPADDR struRemoteHostAddr;//远程主机地址
            public uint dwCardReaderNo; //读卡器编号，为0无效
            public uint dwDoorNo; //门编号，为0无效
            public uint dwPicDataLen;   //图片数据大小，不为0是表示后面带数据
            public IntPtr pPicData;
            public byte byCardType; 
            public byte byDeviceNo;                             // 设备编号，为0时无效（有效范围1-255）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwFingerPrintDataLen;                  // 指纹数据大小，不为0是表示后面带数据
            public IntPtr pFingerPrintData;
            public uint dwCapturePicDataLen;                   // 抓拍图片数据大小，不为0是表示后面带数据
            public IntPtr pCapturePicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 188, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        //警戒事件参数
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_VCA_EVENT_UNION
        {
            [FieldOffsetAttribute(0)]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            public uint[] uLen;//参数

            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TRAVERSE_PLANE struTraversePlane;//穿越警戒面参数 
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_AREA struArea;//进入/离开区域参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_INTRUSION struIntrusion;//入侵参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LOITER struLoiter;//徘徊参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TAKE_LEFT struTakeTeft;//丢包/捡包参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_PARKING struParking;//停车参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_RUN struRun;//奔跑参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_HIGH_DENSITY struHighDensity;//人员聚集参数  
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_VIOLENT_MOTION struViolentMotion;    //剧烈运动
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_REACH_HIGHT struReachHight;      //攀高
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_GET_UP struGetUp;           //起床
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LEFT struLeft;            //物品遗留
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TAKE struTake;            // 物品拿取
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_HUMAN_ENTER struHumanEnter;      //人员进入
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_OVER_TIME struOvertime;        //操作超时
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_STICK_UP struStickUp;//贴纸条
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_SCANNER struScanner;//读卡器参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LEAVE_POSITION struLeavePos;        //离岗参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TRAIL struTrail;           //尾随参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_FALL_DOWN struFallDown;        //倒地参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_AUDIO_ABNORMAL struAudioAbnormal;   //声强突变
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_ADV_REACH_HEIGHT struReachHeight;     //折线攀高参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TOILET_TARRY struToiletTarry;     //如厕超时参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_YARD_TARRY struYardTarry;       //放风场滞留参数
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_ADV_TRAVERSE_PLANE struAdvTraversePlane;//折线警戒面参数            
        }
        //简化的规则信息, 包含规则的基本信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_INFO
        {
            public byte byRuleID;//规则ID,0-7
            public byte byRes;//保留
            public ushort wEventTypeEx;   //行为事件类型扩展，用于代替字段dwEventType，参考VCA_RULE_EVENT_TYPE_EX
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//规则名称
            public VCA_EVENT_TYPE dwEventType;//警戒事件类型
            public NET_VCA_EVENT_UNION uEventParam;//事件参数
        }

        //行为分析结果上报结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_ALARM
        {
            public uint dwSize;//结构长度
            public uint dwRelativeTime;//相对时标
            public uint dwAbsTime;//绝对时标
            public NET_VCA_RULE_INFO struRuleInfo;//事件规则信息
            public NET_VCA_TARGET_INFO struTargetInfo;//报警目标信息
            public NET_VCA_DEV_INFO struDevInfo;//前端设备信息
            public uint dwPicDataLen;//返回图片的长度 为0表示没有图片，大于0表示该结构后面紧跟图片数据*/
            public byte byPicType;        //  0-普通图片 1-对比图片
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;        // 保留字节
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//保留，设置为0
            public IntPtr pImage;//指向图片的指针
        }

        //行为分析规则DSP信息叠加结构
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;//编码是否叠加目标
            public byte byDspAddRule;//编码是否叠加规则
            public byte byDspPicAddTarget;//抓图是否叠加目标
            public byte byDspPicAddRule;//抓图是否叠加规则
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PARK_VEHICLE
        {
            public uint dwSize;
            public byte byGroupNum;
            public byte byPicNo;
            public byte byLocationNum;
            public byte byParkError;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_PARKNO_LEN)]
            public string byParkingNo;
            public byte byLocationStatus;
            public byte bylogicalLaneNum;
            public ushort wUpLoadType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwChanIndex;
            public NET_DVR_PLATE_INFO struPlateInfo;
            public NET_DVR_VEHICLE_INFO struVehicleInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSiteID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDeviceID;
            public uint dwPicNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_PICTURE_INFO[] struPicInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DIAGNOSIS_UPLOAD
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = STREAM_ID_LEN)]
            public string sStreamID;    ///< 流ID，长度小于32个字节
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sMonitorIP;  ///< 监控点ip
            public uint dwChanIndex;  ///< 监控点通道号  
            public uint dwWidth;  ///< 图像宽度
            public uint dwHeight;  ///< 图像高度
            public NET_DVR_TIME struCheckTime;  ///< 检测时间(合并日期和时间字段)，格式：2012-08-06 13:00:00
            public byte byResult;  ///0-未检测 1-正常 2-异常 3-登录失败 4-取流异常
            public byte bySignalResult; ///< 视频丢失检测结果 0-未检测 1-正常 2-异常
            public byte byBlurResult;  ///< 图像模糊检测结果，0-未检测 1-正常 2-异常
            public byte byLumaResult;  ///< 图像过亮检测结果，0-未检测 1-正常 2-异常
            public byte byChromaResult;  ///< 偏色检测结果，0-未检测 1-正常 2-异常
            public byte bySnowResult;  ///< 噪声干扰检测结果，0-未检测 1-正常 2-异常
            public byte byStreakResult;  ///< 条纹干扰检测结果，0-未检测 1-正常 2-异常
            public byte byFreezeResult;  ///< 画面冻结检测结果，0-未检测 1-正常 2-异常
            public byte byPTZResult;  ///< 云台检测结果，0-未检测 1-正常 2-异常
            public byte byContrastResult;     //对比度异常检测结果，0-未检测，1-正常，2-异常
            public byte byMonoResult;         //黑白图像检测结果，0-未检测，1-正常，2-异常
            public byte byShakeResult;        //视频抖动检测结果，0-未检测，1-正常，2-异常
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string sSNapShotURL;    
            public byte byFlashResult;        //视频剧变检测结果，0-未检测，1-正常，2-异常
            public byte byCoverResult;        //视频遮挡检测结果，0-未检测，1-正常，2-异常
            public byte bySceneResult;        //场景变更检测结果，0-未检测，1-正常，2-异常
            public byte byDarkResult;         //图像过暗检测结果，0-未检测，1-正常，2-异常
            public byte byStreamType;        //码流类型，0-无效，1-未知，2-国标类型，3-非国标类型
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 59, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //人脸抓拍信息
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_INFO_ALARM
        {
            public uint dwRelativeTime;     // 相对时标
            public uint dwAbsTime;            // 绝对时标
            public uint dwSnapFacePicID;       //抓拍人脸图ID
            public uint dwSnapFacePicLen;        //抓拍人脸子图的长度，为0表示没有图片，大于0表示有图片
            public NET_VCA_DEV_INFO struDevInfo;   //前端设备信息
            public byte byFaceScore;        //人脸评分，指人脸子图的质量的评分,0-100
            public byte bySex;//性别，0-未知，1-男，2-女
            public byte byGlasses;//是否带眼镜，0-未知，1-是，2-否
            /*
             * 识别人脸的年龄段范围[byAge-byAgeDeviation,byAge+byAgeDeviation]
             */
            public byte byAge;//年龄
            public byte byAgeDeviation;//年龄误差值
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;              // 保留字节
            public uint dwUIDLen; // 上传报警的标识长度
            public IntPtr pUIDBuffer;  //标识指针
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;              // 保留字节
            public IntPtr pBuffer1;  //抓拍人脸子图的图片数据
        }
        //籍贯参数结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AREAINFOCFG
        {
            public ushort wNationalityID;//国籍
            public ushort wProvinceID;//省
            public ushort wCityID;//市
            public ushort wCountyID;//县
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
        }
        //人员信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_ATTRIBUTE
        {
            public byte bySex;//性别：0-男，1-女
            public byte byCertificateType;//证件类型：0-身份证，1-警官证
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HUMAN_BIRTHDATE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBirthDate;//出生年月，如：201106
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName; //姓名
            public NET_DVR_AREAINFOCFG struNativePlace;//籍贯参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCertificateNumber; //证件号
            public uint dwPersonInfoExtendLen;// 人员标签信息扩展长度
            public IntPtr pPersonInfoExtend;  //人员标签信息扩展信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_BLOCKLIST_INFO
        {
            public uint dwSize;//结构大小
            public uint dwRegisterID;//名单注册ID号（只读）
            public uint dwGroupNo;//分组号
            public byte byType;
            public byte byLevel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//保留
            public NET_VCA_HUMAN_ATTRIBUTE struAttribute;//人员信息
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemark;//备注信息
            public uint dwFDDescriptionLen;//人脸库描述数据长度
            public IntPtr pFDDescriptionBuffer;//人脸库描述数据指针
            public uint dwFCAdditionInfoLen;//抓拍库附加信息长度
            public IntPtr pFCAdditionInfoBuffer;//抓拍库附加信息数据指针（FCAdditionInfo中包含相机PTZ坐标）
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//保留
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_BLOCKLIST_INFO_ALARM
        {
            public NET_VCA_BLOCKLIST_INFO struBlockListInfo;
            public uint dwBlockListPicLen;       
            public uint dwFDIDLen;// 人脸库ID长度
            public IntPtr pFDID;  //人脸库Id指针
            public uint dwPIDLen;// 人脸库图片ID长度
            public IntPtr pPID;  //人脸库图片ID指针
            public ushort wThresholdValue; //人脸库阈值[0,100]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//保留
            public IntPtr pBuffer1;//指向图片的指针
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_MATCH_ALARM
        {
            public uint dwSize;             // 结构大小
            public float fSimilarity; //相似度，[0.001,1]
            public NET_VCA_FACESNAP_INFO_ALARM struSnapInfo; //抓拍信息
            public NET_VCA_BLOCKLIST_INFO_ALARM struBlockListInfo; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sStorageIP;        //存储服务IP地址
            public ushort wStoragePort;            //存储服务端口号
            public byte byMatchPicNum; //匹配图片的数量，0-保留（老设备这个值默认0，新设备这个值为0时表示后续没有匹配的图片信息）
            public byte byPicTransType;//图片数据传输方式: 0-二进制；1-url
            public uint dwSnapPicLen;//设备识别抓拍图片长度
            public IntPtr pSnapPicBuffer;//设备识别抓拍图片指针
            public NET_VCA_RECT struRegion;//目标边界框，设备识别抓拍图片中，人脸子图坐标
            public uint dwModelDataLen;//建模数据长度
            public IntPtr pModelDataBuffer;// 建模数据指针
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  // 保留字节
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CID_ALARM
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CID_CODE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCIDCode;    //CID事件号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCIDDescribe;    //CID事件名
            public NET_DVR_TIME_EX struTriggerTime;            //触发报警的时间点
            public NET_DVR_TIME_EX struUploadTime;                //上传报警的时间点
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACCOUNTNUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCenterAccount;    //中心帐号
            public byte byReportType;                    //见定义NET_DVR_ALARMHOST_REPORT_TYPE
            public byte byUserType;                        //用户类型，0-网络用户 1-键盘用户,2-手机用户,3-系统用户
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;        //网络用户用户名
            public ushort wKeyUserNo;                        //键盘用户号    0xFFFF表示无效
            public byte byKeypadNo;                        //键盘号        0xFF表示无效
            public byte bySubSysNo;                        //子系统号        0xFF表示无效
            public ushort wDefenceNo;                        //防区号        0xFFFF表示无效
            public byte byVideoChanNo;                    //视频通道号    0xFF表示无效
            public byte byDiskNo;                        //硬盘号        0xFF表示无效
            public ushort wModuleAddr;                    //模块地址        0xFFFF表示无效
            public byte byCenterType;                    //0-无效, 1-中心账号(长度6),2-扩展的中心账号(长度9)
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACCOUNTNUM_LEN_32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCenterAccountV40;    //中心账号V40,使用此字段时sCenterAccount无效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //行为分析事件类型扩展
        public enum VCA_RULE_EVENT_TYPE_EX : ushort
        {
            ENUM_VCA_EVENT_TRAVERSE_PLANE = 1,   //穿越警戒面
            ENUM_VCA_EVENT_ENTER_AREA = 2,   //目标进入区域,支持区域规则
            ENUM_VCA_EVENT_EXIT_AREA = 3,   //目标离开区域,支持区域规则
            ENUM_VCA_EVENT_INTRUSION = 4,   //周界入侵,支持区域规则
            ENUM_VCA_EVENT_LOITER = 5,   //徘徊,支持区域规则
            ENUM_VCA_EVENT_LEFT_TAKE = 6,   //物品遗留拿取,支持区域规则
            ENUM_VCA_EVENT_PARKING = 7,   //停车,支持区域规则
            ENUM_VCA_EVENT_RUN = 8,   //快速移动,支持区域规则
            ENUM_VCA_EVENT_HIGH_DENSITY = 9,   //区域内人员聚集,支持区域规则
            ENUM_VCA_EVENT_VIOLENT_MOTION = 10,  //剧烈运动检测
            ENUM_VCA_EVENT_REACH_HIGHT = 11,  //攀高检测
            ENUM_VCA_EVENT_GET_UP = 12,  //起身检测
            ENUM_VCA_EVENT_LEFT = 13,  //物品遗留
            ENUM_VCA_EVENT_TAKE = 14,  //物品拿取
            ENUM_VCA_EVENT_LEAVE_POSITION = 15,  //离岗
            ENUM_VCA_EVENT_TRAIL = 16,  //尾随
            ENUM_VCA_EVENT_KEY_PERSON_GET_UP = 17,  //重点人员起身检测
            ENUM_VCA_EVENT_FALL_DOWN = 20,  //倒地检测
            ENUM_VCA_EVENT_AUDIO_ABNORMAL = 21,  //声强突变检测
            ENUM_VCA_EVENT_ADV_REACH_HEIGHT = 22,  //折线攀高
            ENUM_VCA_EVENT_TOILET_TARRY = 23,  //如厕超时
            ENUM_VCA_EVENT_YARD_TARRY = 24,  //放风场滞留
            ENUM_VCA_EVENT_ADV_TRAVERSE_PLANE = 25,  //折线警戒面
            ENUM_VCA_EVENT_HUMAN_ENTER = 29,  //人靠近ATM,只在ATM_PANEL模式下支持   
            ENUM_VCA_EVENT_OVER_TIME = 30,  //操作超时,只在ATM_PANEL模式下支持
            ENUM_VCA_EVENT_STICK_UP = 31,  //贴纸条,支持区域规则
            ENUM_VCA_EVENT_INSTALL_SCANNER = 32   //安装读卡器,支持区域规则
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TRAVERSE_PLANE
        {
            public NET_VCA_LINE struPlaneBottom;//警戒面底边
            public VCA_CROSS_DIRECTION dwCrossDirection;//穿越方向: 0-双向，1-从左到右，2-从右到左
            public byte bySensitivity;//灵敏度参数，范围[1,5]
            public byte byPlaneHeight;//警戒面高度
            /*
            检测目标，可支持多选，具体定义为：
            0~所有目标（表示不锁定检测目标，所有目标都将进行检测）
            0x01 ~ 人，
            0x02 ~ 车，
            */
            public byte byDetectionTarget;
            public byte byPriority;//优先级,0~低,1~中,2~高
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new NET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }
        //进入/离开区域参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_AREA
        {
            public NET_VCA_POLYGON struRegion;//区域范围
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //智能侦测区域配置条件参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SMART_REGION_COND
        {
            public uint dwSize;
            public uint dwChannel;//通道号
            public uint dwRegion;//区域ID号
        }

        //进入区域侦测配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGION_ENTRANCE_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//是否使能: 0-否 , 1-是
            public byte byEnableHumanMisinfoFilter;//启用人体去误报   0-不启用 , 1-启用
            public byte byEnableVehicleMisinfoFilter;//启用车辆去误报   0-不启用 , 1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_REGIONENTRANCE_REGION[] struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //进入区域侦测参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGIONENTRANCE_REGION
        {
            public NET_VCA_POLYGON struRegion;//区域范围
            public byte bySensitivity;//灵敏度参数，范围[1-100]
            /*
           检测目标，可支持多选，具体定义为：
           0~所有目标（表示不锁定检测目标，所有目标都将进行检测）
           0x01 ~ 人，
           0x02 ~ 车，
           */
            public byte byDetectionTarget;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //离开区域侦测配置
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGION_EXITING_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//是否使能: 0-否, 1-是
            public byte byEnableHumanMisinfoFilter;//启用人体去误报   0-不启用, 1-启用
            public byte byEnableVehicleMisinfoFilter;//启用车辆去误报   0-不启用, 1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_REGIONEXITING_REGION[] struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //离开区域侦测参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGIONEXITING_REGION
        {
            public NET_VCA_POLYGON struRegion;//区域范围
            public byte bySensitivity;//灵敏度参数，范围[1-100]
            /*
           检测目标，可支持多选，具体定义为：
           0~所有目标（表示不锁定检测目标，所有目标都将进行检测）
           0x01 ~ 人，
           0x02 ~ 车，
           */
            public byte byDetectionTarget;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //越界侦测配置参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TRAVERSE_PLANE_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//是否使能: 0-否 , 1-是
            public byte byEnableDualVca;// 启用支持智能后检索 0-不启用，1-启用
            public byte byEnableHumanMisinfoFilter;// 启用人体去误报 0-不启用，1-启用
            public byte byEnableVehicleMisinfoFilter;// 启用车辆去误报 0-不启用，1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_TRAVERSE_PLANE[] struAlertParam;//警戒线参数
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //异常处理方式
            public uint dwMaxRelRecordChanNum;  //报警触发的录象通道 数（只读）最大支持数量
            public uint dwRelRecordChanNum;     //报警触发的录象通道 数 实际支持的数量
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//触发录像的通道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;//假日布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //区域入侵侦测配置参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FIELDDETECION
        {
            public uint dwSize;
            public byte byEnabled;//是否使能: 0-否 , 1-是
            public byte byEnableDualVca;// 启用支持智能后检索 0-不启用，1-启用
            public byte byEnableHumanMisinfoFilter;// 启用人体去误报 0-不启用，1-启用
            public byte byEnableVehicleMisinfoFilter;// 启用车辆去误报 0-不启用，1-启用
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_INTRUSION[] struIntrusion;//每个区域的参数设置
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //异常处理方式
            public uint dwMaxRelRecordChanNum;  //报警触发的录象通道 数（只读）最大支持数量
            public uint dwRelRecordChanNum;     //报警触发的录象通道 数 实际支持的数量
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//触发录像的通道号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;//假日布防时间
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }


        //根据报警延迟时间来标识报警中带图片，报警间隔和IO报警一致，1秒发送一个。
        //入侵参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_INTRUSION
        {
            public NET_VCA_POLYGON struRegion;//区域范围
            public ushort wDuration;//报警延迟时间: 1-120秒，建议5秒，判断是有效报警的时间
            public byte bySensitivity;        //灵敏度参数，范围[1-100]
            public byte byRate;               //占比：区域内所有未报警目标尺寸目标占区域面积的比重，归一化为－；
            /*
            检测目标，可支持多选，具体定义为：
            0~所有目标（表示不锁定检测目标，所有目标都将进行检测）
            0x01 ~ 人，
            0x02 ~ 车，
            */
            public byte byDetectionTarget;
            public byte byPriority;//优先级, 0-低, 1-中, 2-高
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //尺寸过滤器
        public struct NET_VCA_SIZE_FILTER
        {
            public byte byActive;            //是否激活尺寸过滤器 0-否 非0-是
            public byte byMode;         //过滤器模式SIZE_FILTER_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;        //保留，置0
            public NET_VCA_RECT struMiniRect;    //最小目标框,全0表示不设置
            public NET_VCA_RECT struMaxRect;      //最大目标框,全0表示不设置
        }

        // 人脸抓拍规则(单条)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_SINGLE_FACESNAPCFG
        {
            public byte byActive;                //是否激活规则：0-否，1-是
            //人脸自动ROI开关使能
            public byte byAutoROIEnable;//0-关闭,1-开启
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //保留
            public NET_VCA_SIZE_FILTER struSizeFilter;   //尺寸过滤器
            public NET_VCA_POLYGON struVcaPolygon;        //人脸识别区域
        }

        // 人脸抓拍配置参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAPCFG
        {
            public UInt32 dwSize;
            public byte bySnapTime;                    //单个目标人脸的抓拍次数0-10
            public byte bySnapInterval;                 //抓拍间隔，单位：帧
            public byte bySnapThreshold;               //抓拍阈值，0-100
            public byte byGenerateRate;         //目标生成速度,范围[1, 5]    
            public byte bySensitive;            //目标检测灵敏度，范围[1, 5]
            public byte byReferenceBright; //2012-3-27参考亮度[0,100]
            public byte byMatchType;         //2012-5-3比对报警模式，0-目标消失后报警，1-实时报警
            public byte byMatchThreshold;  //2012-5-3实时比对阈值，0~100
            public NET_DVR_JPEGPARA struPictureParam; //图片规格结构
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_SINGLE_FACESNAPCFG[] struRule; //人脸抓拍规则
            //人脸曝光最短持续时间（两个字节）
            public ushort wFaceExposureMinDuration;//范围(1~3600秒，默认60)生效于自动模式下
            //人脸曝光模式
            public byte byFaceExposureMode;//1-关闭，2-开启，0-自动（根据人脸判断）
            public byte byBackgroundPic;//背景图上传使能 0-默认值（开启），1-禁止
            public UInt32 dwValidFaceTime;    //有效人脸最短持续时间，单位：秒
            public UInt32 dwUploadInterval; //人脸抓拍统计数据上传间隔时间，单位：秒，默认900秒
            public UInt32 dwFaceFilteringTime;//人脸停留时间过滤,默认5秒，范围0-100秒。0秒表示不过滤
            public byte bySceneID;     //场景号,目前支持1~4场景，0为无效
            public byte byInvalCapFilterEnable;//无效抓拍过滤使能，0为关闭，1为开启，默认为0
            public byte byInvalCapFilterThreshold;//无效抓拍过滤阈值，0~100，当byInvalCapFilterEnable为1时生效
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 81, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int NET_SDK_MAX_FDID_LEN = 256;
        public const int NET_SDK_MAX_INDENTITY_KEY_LEN = 64;//密码长度
        public const int MAX_UPLOADFILE_URL_LEN = 240;//密码长度

        //导入人脸数据条件
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACELIB_COND
        {
            public UInt32 dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_FDID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szFDID;//人脸库ID
            public byte byConcurrent;//设备并发处理 0-不开启，1-开始
            public byte byCover;//是否覆盖式导入 0-否，1-是
            public byte byCustomFaceLibID;//FDID是否是自定义，0-不是，1-是；
            /*当”/ISAPI/Intelligent/channels/<ID>/faceContrast/capabilities”能力中返回isSupportNoSaveUploadPicture能力节点时，
            代表非并发处理模式下，支持不保存上传原图的操作:当上传成功图片并设备建模成功后，会将上传的原图进行删除。
            注：该操作无法与并发处理同时进行。*/
            public byte byPictureSaveMode;//上传原图保存模式，0-保存，1-不保存;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_INDENTITY_KEY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIdentityKey;//交互操作口令
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 发送数据结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SEND_PARAM_IN
        {
            public IntPtr pSendData;             //发送的缓冲区,PicURL == 1 的时候，内存中存储的是 URL 字符串,byUploadModeling == 1 的时候，内存中存储的是 建模base64加密数据
            public UInt32 dwSendDataLen;         //发送数据长度,PicURL == 1 的时候，表示的 URL 字符串的长度,byUploadModeling == 1 的时候，表示为建模数据base64后的加密长度
            public NET_DVR_TIME_V30 struTime;   //图片时间
            public byte byPicType;              //图片格式,1-jpg,2-bmp,3-png,4-SWF,5-GIF
            public byte byPicURL;               //图片数据采用URL方式 0-二进制图片数据，1-图片数据走URL方式
            /*是否上传建模数据；
            0-  二进制图片数据方式(pSendData指向二进制图片数据, dwPicDataLen为图片二进制数据长度)，
            1-  直接上传建模数据(pSendData指向建模base64加密数据, dwPicDataLen为建模数据base64后的加密长度)。
            注：建模数据采用base64加密方式,选择为建模数据上传后，byPicURL 无需。
            当”/ISAPI/Intelligent/channels/<ID>/faceContrast/capabilities”能力中返回isSupportUploadModeling能力节点时，支持上传建模数据. */
            public byte byUploadModeling;
            public byte byRes1;
            public UInt32 dwPicMangeNo;           //图片管理号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPicName;     //图片名称
            public UInt32 dwPicDisplayTime;       //图片播放时长，单位秒
            public IntPtr pSendAppendData;       //发送图片的附加信息缓冲区，对应FaceAppendData 的XML描述；
            public UInt32 dwSendAppendDataLen;    //发送图片的附加信息数据长度  FaceAppendData  XML的长度；
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // 文件上传结果信息结构体
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_UPLOAD_FILE_RET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_UPLOADFILE_URL_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUrl;   //url
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 260, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        } 

        #region 操作提示
        public struct LOCAL_LOG_INFO
        {
            public int iLogType;
            public string strTime;
            public string strLogInfo;
            public string strDevInfo;
            public string strErrInfo;
        }
        
        #endregion

    }

}
