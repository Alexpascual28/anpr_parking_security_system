using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace ANPR
{
    /// <summary>
    /// CHCNetSDK ��ժҪ˵����
    /// </summary>
    public class HCNetSDK
    {

        public HCNetSDK()
        {
            //
            // TODO: �ڴ˴����ӹ��캯���߼�
            //
        }
        #region ��������
        public static string ProtocolType;

        #endregion

        /************************************��Ƶ�ۺ�ƽ̨(begin)***************************************/
        public const int MAX_SUBSYSTEM_NUM = 80;   //һ������ϵͳ�������ϵͳ����
        public const int MAX_SERIALLEN = 36;  //������кų���

        public const int MAX_LOOPPLANNUM = 16;  //���ƻ��л���
        public const int DECODE_TIMESEGMENT = 4;     //�ƻ�����ÿ��ʱ�����

        public const int MAX_DOMAIN_NAME = 64;  /* ����������� */
        public const int MAX_DISKNUM_V30 = 33; //9000�豸���Ӳ����/* ���33��Ӳ��(����16������SATAӲ�̡�1��eSATAӲ�̺�16��NFS��) */
        public const int MAX_DAYS = 7;       //ÿ������

        public const uint DEVICE_ABILITY_INFO = 0x011;     //�豸�¼�������V2.0��


        public const int MAX_ID_NUM_LEN = 32;  //�������֤�ų���
        public const int MAX_ID_NAME_LEN = 128;   //�����������
        public const int MAX_ID_ADDR_LEN = 280;   //���סַ����
        public const int MAX_ID_ISSUING_AUTHORITY_LEN = 128; //���ǩ�����س���

        public const int MAX_ID_LEN = 48;
        public const int MAX_PARKNO_LEN = 16;
        public const int MAX_HUMAN_BIRTHDATE_LEN = 10;
        public const int CID_CODE_LEN = 4;
        public const int ACCOUNTNUM_LEN = 6;
        public const int ACCOUNTNUM_LEN_32 = 32;

        public const int MAX_CATEGORY_LEN = 8; //���Ƹ�����Ϣ����ַ�

        public const int MAX_LEN_XML = 10 * 1024 * 1024; //xml��󳤶�

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct NET_DVR_SUBSYSTEMINFO
        {
            public byte bySubSystemType;//��ϵͳ���ͣ�1-��������ϵͳ��2-��������ϵͳ��0-NULL���˲���ֻ�ܻ�ȡ��
            public byte byChan;//��ϵͳͨ�������˲���ֻ�ܻ�ȡ��
            public byte byLoginType;//ע�����ͣ�1-ֱ����2-DNS��3-������
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struSubSystemIP;/*IP��ַ�����޸ģ�*/
            public ushort wSubSystemPort;//��ϵͳ�˿ںţ����޸ģ�
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;

            public NET_DVR_IPADDR struSubSystemIPMask;//��������
            public NET_DVR_IPADDR struGatewayIpAddr;    /* ���ص�ַ*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� ���˲���ֻ�ܻ�ȡ��*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*���루�˲���ֻ�ܻ�ȡ��*/

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDomainName;//����(���޸�)
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDnsAddress;/*DNS������IP��ַ*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sSerialNumber;//���кţ��˲���ֻ�ܻ�ȡ��
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
            public uint dwPoolTime; /*��ѯ�������λ����*/
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
            public byte byAlarmMode;//�����������ͣ�1-��ѯ��2-���� 
            public ushort wLoopTime;//��ѯʱ��, ��λ���� 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESYSTEMINFO
        {
            public byte bySerialNum;//��ϵͳ���
            public byte byChan;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERINFO
        {
            public uint dwSize;
            public NET_DVR_IPADDR struIP;/*�����IP��ַ*/
            public ushort wPort;//������˿ں�
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;/*���� */
            public byte byChan;//�����485��
            public byte by485Port;//485�ڵ�ַ
            public ushort wBaudRate;// ������(bps)��0��50��1��75��2��110��3��150��4��300��5��600��6��1200��7��2400��8��4800��9��9600��10��19200�� 11��38400��12��57600��13��76800��14��115.2k;
            public byte byDataBit;//�����м�λ 0��5λ��1��6λ��2��7λ��3��8λ;
            public byte byStopBit;// ֹͣλ��0��1λ��1��2λ;
            public byte byParity;// У�飺0����У�飻1����У�飻2��żУ��;
            public byte byFlowControl;// 0���ޣ�1�������أ�2-Ӳ����
            public ushort wDecoderType;// ����������, ��0��ʼ�����ȡ�Ľ�����Э���б��е��±����Ӧ
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERCFG
        {
            public uint dwSize;
            public NET_DVR_CODESYSTEMINFO struCodeSubsystemInfo;//������ϵͳ��Ӧ��Ϣ
            public NET_DVR_CODESPLITTERINFO struCodeSplitterInfo;//�������Ϣ
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ASSOCIATECFG
        {
            public byte byAssociateType;//�������ͣ�1-����
            public ushort wAlarmDelay;//������ʱ��0��5�룻1��10�룻2��30�룻3��1���ӣ�4��2���ӣ�5��5���ӣ�6��10���ӣ�
            public byte byAlarmNum;//�����ţ������ֵ��Ӧ�ø�����ͬ�ı�������ͬ��ֵ
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DYNAMICDECODE
        {
            public uint dwSize;
            public NET_DVR_ASSOCIATECFG struAssociateCfg;//������̬��������ṹ
            public NET_DVR_PU_STREAM_CFG struPuStreamCfg;//��̬����ṹ
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DECODESCHED
        {
            public NET_DVR_SCHEDTIME struSchedTime;
            public byte byDecodeType;/*0-�ޣ�1-��ѯ���룬2-��̬����*/
            public byte byLoopGroup;//��ѯ���
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PU_STREAM_CFG struDynamicDec;//��̬����
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PLANDECODE
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_DAYS * DECODE_TIMESEGMENT, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_DECODESCHED[] struDecodeSched;//��һ��Ϊ��ʼ����9000һ��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byres;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOPLATFORM_ABILITY
        {
            public uint dwSize;
            public byte byCodeSubSystemNums;//������ϵͳ����
            public byte byDecodeSubSystemNums;//������ϵͳ����
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byWindowMode; /*��ʾͨ��֧�ֵĴ���ģʽ*/
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int VIDEOPLATFORM_ABILITY = 0x210; //��Ƶ�ۺ�ƽ̨������
        /************************************��Ƶ�ۺ�ƽ̨(end)***************************************/

        //SDK����
        public const int SDK_PLAYMPEG4 = 1;//���ſ�
        public const int SDK_HCNETSDK = 2;//�����

        //���ݿ����NVR��Ϣ
        //���ݿ����
        public const int INSERTTYPE = 0;        //��������
        public const int MODIFYTYPE = 1;        //�����޸�
        public const int DELETETYPE = 2;        //����ɾ��
        /****************************************��־����******************************************/
        //��������
        public const int DEF_OPE_PREVIEW = 1;   //Ԥ��
        public const int DEF_OPE_TALK = 2;  //�Խ�
        public const int DEF_OPE_SETALARM = 3;   //����
        public const int DEF_OPE_PTZCTRL = 4;   //��̨����
        public const int DEF_OPE_VIDEOPARAM = 5;   //��Ƶ��������
        public const int DEF_OPE_PLAYBACK = 6;   //�ط�
        public const int DEF_OPE_REMOTECFG = 7;   //Զ������
        public const int DEF_OPE_GETSERVSTATE = 8;   //��ȡ�豸״̬
        public const int DEF_OPE_CHECKTIME = 9;   //Уʱ



        //������־����    
        public const int DEF_OPE_PRE_STARTPREVIEW = 1;   //��ʼԤ��
        public const int DEF_OPE_PRE_STOPPREVIEW = 2;   //ֹͣԤ��
        public const int DEF_OPE_PRE_STRATCYCPLAY = 3;   //��ʼѭ������
        public const int DEF_OPE_PRE_STOPCYCPLAY = 4;   //ֹͣѭ������
        public const int DEF_OPE_PRE_STARTRECORD = 5;   //��ʼ¼��
        public const int DEF_OPE_PRE_STOPRECORD = 6;   //ֹͣ¼��
        public const int DEF_OPE_PRE_CAPTURE = 7;   //ץͼ
        public const int DEF_OPE_PRE_OPENSOUND = 8;   //������
        public const int DEF_OPE_PRE_CLOSESOUND = 9;   //�ر�����

        //�Խ�
        public const int DEF_OPE_TALK_STARTTALK = 1;   //��ʼ�Խ�
        public const int DEF_OPE_TALK_STOPTALK = 2;   //ֹͣ�Խ�

        public const int DEF_OPE_ALARM_SETALARM = 1;   //����
        public const int DEF_OPE_ALARM_WITHDRAWALARM = 2;   //����

        public const int DEF_OPE_PTZ_PTZCTRL = 1;   //��̨����

        public const int DEF_OPE_VIDEOPARAM_SET = 1;   //��Ƶ����

        //�ط�
        public const int DEF_OPE_PLAYBACK_LOCALSEARCH = 1;   //���ػطŲ�ѯ�ļ�
        public const int DEF_OPE_PLAYBACK_LOCALPLAY = 2;   //���ػط��ļ�
        public const int DEF_OPE_PLAYBACK_LOCALDOWNLOAD = 3;   //���ػط������ļ�
        public const int DEF_OPE_PLAYBACK_REMOTESEARCH = 4;   //Զ�̻طŲ�ѯ�ļ�
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYFILE = 5;   //Զ�̰��ļ��ط�
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYTIME = 6;   //Զ�̰�ʱ��ط�
        public const int DEF_OPE_PLAYBACK_REMOTEDOWNLOAD = 7;   //Զ�̻ط������ļ�

        public const int DEF_OPE_REMOTE_REMOTECFG = 1;   //Զ�̲�������

        public const int DEF_OPE_STATE_GETSERVSTATE = 1;//��ȡ�豸״̬

        public const int DEF_OPE_CHECKT_CHECKTIME = 1;//Уʱ

        //��������
        public const int DEF_ALARM_IO = 1;   //�ź�������
        public const int DEF_ALARM_HARDFULL = 2;   //Ӳ��������
        public const int DEF_ALARM_VL = 3;  //��Ƶ�źŶ�ʧ����
        public const int DEF_ALARM_MV = 4;     //�ƶ���ⱨ��
        public const int DEF_ALARM_HARDFORMAT = 5;   //Ӳ��δ��ʽ������
        public const int DEF_ALARM_HARDERROR = 6;   //Ӳ�̴�����
        public const int DEF_ALARM_VH = 7;     //�ڵ�����
        public const int DEF_ALARM_NOPATCH = 8;   //��ʽ��ƥ�䱨��
        public const int DEF_ALARM_ERRORVISIT = 9;   //�Ƿ����ʱ���
        public const int DEF_ALARM_EXCEPTION = 10;  //Ѳ���쳣
        public const int DEF_ALARM_RECERROR = 11;  //Ѳ���쳣

        //ϵͳ��־����
        public const int DEF_SYS_LOGIN = 1;   //��½ 
        public const int DEF_SYS_LOGOUT = 2;   //ע��
        public const int DEF_SYS_LOCALCFG = 3;   //��������

        /****************************************��־����******************************************/


        public const int NAME_LEN = 32;//�û�������
        public const int PASSWD_LEN = 16;//���볤��
        public const int MAX_NAMELEN = 16;//DVR���ص�½��
        public const int MAX_RIGHT = 32;//�豸֧�ֵ�Ȩ�ޣ�1-12��ʾ����Ȩ�ޣ�13-32��ʾԶ��Ȩ�ޣ�
        public const int SERIALNO_LEN = 48;//���кų���
        public const int MACADDR_LEN = 6;//mac��ַ����
        public const int MAX_ETHERNET = 2;//�豸������̫����
        public const int PATHNAME_LEN = 128;//·������

        public const int MAX_TIMESEGMENT_V30 = 8;//9000�豸���ʱ�����
        public const int MAX_TIMESEGMENT = 4;//8000�豸���ʱ�����

        public const int MAX_SHELTERNUM = 4;//8000�豸����ڵ�������
        public const int PHONENUMBER_LEN = 32;//pppoe���ź�����󳤶�

        public const int MAX_DISKNUM = 16;//8000�豸���Ӳ����
        public const int MAX_DISKNUM_V10 = 8;//1.2�汾֮ǰ�汾

        public const int MAX_WINDOW_V30 = 32;//9000�豸������ʾ��󲥷Ŵ�����
        public const int MAX_WINDOW = 16;//8000�豸���Ӳ����
        public const int MAX_VGA_V30 = 4;//9000�豸���ɽ�VGA��
        public const int MAX_VGA = 1;//8000�豸���ɽ�VGA��

        public const int MAX_USERNUM_V30 = 32;//9000�豸����û���
        public const int MAX_USERNUM = 16;//8000�豸����û���
        public const int MAX_EXCEPTIONNUM_V30 = 32;//9000�豸����쳣������
        public const int MAX_EXCEPTIONNUM = 16;//8000�豸����쳣������
        public const int MAX_LINK = 6;//8000�豸��ͨ�������Ƶ��������

        public const int MAX_DECPOOLNUM = 4;//��·������ÿ������ͨ������ѭ��������
        public const int MAX_DECNUM = 4;//��·��������������ͨ������ʵ��ֻ��һ������������������
        public const int MAX_TRANSPARENTNUM = 2;//��·���������������͸��ͨ����
        public const int MAX_CYCLE_CHAN = 16; //��·�����������ѭͨ����
        public const int MAX_CYCLE_CHAN_V30 = 64;//�����ѯͨ��������չ��
        public const int MAX_DIRNAME_LENGTH = 80;//���Ŀ¼����
        public const int MAX_WINDOWS = 16;//��󴰿���

        public const int MAX_STRINGNUM_V30 = 8;//9000�豸���OSD�ַ�������
        public const int MAX_STRINGNUM = 4;//8000�豸���OSD�ַ�������
        public const int MAX_STRINGNUM_EX = 8;//8000������չ
        public const int MAX_AUXOUT_V30 = 16;//9000�豸����������
        public const int MAX_AUXOUT = 4;//8000�豸����������
        public const int MAX_HD_GROUP = 16;//9000�豸���Ӳ������
        public const int MAX_NFS_DISK = 8; //8000�豸���NFSӲ����

        public const int IW_ESSID_MAX_SIZE = 32;//WIFI��SSID�ų���
        public const int IW_ENCODING_TOKEN_MAX = 32;//WIFI��������ֽ���
        public const int MAX_SERIAL_NUM = 64;//���֧�ֵ�͸��ͨ��·��
        public const int MAX_DDNS_NUMS = 10;//9000�豸������ddns��
        public const int MAX_EMAIL_ADDR_LEN = 48;//���email��ַ����
        public const int MAX_EMAIL_PWD_LEN = 32;//���email���볤��

        public const int MAXPROGRESS = 100;//�ط�ʱ�����ٷ���
        public const int MAX_SERIALNUM = 2;//8000�豸֧�ֵĴ����� 1-232�� 2-485
        public const int CARDNUM_LEN = 20;//���ų���
        public const int MAX_VIDEOOUT_V30 = 4;//9000�豸����Ƶ�����
        public const int MAX_VIDEOOUT = 2;//8000�豸����Ƶ�����

        public const int MAX_PRESET_V30 = 256;// 9000�豸֧�ֵ���̨Ԥ�õ���
        public const int MAX_TRACK_V30 = 256;// 9000�豸֧�ֵ���̨�켣��
        public const int MAX_CRUISE_V30 = 256;// 9000�豸֧�ֵ���̨Ѳ����
        public const int MAX_PRESET = 128;// 8000�豸֧�ֵ���̨Ԥ�õ��� 
        public const int MAX_TRACK = 128;// 8000�豸֧�ֵ���̨�켣��
        public const int MAX_CRUISE = 128;// 8000�豸֧�ֵ���̨Ѳ���� 

        public const int CRUISE_MAX_PRESET_NUMS = 32;// һ��Ѳ������Ѳ���� 

        public const int MAX_SERIAL_PORT = 8;//9000�豸֧��232������
        public const int MAX_PREVIEW_MODE = 8;// �豸֧�����Ԥ��ģʽ��Ŀ 1����,4����,9����,16����.... 
        public const int MAX_MATRIXOUT = 16;// ���ģ������������ 
        public const int LOG_INFO_LEN = 11840; // ��־������Ϣ 
        public const int DESC_LEN = 16;// ��̨�����ַ������� 
        public const int PTZ_PROTOCOL_NUM = 200;// 9000���֧�ֵ���̨Э���� 

        public const int MAX_AUDIO = 1;//8000�����Խ�ͨ����
        public const int MAX_AUDIO_V30 = 2;//9000�����Խ�ͨ����
        public const int MAX_CHANNUM = 16;//8000�豸���ͨ����
        public const int MAX_ALARMIN = 16;//8000�豸��󱨾�������
        public const int MAX_ALARMOUT = 4;//8000�豸��󱨾������
        //9000 IPC����
        public const int MAX_ANALOG_CHANNUM = 32;//���32��ģ��ͨ��
        public const int MAX_ANALOG_ALARMOUT = 32; //���32·ģ�ⱨ����� 
        public const int MAX_ANALOG_ALARMIN = 32;//���32·ģ�ⱨ������

        public const int MAX_IP_DEVICE = 32;//������������IP�豸��
        public const int MAX_IP_CHANNEL = 32;//������������IPͨ����
        public const int MAX_IP_ALARMIN = 128;//�����������౨��������
        public const int MAX_IP_ALARMOUT = 64;//�����������౨�������

        //SDK_V31 ATM
        public const int MAX_ATM_NUM = 1;
        public const int MAX_ACTION_TYPE = 12;
        public const int ATM_FRAMETYPE_NUM = 4;
        public const int MAX_ATM_PROTOCOL_NUM = 1025;
        public const int ATM_PROTOCOL_SORT = 4;
        public const int ATM_DESC_LEN = 32;
        // SDK_V31 ATM

        /* ���֧�ֵ�ͨ���� ���ģ��������IP֧�� */
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160

        public const int MAX_INTERVAL_NUM = 4;

        //�������ӷ�ʽ
        public const int NORMALCONNECT = 1;
        public const int MEDIACONNECT = 2;

        //�豸�ͺ�(����)
        public const int HCDVR = 1;
        public const int MEDVR = 2;
        public const int PCDVR = 3;
        public const int HC_9000 = 4;
        public const int HF_I = 5;
        public const int PCNVR = 6;
        public const int HC_76NVR = 8;

        //NVR����
        public const int DS8000HC_NVR = 0;
        public const int DS9000HC_NVR = 1;
        public const int DS8000ME_NVR = 2;

        //���߲��±���
        public const int THERMOMETRY_ALARMRULE_NUM = 40;           //�ȳ��񱨾�������
        public const int MAX_THERMOMETRY_REGION_NUM = 40;         //�ȶ�ͼ����������֧����
        public const int MAX_THERMOMETRY_DIFFCOMPARISON_NUM = 40; //�ȳ����²��������
        public const int NET_SDK_MAX_THERMOMETRYALGNAME = 128; //�����㷨��汾��󳤶�
        public const int NET_SDK_MAX_SHIPSALGNAME = 128; //��ֻ�㷨��汾��󳤶�
        public const int NET_SDK_MAX_FIRESALGNAME = 128; //����㷨��汾��󳤶�

        public const int ISAPI_STATUS_LEN = 4096 * 4;
        public const int ISAPI_DATA_LEN = 4096 * 4;

        /*******************ȫ�ִ����� begin**********************/
        public const int NET_DVR_NOERROR = 0;//û�д���
        public const int NET_DVR_PASSWORD_ERROR = 1;//�û����������
        public const int NET_DVR_NOENOUGHPRI = 2;//Ȩ�޲���
        public const int NET_DVR_NOINIT = 3;//û�г�ʼ��
        public const int NET_DVR_CHANNEL_ERROR = 4;//ͨ���Ŵ���
        public const int NET_DVR_OVER_MAXLINK = 5;//���ӵ�DVR�Ŀͻ��˸����������
        public const int NET_DVR_VERSIONNOMATCH = 6;//�汾��ƥ��
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;//���ӷ�����ʧ��
        public const int NET_DVR_NETWORK_SEND_ERROR = 8;//�����������ʧ��
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;//�ӷ�������������ʧ��
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;//�ӷ������������ݳ�ʱ
        public const int NET_DVR_NETWORK_ERRORDATA = 11;//���͵���������
        public const int NET_DVR_ORDER_ERROR = 12;//���ô������
        public const int NET_DVR_OPERNOPERMIT = 13;//�޴�Ȩ��
        public const int NET_DVR_COMMANDTIMEOUT = 14;//DVR����ִ�г�ʱ
        public const int NET_DVR_ERRORSERIALPORT = 15;//���ںŴ���
        public const int NET_DVR_ERRORALARMPORT = 16;//�����˿ڴ���
        public const int NET_DVR_PARAMETER_ERROR = 17;//��������
        public const int NET_DVR_CHAN_EXCEPTION = 18;//������ͨ�����ڴ���״̬
        public const int NET_DVR_NODISK = 19;//û��Ӳ��
        public const int NET_DVR_ERRORDISKNUM = 20;//Ӳ�̺Ŵ���
        public const int NET_DVR_DISK_FULL = 21;//������Ӳ����
        public const int NET_DVR_DISK_ERROR = 22;//������Ӳ�̳���
        public const int NET_DVR_NOSUPPORT = 23;//��������֧��
        public const int NET_DVR_BUSY = 24;//������æ
        public const int NET_DVR_MODIFY_FAIL = 25;//�������޸Ĳ��ɹ�
        public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;//���������ʽ����ȷ
        public const int NET_DVR_DISK_FORMATING = 27;//Ӳ�����ڸ�ʽ����������������
        public const int NET_DVR_DVRNORESOURCE = 28;//DVR��Դ����
        public const int NET_DVR_DVROPRATEFAILED = 29;//DVR����ʧ��
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;//��PC����ʧ��
        public const int NET_DVR_DVRVOICEOPENED = 31;//�����������Խ���ռ��
        public const int NET_DVR_TIMEINPUTERROR = 32;//ʱ�����벻��ȷ
        public const int NET_DVR_NOSPECFILE = 33;//�ط�ʱ������û��ָ�����ļ�
        public const int NET_DVR_CREATEFILE_ERROR = 34;//�����ļ�����
        public const int NET_DVR_FILEOPENFAIL = 35;//���ļ�����
        public const int NET_DVR_OPERNOTFINISH = 36; //�ϴεĲ�����û�����
        public const int NET_DVR_GETPLAYTIMEFAIL = 37;//��ȡ��ǰ���ŵ�ʱ�����
        public const int NET_DVR_PLAYFAIL = 38;//���ų���
        public const int NET_DVR_FILEFORMAT_ERROR = 39;//�ļ���ʽ����ȷ
        public const int NET_DVR_DIR_ERROR = 40;//·������
        public const int NET_DVR_ALLOC_RESOURCE_ERROR = 41;//��Դ�������
        public const int NET_DVR_AUDIO_MODE_ERROR = 42;//����ģʽ����
        public const int NET_DVR_NOENOUGH_BUF = 43;//������̫С
        public const int NET_DVR_CREATESOCKET_ERROR = 44;//����SOCKET����
        public const int NET_DVR_SETSOCKET_ERROR = 45;//����SOCKET����
        public const int NET_DVR_MAX_NUM = 46;//�����ﵽ���
        public const int NET_DVR_USERNOTEXIST = 47;//�û�������
        public const int NET_DVR_WRITEFLASHERROR = 48;//дFLASH����
        public const int NET_DVR_UPGRADEFAIL = 49;//DVR����ʧ��
        public const int NET_DVR_CARDHAVEINIT = 50;//���뿨�Ѿ���ʼ����
        public const int NET_DVR_PLAYERFAILED = 51;//���ò��ſ���ĳ������ʧ��
        public const int NET_DVR_MAX_USERNUM = 52;//�豸���û����ﵽ���
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;//��ÿͻ��˵�IP��ַ��������ַʧ��
        public const int NET_DVR_NOENCODEING = 54;//��ͨ��û�б���
        public const int NET_DVR_IPMISMATCH = 55;//IP��ַ��ƥ��
        public const int NET_DVR_MACMISMATCH = 56;//MAC��ַ��ƥ��
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;//�����ļ����Բ�ƥ��
        public const int NET_DVR_MAX_PLAYERPORT = 58;//������·���ﵽ���
        public const int NET_DVR_NOSPACEBACKUP = 59;//�����豸��û���㹻�ռ���б���
        public const int NET_DVR_NODEVICEBACKUP = 60;//û���ҵ�ָ���ı����豸
        public const int NET_DVR_PICTURE_BITS_ERROR = 61;//ͼ����λ����������24ɫ
        public const int NET_DVR_PICTURE_DIMENSION_ERROR = 62;//ͼƬ��*�����ޣ� ��128*256
        public const int NET_DVR_PICTURE_SIZ_ERROR = 63;//ͼƬ��С���ޣ���100K
        public const int NET_DVR_LOADPLAYERSDKFAILED = 64;//���뵱ǰĿ¼��Player Sdk����
        public const int NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;//�Ҳ���Player Sdk��ĳ���������
        public const int NET_DVR_LOADDSSDKFAILED = 66;//���뵱ǰĿ¼��DSsdk����
        public const int NET_DVR_LOADDSSDKPROC_ERROR = 67;//�Ҳ���DsSdk��ĳ���������
        public const int NET_DVR_DSSDK_ERROR = 68;//����Ӳ�����DsSdk��ĳ������ʧ��
        public const int NET_DVR_VOICEMONOPOLIZE = 69;//��������ռ
        public const int NET_DVR_JOINMULTICASTFAILED = 70;//����ಥ��ʧ��
        public const int NET_DVR_CREATEDIR_ERROR = 71;//������־�ļ�Ŀ¼ʧ��
        public const int NET_DVR_BINDSOCKET_ERROR = 72;//���׽���ʧ��
        public const int NET_DVR_SOCKETCLOSE_ERROR = 73;//socket�����жϣ��˴���ͨ�������������жϻ�Ŀ�ĵز��ɴ�
        public const int NET_DVR_USERID_ISUSING = 74;//ע��ʱ�û�ID���ڽ���ĳ����
        public const int NET_DVR_SOCKETLISTEN_ERROR = 75;//����ʧ��
        public const int NET_DVR_PROGRAM_EXCEPTION = 76;//�����쳣
        public const int NET_DVR_WRITEFILE_FAILED = 77;//д�ļ�ʧ��
        public const int NET_DVR_FORMAT_READONLY = 78;//��ֹ��ʽ��ֻ��Ӳ��
        public const int NET_DVR_WITHSAMEUSERNAME = 79;//�û����ýṹ�д�����ͬ���û���
        public const int NET_DVR_DEVICETYPE_ERROR = 80;//�������ʱ�豸�ͺŲ�ƥ��
        public const int NET_DVR_LANGUAGE_ERROR = 81;//�������ʱ���Բ�ƥ��
        public const int NET_DVR_PARAVERSION_ERROR = 82;//�������ʱ�����汾��ƥ��
        public const int NET_DVR_IPCHAN_NOTALIVE = 83; //Ԥ��ʱ���IPͨ��������
        public const int NET_DVR_RTSP_SDK_ERROR = 84;//���ظ���IPCͨѶ��StreamTransClient.dllʧ��
        public const int NET_DVR_CONVERT_SDK_ERROR = 85;//����ת���ʧ��
        public const int NET_DVR_IPC_COUNT_OVERFLOW = 86;//��������ip����ͨ����

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
        /*******************ȫ�ִ����� end**********************/

        /*************************************************
        NET_DVR_IsSupport()����ֵ
        1��9λ�ֱ��ʾ������Ϣ��λ����TRUE)��ʾ֧�֣�
        **************************************************/
        public const int NET_DVR_SUPPORT_DDRAW = 1;//֧��DIRECTDRAW�������֧�֣��򲥷������ܹ���
        public const int NET_DVR_SUPPORT_BLT = 2;//�Կ�֧��BLT�����������֧�֣��򲥷������ܹ���
        public const int NET_DVR_SUPPORT_BLTFOURCC = 4;//�Կ�BLT֧����ɫת���������֧�֣���������������������RGBת��
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 8;//�Կ�BLT֧��X����С�������֧�֣�ϵͳ������������ת��
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 16;//�Կ�BLT֧��Y����С�������֧�֣�ϵͳ������������ת��
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 32;//�Կ�BLT֧��X��Ŵ������֧�֣�ϵͳ������������ת��
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 64;//�Կ�BLT֧��Y��Ŵ������֧�֣�ϵͳ������������ת��
        public const int NET_DVR_SUPPORT_SSE = 128;//CPU֧��SSEָ�Intel Pentium3����֧��SSEָ��
        public const int NET_DVR_SUPPORT_MMX = 256;//CPU֧��MMXָ���Intel Pentium3����֧��SSEָ��

        /**********************��̨�������� begin*************************/
        public const int LIGHT_PWRON = 2;// ��ͨ�ƹ��Դ
        public const int WIPER_PWRON = 3;// ��ͨ��ˢ���� 
        public const int FAN_PWRON = 4;// ��ͨ���ȿ���
        public const int HEATER_PWRON = 5;// ��ͨ����������
        public const int AUX_PWRON1 = 6;// ��ͨ�����豸����
        public const int AUX_PWRON2 = 7;// ��ͨ�����豸���� 
        public const int SET_PRESET = 8;// ����Ԥ�õ� 
        public const int CLE_PRESET = 9;// ���Ԥ�õ� 

        public const int ZOOM_IN = 11;// �������ٶ�SS���(���ʱ��)
        public const int ZOOM_OUT = 12;// �������ٶ�SS��С(���ʱ�С)
        public const int FOCUS_NEAR = 13;// �������ٶ�SSǰ�� 
        public const int FOCUS_FAR = 14;// �������ٶ�SS���
        public const int IRIS_OPEN = 15;// ��Ȧ���ٶ�SS����
        public const int IRIS_CLOSE = 16;// ��Ȧ���ٶ�SS��С 

        public const int TILT_UP = 21;/* ��̨��SS���ٶ����� */
        public const int TILT_DOWN = 22;/* ��̨��SS���ٶ��¸� */
        public const int PAN_LEFT = 23;/* ��̨��SS���ٶ���ת */
        public const int PAN_RIGHT = 24;/* ��̨��SS���ٶ���ת */
        public const int UP_LEFT = 25;/* ��̨��SS���ٶ���������ת */
        public const int UP_RIGHT = 26;/* ��̨��SS���ٶ���������ת */
        public const int DOWN_LEFT = 27;/* ��̨��SS���ٶ��¸�����ת */
        public const int DOWN_RIGHT = 28;/* ��̨��SS���ٶ��¸�����ת */
        public const int PAN_AUTO = 29;/* ��̨��SS���ٶ������Զ�ɨ�� */

        public const int FILL_PRE_SEQ = 30;/* ��Ԥ�õ����Ѳ������ */
        public const int SET_SEQ_DWELL = 31;/* ����Ѳ����ͣ��ʱ�� */
        public const int SET_SEQ_SPEED = 32;/* ����Ѳ���ٶ� */
        public const int CLE_PRE_SEQ = 33;/* ��Ԥ�õ��Ѳ��������ɾ�� */
        public const int STA_MEM_CRUISE = 34;/* ��ʼ��¼�켣 */
        public const int STO_MEM_CRUISE = 35;/* ֹͣ��¼�켣 */
        public const int RUN_CRUISE = 36;/* ��ʼ�켣 */
        public const int RUN_SEQ = 37;/* ��ʼѲ�� */
        public const int STOP_SEQ = 38;/* ֹͣѲ�� */
        public const int GOTO_PRESET = 39;/* ����ת��Ԥ�õ� */
        public const int DEL_SEQ = 43; /* ɾ��Ѳ��·�� */
        /**********************��̨�������� end*************************/
        /*************************************************
        �ط�ʱ���ſ�������궨�� 
        NET_DVR_PlayBackControl
        NET_DVR_PlayControlLocDisplay
        NET_DVR_DecPlayBackCtrl�ĺ궨��
        ����֧�ֲ鿴����˵���ʹ���
        **************************************************/
        public const int NET_DVR_PLAYSTART = 1;//��ʼ����
        public const int NET_DVR_PLAYSTOP = 2;//ֹͣ����
        public const int NET_DVR_PLAYPAUSE = 3;//��ͣ����
        public const int NET_DVR_PLAYRESTART = 4;//�ָ�����
        public const int NET_DVR_PLAYFAST = 5;//���
        public const int NET_DVR_PLAYSLOW = 6;//����
        public const int NET_DVR_PLAYNORMAL = 7;//�����ٶ�
        public const int NET_DVR_PLAYFRAME = 8;//��֡��
        public const int NET_DVR_PLAYSTARTAUDIO = 9;//������
        public const int NET_DVR_PLAYSTOPAUDIO = 10;//�ر�����
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;//��������
        public const int NET_DVR_PLAYSETPOS = 12;//�ı��ļ��طŵĽ���
        public const int NET_DVR_PLAYGETPOS = 13;//��ȡ�ļ��طŵĽ���
        public const int NET_DVR_PLAYGETTIME = 14;//��ȡ��ǰ�Ѿ����ŵ�ʱ��(���ļ��طŵ�ʱ����Ч)
        public const int NET_DVR_PLAYGETFRAME = 15;//��ȡ��ǰ�Ѿ����ŵ�֡��(���ļ��طŵ�ʱ����Ч)
        public const int NET_DVR_GETTOTALFRAMES = 16;//��ȡ��ǰ�����ļ��ܵ�֡��(���ļ��طŵ�ʱ����Ч)
        public const int NET_DVR_GETTOTALTIME = 17;//��ȡ��ǰ�����ļ��ܵ�ʱ��(���ļ��طŵ�ʱ����Ч)
        public const int NET_DVR_THROWBFRAME = 20;//��B֡
        public const int NET_DVR_SETSPEED = 24;//���������ٶ�
        public const int NET_DVR_KEEPALIVE = 25;//�������豸������(����ص�����������2�뷢��һ��)
        public const int NET_DVR_PLAYSETTIME = 26;//������ʱ�䶨λ
        public const int NET_DVR_PLAYGETTOTALLEN = 27;//��ȡ��ʱ��طŶ�Ӧʱ����ڵ������ļ����ܳ���
        public const int NET_DVR_PLAY_FORWARD = 29; //�����л�Ϊ����
        public const int NET_DVR_PLAY_REVERSE = 30; //�����л�Ϊ����
        public const int NET_DVR_SET_DECODEFFRAMETYPE = 31;
        public const int NET_DVR_SET_TRANS_TYPE = 32; //����ת���ʽ
        public const int NET_DVR_PLAY_CONVERT = 33; //�ط�ת��
        public const int NET_DVR_START_DRAWFRAME = 34; //��ʼ��֡�ط�
        public const int NET_DVR_STOP_DRAWFRAME = 35; //ֹͣ��֡�ط�

        //Զ�̰����������£�
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

        public const int KEY_PTZ_AP1_START = KEY_CODE_EDIT;/* ��Ȧ+ */
        public const int KEY_PTZ_AP1_STOP = 36;

        public const int KEY_PTZ_AP2_START = KEY_CODE_PAN;/* ��Ȧ- */
        public const int KEY_PTZ_AP2_STOP = 37;

        public const int KEY_PTZ_FOCUS1_START = KEY_CODE_A;/* �۽�+ */
        public const int KEY_PTZ_FOCUS1_STOP = 38;

        public const int KEY_PTZ_FOCUS2_START = KEY_CODE_M;/* �۽�- */
        public const int KEY_PTZ_FOCUS2_STOP = 39;

        public const int KEY_PTZ_B1_START = 40;/* �䱶+ */
        public const int KEY_PTZ_B1_STOP = 41;

        public const int KEY_PTZ_B2_START = 42;/* �䱶- */
        public const int KEY_PTZ_B2_STOP = 43;

        //9000����
        public const int KEY_CODE_11 = 44;
        public const int KEY_CODE_12 = 45;
        public const int KEY_CODE_13 = 46;
        public const int KEY_CODE_14 = 47;
        public const int KEY_CODE_15 = 48;
        public const int KEY_CODE_16 = 49;

        /*************************������������ begin*******************************/
        //����NET_DVR_SetDVRConfig��NET_DVR_GetDVRConfig,ע�����Ӧ�����ýṹ
        public const int NET_DVR_GET_DEVICECFG = 100;//��ȡ�豸����
        public const int NET_DVR_SET_DEVICECFG = 101;//�����豸����
        public const int NET_DVR_GET_NETCFG = 102;//��ȡ�������
        public const int NET_DVR_SET_NETCFG = 103;//�����������
        public const int NET_DVR_GET_PICCFG = 104;//��ȡͼ�����
        public const int NET_DVR_SET_PICCFG = 105;//����ͼ�����
        public const int NET_DVR_GET_COMPRESSCFG = 106;//��ȡѹ������
        public const int NET_DVR_SET_COMPRESSCFG = 107;//����ѹ������
        public const int NET_DVR_GET_RECORDCFG = 108;//��ȡ¼��ʱ�����
        public const int NET_DVR_SET_RECORDCFG = 109;//����¼��ʱ�����
        public const int NET_DVR_GET_DECODERCFG = 110;//��ȡ����������
        public const int NET_DVR_SET_DECODERCFG = 111;//���ý���������
        public const int NET_DVR_GET_RS232CFG = 112;//��ȡ232���ڲ���
        public const int NET_DVR_SET_RS232CFG = 113;//����232���ڲ���
        public const int NET_DVR_GET_ALARMINCFG = 114;//��ȡ�����������
        public const int NET_DVR_SET_ALARMINCFG = 115;//���ñ����������
        public const int NET_DVR_GET_ALARMOUTCFG = 116;//��ȡ�����������
        public const int NET_DVR_SET_ALARMOUTCFG = 117;//���ñ����������
        public const int NET_DVR_GET_TIMECFG = 118;//��ȡDVRʱ��
        public const int NET_DVR_SET_TIMECFG = 119;//����DVRʱ��
        public const int NET_DVR_GET_PREVIEWCFG = 120;//��ȡԤ������
        public const int NET_DVR_SET_PREVIEWCFG = 121;//����Ԥ������
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;//��ȡ��Ƶ�������
        public const int NET_DVR_SET_VIDEOOUTCFG = 123;//������Ƶ�������
        public const int NET_DVR_GET_USERCFG = 124;//��ȡ�û�����
        public const int NET_DVR_SET_USERCFG = 125;//�����û�����
        public const int NET_DVR_GET_EXCEPTIONCFG = 126;//��ȡ�쳣����
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;//�����쳣����
        public const int NET_DVR_GET_ZONEANDDST = 128;//��ȡʱ������ʱ�Ʋ���
        public const int NET_DVR_SET_ZONEANDDST = 129;//����ʱ������ʱ�Ʋ���
        public const int NET_DVR_GET_SHOWSTRING = 130;//��ȡ�����ַ�����
        public const int NET_DVR_SET_SHOWSTRING = 131;//���õ����ַ�����
        public const int NET_DVR_GET_EVENTCOMPCFG = 132;//��ȡ�¼�����¼�����
        public const int NET_DVR_SET_EVENTCOMPCFG = 133;//�����¼�����¼�����

        public const int NET_DVR_GET_AUXOUTCFG = 140;//��ȡ�������������������(HS�豸�������2006-02-28)
        public const int NET_DVR_SET_AUXOUTCFG = 141;//���ñ������������������(HS�豸�������2006-02-28)
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142;//��ȡ-sϵ��˫���Ԥ������(-sϵ��˫���2006-04-13)
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143;//����-sϵ��˫���Ԥ������(-sϵ��˫���2006-04-13)

        public const int NET_DVR_GET_PICCFG_EX = 200;//��ȡͼ�����(SDK_V14��չ����)
        public const int NET_DVR_SET_PICCFG_EX = 201;//����ͼ�����(SDK_V14��չ����)
        public const int NET_DVR_GET_USERCFG_EX = 202;//��ȡ�û�����(SDK_V15��չ����)
        public const int NET_DVR_SET_USERCFG_EX = 203;//�����û�����(SDK_V15��չ����)
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204;//��ȡѹ������(SDK_V15��չ����2006-05-15)
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205;//����ѹ������(SDK_V15��չ����2006-05-15)

        public const int NET_DVR_GET_NETAPPCFG = 222;//��ȡ����Ӧ�ò��� NTP/DDNS/EMAIL
        public const int NET_DVR_SET_NETAPPCFG = 223;//��������Ӧ�ò��� NTP/DDNS/EMAIL
        public const int NET_DVR_GET_NTPCFG = 224;//��ȡ����Ӧ�ò��� NTP
        public const int NET_DVR_SET_NTPCFG = 225;//��������Ӧ�ò��� NTP
        public const int NET_DVR_GET_DDNSCFG = 226;//��ȡ����Ӧ�ò��� DDNS
        public const int NET_DVR_SET_DDNSCFG = 227;//��������Ӧ�ò��� DDNS
        //��ӦNET_DVR_EMAILPARA
        public const int NET_DVR_GET_EMAILCFG = 228;//��ȡ����Ӧ�ò��� EMAIL
        public const int NET_DVR_SET_EMAILCFG = 229;//��������Ӧ�ò��� EMAIL

        public const int NET_DVR_GET_NFSCFG = 230;/* NFS disk config */
        public const int NET_DVR_SET_NFSCFG = 231;/* NFS disk config */

        public const int NET_DVR_GET_SHOWSTRING_EX = 238;//��ȡ�����ַ�������չ(֧��8���ַ�)
        public const int NET_DVR_SET_SHOWSTRING_EX = 239;//���õ����ַ�������չ(֧��8���ַ�)
        public const int NET_DVR_GET_NETCFG_OTHER = 244;//��ȡ�������
        public const int NET_DVR_SET_NETCFG_OTHER = 245;//�����������

        //��ӦNET_DVR_EMAILCFG�ṹ
        public const int NET_DVR_GET_EMAILPARACFG = 250;//Get EMAIL parameters
        public const int NET_DVR_SET_EMAILPARACFG = 251;//Setup EMAIL parameters

        public const int NET_DVR_GET_DDNSCFG_EX = 274;//��ȡ��չDDNS����
        public const int NET_DVR_SET_DDNSCFG_EX = 275;//������չDDNS����

        public const int NET_DVR_SET_PTZPOS = 292;//��̨����PTZλ��
        public const int NET_DVR_GET_PTZPOS = 293;//��̨��ȡPTZλ��
        public const int NET_DVR_GET_PTZSCOPE = 294;//��̨��ȡPTZ��Χ

        /***************************DS9000��������(_V30) begin *****************************/
        //����(NET_DVR_NETCFG_V30�ṹ)
        public const int NET_DVR_GET_NETCFG_V30 = 1000;//��ȡ�������
        public const int NET_DVR_SET_NETCFG_V30 = 1001;//�����������

        //ͼ��(NET_DVR_PICCFG_V30�ṹ)
        public const int NET_DVR_GET_PICCFG_V30 = 1002;//��ȡͼ�����
        public const int NET_DVR_SET_PICCFG_V30 = 1003;//����ͼ�����

        //¼��ʱ��(NET_DVR_RECORD_V30�ṹ)
        public const int NET_DVR_GET_RECORDCFG_V30 = 1004;//��ȡ¼�����
        public const int NET_DVR_SET_RECORDCFG_V30 = 1005;//����¼�����

        //�û�(NET_DVR_USER_V30�ṹ)
        public const int NET_DVR_GET_USERCFG_V30 = 1006;//��ȡ�û�����
        public const int NET_DVR_SET_USERCFG_V30 = 1007;//�����û�����

        //9000DDNS��������(NET_DVR_DDNSPARA_V30�ṹ)
        public const int NET_DVR_GET_DDNSCFG_V30 = 1010;//��ȡDDNS(9000��չ)
        public const int NET_DVR_SET_DDNSCFG_V30 = 1011;//����DDNS(9000��չ)

        //EMAIL����(NET_DVR_EMAILCFG_V30�ṹ)
        public const int NET_DVR_GET_EMAILCFG_V30 = 1012;//��ȡEMAIL���� 
        public const int NET_DVR_SET_EMAILCFG_V30 = 1013;//����EMAIL���� 

        //Ѳ������ (NET_DVR_CRUISE_PARA�ṹ)
        public const int NET_DVR_GET_CRUISE = 1020;
        public const int NET_DVR_SET_CRUISE = 1021;

        //��������ṹ���� (NET_DVR_ALARMINCFG_V30�ṹ)
        public const int NET_DVR_GET_ALARMINCFG_V30 = 1024;
        public const int NET_DVR_SET_ALARMINCFG_V30 = 1025;

        //��������ṹ���� (NET_DVR_ALARMOUTCFG_V30�ṹ)
        public const int NET_DVR_GET_ALARMOUTCFG_V30 = 1026;
        public const int NET_DVR_SET_ALARMOUTCFG_V30 = 1027;

        //��Ƶ����ṹ���� (NET_DVR_VIDEOOUT_V30�ṹ)
        public const int NET_DVR_GET_VIDEOOUTCFG_V30 = 1028;
        public const int NET_DVR_SET_VIDEOOUTCFG_V30 = 1029;

        //�����ַ��ṹ���� (NET_DVR_SHOWSTRING_V30�ṹ)
        public const int NET_DVR_GET_SHOWSTRING_V30 = 1030;
        public const int NET_DVR_SET_SHOWSTRING_V30 = 1031;

        //�쳣�ṹ���� (NET_DVR_EXCEPTION_V30�ṹ)
        public const int NET_DVR_GET_EXCEPTIONCFG_V30 = 1034;
        public const int NET_DVR_SET_EXCEPTIONCFG_V30 = 1035;

        //����232�ṹ���� (NET_DVR_RS232CFG_V30�ṹ)
        public const int NET_DVR_GET_RS232CFG_V30 = 1036;
        public const int NET_DVR_SET_RS232CFG_V30 = 1037;

        //����Ӳ�̽���ṹ���� (NET_DVR_NET_DISKCFG�ṹ)
        public const int NET_DVR_GET_NET_DISKCFG = 1038;//����Ӳ�̽����ȡ
        public const int NET_DVR_SET_NET_DISKCFG = 1039;//����Ӳ�̽�������

        //ѹ������ (NET_DVR_COMPRESSIONCFG_V30�ṹ)
        public const int NET_DVR_GET_COMPRESSCFG_V30 = 1040;
        public const int NET_DVR_SET_COMPRESSCFG_V30 = 1041;

        //��ȡ485���������� (NET_DVR_DECODERCFG_V30�ṹ)
        public const int NET_DVR_GET_DECODERCFG_V30 = 1042;//��ȡ����������
        public const int NET_DVR_SET_DECODERCFG_V30 = 1043;//���ý���������

        //��ȡԤ������ (NET_DVR_PREVIEWCFG_V30�ṹ)
        public const int NET_DVR_GET_PREVIEWCFG_V30 = 1044;//��ȡԤ������
        public const int NET_DVR_SET_PREVIEWCFG_V30 = 1045;//����Ԥ������

        //����Ԥ������ (NET_DVR_PREVIEWCFG_AUX_V30�ṹ)
        public const int NET_DVR_GET_PREVIEWCFG_AUX_V30 = 1046;//��ȡ����Ԥ������
        public const int NET_DVR_SET_PREVIEWCFG_AUX_V30 = 1047;//���ø���Ԥ������

        //IP�������ò��� ��NET_DVR_IPPARACFG�ṹ��
        public const int NET_DVR_GET_IPPARACFG = 1048; //��ȡIP����������Ϣ 
        public const int NET_DVR_SET_IPPARACFG = 1049;//����IP����������Ϣ

        //IP��������������ò��� ��NET_DVR_IPALARMINCFG�ṹ��
        public const int NET_DVR_GET_IPALARMINCFG = 1050; //��ȡIP�����������������Ϣ 
        public const int NET_DVR_SET_IPALARMINCFG = 1051; //����IP�����������������Ϣ

        //IP��������������ò��� ��NET_DVR_IPALARMOUTCFG�ṹ��
        public const int NET_DVR_GET_IPALARMOUTCFG = 1052;//��ȡIP�����������������Ϣ 
        public const int NET_DVR_SET_IPALARMOUTCFG = 1053;//����IP�����������������Ϣ

        //Ӳ�̹����Ĳ�����ȡ (NET_DVR_HDCFG�ṹ)
        public const int NET_DVR_GET_HDCFG = 1054;//��ȡӲ�̹������ò���
        public const int NET_DVR_SET_HDCFG = 1055;//����Ӳ�̹������ò���

        //��������Ĳ�����ȡ (NET_DVR_HDGROUP_CFG�ṹ)
        public const int NET_DVR_GET_HDGROUP_CFG = 1056;//��ȡ����������ò���
        public const int NET_DVR_SET_HDGROUP_CFG = 1057;//��������������ò���

        //�豸������������(NET_DVR_COMPRESSION_AUDIO�ṹ)
        public const int NET_DVR_GET_COMPRESSCFG_AUD = 1058;//��ȡ�豸�����Խ��������
        public const int NET_DVR_SET_COMPRESSCFG_AUD = 1059;//�����豸�����Խ��������

        //IP�������ò��� ��NET_DVR_IPPARACFG_V31�ṹ��
        public const int NET_DVR_GET_IPPARACFG_V31 = 1060;//��ȡIP����������Ϣ 
        public const int NET_DVR_SET_IPPARACFG_V31 = 1061; //����IP����������Ϣ

        //Զ�̿�������
        public const int NET_DVR_BARRIERGATE_CTRL = 3128; //��բ����

        //�������򵥸���������
        public const int NET_DVR_GET_REGION_ENTR_REGION = 3505;    //��ȡ��������ĵ�����������
        public const int NET_DVR_SET_REGION_ENTR_REGION = 3506;   //���ý�������ĵ�����������

        //�뿪���򵥸���������
        public const int NET_DVR_GET_REGION_EXITING_REGION = 3514; //��ȡ�뿪����ĵ�����������
        public const int NET_DVR_SET_REGION_EXITING_REGION = 3515; //�����뿪����ĵ�����������

        //Խ���������
        public const int NET_DVR_GET_TRAVERSE_PLANE_DETECTION = 3360 ; //��ȡԽ���������
        public const int NET_DVR_SET_TRAVERSE_PLANE_DETECTION = 3361; //����Խ���������

        //�����������
        public const int NET_DVR_GET_FIELD_DETECTION = 3362; //��ȡ�����������
        public const int NET_DVR_SET_FIELD_DETECTION = 3363; //���������������
        /***************************DS9000��������(_V30) end *****************************/

        /*************************������������ end*******************************/

        /*******************�����ļ�����־��������ֵ*************************/
        public const int NET_DVR_FILE_SUCCESS = 1000;//����ļ���Ϣ
        public const int NET_DVR_FILE_NOFIND = 1001;//û���ļ�
        public const int NET_DVR_ISFINDING = 1002;//���ڲ����ļ�
        public const int NET_DVR_NOMOREFILE = 1003;//�����ļ�ʱû�и�����ļ�
        public const int NET_DVR_FILE_EXCEPTION = 1004;//�����ļ�ʱ�쳣

        /*********************�ص��������� begin************************/
        public const int COMM_ALARM_RULE_CALC = 0x1110;  //��Ϊͳ�Ʊ����ϴ�(��Ա�ܶ�)
        public const int COMM_ALARM = 0x1100;//8000������Ϣ�����ϴ�����ӦNET_DVR_ALARMINFO
        public const int COMM_ALARM_RULE = 0x1102;//��Ϊ����������Ϣ����ӦNET_VCA_RULE_ALARM
        public const int COMM_ALARM_PDC = 0x1103;//������ͳ�Ʊ����ϴ�����ӦNET_DVR_PDC_ALRAM_INFO
        public const int COMM_ALARM_ALARMHOST = 0x1105;//���籨�����������ϴ�����ӦNET_DVR_ALARMHOST_ALARMINFO
        public const int COMM_ALARM_FACE = 0x1106;//�������ʶ�𱨾���Ϣ����ӦNET_DVR_FACEDETECT_ALARM
        public const int COMM_RULE_INFO_UPLOAD = 0x1107;  // �¼�������Ϣ�ϴ�
        public const int COMM_ALARM_AID = 0x1110;  //��ͨ�¼�������Ϣ
        public const int COMM_ALARM_TPS = 0x1111;  //��ͨ����ͳ�Ʊ�����Ϣ
        public const int COMM_UPLOAD_FACESNAP_RESULT = 0x1112;  //����ʶ�����ϴ�
        public const int COMM_ALARM_FACE_DETECTION = 0x4010; //������ⱨ����Ϣ
        public const int COMM_ALARM_TFS = 0x1113;  //��ͨȡ֤������Ϣ
        public const int COMM_ALARM_TPS_V41 = 0x1114;  //��ͨ����ͳ�Ʊ�����Ϣ��չ
        public const int COMM_ALARM_AID_V41 = 0x1115;  //��ͨ�¼�������Ϣ��չ
        public const int COMM_ALARM_VQD_EX = 0x1116;     //��Ƶ������ϱ���
        public const int COMM_SENSOR_VALUE_UPLOAD = 0x1120;  //ģ��������ʵʱ�ϴ�
        public const int COMM_SENSOR_ALARM = 0x1121;  //ģ���������ϴ�
        public const int COMM_SWITCH_ALARM = 0x1122;     //����������
        public const int COMM_ALARMHOST_EXCEPTION = 0x1123; //�����������ϱ���
        public const int COMM_ALARMHOST_OPERATEEVENT_ALARM = 0x1124;  //�����¼������ϴ�
        public const int COMM_ALARMHOST_SAFETYCABINSTATE = 0x1125;     //������״̬
        public const int COMM_ALARMHOST_ALARMOUTSTATUS = 0x1126;     //���������/����״̬
        public const int COMM_ALARMHOST_CID_ALARM = 0x1127;     //CID���汨���ϴ�
        public const int COMM_ALARMHOST_EXTERNAL_DEVICE_ALARM = 0x1128;     //������������豸�����ϴ�
        public const int COMM_ALARMHOST_DATA_UPLOAD = 0x1129;     //���������ϴ�
        public const int COMM_ALARM_AUDIOEXCEPTION = 0x1150;     //����������Ϣ
        public const int COMM_ALARM_DEFOCUS = 0x1151;     //�齹������Ϣ
        public const int COMM_ALARM_BUTTON_DOWN_EXCEPTION = 0x1152;     //��ť���±�����Ϣ
        public const int COMM_ALARM_ALARMGPS = 0x1202; //GPS������Ϣ�ϴ�
        public const int COMM_TRADEINFO = 0x1500;  //ATMDVR�����ϴ�������Ϣ
        public const int COMM_UPLOAD_PLATE_RESULT = 0x2800;     //�ϴ�������Ϣ
        public const int COMM_ITC_STATUS_DETECT_RESULT = 0x2810;  //ʵʱ״̬������ϴ�(���ܸ���IPC)
        public const int COMM_IPC_AUXALARM_RESULT = 0x2820;  //PIR���������߱��������ȱ����ϴ�
        public const int COMM_UPLOAD_PICTUREINFO = 0x2900;     //�ϴ�ͼƬ��Ϣ
        public const int COMM_SNAP_MATCH_ALARM = 0x2902;  
        public const int COMM_ITS_PLATE_RESULT = 0x3050;  //�ն�ͼƬ�ϴ�
        public const int COMM_ITS_TRAFFIC_COLLECT = 0x3051;  //�ն�ͳ�������ϴ�
        public const int COMM_ITS_GATE_VEHICLE = 0x3052;  //����ڳ���ץ�������ϴ�
        public const int COMM_ITS_GATE_FACE = 0x3053; //���������ץ�������ϴ�
        public const int COMM_ITS_GATE_COSTITEM = 0x3054;  //����ڹ����շ���ϸ 2013-11-19
        public const int COMM_ITS_GATE_HANDOVER = 0x3055; //����ڽ��Ӱ����� 2013-11-19
        public const int COMM_ITS_PARK_VEHICLE = 0x3056;  //ͣ���������ϴ�
        public const int COMM_ITS_BLOCKLIST_ALARM = 0x3057;  
        public const int COMM_ALARM_V30 = 0x4000;     //9000������Ϣ�����ϴ�
        public const int COMM_IPCCFG = 0x4001;     //9000�豸IPC�������øı䱨����Ϣ�����ϴ�
        public const int COMM_IPCCFG_V31 = 0x4002;     //9000�豸IPC�������øı䱨����Ϣ�����ϴ���չ 9000_1.1
        public const int COMM_IPCCFG_V40 = 0x4003; // IVMS 2000 ��������� NVR IPC�������øı�ʱ������Ϣ�ϴ�
        public const int COMM_ALARM_DEVICE = 0x4004;  //�豸�������ݣ�����ͨ��ֵ����256����չ
        public const int COMM_ALARM_CVR = 0x4005;  //CVR 2.0.X�ⲿ��������
        public const int COMM_ALARM_HOT_SPARE = 0x4006;  //�ȱ��쳣������N+1ģʽ�쳣������
        public const int COMM_ALARM_V40 = 0x4007;    //�ƶ���⣬��Ƶ��ʧ���ڵ���IO�ź����ȱ�����Ϣ�����ϴ�����������Ϊ�ɱ䳤

        public const int COMM_ITS_ROAD_EXCEPTION = 0x4500;     //·���豸�쳣����
        public const int COMM_ITS_EXTERNAL_CONTROL_ALARM = 0x4520;  //��ر���
        public const int COMM_SCREEN_ALARM = 0x5000;  //������������������
        public const int COMM_DVCS_STATE_ALARM = 0x5001;  //�ֲ�ʽ���������������ϴ�
        public const int COMM_ALARM_VQD = 0x6000;  //VQD���������ϴ� 
        public const int COMM_PUSH_UPDATE_RECORD_INFO = 0x6001;  //��ģʽ¼����Ϣ�ϴ�
        public const int COMM_DIAGNOSIS_UPLOAD = 0x5100;  //��Ϸ�����VQD�����ϴ�
        //public const int COMM_ALARM_ACS = 0x5002;  //�Ž���������
        public const int COMM_ID_INFO_ALARM = 0x5200;  //����֤��Ϣ�ϴ�
        public const int COMM_PASSNUM_INFO_ALARM = 0x5201;  //ͨ�������ϱ�

        public const int COMM_UPLOAD_AIOP_VIDEO =  0x4021; //�豸֧��AI����ƽ̨���룬�ϴ���Ƶ������� ��Ӧ�Ľṹ��(NET_AIOP_VIDEO_HEAD)
        public const int COMM_UPLOAD_AIOP_PICTURE = 0x4022; //�豸֧��AI����ƽ̨���룬�ϴ�ͼƬ������� ��Ӧ�Ľṹ��(NET_AIOP_PICTURE_HEAD)
        public const int COMM_UPLOAD_AIOP_POLLING_SNAP = 0x4023; //�豸֧��AI����ƽ̨���룬�ϴ���ѯץͼͼƬ������� ��Ӧ�Ľṹ��(NET_AIOP_POLLING_PICTURE_HEAD)
        public const int COMM_UPLOAD_AIOP_POLLING_VIDEO = 0x4024; //�豸֧��AI����ƽ̨���룬�ϴ���ѯ��Ƶ������� ��Ӧ�Ľṹ��(NET_AIOP_POLLING_VIDEO_HEAD)


        /*************�����쳣����(��Ϣ��ʽ, �ص���ʽ(����))****************/
        public const int EXCEPTION_EXCHANGE = 32768;//�û�����ʱ�쳣
        public const int EXCEPTION_AUDIOEXCHANGE = 32769;//�����Խ��쳣
        public const int EXCEPTION_ALARM = 32770;//�����쳣
        public const int EXCEPTION_PREVIEW = 32771;//����Ԥ���쳣
        public const int EXCEPTION_SERIAL = 32772;//͸��ͨ���쳣
        public const int EXCEPTION_RECONNECT = 32773;//Ԥ��ʱ����
        public const int EXCEPTION_ALARMRECONNECT = 32774;//����ʱ����
        public const int EXCEPTION_SERIALRECONNECT = 32775;//͸��ͨ������
        public const int EXCEPTION_PLAYBACK = 32784;//�ط��쳣
        public const int EXCEPTION_DISKFMT = 32785;//Ӳ�̸�ʽ��

        /********************Ԥ���ص�����*********************/
        public const int NET_DVR_SYSHEAD = 1;//ϵͳͷ����
        public const int NET_DVR_STREAMDATA = 2;//��Ƶ�����ݣ�����������������Ƶ�ֿ�����Ƶ�����ݣ�
        public const int NET_DVR_AUDIOSTREAMDATA = 3;//��Ƶ������
        public const int NET_DVR_STD_VIDEODATA = 4;//��׼��Ƶ������
        public const int NET_DVR_STD_AUDIODATA = 5;//��׼��Ƶ������

        //�ص�Ԥ���е�״̬����Ϣ
        public const int NET_DVR_REALPLAYEXCEPTION = 111;//Ԥ���쳣
        public const int NET_DVR_REALPLAYNETCLOSE = 112;//Ԥ��ʱ���ӶϿ�
        public const int NET_DVR_REALPLAY5SNODATA = 113;//Ԥ��5sû���յ�����
        public const int NET_DVR_REALPLAYRECONNECT = 114;//Ԥ������

        /********************�طŻص�����*********************/
        public const int NET_DVR_PLAYBACKOVER = 101;//�ط����ݲ������
        public const int NET_DVR_PLAYBACKEXCEPTION = 102;//�ط��쳣
        public const int NET_DVR_PLAYBACKNETCLOSE = 103;//�ط�ʱ�����ӶϿ�
        public const int NET_DVR_PLAYBACK5SNODATA = 104;//�ط�5sû���յ�����

        /*********************�ص��������� end************************/
        //�豸�ͺ�(DVR����)
        /* �豸���� */
        public const int DVR = 1;/*����δ�����dvr���ͷ���NETRET_DVR*/
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
        public const int DVS_A = 13; /* ��ATAӲ�̵�DVS */
        public const int DVR_HC_S = 14; /* 8000HC-S */
        public const int DVR_HT_S = 15;/* 8000HT-S */
        public const int DVR_HF_S = 16;/* 8000HF-S */
        public const int DVR_HS_S = 17; /* 8000HS-S */
        public const int ATMDVR_S = 18;/* ATM-S */
        public const int LOWCOST_DVR = 19;/*7000Hϵ��*/
        public const int DEC_MAT = 20; /*��·������*/
        public const int DVR_MOBILE = 21;/* mobile DVR */
        public const int DVR_HD_S = 22;   /* 8000HD-S */
        public const int DVR_HD_SL = 23;/* 8000HD-SL */
        public const int DVR_HC_SL = 24;/* 8000HC-SL */
        public const int DVR_HS_ST = 25;/* 8000HS_ST */
        public const int DVS_HW = 26; /* 6000HW */
        public const int DS630X_D = 27; /* ��·������ */
        public const int IPCAM = 30;/*IP �����*/
        public const int MEGA_IPCAM = 31;/*X52MFϵ��,752MF,852MF*/
        public const int IPCAM_X62MF = 32;/*X62MFϵ�пɽ���9000�豸,762MF,862MF*/
        public const int IPDOME = 40; /*IP �������*/
        public const int IPDOME_MEGA200 = 41;/*IP 200��������*/
        public const int IPDOME_MEGA130 = 42;/*IP 130��������*/
        public const int IPMOD = 50;/*IP ģ��*/
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
        /**********************�豸���� end***********************/

        /*************************************************
        �������ýṹ������(����_V30Ϊ9000����)
        **************************************************/
        //Уʱ�ṹ����
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

        //ʱ���(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCHEDTIME
        {
            public byte byStartHour;//��ʼʱ��
            public byte byStartMin;//��ʼʱ��
            public byte byStopHour;//����ʱ��
            public byte byStopMin;//����ʱ��
        }

        /*�豸�������쳣������ʽ*/
        public const int NOACTION = 0;/*����Ӧ*/
        public const int WARNONMONITOR = 1;/*�������Ͼ���*/
        public const int WARNONAUDIOOUT = 2;/*��������*/
        public const int UPTOCENTER = 4;/*�ϴ�����*/
        public const int TRIGGERALARMOUT = 8;/*�����������*/

        //�������쳣�����ṹ(�ӽṹ)(�ദʹ��)(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V30
        {
            public uint dwHandleType;/*������ʽ,������ʽ��"��"���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//�������������ͨ��,�������������,Ϊ1��ʾ���������
        }

        //�������쳣�����ṹ(�ӽṹ)(�ദʹ��)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION
        {
            public uint dwHandleType;/*������ʽ,������ʽ��"��"���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;//�������������ͨ��,�������������,Ϊ1��ʾ���������
        }

        //DVR�豸����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;//DVR����
            public uint dwDVRID;//DVR ID,����ң���� //V1.4(0-99), V1.5(0-255)
            public uint dwRecycleRecord;//�Ƿ�ѭ��¼��,0:����; 1:��
            //���²��ɸ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//���к�
            public uint dwSoftwareVersion;//�����汾��,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwSoftwareBuildDate;//������������,0xYYYYMMDD
            public uint dwDSPSoftwareVersion;//DSP�����汾,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwDSPSoftwareBuildDate;// DSP������������,0xYYYYMMDD
            public uint dwPanelVersion;// ǰ���汾,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwHardwareVersion;// Ӳ���汾,��16λ�����汾,��16λ�Ǵΰ汾
            public byte byAlarmInPortNum;//DVR�����������
            public byte byAlarmOutPortNum;//DVR�����������
            public byte byRS232Num;//DVR 232���ڸ���
            public byte byRS485Num;//DVR 485���ڸ���
            public byte byNetworkPortNum;//����ڸ���
            public byte byDiskCtrlNum;//DVR Ӳ�̿���������
            public byte byDiskNum;//DVR Ӳ�̸���
            public byte byDVRType;//DVR����, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR ͨ������
            public byte byStartChan;//��ʼͨ����,����DVS-1,DVR - 1
            public byte byDecordChans;//DVR ����·��
            public byte byVGANum;//VGA�ڵĸ���
            public byte byUSBNum;//USB�ڵĸ���
            public byte byAuxoutNum;//���ڵĸ���
            public byte byAudioNum;//�����ڵĸ���
            public byte byIPChanNum;//�������ͨ����
        }

        /*IP��ַ*/
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

        /*�������ݽṹ(�ӽṹ)(9000��չ)*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ETHERNET_V30
        {
            public NET_DVR_IPADDR struDVRIP;//DVR IP��ַ
            public NET_DVR_IPADDR struDVRIPMask;//DVR IP��ַ����
            public uint dwNetInterface;//����ӿ�1-10MBase-T 2-10MBase-Tȫ˫�� 3-100MBase-TX 4-100Mȫ˫�� 5-10M/100M����Ӧ
            public ushort wDVRPort;//�˿ں�
            public ushort wMTU;//����MTU���ã�Ĭ��1500��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;// ������ַ
        }

        /*�������ݽṹ(�ӽṹ)*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;//DVR IP��ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIPMask;//DVR IP��ַ����
            public uint dwNetInterface;//����ӿ� 1-10MBase-T 2-10MBase-Tȫ˫�� 3-100MBase-TX 4-100Mȫ˫�� 5-10M/100M����Ӧ
            public ushort wDVRPort;//�˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;//��������������ַ
        }

        //pppoe�ṹ
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPOECFG
        {
            public uint dwPPPOE;//0-������,1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE�û���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE����
            public NET_DVR_IPADDR struPPPoEIP;//PPPoE IP��ַ
        }

        //�������ýṹ(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NETCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET_V30[] struEtherNet;//��̫����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struRes1;/*����*/
            public NET_DVR_IPADDR struAlarmHostIpAddr;/* ��������IP��ַ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U2)]
            public ushort[] wRes2;
            public ushort wAlarmHostIpPort;
            public byte byUseDhcp;
            public byte byIPv6Mode;//IPv6���䷽ʽ��0-·�ɹ��棬1-�ֶ����ã�2-����DHCP����
            public NET_DVR_IPADDR struDnsServer1IpAddr;/* ����������1��IP��ַ */
            public NET_DVR_IPADDR struDnsServer2IpAddr;/* ����������2��IP��ַ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byIpResolver;
            public ushort wIpResolverPort;
            public ushort wHttpPortNo;
            public NET_DVR_IPADDR struMulticastIpAddr;/* �ಥ���ַ */
            public NET_DVR_IPADDR struGatewayIpAddr;/* ���ص�ַ */
            public NET_DVR_PPPOECFG struPPPoE;
            public byte byEnablePrivateMulticastDiscovery;  //˽�жಥ������0~Ĭ�ϣ�1~���ã�2-����
            public byte byEnableOnvifMulticastDiscovery;  //Onvif�ಥ������0~Ĭ�ϣ�1~���ã�2-����
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

        //�������ýṹ
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET[] struEtherNet;/* ��̫���� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sManageHostIP;//Զ�̹���������ַ
            public ushort wManageHostPort;//Զ�̹��������˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIPServerIP;//IPServer��������ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sMultiCastIP;//�ಥ���ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIP;//���ص�ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNFSIP;//NFS����IP��ַ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNFSDirectory;//NFSĿ¼
            public uint dwPPPOE;//0-������,1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;//PPPoE�û���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;// PPPoE����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sPPPoEIP;//PPPoE IP��ַ(ֻ��)
            public ushort wHttpPort;//HTTP�˿ں�
        }

        //ͨ��ͼ��ṹ
        //�ƶ����(�ӽṹ)(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MOTION_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96 * 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*�������,0-96λ,��ʾ64��,����96*64��С���,Ϊ1��ʾ���ƶ��������,0-��ʾ����*/
            public byte byMotionSensitive;/*�ƶ����������, 0 - 5,Խ��Խ����,oxff�ر�*/
            public byte byEnableHandleMotion;/* �Ƿ����ƶ���� 0���� 1����*/
            public byte byPrecision;/* �ƶ�����㷨�Ľ���: 0--16*16, 1--32*32, 2--64*64 ... */
            public byte reservedData;
            public NET_DVR_HANDLEEXCEPTION_V30 struMotionHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;/*����ʱ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;/* ����������¼��ͨ��*/
        }

        //�ƶ����(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MOTION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 396, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;/*�������,����22*18��С���,Ϊ1��ʾ�ĺ�����ƶ��������,0-��ʾ����*/
            public byte byMotionSensitive;/*�ƶ����������, 0 - 5,Խ��Խ����,0xff�ر�*/
            public byte byEnableHandleMotion;/* �Ƿ����ƶ���� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string reservedData;
            public NET_DVR_HANDLEEXCEPTION strMotionHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//����������¼��ͨ��,Ϊ1��ʾ������ͨ��
        }

        //�ڵ�����(�ӽṹ)(9000��չ)  �����С704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM_V30
        {
            public uint dwEnableHideAlarm;/* �Ƿ������ڵ����� ,0-��,1-�������� 2-�������� 3-��������*/
            public ushort wHideAlarmAreaTopLeftX;/* �ڵ������x���� */
            public ushort wHideAlarmAreaTopLeftY;/* �ڵ������y���� */
            public ushort wHideAlarmAreaWidth;/* �ڵ�����Ŀ� */
            public ushort wHideAlarmAreaHeight;/*�ڵ�����ĸ�*/
            public NET_DVR_HANDLEEXCEPTION_V30 strHideAlarmHandleType;    /* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
        }

        //�ڵ�����(�ӽṹ)  �����С704*576
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM
        {
            public uint dwEnableHideAlarm;/* �Ƿ������ڵ����� ,0-��,1-�������� 2-�������� 3-��������*/
            public ushort wHideAlarmAreaTopLeftX;/* �ڵ������x���� */
            public ushort wHideAlarmAreaTopLeftY;/* �ڵ������y���� */
            public ushort wHideAlarmAreaWidth;/* �ڵ�����Ŀ� */
            public ushort wHideAlarmAreaHeight;/*�ڵ�����ĸ�*/
            public NET_DVR_HANDLEEXCEPTION strHideAlarmHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
        }

        //�źŶ�ʧ����(�ӽṹ)(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST_V30
        {
            public byte byEnableHandleVILost;/* �Ƿ����źŶ�ʧ���� */
            public NET_DVR_HANDLEEXCEPTION_V30 strVILostHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
        }

        //�źŶ�ʧ����(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST
        {
            public byte byEnableHandleVILost;/* �Ƿ����źŶ�ʧ���� */
            public NET_DVR_HANDLEEXCEPTION strVILostHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
        }

        //�ڵ�����(�ӽṹ)
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_SHELTER
        {
            public ushort wHideAreaTopLeftX;/* �ڵ������x���� */
            public ushort wHideAreaTopLeftY;/* �ڵ������y���� */
            public ushort wHideAreaWidth;/* �ڵ�����Ŀ� */
            public ushort wHideAreaHeight;/*�ڵ�����ĸ�*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COLOR
        {
            public byte byBrightness;/*����,0-255*/
            public byte byContrast;/*�Աȶ�,0-255*/
            public byte bySaturation;/*���Ͷ�,0-255*/
            public byte byHue;/*ɫ��,0-255*/
        }

        //ͨ��ͼ��ṹ(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PICCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            //  [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public string sChanName;
            public uint dwVideoFormat;/* ֻ�� ��Ƶ��ʽ 1-NTSC 2-PAL*/
            public NET_DVR_COLOR struColor;//    ͼ�����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string reservedData;/*����*/
            //��ʾͨ����
            public uint dwShowChanName;// Ԥ����ͼ�����Ƿ���ʾͨ������,0-����ʾ,1-��ʾ �����С704*576
            public ushort wShowNameTopLeftX;/* ͨ��������ʾλ�õ�x���� */
            public ushort wShowNameTopLeftY;/* ͨ��������ʾλ�õ�y���� */
            //��Ƶ�źŶ�ʧ����
            public NET_DVR_VILOST_V30 struVILost;
            public NET_DVR_VILOST_V30 struRes;/*����*/
            //�ƶ����
            public NET_DVR_MOTION_V30 struMotion;
            //�ڵ�����
            public NET_DVR_HIDEALARM_V30 struHideAlarm;
            //�ڵ�  �����С704*576
            public uint dwEnableHide;/* �Ƿ������ڵ� ,0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// Ԥ����ͼ�����Ƿ���ʾOSD,0-����ʾ,1-��ʾ �����С704*576
            public ushort wOSDTopLeftX;/* OSD��x���� */
            public ushort wOSDTopLeftY;/* OSD��y���� */
            public byte byOSDType;/* OSD����(��Ҫ�������ո�ʽ) */
            /* 0: XXXX-XX-XX ������ */
            /* 1: XX-XX-XXXX ������ */
            /* 2: XXXX��XX��XX�� */
            /* 3: XX��XX��XXXX�� */
            /* 4: XX-XX-XXXX ������*/
            /* 5: XX��XX��XXXX�� */
            public byte byDispWeek;/* �Ƿ���ʾ���� */
            public byte byOSDAttrib;/* OSD����:͸������˸ */
            /* 0: ����ʾOSD */
            /* 1: ͸��,��˸ */
            /* 2: ͸��,����˸ */
            /* 3: ��˸,��͸�� */
            /* 4: ��͸��,����˸ */
            public byte byHourOSDType;/* OSDСʱ��:0-24Сʱ��,1-12Сʱ�� */
            public byte byFontSize;//�����С��16*16(��)/8*16(Ӣ)��1-32*32(��)/16*32(Ӣ)��2-64*64(��)/32*64(Ӣ)  3-48*48(��)/24*48(Ӣ) 0xff-����Ӧ(adaptive)
            public byte byOSDColorType;    //0-Ĭ�ϣ��ڰף���1-�Զ���
            public byte byAlignment;//���뷽ʽ 0-����Ӧ��1-�Ҷ���, 2-�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 61, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        //ͨ��ͼ��ṹSDK_V14��չ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* ֻ�� ��Ƶ��ʽ 1-NTSC 2-PAL*/
            public byte byBrightness;/*����,0-255*/
            public byte byContrast;/*�Աȶ�,0-255*/
            public byte bySaturation;/*���Ͷ�,0-255 */
            public byte byHue;/*ɫ��,0-255*/
            //��ʾͨ����
            public uint dwShowChanName;// Ԥ����ͼ�����Ƿ���ʾͨ������,0-����ʾ,1-��ʾ �����С704*576
            public ushort wShowNameTopLeftX;/* ͨ��������ʾλ�õ�x���� */
            public ushort wShowNameTopLeftY;/* ͨ��������ʾλ�õ�y���� */
            //�źŶ�ʧ����
            public NET_DVR_VILOST struVILost;
            //�ƶ����
            public NET_DVR_MOTION struMotion;
            //�ڵ�����
            public NET_DVR_HIDEALARM struHideAlarm;
            //�ڵ�  �����С704*576
            public uint dwEnableHide;/* �Ƿ������ڵ� ,0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;// Ԥ����ͼ�����Ƿ���ʾOSD,0-����ʾ,1-��ʾ �����С704*576
            public ushort wOSDTopLeftX;/* OSD��x���� */
            public ushort wOSDTopLeftY;/* OSD��y���� */
            public byte byOSDType;/* OSD����(��Ҫ�������ո�ʽ) */
            /* 0: XXXX-XX-XX ������ */
            /* 1: XX-XX-XXXX ������ */
            /* 2: XXXX��XX��XX�� */
            /* 3: XX��XX��XXXX�� */
            /* 4: XX-XX-XXXX ������*/
            /* 5: XX��XX��XXXX�� */
            public byte byDispWeek;/* �Ƿ���ʾ���� */
            public byte byOSDAttrib;/* OSD����:͸������˸ */
            /* 0: ����ʾOSD */
            /* 1: ͸��,��˸ */
            /* 2: ͸��,����˸ */
            /* 3: ��˸,��͸�� */
            /* 4: ��͸��,����˸ */
            public byte byHourOsdType;/* OSDСʱ��:0-24Сʱ��,1-12Сʱ�� */
        }

        //ͨ��ͼ��ṹ(SDK_V13��֮ǰ�汾)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;/* ֻ�� ��Ƶ��ʽ 1-NTSC 2-PAL*/
            public byte byBrightness;/*����,0-255*/
            public byte byContrast;/*�Աȶ�,0-255*/
            public byte bySaturation;/*���Ͷ�,0-255 */
            public byte byHue;/*ɫ��,0-255*/
            //��ʾͨ����
            public uint dwShowChanName;// Ԥ����ͼ�����Ƿ���ʾͨ������,0-����ʾ,1-��ʾ �����С704*576
            public ushort wShowNameTopLeftX;/* ͨ��������ʾλ�õ�x���� */
            public ushort wShowNameTopLeftY;/* ͨ��������ʾλ�õ�y���� */
            //�źŶ�ʧ����
            public NET_DVR_VILOST struVILost;
            //�ƶ����
            public NET_DVR_MOTION struMotion;
            //�ڵ�����
            public NET_DVR_HIDEALARM struHideAlarm;
            //�ڵ�  �����С704*576
            public uint dwEnableHide;/* �Ƿ������ڵ� ,0-��,1-��*/
            public ushort wHideAreaTopLeftX;/* �ڵ������x���� */
            public ushort wHideAreaTopLeftY;/* �ڵ������y���� */
            public ushort wHideAreaWidth;/* �ڵ�����Ŀ� */
            public ushort wHideAreaHeight;/*�ڵ�����ĸ�*/
            //OSD
            public uint dwShowOsd;// Ԥ����ͼ�����Ƿ���ʾOSD,0-����ʾ,1-��ʾ �����С704*576
            public ushort wOSDTopLeftX;/* OSD��x���� */
            public ushort wOSDTopLeftY;/* OSD��y���� */
            public byte byOSDType;/* OSD����(��Ҫ�������ո�ʽ) */
            /* 0: XXXX-XX-XX ������ */
            /* 1: XX-XX-XXXX ������ */
            /* 2: XXXX��XX��XX�� */
            /* 3: XX��XX��XXXX�� */
            /* 4: XX-XX-XXXX ������*/
            /* 5: XX��XX��XXXX�� */
            public byte byDispWeek;/* �Ƿ���ʾ���� */
            public byte byOSDAttrib;/* OSD����:͸������˸ */
            /* 0: ����ʾOSD */
            /* 1: ͸��,��˸ */
            /* 2: ͸��,����˸ */
            /* 3: ��˸,��͸�� */
            /* 4: ��͸��,����˸ */
            public byte reservedData2;
        }

        //����ѹ������(�ӽṹ)(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_V30
        {
            public byte byStreamType;//�������� 0-��Ƶ��, 1-������, ��ʾ�¼�ѹ������ʱ���λ��ʾ�Ƿ�����ѹ������
            public byte byResolution;//�ֱ���0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF 5��������16-VGA��640*480�� 17-UXGA��1600*1200�� 18-SVGA ��800*600��19-HD720p��1280*720��20-XVGA  21-HD900p
            public byte byBitrateType;//�������� 0:������, 1:������
            public byte byPicQuality;//ͼ������ 0-��� 1-�κ� 2-�Ϻ� 3-һ�� 4-�ϲ� 5-��
            public uint dwVideoBitrate;//��Ƶ���� 0-���� 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //���λ(31λ)�ó�1��ʾ���Զ�������, 0-30λ��ʾ����ֵ��
            public uint dwVideoFrameRate;//֡�� 0-ȫ��; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20; V2.0�汾���¼�14-15; 15-18; 16-22;
            public ushort wIntervalFrameI;//I֡���
            //2006-08-11 ���ӵ�P֡�����ýӿڣ����Ը���ʵʱ����ʱ����
            public byte byIntervalBPFrame;//0-BBP֡; 1-BP֡; 2-��P֡
            public byte byres1; //����
            public byte byVideoEncType;//��Ƶ�������� 0 hik264;1��׼h264; 2��׼mpeg4;
            public byte byAudioEncType; //��Ƶ�������� 0��OggVorbis
            public byte byVideoEncComplexity; //��Ƶ���븴�Ӷȣ�0-�ͣ�1-�У�2��,0xfe:�Զ�����Դһ��
            public byte byEnableSvc; //0 - ������SVC���ܣ�1- ����SVC����; 2-�Զ�����SVC����
            public byte byFormatType; //��װ���ͣ�1-������2-RTP��װ��3-PS��װ��4-TS��װ��5-˽�У�6-FLV��7-ASF��8-3GP,9-RTP+PS�����꣺GB28181����0xff-��Ч
            public byte byAudioBitRate; //��Ƶ���� �ο� BITRATE_ENCODE_INDEX
            public byte byStreamSmooth;//����ƽ�� 1��100��1�ȼ���ʾ����(Clear)��100��ʾƽ��(Smooth)��
            public byte byAudioSamplingRate;//��Ƶ������0-Ĭ��,1- 16kHZ, 2-32kHZ, 3-48kHZ, 4- 44.1kHZ,5-8kHZ
            public byte bySmartCodec;//�����ܱ��� 0-�رգ�1-��
            public byte byres;
            //ƽ�����ʣ���SmartCodecʹ�ܿ�������Ч��, 0-0K 1-16K 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K 24-2560K 25-3072K 26-4096K 27-5120K 28-6144K 29-7168K 30-8192K
            //���λ(15λ)�ó�1��ʾ���Զ�������, 0-14λ��ʾ����ֵ(MIN- 0 K)��
            public ushort wAverageVideoBitrate;
        }

        //ͨ��ѹ������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_V30
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_V30 struNormHighRecordPara;//¼�� ��Ӧ8000����ͨ
            public NET_DVR_COMPRESSION_INFO_V30 struRes;//���� char reserveData[28];
            public NET_DVR_COMPRESSION_INFO_V30 struEventRecordPara;//�¼�����ѹ������
            public NET_DVR_COMPRESSION_INFO_V30 struNetPara;//����(������)
        }

        //����ѹ������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO
        {
            public byte byStreamType;//��������0-��Ƶ��,1-������,��ʾѹ������ʱ���λ��ʾ�Ƿ�����ѹ������
            public byte byResolution;//�ֱ���0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(����ר��)
            public byte byBitrateType;//��������0:�����ʣ�1:������
            public byte byPicQuality;//ͼ������ 0-��� 1-�κ� 2-�Ϻ� 3-һ�� 4-�ϲ� 5-��
            public uint dwVideoBitrate; //��Ƶ���� 0-���� 1-16K(����) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //���λ(31λ)�ó�1��ʾ���Զ�������, 0-30λ��ʾ����ֵ(MIN-32K MAX-8192K)��
            public uint dwVideoFrameRate;//֡�� 0-ȫ��; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20;
        }

        //ͨ��ѹ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO struRecordPara;//¼��/�¼�����¼��
            public NET_DVR_COMPRESSION_INFO struNetPara;//����/����
        }

        //����ѹ������(�ӽṹ)(��չ) ����I֡���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_EX
        {
            public byte byStreamType;//��������0-��Ƶ��, 1-������
            public byte byResolution;//�ֱ���0-DCIF 1-CIF, 2-QCIF, 3-4CIF, 4-2CIF, 5-2QCIF(352X144)(����ר��)
            public byte byBitrateType;//��������0:�����ʣ�1:������
            public byte byPicQuality;//ͼ������ 0-��� 1-�κ� 2-�Ϻ� 3-һ�� 4-�ϲ� 5-��
            public uint dwVideoBitrate;//��Ƶ���� 0-���� 1-16K(����) 2-32K 3-48k 4-64K 5-80K 6-96K 7-128K 8-160k 9-192K 10-224K 11-256K 12-320K
            // 13-384K 14-448K 15-512K 16-640K 17-768K 18-896K 19-1024K 20-1280K 21-1536K 22-1792K 23-2048K
            //���λ(31λ)�ó�1��ʾ���Զ�������, 0-30λ��ʾ����ֵ(MIN-32K MAX-8192K)��
            public uint dwVideoFrameRate;//֡�� 0-ȫ��; 1-1/16; 2-1/8; 3-1/4; 4-1/2; 5-1; 6-2; 7-4; 8-6; 9-8; 10-10; 11-12; 12-16; 13-20, //V2.0����14-15, 15-18, 16-22;
            public ushort wIntervalFrameI;//I֡���
            //2006-08-11 ���ӵ�P֡�����ýӿڣ����Ը���ʵʱ����ʱ����
            public byte byIntervalBPFrame;//0-BBP֡; 1-BP֡; 2-��P֡
            public byte byRes;
        }

        //ͨ��ѹ������(��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_EX
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struRecordPara;//¼��
            public NET_DVR_COMPRESSION_INFO_EX struNetPara;//����
        }

        //ʱ���¼���������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RECORDSCHED
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            public byte byRecordType;//0:��ʱ¼��1:�ƶ���⣬2:����¼��3:����|������4:����&����, 5:�����, 6: ����¼��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string reservedData;
        }

        //ȫ��¼���������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY
        {
            public ushort wAllDayRecord;/* �Ƿ�ȫ��¼�� 0-�� 1-��*/
            public byte byRecordType;/* ¼������ 0:��ʱ¼��1:�ƶ���⣬2:����¼��3:����|������4:����&���� 5:�����, 6: ����¼��*/
            public byte reservedData;
        }

        //ͨ��¼���������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V30
        {
            public uint dwSize;
            public uint dwRecord;/*�Ƿ�¼�� 0-�� 1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* ¼����ʱ���� 0-5�룬 1-20�룬 2-30�룬 3-1���ӣ� 4-2���ӣ� 5-5���ӣ� 6-10����*/
            public uint dwPreRecordTime;/* Ԥ¼ʱ�� 0-��Ԥ¼ 1-5�� 2-10�� 3-15�� 4-20�� 5-25�� 6-30�� 7-0xffffffff(������Ԥ¼) */
            public uint dwRecorderDuration;/* ¼�񱣴���ʱ�� */
            public byte byRedundancyRec;/*�Ƿ�����¼��,��Ҫ����˫���ݣ�0/1*/
            public byte byAudioRec;/*¼��ʱ����������ʱ�Ƿ��¼��Ƶ���ݣ������д˷���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;
        }

        //ͨ��¼���������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD
        {
            public uint dwSize;
            public uint dwRecord;/*�Ƿ�¼�� 0-�� 1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;/* ¼��ʱ�䳤�� */
            public uint dwPreRecordTime;/* Ԥ¼ʱ�� 0-��Ԥ¼ 1-5�� 2-10�� 3-15�� 4-20�� 5-25�� 6-30�� 7-0xffffffff(������Ԥ¼) */
        }

        //��̨Э����ṹ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZ_PROTOCOL
        {
            public uint dwType;/*����������ֵ����1��ʼ��������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DESC_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDescribe;/*������������������8000�е�һ��*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PTZ_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PTZ_PROTOCOL[] struPtz;/*���200��PTZЭ��*/
            public uint dwPtzNum;/*��Ч��ptzЭ����Ŀ����0��ʼ(������ʱ��1)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /***************************��̨����(end)******************************/

        //ͨ��������(��̨)��������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG_V30
        {
            public uint dwSize;
            public uint dwBaudRate;//������(bps)��0��50��1��75��2��110��3��150��4��300��5��600��6��1200��7��2400��8��4800��9��9600��10��19200�� 11��38400��12��57600��13��76800��14��115.2k;
            public byte byDataBit;// �����м�λ 0��5λ��1��6λ��2��7λ��3��8λ;
            public byte byStopBit;// ֹͣλ 0��1λ��1��2λ
            public byte byParity;// У�� 0����У�飬1����У�飬2��żУ��;
            public byte byFlowcontrol;// 0���ޣ�1��������,2-Ӳ����
            public ushort wDecoderType;//����������, ��0��ʼ����ӦptzЭ���б�
            public ushort wDecoderAddress;/*��������ַ:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* Ԥ�õ��Ƿ�����,0-û������,1-����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* Ѳ���Ƿ�����: 0-û������,1-���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* �켣�Ƿ�����,0-û������,1-����*/
        }

        //ͨ��������(��̨)��������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG
        {
            public uint dwSize;
            public uint dwBaudRate; //������(bps)��0��50��1��75��2��110��3��150��4��300��5��600��6��1200��7��2400��8��4800��9��9600��10��19200�� 11��38400��12��57600��13��76800��14��115.2k;
            public byte byDataBit; // �����м�λ 0��5λ��1��6λ��2��7λ��3��8λ;
            public byte byStopBit;// ֹͣλ 0��1λ��1��2λ;
            public byte byParity; // У�� 0����У�飬1����У�飬2��żУ��;
            public byte byFlowcontrol;// 0���ޣ�1��������,2-Ӳ����
            public ushort wDecoderType;//����������, 0��YouLi��1��LiLin-1016��2��LiLin-820��3��Pelco-p��4��DM DynaColor��5��HD600��6��JC-4116��7��Pelco-d WX��8��Pelco-d PICO
            public ushort wDecoderAddress;/*��������ַ:0 - 255*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;/* Ԥ�õ��Ƿ�����,0-û������,1-����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;/* Ѳ���Ƿ�����: 0-û������,1-���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;/* �켣�Ƿ�����,0-û������,1-����*/
        }

        //ppp��������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG_V30
        {
            public NET_DVR_IPADDR struRemoteIP;//Զ��IP��ַ
            public NET_DVR_IPADDR struLocalIP;//����IP��ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//����IP��ַ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            public byte byPPPMode;//PPPģʽ, 0��������1������
            public byte byRedial;//�Ƿ�ز� ��0-��,1-��
            public byte byRedialMode;//�ز�ģʽ,0-�ɲ�����ָ��,1-Ԥ�ûز�����
            public byte byDataEncrypt;//���ݼ���,0-��,1-��
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//�绰����
        }

        //ppp��������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteIP;//Զ��IP��ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIP;//����IP��ַ
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;//����IP��ַ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            public byte byPPPMode;//PPPģʽ, 0��������1������
            public byte byRedial;//�Ƿ�ز� ��0-��,1-��
            public byte byRedialMode;//�ز�ģʽ,0-�ɲ�����ָ��,1-Ԥ�ûز�����
            public byte byDataEncrypt;//���ݼ���,0-��,1-��
            public uint dwMTU;//MTU
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;//�绰����
        }

        //RS232���ڲ�������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_RS232
        {
            public uint dwBaudRate;/*������(bps)��0��50��1��75��2��110��3��150��4��300��5��600��6��1200��7��2400��8��4800��9��9600��10��19200�� 11��38400��12��57600��13��76800��14��115.2k;*/
            public byte byDataBit;/* �����м�λ 0��5λ��1��6λ��2��7λ��3��8λ */
            public byte byStopBit;/* ֹͣλ 0��1λ��1��2λ */
            public byte byParity;/* У�� 0����У�飬1����У�飬2��żУ�� */
            public byte byFlowcontrol;/* 0���ޣ�1��������,2-Ӳ���� */
            public uint dwWorkMode; /* ����ģʽ��0��232��������PPP���ţ�1��232�������ڲ������ƣ�2��͸��ͨ�� */
        }

        //RS232���ڲ�������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG_V30
        {
            public uint dwSize;
            public NET_DVR_SINGLE_RS232 struRs232;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 84, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PPPCFG_V30 struPPPConfig;
        }

        //RS232���ڲ�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG
        {
            public uint dwSize;
            public uint dwBaudRate;//������(bps)��0��50��1��75��2��110��3��150��4��300��5��600��6��1200��7��2400��8��4800��9��9600��10��19200�� 11��38400��12��57600��13��76800��14��115.2k;
            public byte byDataBit;// �����м�λ 0��5λ��1��6λ��2��7λ��3��8λ;
            public byte byStopBit;// ֹͣλ 0��1λ��1��2λ;
            public byte byParity;// У�� 0����У�飬1����У�飬2��żУ��;
            public byte byFlowcontrol;// 0���ޣ�1��������,2-Ӳ����
            public uint dwWorkMode;// ����ģʽ��0��խ������(232��������PPP����)��1������̨(232�������ڲ�������)��2��͸��ͨ��
            public NET_DVR_PPPCFG struPPPConfig;
        }

        //���������������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* ���� */
            public byte byAlarmType; //����������,0������,1������
            public byte byAlarmInHandle; /* �Ƿ��� 0-������ 1-����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_HANDLEEXCEPTION_V30 struAlarmHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//����������¼��ͨ��,Ϊ1��ʾ������ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* �Ƿ����Ԥ�õ� 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* ���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ㡣*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* �Ƿ����Ѳ�� 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* Ѳ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* �Ƿ���ù켣 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* ���õ���̨�Ĺ켣��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        //���������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;/* ���� */
            public byte byAlarmType;//����������,0������,1������
            public byte byAlarmInHandle;/* �Ƿ��� 0-������ 1-����*/
            public NET_DVR_HANDLEEXCEPTION struAlarmHandleType;/* ������ʽ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//����������¼��ͨ��,Ϊ1��ʾ������ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;/* �Ƿ����Ԥ�õ� 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* ���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ㡣*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;/* �Ƿ����Ѳ�� 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;/* Ѳ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;/* �Ƿ���ù켣 0-��,1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;/* ���õ���̨�Ĺ켣��� */
        }

        //�ϴ�������Ϣ(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V30
        {
            public int dwAlarmType;/*0-�ź�������,1-Ӳ����,2-�źŶ�ʧ,3���ƶ����,4��Ӳ��δ��ʽ��,5-��дӲ�̳���,6-�ڵ�����,7-��ʽ��ƥ��, 8-�Ƿ�����, 0xa-GPS��λ��Ϣ(���ض���)*/
            public int dwAlarmInputNumber;/*��������˿�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutputNumber;/*����������˿ڣ�Ϊ1��ʾ��Ӧ���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmRelateChannel;/*������¼��ͨ����Ϊ1��ʾ��Ӧ¼��, dwAlarmRelateChannel[0]��Ӧ��1��ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;/*dwAlarmTypeΪ2��3,6ʱ����ʾ�ĸ�ͨ����dwChannel[0]��Ӧ��1��ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byDiskNumber;/*dwAlarmTypeΪ1,4,5ʱ,��ʾ�ĸ�Ӳ��, dwDiskNumber[0]��Ӧ��1��Ӳ��*/
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

        //�ź�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_TRIGER_ALARM
        {
            //�����̶�����
            public uint dwAlarmType;//��������,��Ϊ0-�ź�������
            public NET_DVR_TIME_EX struAlarmTime;    //����������ʱ��
            public uint dwAlarmInputNo;        //���������ı�������ͨ���ţ�һ��ֻ��һ��
            public uint dwTrigerAlarmOutNum;    /*�����ı���������������ں������䳤���ݲ��������д����ı������ͨ���ţ����ֽڱ�ʾһ��*/
            public uint dwTrigerRecordChanNum;    /*������¼��ͨ�����������ں������䳤���ݲ��������д�����¼��ͨ���ţ����ֽڱ�ʾһ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
            public IntPtr pAlarmData;    //�����ɱ䲿������
        }

        //�ƶ������ر���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_CHAN_ALARM
        {
            //�����̶�����
            public uint dwAlarmType;//��������,2-�źŶ�ʧ��3���ƶ����,6-�ڵ�������9-��Ƶ�ź��쳣��10-¼���쳣��13-ǰ��/¼��ֱ��ʲ�ƥ��
            public NET_DVR_TIME_EX struAlarmTime;    //����������ʱ��
            public uint dwAlarmChanNum;    /*��������ͨ�����ݸ��������ں������䳤���ݲ��������з����ı���ͨ���ţ����ֽڱ�ʾһ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
            public IntPtr pAlarmData;    //�����ɱ䲿������
        }

        //Ӳ����ر���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_HARDDISK_ALARM
        {
            //�����̶�����
            public uint dwAlarmType;//��������,1-Ӳ������4��Ӳ��δ��ʽ��,5-дӲ�̳���,
            public NET_DVR_TIME_EX struAlarmTime;    //����������ʱ��
            public uint dwAlarmHardDiskNum;    /*����������Ӳ�����ݳ��ȣ����ں������䳤���ݲ��������з���������Ӳ�̺ţ��Ľڱ�ʾһ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
            public IntPtr pAlarmData;    //�����ɱ䲿������
        }

        //¼��������ر���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_RECORDHOST_ALARM
        {
            //�����̶�����
            public uint dwAlarmType;//��������,17-¼����������,
            public NET_DVR_TIME_EX struAlarmTime;    //����������ʱ��
            public byte bySubAlarmType;  //���������ͣ�1-һ���ӳ�¼�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;/* ���� */
            public NET_DVR_TIME_EX struRecordEndTime; //¼������ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
            public IntPtr pAlarmData;    //�����ɱ䲿������
        }

        //��������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_OTHER_ALARM
        {
            //�����̶�����
            public uint dwAlarmType;//��������,7-��ʽ��ƥ��, 8-�Ƿ����ʣ�11-���ܳ����仯��12-�����쳣��14-���������Դʧ��,15-������ⱨ��, 16-�ȱ��쳣��18-�����Խ����󱨾�
            public NET_DVR_TIME_EX struAlarmTime;    //����������ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
            public IntPtr pAlarmData;    //�����ɱ䲿������
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO
        {
            public int dwAlarmType;/*0-�ź�������,1-Ӳ����,2-�źŶ�ʧ,3���ƶ����,4��Ӳ��δ��ʽ��,5-��дӲ�̳���,6-�ڵ�����,7-��ʽ��ƥ��, 8-�Ƿ�����, 9-����״̬, 0xa-GPS��λ��Ϣ(���ض���)*/
            public int dwAlarmInputNumber;/*��������˿�, ����������Ϊ9ʱ�ñ�����ʾ����״̬0��ʾ������ -1��ʾ����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmOutputNumber;/*����������˿ڣ���һλΪ1��ʾ��Ӧ��һ�����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmRelateChannel;/*������¼��ͨ������һλΪ1��ʾ��Ӧ��һ·¼��, dwAlarmRelateChannel[0]��Ӧ��1��ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwChannel;/*dwAlarmTypeΪ2��3,6ʱ����ʾ�ĸ�ͨ����dwChannel[0]λ��Ӧ��1��ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwDiskNumber;/*dwAlarmTypeΪ1,4,5ʱ,��ʾ�ĸ�Ӳ��, dwDiskNumber[0]λ��Ӧ��1��Ӳ��*/
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
        //IPC�����������
        /* IP�豸�ṹ */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO
        {
            public uint dwEnable;/* ��IP�豸�Ƿ����� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword; /* ���� */
            public NET_DVR_IPADDR struIP;/* IP��ַ */
            public ushort wDVRPort;/* �˿ں� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */

            public void Init()
            {
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byRes = new byte[34];
            }
        }

        //ipc�����豸��Ϣ��չ��֧��ip�豸����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO_V31
        {
            public byte byEnable;//��IP�豸�Ƿ���Ч
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//�����ֶΣ���0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;//�û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;//����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byDomain;//�豸����
            public NET_DVR_IPADDR struIP;//IP��ַ
            public ushort wDVRPort;// �˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//�����ֶΣ���0
        }

        /* IPͨ��ƥ����� */
        //sizeof(NET_DVR_IPCHANINFO) == 492
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO
        {
            public byte byEnable;/* ��ͨ���Ƿ����� */
            public byte byIPID;/* IP�豸ID ȡֵ1- MAX_IP_DEVICE */
            public byte byChannel;/* ͨ���� */
            public byte byIPIDHigh;//IP�豸ID�ĸ�8λ
            public byte byProType;//Э�����ͣ�0-����Э��(default)��1-����Э�飬2-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 487, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����,��0
        }

        /* IP�������ýṹ */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG
        {
            public uint dwSize;/* �ṹ��С */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;/* IP�豸 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* ģ��ͨ���Ƿ����ã��ӵ͵��߱�ʾ1-32ͨ����0��ʾ��Ч 1��Ч */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IPͨ�� */
        }

        /* ��չIP�������ýṹ */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V31
        {
            public uint dwSize;/* �ṹ��С */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP�豸 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* ģ��ͨ���Ƿ����ã��ӵ͵��߱�ʾ1-32ͨ����0��ʾ��Ч 1��Ч */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IPͨ�� */
        }

        /* ����������� */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO
        {
            public byte byIPID;/* IP�豸IDȡֵ1- MAX_IP_DEVICE */
            public byte byAlarmOut;/* ��������� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */

            public void Init()
            {
                byRes = new byte[18];
            }
        }

        /* IP����������ýṹ */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG
        {
            public uint dwSize; /* �ṹ��С */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP������� */

            public void Init()
            {
                struIPAlarmOutInfo = new NET_DVR_IPALARMOUTINFO[MAX_IP_ALARMOUT];
                for (int i = 0; i < MAX_IP_ALARMOUT; i++)
                {
                    struIPAlarmOutInfo[i].Init();
                }
            }
        }

        /* ����������� */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO
        {
            public byte byIPID;/* IP�豸IDȡֵ1- MAX_IP_DEVICE */
            public byte byAlarmIn;/* ��������� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;/* ���� */
        }

        /* IP�����������ýṹ */
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG
        {
            public uint dwSize;/* �ṹ��С */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP�������� */
        }

        //ipc alarm info
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo; /* IP�豸 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; /* ģ��ͨ���Ƿ����ã�0-δ���� 1-���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IPͨ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;/* IP�������� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP������� */
        }

        //ipc���øı䱨����Ϣ��չ 9000_1.1
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO_V31
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; /* IP�豸 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;/* ģ��ͨ���Ƿ����ã�0-δ���� 1-���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;/* IPͨ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo; /* IP�������� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;/* IP������� */
        }

        public enum HD_STAT
        {
            HD_STAT_OK = 0,/* ���� */
            HD_STAT_UNFORMATTED = 1,/* δ��ʽ�� */
            HD_STAT_ERROR = 2,/* ���� */
            HD_STAT_SMART_FAILED = 3,/* SMART״̬ */
            HD_STAT_MISMATCH = 4,/* ��ƥ�� */
            HD_STAT_IDLE = 5, /* ����*/
            NET_HD_STAT_OFFLINE = 6,/*�����̴���δ����״̬ */
        }

        //����Ӳ����Ϣ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HD
        {
            public uint dwHDNo;/*Ӳ�̺�, ȡֵ0~MAX_DISKNUM_V30-1*/
            public uint dwCapacity;/*Ӳ������(��������)*/
            public uint dwFreeSpace;/*Ӳ��ʣ��ռ�(��������)*/
            public uint dwHdStatus;/*Ӳ��״̬(��������) HD_STAT*/
            public byte byHDAttr;/*0-Ĭ��, 1-����; 2-ֻ��*/
            public byte byHDType;/*0-����Ӳ��,1-ESATAӲ��,2-NASӲ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwHdGroup; /*�����ĸ����� 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDCFG
        {
            public uint dwSize;
            public uint dwHDCount;/*Ӳ����(��������)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HD[] struHDInfo;//Ӳ����ز�������Ҫ����������Ч��
        }

        //����������Ϣ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HDGROUP
        {
            public uint dwHDGroupNo;/*�����(��������) 1-MAX_HD_GROUP*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byHDGroupChans;/*�����Ӧ��¼��ͨ��, 0-��ʾ��ͨ����¼�󵽸����飬1-��ʾ¼�󵽸�����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDGROUP_CFG
        {
            public uint dwSize;
            public uint dwHDGroupCount;/*��������(��������)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HD_GROUP, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HDGROUP[] struHDGroupAttr;//Ӳ����ز�������Ҫ����������Ч
        }

        //�������Ų����Ľṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCALECFG
        {
            public uint dwSize;
            public uint dwMajorScale;/* ����ʾ 0-�����ţ�1-����*/
            public uint dwMinorScale;/* ����ʾ 0-�����ţ�1-����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //DVR�������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* ���� */
            public uint dwAlarmOutDelay;/* �������ʱ��(-1Ϊ���ޣ��ֶ��ر�) */
            //0-5��,1-10��,2-30��,3-1����,4-2����,5-5����,6-10����,7-�ֶ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* �����������ʱ��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;/* ���� */
            public uint dwAlarmOutDelay;/* �������ʱ��(-1Ϊ���ޣ��ֶ��ر�) */
            //0-5��,1-10��,2-30��,3-1����,4-2����,5-5����,6-10����,7-�ֶ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;/* �����������ʱ��� */
        }

        //DVR����Ԥ������(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG_V30
        {
            public uint dwSize;
            public byte byPreviewNumber;//Ԥ����Ŀ,0-1����,1-4����,2-9����,3-16����,0xff:�����
            public byte byEnableAudio;//�Ƿ�����Ԥ��,0-��Ԥ��,1-Ԥ��
            public ushort wSwitchTime;//�л�ʱ��,0-���л�,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PREVIEW_MODE * MAX_WINDOW_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//�л�˳��,���lSwitchSeq[i]Ϊ 0xff��ʾ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR����Ԥ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG
        {
            public uint dwSize;
            public byte byPreviewNumber;//Ԥ����Ŀ,0-1����,1-4����,2-9����,3-16����,0xff:�����
            public byte byEnableAudio;//�Ƿ�����Ԥ��,0-��Ԥ��,1-Ԥ��
            public ushort wSwitchTime;//�л�ʱ��,0-���л�,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;//�л�˳��,���lSwitchSeq[i]Ϊ 0xff��ʾ����
        }

        //DVR��Ƶ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VGAPARA
        {
            public ushort wResolution;/* �ֱ��� */
            public ushort wFreq;/* ˢ��Ƶ�� */
            public uint dwBrightness;/* ���� */
        }

        //MATRIX��������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.U2)]
            public ushort[] wOrder;/* Ԥ��˳��, 0xff��ʾ��Ӧ�Ĵ��ڲ�Ԥ�� */
            public ushort wSwitchTime;// Ԥ���л�ʱ�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA
        {
            public ushort wDisplayLogo;/* ��ʾ��Ƶͨ���� */
            public ushort wDisplayOsd;/* ��ʾʱ�� */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VOOUT
        {
            public byte byVideoFormat;/* �����ʽ,0-PAL,1-NTSC */
            public byte byMenuAlphaValue;/* �˵��뱳��ͼ��Աȶ� */
            public ushort wScreenSaveTime;/* ��Ļ����ʱ�� 0-�Ӳ�,1-1����,2-2����,3-5����,4-10����,5-20����,6-30���� */
            public ushort wVOffset;/* ��Ƶ���ƫ�� */
            public ushort wBrightness;/* ��Ƶ������� */
            public byte byStartMode;/* ��������Ƶ���ģʽ(0:�˵�,1:Ԥ��)*/
            public byte byEnableScaler;/* �Ƿ��������� (0-������, 1-����)*/
        }

        //DVR��Ƶ���(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;/* VGA���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MATRIXOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIXPARA_V30[] struMatrixPara;/* MATRIX���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR��Ƶ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;    /* VGA���� */
            public NET_DVR_MATRIXPARA struMatrixPara;/* MATRIX���� */
        }

        //���û�����(�ӽṹ)(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_INFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRight;/* ����Ȩ�� */
            /*����0: ���ؿ�����̨*/
            /*����1: �����ֶ�¼��*/
            /*����2: ���ػط�*/
            /*����3: �������ò���*/
            /*����4: ���ز鿴״̬����־*/
            /*����5: ���ظ߼�����(��������ʽ�����������ػ�)*/
            /*����6: ���ز鿴���� */
            /*����7: ���ع���ģ���IP camera */
            /*����8: ���ر��� */
            /*����9: ���عػ�/���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemoteRight;/* Զ��Ȩ�� */
            /*����0: Զ�̿�����̨*/
            /*����1: Զ���ֶ�¼��*/
            /*����2: Զ�̻ط� */
            /*����3: Զ�����ò���*/
            /*����4: Զ�̲鿴״̬����־*/
            /*����5: Զ�̸߼�����(��������ʽ�����������ػ�)*/
            /*����6: Զ�̷��������Խ�*/
            /*����7: Զ��Ԥ��*/
            /*����8: Զ�����󱨾��ϴ����������*/
            /*����9: Զ�̿��ƣ��������*/
            /*����10: Զ�̿��ƴ���*/
            /*����11: Զ�̲鿴���� */
            /*����12: Զ�̹���ģ���IP camera */
            /*����13: Զ�̹ػ�/���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPreviewRight;/* Զ�̿���Ԥ����ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPlaybackRight;/* ���ؿ��Իطŵ�ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPlaybackRight;/* Զ�̿��Իطŵ�ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRecordRight;/* ���ؿ���¼���ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetRecordRight;/* Զ�̿���¼���ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPTZRight;/* ���ؿ���PTZ��ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPTZRight;/* Զ�̿���PTZ��ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalBackupRight;/* ���ر���Ȩ��ͨ�� 0-��Ȩ�ޣ�1-��Ȩ��*/
            public NET_DVR_IPADDR struUserIP;/* �û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* ������ַ */
            public byte byPriority;/* ���ȼ���0xff-�ޣ�0--�ͣ�1--�У�2--�� */
            /*
            �ޡ�����ʾ��֧�����ȼ�������
            �͡���Ĭ��Ȩ��:�������غ�Զ�̻ط�,���غ�Զ�̲鿴��־��״̬,���غ�Զ�̹ػ�/����
            �С����������غ�Զ�̿�����̨,���غ�Զ���ֶ�¼��,���غ�Զ�̻ط�,�����Խ���Զ��Ԥ��
                  ���ر���,����/Զ�̹ػ�/����
            �ߡ�������Ա
            */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //���û�����(SDK_V15��չ)(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO_EX
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* Ȩ�� */
            /*����0: ���ؿ�����̨*/
            /*����1: �����ֶ�¼��*/
            /*����2: ���ػط�*/
            /*����3: �������ò���*/
            /*����4: ���ز鿴״̬����־*/
            /*����5: ���ظ߼�����(��������ʽ�����������ػ�)*/
            public uint dwLocalPlaybackRight;/* ���ؿ��Իطŵ�ͨ�� bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* Ȩ�� */
            /*����0: Զ�̿�����̨*/
            /*����1: Զ���ֶ�¼��*/
            /*����2: Զ�̻ط� */
            /*����3: Զ�����ò���*/
            /*����4: Զ�̲鿴״̬����־*/
            /*����5: Զ�̸߼�����(��������ʽ�����������ػ�)*/
            /*����6: Զ�̷��������Խ�*/
            /*����7: Զ��Ԥ��*/
            /*����8: Զ�����󱨾��ϴ����������*/
            /*����9: Զ�̿��ƣ��������*/
            /*����10: Զ�̿��ƴ���*/
            public uint dwNetPreviewRight;/* Զ�̿���Ԥ����ͨ�� bit0 -- channel 1*/
            public uint dwNetPlaybackRight;/* Զ�̿��Իطŵ�ͨ�� bit0 -- channel 1*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* �û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* ������ַ */
        }

        //���û�����(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;/* Ȩ�� */
            /*����0: ���ؿ�����̨*/
            /*����1: �����ֶ�¼��*/
            /*����2: ���ػط�*/
            /*����3: �������ò���*/
            /*����4: ���ز鿴״̬����־*/
            /*����5: ���ظ߼�����(��������ʽ�����������ػ�)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;/* Ȩ�� */
            /*����0: Զ�̿�����̨*/
            /*����1: Զ���ֶ�¼��*/
            /*����2: Զ�̻ط� */
            /*����3: Զ�����ò���*/
            /*����4: Զ�̲鿴״̬����־*/
            /*����5: Զ�̸߼�����(��������ʽ�����������ػ�)*/
            /*����6: Զ�̷��������Խ�*/
            /*����7: Զ��Ԥ��*/
            /*����8: Զ�����󱨾��ϴ����������*/
            /*����9: Զ�̿��ƣ��������*/
            /*����10: Զ�̿��ƴ���*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;/* �û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ) */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/* ������ַ */
        }

        //DVR�û�����(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_V30[] struUser;
        }

        //DVR�û�����(SDK_V15��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_EX[] struUser;
        }

        //DVR�û�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO[] struUser;
        }

        //DVR�쳣����(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION_V30[] struExceptionHandleType;
            /*����0-����,1- Ӳ�̳���,2-���߶�,3-��������IP ��ַ��ͻ, 4-�Ƿ�����, 5-����/�����Ƶ��ʽ��ƥ��, 6-��Ƶ�ź��쳣, 7-¼���쳣*/
        }

        //DVR�쳣����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION[] struExceptionHandleType;
            /*����0-����,1- Ӳ�̳���,2-���߶�,3-��������IP ��ַ��ͻ,4-�Ƿ�����, 5-����/�����Ƶ��ʽ��ƥ��, 6-��Ƶ�ź��쳣*/
        }

        //ͨ��״̬(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE_V30
        {
            public byte byRecordStatic;//ͨ���Ƿ���¼��,0-��¼��,1-¼��
            public byte bySignalStatic;//���ӵ��ź�״̬,0-����,1-�źŶ�ʧ
            public byte byHardwareStatic;//ͨ��Ӳ��״̬,0-����,1-�쳣,����DSP����
            public byte byRes1;//����
            public uint dwBitRate;//ʵ������
            public uint dwLinkNum;//�ͻ������ӵĸ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struClientIP;//�ͻ��˵�IP��ַ
            public uint dwIPLinkNum;//�����ͨ��ΪIP���룬��ô��ʾIP���뵱ǰ��������
            public byte byExceedMaxLink;//�Ƿ񳬳��˵�·6·������ 0 - δ����, 1-���� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uint dwAllBitRate;//���пͻ��˺͸�ͨ�����ӵ�ʵ������֮�� 
            public uint dwChannelNo;//��ǰ��ͨ���ţ�0xffffffff��ʾ��ǰ������ͨ����Ϣ��Ч
        }

        //ͨ��״̬
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE
        {
            public byte byRecordStatic;//ͨ���Ƿ���¼��,0-��¼��,1-¼��
            public byte bySignalStatic;//���ӵ��ź�״̬,0-����,1-�źŶ�ʧ
            public byte byHardwareStatic;//ͨ��Ӳ��״̬,0-����,1-�쳣,����DSP����
            public byte reservedData;//����
            public uint dwBitRate;//ʵ������
            public uint dwLinkNum;//�ͻ������ӵĸ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.U4)]
            public uint[] dwClientIP;//�ͻ��˵�IP��ַ
        }

        //Ӳ��״̬
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISKSTATE
        {
            public uint dwVolume;//Ӳ�̵�����
            public uint dwFreeSpace;//Ӳ�̵�ʣ��ռ�
            public uint dwHardDiskStatic;//Ӳ�̵�״̬,0-�,1-����,2-������
        }

        //DVR����״̬(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V30
        {
            public uint dwDeviceStatic;//�豸��״̬,0-����,1-CPUռ����̫��,����85%,2-Ӳ������,���紮������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;//ͨ����״̬
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//�����˿ڵ�״̬,0-û�б���,1-�б���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//��������˿ڵ�״̬,0-û�����,1-�б������
            public uint dwLocalDisplay;//������ʾ״̬,0-����,1-������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;//��ʾ����ͨ����״̬ 0-δʹ�ã�1-ʹ����, 0xff��Ч
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //DVR����״̬
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE
        {
            public uint dwDeviceStatic;//�豸��״̬,0-����,1-CPUռ����̫��,����85%,2-Ӳ������,���紮������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE[] struChanStatic;//ͨ����״̬
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;//�����˿ڵ�״̬,0-û�б���,1-�б���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;//��������˿ڵ�״̬,0-û�����,1-�б������
            public uint dwLocalDisplay;//������ʾ״̬,0-����,1-������

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM];
                struChanStatic = new NET_DVR_CHANNELSTATE[MAX_CHANNUM];
                byAlarmInStatic = new byte[MAX_ALARMIN];
                byAlarmOutStatic = new byte[MAX_ALARMOUT];
            }
        }

        /************************DVR��־ begin***************************/
        /* ���� */
        //������
        public const int MAJOR_ALARM = 1;
        //������
        public const int MINOR_ALARM_IN = 1;/* �������� */
        public const int MINOR_ALARM_OUT = 2;/* ������� */
        public const int MINOR_MOTDET_START = 3; /* �ƶ���ⱨ����ʼ */
        public const int MINOR_MOTDET_STOP = 4; /* �ƶ���ⱨ������ */
        public const int MINOR_HIDE_ALARM_START = 5;/* �ڵ�������ʼ */
        public const int MINOR_HIDE_ALARM_STOP = 6;/* �ڵ��������� */
        public const int MINOR_VCA_ALARM_START = 7;/*���ܱ�����ʼ*/
        public const int MINOR_VCA_ALARM_STOP = 8;/*���ܱ���ֹͣ*/
        public const int MINOR_ITS_ALARM_START = 0x09; // ��ͨ�¼�������ʼ 
        public const int MINOR_ITS_ALARM_STOP = 0x0a; // ��ͨ�¼��������� 
        public const int MINOR_NETALARM_START = 0x0b; // ���籨����ʼ 
        public const int MINOR_NETALARM_STOP = 0x0c; // ���籨������ 
        public const int MINOR_NETALARM_RESUME = 0x0d; // ���籨���ָ� 
        public const int MINOR_WIRELESS_ALARM_START = 0x0e; // ���߱�����ʼ 
        public const int MINOR_WIRELESS_ALARM_STOP = 0x0f; // ���߱������� 
        public const int MINOR_PIR_ALARM_START = 0x10; // �����Ӧ������ʼ 
        public const int MINOR_PIR_ALARM_STOP = 0x11; // �����Ӧ�������� 
        public const int MINOR_CALLHELP_ALARM_START = 0x12; // ���ȱ�����ʼ 
        public const int MINOR_CALLHELP_ALARM_STOP = 0x13; // ���ȱ������� 
        public const int MINOR_DETECTFACE_ALARM_START = 0x16; // ������ⱨ����ʼ 
        public const int MINOR_DETECTFACE_ALARM_STOP = 0x17; // ������ⱨ������ 
        public const int MINOR_VQD_ALARM_START = 0x18; //VQD���� 
        public const int MINOR_VQD_ALARM_STOP = 0x19; //VQD�������� 
        public const int MINOR_VCA_SECNECHANGE_DETECTION = 0x1a; // ������ⱨ�� 
        public const int MINOR_SMART_REGION_EXITING_BEGIN = 0x1b; // �뿪������⿪ʼ 
        public const int MINOR_SMART_REGION_EXITING_END = 0x1c; // �뿪���������� 
        public const int MINOR_SMART_LOITERING_BEGIN = 0x1d; // �ǻ���⿪ʼ 
        public const int MINOR_SMART_LOITERING_END = 0x1e; // �ǻ������� 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_BEGIN = 0x20; // Խ����⿪ʼ 
        public const int MINOR_VCA_ALARM_LINE_DETECTION_END = 0x21; // Խ�������� 
        public const int MINOR_VCA_ALARM_INTRUDE_BEGIN = 0x22; // ����������⿪ʼ 
        public const int MINOR_VCA_ALARM_INTRUDE_END = 0x23; // �������������� 
        public const int MINOR_VCA_ALARM_AUDIOINPUT = 0x24; // ��Ƶ��ʧ��� 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL = 0x25; // ��Ƶ�쳣��� 
        public const int MINOR_VCA_DEFOCUS_DETECTION_BEGIN = 0x26; // �齹��⿪ʼ 
        public const int MINOR_VCA_DEFOCUS_DETECTION_END = 0x27; // �齹������
        public const int MINOR_EXT_ALARM = 0x28; // IPC�ⲿ����
        public const int MINOR_VCA_FACE_ALARM_BEGIN = 0x29; // ������⿪ʼ 
        public const int MINOR_SMART_REGION_ENTRANCE_BEGIN = 0x2a; // ����������⿪ʼ 
        public const int MINOR_SMART_REGION_ENTRANCE_END = 0x2b; // �������������� 
        public const int MINOR_SMART_PEOPLE_GATHERING_BEGIN = 0x2c; // ��Ա�ۼ���⿪ʼ 
        public const int MINOR_SMART_PEOPLE_GATHERING_END = 0x2d; // ��Ա�ۼ������� 
        public const int MINOR_SMART_FAST_MOVING_BEGIN = 0x2e; // �����˶���⿪ʼ 
        public const int MINOR_SMART_FAST_MOVING_END = 0x2f; // �����˶������� 
        public const int MINOR_VCA_FACE_ALARM_END = 0x30; // ���������� 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_BEGIN = 0x31; // ���������⿪ʼ 
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_END = 0x32; // ������������� 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_BEGIN = 0x33; // ��Ƶ��ʧ��⿪ʼ 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_END = 0x34; // ��Ƶ��ʧ������ 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_BEGIN = 0x35; // ��ǿͻ����⿪ʼ 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_END = 0x36; // ��ǿͻ�������� 

        public const int MINOR_VCA_LECTURE_DETECTION_BEGIN = 0x37;  //�ڿ���⿪ʼ����
        public const int MINOR_VCA_LECTURE_DETECTION_END = 0x38;  //�ڿ�����������
        public const int MINOR_VCA_ALARM_AUDIOSTEEPDROP = 0x39;  //��ǿ���� 2014-03-21
        public const int MINOR_VCA_ANSWER_DETECTION_BEGIN = 0x3a;  //�ش�������⿪ʼ����
        public const int MINOR_VCA_ANSWER_DETECTION_END = 0x3b;  //�ش���������������

        public const int MINOR_SMART_PARKING_BEGIN = 0x3c; // ͣ����⿪ʼ 
        public const int MINOR_SMART_PARKING_END = 0x3d; // ͣ�������� 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_BEGIN = 0x3e; // ��Ʒ������⿪ʼ 
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_END = 0x3f; // ��Ʒ���������� 
        public const int MINOR_SMART_OBJECT_REMOVAL_BEGIN = 0x40; // ��Ʒ��ȡ��⿪ʼ 
        public const int MINOR_SMART_OBJECT_REMOVAL_END = 0x41; // ��Ʒ��ȡ������ 
        public const int MINOR_SMART_VEHICLE_ALARM_START = 0x46;   //���Ƽ�⿪ʼ
        public const int MINOR_SMART_VEHICLE_ALARM_STOP = 0x47;   //���Ƽ�����
        public const int MINOR_THERMAL_FIREDETECTION = 0x48;   //�ȳ���������⿪ʼ
        public const int MINOR_THERMAL_FIREDETECTION_END = 0x49;   //�ȳ�������������
        public const int MINOR_SMART_VANDALPROOF_BEGIN = 0x50;   //���ƻ���⿪ʼ
        public const int MINOR_SMART_VANDALPROOF_END = 0x51; //���ƻ�������

        public const int MINOR_ALARMIN_SHORT_CIRCUIT = 0x400; // ������·���� 
        public const int MINOR_ALARMIN_BROKEN_CIRCUIT = 0x401; // ������·���� 
        public const int MINOR_ALARMIN_EXCEPTION = 0x402; // �����쳣���� 
        public const int MINOR_ALARMIN_RESUME = 0x403; // ���������ָ� 
        public const int MINOR_HOST_DESMANTLE_ALARM = 0x404; //�������𱨾�  
        public const int MINOR_HOST_DESMANTLE_RESUME = 0x405; // ��������ָ� 
        public const int MINOR_CARD_READER_DESMANTLE_ALARM = 0x406; //���������𱨾� 
        public const int MINOR_CARD_READER_DESMANTLE_RESUME = 0x407; // ����������ָ�  
        public const int MINOR_CASE_SENSOR_ALARM = 0x408; // �¼����뱨�� 
        public const int MINOR_CASE_SENSOR_RESUME = 0x409; // �¼�����ָ� 
        public const int MINOR_STRESS_ALARM = 0x40a; // в�ȱ��� 
        public const int MINOR_OFFLINE_ECENT_NEARLY_FULL = 0x40b; // �����¼���90%���� 
        public const int MINOR_CARD_MAX_AUTHENTICATE_FAIL = 0x40c; // ������֤ʧ�ܳ��α��� 
        public const int MINOR_SD_CARD_FULL = 0x40d;  //SD���洢������
        public const int MINOR_LINKAGE_CAPTURE_PIC = 0x40e;  //����ץ���¼�����

        /* �쳣 */
        //������
        public const int MAJOR_EXCEPTION = 2;
        //������
        public const int MINOR_VI_LOST = 33;/* ��Ƶ�źŶ�ʧ */
        public const int MINOR_ILLEGAL_ACCESS = 34;/* �Ƿ����� */
        public const int MINOR_HD_FULL = 35;/* Ӳ���� */
        public const int MINOR_HD_ERROR = 36;/* Ӳ�̴��� */
        public const int MINOR_DCD_LOST = 37;/* MODEM ����(������ʹ��) */
        public const int MINOR_IP_CONFLICT = 38;/* IP��ַ��ͻ */
        public const int MINOR_NET_BROKEN = 39;/* ����Ͽ�*/
        public const int MINOR_REC_ERROR = 40;/* ¼����� */
        public const int MINOR_IPC_NO_LINK = 41;/* IPC�����쳣 */
        public const int MINOR_VI_EXCEPTION = 42;/* ��Ƶ�����쳣(ֻ���ģ��ͨ��) */
        public const int MINOR_IPC_IP_CONFLICT = 43;/*ipc ip ��ַ ��ͻ*/
        public const int MINOR_RAID_ERROR = 0x20; // �����쳣  
        public const int MINOR_SENCE_EXCEPTION = 0x2c; // �����쳣 
        public const int MINOR_PIC_REC_ERROR = 0x2d; // ץͼ����,��ȡͼƬ�ļ�ʧ�� 
        public const int MINOR_VI_MISMATCH = 0x2e; // ��Ƶ��ʽ��ƥ�� 
        public const int MINOR_RESOLUTION_MISMATCH = 0x2f; // ����ֱ��ʺ�ǰ�˷ֱ��ʲ�ƥ�� 

        //2010-01-22 ������Ƶ�ۺ�ƽ̨�쳣��־������
        public const int MINOR_NET_ABNORMAL = 0x35; /*����״̬�쳣*/
        public const int MINOR_MEM_ABNORMAL = 0x36; /*�ڴ�״̬�쳣*/
        public const int MINOR_FILE_ABNORMAL = 0x37; /*�ļ�״̬�쳣*/
        public const int MINOR_PANEL_ABNORMAL = 0x38; /*ǰ��������쳣*/
        public const int MINOR_PANEL_RESUME = 0x39; /*ǰ���ָ�����*/
        public const int MINOR_RS485_DEVICE_ABNORMAL = 0x3a; /*RS485����״̬�쳣*/
        public const int MINOR_RS485_DEVICE_REVERT = 0x3b; /*RS485����״̬�쳣�ָ�*/
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALREBOOT = 0x3c; // �Ӱ��쳣���� 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALINSERT = 0x3d; // �Ӱ���� 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALPULLOUT = 0x3e; // �Ӱ�γ� 
        public const int MINOR_SCREEN_ABNARMALTEMPERATURE = 0x3f; // �¶��쳣 
        public const int MINOR_RECORD_OVERFLOW = 0x41; // ��������� 
        public const int MINOR_DSP_ABNORMAL = 0x42; // DSP�쳣 
        public const int MINOR_ANR_RECORD_FAIED = 0x43; // ANR¼��ʧ�� 
        public const int MINOR_SPARE_WORK_DEVICE_EXCEPT = 0x44; // �ȱ��豸�������쳣 
        public const int MINOR_START_IPC_MAS_FAILED = 0x45; // ����IPC MASʧ�� 
        public const int MINOR_IPCM_CRASH = 0x46; // IPCM�쳣���� 
        public const int MINOR_POE_POWER_EXCEPTION = 0x47; // POE�����쳣 
        public const int MINOR_UPLOAD_DATA_CS_EXCEPTION = 0x48; // �ƴ洢�����ϴ�ʧ�� 
        public const int MINOR_DIAL_EXCEPTION = 0x49;         /*�����쳣*/
        public const int MINOR_DEV_EXCEPTION_OFFLINE = 0x50;  //�豸�쳣����
        public const int MINOR_UPGRADEFAIL = 0x51; //Զ�������豸ʧ��
        public const int MINOR_AI_LOST = 0x52; /* ��Ƶ�źŶ�ʧ */

        public const int MINOR_DEV_POWER_ON = 0x400; // �豸�ϵ����� 
        public const int MINOR_DEV_POWER_OFF = 0x401; // �豸����ر� 
        public const int MINOR_WATCH_DOG_RESET = 0x402; // ���Ź���λ 
        public const int MINOR_LOW_BATTERY = 0x403; // ���ص�ѹ�� 
        public const int MINOR_BATTERY_RESUME = 0x404; // ���ص�ѹ�ָ����� 
        public const int MINOR_AC_OFF = 0x405; // ������ϵ� 
        public const int MINOR_AC_RESUME = 0x406; // ������ָ� 
        public const int MINOR_NET_RESUME = 0x407; // ����ָ� 
        public const int MINOR_FLASH_ABNORMAL = 0x408; // FLASH��д�쳣 
        public const int MINOR_CARD_READER_OFFLINE = 0x409; // ���������� 
        public const int MINOR_CARD_READER_RESUME = 0x40a; // ���������߻ָ� 
        public const int MINOR_SUBSYSTEM_IP_CONFLICT = 0x4000; // �Ӱ�IP��ͻ 
        public const int MINOR_SUBSYSTEM_NET_BROKEN = 0x4001; // �Ӱ���� 
        public const int MINOR_FAN_ABNORMAL = 0x4002; // �����쳣 
        public const int MINOR_BACKPANEL_TEMPERATURE_ABNORMAL = 0x4003; // �����¶��쳣 

        //��Ƶ�ۺ�ƽ̨
        public const int MINOR_FANABNORMAL = 49;/* ��Ƶ�ۺ�ƽ̨������״̬�쳣 */
        public const int MINOR_FANRESUME = 50;/* ��Ƶ�ۺ�ƽ̨������״̬�ָ����� */
        public const int MINOR_SUBSYSTEM_ABNORMALREBOOT = 51;/* ��Ƶ�ۺ�ƽ̨��6467�쳣���� */
        public const int MINOR_MATRIX_STARTBUZZER = 52;/* ��Ƶ�ۺ�ƽ̨��dm6467�쳣������������ */

        /* ���� */
        //������
        public const int MAJOR_OPERATION = 3;

        //������
        public const int MINOR_VCA_MOTIONEXCEPTION = 0x29; //��������쳣
        public const int MINOR_START_DVR = 0x41; // ���� 
        public const int MINOR_STOP_DVR = 0x42; // �ػ� 
        public const int MINOR_STOP_ABNORMAL = 0x43; // �쳣�ػ� 
        public const int MINOR_REBOOT_DVR = 0x44; // ���������豸 

        public const int MINOR_LOCAL_LOGIN = 0x50; // ���ص�½ 
        public const int MINOR_LOCAL_LOGOUT = 0x51; // ����ע����½ 
        public const int MINOR_LOCAL_CFG_PARM = 0x52; // �������ò��� 
        public const int MINOR_LOCAL_PLAYBYFILE = 0x53; // ���ذ��ļ��طŻ����� 
        public const int MINOR_LOCAL_PLAYBYTIME = 0x54; // ���ذ�ʱ��طŻ����� 
        public const int MINOR_LOCAL_START_REC = 0x55; // ���ؿ�ʼ¼�� 
        public const int MINOR_LOCAL_STOP_REC = 0x56; // ����ֹͣ¼�� 
        public const int MINOR_LOCAL_PTZCTRL = 0x57; // ������̨���� 
        public const int MINOR_LOCAL_PREVIEW = 0x58; // ����Ԥ��(������ʹ��) 
        public const int MINOR_LOCAL_MODIFY_TIME = 0x59; // �����޸�ʱ��(������ʹ��) 
        public const int MINOR_LOCAL_UPGRADE = 0x5a; // �������� 
        public const int MINOR_LOCAL_RECFILE_OUTPUT = 0x5b; // ���ر���¼���ļ� 
        public const int MINOR_LOCAL_FORMAT_HDD = 0x5c; // ���س�ʼ��Ӳ�� 
        public const int MINOR_LOCAL_CFGFILE_OUTPUT = 0x5d; // �������������ļ� 
        public const int MINOR_LOCAL_CFGFILE_INPUT = 0x5e; // ���뱾�������ļ� 
        public const int MINOR_LOCAL_COPYFILE = 0x5f; // ���ر����ļ� 
        public const int MINOR_LOCAL_LOCKFILE = 0x60; // ��������¼���ļ� 
        public const int MINOR_LOCAL_UNLOCKFILE = 0x61; // ���ؽ���¼���ļ� 
        public const int MINOR_LOCAL_DVR_ALARM = 0x62; // �����ֶ�����ʹ������� 
        public const int MINOR_IPC_ADD = 0x63; // ��������IPC 
        public const int MINOR_IPC_DEL = 0x64; // ����ɾ��IPC 
        public const int MINOR_IPC_SET = 0x65; // ��������IPC 
        public const int MINOR_LOCAL_START_BACKUP = 0x66; // ���ؿ�ʼ���� 
        public const int MINOR_LOCAL_STOP_BACKUP = 0x67; // ����ֹͣ���� 
        public const int MINOR_LOCAL_COPYFILE_START_TIME = 0x68; // ���ر��ݿ�ʼʱ�� 
        public const int MINOR_LOCAL_COPYFILE_END_TIME = 0x69; // ���ر��ݽ���ʱ�� 
        public const int MINOR_LOCAL_ADD_NAS = 0x6a; // ������������Ӳ�� 
        public const int MINOR_LOCAL_DEL_NAS = 0x6b; // ����ɾ��NAS�� 
        public const int MINOR_LOCAL_SET_NAS = 0x6c; // ��������NAS�� 
        public const int MINOR_LOCAL_RESET_PASSWD = 0x6d; /* ���ػָ�����ԱĬ������*/

        public const int MINOR_REMOTE_LOGIN = 0x70; // Զ�̵�¼ 
        public const int MINOR_REMOTE_LOGOUT = 0x71; // Զ��ע����½ 
        public const int MINOR_REMOTE_START_REC = 0x72; // Զ�̿�ʼ¼�� 
        public const int MINOR_REMOTE_STOP_REC = 0x73; // Զ��ֹͣ¼�� 
        public const int MINOR_START_TRANS_CHAN = 0x74; // ��ʼ͸������ 
        public const int MINOR_STOP_TRANS_CHAN = 0x75; // ֹͣ͸������ 
        public const int MINOR_REMOTE_GET_PARM = 0x76; // Զ�̻�ȡ���� 
        public const int MINOR_REMOTE_CFG_PARM = 0x77; // Զ�����ò��� 
        public const int MINOR_REMOTE_GET_STATUS = 0x78; // Զ�̻�ȡ״̬ 
        public const int MINOR_REMOTE_ARM = 0x79; // Զ�̲��� 
        public const int MINOR_REMOTE_DISARM = 0x7a; // Զ�̳��� 
        public const int MINOR_REMOTE_REBOOT = 0x7b; // Զ������ 
        public const int MINOR_START_VT = 0x7c; // ��ʼ�����Խ� 
        public const int MINOR_STOP_VT = 0x7d; // ֹͣ�����Խ� 
        public const int MINOR_REMOTE_UPGRADE = 0x7e; // Զ������ 
        public const int MINOR_REMOTE_PLAYBYFILE = 0x7f; // Զ�̰��ļ��ط� 
        public const int MINOR_REMOTE_PLAYBYTIME = 0x80; // Զ�̰�ʱ��ط� 
        public const int MINOR_REMOTE_PTZCTRL = 0x81; // Զ����̨���� 
        public const int MINOR_REMOTE_FORMAT_HDD = 0x82; // Զ�̸�ʽ��Ӳ�� 
        public const int MINOR_REMOTE_STOP = 0x83; // Զ�̹ػ� 
        public const int MINOR_REMOTE_LOCKFILE = 0x84; // Զ�������ļ� 
        public const int MINOR_REMOTE_UNLOCKFILE = 0x85; // Զ�̽����ļ� 
        public const int MINOR_REMOTE_CFGFILE_OUTPUT = 0x86; // Զ�̵��������ļ� 
        public const int MINOR_REMOTE_CFGFILE_INTPUT = 0x87; // Զ�̵��������ļ� 
        public const int MINOR_REMOTE_RECFILE_OUTPUT = 0x88; // Զ�̵���¼���ļ� 
        public const int MINOR_REMOTE_DVR_ALARM = 0x89; // Զ���ֶ�����ʹ������� 
        public const int MINOR_REMOTE_IPC_ADD = 0x8a; // Զ������IPC 
        public const int MINOR_REMOTE_IPC_DEL = 0x8b; // Զ��ɾ��IPC 
        public const int MINOR_REMOTE_IPC_SET = 0x8c; // Զ������IPC 
        public const int MINOR_REBOOT_VCA_LIB = 0x8d; // �������ܿ� 
        public const int MINOR_REMOTE_ADD_NAS = 0x8e; // Զ������NAS�� 
        public const int MINOR_REMOTE_DEL_NAS = 0x8f; // Զ��ɾ��NAS�� 

        public const int MINOR_REMOTE_SET_NAS = 0x90; // Զ������NAS�� 
        public const int MINOR_LOCAL_START_REC_CDRW = 0x91; // ���ؿ�ʼ��¼ 
        public const int MINOR_LOCAL_STOP_REC_CDRW = 0x92; // ����ֹͣ��¼ 
        public const int MINOR_REMOTE_START_REC_CDRW = 0x93; // Զ�̿�ʼ��¼ 
        public const int MINOR_REMOTE_STOP_REC_CDRW = 0x94; // Զ��ֹͣ��¼ 
        public const int MINOR_LOCAL_PIC_OUTPUT = 0x95; // ���ر���ͼƬ�ļ� 
        public const int MINOR_REMOTE_PIC_OUTPUT = 0x96; // Զ�̱���ͼƬ�ļ� 
        public const int MINOR_LOCAL_INQUEST_RESUME = 0x97; // ���ػָ���Ѷ�¼� 
        public const int MINOR_REMOTE_INQUEST_RESUME = 0x98; // Զ�ָ̻���Ѷ�¼� 
        public const int MINOR_LOCAL_ADD_FILE = 0x99; // ���ص����ļ� 
        public const int MINOR_REMOTE_DELETE_HDISK = 0x9a; // Զ��ɾ���쳣�����ڵ�Ӳ�� 
        public const int MINOR_REMOTE_LOAD_HDISK = 0x9b; // Զ�̼���Ӳ�� 
        public const int MINOR_REMOTE_UNLOAD_HDISK = 0x9c; // Զ��ж��Ӳ�� 
        public const int MINOR_LOCAL_OPERATE_LOCK = 0x9d; // ���ز������� 
        public const int MINOR_LOCAL_OPERATE_UNLOCK = 0x9e; // ���ز���������� 
        public const int MINOR_LOCAL_DEL_FILE = 0x9f; // ����ɾ����Ѷ�ļ� 

        public const int MINOR_SUBSYSTEMREBOOT = 0xa0; /*��Ƶ�ۺ�ƽ̨��dm6467 ��������*/
        public const int MINOR_MATRIX_STARTTRANSFERVIDEO = 0xa1; /*��Ƶ�ۺ�ƽ̨�������л���ʼ����ͼ��*/
        public const int MINOR_MATRIX_STOPTRANSFERVIDEO = 0xa2; /*��Ƶ�ۺ�ƽ̨�������л�ֹͣ����ͼ��*/
        public const int MINOR_REMOTE_SET_ALLSUBSYSTEM = 0xa3; /*��Ƶ�ۺ�ƽ̨����������6467��ϵͳ��Ϣ*/
        public const int MINOR_REMOTE_GET_ALLSUBSYSTEM = 0xa4; /*��Ƶ�ۺ�ƽ̨����ȡ����6467��ϵͳ��Ϣ*/
        public const int MINOR_REMOTE_SET_PLANARRAY = 0xa5; /*��Ƶ�ۺ�ƽ̨�����üƻ���Ѳ��*/
        public const int MINOR_REMOTE_GET_PLANARRAY = 0xa6; /*��Ƶ�ۺ�ƽ̨����ȡ�ƻ���Ѳ��*/
        public const int MINOR_MATRIX_STARTTRANSFERAUDIO = 0xa7; /*��Ƶ�ۺ�ƽ̨�������л���ʼ������Ƶ*/
        public const int MINOR_MATRIX_STOPRANSFERAUDIO = 0xa8; /*��Ƶ�ۺ�ƽ̨�������л�ֹͣ������Ƶ*/
        public const int MINOR_LOGON_CODESPITTER = 0xa9; /*��Ƶ�ۺ�ƽ̨����½�����*/
        public const int MINOR_LOGOFF_CODESPITTER = 0xaa; /*��Ƶ�ۺ�ƽ̨���˳������*/

        //KY2013 3.0.0
        public const int MINOR_LOCAL_MAIN_AUXILIARY_PORT_SWITCH = 0X302; //�����������л�
        public const int MINOR_LOCAL_HARD_DISK_CHECK = 0x303; //��������Ӳ���Լ�
        //2010-01-22 ������Ƶ�ۺ�ƽ̨�н�����������־
        public const int MINOR_START_DYNAMIC_DECODE = 0xb; /*��ʼ��̬����*/
        public const int MINOR_STOP_DYNAMIC_DECODE = 0xb1; /*ֹͣ��̬����*/
        public const int MINOR_GET_CYC_CFG = 0xb2; /*��ȡ������ͨ����Ѳ����*/
        public const int MINOR_SET_CYC_CFG = 0xb3; /*���ý���ͨ����Ѳ����*/
        public const int MINOR_START_CYC_DECODE = 0xb4; /*��ʼ��Ѳ����*/
        public const int MINOR_STOP_CYC_DECODE = 0xb5; /*ֹͣ��Ѳ����*/
        public const int MINOR_GET_DECCHAN_STATUS = 0xb6; /*��ȡ����ͨ��״̬*/
        public const int MINOR_GET_DECCHAN_INFO = 0xb7; /*��ȡ����ͨ����ǰ��Ϣ*/
        public const int MINOR_START_PASSIVE_DEC = 0xb8; /*��ʼ��������*/
        public const int MINOR_STOP_PASSIVE_DEC = 0xb9; /*ֹͣ��������*/
        public const int MINOR_CTRL_PASSIVE_DEC = 0xba; /*���Ʊ�������*/
        public const int MINOR_RECON_PASSIVE_DEC = 0xbb; /*������������*/
        public const int MINOR_GET_DEC_CHAN_SW = 0xbc; /*��ȡ����ͨ���ܿ���*/
        public const int MINOR_SET_DEC_CHAN_SW = 0xbd; /*���ý���ͨ���ܿ���*/
        public const int MINOR_CTRL_DEC_CHAN_SCALE = 0xbe; /*����ͨ�����ſ���*/
        public const int MINOR_SET_REMOTE_REPLAY = 0xbf; /*����Զ�̻ط�*/
        public const int MINOR_GET_REMOTE_REPLAY = 0xc0; /*��ȡԶ�̻ط�״̬*/
        public const int MINOR_CTRL_REMOTE_REPLAY = 0xc1; /*Զ�̻طſ���*/
        public const int MINOR_SET_DISP_CFG = 0xc2; /*������ʾͨ��*/
        public const int MINOR_GET_DISP_CFG = 0xc3; /*��ȡ��ʾͨ������*/
        public const int MINOR_SET_PLANTABLE = 0xc4; /*���üƻ���Ѳ��*/
        public const int MINOR_GET_PLANTABLE = 0xc5;/*��ȡ�ƻ���Ѳ��*/
        public const int MINOR_START_PPPPOE = 0xc6; /*��ʼPPPoE����*/
        public const int MINOR_STOP_PPPPOE = 0xc7; /*����PPPoE����*/
        public const int MINOR_UPLOAD_LOGO = 0xc8; /*�ϴ�LOGO*/

        //��ģʽ������־
        public const int MINOR_LOCAL_PIN = 0xc9; /* ����PIN���ܲ��� */
        public const int MINOR_LOCAL_DIAL = 0xca; /* �����ֶ������Ͽ����� */
        public const int MINOR_SMS_CONTROL = 0xcb; /* ���ſ��������� */
        public const int MINOR_CALL_ONLINE = 0xc; /* ���п������� */
        public const int MINOR_REMOTE_PIN = 0xcd; /* Զ��PIN���ܲ��� */

        public const int MINOR_REMOTE_BYPASS = 0xd0; // Զ����· 
        public const int MINOR_REMOTE_UNBYPASS = 0xd1; // Զ����·�ָ� 
        public const int MINOR_REMOTE_SET_ALARMIN_CFG = 0xd2; // Զ�����ñ���������� 
        public const int MINOR_REMOTE_GET_ALARMIN_CFG = 0xd3; // Զ�̻�ȡ����������� 
        public const int MINOR_REMOTE_SET_ALARMOUT_CFG = 0xd4; // Զ�����ñ���������� 
        public const int MINOR_REMOTE_GET_ALARMOUT_CFG = 0xd5; // Զ�̻�ȡ����������� 
        public const int MINOR_REMOTE_ALARMOUT_OPEN_MAN = 0xd6; // Զ���ֶ������������ 
        public const int MINOR_REMOTE_ALARMOUT_CLOSE_MAN = 0xd7; // Զ���ֶ��رձ������ 
        public const int MINOR_REMOTE_ALARM_ENABLE_CFG = 0xd8; // Զ�����ñ���������RS485����ʹ��״̬ 
        public const int MINOR_DBDATA_OUTPUT = 0xd9; // �������ݿ��¼ 
        public const int MINOR_DBDATA_INPUT = 0xda; // �������ݿ��¼ 
        public const int MINOR_MU_SWITCH = 0xdb; // �����л� 
        public const int MINOR_MU_PTZ = 0xdc; // ����PTZ����
        public const int MINOR_DELETE_LOGO = 0xdd; /* ɾ��logo */

        public const int MINOR_LOCAL_CONF_REB_RAID = 0x101; // ���������Զ��ؽ� 
        public const int MINOR_LOCAL_CONF_SPARE = 0x102; // ���������ȱ� 
        public const int MINOR_LOCAL_ADD_RAID = 0x103; // ���ش������� 
        public const int MINOR_LOCAL_DEL_RAID = 0x104; // ����ɾ������ 
        public const int MINOR_LOCAL_MIG_RAID = 0x105; // ����Ǩ������ 
        public const int MINOR_LOCAL_REB_RAID = 0x106; // �����ֶ��ؽ����� 
        public const int MINOR_LOCAL_QUICK_CONF_RAID = 0x107; // ����һ������ 
        public const int MINOR_LOCAL_ADD_VD = 0x108; // ���ش���������� 
        public const int MINOR_LOCAL_DEL_VD = 0x109; // ����ɾ��������� 
        public const int MINOR_LOCAL_RP_VD = 0x10a; // �����޸�������� 
        public const int MINOR_LOCAL_FORMAT_EXPANDVD = 0x10b; // ������չ����������� 
        public const int MINOR_LOCAL_RAID_UPGRADE = 0x10c; // ����raid������ 
        public const int MINOR_LOCAL_STOP_RAID = 0x10d; // ������ͣRAID����(����ȫ����) 
        public const int MINOR_REMOTE_CONF_REB_RAID = 0x111; // Զ�������Զ��ؽ� 
        public const int MINOR_REMOTE_CONF_SPARE = 0x112; // Զ�������ȱ� 
        public const int MINOR_REMOTE_ADD_RAID = 0x113; // Զ�̴������� 
        public const int MINOR_REMOTE_DEL_RAID = 0x114; // Զ��ɾ������ 
        public const int MINOR_REMOTE_MIG_RAID = 0x115; // Զ��Ǩ������ 
        public const int MINOR_REMOTE_REB_RAID = 0x116; // Զ���ֶ��ؽ����� 
        public const int MINOR_REMOTE_QUICK_CONF_RAID = 0x117; // Զ��һ������ 
        public const int MINOR_REMOTE_ADD_VD = 0x118; // Զ�̴���������� 
        public const int MINOR_REMOTE_DEL_VD = 0x119; // Զ��ɾ��������� 
        public const int MINOR_REMOTE_RP_VD = 0x11a; // Զ���޸�������� 
        public const int MINOR_REMOTE_FORMAT_EXPANDVD = 0x11b; // Զ������������� 
        public const int MINOR_REMOTE_RAID_UPGRADE = 0x11c; // Զ��raid������ 
        public const int MINOR_REMOTE_STOP_RAID = 0x11d; // Զ����ͣRAID����(����ȫ����) 
        public const int MINOR_LOCAL_START_PIC_REC = 0x121; // ���ؿ�ʼץͼ 
        public const int MINOR_LOCAL_STOP_PIC_REC = 0x122; // ����ֹͣץͼ 
        public const int MINOR_LOCAL_SET_SNMP = 0x125; // ��������SNMP 
        public const int MINOR_LOCAL_TAG_OPT = 0x126; // ���ر�ǩ���� 
        public const int MINOR_REMOTE_START_PIC_REC = 0x131; // Զ�̿�ʼץͼ 
        public const int MINOR_REMOTE_STOP_PIC_REC = 0x132; // Զ��ֹͣץͼ 
        public const int MINOR_REMOTE_SET_SNMP = 0x135; // Զ������SNMP 
        public const int MINOR_REMOTE_TAG_OPT = 0x136; // Զ�̱�ǩ���� 

        public const int MINOR_LOCAL_VOUT_SWITCH = 0x140; // ����������л����� 
        public const int MINOR_STREAM_CABAC = 0x141; // ����ѹ������ѡ�����ò��� 

        public const int MINOR_LOCAL_SPARE_OPT = 0x142;   /*����N+1 �ȱ���ز���*/
        public const int MINOR_REMOTE_SPARE_OPT = 0x143;   /*Զ��N+1 �ȱ���ز���*/
        public const int MINOR_LOCAL_IPCCFGFILE_OUTPUT = 0x144;      /* ���ص���ipc�����ļ�*/
        public const int MINOR_LOCAL_IPCCFGFILE_INPUT = 0x145;   /* ���ص���ipc�����ļ� */
        public const int MINOR_LOCAL_IPC_UPGRADE = 0x146;   /* ��������IPC */
        public const int MINOR_REMOTE_IPCCFGFILE_OUTPUT = 0x147;   /* Զ�̵���ipc�����ļ�*/
        public const int MINOR_REMOTE_IPCCFGFILE_INPUT = 0x148;   /* Զ�̵���ipc�����ļ�*/
        public const int MINOR_REMOTE_IPC_UPGRADE = 0x149;   /* Զ������IPC */

        public const int MINOR_SET_MULTI_MASTER = 0x201; // ���ô������� 
        public const int MINOR_SET_MULTI_SLAVE = 0x202; // ���ô������� 
        public const int MINOR_CANCEL_MULTI_MASTER = 0x203; // ȡ���������� 
        public const int MINOR_CANCEL_MULTI_SLAVE = 0x204; // ȡ���������� 

        public const int MINOR_DISPLAY_LOGO = 0x205;    /*��ʾLOGO*/
        public const int MINOR_HIDE_LOGO = 0x206;    /*����LOGO*/
        public const int MINOR_SET_DEC_DELAY_LEVEL = 0x207;    /*����ͨ����ʱ��������*/
        public const int MINOR_SET_BIGSCREEN_DIPLAY_AREA = 0x208;    /*���ô�����ʾ����*/
        public const int MINOR_CUT_VIDEO_SOURCE = 0x209;    /*������ƵԴ�и�����*/
        public const int MINOR_SET_BASEMAP_AREA = 0x210;    /*������ͼ��������*/
        public const int MINOR_DOWNLOAD_BASEMAP = 0x211;    /*���ش�����ͼ*/
        public const int MINOR_CUT_BASEMAP = 0x212;    /*��ͼ�и�����*/
        public const int MINOR_CONTROL_ELEC_ENLARGE = 0x213;    /*���ӷŴ����(�Ŵ��ԭ)*/
        public const int MINOR_SET_OUTPUT_RESOLUTION = 0x214;    /*��ʾ����ֱ�������*/
        public const int MINOR_SET_TRANCSPARENCY = 0X215;    /*ͼ��͸��������*/
        public const int MINOR_SET_OSD = 0x216;    /*��ʾOSD����*/
        public const int MINOR_RESTORE_DEC_STATUS = 0x217;    /*�ָ���ʼ״̬(�����л�ʱ������ָ���ʼ״̬)*/

        public const int MINOR_SCREEN_SET_INPUT = 0x251; // �޸�����Դ 
        public const int MINOR_SCREEN_SET_OUTPUT = 0x252; // �޸����ͨ�� 
        public const int MINOR_SCREEN_SET_OSD = 0x253; // �޸�����LED 
        public const int MINOR_SCREEN_SET_LOGO = 0x254; // �޸�LOGO 
        public const int MINOR_SCREEN_SET_LAYOUT = 0x255; // ���ó��� 
        public const int MINOR_SCREEN_PICTUREPREVIEW = 0x256; // ���Բ��� 

        public const int MINOR_SCREEN_GET_OSD = 0x257; // ��ȡ����LED 
        public const int MINOR_SCREEN_GET_LAYOUT = 0x258; // ��ȡ���� 
        public const int MINOR_SCREEN_LAYOUT_CTRL = 0x259; // �������� 
        public const int MINOR_GET_ALL_VALID_WND = 0x260; // ��ȡ������Ч���� 
        public const int MINOR_GET_SIGNAL_WND = 0x261; // ��ȡ����������Ϣ 
        public const int MINOR_WINDOW_CTRL = 0x262; // ���ڿ��� 
        public const int MINOR_GET_LAYOUT_LIST = 0x263; // ��ȡ�����б� 
        public const int MINOR_LAYOUT_CTRL = 0x264; // �������� 
        public const int MINOR_SET_LAYOUT = 0x265; // ���õ������� 
        public const int MINOR_GET_SIGNAL_LIST = 0x266; // ��ȡ�����ź�Դ�б� 
        public const int MINOR_GET_PLAN_LIST = 0x267; // ��ȡԤ���б� 
        public const int MINOR_SET_PLAN = 0x268; // �޸�Ԥ�� 
        public const int MINOR_CTRL_PLAN = 0x269; // ����Ԥ�� 
        public const int MINOR_CTRL_SCREEN = 0x270; // ��Ļ���� 
        public const int MINOR_ADD_NETSIG = 0x271; // �����ź�Դ 
        public const int MINOR_SET_NETSIG = 0x272; // �޸��ź�Դ 
        public const int MINOR_SET_DECBDCFG = 0x273; // ���ý������� 
        public const int MINOR_GET_DECBDCFG = 0x274; // ��ȡ�������� 
        public const int MINOR_GET_DEVICE_STATUS = 0x275; // ��ȡ�豸��Ϣ 
        public const int MINOR_UPLOAD_PICTURE = 0x276; // ��ͼ�ϴ� 
        public const int MINOR_SET_USERPWD = 0x277; // �����û����� 
        public const int MINOR_ADD_LAYOUT = 0x278; // ���ӳ��� 
        public const int MINOR_DEL_LAYOUT = 0x279; // ɾ������ 
        public const int MINOR_DEL_NETSIG = 0x280; // ɾ���ź�Դ 
        public const int MINOR_ADD_PLAN = 0x281; // ����Ԥ�� 
        public const int MINOR_DEL_PLAN = 0x282; // ɾ��Ԥ�� 
        public const int MINOR_GET_EXTERNAL_MATRIX_CFG = 0x283; // ��ȡ��Ӿ������� 
        public const int MINOR_SET_EXTERNAL_MATRIX_CFG = 0x284; // ������Ӿ������� 
        public const int MINOR_GET_USER_CFG = 0x285; // ��ȡ�û����� 
        public const int MINOR_SET_USER_CFG = 0x286; // �����û����� 
        public const int MINOR_GET_DISPLAY_PANEL_LINK_CFG = 0x287; // ��ȡ��ʾǽ�������� 
        public const int MINOR_SET_DISPLAY_PANEL_LINK_CFG = 0x288; // ������ʾǽ�������� 

        public const int MINOR_GET_WALLSCENE_PARAM = 0x289; // ��ȡ����ǽ���� 
        public const int MINOR_SET_WALLSCENE_PARAM = 0x28a; // ���õ���ǽ���� 
        public const int MINOR_GET_CURRENT_WALLSCENE = 0x28b; // ��ȡ��ǰʹ�ó��� 
        public const int MINOR_SWITCH_WALLSCENE = 0x28c; // �����л� 
        public const int MINOR_SIP_LOGIN = 0x28d; //SIPע��ɹ�
        public const int MINOR_VOIP_START = 0x28e; //VOIP�Խ���ʼ
        public const int MINOR_VOIP_STOP = 0x28f; //VOIP�Խ�ֹͣ
        public const int MINOR_WIN_TOP = 0x290; //����ǽ�����ö�
        public const int MINOR_WIN_BOTTOM = 0x291; //����ǽ�����õ�

        // Netra 2.2.2
        public const int MINOR_LOCAL_LOAD_HDISK = 0x300; // ���ؼ���Ӳ�� 
        public const int MINOR_LOCAL_DELETE_HDISK = 0x301; // ����ɾ���쳣�����ڵ�Ӳ��

        //Netra3.1.0
        public const int MINOR_LOCAL_CFG_DEVICE_TYPE = 0x310; //���������豸����
        public const int MINOR_REMOTE_CFG_DEVICE_TYPE = 0x311; //Զ�������豸����
        public const int MINOR_LOCAL_CFG_WORK_HOT_SERVER = 0x312; //�������ù������ȱ�������
        public const int MINOR_REMOTE_CFG_WORK_HOT_SERVER = 0x313; //Զ�����ù������ȱ�������
        public const int MINOR_LOCAL_DELETE_WORK = 0x314; //����ɾ��������
        public const int MINOR_REMOTE_DELETE_WORK = 0x315; //Զ��ɾ��������
        public const int MINOR_LOCAL_ADD_WORK = 0x316; //�������ӹ�����
        public const int MINOR_REMOTE_ADD_WORK = 0x317; //Զ�����ӹ�����
        public const int MINOR_LOCAL_IPCHEATMAP_OUTPUT = 0x318; /* ���ص����ȶ�ͼ�ļ�      */
        public const int MINOR_LOCAL_IPCHEATFLOW_OUTPUT = 0x319; /* ���ص����ȶ������ļ�      */
        public const int MINOR_REMOTE_SMS_SEND = 0x350; /*Զ�̷��Ͷ���*/
        public const int MINOR_LOCAL_SMS_SEND = 0x351; /*���ط��Ͷ���*/
        public const int MINOR_ALARM_SMS_SEND = 0x352; /*���Ͷ��ű���*/
        public const int MINOR_SMS_RECV = 0x353; /*���ն���*/
        //����ע��0x350��0x351��ָ�˹���GUI��IE�ؼ��ϱ༭�����Ͷ��ţ�
        public const int MINOR_LOCAL_SMS_SEARCH = 0x354; /*������������*/
        public const int MINOR_REMOTE_SMS_SEARCH = 0x355; /*Զ����������*/
        public const int MINOR_LOCAL_SMS_READ = 0x356; /*���ز鿴����*/
        public const int MINOR_REMOTE_SMS_READ = 0x357; /*Զ�̲鿴����*/
        public const int MINOR_REMOTE_DIAL_CONNECT = 0x358; /*Զ�̿����ֶ�����*/
        public const int MINOR_REMOTE_DIAL_DISCONN = 0x359; /*Զ��ֹͣ�ֶ�����*/
        public const int MINOR_LOCAL_ALLOWLIST_SET = 0x35A; /*�������ð�����*/
        public const int MINOR_REMOTE_ALLOWLIST_SET = 0x35B; /*Զ�����ð�����*/
        public const int MINOR_LOCAL_DIAL_PARA_SET = 0x35C; /*�������ò��Ų���*/
        public const int MINOR_REMOTE_DIAL_PARA_SET = 0x35D; /*Զ�����ò��Ų���*/
        public const int MINOR_LOCAL_DIAL_SCHEDULE_SET = 0x35E; /*�������ò��żƻ�*/
        public const int MINOR_REMOTE_DIAL_SCHEDULE_SET = 0x35F; /*Զ�����ò��żƻ�*/
        public const int MINOR_PLAT_OPER = 0x36; /* ƽ̨����*/

        public const int MINOR_REMOTE_OPEN_DOOR = 0x400; // Զ�̿��� 
        public const int MINOR_REMOTE_CLOSE_DOOR = 0x401; // Զ�̹��� 
        public const int MINOR_REMOTE_ALWAYS_OPEN = 0x402; // Զ�̳��� 
        public const int MINOR_REMOTE_ALWAYS_CLOSE = 0x403; // Զ�̳��� 
        public const int MINOR_REMOTE_CHECK_TIME = 0x404; // Զ���ֶ�Уʱ 
        public const int MINOR_NTP_CHECK_TIME = 0x405; // NTP�Զ�Уʱ 
        public const int MINOR_REMOTE_CLEAR_CARD = 0x406; // Զ����տ��� 
        public const int MINOR_REMOTE_RESTORE_CFG = 0x407; // Զ�ָ̻�Ĭ�ϲ��� 
        public const int MINOR_ALARMIN_ARM = 0x408; // �������� 
        public const int MINOR_ALARMIN_DISARM = 0x409; // �������� 
        public const int MINOR_LOCAL_RESTORE_CFG = 0x40a; // ���ػָ�Ĭ�ϲ��� 
        public const int MINOR_REMOTE_CAPTURE_PIC = 0x40b; //Զ��ץ��
        public const int MINOR_MOD_NET_REPORT_CFG = 0x40c; //�޸��������Ĳ�������
        public const int MINOR_MOD_GPRS_REPORT_PARAM = 0x40d; //�޸�GPRS���Ĳ�������
        public const int MINOR_MOD_REPORT_GROUP_PARAM = 0x40e; //�޸��������������
        public const int MINOR_UNLOCK_PASSWORD_OPEN_DOOR = 0x40f; //���������

        public const int MINOR_SET_TRIGGERMODE_CFG = 0x1001; // ���ô���ģʽ���� 
        public const int MINOR_GET_TRIGGERMODE_CFG = 0x1002; // ��ȡ����ģʽ���� 
        public const int MINOR_SET_IOOUT_CFG = 0x1003; // ����IO������� 
        public const int MINOR_GET_IOOUT_CFG = 0x1004; // ��ȡIO������� 
        public const int MINOR_GET_TRIGGERMODE_DEFAULT = 0x1005; // ��ȡ����ģʽ�Ƽ����� 
        public const int MINOR_GET_ITCSTATUS = 0x1006; // ��ȡ״̬������ 
        public const int MINOR_SET_STATUS_DETECT_CFG = 0x1007; // ����״̬������ 
        public const int MINOR_GET_STATUS_DETECT_CFG = 0x1008; // ��ȡ״̬������ 
        public const int MINOR_GET_VIDEO_TRIGGERMODE_CFG = 0x1009; // ��ȡ��Ƶ�羯ģʽ���� 
        public const int MINOR_SET_VIDEO_TRIGGERMODE_CFG = 0x100a; // ������Ƶ�羯ģʽ���� 

        public const int MINOR_LOCAL_ADD_CAR_INFO = 0x2001; // �������ӳ�����Ϣ 
        public const int MINOR_LOCAL_MOD_CAR_INFO = 0x2002; // �����޸ĳ�����Ϣ 
        public const int MINOR_LOCAL_DEL_CAR_INFO = 0x2003; // ����ɾ��������Ϣ 
        public const int MINOR_LOCAL_FIND_CAR_INFO = 0x2004; // ���ز��ҳ�����Ϣ 
        public const int MINOR_LOCAL_ADD_MONITOR_INFO = 0x2005; // �������Ӳ�����Ϣ 
        public const int MINOR_LOCAL_MOD_MONITOR_INFO = 0x2006; // �����޸Ĳ�����Ϣ 
        public const int MINOR_LOCAL_DEL_MONITOR_INFO = 0x2007; // ����ɾ��������Ϣ 
        public const int MINOR_LOCAL_FIND_MONITOR_INFO = 0x2008; // ���ز�ѯ������Ϣ 
        public const int MINOR_LOCAL_FIND_NORMAL_PASS_INFO = 0x2009; // ���ز�ѯ����ͨ����Ϣ 
        public const int MINOR_LOCAL_FIND_ABNORMAL_PASS_INFO = 0x200a; // ���ز�ѯ�쳣ͨ����Ϣ 
        public const int MINOR_LOCAL_FIND_PEDESTRIAN_PASS_INFO = 0x200b; // ���ز�ѯ����ͨ����Ϣ 
        public const int MINOR_LOCAL_PIC_PREVIEW = 0x200c; // ����ͼƬԤ�� 
        public const int MINOR_LOCAL_SET_GATE_PARM_CFG = 0x200d; // ���ñ������ó���ڲ��� 
        public const int MINOR_LOCAL_GET_GATE_PARM_CFG = 0x200e; // ��ȡ�������ó���ڲ��� 
        public const int MINOR_LOCAL_SET_DATAUPLOAD_PARM_CFG = 0x200f; // ���ñ������������ϴ����� 
        public const int MINOR_LOCAL_GET_DATAUPLOAD_PARM_CFG = 0x2010; // ��ȡ�������������ϴ����� 

        public const int MINOR_LOCAL_DEVICE_CONTROL = 0x2011; // �����豸����(���ؿ���բ) 
        public const int MINOR_LOCAL_ADD_EXTERNAL_DEVICE_INFO = 0x2012; // ������������豸��Ϣ 
        public const int MINOR_LOCAL_MOD_EXTERNAL_DEVICE_INFO = 0x2013; // �����޸�����豸��Ϣ 
        public const int MINOR_LOCAL_DEL_EXTERNAL_DEVICE_INFO = 0x2014; // ����ɾ������豸��Ϣ 
        public const int MINOR_LOCAL_FIND_EXTERNAL_DEVICE_INFO = 0x2015; // ���ز�ѯ����豸��Ϣ 
        public const int MINOR_LOCAL_ADD_CHARGE_RULE = 0x2016; // ���������շѹ��� 
        public const int MINOR_LOCAL_MOD_CHARGE_RULE = 0x2017; // �����޸��շѹ��� 
        public const int MINOR_LOCAL_DEL_CHARGE_RULE = 0x2018; // ����ɾ���շѹ��� 
        public const int MINOR_LOCAL_FIND_CHARGE_RULE = 0x2019; // ���ز�ѯ�շѹ��� 
        public const int MINOR_LOCAL_COUNT_NORMAL_CURRENTINFO = 0x2020; // ����ͳ������ͨ����Ϣ 
        public const int MINOR_LOCAL_EXPORT_NORMAL_CURRENTINFO_REPORT = 0x2021; // ���ص�������ͨ����Ϣͳ�Ʊ��� 
        public const int MINOR_LOCAL_COUNT_ABNORMAL_CURRENTINFO = 0x2022; // ����ͳ���쳣ͨ����Ϣ 
        public const int MINOR_LOCAL_EXPORT_ABNORMAL_CURRENTINFO_REPORT = 0x2023; // ���ص����쳣ͨ����Ϣͳ�Ʊ��� 
        public const int MINOR_LOCAL_COUNT_PEDESTRIAN_CURRENTINFO = 0x2024; // ����ͳ������ͨ����Ϣ 
        public const int MINOR_LOCAL_EXPORT_PEDESTRIAN_CURRENTINFO_REPORT = 0x2025; // ���ص�������ͨ����Ϣͳ�Ʊ��� 
        public const int MINOR_LOCAL_FIND_CAR_CHARGEINFO = 0x2026; // ���ز�ѯ�����շ���Ϣ 
        public const int MINOR_LOCAL_COUNT_CAR_CHARGEINFO = 0x2027; // ����ͳ�ƹ����շ���Ϣ 
        public const int MINOR_LOCAL_EXPORT_CAR_CHARGEINFO_REPORT = 0x2028; // ���ص��������շ���Ϣͳ�Ʊ��� 
        public const int MINOR_LOCAL_FIND_SHIFTINFO = 0x2029; // ���ز�ѯ���Ӱ���Ϣ 
        public const int MINOR_LOCAL_FIND_CARDINFO = 0x2030; // ���ز�ѯ��Ƭ��Ϣ 
        public const int MINOR_LOCAL_ADD_RELIEF_RULE = 0x2031; // �������Ӽ������ 
        public const int MINOR_LOCAL_MOD_RELIEF_RULE = 0x2032; // �����޸ļ������ 
        public const int MINOR_LOCAL_DEL_RELIEF_RULE = 0x2033; // ����ɾ��������� 
        public const int MINOR_LOCAL_FIND_RELIEF_RULE = 0x2034; // ���ز�ѯ������� 
        public const int MINOR_LOCAL_GET_ENDETCFG = 0x2035; // ���ػ�ȡ����ڿ��ƻ����߼������ 
        public const int MINOR_LOCAL_SET_ENDETCFG = 0x2036; // �������ó���ڿ��ƻ����߼������ 
        public const int MINOR_LOCAL_SET_ENDEV_ISSUEDDATA = 0x2037; // �������ó���ڿ��ƻ��·���Ƭ��Ϣ 
        public const int MINOR_LOCAL_DEL_ENDEV_ISSUEDDATA = 0x2038; // ������ճ���ڿ��ƻ��·���Ƭ��Ϣ 
        public const int MINOR_REMOTE_DEVICE_CONTROL = 0x2101; // Զ���豸���� 
        public const int MINOR_REMOTE_SET_GATE_PARM_CFG = 0x2102; // ����Զ�����ó���ڲ��� 
        public const int MINOR_REMOTE_GET_GATE_PARM_CFG = 0x2103; // ��ȡԶ�����ó���ڲ��� 
        public const int MINOR_REMOTE_SET_DATAUPLOAD_PARM_CFG = 0x2104; // ����Զ�����������ϴ����� 
        public const int MINOR_REMOTE_GET_DATAUPLOAD_PARM_CFG = 0x2105; // ��ȡԶ�����������ϴ����� 
        public const int MINOR_REMOTE_GET_BASE_INFO = 0x2106; // Զ�̻�ȡ�ն˻�����Ϣ 
        public const int MINOR_REMOTE_GET_OVERLAP_CFG = 0x2107; // Զ�̻�ȡ�ַ����Ӳ������� 
        public const int MINOR_REMOTE_SET_OVERLAP_CFG = 0x2108; // Զ�������ַ����Ӳ������� 
        public const int MINOR_REMOTE_GET_ROAD_INFO = 0x2109; // Զ�̻�ȡ·����Ϣ 
        public const int MINOR_REMOTE_START_TRANSCHAN = 0x210a; // Զ�̽���ͬ�����ݷ����� 
        public const int MINOR_REMOTE_GET_ECTWORKSTATE = 0x210b; // Զ�̻�ȡ������ն˹���״̬ 
        public const int MINOR_REMOTE_GET_ECTCHANINFO = 0x210c; // Զ�̻�ȡ������ն�ͨ��״̬ 
        public const int MINOR_REMOTE_ADD_EXTERNAL_DEVICE_INFO = 0x210d; // Զ����������豸��Ϣ 
        public const int MINOR_REMOTE_MOD_EXTERNAL_DEVICE_INFO = 0x210e; // Զ���޸�����豸��Ϣ 
        public const int MINOR_REMOTE_GET_ENDETCFG = 0x210f; // Զ�̻�ȡ����ڿ��ƻ����߼������ 
        public const int MINOR_REMOTE_SET_ENDETCFG = 0x2110; // Զ�����ó���ڿ��ƻ����߼������ 
        public const int MINOR_REMOTE_ENDEV_ISSUEDDATA = 0x2111; // Զ�����ó���ڿ��ƻ��·���Ƭ��Ϣ 
        public const int MINOR_REMOTE_DEL_ENDEV_ISSUEDDATA = 0x2112; // Զ����ճ���ڿ��ƻ��·���Ƭ��Ϣ 

        public const int MINOR_REMOTE_ON_CTRL_LAMP = 0x2115; // ����Զ�̿��Ƴ�λָʾ�� 
        public const int MINOR_REMOTE_OFF_CTRL_LAMP = 0x2116; // �ر�Զ�̿��Ƴ�λָʾ�� 
        public const int MINOR_SET_VOICE_LEVEL_PARAM = 0x2117; // ����������С 
        public const int MINOR_SET_VOICE_INTERCOM_PARAM = 0x2118; // ��������¼�� 
        public const int MINOR_SET_INTELLIGENT_PARAM = 0x2119; // �������� 
        public const int MINOR_LOCAL_SET_RAID_SPEED = 0x211a; // ��������raid�ٶ� 
        public const int MINOR_REMOTE_SET_RAID_SPEED = 0x211b; // Զ������raid�ٶ� 
        public const int MINOR_REMOTE_CREATE_STORAGE_POOL = 0x211c; // Զ�����Ӵ洢�� 
        public const int MINOR_REMOTE_DEL_STORAGE_POOL = 0x211d; // Զ��ɾ���洢�� 

        public const int MINOR_REMOTE_DEL_PIC = 0x2120; // Զ��ɾ��ͼƬ���� 
        public const int MINOR_REMOTE_DEL_RECORD = 0x2121; // Զ��ɾ��¼������ 
        public const int MINOR_REMOTE_CLOUD_ENABLE = 0x2123; // Զ�������ƴ洢���� 
        public const int MINOR_REMOTE_CLOUD_DISABLE = 0x2124; // Զ�������ƴ洢���� 
        public const int MINOR_REMOTE_CLOUD_MODIFY_PARAM = 0x2125; // Զ���޸��ƴ洢�ز��� 
        public const int MINOR_REMOTE_CLOUD_MODIFY_VOLUME = 0x2126; // Զ���޸��ƴ洢������ 
        public const int MINOR_REMOTE_GET_GB28181_SERVICE_PARAM = 0x2127; //Զ�̻�ȡGB28181�������
        public const int MINOR_REMOTE_SET_GB28181_SERVICE_PARAM = 0x2128; //Զ������GB28181�������
        public const int MINOR_LOCAL_GET_GB28181_SERVICE_PARAM = 0x2129; //���ػ�ȡGB28181�������
        public const int MINOR_LOCAL_SET_GB28181_SERVICE_PARAM = 0x212a; //��������B28181�������
        public const int MINOR_REMOTE_SET_SIP_SERVER = 0x212b; //Զ������SIP SERVER
        public const int MINOR_LOCAL_SET_SIP_SERVER = 0x212c; //��������SIP SERVER
        public const int MINOR_LOCAL_BLOCKALLOWFILE_OUTPUT = 0x212d; 
        public const int MINOR_LOCAL_BLOCKALLOWFILE_INPUT = 0x212e; 
        public const int MINOR_REMOTE_BALCKALLOWCFGFILE_OUTPUT = 0x212f; 
        public const int MINOR_REMOTE_BALCKALLOWCFGFILE_INPUT = 0x2130; 

        public const int MINOR_REMOTE_CREATE_MOD_VIEWLIB_SPACE = 0x2200; // Զ�̴���/�޸���ͼ��ռ� 
        public const int MINOR_REMOTE_DELETE_VIEWLIB_FILE = 0x2201; // Զ��ɾ����ͼ���ļ� 
        public const int MINOR_REMOTE_DOWNLOAD_VIEWLIB_FILE = 0x2202; // Զ��������ͼ���ļ� 
        public const int MINOR_REMOTE_UPLOAD_VIEWLIB_FILE = 0x2203; // Զ���ϴ���ͼ���ļ� 
        public const int MINOR_LOCAL_CREATE_MOD_VIEWLIB_SPACE = 0x2204; // ���ش���/�޸���ͼ��ռ� 

        public const int MINOR_LOCAL_SET_DEVICE_ACTIVE = 0x3000; //���ؼ����豸
        public const int MINOR_REMOTE_SET_DEVICE_ACTIVE = 0x3001; //Զ�̼����豸
        public const int MINOR_LOCAL_PARA_FACTORY_DEFAULT = 0x3002; //���ػظ���������
        public const int MINOR_REMOTE_PARA_FACTORY_DEFAULT = 0x3003; //Զ�ָ̻���������

        // ������������
        public const int NET_DVR_GET_THERMOMETRY_BASICPARAM_CAPABILITIES = 3620; //��ȡ������������
        public const int NET_DVR_GET_THERMOMETRY_BASICPARAM = 3621;    //��ȡ�������ò���
        public const int NET_DVR_SET_THERMOMETRY_BASICPARAM = 3622;    //���ò������ò���
        public const int NET_DVR_GET_THERMOMETRY_SCENE_CAPABILITIES = 3623; //��ȡ����Ԥ�õ������������
        public const int NET_DVR_GET_THERMOMETRY_PRESETINFO = 3624;    //��ȡ����Ԥ�õ�������ò���
        public const int NET_DVR_SET_THERMOMETRY_PRESETINFO = 3625;    //���ò���Ԥ�õ�������ò���
        public const int NET_DVR_GET_THERMOMETRY_ALARMRULE_CAPABILITIES = 3626;//��ȡ���±�����ʽ��������
        public const int NET_DVR_GET_THERMOMETRY_ALARMRULE = 3627;    //��ȡ����Ԥ�õ㱨���������ò���
        public const int NET_DVR_SET_THERMOMETRY_ALARMRULE = 3628;    //���ò���Ԥ�õ㱨���������ò���
        public const int NET_DVR_GET_REALTIME_THERMOMETRY = 3629;    //ʵʱ�¶ȼ��
        public const int NET_DVR_GET_THERMOMETRY_DIFFCOMPARISON = 3630;    //��ȡ����Ԥ�õ��²�������ò���
        public const int NET_DVR_SET_THERMOMETRY_DIFFCOMPARISON = 3631;    //���ò���Ԥ�õ��²�������ò���
        public const int NET_DVR_GET_THERMOMETRY_TRIGGER = 3632;    //��ȡ������������
        public const int NET_DVR_SET_THERMOMETRY_TRIGGER = 3633;    //���ò�����������

        public const int NET_DVR_GET_THERMAL_CAPABILITIES = 3634;    //��ȡ�ȳ���Thermal������
        public const int NET_DVR_GET_FIREDETECTION_CAPABILITIES = 3635;    //��ȡ�������������
        public const int NET_DVR_GET_FIREDETECTION = 3636;    //��ȡ��������
        public const int NET_DVR_SET_FIREDETECTION = 3637;    //���û�������
        public const int NET_DVR_GET_FIREDETECTION_TRIGGER = 3638;  //��ȡ�������������
        public const int NET_DVR_SET_FIREDETECTION_TRIGGER = 3639;    //���û������������
        public const int NET_DVR_GET_THERMOMETRY_SCHEDULE_CAPABILITIES = 6750;     //��ȡ���¼�Ⲽ��ʱ������
        public const int NET_DVR_GET_THERMOMETRY_SCHEDULE = 6751;     //��ȡ���¼�Ⲽ��ʱ������
        public const int NET_DVR_SET_THERMOMETRY_SCHEDULE = 6752;     //���ò��¼�Ⲽ��ʱ������
        public const int NET_DVR_GET_TEMPERTURE_SCHEDULE_CAPABILITIES = 6753;     //��ȡ�²��ʱ������
        public const int NET_DVR_GET_TEMPERTURE_SCHEDULE = 6754;     //��ȡ�²��ʱ������
        public const int NET_DVR_SET_TEMPERTURE_SCHEDULE = 6755;     //�����²��ʱ������
        public const int NET_DVR_GET_SEARCH_LOG_CAPABILITIES = 6756;  //��־����֧������
        public const int NET_DVR_GET_VEHICLEFLOW = 6758;    //��ȡ����������
        public const int NET_DVR_GET_IPADDR_FILTERCFG_V50 = 6759;     //��ȡIP��ַ���˲�����չ
        public const int NET_DVR_SET_IPADDR_FILTERCFG_V50 = 6760;     //����IP��ַ���˲�����չ
        public const int NET_DVR_GET_TEMPHUMSENSOR_CAPABILITIES = 6761;     //��ȡ��ʪ�ȴ�����������
        public const int NET_DVR_GET_TEMPHUMSENSOR = 6762;     //��ȡ��ʪ�ȴ���������Э��
        public const int NET_DVR_SET_TEMPHUMSENSOR = 6763;     //������ʪ�ȴ���������Э��

        public const int NET_DVR_GET_THERMOMETRY_MODE_CAPABILITIES = 6764; //��ȡ����ģʽ����
        public const int NET_DVR_GET_THERMOMETRY_MODE = 6765; //��ȡ����ģʽ����
        public const int NET_DVR_SET_THERMOMETRY_MODE = 6766;  //���ò���ģʽ����

        public const int NET_DVR_GET_THERMAL_PIP_CAPABILITIES = 6767;    //��ȡ�ȳ����л���������
        public const int NET_DVR_GET_THERMAL_PIP = 6768;    //��ȡ�ȳ����л����ò���
        public const int NET_DVR_SET_THERMAL_PIP = 6769;    //�����ȳ����л����ò���
        public const int NET_DVR_GET_THERMAL_INTELRULEDISPLAY_CAPABILITIES = 6770;    //��ȡ�ȳ������ܹ�����ʾ����
        public const int NET_DVR_GET_THERMAL_INTELRULE_DISPLAY = 6771;    //��ȡ�ȳ������ܹ�����ʾ����
        public const int NET_DVR_SET_THERMAL_INTELRULE_DISPLAY = 6772;    //�����ȳ������ܹ�����ʾ����
        public const int NET_DVR_GET_THERMAL_ALGVERSION = 6773;   //��ȡ�ȳ�������㷨��汾
        public const int NET_DVR_GET_CURRENT_LOCK_CAPABILITIES = 6774;    //��ȡ����������������
        public const int NET_DVR_GET_CURRENT_LOCK = 6775;    //��ȡ�����������ò���
        public const int NET_DVR_SET_CURRENT_LOCK = 6776;    //���õ����������ò���

        public const int NET_DVR_SET_MANUALTHERM_BASICPARAM = 6716;  //�����ֶ����»�������
        public const int NET_DVR_GET_MANUALTHERM_BASICPARAM = 6717;  //��ȡ�ֶ����»�������

        public const int NET_DVR_GET_MANUALTHERM_INFO = 6706;   //�ֶ�����ʵʱ��ȡ
        public const int NET_DVR_SET_MANUALTHERM = 6708; //�����ֶ�������������
        public const int NET_DVR_DEL_MANUALTHERM_RULE = 6778;     //ɾ���ֶ����¹���

        public const int NET_DVR_GET_THERMOMETRYRULE_TEMPERATURE_INFO = 23001;    //�ֶ���ȡ���¹����¶���Ϣ

        public const int  NET_DVR_GET_BAREDATAOVERLAY_CAPABILITIES  = 6660;   //��ȡ�����ݵ�������
        public const int   NET_DVR_SET_BAREDATAOVERLAY_CFG  =  6661;    //���������ݵ���
        public const int NET_DVR_GET_BAREDATAOVERLAY_CFG = 6662;    //��ȡ�����ݵ���

        /********************************��������ʶ��****************************/
        public const int NET_DVR_GET_FACESNAPCFG = 5001;//��ȡ����ץ�Ĳ���
        public const int NET_DVR_SET_FACESNAPCFG = 5002;//��������ץ�Ĳ���
        public const int NET_DVR_GET_DEVACCESS_CFG = 5005;//��ȡ�����豸����
        public const int NET_DVR_SET_DEVACCESS_CFG = 5006;//���ý����豸����
        public const int NET_DVR_GET_SAVE_PATH_CFG = 5007;//��ȡ�洢��Ϣ����
        public const int NET_DVR_SET_SAVE_PATH_CFG = 5008;//���ô洢��Ϣ����
        public const int NET_VCA_GET_RULECFG_V41 = 5011;//��ȡ��Ϊ��������(��չ)
        public const int NET_VCA_SET_RULECFG_V41 = 5012;//������Ϊ��������(��չ)
        public const int NET_DVR_GET_AID_RULECFG_V41 = 5013;//��ȡ��ͨ�¼��������
        public const int NET_DVR_SET_AID_RULECFG_V41 = 5014;//���ý�ͨ�¼��������
        public const int NET_DVR_GET_TPS_RULECFG_V41 = 5015;//��ȡ��ͨͳ�ƹ������(��չ)
        public const int NET_DVR_SET_TPS_RULECFG_V41 = 5016;//���ý�ͨͳ�ƹ������(��չ)
        public const int NET_VCA_GET_FACEDETECT_RULECFG_V41 = 5017;//��ȡATM����������(��չ) 
        public const int NET_VCA_SET_FACEDETECT_RULECFG_V41 = 5018;//����ATM����������(��չ)
        public const int NET_DVR_GET_PDC_RULECFG_V41 = 5019;//����������ͳ�ƹ���(��չ)
        public const int NET_DVR_SET_PDC_RULECFG_V41 = 5020;//��ȡ������ͳ�ƹ���(��չ)
        public const int NET_DVR_GET_TRIAL_VERSION_CFG = 5021;//��ȡ���ð���Ϣ
        public const int NET_DVR_GET_VCA_CTRLINFO_CFG = 5022;//������ȡ���ܿ��Ʋ���
        public const int NET_DVR_SET_VCA_CTRLINFO_CFG = 5023;//�����������ܿ��Ʋ���
        public const int NET_DVR_SYN_CHANNEL_NAME = 5024;//ͬ��ͨ����
        public const int NET_DVR_GET_RESET_COUNTER = 5025;//��ȡͳ�������������������������ͨͳ�ƣ�
        public const int NET_DVR_SET_RESET_COUNTER = 5026;//����ͳ�������������������������ͨͳ�ƣ�
        public const int NET_DVR_GET_OBJECT_COLOR = 5027;//��ȡ������ɫ����
        public const int NET_DVR_SET_OBJECT_COLOR = 5028;//����������ɫ����
        public const int NET_DVR_GET_AUX_AREA = 5029;//��ȡ��������
        public const int NET_DVR_SET_AUX_AREA = 5030;//���ø�������
        public const int NET_DVR_GET_CHAN_WORKMODE = 5031;//��ȡͨ������ģʽ
        public const int NET_DVR_SET_CHAN_WORKMODE = 5032;//����ͨ������ģʽ
        public const int NET_DVR_GET_SLAVE_CHANNEL = 5033;//��ȡ��ͨ������
        public const int NET_DVR_SET_SLAVE_CHANNEL = 5034;//���ô�ͨ������
        public const int NET_DVR_GET_VQD_EVENT_RULE = 5035;//��ȡ��Ƶ��������¼�����
        public const int NET_DVR_SET_VQD_EVENT_RULE = 5036;//������Ƶ��������¼�����
        public const int NET_DVR_GET_BASELINE_SCENE = 5037;//��ȡ��׼��������
        public const int NET_DVR_SET_BASELINE_SCENE = 5038;//���û�׼��������
        public const int NET_DVR_CONTROL_BASELINE_SCENE = 5039;//��׼��������
        public const int NET_DVR_SET_VCA_DETION_CFG = 5040;//���������ƶ���������
        public const int NET_DVR_GET_VCA_DETION_CFG = 5041;//��ȡ�����ƶ���������
        public const int NET_DVR_GET_STREAM_ATTACHINFO_CFG = 5042;//��ȡ����������Ϣ����
        public const int NET_DVR_SET_STREAM_ATTACHINFO_CFG = 5043;//��������������Ϣ����

        public const int NET_DVR_GET_BV_CALIB_TYPE = 5044;//��ȡ˫Ŀ�궨����
        public const int NET_DVR_CONTROL_BV_SAMPLE_CALIB = 5045;//˫Ŀ�����궨
        public const int NET_DVR_GET_BV_SAMPLE_CALIB_CFG = 5046;//��ȡ˫Ŀ�궨����
        public const int NET_DVR_GET_RULECFG_V42 = 5049;//��ȡ��Ϊ��������(֧��16��������չ)
        public const int NET_DVR_SET_RULECFG_V42 = 5050;//������Ϊ��������(֧��16��������չ)
        public const int NET_DVR_SET_VCA_DETION_CFG_V40 = 5051;//���������ƶ���������
        public const int NET_DVR_GET_VCA_DETION_CFG_V40 = 5052;//��ȡ�����ƶ���������
        public const int NET_DVR_SET_FLASH_CFG = 5110;//д�����ݵ�Flash ����ʹ��
        /********************************��������ʶ�� end****************************/


        /*��־������Ϣ*/
        //������
        public const int MAJOR_INFORMATION = 4;/*������Ϣ*/
        //������
        public const int MINOR_HDD_INFO = 0xa1; // Ӳ����Ϣ 
        public const int MINOR_SMART_INFO = 0xa2; // S.M.A.R.T��Ϣ 
        public const int MINOR_REC_START = 0xa3; // ��ʼ¼�� 
        public const int MINOR_REC_STOP = 0xa4; // ֹͣ¼�� 
        public const int MINOR_REC_OVERDUE = 0xa5; // ����¼��ɾ�� 
        public const int MINOR_LINK_START = 0xa6; // ����ǰ���豸 
        public const int MINOR_LINK_STOP = 0xa7; // �Ͽ�ǰ���豸 
        public const int MINOR_NET_DISK_INFO = 0xa8; // ����Ӳ����Ϣ 
        public const int MINOR_RAID_INFO = 0xa9; // raid�����Ϣ 
        public const int MINOR_RUN_STATUS_INFO = 0xaa; // ϵͳ����״̬��Ϣ 
        public const int MINOR_SPARE_START_BACKUP = 0xab; // �ȱ�ϵͳ��ʼ����ָ�������� 
        public const int MINOR_SPARE_STOP_BACKUP = 0xac; // �ȱ�ϵͳֹͣ����ָ�������� 
        public const int MINOR_SPARE_CLIENT_INFO = 0xad; // �ȱ��ͻ�����Ϣ 
        public const int MINOR_ANR_RECORD_START = 0xae; // ANR¼��ʼ 
        public const int MINOR_ANR_RECORD_END = 0xaf; // ANR¼����� 
        public const int MINOR_ANR_ADD_TIME_QUANTUM = 0xb0; // ANR����ʱ��� 
        public const int MINOR_ANR_DEL_TIME_QUANTUM = 0xb1; // ANRɾ��ʱ��� 
        public const int MINOR_PIC_REC_START = 0xb3; // ��ʼץͼ 
        public const int MINOR_PIC_REC_STOP = 0xb4; // ֹͣץͼ 
        public const int MINOR_PIC_REC_OVERDUE = 0xb5; //����ͼƬ�ļ�ɾ�� 
        public const int MINOR_CLIENT_LOGIN = 0xb6; // ��¼�������ɹ� 
        public const int MINOR_CLIENT_RELOGIN = 0xb7; // ���µ�¼������ 
        public const int MINOR_CLIENT_LOGOUT = 0xb8; // �˳��������ɹ� 
        public const int MINOR_CLIENT_SYNC_START = 0xb9; // ¼��ͬ����ʼ 
        public const int MINOR_CLIENT_SYNC_STOP = 0xba; // ¼��ͬ����ֹ 
        public const int MINOR_CLIENT_SYNC_SUCC = 0xbb; // ¼��ͬ���ɹ� 
        public const int MINOR_CLIENT_SYNC_EXCP = 0xbc; // ¼��ͬ���쳣 
        public const int MINOR_GLOBAL_RECORD_ERR_INFO = 0xbd; // ȫ�ִ����¼��Ϣ 
        public const int MINOR_BUFFER_STATE = 0xbe; // ������״̬��־��¼ 
        public const int MINOR_DISK_ERRORINFO_V2 = 0xbf; // Ӳ�̴�����ϸ��ϢV2 
        public const int MINOR_UNLOCK_RECORD = 0xc3; // ������¼ 
        public const int MINOR_VIS_ALARM = 0xc4; // �������� 
        public const int MINOR_TALK_RECORD = 0xc5; // ͨ����¼ 

        /*��־������Ϣ*/
        //������
        public const int MAJOR_EVENT = 0x5;/*�¼�*/
        //������
        public const int MINOR_LEGAL_CARD_PASS = 0x01; // �Ϸ�����֤ͨ�� 
        public const int MINOR_CARD_AND_PSW_PASS = 0x02; // ˢ����������֤ͨ�� 
        public const int MINOR_CARD_AND_PSW_FAIL = 0x03; // ˢ����������֤ʧ�� 
        public const int MINOR_CARD_AND_PSW_TIMEOUT = 0x04; // ������������֤��ʱ 
        public const int MINOR_CARD_AND_PSW_OVER_TIME = 0x05; // ˢ�������볬�� 
        public const int MINOR_CARD_NO_RIGHT = 0x06; // δ����Ȩ�� 
        public const int MINOR_CARD_INVALID_PERIOD = 0x07; // ��Чʱ�� 
        public const int MINOR_CARD_OUT_OF_DATE = 0x08; // ���Ź��� 
        public const int MINOR_INVALID_CARD = 0x09; // �޴˿��� 
        public const int MINOR_ANTI_SNEAK_FAIL = 0x0a; // ��Ǳ����֤ʧ�� 
        public const int MINOR_INTERLOCK_DOOR_NOT_CLOSE = 0x0b; // ������δ�ر� 
        public const int MINOR_NOT_BELONG_MULTI_GROUP = 0x0c; // �������ڶ�����֤Ⱥ�� 
        public const int MINOR_INVALID_MULTI_VERIFY_PERIOD = 0x0d; // �����ڶ�����֤ʱ����� 
        public const int MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL = 0x0e; // ������֤ģʽ����Ȩ����֤ʧ�� 
        public const int MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL = 0x0f; // ������֤ģʽԶ����֤ʧ�� 
        public const int MINOR_MULTI_VERIFY_SUCCESS = 0x10; // ������֤�ɹ� 
        public const int MINOR_LEADER_CARD_OPEN_BEGIN = 0x11; // �׿����ſ�ʼ 
        public const int MINOR_LEADER_CARD_OPEN_END = 0x12; // �׿����Ž��� 
        public const int MINOR_ALWAYS_OPEN_BEGIN = 0x13; // ����״̬��ʼ 
        public const int MINOR_ALWAYS_OPEN_END = 0x14; // ����״̬���� 
        public const int MINOR_LOCK_OPEN = 0x15; // ������ 
        public const int MINOR_LOCK_CLOSE = 0x16; // �����ر� 
        public const int MINOR_DOOR_BUTTON_PRESS = 0x17; // ���Ű�ť�� 
        public const int MINOR_DOOR_BUTTON_RELEASE = 0x18; // ���Ű�ť�ſ� 
        public const int MINOR_DOOR_OPEN_NORMAL = 0x19; // �������ţ��Ŵţ� 
        public const int MINOR_DOOR_CLOSE_NORMAL = 0x1a; // �������ţ��Ŵţ� 
        public const int MINOR_DOOR_OPEN_ABNORMAL = 0x1b; // ���쳣�򿪣��Ŵţ� 
        public const int MINOR_DOOR_OPEN_TIMEOUT = 0x1c; // �Ŵ򿪳�ʱ���Ŵţ�  
        public const int MINOR_ALARMOUT_ON = 0x1d; // ��������� 
        public const int MINOR_ALARMOUT_OFF = 0x1e; // ��������ر� 
        public const int MINOR_ALWAYS_CLOSE_BEGIN = 0x1f; // ����״̬��ʼ 
        public const int MINOR_ALWAYS_CLOSE_END = 0x20; // ����״̬���� 
        public const int MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN = 0x21; // ���ض�����֤��ҪԶ�̿��� 
        public const int MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS = 0x22; // ������֤����������֤�ɹ��¼� 
        public const int MINOR_MULTI_VERIFY_REPEAT_VERIFY = 0x23; // ������֤�ظ���֤�¼� 
        public const int MINOR_MULTI_VERIFY_TIMEOUT = 0x24; // ������֤�ظ���֤�¼� 
        public const int MINOR_DOORBELL_RINGING = 0x25; // ������
        public const int MINOR_FINGERPRINT_COMPARE_PASS = 0x26; // ָ�Ʊȶ�ͨ��
        public const int MINOR_FINGERPRINT_COMPARE_FAIL = 0x27; // ָ�Ʊȶ�ʧ��
        public const int MINOR_CARD_FINGERPRINT_VERIFY_PASS = 0x28; // ˢ����ָ����֤ͨ��
        public const int MINOR_CARD_FINGERPRINT_VERIFY_FAIL = 0x29; // ˢ����ָ����֤ʧ��
        public const int MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT = 0x2a; // ˢ����ָ����֤��ʱ
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2b; // ˢ����ָ�Ƽ�������֤ͨ��
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2c; // ˢ����ָ�Ƽ�������֤ʧ��
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x2d; // ˢ����ָ�Ƽ�������֤��ʱ
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2e; // ָ�Ƽ�������֤ͨ��
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2f; // ָ�Ƽ�������֤ʧ��
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x30; // ָ�Ƽ�������֤��ʱ
        public const int MINOR_FINGERPRINT_INEXISTENCE = 0x31; // ָ�Ʋ�����

        //����־��������ΪMAJOR_OPERATION=03��������ΪMINOR_LOCAL_CFG_PARM=0x52����MINOR_REMOTE_GET_PARM=0x76����MINOR_REMOTE_CFG_PARM=0x77ʱ��dwParaType:����������Ч���京�����£�
        public const int PARA_VIDEOOUT = 1;
        public const int PARA_IMAGE = 2;
        public const int PARA_ENCODE = 4;
        public const int PARA_NETWORK = 8;
        public const int PARA_ALARM = 16;
        public const int PARA_EXCEPTION = 32;
        public const int PARA_DECODER = 64;/*������*/
        public const int PARA_RS232 = 128;
        public const int PARA_PREVIEW = 256;
        public const int PARA_SECURITY = 512;
        public const int PARA_DATETIME = 1024;
        public const int PARA_FRAMETYPE = 2048;/*֡��ʽ*/

        //vca
        //        public const int PARA_VCA_RULE = 4096;//��Ϊ����

        public const int PARA_DETECTION = 0x1000;   //�������
        public const int PARA_VCA_RULE = 0x1001;  //��Ϊ���� 
        public const int PARA_VCA_CTRL = 0x1002;  //�������ܿ�����Ϣ
        public const int PARA_VCA_PLATE = 0x1003; // ����ʶ��

        public const int PARA_CODESPLITTER = 0x2000; /*���������*/
        //2010-01-22 ������Ƶ�ۺ�ƽ̨��־��Ϣ������
        public const int PARA_RS485 = 0x2001; /* RS485������Ϣ*/
        public const int PARA_DEVICE = 0x2002; /* �豸������Ϣ*/
        public const int PARA_HARDDISK = 0x2003; /* Ӳ��������Ϣ */
        public const int PARA_AUTOBOOT = 0x2004; /* �Զ�����������Ϣ*/
        public const int PARA_HOLIDAY = 0x2005; /* �ڼ���������Ϣ*/
        public const int PARA_IPC = 0x2006; /* IPͨ������ */




        //��־��Ϣ(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG_V30
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//������ 1-����; 2-�쳣; 3-����; 0xff-ȫ��
            public uint dwMinorType;//������ 0-ȫ��;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//���������û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//����������û���
            public NET_DVR_IPADDR struRemoteHostAddr;//Զ��������ַ
            public uint dwParaType;//��������
            public uint dwChannel;//ͨ����
            public uint dwDiskNumber;//Ӳ�̺�
            public uint dwAlarmInPort;//��������˿�
            public uint dwAlarmOutPort;//��������˿�
            public uint dwInfoLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN)]
            public string sInfo;
        }

        //��־��Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//������ 1-����; 2-�쳣; 3-����; 0xff-ȫ��
            public uint dwMinorType;//������ 0-ȫ��;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//���������û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//����������û���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteHostAddr;//Զ��������ַ
            public uint dwParaType;//��������
            public uint dwChannel;//ͨ����
            public uint dwDiskNumber;//Ӳ�̺�
            public uint dwAlarmInPort;//��������˿�
            public uint dwAlarmOutPort;//��������˿�
        }

        /************************DVR��־ end***************************/

        //�������״̬(9000��չ)
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

        //�������״̬
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;
        }

        //������Ϣ
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
            public byte[] DeviceName;//�豸����
            public uint dwChannelNumer;//ͨ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] CardNumber;//����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string cTradeType;//��������
            public uint dwCash;//���׽��
        }

        //ATMר��
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




        ///*************�����쳣����(��Ϣ��ʽ, �ص���ʽ(����))****************/
        //public const int EXCEPTION_EXCHANGE = 32768;//�û�����ʱ�쳣
        //public const int EXCEPTION_AUDIOEXCHANGE = 32769;//�����Խ��쳣
        //public const int EXCEPTION_ALARM = 32770;//�����쳣
        //public const int EXCEPTION_PREVIEW = 32771;//����Ԥ���쳣
        //public const int EXCEPTION_SERIAL = 32772;//͸��ͨ���쳣
        //public const int EXCEPTION_RECONNECT = 32773;//Ԥ��ʱ����
        //public const int EXCEPTION_ALARMRECONNECT = 32774;//����ʱ����
        //public const int EXCEPTION_SERIALRECONNECT = 32775;//͸��ͨ������
        //public const int EXCEPTION_PLAYBACK = 32784;//�ط��쳣
        //public const int EXCEPTION_DISKFMT = 32785;//Ӳ�̸�ʽ��


        /*֡��ʽ*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMETYPECODE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] code;/* ���� */
        }

        //ATM����(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMEFORMAT_V30
        {
            public uint dwSize;
            public NET_DVR_IPADDR struATMIP;/* ATM IP��ַ */
            public uint dwATMType;/* ATM���� */
            public uint dwInputMode;/* ���뷽ʽ    0-�������� 1-������� 2-����ֱ������ 3-����ATM��������*/
            public uint dwFrameSignBeginPos;/* ���ı�־λ����ʼλ��*/
            public uint dwFrameSignLength;/* ���ı�־λ�ĳ��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* ���ı�־λ������ */
            public uint dwCardLengthInfoBeginPos;/* ���ų�����Ϣ����ʼλ�� */
            public uint dwCardLengthInfoLength;/* ���ų�����Ϣ�ĳ��� */
            public uint dwCardNumberInfoBeginPos;/* ������Ϣ����ʼλ�� */
            public uint dwCardNumberInfoLength;/* ������Ϣ�ĳ��� */
            public uint dwBusinessTypeBeginPos;/* �������͵���ʼλ�� */
            public uint dwBusinessTypeLength;/* �������͵ĳ��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* ���� */
            public ushort wATMPort;/* ���Ų�׽�˿ں�(����Э�鷽ʽ) */
            public ushort wProtocolType;/* ����Э������ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ATM����
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FRAMEFORMAT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sATMIP;/* ATM IP��ַ */
            public uint dwATMType;/* ATM���� */
            public uint dwInputMode;/* ���뷽ʽ    0-�������� 1-������� 2-����ֱ������ 3-����ATM��������*/
            public uint dwFrameSignBeginPos;/* ���ı�־λ����ʼλ��*/
            public uint dwFrameSignLength;/* ���ı�־λ�ĳ��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;/* ���ı�־λ������ */
            public uint dwCardLengthInfoBeginPos;/* ���ų�����Ϣ����ʼλ�� */
            public uint dwCardLengthInfoLength;/* ���ų�����Ϣ�ĳ��� */
            public uint dwCardNumberInfoBeginPos;/* ������Ϣ����ʼλ�� */
            public uint dwCardNumberInfoLength;/* ������Ϣ�ĳ��� */
            public uint dwBusinessTypeBeginPos;/* �������͵���ʼλ�� */
            public uint dwBusinessTypeLength;/* �������͵ĳ��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;/* ���� */
        }

        //SDK_V31 ATM
        /*��������*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FILTER
        {
            public byte byEnable;/*0,������;1,����*/
            public byte byMode;/*0,ASCII;1,HEX*/
            public byte byFrameBeginPos;// ���ı�־λ����ʼλ��     
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byFilterText;/*�����ַ���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*��ʼ��ʶ����*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IDENTIFICAT
        {
            public byte byStartMode;/*0,ASCII;1,HEX*/
            public byte byEndMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_FRAMETYPECODE struStartCode;/*��ʼ�ַ�*/
            public NET_DVR_FRAMETYPECODE struEndCode;/*�����ַ�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        /*������Ϣλ��*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LOCATION
        {
            public byte byOffsetMode;/*0,token;1,fix*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwOffsetPos;/*modeΪ1��ʱ��ʹ��*/
            public NET_DVR_FRAMETYPECODE struTokenCode;/*��־λ*/
            public byte byMultiplierValue;/*��־λ���ٴγ���*/
            public byte byEternOffset;/*���ӵ�ƫ����*/
            public byte byCodeMode;/*0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*������Ϣ����*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LENGTH
        {
            public byte byLengthMode;/*�������ͣ�0,variable;1,fix;2,get from package(���ÿ��ų���ʹ��)*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwFixLength;/*modeΪ1��ʱ��ʹ��*/
            public uint dwMaxLength;
            public uint dwMinLength;
            public byte byEndMode;/*�ս��0,ASCII;1,HEX*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_FRAMETYPECODE struEndCode;/*�ս��*/
            public uint dwLengthPos;/*lengthModeΪ2��ʱ��ʹ�ã����ų����ڱ����е�λ��*/
            public uint dwLengthLen;/*lengthModeΪ2��ʱ��ʹ�ã����ų��ȵĳ���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        /*OSD ���ӵ�λ��*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OSD_POSITION
        {
            public byte byPositionMode;/*���ӷ�񣬹�2�֣�0������ʾ��1��Custom*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPos_x;/*x���꣬positionmodeΪCustomʱʹ��*/
            public uint dwPos_y;/*y���꣬positionmodeΪCustomʱʹ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*������ʾ��ʽ*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_DATE_FORMAT
        {
            public byte byItem1;/*Month,0.mm;1.mmm;2.mmmm*/
            public byte byItem2;/*Day,0.dd;*/
            public byte byItem3;/*Year,0.yy;1.yyyy*/
            public byte byDateForm;/*0~5��3��item���������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;/*�ָ���*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*��ʾ�ָ���*/
            public byte byDisplayForm;/*0~5��3��item���������*///lili mode by lili
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        /*ʱ����ʾ��ʽ*/
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
            public string chSeprator;/*���ķָ�������ʱû��*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;/*��ʾ�ָ���*/
            public byte byDisplayForm;/*0~5��3��item���������*/
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
            public byte[] byChannel;/*���ӵ�ͨ��*/
            public uint dwDelayTime;/*������ʱʱ��*/
            public byte byEnableDelayTime;/*�Ƿ����õ�����ʱ�������˿�����ʱ����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_ACTION
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struActionCode;/*�������͵ȶ�Ӧ����*/
            public NET_DVR_FRAMETYPECODE struPreCode;/*�����ַ�ǰ���ַ�*/
            public byte byActionCodeMode;/*�������͵ȶ�Ӧ����0,ASCII;1,HEX*/
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
            public NET_DVR_FRAMETYPECODE struPreCode;/*�����ַ�ǰ���ַ�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_FRAMETYPE_NEW
        {
            public byte byEnable;/*�Ƿ�����0,������;1,����*/
            public byte byInputMode;/*���뷽ʽ:������������ڼ���������Э�顢����Э�顢���ڷ�����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAtmName;/*ATM ����*/
            public NET_DVR_IPADDR struAtmIp;/*ATM ��IP  */
            public ushort wAtmPort;/* ���Ų�׽�˿ں�(����Э�鷽ʽ) �򴮿ڷ������˿ں�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwAtmType;/*ATM ������*/
            public tagNET_DVR_IDENTIFICAT struIdentification;/*���ı�־*/
            public tagNET_DVR_FILTER struFilter;/*��������*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struCardNoPara;/*���ӿ�������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ACTION_TYPE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PACKAGE_ACTION[] struTradeActionPara;/*���ӽ�����Ϊ����*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struAmountPara;/*���ӽ��׽������*/
            public tagNET_DVR_ATM_PACKAGE_OTHERS struSerialNoPara;/*���ӽ����������*/
            public tagNET_DVR_OVERLAY_CHANNEL struOverlayChan;/*����ͨ������*/
            public tagNET_DVR_ATM_PACKAGE_DATE byRes4;/*�����������ã���ʱ����*/
            public tagNET_DVR_ATM_PACKAGE_TIME byRes5;/*����ʱ�����ã���ʱ����*/
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
            public uint dwAtmType;/*ATMЭ�����ͣ�ͬʱ��Ϊ�������*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = ATM_DESC_LEN)]
            public string chDesc;/*ATMЭ�������*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PROTOCOL
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ATM_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PROTOIDX[] struAtmProtoidx;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ATM_PROTOCOL_SORT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAtmNumPerSort;/*ÿ��Э����*/
        }

        /*****************************DS-6001D/F(begin)***************************/
        //DS-6001D Decoder
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//�����豸���ӵķ�����IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//�����豸���ӵķ��������û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//�����豸���ӵķ�����������
            public byte bySendMode;//�����豸���ӷ�����������ģʽ
            public byte byEncoderChannel;//�����豸���ӵķ�������ͨ����
            public ushort wEncoderPort;//�����豸���ӵķ������Ķ˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERSTATE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;//�����豸���ӵķ�����IP
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;//�����豸���ӵķ��������û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;//�����豸���ӵķ�����������
            public byte byEncoderChannel;//�����豸���ӵķ�������ͨ����
            public byte bySendMode;//�����豸���ӵķ�����������ģʽ
            public ushort wEncoderPort;//�����豸���ӵķ������Ķ˿ں�
            public uint dwConnectState;//�����豸���ӷ�������״̬
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;//����
        }

        /*�����豸�����붨��*/
        public const int NET_DEC_STARTDEC = 1;
        public const int NET_DEC_STOPDEC = 2;
        public const int NET_DEC_STOPCYCLE = 3;
        public const int NET_DEC_CONTINUECYCLE = 4;

        /*���ӵ�ͨ������*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP��ַ */
            public ushort wDVRPort;/* �˿ں� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            public byte byChannel;/* ͨ���� */
            public byte byLinkMode;/* ����ģʽ */
            public byte byLinkType;/* �������� 0�������� 1�������� */
        }

        /*ÿ������ͨ��������*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECINFO
        {
            public byte byPoolChans;/*ÿ·����ͨ���ϵ�ѭ��ͨ������, ���4ͨ�� 0��ʾû�н���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECPOOLNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANINFO[] struchanConInfo;
            public byte byEnablePoll;/*�Ƿ���Ѳ 0-�� 1-��*/
            public byte byPoolTime;/*��Ѳʱ�� 0-���� 1-10�� 2-15�� 3-20�� 4-30�� 5-45�� 6-1���� 7-2���� 8-5���� */
        }

        /*�����豸��������*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECCFG
        {
            public uint dwSize;
            public uint dwDecChanNum;/*����ͨ��������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECINFO[] struDecInfo;
        }

        //2005-08-01
        /* �����豸͸��ͨ������ */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PORTINFO
        {
            public uint dwEnableTransPort;/* �Ƿ�����͸��ͨ�� 0�������� 1������*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP��ַ */
            public ushort wDecoderPort;/* �˿ں� */
            public ushort wDVRTransPort;/* ����ǰ��DVR�Ǵ�485/232�����1��ʾ232����,2��ʾ485���� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string cReserve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PORTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRANSPARENTNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PORTINFO[] struTransPortInfo;/* ����0��ʾ232 ����1��ʾ485 */
        }

        /* ���������ļ��ط� */
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PLAYREMOTEFILE
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;/* DVR IP��ַ */
            public ushort wDecoderPort;/* �˿ں� */
            public ushort wLoadMode;/* �ط�����ģʽ 1�������� 2����ʱ�� */

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct mode_size
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                [FieldOffsetAttribute(0)]
                public byte[] byFile;/* �طŵ��ļ��� */

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct bytime
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*������Ƶ�û���*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* ���� */
                    public NET_DVR_TIME struStartTime;/* ��ʱ��طŵĿ�ʼʱ�� */
                    public NET_DVR_TIME struStopTime;/* ��ʱ��طŵĽ���ʱ�� */
                }
            }
        }

        /*��ǰ�豸��������״̬*/
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANSTATUS
        {
            public uint dwWorkType;/*������ʽ��1����Ѳ��2����̬���ӽ��롢3���ļ��ط����� 4����ʱ��ط�����*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/*���ӵ��豸ip*/
            public ushort wDVRPort;/*���Ӷ˿ں�*/
            public byte byChannel;/* ͨ���� */
            public byte byLinkMode;/* ����ģʽ */
            public uint dwLinkType;/*�������� 0�������� 1��������*/

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct objectInfo
            {
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct userInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;/*������Ƶ�û���*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* ���� */
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
                    public byte[] sUserName;/*������Ƶ�û���*/
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;/* ���� */
                    public NET_DVR_TIME struStartTime;/* ��ʱ��طŵĿ�ʼʱ�� */
                    public NET_DVR_TIME struStopTime;/* ��ʱ��طŵĽ���ʱ�� */
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

        //���ַ�����(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SHOWSTRINGINFO
        {
            public ushort wShowString;// Ԥ����ͼ�����Ƿ���ʾ�ַ�,0-����ʾ,1-��ʾ �����С704*576,�����ַ��Ĵ�СΪ32*32
            public ushort wStringSize;/* �����ַ��ĳ��ȣ����ܴ���44���ַ� */
            public ushort wShowStringTopLeftX;/* �ַ���ʾλ�õ�x���� */
            public ushort wShowStringTopLeftY;/* �ַ�������ʾλ�õ�y���� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 44)]
            public string sString;/* Ҫ��ʾ���ַ����� */
        }

        //�����ַ�(9000��չ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* Ҫ��ʾ���ַ����� */

            public void Init()
            {
                struStringInfo = new NET_DVR_SHOWSTRINGINFO[MAX_STRINGNUM_V30];
            }
        }

        //�����ַ���չ(8���ַ�)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_EX, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* Ҫ��ʾ���ַ����� */
        }

        //�����ַ�
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;/* Ҫ��ʾ���ַ����� */
        }

        /****************************DS9000�����ṹ(begin)******************************/

        /*EMAIL�����ṹ*/
        //��ԭ�ṹ���в���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struReceiver
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sName;/* �ռ������� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAddress;/* �ռ��˵�ַ */
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAccount;/* �˺�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_PWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/*���� */

            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct struSender
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sName;/* ���������� */
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sAddress;/* �����˵�ַ */
            }

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;/* smtp������ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;/* pop3������ */

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public struReceiver[] struStringInfo;/* ����������3���ռ��� */

            public byte byAttachment;/* �Ƿ������ */
            public byte bySmtpServerVerify;/* ���ͷ�����Ҫ��������֤ */
            public byte byMailInterval;/* mail interval */
            public byte byEnableSSL;//ssl�Ƿ�����9000_1.1
            public ushort wSmtpPort;//gmail��465����ͨ��Ϊ25  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 74, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        /*DVRʵ��Ѳ�����ݽṹ*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_PARA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;/* Ԥ�õ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseSpeed;/* Ѳ���ٶ� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.U2)]
            public ushort[] wDwellTime;/* ͣ��ʱ�� */
            public byte byEnableThisCruise;/* �Ƿ����� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
        /****************************DS9000�����ṹ(end)******************************/

        //ʱ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIMEPOINT
        {
            public uint dwMonth;//�� 0-11��ʾ1-12����
            public uint dwWeekNo;//�ڼ��� 0����1�� 1����2�� 2����3�� 3����4�� 4�����һ��
            public uint dwWeekDate;//���ڼ� 0�������� 1������һ 2�����ڶ� 3�������� 4�������� 5�������� 6��������
            public uint dwHour;//Сʱ    ��ʼʱ��0��23 ����ʱ��1��23
            public uint dwMin;//��    0��59
        }

        //����ʱ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ZONEANDDST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public uint dwEnableDST;//�Ƿ�������ʱ�� 0�������� 1������
            public byte byDSTBias;//����ʱƫ��ֵ��30min, 60min, 90min, 120min, �Է��Ӽƣ�����ԭʼ��ֵ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_TIMEPOINT struBeginPoint;//��ʱ�ƿ�ʼʱ��
            public NET_DVR_TIMEPOINT struEndPoint;//��ʱ��ֹͣʱ��
        }

        //ͼƬ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_JPEGPARA
        {
            /*ע�⣺��ͼ��ѹ���ֱ���ΪVGAʱ��֧��0=CIF, 1=QCIF, 2=D1ץͼ��
            ���ֱ���Ϊ3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA,7=XVGA, 8=HD900p
            ��֧�ֵ�ǰ�ֱ��ʵ�ץͼ*/
            public ushort wPicSize;/* 0=CIF, 1=QCIF, 2=D1 3=UXGA(1600x1200), 4=SVGA(800x600), 5=HD720p(1280x720),6=VGA*/
            public ushort wPicQuality;/* ͼƬ����ϵ�� 0-��� 1-�Ϻ� 2-һ�� */
        }

        //ȫ������
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

        //����ģʽ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_MODE
        {
            public uint dwSize;
            public byte byMode;  // ����ģʽ��0- ��ͨģʽ��1- ר��ģʽ 
            public byte byThermometryROIEnabled; //����ROIʹ�� 0-���� 1-������ 2-���������ڻ�����ݿ��ǣ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        /* aux video out parameter */
        //���������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUXOUTCFG
        {
            public uint dwSize;
            public uint dwAlarmOutChan;/* ѡ�񱨾������󱨾�ͨ���л�ʱ�䣺1��������ͨ��: 0:�����/1:��1/2:��2/3:��3/4:��4 */
            public uint dwAlarmChanSwitchTime;/* :1�� - 10:10�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAuxSwitchTime;/* ��������л�ʱ��: 0-���л�,1-5s,2-10s,3-20s,4-30s,5-60s,6-120s,7-300s */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT * MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] byAuxOrder;/* �������Ԥ��˳��, 0xff��ʾ��Ӧ�Ĵ��ڲ�Ԥ�� */
        }

        //ntp
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NTPPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sNTPServer;/* Domain Name or IP addr of NTP server */
            public ushort wInterval;/* adjust time interval(hours) */
            public byte byEnableNTP;/* enable NPT client 0-no��1-yes*/
            public byte cTimeDifferenceH;/* ����ʱ�׼ʱ��� Сʱƫ��-12 ... +13 */
            public byte cTimeDifferenceM;/* ����ʱ�׼ʱ��� ����ƫ��0, 30, 45*/
            public byte res1;
            public ushort wNtpPort; /* ntp server port 9000���� �豸Ĭ��Ϊ123*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
        }

        //ddns
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS�˺��û���/���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName; /* ���� */
            public byte byEnableDDNS;/*�Ƿ�Ӧ�� 0-��1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_EX
        {
            public byte byHostIndex;/* 0-Hikvision DNS 1��Dyndns 2��PeanutHull(������)*/
            public byte byEnableDDNS;/*�Ƿ�Ӧ��DDNS 0-��1-��*/
            public ushort wDDNSPort;/* DDNS�˿ں� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS�û���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* DDNS���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* �豸�䱸��������ַ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNS ��Ӧ�ķ�������ַ��������IP��ַ������ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //9000��չ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struDDNS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;/* DDNS�˺��û���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;/* �豸�䱸��������ַ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;/* DDNSЭ���Ӧ�ķ�������ַ��������IP��ַ������ */
            public ushort wDDNSPort;/* �˿ں� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_V30
        {
            public byte byEnableDDNS;
            public byte byHostIndex;/* 0-Hikvision DNS(����) 1��Dyndns 2��PeanutHull(������)*/
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
            public byte[] sUsername;/* �ʼ��˺�/���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sMailAddr;/* email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr1;/* �ϴ�����/�쳣�ȵ�email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //�����������
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETAPPCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDNSIp; /* DNS��������ַ */
            public NET_DVR_NTPPARA struNtpClientParam;/* NTP���� */
            public NET_DVR_DDNSPARA struDDNSClientParam;/* DDNS���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 464, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* ���� */
        }

        //nfs�ṹ����
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

        //Ѳ��������(HIK IP����ר��)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_POINT
        {
            public byte PresetNum;//Ԥ�õ�
            public byte Dwell;//ͣ��ʱ��
            public byte Speed;//�ٶ�
            public byte Reserve;//����

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
            public NET_DVR_CRUISE_POINT[] struCruisePoint;//���֧��32��Ѳ����

            public void Init()
            {
                struCruisePoint = new NET_DVR_CRUISE_POINT[32];
                for (int i = 0; i < 32; i++)
                {
                    struCruisePoint[i].Init();
                }
            }
        }

        /************************************��·������(begin)***************************************/
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
            public string sDVRIP;/* DVR IP��ַ */
            public ushort wDVRPort;/* �˿ں� */
            public byte byChannel;/* ͨ���� */
            public byte byTransProtocol;/* ����Э������ 0-TCP, 1-UDP */
            public byte byTransMode;/* ��������ģʽ 0�������� 1��������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* ���������½�ʺ� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ����������� */
        }

        //����/ֹͣ��̬����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DYNAMIC_DEC
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* ��̬����ͨ����Ϣ */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
        {
            public uint dwSize;
            public uint dwIsLinked;/* ����ͨ��״̬ 0������ 1���������� 2�������� 3-���ڽ��� */
            public uint dwStreamCpRate;/* Stream copy rate, X kbits/second */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cRes;/* ���� */
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* ����ͨ����Ϣ */
            public uint dwDecState;/* 0-��̬���� 1��ѭ������ 2����ʱ��ط� 3�����ļ��ط� */
            public NET_DVR_TIME StartTime;/* ��ʱ��طſ�ʼʱ�� */
            public NET_DVR_TIME StopTime;/* ��ʱ��ط�ֹͣʱ�� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* ���ļ��ط��ļ��� */
        }

        //���ӵ�ͨ������ 2007-11-05
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DECCHANINFO
        {
            public uint dwEnable;/* �Ƿ����� 0���� 1������*/
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;/* ��ѭ����ͨ����Ϣ */
        }

        //2007-11-05 ����ÿ������ͨ��������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_LOOP_DECINFO
        {
            public uint dwSize;
            public uint dwPoolTime;/*��Ѳʱ�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_DECCHANINFO[] struchanConInfo;
        }

        //2007-12-22
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TTY_CONFIG
        {
            public byte baudrate;/* ������ */
            public byte databits;/* ����λ */
            public byte stopbits;/* ֹͣλ */
            public byte parity;/* ��żУ��λ */
            public byte flowcontrol;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* ��ǰ͸��ͨ���Ƿ�� 0���ر� 1���� */
            /*
             *    ��·������������1��485���ڣ�1��232���ڶ�������Ϊ͸��ͨ��,�豸�ŷ������£�
             *    0 RS485
             *    1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *    Զ�̴��������������,һ��RS232��һ��RS485
             *    1��ʾ232����
             *    2��ʾ485����
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte res1;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;/* ���� */
            public TTY_CONFIG RemoteSerialDevCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;/* ������·232͸��ͨ����ȫ˫���� ȡֵ1��MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* ������·485͸��ͨ����ȫ˫���� ȡֵ1��MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_TRAN_CHAN_INFO[] struTranInfo;/*ͬʱ֧�ֽ���MAX_SERIAL_NUM��͸��ͨ��*/
        }

        //2007-12-24 Merry Christmas Eve...
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;/* DVR IP��ַ */
            public ushort wDVRPort;/* �˿ں� */
            public byte byChannel;/* ͨ���� */
            public byte byReserve;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* �û��� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ���� */
            public uint dwPlayMode;/* 0�����ļ� 1����ʱ��*/
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_CONTROL
        {
            public uint dwSize;
            public uint dwPlayCmd;/* �������� ���ļ���������*/
            public uint dwCmdParam;/* ����������� */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS
        {
            public uint dwSize;
            public uint dwCurMediaFileLen;/* ��ǰ���ŵ�ý���ļ����� */
            public uint dwCurMediaFilePosition;/* ��ǰ�����ļ��Ĳ���λ�� */
            public uint dwCurMediaFileDuration;/* ��ǰ�����ļ�����ʱ�� */
            public uint dwCurPlayTime;/* ��ǰ�Ѿ����ŵ�ʱ�� */
            public uint dwCurMediaFIleFrames;/* ��ǰ�����ļ�����֡�� */
            public uint dwCurDataType;/* ��ǰ������������ͣ�19-�ļ�ͷ��20-�����ݣ� 21-���Ž�����־ */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 72, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //2009-4-11 added by likui ��·������new
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_MATRIX_PASSIVEMODE
        {
            public ushort wTransProtol;//����Э�飬0-TCP, 1-UDP, 2-MCAST
            public ushort wPassivePort;//UDP�˿�, TCPʱĬ��
            // char    sMcastIP[16];  //TCP,UDPʱ��Ч, MCASTʱΪ�ಥ��ַ
            public NET_DVR_IPADDR struMcastIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagDEV_CHAN_INFO
        {
            public NET_DVR_IPADDR struIP;/* DVR IP��ַ */
            public ushort wDVRPort;/* �˿ں� */
            public byte byChannel;/* ͨ���� */
            public byte byTransProtocol;/* ����Э������0-TCP��1-UDP */
            public byte byTransMode;/* ��������ģʽ 0�������� 1��������*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 71, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;/* ���������½�ʺ� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;/* ����������� */
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;/* ��ǰ͸��ͨ���Ƿ�� 0���ر� 1���� */
            /*
             *    ��·������������1��485���ڣ�1��232���ڶ�������Ϊ͸��ͨ��,�豸�ŷ������£�
             *    0 RS485
             *    1 RS232 Console
             */
            public byte byLocalSerialDevice;/* Local serial device */
            /*
             *    Զ�̴��������������,һ��RS232��һ��RS485
             *    1��ʾ232����
             *    2��ʾ485����
             */
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte byRes1;/* ���� */
            public NET_DVR_IPADDR struRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            public byte byIsEstablished;/* ͸��ͨ�������ɹ���־��0-û�гɹ���1-�����ɹ� */
            public byte byRes2;/* ���� */
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
            public byte by232IsDualChan;/* ������·232͸��ͨ����ȫ˫���� ȡֵ1��MAX_SERIAL_NUM */
            public byte by485IsDualChan;/* ������·485͸��ͨ����ȫ˫���� ȡֵ1��MAX_SERIAL_NUM */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] vyRes;/* ���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagMATRIX_TRAN_CHAN_INFO[] struTranInfo;/*ͬʱ֧�ֽ���MAX_SERIAL_NUM��͸��ͨ��*/
        }

        /*��ý���������������*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MEDIA_SERVER_CFG
        {
            public byte byValid;/*�Ƿ�������ý�������ȡ��,0��ʾ��Ч����0��ʾ��Ч*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struDevIP;
            public ushort wDevPort;/*��ý��������˿�*/
            public byte byTransmitType;/*����Э������ 0-TCP��1-UDP*/
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
            public uint dwEnable;/* �Ƿ����� 0���� 1������*/
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;/* ��ѭ����ͨ����Ϣ */
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
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;/*��ý�����������*/
            public tagDEV_CHAN_INFO struDevChanInfo;/* ����ͨ����Ϣ */
            public uint dwDecState;/* 0-��̬���� 1��ѭ������ 2����ʱ��ط� 3�����ļ��ط� */
            public NET_DVR_TIME StartTime;/* ��ʱ��طſ�ʼʱ�� */
            public NET_DVR_TIME StopTime;/* ��ʱ��ط�ֹͣʱ�� */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;/* ���ļ��ط��ļ��� */
            public uint dwGetStreamMode;/*ȡ��ģʽ:1-������2-����*/
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
            public byte[] byVGAWindowMode;/*VGA֧�ֵĴ���ģʽ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byBNCWindowMode;/*BNC֧�ֵĴ���ģʽ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        //�ϴ�logo�ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_DISP_LOGOCFG
        {
            public uint dwCorordinateX;//ͼƬ��ʾ����X����
            public uint dwCorordinateY;//ͼƬ��ʾ����Y����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byFlash;//�Ƿ���˸1-��˸��0-����˸
            public byte byTranslucent;//�Ƿ��͸��1-��͸����0-����͸��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//����
            public uint dwLogoSize;//LOGO��С������BMP���ļ�ͷ
        }

        /*��������*/
        public const int NET_DVR_ENCODER_UNKOWN = 0;/*δ֪�����ʽ*/
        public const int NET_DVR_ENCODER_H264 = 1;/*HIK 264*/
        public const int NET_DVR_ENCODER_S264 = 2;/*Standard H264*/
        public const int NET_DVR_ENCODER_MPEG4 = 3;/*MPEG4*/
        /* �����ʽ */
        public const int NET_DVR_STREAM_TYPE_UNKOWN = 0;/*δ֪�����ʽ*/
        public const int NET_DVR_STREAM_TYPE_HIKPRIVT = 1; /*�����Զ�������ʽ*/
        public const int NET_DVR_STREAM_TYPE_TS = 7;/* TS��� */
        public const int NET_DVR_STREAM_TYPE_PS = 8;/* PS��� */
        public const int NET_DVR_STREAM_TYPE_RTP = 9;/* RTP��� */

        /*����ͨ��״̬*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_STATUS
        {
            public byte byDecodeStatus;/*��ǰ״̬:0:δ������1����������*/
            public byte byStreamType;/*��������*/
            public byte byPacketType;/*�����ʽ*/
            public byte byRecvBufUsage;/*���ջ���ʹ����*/
            public byte byDecBufUsage;/*���뻺��ʹ����*/
            public byte byFpsDecV;/*��Ƶ����֡��*/
            public byte byFpsDecA;/*��Ƶ����֡��*/
            public byte byCpuLoad;/*DSP CPUʹ����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDecodedV;/*�������Ƶ֡*/
            public uint dwDecodedA;/*�������Ƶ֡*/
            public ushort wImgW;/*��������ǰ��ͼ���С,��*/
            public ushort wImgH; //��
            public byte byVideoFormat;/*��Ƶ��ʽ:0-NON,NTSC--1,PAL--2*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*��ʾͨ��״̬*/
        public const int NET_DVR_MAX_DISPREGION = 16;         /*ÿ����ʾͨ����������ʾ�Ĵ���*/
        //VGA�ֱ��ʣ�Ŀǰ���õ��ǣ�VGA_THS8200_MODE_XGA_60HZ��VGA_THS8200_MODE_SXGA_60HZ��
        //
        public enum VGA_MODE
        {
            VGA_NOT_AVALIABLE,
            VGA_THS8200_MODE_SVGA_60HZ,//��800*600��
            VGA_THS8200_MODE_SVGA_75HZ, //��800*600��
            VGA_THS8200_MODE_XGA_60HZ,//��1024*768��
            VGA_THS8200_MODE_XGA_70HZ, //��1024*768��
            VGA_THS8200_MODE_SXGA_60HZ,//��1280*1024��
            VGA_THS8200_MODE_720P_60HZ,//��1280*720 ��
            VGA_THS8200_MODE_1080i_60HZ,//��1920*1080��
            VGA_THS8200_MODE_1080P_30HZ,//��1920*1080��
            VGA_THS8200_MODE_1080P_25HZ,//��1920*1080��
            VGA_THS8200_MODE_UXGA_30HZ,//��1600*1200��
        }

        /*��Ƶ��ʽ��׼*/
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
            public byte byAudio;/*��Ƶ�Ƿ���,0-��1-��*/
            public byte byAudioWindowIdx;/*��Ƶ�����Ӵ���*/
            public byte byVgaResolution;/*VGA�ķֱ���*/
            public byte byVedioFormat;/*��Ƶ��ʽ��1:NTSC,2:PAL,0-NON*/
            public uint dwWindowMode;/*����ģʽ�������������ȡ��Ŀǰ֧��1,2,4,9,16*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*�����Ӵ��ڹ����Ľ���ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS
        {
            public byte byDispStatus;/*��ʾ״̬��0��δ��ʾ��1��������ʾ*/
            public byte byBVGA; /*VGA/BNC*/
            public byte byVideoFormat;/*��Ƶ��ʽ:1:NTSC,2:PAL,0-NON*/
            public byte byWindowMode;/*��ǰ����ģʽ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;/*�����Ӵ��ڹ����Ľ���ͨ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_DVR_MAX_DISPREGION, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;/*ÿ���ӻ������ʾ֡��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int MAX_DECODECHANNUM = 32;//��·������������ͨ����
        public const int MAX_DISPCHANNUM = 24;//��·�����������ʾͨ����

        /*�������豸״̬*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR__DECODER_WORK_STATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;/*����ͨ��״̬*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISPCHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS[] struDispChanStatus;/*��ʾͨ��״̬*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;/*��������״̬*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;/*�������״̬*/
            public byte byAudioInChanStatus;/*�����Խ�״̬*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        /************************************��·������(end)***************************************/

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_EMAILCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sPassWord;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sFromName;/* Sender *///�ַ����еĵ�һ���ַ������һ���ַ�������"@",�����ַ�����Ҫ��"@"�ַ�
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
            public byte byServerType;/* Email server type: 0-SMTP, 1-POP, 2-IMTP��*/
            public byte byUseAuthen;/* Email server authentication method: 1-enable, 0-disable */
            public byte byAttachment;/* enable attachment */
            public byte byMailinterval;/* mail interval 0-2s, 1-3s, 2-4s. 3-5s*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_NEW
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struLowCompression;//��ʱ¼��
            public NET_DVR_COMPRESSION_INFO_EX struEventCompression;//�¼�����¼��
        }

        //���λ����Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZPOS
        {
            public ushort wAction;//��ȡʱ���ֶ���Ч
            public ushort wPanPos;//ˮƽ����
            public ushort wTiltPos;//��ֱ����
            public ushort wZoomPos;//�䱶����
        }

        //�����Χ��Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZSCOPE
        {
            public ushort wPanPosMin;//ˮƽ����min
            public ushort wPanPosMax;//ˮƽ����max
            public ushort wTiltPosMin;//��ֱ����min
            public ushort wTiltPosMax;//��ֱ����max
            public ushort wZoomPosMin;//�䱶����min
            public ushort wZoomPosMax;//�䱶����max
        }

        //rtsp���� ipcameraר��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RTSPCFG
        {
            public uint dwSize;//����
            public ushort wPort;//rtsp�����������˿�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 54, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;//Ԥ��
        }

        /********************************�ӿڲ����ṹ(begin)*********************************/

        //NET_DVR_Login()�����ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//���к�
            public byte byAlarmInPortNum;//DVR�����������
            public byte byAlarmOutPortNum;//DVR�����������
            public byte byDiskNum;//DVRӲ�̸���
            public byte byDVRType;//DVR����, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;//DVR ͨ������
            public byte byStartChan;//��ʼͨ����,����DVS-1,DVR - 1
        }

        //NET_DVR_Login_V30()�����ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//���к�
            public byte byAlarmInPortNum;//�����������
            public byte byAlarmOutPortNum;//�����������
            public byte byDiskNum;//Ӳ�̸���
            public byte byDVRType;//�豸����, 1:DVR 2:ATM DVR 3:DVS ...
            public byte byChanNum;//ģ��ͨ������
            public byte byStartChan;//��ʼͨ����,����DVS-1,DVR - 1
            public byte byAudioChanNum;//����ͨ����
            public byte byIPChanNum;//�������ͨ������  
            public byte byZeroChanNum;      //��ͨ��������� //2010-01-16
            public byte byMainProto;      //����������Э������ 0-private, 1-rtsp,2-ͬʱ֧��private��rtsp
            public byte bySubProto;    //����������Э������0-private, 1-rtsp,2-ͬʱ֧��private��rtsp
            public byte bySupport;        //������λ����Ϊ0��ʾ��֧�֣�1��ʾ֧�֣�
            //bySupport & 0x1, ��ʾ�Ƿ�֧����������
            //bySupport & 0x2, ��ʾ�Ƿ�֧�ֱ���
            //bySupport & 0x4, ��ʾ�Ƿ�֧��ѹ������������ȡ
            //bySupport & 0x8, ��ʾ�Ƿ�֧�ֶ�����
            //bySupport & 0x10, ��ʾ֧��Զ��SADP
            //bySupport & 0x20, ��ʾ֧��Raid������
            //bySupport & 0x40, ��ʾ֧��IPSAN Ŀ¼����
            //bySupport & 0x80, ��ʾ֧��rtp over rtsp
            public byte bySupport1;        // ���������䣬λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport1 & 0x1, ��ʾ�Ƿ�֧��snmp v30
            //bySupport1 & 0x2, ֧�����ֻطź�����
            //bySupport1 & 0x4, �Ƿ�֧�ֲ������ȼ�    
            //bySupport1 & 0x8, �����豸�Ƿ�֧�ֲ���ʱ�����չ
            //bySupport1 & 0x10, ��ʾ�Ƿ�֧�ֶ������������33����
            //bySupport1 & 0x20, ��ʾ�Ƿ�֧��rtsp over http    
            //bySupport1 & 0x80, ��ʾ�Ƿ�֧�ֳ����±�����Ϣ2012-9-28, �һ���ʾ�Ƿ�֧��NET_DVR_IPPARACFG_V40�ṹ��
            public byte bySupport2; /*������λ����Ϊ0��ʾ��֧�֣���0��ʾ֧��          
                     bySupport2 & 0x1, ��ʾ�������Ƿ�֧��ͨ��URLȡ������
                     bySupport2 & 0x2,  ��ʾ֧��FTPV40
                     bySupport2 & 0x4,  ��ʾ֧��ANR
                     bySupport2 & 0x8,  ��ʾ֧��CCD��ͨ����������
                     bySupport2 & 0x10,  ��ʾ֧�ֲ��������ش���Ϣ����֧��ץ�Ļ����� ���ϱ����ṹ��
                     bySupport2 & 0x20,  ��ʾ�Ƿ�֧�ֵ�����ȡ�豸״̬����
            bySupport2 & 0x40,  ��ʾ�Ƿ������������豸*/
            public ushort wDevType;              //�豸�ͺ�
            public byte bySupport3; //��������չ��λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport3 & 0x1, ��ʾ�Ƿ������
            // bySupport3 & 0x4 ��ʾ֧�ְ������ã� ������� ͨ��ͼ��������������������IP�������롢������������
            // �û��������豸����״̬��JPEGץͼ����ʱ��ʱ��ץͼ��Ӳ��������� 
            //bySupport3 & 0x8Ϊ1 ��ʾ֧��ʹ��TCPԤ����UDPԤ�����ಥԤ���е�"��ʱԤ��"�ֶ���������ʱԤ������������ʹ�����ַ�ʽ������ʱԤ����������bySupport3 & 0x8Ϊ0ʱ����ʹ�� "˽����ʱԤ��"Э�顣
            //bySupport3 & 0x10 ��ʾ֧��"��ȡ����������Ҫ״̬��V40��"��
            //bySupport3 & 0x20 ��ʾ�Ƿ�֧��ͨ��DDNS��������ȡ��
            public byte byMultiStreamProto;//�Ƿ�֧�ֶ�����,��λ��ʾ,0-��֧��,1-֧��,bit1-����3,bit2-����4,bit7-��������bit-8������
            public byte byStartDChan;  //��ʼ����ͨ����,0��ʾ��Ч
            public byte byStartDTalkChan;    //��ʼ���ֶԽ�ͨ���ţ�������ģ��Խ�ͨ���ţ�0��ʾ��Ч
            public byte byHighDChanNum;  //����ͨ����������λ
            public byte bySupport4;        //��������չ��λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport4 & 0x4��ʾ�Ƿ�֧��ƴ��ͳһ�ӿ�
            // bySupport4 & 0x80 ֧���豸�ϴ����ı���ʹ�ܡ���ʾ�жϵ��ýӿ��� NET_DVR_PDC_RULE_CFG_V42���� NET_DVR_PDC_RULE_CFG_V41
            public byte byLanguageType;// ֧����������,��λ��ʾ,ÿһλ0-��֧��,1-֧��  
            //  byLanguageType ����0 ��ʾ ���豸
            //  byLanguageType & 0x1��ʾ֧������
            //  byLanguageType & 0x2��ʾ֧��Ӣ��
            public byte byVoiceInChanNum;   //��Ƶ����ͨ���� 
            public byte byStartVoiceInChanNo; //��Ƶ������ʼͨ���� 0��ʾ��Ч
            public byte bySupport5;  //��λ��ʾ,0-��֧��,1-֧��,bit0-֧�ֶ�����
            //bySupport5 &0x01��ʾ֧��wEventTypeEx ,����dwEventType ���¼����ͣ�֧����Ϊ�¼���չ��--��ռס����ֹ��ͻ
            //bySupport5 &0x04��ʾ�Ƿ�֧��ʹ����չ�ĳ���ģʽ�ӿ�
            /*
               bySupport5 &0x08 �豸���ظ�ֵ��ʾ�Ƿ�֧�ּƻ�¼������V40�ӿ�Э��(DVR_SET_RECORDCFG_V40/ DVR_GET_RECORDCFG_V40)(�ڸ�Э�����豸֧����������13������)
               ֮ǰ�Ĳ��ַ������豸��֧��¼������13��������¼������13�������֧�֣�ͳһת����¼������3���ݴ�����SDKͨ������̽�⴦��)
            */
            public byte bySupport6;   //��������λ��ʾ��0-��֧��,1-֧��
            //bySupport6 0x1  ��ʾ�豸�Ƿ�֧��ѹ�� 
            //bySupport6 0x2 ��ʾ�Ƿ�֧����ID��ʽ��������Դ��չ���DVR_SET_STREAM_SRC_INFO_V40
            //bySupport6 0x4 ��ʾ�Ƿ�֧���¼�����V40�ӿ�
            //bySupport6 0x8 ��ʾ�Ƿ�֧����չ���������������
            //bySupport6 0x40��ʾͼƬ��ѯ���V40��չ
            public byte byMirrorChanNum;    //����ͨ��������<¼�����������ڱ�ʾ����ͨ��>
            public ushort wStartMirrorChanNo;  //��ʼ����ͨ����
            public byte bySupport7;   //����,��λ��ʾ,0-��֧��,1-֧��
            // bySupport7 & 0x1  ��ʾ�豸�Ƿ�֧�� INTER_VCA_RULECFG_V42 ��չ
            // bySupport7 & 0x2  ��ʾ�豸�Ƿ�֧�� IPC HVT ģʽ��չ
            // bySupport7 & 0x04  ��ʾ�豸�Ƿ�֧�� ��������ʱ��
            // bySupport7 & 0x08  ��ʾ������̨PTZλ��ʱ���Ƿ�֧�ִ�ͨ����
            // bySupport7 & 0x10  ��ʾ�豸�Ƿ�֧��˫ϵͳ��������
            // bySupport7 & 0x20  ��ʾ�豸�Ƿ�֧�� OSD�ַ����� V50
            // bySupport7 & 0x40  ��ʾ�豸�Ƿ�֧�� ���Ӹ��٣����������
            // bySupport7 & 0x80  ��ʾ�豸�Ƿ�֧�� ���ļ���
            public byte byRes2;
        }

        //sdk���绷��ö�ٱ���������Զ������
        public enum SDK_NETWORK_ENVIRONMENT
        {
            LOCAL_AREA_NETWORK = 0,
            WIDE_AREA_NETWORK,
        }

        //��ʾģʽ
        public enum DISPLAY_MODE
        {
            NORMALMODE = 0,
            OVERLAYMODE
        }

        //����ģʽ
        public enum SEND_MODE
        {
            PTOPTCPMODE = 0,
            PTOPUDPMODE,
            MULTIMODE,
            RTPMODE,
            RESERVEDMODE
        }

        //ץͼģʽ
        public enum CAPTURE_MODE
        {
            BMP_MODE = 0,  //BMPģʽ
            JPEG_MODE = 1  //JPEGģʽ 
        }

        //ʵʱ����ģʽ
        public enum REALSOUND_MODE
        {
            MONOPOLIZE_MODE = 1,//��ռģʽ
            SHARE_MODE = 2  //����ģʽ
        }



        //SDK״̬��Ϣ(9000����)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKSTATE
        {
            public uint dwTotalLoginNum;//��ǰlogin�û���
            public uint dwTotalRealPlayNum;//��ǰrealplay·��
            public uint dwTotalPlayBackNum;//��ǰ�طŻ�����·��
            public uint dwTotalAlarmChanNum;//��ǰ��������ͨ��·��
            public uint dwTotalFormatNum;//��ǰӲ�̸�ʽ��·��
            public uint dwTotalFileSearchNum;//��ǰ��־���ļ�����·��
            public uint dwTotalLogSearchNum;//��ǰ��־���ļ�����·��
            public uint dwTotalSerialNum;//��ǰ͸��ͨ��·��
            public uint dwTotalUpgradeNum;//��ǰ����·��
            public uint dwTotalVoiceComNum;//��ǰ����ת��·��
            public uint dwTotalBroadCastNum;//��ǰ�����㲥·��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //SDK����֧����Ϣ(9000����)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKABL
        {
            public uint dwMaxLoginNum;//���login�û��� MAX_LOGIN_USERS
            public uint dwMaxRealPlayNum;//���realplay·�� WATCH_NUM
            public uint dwMaxPlayBackNum;//���طŻ�����·�� WATCH_NUM
            public uint dwMaxAlarmChanNum;//���������ͨ��·�� ALARM_NUM
            public uint dwMaxFormatNum;//���Ӳ�̸�ʽ��·�� SERVER_NUM
            public uint dwMaxFileSearchNum;//����ļ�����·�� SERVER_NUM
            public uint dwMaxLogSearchNum;//�����־����·�� SERVER_NUM
            public uint dwMaxSerialNum;//���͸��ͨ��·�� SERVER_NUM
            public uint dwMaxUpgradeNum;//�������·�� SERVER_NUM
            public uint dwMaxVoiceComNum;//�������ת��·�� SERVER_NUM
            public uint dwMaxBroadCastNum;//��������㲥·�� MAX_CASTNUM
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        //�����豸��Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            public byte byUserIDValid;/* userid�Ƿ���Ч 0-��Ч��1-��Ч */
            public byte bySerialValid;/* ���к��Ƿ���Ч 0-��Ч��1-��Ч */
            public byte byVersionValid;/* �汾���Ƿ���Ч 0-��Ч��1-��Ч */
            public byte byDeviceNameValid;/* �豸�����Ƿ���Ч 0-��Ч��1-��Ч */
            public byte byMacAddrValid; /* MAC��ַ�Ƿ���Ч 0-��Ч��1-��Ч */
            public byte byLinkPortValid;/* login�˿��Ƿ���Ч 0-��Ч��1-��Ч */
            public byte byDeviceIPValid;/* �豸IP�Ƿ���Ч 0-��Ч��1-��Ч */
            public byte bySocketIPValid;/* socket ip�Ƿ���Ч 0-��Ч��1-��Ч */
            public int lUserID; /* NET_DVR_Login()����ֵ, ����ʱ��Ч */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;/* ���к� */
            public uint dwDeviceVersion;/* �汾��Ϣ ��16λ��ʾ���汾����16λ��ʾ�ΰ汾*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;/* �豸���� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;/* MAC��ַ */
            public ushort wLinkPort; /* link port */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;/* IP��ַ */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;/* ���������ϴ�ʱ��socket IP��ַ */
            public byte byIpProtocol; /* IpЭ�� 0-IPV4, 1-IPV6 */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SYSTEM_TIME
        {
            public ushort  wYear;           //��
            public ushort  wMonth;          //��
            public ushort  wDay;            //��
            public ushort  wHour;           //ʱ
            public ushort  wMinute;      //��
            public ushort  wSecond;      //��
            public ushort  wMilliSec;    //����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_AIOP_VIDEO_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //�豸����ͨ����ͨ���ţ�
            public NET_DVR_SYSTEM_TIME struTime; //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string  szTaskID;     //��Ƶ����ID����������Ƶ�����ɷ�
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize; //��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //���ģ�Ͱ�ID������ƥ��AIOP�ļ�����ݽ���������ͨ��URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)��ȡ��ǰ�豸���ص�ģ�Ͱ���label description��Ϣ��
            public IntPtr pBufferAIOPData;  //AIOPDdata����
            public IntPtr pBufferPicture;//��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_PICTURE_HEAD
        {
            public uint dwSize;//dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public NET_DVR_SYSTEM_TIME struTime;  //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string  szPID;//͸���·���ͼƬID��������ͼƬ�����ɷ�
            public uint dwAIOPDataSize;//dwSize = sizeof(AIOP_DATA)
            public byte  byStatus;//״ֵ̬��0-�ɹ���1-ͼƬ��С����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;//���ģ�Ͱ�ID������ƥ��AIOP�ļ�����ݽ�����
            public IntPtr pBufferAIOPData;//AIOPDdata����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_POLLING_SNAP_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //�豸����ͨ����ͨ���ţ�
            public NET_DVR_SYSTEM_TIME struTime;     //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szTaskID;     //��Ƶ����ID����������Ƶ�����ɷ�
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize;    //��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //���ģ�Ͱ�ID������ƥ��AIOP�ļ�����ݽ���������ͨ��URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)��ȡ��ǰ�豸���ص�ģ�Ͱ���label description��Ϣ��
            public IntPtr pBufferAIOPData;  //AIOPDdata����
            public IntPtr pBufferPicture;//��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_AIOP_POLLING_VIDEO_HEAD
        {
            public uint dwSize;       //dwSize = sizeof(NET_AIOP_VIDEO_HEAD)
            public uint dwChannel;    //�豸����ͨ����ͨ���ţ�
            public NET_DVR_SYSTEM_TIME struTime;     //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szTaskID;     //��Ƶ����ID����������Ƶ�����ɷ�
            public uint dwAIOPDataSize;   //dwSize = sizeof(AIOP_DATA)
            public uint dwPictureSize;    //��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMPID;        //���ģ�Ͱ�ID������ƥ��AIOP�ļ�����ݽ���������ͨ��URI(GET /ISAPI/Intelligent/AIOpenPlatform/algorithmModel/management?format=json)��ȡ��ǰ�豸���ص�ģ�Ͱ���label description��Ϣ��
            public IntPtr pBufferAIOPData;  //AIOPDdata����
            public IntPtr pBufferPicture;//��Ӧ����ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 184, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //Ӳ������ʾ�������(�ӽṹ)
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

        //Ӳ����Ԥ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARDINFO
        {
            public int lChannel;//ͨ����
            public int lLinkMode;//���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ�ӣ�0��30λ��ʾ�������ӷ�ʽ:0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-�绰�ߣ�5��128k������6��256k������7��384k������8��512k������
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string sMultiCastIP;
            public NET_DVR_DISPLAY_PARA struDisplayPara;
        }

        //¼���ļ�����
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FIND_DATA
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//�ļ���
            public NET_DVR_TIME struStartTime;//�ļ��Ŀ�ʼʱ��
            public NET_DVR_TIME struStopTime;//�ļ��Ľ���ʱ��
            public uint dwFileSize;//�ļ��Ĵ�С
        }

        //¼���ļ�����(9000)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//�ļ���
            public NET_DVR_TIME struStartTime;//�ļ��Ŀ�ʼʱ��
            public NET_DVR_TIME struStopTime;//�ļ��Ľ���ʱ��
            public uint dwFileSize;//�ļ��Ĵ�С
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;//9000�豸֧��,1��ʾ���ļ��Ѿ�������,0��ʾ�������ļ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //¼���ļ�����(������)
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_CARD
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//�ļ���
            public NET_DVR_TIME struStartTime;//�ļ��Ŀ�ʼʱ��
            public NET_DVR_TIME struStopTime;//�ļ��Ľ���ʱ��
            public uint dwFileSize;//�ļ��Ĵ�С
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
        }

        //¼���ļ����������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND
        {
            public int lChannel;//ͨ����
            public uint dwFileType;//¼���ļ�����0xff��ȫ����0����ʱ¼��,1-�ƶ���� ��2������������
            //3-����|�ƶ���� 4-����&�ƶ���� 5-����� 6-�ֶ�¼��
            public uint dwIsLocked;//�Ƿ����� 0-�����ļ�,1-�����ļ�, 0xff��ʾ�����ļ�
            public uint dwUseCardNo;//�Ƿ�ʹ�ÿ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;//����
            public NET_DVR_TIME struStartTime;//��ʼʱ��
            public NET_DVR_TIME struStopTime;//����ʱ��
        }

        //��̨����ѡ��Ŵ���С(HIK ����ר��)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_POINT_FRAME
        {
            public int xTop;//������ʼ���x����
            public int yTop;//����������y����
            public int xBottom;//����������x����
            public int yBottom;//����������y����
            public int bCounter;//����
        }

        //�����Խ�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_COMPRESSION_AUDIO
        {
            public byte byAudioEncType;//��Ƶ�������� 0-G722; 1-G711
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byres;//���ﱣ����Ƶ��ѹ������ 
        }




        ////////////////////////////////////////////////////////////////////////////////////////
        ///ץ�Ļ�
        ///
        public const int MAX_OVERLAP_ITEM_NUM = 50;       //����ַ���������
        public const int NET_ITS_GET_OVERLAP_CFG = 5072;//��ȡ�ַ����Ӳ������ã������ITS�նˣ�
        public const int NET_ITS_SET_OVERLAP_CFG = 5073;//�����ַ����Ӳ������ã������ITS�նˣ�

        //����ʶ�����ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_INFO
        {
            public byte byPlateType; //��������
            public byte byColor; //������ɫ
            public byte byBright; //��������
            public byte byLicenseLen;    //�����ַ�����
            public byte byEntireBelieve;//�������Ƶ����Ŷȣ�0-100
            public byte byRegion;                       // ��������ֵ 0-������1-ŷ��(EU)��2-��������(ER)��3-ŷ��&����˹(EU&CIS) ,4-�ж�(ME),0xff-����
            public byte byCountry;                      // ��������ֵ������ö��COUNTRY_INDEX����֧��"COUNTRY_ALL = 0xff, //ALL  ȫ��"��
            public byte byArea;                         //����ʡ�ݣ����������ڲ�����ö�٣����������� EMI_AREA
            public byte byPlateSize;                    //���Ƴߴ磬0~δ֪��1~long, 2~short(�ж�����ʹ��)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;                       //����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CATEGORY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPlateCategory;//���Ƹ�����Ϣ, ���ж������г��ƺ����Աߵ�С����Ϣ��(Ŀǰֻ���ж�����֧��)
            public uint dwXmlLen;                        //XML������Ϣ����
            public IntPtr pXmlBuf;                      // XML������Ϣָ��,��������Ϊ COMM_ITS_PLATE_RESULʱ��Ч����XML��Ӧ��EventNotificationAlert XML Block
            public NET_VCA_RECT struPlateRect;    //����λ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sLicense;    //���ƺ��� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBelieve; //����ʶ���ַ������Ŷȣ����⵽����"��A12345", ���Ŷ�Ϊ,20,30,40,50,60,70�����ʾ"��"����ȷ�Ŀ�����ֻ��%��"A"�ֵ���ȷ�Ŀ�������%
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
            public byte byVehicleLogoRecog; //�ο�ö������ VLR_VEHICLE_CLASS
            public byte byVehicleSubLogoRecog; //����Ʒ��������ʶ�𣻲ο�VSB_VOLKSWAGEN_CLASS��������ö�١�
            public byte byVehicleModel; //������Ʒ����0-δ֪���ο�"������Ʒ�����.xlsx"
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byCustomInfo;  //�Զ�����Ϣ
            public uint wVehicleLogoRecog;  //������Ʒ�ƣ��ο�"������Ʒ��.xlsx" (���ֶμ���byVehicleLogoRecog);
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

        //����ץ�Ľ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TARGET_INFO
        {
            public uint dwID;//Ŀ��ID ,��Ա�ܶȹ��߱���ʱΪ0
            public NET_VCA_RECT struRect; //Ŀ��߽�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;//ǰ���豸��ַ��
            public ushort wPort;//ǰ���豸�˿ںţ� 
            public byte byChannel;//ǰ���豸ͨ����
            public byte byIvmsChannel;// Ivms ͨ�� 
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_FEATURE
        {
            public byte byAgeGroup;           //�����,�μ� HUMAN_AGE_GROUP_ENUM
            public byte bySex;                //�Ա�, 1 �C �� , 2 �C Ů
            public byte byEyeGlass;           //�Ƿ���۾� 1 �C ����, 2 �C ��
            public byte byAge;           //���� 0-��ʾ��δ֪�����㷨��֧�֣�,0xff-�㷨֧�֣�����û��ʶ�����
            public byte byAgeDeviation;           //�������ֵ
            public byte byRes0;           //�Ƿ���۾� 1 �C ����, 2 �C ��
            public byte byMask;           //�Ƿ������ 0-��ʾ��δ֪�����㷨��֧�֣�,1 �C ����, 2 �C ��, 0xff-�㷨֧�֣�����û��ʶ�����
            public byte bySmile;           //�Ƿ�΢Ц 0-��ʾ��δ֪�����㷨��֧�֣�,1 �C ��΢Ц, 2 �C ΢Ц, 0xff-�㷨֧�֣�����û��ʶ�����
            public byte byFaceExpression;           // ����,�μ�FACE_EXPRESSION_GROUP_ENUM
            public byte byRes1;
            public byte byRes2;           //�Ƿ���۾� 1 �C ����, 2 �C ��
            public byte byHat;           //ñ��, 0-��֧��,1-����ñ��,2-��ñ��, 3-ͷ�� 0xff-unknow��ʾδ֪,�㷨֧��δ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           //����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_ADDINFO
        {
            NET_VCA_RECT struFacePicRect;//�������ο�,������Ϊ����Сͼ(ͷ����)������������
            public int iSwingAngle;//��ת��, -90~90��
            public int iTiltAngle;//������, -90~90��
            public uint dwPupilDistance;//ͫ��,��ΧΪ����СֵΪ10����,���ֵΪ��ǰ�ֱ��ʿ���/1.6
            public byte byBlockingState;//Ŀ���ڵ�״̬�� 0-��ʾ��δ֪�����㷨��֧�֣�,1~���ڵ�,2~˲ʱ����ڵ���3~��������ڵ���4~�����ڵ�
            public byte byFaceSnapThermometryEnabled;//����ץ�Ĳ���ʹ�� 1-���� 0-�ر�
            public byte byIsAbnomalTemperature;//����ץ�Ĳ����Ƿ��¶��쳣 1-�� 0-��
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)
            NET_DVR_TIME_EX struEnterTime;   // ���ץ���½���ʱ��
            NET_DVR_TIME_EX struExitTime;    // ���ץ�����뿪ʱ��
            public float fFaceTemperature; // �����¶ȣ� - 20.0��~150.0�棬��ȷ��С�����1λ��
            public float fAlarmTemperature;// ���±�������ֵ����ȷ��С�����1λ��
            public uint dwThermalPicLen;//�ȳ���ͼƬ����
            public IntPtr pThermalPicBuff;// �ȳ���ͼƬָ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 65, ArraySubType = UnmanagedType.I1)]
            public byte[] szCustomChanID;// �Զ����ص�ͨ����  string  max.len = 64
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 399, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;// �����ֽ�
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_RESULT
        {
            public uint dwSize;             // �ṹ��С
            public uint dwRelativeTime;     // ���ʱ��
            public uint dwAbsTime;            // ����ʱ��
            public uint dwFacePicID;       //����ͼID
            public uint dwFaceScore;        //��������,0-100
            public NET_VCA_TARGET_INFO struTargetInfo;    //����Ŀ����Ϣ
            public NET_VCA_RECT struRect;      //������ͼ����
            public NET_VCA_DEV_INFO struDevInfo;        //ǰ���豸��Ϣ
            public uint dwFacePicLen;        //������ͼ�ĳ��ȣ�Ϊ0��ʾû��ͼƬ������0��ʾ��ͼƬ
            public uint dwBackgroundPicLen; //����ͼ�ĳ��ȣ�Ϊ0��ʾû��ͼƬ������0��ʾ��ͼƬ
            public byte bySmart;             //0- IDS�豸����0,1-Smart�豸����
            public byte byAlarmEndMark;//�����������0-������1-������ǣ����ֶν������ID�ֶ�ʹ�ã���ʾ��ID��Ӧ���±�����������Ҫ�ṩ��NVRʹ�ã������жϱ�����������ȡʶ��ͼƬ�����У���������ߵ�ͼƬ��
            public byte byRepeatTimes;   //�ظ�����������0-������
            public byte byUploadEventDataType;//����ͼƬ���ݳ�����ʽ��0-���������ݣ�1-URL
            public NET_VCA_HUMAN_FEATURE struFeature; //��������
            public float fStayDuration;     //ͣ��������ʱ��(��λ:��)
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sStorageIP;        //�洢����IP��ַ
            public ushort wStoragePort;            //�洢����˿ں�
            public ushort wDevInfoIvmsChannelEx;            //��NET_VCA_DEV_INFO���byIvmsChannel������ͬ���ܱ�ʾ�����ֵ���Ͽͻ�����byIvmsChannel�ܼ������ݣ��������255���¿ͻ��˰汾��ʹ��wDevInfoIvmsChannelEx��
            /*������ͼͼƬ���������ȼ���0-�͵�����,1-�е�����,2-�ߵ�����,
            ��������ץ���������ֻ�����ԭ�е�������������
            ԭ�е�����������������ץ����ʵ�֣�Ŀ����ȷ��һ������ץ���л�ȡ��õ�ͼ��Ч������һ�����ͼ�����������֣���������ץ���������ֻ���������Ѿ�ץ����ɵ�����ͼƬ��ʹ��ͼ���㷨��ͼ�������������ۣ�����������Ҫ����Ϣ���з���������ͳһ�ı�׼������ͼƬЧ��������������*/
            public byte byFacePicQuality;
            public byte byUIDLen;        //�ϴ������ı�ʶ����
            public byte byLivenessDetectionStatus;   // ������״̬��0-������1-δ֪(���ʧ��)��2-������������3-����������4-δ����������
            public byte byAddInfo;      //������Ϣ��ʶλ�����Ƿ���NET_VCA_FACESNAP_ADDINFO�ṹ�壩,0-�޸�����Ϣ, 1-�и�����Ϣ
            public IntPtr pUIDBuffer;   //��ʶָ��
            public IntPtr pAddInfoBuffer;   //������Ϣָ��,ָ��NET_VCA_FACESNAP_ADDINFO�ṹ��
            public byte byTimeDiffFlag;   //ʱ���ֶ��Ƿ���Ч  0-ʱ����Ч�� 1-ʱ����Ч
            public byte cTimeDifferenceH;   //��UTC��ʱ�Сʱ����-12 ... +14�� +��ʾ����,��byTimeDiffFlagΪ1ʱ��Ч
            public byte cTimeDifferenceM;   //��UTC��ʱ����ӣ���-30, 30, 45�� +��ʾ������byTimeDiffFlagΪ1ʱ��Ч
            public byte byBrokenNetHttp;   //����������־λ��0-�����ش����ݣ�1-�ش�����
            public IntPtr pBuffer1;          //������ͼ��ͼƬ����
            public IntPtr pBuffer2;          //����ͼ��ͼƬ����
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
            public uint dwDataLen;              //ý�����ݳ���
            public byte byType;                           // 0:����ͼ;1:����ͼ;2:�ϳ�ͼ;3:����
            public byte byDataType;                       // 0-����ֱ���ϴ�; 1-�ƴ洢������URL(3.7Ver)ԭ�ȵ�ͼƬ���ݱ��URL���ݣ�ͼƬ���ȱ��URL����
            public byte byCloseUpType;                    //��дͼ���ͣ�0-����,1-�ǻ�����,2-����
            public byte byPicRecogMode;                   //ͼƬ����ʶ��0-������ʶ��1-����ʶ��(β��ʶ��)
            public uint dwRedLightTime;                   //�����ĺ��ʱ��  ��s��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;                 //����ʱ���,yyyymmddhhmmssxxx,e.g.20090810235959999  �����λΪ������
            public NET_VCA_RECT struPlateRect;         //����λ��
            public NET_VCA_RECT struPlateRecgRect;   //��ʶ��������
            public IntPtr pBuffer;     //����ָ��
            public uint dwUTCTime;//UTCʱ�䶨��
            public byte byCompatibleAblity; //���������ֶ� 0��ʾ��Ч��1��ʾ��Ч; bit0-��ʾdwUTCTime�ֶ���Ч
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;              //����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PLATE_RESULT
        {
            public uint dwSize;
            public uint dwMatchNo;
            public byte byGroupNum;
            public byte byPicNo;
            public byte bySecondCam;    //�Ƿ�ڶ����ץ�ģ���Զ����ץ�ĵ�Զ���������ǰ��ץ�ĵĺ������������Ŀ�л��õ���
            public byte byFeaturePicNo; //����Ƶ羯��ȡ�ڼ���ͼ��Ϊ��дͼ,0xff-��ʾ��ȡ
            public byte byDriveChan;                //����������
            public byte byVehicleType;     //0- δ֪��1-�ͳ���2-������3-�γ���4-�������5-С����
            public byte byDetSceneID;//��ⳡ����[1,4], IPCĬ����0
            public byte byVehicleAttribute;//�������ԣ���λ��ʾ��0- �޸�������(��ͨ��)��bit1- �Ʊ공(�������ı�־)��bit2- Σ��Ʒ������ֵ��0- ��1- ��                      //����
            public ushort wIllegalType;       //Υ�����Ͳ��ù��궨��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byIllegalSubType;   //Υ��������
            public byte byPostPicNo;    //Υ��ʱȡ�ڼ���ͼƬ��Ϊ����ͼ,0xff-��ʾ��ȡ
            public byte byChanIndex;                //ͨ���ţ�������
            public ushort wSpeedLimit;            //�������ޣ�����ʱ��Ч��
            public byte byChanIndexEx;      //byChanIndexEx*256+byChanIndex��ʾ��ʵͨ���š�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_PLATE_INFO struPlateInfo;       //������Ϣ�ṹ
            public NET_DVR_VEHICLE_INFO struVehicleInfo;        //������Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSiteID;          //������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byDeviceID;                                   //�豸���
            public byte byDir;                //��ⷽ��1-���У�2-���У�3-˫��4-�ɶ�������5-������,6-�����򶫣�7-�ɱ����ϣ�8-����
            public byte byDetectType;    //��ⷽʽ,1-�ظд�����2-��Ƶ������3-��֡ʶ��4-�״ﴥ��
            public byte byRelaLaneDirectionType;
            public byte byCarDirectionType; //����������ʻ�ķ���0��ʾ�������£�1��ʾ�������ϣ�����ʵ�ʳ�������ʻ�����������֣�
            //��wIllegalType����Ϊ��ʱ��ʹ�øò�������wIllegalType����Ϊ��ֵʱ����wIllegalType����Ϊ׼���ò�����Ч��
            public uint dwCustomIllegalType; //Υ�����Ͷ���(�û��Զ���)
            /*Ϊ0~���ָ�ʽʱ��Ϊ�ϵ�Υ�����ͣ�wIllegalType��dwCustomIllegalType������Ч����ֵ����Υ�����롣
              Ϊ1~�ַ���ʽʱ��pIllegalInfoBuf������Ч���ϵ�Υ�����ͣ�wIllegalType��dwCustomIllegalType������Ȼ��ֵ����Υ������*/
            public IntPtr pIllegalInfoBuf;    //Υ�������ַ���Ϣ�ṹ��ָ�룻ָ��NET_ITS_ILLEGAL_INFO 
            public byte byIllegalFromatType; //Υ����Ϣ��ʽ���ͣ� 0~���ָ�ʽ�� 1~�ַ���ʽ
            public byte byPendant;// 0-��ʾδ֪,1-�����������2-������������
            public byte byDataAnalysis;            //0-����δ����, 1-�����ѷ���
            public byte byYellowLabelCar;        //0-��ʾδ֪, 1-�ǻƱ공,2-�Ʊ공
            public byte byDangerousVehicles;    //0-��ʾδ֪, 1-��Σ��Ʒ��,2-Σ��Ʒ��
            //�����ֶΰ���Pilot�ַ���Ϊ����ʻ����Copilot�ַ���Ϊ����ʻ
            public byte byPilotSafebelt;//0-��ʾδ֪,1-ϵ��ȫ��,2-��ϵ��ȫ��
            public byte byCopilotSafebelt;//0-��ʾδ֪,1-ϵ��ȫ��,2-��ϵ��ȫ��
            public byte byPilotSunVisor;//0-��ʾδ֪,1-����������,2-��������
            public byte byCopilotSunVisor;//0-��ʾδ֪, 1-����������,2-��������
            public byte byPilotCall;// 0-��ʾδ֪, 1-����绰,2-��绰
            //0-��բ��1-δ��բ ()
            public byte byBarrierGateCtrlType;
            public byte byAlarmDataType;//0-ʵʱ���ݣ�1-��ʷ����
            public NET_DVR_TIME_V30 struSnapFirstPicTime;//�˵�ʱ��(ms)��ץ�ĵ�һ��ͼƬ��ʱ�䣩
            public uint dwIllegalTime;//Υ������ʱ�䣨ms�� = ץ�����һ��ͼƬ��ʱ�� - ץ�ĵ�һ��ͼƬ��ʱ��
            public uint dwPicNum;            //ͼƬ��������picGroupNum��ͬ������������Ϣ������ͼƬ������ͼƬ��Ϣ��struVehicleInfoEx����   
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_PICTURE_INFO[] struPicInfo;                //ͼƬ��Ϣ,���Żص������6��ͼ�����������            
        }

        //�ַ������������������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAPCFG_COND
        {
            public uint dwSize;
            public uint dwChannel;//ͨ���� 
            public uint dwConfigMode;//����ģʽ��0- �նˣ�1- ǰ��(ֱ��ǰ�˻��ն˽�ǰ��)
            public byte byPicModeType;//0-��ʾСͼ(����ͼ)��1-��ʾ��ͼ(�ϳ�ͼ)
            /*
            0��ʾ���� ץ��MPRģʽ����֡����ץ�� IPCʹ�ã�
            1 ��ʾ���� ץ�� HVT ģʽ���쿨IPCʹ�ã�
*/
            public byte byRelateType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        public enum ITS_OVERLAP_ITEM_TYPE
        {
            OVERLAP_ITEM_NULL = 0,          //0-δ֪
            OVERLAP_ITEM_SITE,                //1-�ص�
            OVERLAP_ITEM_ROADNUM,             //2-·�ڱ��
            OVERLAP_ITEM_INSTRUMENTNUM,       //3-�豸���
            OVERLAP_ITEM_DIRECTION,           //4-������
            OVERLAP_ITEM_DIRECTIONDESC,       //5-����
            OVERLAP_ITEM_LANENUM,             //6-������
            OVERLAP_ITEM_LANEDES,             //7-����
            OVERLAP_ITEM_CAPTIME,             //8-ץ��ʱ��(��������)
            OVERLAP_ITEM_CAPTIME_MILLSECOND,  //9-ץ��ʱ��(������)
            OVERLAP_ITEM_PLATENUM,            //10-���ƺ�
            OVERLAP_ITEM_CARCOLOR,            //11-������ɫ
            OVERLAP_ITEM_CARTYPE,             //12-��������
            OVERLAP_ITEM_CARBRAND,            //13-����Ʒ��
            OVERLAP_ITEM_CARSPEED,            //14-�����ٶ�
            OVERLAP_ITEM_SPEEDLIMIT,          //15-���ٱ�־
            OVERLAP_ITEM_CARLENGTH,           //16-��������1~99m
            OVERLAP_ITEM_ILLEGALNUM,          //17-Υ������(Υ���������Ӧ��û�õģ�Ӧ��ֱ�ӵ���Υ����Ϣ���������������١����١����С�����ơ�ռ����ѹ���ߵ�)
            OVERLAP_ITEM_MONITOR_INFO,      //18-������Ϣ
            OVERLAP_ITEM_ILLEGALDES,          //19-Υ����Ϊ
            OVERLAP_ITEM_OVERSPEED_PERCENT,    //20-���ٱ�
            OVERLAP_ITEM_RED_STARTTIME,           //21-��ƿ�ʼʱ��
            OVERLAP_ITEM_RED_STOPTIME,        //22-��ƽ���ʱ��
            OVERLAP_ITEM_RED_DURATION,        //23-�������ʱ��
            OVERLAP_ITEM_SECUNITY_CODE,        //24-��α��
            OVERLAP_ITEM_CAP_CODE,        //25-ץ�ı��
            OVERLAP_ITEM_SEATBELT,      //26-��ȫ��  
            OVERLAP_ITEM_MONITOR_ID,    //27-������
            OVERLAP_ITEM_SUN_VISOR,     //28-������ 
            OVERLAP_ITEM_LANE_DIRECTION,  //29-������ʻ���� 
            OVERLAP_ITEM_LICENSE_PLATE_COLOR,  // 30-������ɫ
            OVERLAP_ITEM_SCENE_NUMBER,  //31-�������
            OVERLAP_ITEM_SCENE_NAME,   //32-��������
            OVERLAP_ITEM_YELLOW_SIGN_CAR,  //33-�Ʊ공
            OVERLAP_ITEM_DANGEROUS_CAR,    //34-Σ��Ʒ��
            OVERLAP_ITEM_CAR_SUBBRAND,  //35-������Ʒ��
            OVERLAP_ITEM_CAR_DIRECTION,  //36-��������
            OVERLAP_ITEM_PENDANT,  //37-����������
            OVERLAP_ITEM_CALL,  //38-��绰
            OVERLAP_ITEM_CAR_VALIDITY  //39-���Ŷ�
        }


        //�����ַ�������Ϣ�ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_SINGLE_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public byte byItemType;//����
            public byte byChangeLineNum;//�������Ļ�������ȡֵ��Χ��[0,10]��Ĭ�ϣ�0 
            public byte bySpaceNum;//�������Ŀո�����ȡֵ��Χ��[0-255]��Ĭ�ϣ�0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        //�ַ����������ýṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_OVERLAP_ITEM_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_OVERLAP_SINGLE_ITEM_PARAM[] struSingleItem;//�ַ���������Ϣ
            public uint dwLinePercent;
            public uint dwItemsStlye;
            public ushort wStartPosTop;
            public ushort wStartPosLeft;
            public ushort wCharStyle;
            public ushort wCharSize;
            public ushort wCharInterval;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public uint dwForeClorRGB;//ǰ��ɫ��RGBֵ��bit0~bit7: B��bit8~bit15: G��bit16~bit23: R��Ĭ�ϣ�x00FFFFFF-��
            public uint dwBackClorRGB;//����ɫ��RGBֵ��ֻ��ͼƬ�������Ч��bit0~bit7: B��bit8~bit15: G��bit16~bit23: R��Ĭ�ϣ�x00000000-�� 
            public byte byColorAdapt;//��ɫ�Ƿ�����Ӧ��0-��1-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        //�ַ�����������Ϣ�ṹ��
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
            public byte[] byRes1;//���ﱣ����Ƶ��ѹ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite1;//
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite2;//���ﱣ����Ƶ��ѹ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//���ﱣ����Ƶ��ѹ������ 
        }

        //�ַ������������������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_CFG
        {
            public uint dwSize;
            public byte byEnable;//�Ƿ����ã�0- �����ã�1- ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//���ﱣ����Ƶ��ѹ������
            public NET_ITS_OVERLAP_ITEM_PARAM struOverLapItem;//�ַ�������
            public NET_ITS_OVERLAP_INFO_PARAM struOverLapInfo;//�ַ���������Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//���ﱣ����Ƶ��ѹ������ 
        }

        //�������������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM
        {
            public uint dwSize;
            public byte byLevel;//�������ȼ���0- һ�ȼ����ߣ���1- ���ȼ����У���2- ���ȼ����ͣ�������
            public byte byAlarmInfoType;//�ϴ�������Ϣ���ͣ����ܽ�ͨ�����֧�֣���0- �ϱ�����Ϣ��NET_DVR_PLATE_RESULT����1- �±�����Ϣ(NET_ITS_PLATE_RESULT) 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//���ﱣ����Ƶ��ѹ������ 
            public byte byFaceAlarmDetection;//1-��ʾ������ⱨ����չ(INTER_FACE_DETECTION),0-��ʾԭ��֧�ֽṹ(INTER_FACESNAP_RESULT)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//���ﱣ����Ƶ��ѹ������ 
        }

        // ������������V50�ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM_V50
        {
            public uint dwSize;
            public byte byLevel; //�������ȼ���0-һ�ȼ����ߣ���1-���ȼ����У���2-���ȼ����ͣ�
            public byte byAlarmInfoType; //�ϴ�������Ϣ���ͣ�ץ�Ļ�֧�֣���0-�ϱ�����Ϣ��NET_DVR_PLATE_RESULT����1-�±�����Ϣ(NET_ITS_PLATE_RESULT)2012-9-28
            public byte byRetAlarmTypeV40; //0--����NET_DVR_ALARMINFO_V30��NET_DVR_ALARMINFO, 1--�豸֧��NET_DVR_ALARMINFO_V40�򷵻�NET_DVR_ALARMINFO_V40����֧���򷵻�NET_DVR_ALARMINFO_V30��NET_DVR_ALARMINFO
            public byte byRetDevInfoVersion; //CVR�ϴ�������Ϣ�ص��ṹ��汾�� 0-COMM_ALARM_DEVICE�� 1-COMM_ALARM_DEVICE_V40
            public byte byRetVQDAlarmType; //VQD�����ϴ����ͣ�0-�ϴ�������NET_DVR_VQD_DIAGNOSE_INFO��1-�ϴ�����NET_DVR_VQD_ALARM
            //1-��ʾ������ⱨ����չ(INTER_FACE_DETECTION),0-��ʾԭ��֧�ֽṹ(INTER_FACESNAP_RESULT)
            public byte byFaceAlarmDetection;
            //Bit0- ��ʾ���������Ƿ��ϴ�ͼƬ: 0-�ϴ���1-���ϴ�
            //Bit1- ��ʾ���������ϴ�ȷ�ϻ��ƣ�0-��������1-����
            //Bit6- ��ʾ�״��ⱨ��(eventType:radarDetection)�Ƿ���ʵʱ�ϴ���0-��������1-����������web���ʵʱ��ʾ�״�Ŀ��켣��
            public byte bySupport;
            //������������ 
            //bit0-���Ƽ�⣨IPC�� ��0-��������1-������
            //bit1-����ͳ�ƣ�IPC��  ��0-��������1-������
            //bit2-�ȶ�ͼͳ�ƣ�IPC�� ��0-��������1-������
            //bit3-����ץ�ģ�IPC�� ��0-��������1-������
            //bit4-�����Աȣ�IPC�� ��0-��������1-������
            //bit5-JSON����͸����IPC�� ��0-��������1-������
            //bit6-�ȶ�ͼ����Աͣ��ʱ��ͳ�������ϴ��¼���0-��������1-������
            //bit7-�ȶ�ͼ������ͳ�������ϴ��¼���ȷ�ϻ��ƣ�0-��������1-������
            public byte byBrokenNetHttp;
            public ushort wTaskNo;    //�������� �� (�ϴ�����NET_DVR_VEHICLE_RECOG_RESULT�е��ֶ�dwTaskNo��Ӧ ͬʱ �·�����ṹ NET_DVR_VEHICLE_RECOG_COND�е��ֶ�dwTaskNo��Ӧ)
            public byte byDeployType;    //�������ͣ�0-�ͻ��˲�����1-ʵʱ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byAlarmTypeURL;//bit0-��ʾ����ץ�ı����ϴ���INTER_FACESNAP_RESULT����0-��ʾ�����ƴ��䣬1-��ʾURL���䣨�豸֧�ֵ�����£��豸֧���������ݾ��屨���������ж�,ͬʱ�豸��Ҫ֧��URL����ط��񣬵�ǰ�ǡ��ƴ洢����
            //bit1-��ʾEVENT_JSON��ͼƬ���ݳ������ͣ�0-��ʾ�����ƴ��䣬1-��ʾURL���䣨�豸֧�ֵ�����£��豸֧���������ݾ��屨���������жϣ�
            //bit2 - �����ȶ�(��������ΪCOMM_SNAP_MATCH_ALARM)��ͼƬ�����ϴ����ͣ�0 - �����ƴ��䣬1 - URL����
            //bit3 - ��Ϊ����(��������ΪCOMM_ALARM_RULE)��ͼƬ�����ϴ����ͣ�0 - �����ƴ��䣬1 - URL���䣬���ֶ��豸�Ƿ�֧�֣���Ӧ��Ӳ����������<isSupportBehaviorUploadByCloudStorageURL>�ڵ��Ƿ񷵻���Ϊtrue
            public byte byCustomCtrl;//Bit0- ��ʾ֧�ָ���ʻ������ͼ�ϴ�: 0-���ϴ�,1-�ϴ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
        }

        //��բ���Ʋ���
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

        public const int NET_DVR_GET_ENTRANCE_PARAMCFG = 3126; //��ȡ����ڿ��Ʋ���
        public const int NET_DVR_SET_ENTRANCE_PARAMCFG = 3127; //���ó���ڿ��Ʋ���

        //����ڿ�������
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_COND
        {
            public byte byLaneNo;//�����ţ�0- ��ʾ��Чֵ(�豸��Ҫ����Чֵ�ж�)��1- ����1
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�̵�����������
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RELAY_PARAM
        {
            public byte byAccessDevInfo;//0-�������豸��1-����բ��2-�ص�բ��3-ͣ��բ��4-�����źš�5-������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //������Ϣ�ܿز���
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_VEHICLE_CONTROL
        {
            public byte byGateOperateType;//�������ͣ�0- �޲�����1- ����բ
            public byte byRes1;
            public ushort wAlarmOperateType; //�����������ͣ�0- �޲�����bit0- �̵������������bit1- �����ϴ�������bit3- �澯�����ϴ���ֵ��0-��ʾ�أ�1-��ʾ�����ɸ�ѡ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //����ڿ��Ʋ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ENTRANCE_CFG
        {
            public uint dwSize;
            public byte byEnable;
            public byte byBarrierGateCtrlMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public uint dwRelateTriggerMode;
            public uint dwMatchContent;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RELAY_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RELAY_PARAM[] struRelayRelateInfo;//�̵�������������Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IOIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byGateSingleIO;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLE_TYPE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VEHICLE_CONTROL[] struVehicleCtrl;//������Ϣ�ܿ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//����
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MANUALSNAP
        {
            public byte byOSDEnable;
            public byte byLaneNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }




        /********************************�ӿڲ����ṹ(end)*********************************/


        /********************************SDK�ӿں�������*********************************/

        /*********************************************************
        Function:    NET_DVR_Init
        Desc:  ��ʼ��SDK����������SDK������ǰ�ᡣ
        Input:    
        Output:    
        Return:    TRUE��ʾ�ɹ���FALSE��ʾʧ�ܡ�
        **********************************************************/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();

        /*********************************************************
        Function:    NET_DVR_Cleanup
        Desc:  �ͷ�SDK��Դ���ڽ���֮ǰ������
        Input:    
        Output:    
        Return:    TRUE��ʾ�ɹ���FALSE��ʾʧ��
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);

        /*********************************************************
        Function:    EXCEPYIONCALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void EXCEPYIONCALLBACK(uint dwType, int lUserID, int lHandle, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetExceptionCallBack_V30(uint nMessage, IntPtr hWnd, EXCEPYIONCALLBACK fExceptionCallBack, IntPtr pUser);


        /*********************************************************
        Function:    MESSCALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACK(int lCommand, string sDVRIP, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack(MESSCALLBACK fMessCallBack);

        /*********************************************************
        Function:    MESSCALLBACKEX
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACKEX(int iCommand, int iUserID, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_EX(MESSCALLBACKEX fMessCallBack_EX);

        /*********************************************************
        Function:    MESSCALLBACKNEW
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSCALLBACKNEW(int lCommand, string sDVRIP, string pBuf, uint dwBufLen, ushort dwLinkDVRPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(MESSCALLBACKNEW fMessCallBack_NEW);

        /*********************************************************
        Function:    MESSAGECALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate int MESSAGECALLBACK(int lCommand, System.IntPtr sDVRIP, System.IntPtr pBuf, uint dwBufLen, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack(MESSAGECALLBACK fMessageCallBack, uint dwUser);


        /*********************************************************
        Function:    MSGCallBack
        Desc:  (�ص�����)
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

        //Ԥ����ؽӿ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_RealPlay(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_SDK_RealPlay(int iUserLogID, ref NET_SDK_CLIENTINFO lpDVRClientInfo);
        /*********************************************************
        Function:    REALDATACALLBACK
        Desc:  Ԥ���ص�
        Input:    lRealHandle ��ǰ��Ԥ����� 
                dwDataType ��������
                pBuffer ������ݵĻ�����ָ�� 
                dwBufSize ��������С 
                pUser �û����� 
        Output:    
        Return:    void
        **********************************************************/
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]

        /*********************************************************
        Function:    NET_DVR_RealPlay_V30
        Desc:  ʵʱԤ����
        Input:    lUserID [in] NET_DVR_Login()��NET_DVR_Login_V30()�ķ���ֵ 
                lpClientInfo [in] Ԥ������ 
                cbRealDataCallBack [in] �������ݻص����� 
                pUser [in] �û����� 
                bBlocked [in] �������������Ƿ�������0����1���� 
        Output:    
        Return:    1��ʾʧ�ܣ�����ֵ��ΪNET_DVR_StopRealPlay�Ⱥ����ľ������
        **********************************************************/
        public static extern int NET_DVR_RealPlay_V30(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser, UInt32 bBlocked);

        /*********************************************************
        Function:    NET_DVR_RealPlay_V40
        Desc:  ʵʱԤ����
        Input:    lUserID [in] NET_DVR_Login()��NET_DVR_Login_V30()�ķ���ֵ 
                lpClientInfo [in] Ԥ������ 
                cbRealDataCallBack [in] �������ݻص����� 
                pUser [in] �û����� 
                bBlocked [in] �������������Ƿ�������0����1���� 
        Output:    
        Return:    1��ʾʧ�ܣ�����ֵ��ΪNET_DVR_StopRealPlay�Ⱥ����ľ������
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_V40(int iUserID, ref NET_DVR_PREVIEWINFO lpPreviewInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);

        /*********************************************************
        Function:    NET_DVR_StopRealPlay
        Desc:  ֹͣԤ����
        Input:    lRealHandle [in] Ԥ�������NET_DVR_RealPlay����NET_DVR_RealPlay_V30�ķ���ֵ 
        Output:    
        Return:    
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int iRealHandle);

        /*********************************************************
        Function:    DRAWFUN
        Desc:  (�ص�����)
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
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void SETREALDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(int lRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint dwUser);

        /*********************************************************
        Function:    STDDATACALLBACK
        Desc:  (�ص�����)
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

        //��̬����I֡
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(Int32 lUserID, Int32 lChannel);//������

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrameSub(Int32 lUserID, Int32 lChannel);//������

        //��̨������ؽӿ�
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

        //�ļ�������ط�
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

        //2007-04-16���Ӳ�ѯ��������ŵ��ļ�����
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
        Desc:  (�ص�����)
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

        //����
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



        //Զ�̸�ʽ��Ӳ��
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, ref int pCurrentFormatDisk, ref int pCurrentDiskPos, ref int pFormatStatic);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);

        //����
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

        //�����Խ�
        /*********************************************************
        Function:    VOICEDATACALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void VOICEDATACALLBACK(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        /*********************************************************
        Function:    VOICEDATACALLBACKV30
        Desc:  (�ص�����)
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

        //����ת��
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR_V30(int lUserID, uint dwVoiceChan, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, string pSendBuf, uint dwBufSize);

        //�����㲥
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();

        /*********************************************************
        Function:    VOICEAUDIOSTART
        Desc:  (�ص�����)
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


        //͸��ͨ������
        /*********************************************************
        Function:    SERIALDATACALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void SERIALDATACALLBACK(int lSerialHandle, string pRecvDataBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStart(int lUserID, int lSerialPort, SERIALDATACALLBACK fSerialDataCallBack, uint dwUser);

        //485��Ϊ͸��ͨ��ʱ����Ҫָ��ͨ���ţ���Ϊ��ͬͨ����485�����ÿ��Բ�ͬ(���粨����)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendToSerialPort(int lUserID, uint dwSerialPort, uint dwSerialIndex, string pSendBuf, uint dwBufSize);

        //���� nBitrate = 16000
        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_InitG722Decoder(int nBitrate);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Decoder(IntPtr pDecHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG722Frame(IntPtr pDecHandle, ref byte pInBuffer, ref byte pOutBuffer);

        //����
        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Encoder();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG722Frame(IntPtr pEncodeHandle, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Encoder(IntPtr pEncodeHandle);

        //Զ�̿��Ʊ�����ʾ
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(int lUserID, int lKeyIndex);

        //Զ�̿����豸���ֶ�¼��
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID, int lChannel, int lRecordType);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID, int lChannel);

        //���뿨
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

        //��ȡ���뿨���кŴ˽ӿ���Ч������GetBoardDetail�ӿڻ��(2005-12-08֧��)
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSerialNum_Card(int lChannelNum, ref uint pDeviceSerialNo);

        //��־
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

        //��ֹ2004��8��5��,��113���ӿ�
        //ATM DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByCard(int lUserID, int lChannel, uint dwFileType, int nFindType, ref byte sCardNumber, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);


        //2005-09-15
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sPicFileName);

        //JPEGץͼ���ڴ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sJpegPicBuffer, uint dwPicSize, ref uint lpSizeReturned);

        //2006-02-16
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetRealPlayerIndex(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPlayerIndex(int lPlayHandle);

        //2006-08-28 704-640 ��������
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG(int lUserID, uint dwScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG(int lUserID, ref uint lpOutScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        //2006-08-28 ATM���˿�����
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetATMPortCFG(int lUserID, ushort wATMPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetATMPortCFG(int lUserID, ref ushort LPOutATMPort);

        //2006-11-10 ֧���Կ��������
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

        //�����豸DS-6001D/DS-6001F
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

        //��·������
        //2007-11-30 V211֧�����½ӿ� //11
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

        //2007-12-22 ����֧�ֽӿ� //18
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

        //2009-4-13 ����
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

        public const int NET_DVR_SHOWLOGO = 1;/*��ʾLOGO*/
        public const int NET_DVR_HIDELOGO = 2;/*����LOGO*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LogoSwitch(int lUserID, uint dwDecChan, uint dwLogoSwitch);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus(int lUserID, ref tagNET_DVR__DECODER_WORK_STATUS lpDecoderCfg);

        /*��ʾͨ�������붨��*/
        //�Ϻ����� ����
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterPlayBackDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);

        public const int DISP_CMD_ENLARGE_WINDOW = 1;    /*��ʾͨ���Ŵ�ĳ������*/
        public const int DISP_CMD_RENEW_WINDOW = 2;    /*��ʾͨ�����ڻ�ԭ*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixDiaplayControl(int lUserID, uint dwDispChanNum, uint dwDispChanCmd, uint dwCmdParam);

        //end
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshPlay(int lPlayHandle);

        //�ָ�Ĭ��ֵ
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);

        //�������
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(int lUserID);

        //����
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);

        //�ر�DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);

        //�������� begin
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, System.IntPtr lpInBuffer, uint dwInBufferSize);

        //���������豸�û�����
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        //������������
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

        //��Ƶ��������
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(int lRealHandle, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        //�����ļ�
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

        //������־�ļ�д��ӿ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLogToFile(int bLogEnable, string strLogDir, bool bAutoDel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKState(ref NET_DVR_SDKSTATE pSDKState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKAbility(ref NET_DVR_SDKABL pSDKAbl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZProtocol(int lUserID, ref NET_DVR_PTZCFG pPtzcfg);

        //ǰ�������
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
        //�����豸����
        public const int DS6001_HF_B = 60;//��Ϊ������DS6001-HF/B
        public const int DS6001_HF_P = 61;//����ʶ��DS6001-HF/P
        public const int DS6002_HF_B = 62;//˫�����٣�DS6002-HF/B
        public const int DS6101_HF_B = 63;//��Ϊ������DS6101-HF/B
        public const int IDS52XX = 64;//���ܷ�����IVMS
        public const int DS9000_IVS = 65;//9000ϵ������DVR
        public const int DS8004_AHL_A = 66;//����ATM, DS8004AHL-S/A
        public const int DS6101_HF_P = 67;//����ʶ��DS6101-HF/P

        //������ȡ����
        public const int VCA_DEV_ABILITY = 256;//�豸���ܷ�����������
        public const int VCA_CHAN_ABILITY = 272;//��Ϊ��������
        public const int MATRIXDECODER_ABILITY = 512;//��·��������ʾ����������
        //��ȡ/���ô�ӿڲ�����������
        //����ʶ��NET_VCA_PLATE_CFG��
        public const int NET_DVR_SET_PLATECFG = 150;//���ó���ʶ�����
        public const int NET_DVR_GET_PLATECFG = 151;//��ȡ����ʶ�����
        //��Ϊ��Ӧ��NET_VCA_RULECFG��
        public const int NET_DVR_SET_RULECFG = 152;//������Ϊ��������
        public const int NET_DVR_GET_RULECFG = 153;//��ȡ��Ϊ��������

        //˫������궨������NET_DVR_LF_CFG��
        public const int NET_DVR_SET_LF_CFG = 160;//����˫����������ò���
        public const int NET_DVR_GET_LF_CFG = 161;//��ȡ˫����������ò���

        //���ܷ�����ȡ�����ýṹ
        public const int NET_DVR_SET_IVMS_STREAMCFG = 162;//�������ܷ�����ȡ������
        public const int NET_DVR_GET_IVMS_STREAMCFG = 163;//��ȡ���ܷ�����ȡ������

        //���ܿ��Ʋ����ṹ
        public const int NET_DVR_SET_VCA_CTRLCFG = 164;//�������ܿ��Ʋ���
        public const int NET_DVR_GET_VCA_CTRLCFG = 165;//��ȡ���ܿ��Ʋ���

        //��������NET_VCA_MASK_REGION_LIST
        public const int NET_DVR_SET_VCA_MASK_REGION = 166;//���������������
        public const int NET_DVR_GET_VCA_MASK_REGION = 167;//��ȡ�����������

        //ATM�������� NET_VCA_ENTER_REGION
        public const int NET_DVR_SET_VCA_ENTER_REGION = 168;//���ý����������
        public const int NET_DVR_GET_VCA_ENTER_REGION = 169;//��ȡ�����������

        //�궨������NET_VCA_LINE_SEGMENT_LIST
        public const int NET_DVR_SET_VCA_LINE_SEGMENT = 170;//���ñ궨��
        public const int NET_DVR_GET_VCA_LINE_SEGMENT = 171;//��ȡ�궨��

        // ivms��������NET_IVMS_MASK_REGION_LIST
        public const int NET_DVR_SET_IVMS_MASK_REGION = 172;//����IVMS�����������
        public const int NET_DVR_GET_IVMS_MASK_REGION = 173;//��ȡIVMS�����������
        // ivms����������NET_IVMS_ENTER_REGION
        public const int NET_DVR_SET_IVMS_ENTER_REGION = 174;//����IVMS�����������
        public const int NET_DVR_GET_IVMS_ENTER_REGION = 175;//��ȡIVMS�����������

        public const int NET_DVR_SET_IVMS_BEHAVIORCFG = 176;//�������ܷ�������Ϊ�������
        public const int NET_DVR_GET_IVMS_BEHAVIORCFG = 177;//��ȡ���ܷ�������Ϊ�������

        // IVMS �طż���
        public const int NET_DVR_IVMS_SET_SEARCHCFG = 178;//����IVMS�طż�������
        public const int NET_DVR_IVMS_GET_SEARCHCFG = 179;//��ȡIVMS�طż�������

        //�����ص�����
        //��ӦNET_VCA_PLATE_RESULT
        public const int COMM_ALARM_PLATE = 4353;//����ʶ�𱨾���Ϣ

        //�ṹ�����궨�� 
        public const int VCA_MAX_POLYGON_POINT_NUM = 10;//����������֧��10����Ķ����
        public const int MAX_RULE_NUM = 8;//����������
        public const int MAX_TARGET_NUM = 30;//���Ŀ�����
        public const int MAX_CALIB_PT = 6;//���궨�����
        public const int MIN_CALIB_PT = 4;//��С�궨�����
        public const int MAX_TIMESEGMENT_2 = 2;//���ʱ�����
        public const int MAX_LICENSE_LEN = 16;//���ƺ���󳤶�
        public const int MAX_PLATE_NUM = 3;//���Ƹ���
        public const int MAX_MASK_REGION_NUM = 4;//����ĸ���������
        public const int MAX_SEGMENT_NUM = 6;//������궨�����������Ŀ
        public const int MIN_SEGMENT_NUM = 3;//������궨��С��������Ŀ

        //���ܿ�����Ϣ
        public const int MAX_VCA_CHAN = 16;//�������ͨ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLINFO
        {
            public byte byVCAEnable;//�Ƿ�������
            public byte byVCAType;//�����������ͣ�VCA_CHAN_ABILITY_TYPE 
            public byte byStreamWithVCA;//�������Ƿ��������Ϣ
            public byte byMode;//ģʽ��VCA_CHAN_MODE_TYPE ,atm������ʱ����Ҫ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����������Ϊ0 
        }

        //���ܿ�����Ϣ�ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VCA_CHAN, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_CTRLINFO[] struCtrlInfo;//������Ϣ,����0��Ӧ�豸����ʼͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�����豸������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_ABILITY
        {
            public uint dwSize;//�ṹ����
            public byte byVCAChanNum;//����ͨ������
            public byte byPlateChanNum;//����ͨ������
            public byte byBBaseChanNum;//��Ϊ���������
            public byte byBAdvanceChanNum;//��Ϊ�߼������
            public byte byBFullChanNum;//��Ϊ���������
            public byte byATMChanNum;//����ATM����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //��Ϊ������������
        public enum VCA_ABILITY_TYPE
        {
            TRAVERSE_PLANE_ABILITY = 1,//��Խ������
            ENTER_AREA_ABILITY = 2,//��������
            EXIT_AREA_ABILITY = 4,//�뿪����
            INTRUSION_ABILITY = 8,//����
            LOITER_ABILITY = 16,//�ǻ�
            LEFT_TAKE_ABILITY = 32,//�������
            PARKING_ABILITY = 64,//ͣ��
            RUN_ABILITY = 128,//����
            HIGH_DENSITY_ABILITY = 256,//����Ա�ܶ�
            LF_TRACK_ABILITY = 512,//˫���������
            STICK_UP_ABILITY = 1073741824,//��ֽ��
            INSTALL_SCANNER_ABILITY = -2147483648,//��װ������
        }

        //����ͨ������
        public enum VCA_CHAN_ABILITY_TYPE
        {
            VCA_BEHAVIOR_BASE = 1,//��Ϊ����������
            VCA_BEHAVIOR_ADVANCE = 2,//��Ϊ�����߼���
            VCA_BEHAVIOR_FULL = 3,//��Ϊ����������
            VCA_PLATE = 4,//��������
            VCA_ATM = 5,//ATM����
        }

        //����ATMģʽ����(ATM��������)
        public enum VCA_CHAN_MODE_TYPE
        {
            VCA_ATM_PANEL = 0,//ATM���
            VCA_ATM_SURROUND = 1,//ATM����
        }

        //ͨ�������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CHAN_IN_PARAM
        {
            public byte byVCAType;//VCA_CHAN_ABILITY_TYPEö��ֵ
            public byte byMode;//ģʽ��VCA_CHAN_MODE_TYPE ,atm������ʱ����Ҫ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����������Ϊ0 
        }

        //��Ϊ�������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_BEHAVIOR_ABILITY
        {
            public uint dwSize;//�ṹ����
            public uint dwAbilityType;//֧�ֵ��������ͣ���λ��ʾ����VCA_ABILITY_TYPE����
            public byte byMaxRuleNum;//��������
            public byte byMaxTargetNum;//���Ŀ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����������Ϊ0
        }

        /*********************************************************
        Function:    NET_DVR_GetDeviceAbility
        Desc:  
        Input:    
        Output:    
        Return:    TRUE��ʾ�ɹ���FALSE��ʾʧ�ܡ�
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);


        //���ܹ��ýṹ
        //����ֵ��һ��,������ֵΪ��ǰ����İٷֱȴ�С, ����ΪС�������λ
        //������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POINT
        {
            public float fX;// X������, 0.001~1
            public float fY;//Y������, 0.001~1
        }

        public const int DRAG_PTZ = 51;//�϶�PTZ
        public const int NET_DVR_FISHEYE_CFG = 3244; //���۳���������

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DRAG_POS_PARAM
        {
            public uint dwChannel;  //�豸ͨ����
            public uint dwPtzChannel;  //dwChannel��ָͨ��ģʽΪFISHEYE_STREAM_MODE_FISHEYE������ģʽʱ������ֵΪ���϶���������ptzͨ����dwChannelͨ��ģʽΪ������ģʽʱ����Ϊ0����
            public tagNET_VCA_POINT struToPoint;  //�϶�����Ҫ��ת����Ŀ��㣬Ŀ���λ�������Ԥ����������Ͻ�
            public tagNET_VCA_POINT struOriPoint;  //�϶�������ʼ�㣬�˵�Ϊ�����϶�������ʼʱ�����ָ�������Ԥ���������Ͻǵ�λ�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����������Ϊ0
        }
        public enum FISHEYE_STREAM_OUTPUT_MODE
        {
            FISHEYE_STREAM_MODE_FISHEYE = 1,
            FISHEYE_STREAM_MODE_PTZ = 2,
            FISHEYE_STREAM_MODE_PANORAMA = 3
        }

        public enum CALLBACK_TYPE_DATA_ENUM
        {
            ENUM_FISHEYE_STREAM_STATUS = 1,  //�����������״̬
            ENUM_FISHEYE_PTZPOS = 2,  //ptzͨ����ǰ��������ģʽ�µ�����
            ENUM_FISHEYE_REALTIME_OUTPUT = 3   //ʵʱ���ģʽ
        }

        //��������״̬
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FISHEYE_STREAM_STATUS
        {
            public uint dwSize;
            public byte byStreamMode;    //�������ģʽ���μ�FISHEYE_STREAM_OUTPUT_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����������Ϊ0
        }

        //�����ӻص����ݽṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CALLBACK_TYPE_DATA
        {
            public uint dwChannel;      //ͨ����
            public uint dwDataType;     //�μ�CALLBACK_TYPE_DATA_ENUM
            public uint dwDataLen;      //���ݳ���
            public IntPtr pData;          //���ݣ���dwTypeDataΪ ENUM_FISHEYE_STREAM_STATUS�����ӦΪNET_DVR_FISHEYE_STREAM_STATUS
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����������Ϊ0
        }

        //�����ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RECT
        {
            public float fX;//�߽�����Ͻǵ��X������, 0.001~1
            public float fY;//�߽�����Ͻǵ��Y������, 0.001~1
            public float fWidth;//�߽��Ŀ���, 0.001~1
            public float fHeight;//�߽��ĸ߶�, 0.001~1
        }

        //��Ϊ�����¼�����
        public enum VCA_EVENT_TYPE
        {
            VCA_TRAVERSE_PLANE = 1,//��Խ������
            VCA_ENTER_AREA = 2,//Ŀ���������,֧���������
            VCA_EXIT_AREA = 4,//Ŀ���뿪����,֧���������
            VCA_INTRUSION = 8,//�ܽ�����,֧���������
            VCA_LOITER = 16,//�ǻ�,֧���������
            VCA_LEFT_TAKE = 32,//�������,֧���������
            VCA_PARKING = 64,//ͣ��,֧���������
            VCA_RUN = 128,//����,֧���������
            VCA_HIGH_DENSITY = 256,//��������Ա�ܶ�,֧���������
            VCA_STICK_UP = 1073741824,//��ֽ��,֧���������
            VCA_INSTALL_SCANNER = -2147483648,//��װ������,֧���������
        }

        //�����洩Խ��������
        public enum VCA_CROSS_DIRECTION
        {
            VCA_BOTH_DIRECTION,// ˫�� 
            VCA_LEFT_GO_RIGHT,// �������� 
            VCA_RIGHT_GO_LEFT,// �������� 
        }

        //�߽ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE
        {
            public tagNET_VCA_POINT struStart;//��� 
            public tagNET_VCA_POINT struEnd; //�յ�

            //             public void init()
            //             {
            //                 struStart = new tagNET_VCA_POINT();
            //                 struEnd = new tagNET_VCA_POINT();
            //             }
        }

        //�ýṹ�ᵼ��xaml�������������������������������������ʱ��û���ҵ�  
        //��ʱ���νṹ��
        //����ͽṹ��
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
            public tagNET_VCA_LINE struPlaneBottom;//������ױ�
            public VCA_CROSS_DIRECTION dwCrossDirection;//��Խ����: 0-˫��1-�����ң�2-���ҵ���
            public byte byRes1;//����
            public byte byPlaneHeight;//������߶�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 38, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new tagNET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }

        //����/�뿪�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_AREA
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //���ݱ����ӳ�ʱ������ʶ�����д�ͼƬ�����������IO����һ�£�1�뷢��һ����
        //���ֲ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_INTRUSION
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//�����ӳ�ʱ��: 1-120�룬����5�룬�ж�����Ч������ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�ǻ�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARAM_LOITER
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//�����ǻ������ĳ���ʱ�䣺1-120�룬����10��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //����/�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TAKE_LEFT
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//��������/��������ĳ���ʱ�䣺1-120�룬����10��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ͣ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARKING
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//����ͣ����������ʱ�䣺1-120�룬����10��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //���ܲ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RUN
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public float fRunDistance;//�˱���������, ��Χ: [0.1, 1.00]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //��Ա�ۼ�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_HIGH_DENSITY
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public float fDensity;//�ܶȱ���, ��Χ: [0.1, 1.0]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //��ֽ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_STICK_UP
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//��������ʱ�䣺10-60�룬����10��
            public byte bySensitivity;//�����Ȳ�������Χ[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SCANNER
        {
            public tagNET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//��������ʱ�䣺10-60��
            public byte bySensitivity;//�����Ȳ�������Χ[1,5]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�����¼�����
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct tagNET_VCA_EVENT_UNION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            [FieldOffsetAttribute(0)]
            public uint[] uLen;//����
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TRAVERSE_PLANE struTraversePlane;//��Խ��������� 
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_AREA struArea;//����/�뿪�������
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_INTRUSION struIntrusion;//���ֲ���
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARAM_LOITER struLoiter;//�ǻ�����
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TAKE_LEFT struTakeTeft;//����/�������
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARKING struParking;//ͣ������
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_RUN struRun;//���ܲ���
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_HIGH_DENSITY struHighDensity;//��Ա�ۼ�����  
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_STICK_UP struStickUp;//��ֽ��
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_SCANNER struScanner;//���������� 

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

        // �ߴ����������
        public enum SIZE_FILTER_MODE
        {
            IMAGE_PIX_MODE,//�������ش�С����
            REAL_WORLD_MODE,//����ʵ�ʴ�С����
        }

        //�ߴ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SIZE_FILTER
        {
            public byte byActive;//�Ƿ񼤻�ߴ������ 0-�� ��0-��
            public byte byMode;//������ģʽSIZE_FILTER_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//��������0
            public NET_VCA_RECT struMiniRect;//��СĿ���,ȫ0��ʾ������
            public NET_VCA_RECT struMaxRect;//���Ŀ���,ȫ0��ʾ������
        }

        //�������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ONE_RULE
        {
            public byte byActive;//�Ƿ񼤻����,0-��,��0-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����������Ϊ0�ֶ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//��������
            public VCA_EVENT_TYPE dwEventType;//��Ϊ�����¼�����
            public tagNET_VCA_EVENT_UNION uEventParam;//��Ϊ�����¼�����
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//�ߴ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//������ʽ 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//����������¼��ͨ��,Ϊ1��ʾ������ͨ��
        }

        //��Ϊ�������ýṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULECFG
        {
            public uint dwSize;//�ṹ����
            public byte byPicProType;//����ʱͼƬ������ʽ 0-������ ��0-�ϴ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPictureParam;//ͼƬ���ṹ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ONE_RULE[] struRule;//��������
        }

        //��Ŀ��ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TARGET_INFO
        {
            public uint dwID;//Ŀ��ID ,��Ա�ܶȹ��߱���ʱΪ0
            public NET_VCA_RECT struRect; //Ŀ��߽�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        //�򻯵Ĺ�����Ϣ, ��������Ļ�����Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_INFO
        {
            public byte byRuleID;//����ID,0-7
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//��������
            public VCA_EVENT_TYPE dwEventType;//�����¼�����
            public tagNET_VCA_EVENT_UNION uEventParam;//�¼�����
        }

        //ǰ���豸��ַ��Ϣ�����ܷ����Ǳ�ʾ����ǰ���豸�ĵ�ַ��Ϣ�������豸��ʾ�����ĵ�ַ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;//ǰ���豸��ַ��
            public ushort wPort;//ǰ���豸�˿ںţ� 
            public byte byChannel;//ǰ���豸ͨ����
            public byte byRes;// �����ֽ�
        }

        //��Ϊ��������ϱ��ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_ALARM
        {
            public uint dwSize;//�ṹ����
            public uint dwRelativeTime;//���ʱ��
            public uint dwAbsTime;//����ʱ��
            public tagNET_VCA_RULE_INFO struRuleInfo;//�¼�������Ϣ
            public tagNET_VCA_TARGET_INFO struTargetInfo;//����Ŀ����Ϣ
            public tagNET_VCA_DEV_INFO struDevInfo;//ǰ���豸��Ϣ
            public uint dwPicDataLen;//����ͼƬ�ĳ��� Ϊ0��ʾû��ͼƬ������0��ʾ�ýṹ�������ͼƬ����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//����������Ϊ0
            public IntPtr pImage;//ָ��ͼƬ��ָ��
        }

        //�����ؼ���
        public enum IVS_PARAM_KEY
        {
            OBJECT_DETECT_SENSITIVE = 1,//Ŀ����������
            BACKGROUND_UPDATE_RATE = 2,//���������ٶ�
            SCENE_CHANGE_RATIO = 3,//�����仯�����Сֵ
            SUPPRESS_LAMP = 4,//�Ƿ����Ƴ�ͷ��
            MIN_OBJECT_SIZE = 5,//�ܼ�������СĿ���С
            OBJECT_GENERATE_RATE = 6,//Ŀ�������ٶ�
            MISSING_OBJECT_HOLD = 7,//Ŀ����ʧ���������ʱ��
            MAX_MISSING_DISTANCE = 8,//Ŀ����ʧ��������پ���
            OBJECT_MERGE_SPEED = 9,//���Ŀ�꽻��ʱ��Ŀ����ں��ٶ�
            REPEATED_MOTION_SUPPRESS = 10,//�ظ��˶�����
            ILLUMINATION_CHANGE = 11,//��Ӱ�仯���ƿ���
            TRACK_OUTPUT_MODE = 12,//�켣���ģʽ��0-���Ŀ������ģ�1-���Ŀ��ĵײ�����
            ENTER_CHANGE_HOLD = 13,//�������仯��ֵ
            RESUME_DEFAULT_PARAM = 255,//�ָ�Ĭ�Ϲؼ��ֲ���
        }

        //����/��ȡ�����ؼ���
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, int nValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, ref int pValue);

        //��Ϊ��������DSP��Ϣ���ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;//�����Ƿ����Ŀ��
            public byte byDspAddRule;//�����Ƿ���ӹ���
            public byte byDspPicAddTarget;//ץͼ�Ƿ����Ŀ��
            public byte byDspPicAddRule;//ץͼ�Ƿ���ӹ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //��ȡ/������Ϊ����Ŀ����ӽӿ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        //�궨���ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CB_POINT
        {
            public tagNET_VCA_POINT struPoint;//�궨�㣬���������ǹ����
            public NET_DVR_PTZPOS struPtzPos;//��������PTZ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�궨�������ýṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CALIBRATION_PARAM
        {
            public byte byPointNum;//��Ч�궨�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CALIB_PT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_CB_POINT[] struCBPoint;//�궨����
        }

        //LF˫��������ýṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CFG
        {
            public uint dwSize;//�ṹ����    
            public byte byEnable;//�궨ʹ��
            public byte byFollowChan;// �����ƵĴ�ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public tagNET_DVR_LF_CALIBRATION_PARAM struCalParam;//�궨����
        }

        //L/F����ģʽ
        public enum TRACK_MODE
        {
            MANUAL_CTRL = 0,//�ֶ�����
            ALARM_TRACK,//������������
            TARGET_TRACK,//Ŀ�����
        }

        //L/F�ֶ����ƽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_MANUAL_CTRL_INFO
        {
            public tagNET_VCA_POINT struCtrlPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //L/FĿ����ٽṹ
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
            public uint dwSize;//�ṹ����
            public byte byTrackMode;//����ģʽ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//��������0
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct uModeParam
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
                [FieldOffsetAttribute(0)]
                public uint[] dwULen;
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_MANUAL_CTRL_INFO struManualCtrl;//�ֶ����ٽṹ
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_TRACK_TARGET_INFO struTargetTrack;//Ŀ����ٽṹ
            }
        }

        //˫���������ģʽ���ýӿ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        //ʶ�𳡾�
        public enum VCA_RECOGNIZE_SCENE
        {
            VCA_LOW_SPEED_SCENE = 0,//����ͨ���������շ�վ��С���ſڡ�ͣ������
            VCA_HIGH_SPEED_SCENE = 1,//����ͨ�����������ڡ����ٹ�·���ƶ�����)
            VCA_MOBILE_CAMERA_SCENE = 2,//�ƶ������Ӧ�ã� 
        }

        //ʶ������־
        public enum VCA_RECOGNIZE_RESULT
        {
            VCA_RECOGNIZE_FAILURE = 0,//ʶ��ʧ��
            VCA_IMAGE_RECOGNIZE_SUCCESS,//ͼ��ʶ��ɹ�
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_BEST_LICENSE,//��Ƶʶ����Ž��
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_NEW_LICENSE,//��Ƶʶ���µĳ���
            VCA_VIDEO_RECOGNIZE_FINISH_OF_CUR_LICENSE,//��Ƶʶ���ƽ���
        }

        //������ɫ
        public enum VCA_PLATE_COLOR
        {
            VCA_BLUE_PLATE = 0,//��ɫ����
            VCA_YELLOW_PLATE,//��ɫ����
            VCA_WHITE_PLATE,//��ɫ����
            VCA_BLACK_PLATE,       //��ɫ����
            VCA_GREEN_PLATE,       //��ɫ����
            VCA_BKAIR_PLATE,       //�񺽺�ɫ����
            VCA_OTHER = 0xff       //����
        }

        //��������
        public enum VCA_PLATE_TYPE
        {
            VCA_STANDARD92_PLATE = 0,    //��׼���ó������
            VCA_STANDARD02_PLATE,  //02ʽ���ó��� 
            VCA_WJPOLICE_PLATE,      //�侯�� 
            VCA_JINGCHE_PLATE,      //����
            STANDARD92_BACK_PLATE,         //���ó�˫��β��
            VCA_SHIGUAN_PLATE,          //ʹ�ݳ���
            VCA_NONGYONG_PLATE,         //ũ�ó�
            VCA_MOTO_PLATE              //Ħ�г�
        }

        //��Ƶʶ�𴥷�����
        public enum VCA_TRIGGER_TYPE
        {
            INTER_TRIGGER = 0,// ģ���ڲ�����ʶ��
            EXTER_TRIGGER = 1,// �ⲿ�����źŴ�������Ȧ���״�ֶ������źţ�
        }

        public const int MAX_CHINESE_CHAR_NUM = 64;    // ������������
        //���ƿɶ�̬�޸Ĳ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_PARAM
        {
            public NET_VCA_RECT struSearchRect;//��������(��һ��)
            public NET_VCA_RECT struInvalidateRect;//��Ч���������������ڲ� (��һ��)
            public ushort wMinPlateWidth;//������С����
            public ushort wTriggerDuration;//��������֡��
            public byte byTriggerType;//����ģʽ, VCA_TRIGGER_TYPE
            public byte bySensitivity;//������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//��������0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byCharPriority;// �������ȼ�
        }

        /*wMinPlateWidth:�ò���Ĭ������Ϊ80���أ��ò��������ö��ڳ��ƺ������ӳ���ʶ��˵���ĵ� 
        ʶ����Ӱ�죬������ù�����ô��������г���С���ƾͻ�©ʶ����������г��ƿ����ձ�ϴ󣬿��԰Ѹò��������Դ󣬱��ڼ��ٶ���ٳ��ƵĴ������ڱ�������½�������Ϊ80�� �ڸ�������½�������Ϊ120
        wTriggerDuration �� �ⲿ�����źų���֡�������京���ǴӴ����źſ�ʼʶ���֡��������ֵ�ڵ��ٳ�����������Ϊ50��100�����ٳ�����������Ϊ15��25���ƶ�ʶ��ʱ���Ҳ���ⲿ����������Ϊ15��25��������Ը����ֳ������������
        */
        //����ʶ������ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATEINFO
        {
            public VCA_RECOGNIZE_SCENE eRecogniseScene;//ʶ�𳡾�(���ٺ͸���)
            public tagNET_VCA_PLATE_PARAM struModifyParam;//���ƿɶ�̬�޸Ĳ���
        }

        //����ʶ�����ò���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATECFG
        {
            public uint dwSize;
            public byte byPicProType;//����ʱͼƬ������ʽ 0-������ 1-�ϴ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����������Ϊ0
            public NET_DVR_JPEGPARA struPictureParam;//ͼƬ���ṹ
            public tagNET_VCA_PLATEINFO struPlateInfo;//������Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;//������ʽ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;//����������¼��ͨ��,Ϊ1��ʾ������ͨ��
        }

        //����ʶ�����ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_VCA_PLATE_INFO
        {
            public VCA_RECOGNIZE_RESULT eResultFlag;//ʶ������־ 
            public VCA_PLATE_TYPE ePlateType;//��������
            public VCA_PLATE_COLOR ePlateColor;//������ɫ
            public NET_VCA_RECT struPlateRect;//����λ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//����������Ϊ0 
            public uint dwLicenseLen;//���Ƴ���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense;//���ƺ��� 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sBelieve;//����ʶ���ַ������Ŷȣ����⵽����"��A12345", ���Ŷ�Ϊ10,20,30,40,50,60,70�����ʾ"��"����ȷ�Ŀ�����ֻ��10%��"A"�ֵ���ȷ�Ŀ�������20%
        }

        //���Ƽ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_RESULT
        {
            public uint dwSize;//�ṹ����
            public uint dwRelativeTime;//���ʱ��
            public uint dwAbsTime;//����ʱ��
            public byte byPlateNum;//���Ƹ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PLATE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_PLATE_INFO[] struPlateInfo;//������Ϣ�ṹ
            public uint dwPicDataLen;//����ͼƬ�ĳ��� Ϊ0��ʾû��ͼƬ������0��ʾ�ýṹ�������ͼƬ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes2;//����������Ϊ0 ͼƬ�ĸ߿�
            public System.IntPtr pImage;//ָ��ͼƬ��ָ��
        }

        //��������Ϊ��������ṹ
        //�������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ONE_RULE_
        {
            public byte byActive;/* �Ƿ񼤻����,0-��, ��0-�� */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����������Ϊ0�ֶ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//��������
            public VCA_EVENT_TYPE dwEventType;//��Ϊ�����¼�����
            public tagNET_VCA_EVENT_UNION uEventParam;//��Ϊ�����¼�����
            public tagNET_VCA_SIZE_FILTER struSizeFilter;//�ߴ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;/*����������Ϊ0*/
        }

        // �����ǹ���ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_RULECFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_ONE_RULE_[] struRule; //��������
        }

        // IVMS��Ϊ�������ýṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_BEHAVIORCFG
        {
            public uint dwSize;
            public byte byPicProType;//����ʱͼƬ������ʽ 0-������ ��0-�ϴ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;//ͼƬ���ṹ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_RULECFG[] struRuleCfg;//ÿ��ʱ��ζ�Ӧ����
        }

        //���ܷ�����ȡ���ƻ��ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_DEVSCHED
        {
            public NET_DVR_SCHEDTIME struTime;//ʱ�����
            public NET_DVR_PU_STREAM_CFG struPUStream;//ǰ��ȡ������
        }

        //���ܷ����ǲ������ýṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_STREAMCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_DEVSCHED[] struDevSched;//��ʱ�������ǰ��ȡ���Լ�������Ϣ
        }

        //��������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION
        {
            public byte byEnable;//�Ƿ񼤻�, 0-�񣬷�0-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//��������0
            public tagNET_VCA_POLYGON struPolygon;//���ζ����
        }

        //�������������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION_LIST
        {
            public uint dwSize;//�ṹ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //��������0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MASK_REGION_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION[] struMask;//������������
        }

        //ATM�����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ENTER_REGION
        {
            public uint dwSize;
            public byte byEnable;//�Ƿ񼤻0-�񣬷�0-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public tagNET_VCA_POLYGON struPolygon;//��������
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
		
        //    �������ܿ�
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_VCA_RestartLib(int lUserID, int lChannel);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEGMENT
        {
            public tagNET_VCA_POINT struStartPoint;//��ʾ�߶���ʱ����ʾͷ����
            public tagNET_VCA_POINT struEndPoint;//��ʾ�߶���ʱ����ʾ�Ų���
            public float fValue;//�߶�ֵ����λ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�궨������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEG_LIST
        {
            public uint dwSize;//�ṹ����
            public byte bySegNum;//�궨������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;//��������0
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SEGMENT_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_LINE_SEGMENT[] struSeg;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealHeight(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpHeight);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealLength(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpLength);

        //IVMS������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_MASK_REGION_LIST
        {
            public uint dwSize;//�ṹ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION_LIST[] struList;
        }

        //IVMS��ATM�����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ENTER_REGION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ENTER_REGION[] struEnter;//��������
        }

        // ivms ����ͼƬ�ϴ��ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ALARM_JPEG
        {
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;
        }

        // IVMS ���������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_SEARCHCFG
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DEC_REMOTE_PLAY struRemotePlay;// Զ�̻ط�
            public tagNET_IVMS_ALARM_JPEG struAlarmJpeg;// �����ϴ�ͼƬ����
            public tagNET_IVMS_RULECFG struRuleCfg;//IVMS ��Ϊ��������
        }

        //2009-7-22
        public const int NET_DVR_GET_AP_INFO_LIST = 305;//��ȡ����������Դ����
        public const int NET_DVR_SET_WIFI_CFG = 306;//����IP����豸���߲���
        public const int NET_DVR_GET_WIFI_CFG = 307;//��ȡIP����豸���߲���
        public const int NET_DVR_SET_WIFI_WORKMODE = 308;//����IP����豸���ڹ���ģʽ����
        public const int NET_DVR_GET_WIFI_WORKMODE = 309;//��ȡIP����豸���ڹ���ģʽ����

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
            public uint dwMode;/* 0 mange ģʽ;1 ad-hocģʽ���μ�NICMODE */
            public uint dwSecurity;  /*0 �����ܣ�1 wep���ܣ�2 wpa-psk;3 wpa-Enterprise���μ�WIFISECURITY*/
            public uint dwChannel;/*1-11��ʾ11��ͨ��*/
            public uint dwSignalStrength;/*0-100�ź���������Ϊ��ǿ*/
            public uint dwSpeed;/*����,��λ��0.01mbps*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_AP_INFO_LIST
        {
            public uint dwSize;
            public uint dwCount;/*����AP������������20*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = WIFI_MAX_AP_COUNT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_AP_INFO[] struApInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_WIFIETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpAddress;/*IP��ַ*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpMask;/*����*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;/*������ַ��ֻ������ʾ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes;
            public uint dwEnableDhcp;/*�Ƿ�����dhcp  0������ 1����*/
            public uint dwAutoDns;/*�������dhcp�Ƿ��Զ���ȡdns,0���Զ���ȡ 1�Զ���ȡ�����������������dhcpĿǰ�Զ���ȡdns*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDns; /*��һ��dns����*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDns;/*�ڶ���dns����*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIpAddr;/* ���ص�ַ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR__WIFI_CFG_EX
        {
            public tagNET_DVR_WIFIETHERNET struEtherNet;/*wifi����*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sEssid;/*SSID*/
            public uint dwMode;/* 0 mange ģʽ;1 ad-hocģʽ���μ�*/
            public uint dwSecurity;/*0 �����ܣ�1 wep���ܣ�2 wpa-psk; */
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct key
            {
                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wep
                {
                    public uint dwAuthentication;/*0 -����ʽ 1-����ʽ*/
                    public uint dwKeyLength;/* 0 -64λ��1- 128λ��2-152λ*/
                    public uint dwKeyType;/*0 16����;1 ASCI */
                    public uint dwActive;/*0 ������0---3��ʾ����һ����Կ*/
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WEP_MAX_KEY_COUNT * WIFI_WEP_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                }

                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wpa_psk
                {
                    public uint dwKeyLength;/*8-63��ASCII�ַ�*/
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
            public uint dwNetworkInterfaceMode;/*0 �Զ��л�ģʽ��1 ����ģʽ*/
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData_V30(int lRealHandle, uint dwTransType, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        //2009-7-22 end

        //SDK 9000_1.1
        //����Ӳ�̽ṹ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SINGLE_NET_DISK_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public NET_DVR_IPADDR struNetDiskAddr;//����Ӳ�̵�ַ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDirectory;// PATHNAME_LEN = 128
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//����
        }

        public const int MAX_NET_DISK = 16;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_NET_DISKCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NET_DISK, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_SINGLE_NET_DISK_INFO[] struNetDiskParam;
        }

        //�¼�����
        //������
        public enum MAIN_EVENT_TYPE
        {
            EVENT_MOT_DET = 0,//�ƶ����
            EVENT_ALARM_IN = 1,//��������
            EVENT_VCA_BEHAVIOR = 2,//��Ϊ����
        }

        //��Ϊ���������Ͷ�Ӧ�Ĵ����ͣ� 0xffff��ʾȫ��
        public enum BEHAVIOR_MINOR_TYPE
        {
            EVENT_TRAVERSE_PLANE = 0,// ��Խ������,
            EVENT_ENTER_AREA,//Ŀ���������,֧���������
            EVENT_EXIT_AREA,//Ŀ���뿪����,֧���������
            EVENT_INTRUSION,// �ܽ�����,֧���������
            EVENT_LOITER,//�ǻ�,֧���������
            EVENT_LEFT_TAKE,//�������,֧���������
            EVENT_PARKING,//ͣ��,֧���������
            EVENT_RUN,//����,֧���������
            EVENT_HIGH_DENSITY,//��������Ա�ܶ�,֧���������
            EVENT_STICK_UP,//��ֽ��,֧���������
            EVENT_INSTALL_SCANNER,//��װ������,֧���������
        }

        //�¼��������� 200-04-07 9000_1.1
        public const int SEARCH_EVENT_INFO_LEN = 300;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        //��������
        public struct struAlarmParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInNo;//��������ţ�byAlarmInNo[0]����1���ʾ�����ɱ�������1�������¼�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byAlarmInNo = new byte[MAX_ALARMIN_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //�ƶ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotDetChanNo;//�ƶ����ͨ����byMotDetChanNo[0]����1���ʾ������ͨ��1�����ƶ���ⴥ�����¼�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byMotDetChanNo = new byte[MAX_CHANNUM_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        //��Ϊ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChanNo;//�����¼���ͨ��
            public byte byRuleID;//����ID��0xff��ʾȫ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 43, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����

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
            public ushort wMajorType;//0-�ƶ���⣬1-��������, 2-�����¼�
            public ushort wMinorType;//����������- ���������ͱ仯��0xffff��ʾȫ��
            public NET_DVR_TIME struStartTime;//�����Ŀ�ʼʱ�䣬ֹͣʱ��: ͬʱΪ(0, 0) ��ʾ�������ʱ�俪ʼ���������ǰ���4000���¼�
            public NET_DVR_TIME struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
            public uSeniorParam uSeniorPara;

            public void init()
            {
                byRes = new byte[132];
                uSeniorPara = new uSeniorParam();
                uSeniorPara.init();
            }
        }

        //����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struAlarmRet
        {
            public uint dwAlarmInNo;//���������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //�ƶ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionRet
        {
            public uint dwMotDetNo;//�ƶ����ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        //��Ϊ������� 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaRet
        {
            public uint dwChanNo;//�����¼���ͨ����
            public byte byRuleID;//����ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//��������
            public tagNET_VCA_EVENT_UNION uEvent;//��Ϊ�¼�������wMinorType = VCA_EVENT_TYPE�����¼�����

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
        //���ҷ��ؽ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_RET
        {
            public ushort wMajorType;//������MA
            public ushort wMinorType;//������
            public NET_DVR_TIME struStartTime;//�¼���ʼ��ʱ��
            public NET_DVR_TIME struEndTime;//�¼�ֹͣ��ʱ�䣬�����¼�ʱ�Ϳ�ʼʱ��һ��
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


        //�ʼ�������� 9000_1.1
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EmailTest(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByEvent(int lUserID, ref tagNET_DVR_SEARCH_EVENT_PARAM lpSearchEventParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextEvent(int lSearchHandle, ref tagNET_DVR_SEARCH_EVENT_RET lpSearchEventRet);


        //2009-8-18 ץ�Ļ�
        public const int PLATE_INFO_LEN = 1024;
        public const int PLATE_NUM_LEN = 16;
        public const int FILE_NAME_LEN = 256;

        // ������ɫ
        public enum Anonymous_26594f67_851c_4f7d_bec4_094765b7ff83
        {
            BLUE_PLATE, // ��ɫ���� 
            YELLOW_PLATE, // ��ɫ����
            WHITE_PLATE,// ��ɫ����
            BLACK_PLATE,// ��ɫ����
        }

        //liscense plate result
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PLATE_RET
        {
            public uint dwSize;//�ṹ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPlateNum;//���ƺ�
            public byte byVehicleType;// ������
            public byte byTrafficLight;//0-�̵ƣ�1-���
            public byte byPlateColor;//������ɫ
            public byte byDriveChan;//����������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byTimeInfo;/*ʱ����Ϣ*///plate_172.6.113.64_20090724155526948_197170484 
            //Ŀǰ��17λ����ȷ��ms:20090724155526948
            public byte byCarSpeed;/*��λkm/h*/
            public byte byCarSpeedH;/*cm/s��8λ*/
            public byte byCarSpeedL;/*cm/s��8λ*/
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_INFO_LEN - 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byInfo;
            public uint dwPicLen;
        }
        /*ע��������� dwPicLen ���ȵ� ͼƬ ��Ϣ*/

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_INVOKE_PLATE_RECOGNIZE(int lUserID, int lChannel, string pPicFileName, ref tagNET_DVR_PLATE_RET pPlateRet, string pPicBuf, uint dwPicBufLen);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CCD_CFG
        {
            public uint dwSize;//�ṹ����
            public byte byBlc;/*���ⲹ��0-off; 1-on*/
            public byte byBlcMode;/*blc����0-�Զ���1-�ϣ�2-�£�3-��4-�ң�5-�У�ע��������blcΪ on ʱ����Ч*/
            public byte byAwb;/*�Զ���ƽ��0-�Զ�1; 1-�Զ�2; 2-�Զ�����*/
            public byte byAgc;/*�Զ�����0-��; 1-��; 2-��; 3-��*/
            public byte byDayNight;/*��ҹת����0 ��ɫ��1�ڰף�2�Զ�*/
            public byte byMirror;/*����0-��;1-����;2-����;3-����*/
            public byte byShutter;/*����0-�Զ�; 1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k;*/
            public byte byIrCutTime;/*IRCUT�л�ʱ�䣬5, 10, 15, 20, 25*/
            public byte byLensType;/*��ͷ����0-���ӹ�Ȧ; 1-�Զ���Ȧ*/
            public byte byEnVideoTrig;/*��Ƶ����ʹ�ܣ�1-֧�֣�0-��֧�֡���Ƶ����ģʽ����Ƶ�����ٶȰ���byShutter�ٶȣ�ץ��ͼƬ�Ŀ����ٶȰ���byCapShutter�ٶȣ�ץ����ɺ���Զ����ڻ���Ƶģʽ*/
            public byte byCapShutter;/*ץ��ʱ�Ŀ����ٶȣ�1-1/25; 2-1/50; 3-1/100; 4-1/250;5-1/500; 6-1/1k ;7-1/2k; 8-1/4k; 9-1/10k; 10-1/100k; 11-1/150; 12-1/200*/
            public byte byEnRecognise;/*1-֧��ʶ��0-��֧��ʶ��*/
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
            public uint dwWhiteBalanceMode;/*0�ֶ���ƽ��; 1�Զ���ƽ��1����ΧС��; 2 �Զ���ƽ��2����Χ����2200K-15000K��;3�Զ�����3*/
            public uint dwWhiteBalanceModeRGain;/*�ֶ���ƽ��ʱ��Ч���ֶ���ƽ�� R����*/
            public uint dwWhiteBalanceModeBGain;/*�ֶ���ƽ��ʱ��Ч���ֶ���ƽ�� B����*/
            public uint dwExposureMode;/*0 �ֶ��ع� 1�Զ��ع�*/
            public uint dwExposureSet;/* 0-USERSET, 1-�Զ�x2��2-�Զ�4��3-�Զ�81/25, 4-1/50, 5-1/100, 6-1/250, 7-1/500, 8-1/750, 9-1/1000, 10-1/2000, 11-1/4000,12-1/10,000; 13-1/100,000*/
            public uint dwExposureUserSet;/* �Զ��Զ����ع�ʱ��*/
            public uint dwExposureTarget;/*�ֶ��ع�ʱ�� ��Χ��Manumal��Ч��΢�룩*/
            public uint dwIrisMode;/*0 �Զ���Ȧ 1�ֶ���Ȧ*/
            public uint dwGainLevel;/*���棺0-100*/
            public uint dwBrightnessLevel;/*0-100*/
            public uint dwContrastLevel;/*0-100*/
            public uint dwSharpnessLevel;/*0-100*/
            public uint dwSaturationLevel;/*0-100*/
            public uint dwHueLevel;/*0-100����������*/
            public uint dwGammaCorrectionEnabled;/*0 dsibale  1 enable*/
            public uint dwGammaCorrectionLevel;/*0-100*/
            public uint dwWDREnabled;/*����̬��0 dsibale  1 enable*/
            public uint dwWDRLevel1;/*0-F*/
            public uint dwWDRLevel2;/*0-F*/
            public uint dwWDRContrastLevel;/*0-100*/
            public uint dwDayNightFilterType;/*��ҹ�л���0 day,1 night,2 auto */
            public uint dwSwitchScheduleEnabled;/*0 dsibale  1 enable,(����)*/
            //ģʽ1(����)
            public uint dwBeginTime;    /*0-100*/
            public uint dwEndTime;/*0-100*/
            //ģʽ2
            public uint dwDayToNightFilterLevel;//0-7
            public uint dwNightToDayFilterLevel;//0-7
            public uint dwDayNightFilterTime;//(60��)
            public uint dwBacklightMode;/*���ⲹ��:0 USERSET 1 UP��2 DOWN��3 LEFT��4 RIGHT��5MIDDLE*/
            public uint dwPositionX1;//��X����1��
            public uint dwPositionY1;//��Y����1��
            public uint dwPositionX2;//��X����2��
            public uint dwPositionY2;//��Y����2��
            public uint dwBacklightLevel;/*0x0-0xF*/
            public uint dwDigitalNoiseRemoveEnable; /*����ȥ�룺0 dsibale  1 enable*/
            public uint dwDigitalNoiseRemoveLevel;/*0x0-0xF*/
            public uint dwMirror; /* ����0 Left;1 Right,;2 Up;3Down */
            public uint dwDigitalZoom;/*��������:0 dsibale  1 enable*/
            public uint dwDeadPixelDetect;/*������,0 dsibale  1 enable*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        public const int NET_DVR_GET_CCDPARAMCFG = 1067;       //IPC��ȡCCD��������
        public const int NET_DVR_SET_CCDPARAMCFG = 1068;      //IPC����CCD��������

        //ͼ����ǿ��
        //ͼ����ǿȥ����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEREGION
        {
            public uint dwSize;//�ܵĽṹ����
            public ushort wImageRegionTopLeftX;/* ͼ����ǿȥ�������x���� */
            public ushort wImageRegionTopLeftY;/* ͼ����ǿȥ�������y���� */
            public ushort wImageRegionWidth;/* ͼ����ǿȥ������Ŀ� */
            public ushort wImageRegionHeight;/*ͼ����ǿȥ������ĸ�*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ͼ����ǿ��ȥ�뼶���ȶ���ʹ������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGESUBPARAM
        {
            public NET_DVR_SCHEDTIME struImageStatusTime;//ͼ��״̬ʱ���
            public byte byImageEnhancementLevel;//ͼ����ǿ�ļ���0-7��0��ʾ�ر�
            public byte byImageDenoiseLevel;//ͼ��ȥ��ļ���0-7��0��ʾ�ر�
            public byte byImageStableEnable;//ͼ���ȶ���ʹ�ܣ�0��ʾ�رգ�1��ʾ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_IMAGEREGION = 1062;       //ͼ����ǿ��ͼ����ǿȥ�������ȡ
        public const int NET_DVR_SET_IMAGEREGION = 1063;       //ͼ����ǿ��ͼ����ǿȥ�������ȡ
        public const int NET_DVR_GET_IMAGEPARAM = 1064;       // ͼ����ǿ��ͼ�����(ȥ�롢��ǿ�����ȶ���ʹ��)��ȡ
        public const int NET_DVR_SET_IMAGEPARAM = 1065;       // ͼ����ǿ��ͼ�����(ȥ�롢��ǿ�����ȶ���ʹ��)����

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEPARAM
        {
            public uint dwSize;
            //ͼ����ǿʱ��β������ã����տ�ʼ    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagIMAGESUBPARAM[] struImageParamSched;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetParamSetMode(int lUserID, ref uint dwParamSetMode);

        public struct NET_DVR_CLIENTINFO
        {
            public Int32 lChannel;//ͨ����
            public uint lLinkMode;//���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-����Ƶ�ֿ�(TCP)
            public IntPtr hPlayWnd;//���Ŵ��ڵľ��,ΪNULL��ʾ������ͼ��
            public string sMultiCastIP;//�ಥ���ַ
        }

        public struct NET_SDK_CLIENTINFO
        {
            public Int32 lChannel;//ͨ����
            public Int32 lLinkType; //����sdk�ķ�ʽ���Ƿ�ͨ����ý��ı�־
            public Int32 lLinkMode;//���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-����Ƶ�ֿ�(TCP)
            public IntPtr hPlayWnd;//���Ŵ��ڵľ��,ΪNULL��ʾ������ͼ��
            public string sMultiCastIP;//�ಥ���ַ
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
        Input:    sDVRIP [in] �豸IP��ַ 
                wServerPort [in] �豸�˿ں� 
                sUserName [in] ��¼���û��� 
                sPassword [in] �û����� 
        Output:    lpDeviceInfo [out] �豸��Ϣ 
        Return:    -1��ʾʧ�ܣ�����ֵ��ʾ���ص��û�IDֵ
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);

        /*********************************************************
        Function:    NET_DVR_Logout_V30
        Desc:  �û�ע���豸��
        Input:    lUserID [in] �û�ID��
        Output:    
        Return:    TRUE��ʾ�ɹ���FALSE��ʾʧ��
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
        Desc:  �ֶ��������ġ�
        Input:        lUserID [in] �û�ID��
                    lpInter [in] �ֶ����Ĳ����ṹ
        Output:    
        Return:    TRUE��ʾ�ɹ���FALSE��ʾʧ��
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ContinuousShoot(Int32 lUserID, ref NET_DVR_SNAPCFG lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ManualSnap(Int32 lUserID, ref NET_DVR_MANUALSNAP lpInter, ref NET_DVR_PLATE_RESULT lpOuter);

        #region  ȡ��ģ����ؽṹ��ӿ�

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PLAY_INFO
        {
            public int iUserID;      //ע���û�ID
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strDeviceIP;
            public int iDevicePort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string strDevAdmin;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string strDevPsd;
            public int iChannel;      //����ͨ����(��0��ʼ)
            public int iLinkMode;   //���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-����Ƶ�ֿ�(TCP)
            public bool bUseMedia;     //�Ƿ�������ý��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strMediaIP; //��ý��IP��ַ
            public int iMediaPort;   //��ý��˿ں�
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


        #region VOD�㲥�ſ�

        public const int WM_NETERROR = 0x0400 + 102;          //�����쳣��Ϣ
        public const int WM_STREAMEND = 0x0400 + 103;    //�ļ����Ž���

        public const int FILE_HEAD = 0;      //�ļ�ͷ
        public const int VIDEO_I_FRAME = 1;  //��ƵI֡
        public const int VIDEO_B_FRAME = 2;  //��ƵB֡
        public const int VIDEO_P_FRAME = 3;  //��ƵP֡
        public const int VIDEO_BP_FRAME = 4; //��ƵBP֡
        public const int VIDEO_BBP_FRAME = 5; //��ƵB֡B֡P֡
        public const int AUDIO_PACKET = 10;   //��Ƶ��

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
            public IntPtr sessionHandle;                                    //[in]VOD�ͻ��˾��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //    [in]DVR�������ַ
            public uint dvrPort;                                            //    [in]DVR�Ķ˿ڵ�ַ
            public uint channelNum;                                         //  [in]DVR��ͨ����
            public BLOCKTIME startTime;                                     //    [in]��ѯ�Ŀ�ʼʱ��
            public BLOCKTIME stopTime;                                      //    [in]��ѯ�Ľ���ʱ��
            public bool bUseIPServer;                                       //  [in]�Ƿ�ʹ��IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]�豸�����к�
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
            public IntPtr sessionHandle;                                    //[in]VOD�ͻ��˾��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            //    [in]DVR�������ַ
            public uint dvrPort;                                            //    [in]DVR�Ķ˿ڵ�ַ
            public uint channelNum;                                         //  [in]DVR��ͨ����
            public BLOCKTIME startTime;                                     //    [in]��ѯ�Ŀ�ʼʱ��
            public BLOCKTIME stopTime;                                      //    [in]��ѯ�Ľ���ʱ��
            public uint uiUser;
            public bool bUseIPServer;                                       //  [in]�Ƿ�ʹ��IPServer 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     //  [in]�豸�����к�

            public VodStreamFrameData streamFrameData;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CONNPARAM
        {
            public uint uiUser;
            public ErrorCallback errorCB;
        }


        // �쳣�ص�����
        public delegate void ErrorCallback(System.IntPtr hSession, uint dwUser, int lErrorType);
        //֡���ݻص�����
        public delegate void VodStreamFrameData(System.IntPtr hStream, uint dwUser, int lFrameType, System.IntPtr pBuffer, uint dwSize);

        //ģ���ʼ��
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerConnect(string strServerIp, uint uiServerPort, ref IntPtr hSession, ref CONNPARAM struConn, IntPtr hWnd);

        //ģ������
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerDisconnect(IntPtr hSession);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStreamSearch(IntPtr pSearchParam, ref IntPtr pSecList);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODDeleteSectionList(IntPtr pSecList);

        // ����ID��ʱ��δ�����ȡ�����
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenStream(IntPtr pOpenParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseStream(IntPtr hStream);

        //����ID��ʱ��δ���������
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenDownloadStream(ref VODOPENPARAM struVodParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseDownloadStream(IntPtr hStream);

        // ��ʼ����������������֡
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStartStreamData(IntPtr phStream);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODPauseStreamData(IntPtr hStream, bool bPause);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStopStreamData(IntPtr hStream);

        // ����ʱ�䶨λ
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSeekStreamData(IntPtr hStream, IntPtr pStartTime);


        // ����ʱ�䶨λ
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSetStreamSpeed(IntPtr hStream, int iSpeed);

        // ����ʱ�䶨λ
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODGetStreamCurrentTime(IntPtr hStream, ref BLOCKTIME pCurrentTime);

        #endregion


        #region ֡������


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
        * function��get a empty port number
        * parameters��
        * return�� 0 - 499 : empty port number
        *          -1      : server is full        
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetSafeHandle();


        /******************************************************************************
        * function��open standard stream data for analyzing
        * parameters��lHandle - working port number
        *             pHeader - pointer to file header or info header
        * return��TRUE or FALSE
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataOpenStreamEx(int iHandle, byte[] pFileHead);


        /******************************************************************************
        * function��close analyzing
        * parameters��lHandle - working port number
        * return��
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataClose(int iHandle);


        /******************************************************************************
        * function��input stream data
        * parameters��lHandle  - working port number
        *        pBuffer  - data pointer
        *        dwBuffersize    - data size
        * return��TRUE or FALSE
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataInputData(int iHandle, IntPtr pBuffer, uint uiSize); //byte []


        /******************************************************************************
        * function��get analyzed packet
        * parameters��lHandle  - working port number
        *        pPacketInfo    - returned structure
        * return��-1 : error
        *          0 : succeed
        *     1 : failed
        *     2 : file end (only in file mode)    
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern int AnalyzeDataGetPacket(int iHandle, ref PACKET_INFO pPacketInfo);  //Ҫ��pPacketInfoת����PACKET_INFO�ṹ


        /******************************************************************************
        * function��get remain data from input buffer
        * parameters��lHandle  - working port number
        *        pBuf            - pointer to the mem which stored remain data
        *             dwSize        - size of remain data  
        * return�� TRUE or FALSE    
        * comment��
        ******************************************************************************/
        [DllImport("AnalyzeData.dll")]
        public static extern bool AnalyzeDataGetTail(int iHandle, ref IntPtr pBuffer, ref uint uiSize);


        [DllImport("AnalyzeData.dll")]
        public static extern uint AnalyzeDataGetLastError(int iHandle);

        #endregion


        #region ¼���

        public const int DATASTREAM_HEAD = 0;  //����ͷ
        public const int DATASTREAM_BITBLOCK = 1;  //�ֽ�����
        public const int DATASTREAM_KEYFRAME = 2;  //�ؼ�֡����
        public const int DATASTREAM_NORMALFRAME = 3;  //�ǹؼ�֡����


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

        //�豸��������
        public const int REGIONTYPE = 0;//��������
        public const int MATRIXTYPE = 11;//����ڵ�
        public const int DEVICETYPE = 2;//�����豸
        public const int CHANNELTYPE = 3;//����ͨ��
        public const int USERTYPE = 5;//�����û�

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
            public byte byDevSequence;//��λ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC��ַ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//���к�
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN - SERIALNO_LEN - MACADDR_LEN - 1)]
            public string sInfo;
        }

        //��Ƶ�ۺ�ƽ̨����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagVEDIOPLATLOG
        {
            public byte bySearchCondition;//����������0-����λ��������1-�����к����� 2-��MAC��ַ��������
            public byte byDevSequence;//��λ�ţ�0-79����Ӧ��ϵͳ�Ĳ�λ�ţ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;//���к�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;//MAC��ַ
        }

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_MATRIX(int iLogHandle, ref NET_DVR_LOG_MATRIX lpLogData);


        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_Matrix(int iUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref tagVEDIOPLATLOG lpVedioPlatLog, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);




        /*************************************************
        ����Զ�����ú궨�� 
        NET_DVR_StartRemoteConfig
        ����֧�ֲ鿴����˵���ʹ���
        **************************************************/
        public const int MAX_CARDNO_LEN = 48;
        public const int MAX_OPERATE_INDEX_LEN = 32;
        public const int NET_DVR_GET_ALL_VEHICLE_CONTROL_LIST = 3124;
        public const int NET_DVR_VEHICLE_DELINFO_CTRL = 3125; 
        public const int NET_DVR_VEHICLELIST_CTRL_START = 3133;

        /*********************************************************
        Function:    REMOTECONFIGCALLBACK
        Desc:  (�ص�����)
        Input:    
        Output:    
        Return:    
        **********************************************************/
        public delegate void REMOTECONFIGCALLBACK(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_StartRemoteConfig(Int32 lUserID, uint dwCommand, IntPtr lpInBuffer, Int32 dwInBufferLen, REMOTECONFIGCALLBACK cbStateCallback, IntPtr pUserData);

        // �������ͳ�����������رճ��������ýӿ��������ľ�����ͷ���Դ
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(Int32 lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(Int32 lHandle);

        // �����ȡ���ҵ��ĳ���������Ϣ��
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_GetNextRemoteConfig(Int32 lHandle, IntPtr lpOutBuff, uint dwOutBuffSize);


        // ���ܽ�ͨ��������
        public enum VCA_OPERATE_TYPE
        {
            VCA_LICENSE_TYPE = 0x1,  //���ƺ���
            VCA_PLATECOLOR_TYPE = 0x2,  //������ɫ
            VCA_CARDNO_TYPE = 0x4,  //����
            VCA_PLATETYPE_TYPE = 0x8,  //��������
            VCA_LISTTYPE_TYPE = 0x10,  //������������
            VCA_INDEX_TYPE = 0x20,  //������ˮ�� 2014-02-25
            VCA_OPERATE_INDEX_TYPE = 0x40  //������ 2014-03-03
        }
        // NET_DVR_StartRemoteConfig CallBack ��������
        public enum NET_SDK_CALLBACK_TYPE
        {
            NET_SDK_CALLBACK_TYPE_STATUS = 0,
            NET_SDK_CALLBACK_TYPE_PROGRESS,
            NET_SDK_CALLBACK_TYPE_DATA
        }

        // NET_DVR_StartRemoteConfig CallBack�����豸���ص�״ֵ̬
        public enum NET_SDK_CALLBACK_STATUS_NORMAL
        {
            NET_SDK_CALLBACK_STATUS_SUCCESS = 1000,  // �ɹ�
            NET_SDK_CALLBACK_STATUS_PROCESSING,      // ������
            NET_SDK_CALLBACK_STATUS_FAILED,    // ʧ��
            NET_SDK_CALLBACK_STATUS_EXCEPTION,      // �쳣
            NET_SDK_CALLBACK_STATUS_LANGUAGE_MISMATCH,    //��IPC�����ļ����룩���Բ�ƥ��
            NET_SDK_CALLBACK_STATUS_DEV_TYPE_MISMATCH,    //��IPC�����ļ����룩�豸���Ͳ�ƥ��
            NET_DVR_CALLBACK_STATUS_SEND_WAIT           // ���͵ȴ�
        }

        // ����ȫ����ȡ�ӿ� �������ӻ�ȡ��
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

        // NET_DVR_GetNextRemoteConfig �豸����״̬
        public enum NET_SDK_GET_NEXT_STATUS
        {
            NET_SDK_GET_NEXT_STATUS_SUCCESS = 1000,    // �ɹ���ȡ�����ݣ��ͻ��˴����걾�����ݺ���Ҫ�ٴε���NET_DVR_RemoteConfigGetNext��ȡ��һ������
            NET_SDK_GET_NETX_STATUS_NEED_WAIT,  // ��ȴ��豸�������ݣ���������NET_DVR_RemoteConfigGetNext����
            NET_SDK_GET_NEXT_STATUS_FINISH,      // ����ȫ��ȡ�꣬��ʱ�ͻ��˿ɵ���NET_DVR_StopRemoteConfig����������
            NET_SDK_GET_NEXT_STATUS_FAILED,      // �����쳣���ͻ��˿ɵ���NET_DVR_StopRemoteConfig����������
        }


        public struct tagNET_DVR_VEHICLE_CONTROL_LIST_INFO
        {
            public uint dwSize;
            public uint dwChannel;//ͨ����0xff - ȫ��ͨ����ITC Ĭ����1��
            public uint dwDataIndex;//������ˮ�ţ�ƽ̨ά��������Ψһֵ���ͻ��˲�����ʱ�򣬸�ֵ������Ч����ֵ��Ҫ������������ͬ����
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //���ƺ���
            public byte byListType;//��������
            public byte byPlateType;    //��������
            public byte byPlateColor;    //������ɫ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 21, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // ����
            public NET_DVR_TIME_V30 struStartTime;//��Ч��ʼʱ��
            public NET_DVR_TIME_V30 struStopTime;//��Ч����ʱ��
            //��������ƽ̨ͬ������ˮ�Ų����ظ��������������£�����ͬ����ͬ������ĳһ����¼�ˣ���������ڴ棬���������0��2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 224, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; // �����ֽ�
        }

        // ����ȫ����ȡ�ӿ� �������ӻ�ȡ��
        public struct tagNET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;//ͨ����0xffffffff - ȫ��ͨ����ITC Ĭ����1��
            public uint dwOperateType;//�������ͣ�����VCA_OPERATE _TYPE�����ɸ�ѡ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; //���ƺ���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; // ����
            public byte byListType;//��������
            //2014-02-25
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;//������ˮ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        public struct NET_DVR_VEHICLE_CONTROL_DELINFO
        {
            public uint dwSize;
            public uint dwDelType;//ɾ���������ͣ�ɾ���������ͣ�����VCA_OPERATE _TYPE�����ɸ�ѡ��
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public String sLicense; //���ƺ���
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public String sCardNo; // ���� 
            public byte byPlateType;    //��������
            public byte byPlateColor;    //������ɫ
            public byte byOperateType;    //ɾ����������(0-����ɾ��,0xff-ɾ��ȫ��)
            //2014-02-25
            public byte byListType;//��������
            public uint dwDataIndex;//������ˮ��     
            //��������ƽ̨ͬ������ˮ�Ų����ظ��������������£�����ͬ����ͬ������ĳһ����¼�ˣ���������ڴ棬���������0��2014-03-03
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�������� ��������
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLocalIP(byte[] strIP, ref Int32 pValidNum, ref bool pEnableBind);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetValidIP(uint dwIPIndex, bool bEnableBind);

        // �Ų������ýṹ��
        public const int DOOR_NAME_LEN = 32;
        public const int STRESS_PASSWORD_LEN = 8;
        public const int SUPER_PASSWORD_LEN = 8;
        public const int UNLOCK_PASSWORD_LEN = 8;
        public const int NET_DVR_GET_DOOR_CFG = 2108; // ��ȡ�Ų���
        public const int NET_DVR_SET_DOOR_CFG = 2109; // �����Ų���
        public const int COMM_ALARM_ACS = 0x5002; // �Ž���������
        public const int ACS_CARD_NO_LEN = 32; // �Ž����ų���
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

        // �Ž�����������Ϣ�ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_ALARM_INFO
        {
            public uint dwSize;
            public uint dwMajor; //���������ͣ��ο��궨��
            public uint dwMinor; //���������ͣ��ο��궨��
            public NET_DVR_TIME struTime; //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//����������û���
            public NET_DVR_IPADDR struRemoteHostAddr;//Զ��������ַ
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo; //��ϸ����
            public uint dwPicDataLen;   //ͼƬ���ݴ�С����Ϊ0�Ǳ�ʾ���������
            public IntPtr pPicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // �Ž������¼���Ϣ
        public struct NET_DVR_ACS_EVENT_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //���ţ�Ϊ0��Ч
            public byte byCardType; 
            public byte byAllowListNo; 
            public byte byReportChannel; //�����ϴ�ͨ����1-�����ϴ���2-������1�ϴ���3-������2�ϴ���Ϊ0��Ч
            public byte byCardReaderKind; //������������һ�࣬0-��Ч��1-IT��������2-����֤��������3-��ά�������
            public uint dwCardReaderNo; //��������ţ�Ϊ0��Ч
            public uint dwDoorNo; //�ű�ţ�Ϊ0��Ч
            public uint dwVerifyNo; //���ؿ���֤��ţ�Ϊ0��Ч
            public uint dwAlarmInNo; //��������ţ�Ϊ0��Ч
            public uint dwAlarmOutNo; //��������ţ�Ϊ0��Ч
            public uint dwCaseSensorNo; //�¼����������
            public uint dwRs485No; //RS485ͨ���ţ�Ϊ0��Ч
            public uint dwMultiCardGroupNo; //Ⱥ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //������������
        public const int ZERO_CHAN_INDEX = 500;
        public const int MIRROR_CHAN_INDEX = 400;
        public const int STREAM_ID_LEN = 32;
        public const int MAX_CHANNUM_V40 = 512;

        //compression parameter
        public const int NORM_HIGH_STREAM_COMPRESSION = 0; //������ͼ��ѹ������,ѹ������ǿ,���ܿ��Ը���
        public const int SUB_STREAM_COMPRESSION = 1; //������ͼ��ѹ������
        public const int EVENT_INVOKE_COMPRESSION = 2; //�¼�����ͼ��ѹ������,һЩ������Թ̶�
        public const int THIRD_STREAM_COMPRESSION = 3;  //��������
        public const int TRANS_STREAM_COMPRESSION = 4;  //ת������

        public const int NET_DVR_GET_AUDIO_INPUT = 3201; //��ȡ��Ƶ�������
        public const int NET_DVR_SET_AUDIO_INPUT = 3202; //������Ƶ�������
        public const int NET_DVR_GET_MULTI_STREAM_COMPRESSIONCFG = 3216; //Զ�̻�ȡ������ѹ������
        public const int NET_DVR_SET_MULTI_STREAM_COMPRESSIONCFG = 3217; //Զ�����ö�����ѹ������

        public struct NET_DVR_VALID_PERIOD_CFG
        {
            public byte byEnable; //ʹ����Ч�ڣ�0-��ʹ�ܣ�1ʹ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struBeginTime; //��Ч����ʼʱ��
            public NET_DVR_TIME_EX struEndTime; //��Ч�ڽ���ʱ��
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
        // ����Ϣ - 72�ֽڳ�
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

        //������ѹ���������������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG_COND
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struStreamInfo;
            public uint dwStreamType; //�������ͣ�0-��������1-��������2-�¼����ͣ�3-����3������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //������ѹ�������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG
        {
            public uint dwSize;
            public uint dwStreamType; //�������ͣ�0-��������1-��������2-�¼����ͣ�3-����3������
            public NET_DVR_COMPRESSION_INFO_V30 struStreamPara; //����ѹ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUDIO_INPUT_PARAM
        {
            public byte byAudioInputType;  //��Ƶ�������ͣ�0-mic in��1-line in
            public byte byVolume; //volume,[0-100]
            public byte byEnableNoiseFilter; //�Ƿ�����������-�أ�-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ʱ���¼���������(�ӽṹ)
        public struct NET_DVR_RECORDSCHED_V40
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            /*¼�����ͣ�0:��ʱ¼��1:�ƶ���⣬2:����¼��3:����|������4:����&���� 5:�����, 
            6-���ܱ���¼��10-PIR������11-���߱�����12-���ȱ�����13-ȫ���¼�,14-���ܽ�ͨ�¼�, 
            15-Խ�����,16-��������,17-�����쳣,18-����������,
            19-�������(Խ�����|��������|�������|�����쳣|����������),20���������,21-POS¼��,
            22-�����������, 23-�뿪�������,24-�ǻ����,25-��Ա�ۼ����,26-�����˶����,27-ͣ�����,
            28-��Ʒ�������,29-��Ʒ��ȡ���,30-����⣬31-���ƻ����*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //ȫ��¼���������(�ӽṹ)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY_V40
        {
            public byte byAllDayRecord;/* �Ƿ�ȫ��¼�� 0-�� 1-��*/
            /*¼�����ͣ�0:��ʱ¼��1:�ƶ���⣬2:����¼��3:����|������4:����&���� 5:�����, 
            6-���ܱ���¼��10-PIR������11-���߱�����12-���ȱ�����13-ȫ���¼�,14-���ܽ�ͨ�¼�, 
            15-Խ�����,16-��������,17-�����쳣,18-����������,
            19-�������(Խ�����|��������|�������|�����쳣|����������),20���������,21-POS¼��,
            22-�����������, 23-�뿪�������,24-�ǻ����,25-��Ա�ۼ����,26-�����˶����,27-ͣ�����,
            28-��Ʒ�������,29-��Ʒ��ȡ���,30-����⣬31-���ƻ����*/
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V40
        {
            public uint dwSize;
            public uint dwRecord; /*�Ƿ�¼�� 0-�� 1-��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY_V40[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED_V40[] struRecordSched;
            public uint dwRecordTime; /* ¼����ʱ���� 0-5�룬 1-10�룬 2-30�룬 3-1���ӣ� 4-2���ӣ� 5-5���ӣ� 6-10����*/
            public uint dwPreRecordTime; /* Ԥ¼ʱ�� 0-��Ԥ¼ 1-5�� 2-10�� 3-15�� 4-20�� 5-25�� 6-30�� 7-0xffffffff(������Ԥ¼) */
            public uint dwRecorderDuration; /* ¼�񱣴���ʱ�� */
            public byte byRedundancyRec; /*�Ƿ�����¼��,��Ҫ����˫���ݣ�0/1*/
            public byte byAudioRec; /*¼��ʱ����������ʱ�Ƿ��¼��Ƶ���ݣ������д˷���*/
            public byte byStreamType;  // 0-��������1-��������2-��������ͬʱ 3-������
            public byte byPassbackRecord;  // 0:���ش�¼�� 1���ش�¼��
            public ushort wLockDuration;  // ¼������ʱ������λСʱ 0��ʾ��������0xffff��ʾ����������¼��ε�ʱ�����������ĳ���ʱ����¼�񣬽���������
            public byte byRecordBackup;  // 0:¼�񲻴浵 1��¼��浵
            public byte bySVCLevel;    //SVC��֡���ͣ�0-���飬1-�����֮һ 2-���ķ�֮��
            public byte byRecordManage;   //¼����ȣ�0-���ã� 1-������; ����ʱ���ж�ʱ¼�񣻲�����ʱ�����ж�ʱ¼�񣬵���¼��ƻ�����ʹ�ã������ƶ���⣬�ش������ڰ�����¼��ƻ�����
            public byte byExtraSaveAudio;//��Ƶ�����洢
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_CARD_CFG = 2116;    //��ȡ������
        public const int NET_DVR_SET_CARD_CFG = 2117;    //���ÿ�����

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG
        {
            public uint dwSize;
            public uint dwModifyParamType;
            // ��Ҫ�޸ĵĿ����������ÿ�����ʱ��Ч����λ��ʾ��ÿλ����һ�ֲ�����1Ϊ��Ҫ�޸ģ�0Ϊ���޸�
            // #define CARD_PARAM_CARD_VALID       0x00000001 //���Ƿ���Ч����
            // #define CARD_PARAM_VALID            0x00000002  //��Ч�ڲ���
            // #define CARD_PARAM_CARD_TYPE        0x00000004  //�����Ͳ���
            // #define CARD_PARAM_DOOR_RIGHT       0x00000008  //��Ȩ�޲���
            // #define CARD_PARAM_LEADER_CARD      0x00000010  //�׿�����
            // #define CARD_PARAM_SWIPE_NUM        0x00000020  //���ˢ����������
            // #define CARD_PARAM_GROUP            0x00000040  //����Ⱥ�����
            // #define CARD_PARAM_PASSWORD         0x00000080  //���������
            // #define CARD_PARAM_RIGHT_PLAN       0x00000100  //��Ȩ�޼ƻ�����
            // #define CARD_PARAM_SWIPED_NUM       0x00000200  //��ˢ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //����
            public byte byCardValid; //���Ƿ���Ч��0-��Ч��1-��Ч������ɾ����������ʱ��Ϊ0����ɾ������ȡʱ���ֶ�ʼ��Ϊ1��
            public byte byCardType; 
            public byte byLeaderCard; //�Ƿ�Ϊ�׿���1-�ǣ�0-��
            public byte byRes1;
            public uint dwDoorRight; //��Ȩ�ޣ���λ��ʾ��1Ϊ��Ȩ�ޣ�0Ϊ��Ȩ�ޣ��ӵ�λ����λ��ʾ����1-N�Ƿ���Ȩ��
            public NET_DVR_VALID_PERIOD_CFG struValid; //��Ч�ڲ���
            public uint dwBelongGroup; //����Ⱥ�飬��λ��ʾ��1-���ڣ�0-�����ڣ��ӵ�λ����λ��ʾ�Ƿ����Ⱥ��1-N
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARD_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardPassword; //������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM * MAX_CARD_RIGHT_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardRightPlan; //��Ȩ�޼ƻ���ȡֵΪ�ƻ�ģ���ţ�ͬ���Ų�ͬ�ƻ�ģ�����Ȩ�޻�ķ�ʽ����
            public uint dwMaxSwipeTime; //���ˢ��������0Ϊ�޴�������
            public uint dwSwipeTime; //��ˢ������
            public ushort wRoomNumber; //����� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_COND
        {
            public uint dwSize;
            public uint dwCardNum; //���û��ȡ����������ȡʱ��Ϊ0xffffffff��ʾ��ȡ���п���Ϣ
            public byte byCheckCardNo; //�豸�Ƿ���п���У�飬0-��У�飬1-У��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_SEND_DATA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        public const int NET_DVR_GET_GROUP_CFG = 2112;   //��ȡȺ�����
        public const int NET_DVR_SET_GROUP_CFG = 2113;    //����Ⱥ�����

        //�����ṹ�壨Ⱥ�飩
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

        public const int NET_DVR_GET_FINGERPRINT_CFG = 2150;    //��ȡָ�Ʋ���
        public const int NET_DVR_SET_FINGERPRINT_CFG = 2151;    //����ָ�Ʋ���
        public const int NET_DVR_DEL_FINGERPRINT_CFG = 2152;    //ɾ��ָ�Ʋ���
        public const int MAX_FINGER_PRINT_LEN = 768;            //���ָ�Ƴ���

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;  //ָ�ƹ�������
            public uint dwFingerPrintLen; //ָ�����ݳ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCardReader; //��Ҫ�·�ָ�ƵĶ��������������ʾ��0-���·��ö�������1-�·����ö�����
            public byte byFingerPrintID;     //ָ�Ʊ�ţ���Чֵ��ΧΪ1-10
            public byte byFingerType;       //ָ������  0-��ָͨ�ƣ�1-в��ָ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FINGER_PRINT_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byFingerData;        //ָ����������
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
            public byte[] byCardNo; //ָ�ƹ����Ŀ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byCardReaderRecvStatus;   //ָ�ƶ�����״̬���������ʾ
            public byte byFingerPrintID;  //ָ�Ʊ�ţ���Чֵ��ΧΪ1-10
            public byte byFingerType;   //ָ������  0-��ָͨ�ƣ�1-в��ָ��
            public byte byTotalStatus;  //�·��ܵ�״̬��0-��ǰָ��δ�������ж�������1-���������ж�����(���������ָ�����Ž����������еĶ������·��ˣ����ܳɹ����)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 61)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_COND
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //ָ�ƹ����Ŀ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //ָ�ƵĶ�������Ϣ���������ʾ
            public uint dwFingerPrintNum; //���û��ȡ����������ȡʱ��Ϊ0xffffffff��ʾ��ȡ���п���Ϣ
            public byte byFingerPrintID;  //ָ�Ʊ�ţ���Чֵ��ΧΪ-10   0xff��ʾ�ÿ�����ָ��
            public byte byCallbackMode;   //�豸�ص���ʽ��0-�豸���ж����������˷�Χ��1-��ʱ��������˲���Ҳ����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYCARD
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //ָ�ƹ����Ŀ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; //ָ�ƵĶ�������Ϣ���������ʾ
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] byFingerPrintID;    //��Ҫ��ȡ��ָ����Ϣ���������±ֵ꣬��ʾ0-��ɾ����1-ɾ����ָ��
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
            public uint dwCardReaderNo;  //��ֵ��ʾ��ָ�ƶ��������
            public byte byClearAllCard;  //�Ƿ�ɾ�����п���ָ����Ϣ��0-������ɾ��ָ����Ϣ��1-ɾ�����п���ָ����Ϣ
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; //ָ�ƹ����Ŀ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 548)]
            public byte[] byRes;

            //public void init()
            //{
            //    byRes1 = new byte[3];
            //    byCardNo = new byte[32];
            //    byRes = new byte[548];
            //}
        }

        //public const int DEL_FINGER_PRINT_MODE_LEN = 588; //�������С
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_DEL_FINGER_PRINT_MODE
        //{
        //    public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //�����ŵķ�ʽɾ��
        //    public NET_DVR_FINGER_PRINT_BYREADER struByReader; //���������ķ�ʽɾ��

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
            public byte byMode;          //ɾ����ʽ��0-�����ŷ�ʽɾ����1-��������ɾ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYCARD struByCard;   //�����ŵķ�ʽɾ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYREADER
        {
            public uint dwSize;
            public byte byMode;          //ɾ����ʽ��0-�����ŷ�ʽɾ����1-��������ɾ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYREADER struByReader; //���������ķ�ʽɾ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        public const int NET_DVR_GET_CARD_READER_CFG = 2140;      //��ȡ����������
        public const int NET_DVR_SET_CARD_READER_CFG = 2141;      //���ö���������

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_CFG
        {
            public uint dwSize;
            public byte byEnable; //�Ƿ�ʹ�ܣ�1-ʹ�ܣ�0-��ʹ��
            public byte byCardReaderType; //���������ͣ�1-DS-K110XM/MK/C/CK��2-DS-K192AM/AMP��3-DS-K192BM/BMP��4-DS-K182AM/AMP��5-DS-K182BM/BMP��6-DS-K182AMF/ACF��7-Τ����485������,8- DS-K1101M/MK��9- DS-K1101C/CK��10- DS-K1102M/MK/M-A
            //11- DS-K1102C/CK��12- DS-K1103M/MK��13- DS-K1103C/CK��14- DS-K1104M/MK��15- DS-K1104C/CK��16- DS-K1102S/SK/S-A��17- DS-K1102G/GK��18- DS-K1100S-B��19- DS-K1102EM/EMK��20- DS-K1102E/EK��
            //21- DS-K1200EF��22- DS-K1200MF��23- DS-K1200CF��24- DS-K1300EF��25- DS-K1300MF��26- DS-K1300CF��27- DS-K1105E��28- DS-K1105M��29- DS-K1105C��30- DS-K182AMF��31- DS-K196AMF��32-DS-K194AMP
            //33-DS-K1T200EF/EF-C/MF/MF-C/CF/CF-C,34-DS-K1T300EF/EF-C/MF/MF-C/CF/CF-C��35-DS-K1T105E/E-C/M/M-C/C/C-C
            public byte byOkLedPolarity; //OK LED���ԣ�0-������1-����
            public byte byErrorLedPolarity; //Error LED���ԣ�0-������1-����
            public byte byBuzzerPolarity; //���������ԣ�0-������1-����
            public byte bySwipeInterval; //�ظ�ˢ�����ʱ�䣬��λ����
            public byte byPressTimeout;  //������ʱʱ�䣬��λ����
            public byte byEnableFailAlarm; //�Ƿ����ö���ʧ�ܳ��α�����0-�����ã�1-����
            public byte byMaxReadCardFailNum; //������ʧ�ܴ���
            public byte byEnableTamperCheck;  //�Ƿ�֧�ַ����⣬0-disable ��1-enable
            public byte byOfflineCheckTime;  //���߼��ʱ�� ��λ��
            public byte byFingerPrintCheckLevel;   //ָ��ʶ��ȼ���1-1/10�����ʣ�2-1/100�����ʣ�3-1/1000�����ʣ�4-1/10000�����ʣ�5-1/100000�����ʣ�6-1/1000000�����ʣ�7-1/10000000�����ʣ�8-1/100000000�����ʣ�9-3/100�����ʣ�10-3/1000�����ʣ�11-3/10000�����ʣ�12-3/100000�����ʣ�13-3/1000000�����ʣ�14-3/10000000�����ʣ�15-3/100000000�����ʣ�16-Automatic Normal,17-Automatic Secure,18-Automatic More Secure
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_WEEK_PLAN_CFG = 2100; //��ȡ��״̬�ܼƻ�����
        public const int NET_DVR_SET_WEEK_PLAN_CFG = 2101; //������״̬�ܼƻ�����
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_PLAN = 2102; //��ȡ��״̬���ռƻ�����
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_PLAN = 2103; //������״̬���ռƻ�����
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_GROUP = 2104; //��ȡ��״̬���������
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_GROUP = 2105; //������״̬���������
        public const int NET_DVR_GET_DOOR_STATUS_PLAN_TEMPLATE = 2106; //��ȡ��״̬�ƻ�ģ�����
        public const int NET_DVR_SET_DOOR_STATUS_PLAN_TEMPLATE = 2107; //������״̬�ƻ�ģ�����
        public const int NET_DVR_GET_DOOR_STATUS_PLAN = 2110; //��ȡ��״̬�ƻ�����
        public const int NET_DVR_SET_DOOR_STATUS_PLAN = 2111; //������״̬�ƻ�����
        public const int NET_DVR_CLEAR_ACS_PARAM = 2118; //����Ž���������
        public const int NET_DVR_GET_VERIFY_WEEK_PLAN = 2124; //��ȡ��������֤��ʽ�ܼƻ�����
        public const int NET_DVR_SET_VERIFY_WEEK_PLAN = 2125; //���ö�������֤��ʽ�ܼƻ�����
        public const int NET_DVR_GET_CARD_RIGHT_WEEK_PLAN = 2126; //��ȡ��Ȩ���ܼƻ�����
        public const int NET_DVR_SET_CARD_RIGHT_WEEK_PLAN = 2127; //���ÿ�Ȩ���ܼƻ�����
        public const int NET_DVR_GET_VERIFY_HOLIDAY_PLAN = 2128; //��ȡ��������֤��ʽ���ռƻ�����
        public const int NET_DVR_SET_VERIFY_HOLIDAY_PLAN = 2129; //���ö�������֤��ʽ���ռƻ�����
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN = 2130; //��ȡ��Ȩ�޼��ռƻ�����
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN = 2131; //���ÿ�Ȩ�޼��ռƻ�����
        public const int NET_DVR_GET_VERIFY_HOLIDAY_GROUP = 2132; //��ȡ��������֤��ʽ���������
        public const int NET_DVR_SET_VERIFY_HOLIDAY_GROUP = 2133; //���ö�������֤��ʽ���������
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_GROUP = 2134; //��ȡ��Ȩ�޼��������
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_GROUP = 2135; //���ÿ�Ȩ�޼��������
        public const int NET_DVR_GET_VERIFY_PLAN_TEMPLATE = 2136; //��ȡ��������֤��ʽ�ƻ�ģ�����
        public const int NET_DVR_SET_VERIFY_PLAN_TEMPLATE = 2137; //���ö�������֤��ʽ�ƻ�ģ�����
        public const int NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE = 2138; //��ȡ��Ȩ�޼ƻ�ģ�����
        public const int NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE = 2139; //���ÿ�Ȩ�޼ƻ�ģ�����
        public const int NET_DVR_GET_CARD_READER_PLAN = 2142; //��ȡ��������֤�ƻ�����
        public const int NET_DVR_SET_CARD_READER_PLAN = 2143; //���ö�������֤�ƻ�����
        public const int NET_DVR_GET_CARD_USERINFO_CFG = 2163; //��ȡ���Ź����û���Ϣ����
        public const int NET_DVR_SET_CARD_USERINFO_CFG = 2164; //���ÿ��Ź����û���Ϣ����

        public const int NET_DVR_CONTROL_PTZ_MANUALTRACE = 3316; //�ֶ����ٶ�λ
        public const int NET_DVR_GET_SMARTTRACKCFG = 3293; //��ȡ�����˶�����������Ϣ    
        public const int NET_DVR_SET_SMARTTRACKCFG = 3294; //���������˶�����������Ϣ

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DATE
        {
            public ushort wYear; //��
            public byte byMonth; //��
            public byte byDay; //��
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SIMPLE_DAYTIME
        {
            public byte byHour; //ʱ
            public byte byMinute; //��
            public byte bySecond; //��
            public byte byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_SEGMENT
        {
            public NET_DVR_SIMPLE_DAYTIME struBeginTime; //��ʼʱ���
            public NET_DVR_SIMPLE_DAYTIME struEndTime;   //����ʱ���
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_PLAN_SEGMENT
        {
            public byte byEnable; //�Ƿ�ʹ�ܣ�1-ʹ�ܣ�0-��ʹ��
            public byte byDoorStatus; //��״̬ģʽ��0-��Ч��1-����״̬��2-����״̬��3-��ͨ״̬����״̬�ƻ�ʹ�ã�
            public byte byVerifyMode; //��֤��ʽ��0-��Ч��1-ˢ����2-ˢ��+����(��������֤��ʽ�ƻ�ʹ��)��3-ˢ��,4-ˢ��������(��������֤��ʽ�ƻ�ʹ��), 5-ָ�ƣ�6-ָ��+���룬7-ָ�ƻ�ˢ����8-ָ��+ˢ����9-ָ��+ˢ��+���루���Ⱥ�˳��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
            public NET_DVR_TIME_SEGMENT struTimeSegment; //ʱ��β���
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WEEK_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //�Ƿ�ʹ�ܣ�1-ʹ�ܣ�0-��ʹ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //�ܼƻ�����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;  //�Ƿ�ʹ�ܣ�1-ʹ�ܣ�0-��ʹ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public CHCNetSDK.NET_DVR_DATE struBeginDate; //���տ�ʼ����
            public CHCNetSDK.NET_DVR_DATE struEndDate; //���ս�������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public HCNetSDK.NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg; //ʱ��β���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        public const int TEMPLATE_NAME_LEN = 32; //�ƻ�ģ�����Ƴ���
        public const int MAX_HOLIDAY_GROUP_NUM = 16; //�ƻ�ģ������������
        public const int HOLIDAY_GROUP_NAME_LEN = 32; //���������Ƴ���
        public const int MAX_HOLIDAY_PLAN_NUM = 16; //�����������ռƻ���

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;  //�Ƿ�ʹ�ܣ�1-ʹ�ܣ�0-��ʹ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.HOLIDAY_GROUP_NAME_LEN)]
            public byte[] byGroupName; //����������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayPlanNo; //�������ţ���ǰ��䣬��0��Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PLAN_TEMPLATE
        {
            public uint dwSize;
            public byte byEnable; //�Ƿ����ã�1-���ã�0-������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.TEMPLATE_NAME_LEN)]
            public byte[] byTemplateName; //ģ������
            public uint dwWeekPlanNo; //�ܼƻ���ţ�0Ϊ��Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayGroupNo; //�������ţ���ǰ��䣬��0��Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DOOR_STATUS_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //�ƻ�ģ���ţ�Ϊ0��ʾȡ���������ָ�Ĭ��״̬����ͨ״̬��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; //�ƻ�ģ���ţ�Ϊ0��ʾȡ���������ָ�Ĭ��״̬��ˢ�����ţ�
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        public const int ACS_PARAM_DOOR_STATUS_WEEK_PLAN = 0x00000001;      //��״̬�ܼƻ�����
        public const int ACS_PARAM_VERIFY_WEEK_PALN = 0x00000002;           //�������ܼƻ�����
        public const int ACS_PARAM_CARD_RIGHT_WEEK_PLAN = 0x00000004;       //��Ȩ���ܼƻ�����
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN = 0x00000008;   //��״̬���ռƻ�����
        public const int ACS_PARAM_VERIFY_HOLIDAY_PALN = 0x00000010;        //���������ռƻ�����
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN = 0x00000020;    //��Ȩ�޼��ռƻ�����
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP = 0x00000040;  //��״̬���������
        public const int ACS_PARAM_VERIFY_HOLIDAY_GROUP = 0x00000080;       //��������֤��ʽ���������
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP = 0x00000100;   //��Ȩ�޼��������
        public const int ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE = 0x00000200;  //��״̬�ƻ�ģ�����
        public const int ACS_PARAM_VERIFY_PALN_TEMPLATE = 0x00000400;       //��������֤��ʽ�ƻ�ģ�����
        public const int ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE = 0x00000800;   //��Ȩ�޼ƻ�ģ�����
        public const int ACS_PARAM_CARD = 0x00001000;                       //������
        public const int ACS_PARAM_GROUP = 0x00002000;                      //Ⱥ�����
        public const int ACS_PARAM_ANTI_SNEAK_CFG = 0x00004000;             //��Ǳ�ز���
        public const int ACS_PAPAM_EVENT_CARD_LINKAGE = 0x00008000;         //�¼���������������
        public const int ACS_PAPAM_CARD_PASSWD_CFG = 0x00010000;            //���뿪��ʹ�ܲ���

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_PARAM_TYPE
        {
            public uint dwSize;
            public uint dwParamType;
            //�������ͣ���λ��ʾ
            //#define ACS_PARAM_DOOR_STATUS_WEEK_PLAN        0x00000001 //��״̬�ܼƻ�����
            //#define ACS_PARAM_VERIFY_WEEK_PALN             0x00000002 //�������ܼƻ�����
            //#define ACS_PARAM_CARD_RIGHT_WEEK_PLAN         0x00000004 //��Ȩ���ܼƻ�����
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN     0x00000008 //��״̬���ռƻ�����
            //#define ACS_PARAM_VERIFY_HOLIDAY_PALN          0x00000010 //���������ռƻ�����
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN      0x00000020 //��Ȩ�޼��ռƻ�����
            //#define ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP    0x00000040 //��״̬���������
            //#define ACS_PARAM_VERIFY_HOLIDAY_GROUP         0x00000080 //��������֤��ʽ���������
            //#define ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP     0x00000100 //��Ȩ�޼��������
            //#define ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE    0x00000200 //��״̬�ƻ�ģ�����
            //#define ACS_PARAM_VERIFY_PALN_TEMPLATE         0x00000400 //��������֤��ʽ�ƻ�ģ�����
            //#define ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE     0x00000800 //��Ȩ�޼ƻ�ģ�����
            //#define ACS_PARAM_CARD                         0x00001000 //������
            //#define ACS_PARAM_GROUP                        0x00002000 //Ⱥ�����
            //#define ACS_PARAM_ANTI_SNEAK_CFG           0x00004000 //��Ǳ�ز���
            //#define ACS_PAPAM_EVENT_CARD_LINKAGE           0x00008000 //�¼���������������
            //#define ACS_PAPAM_CARD_PASSWD_CFG              0x00010000 //���뿪��ʹ�ܲ���
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


        //��Ƶ��������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOEFFECT
        {
            public byte byBrightnessLevel; /*0-100*/
            public byte byContrastLevel; /*0-100*/
            public byte bySharpnessLevel; /*0-100*/
            public byte bySaturationLevel; /*0-100*/
            public byte byHueLevel; /*0-100,��������*/
            public byte byEnableFunc; //ʹ�ܣ���λ��ʾ��bit0-SMART IR(������)��bit1-���ն�,bit2-ǿ������ʹ�ܣ�0-��1-��
            public byte byLightInhibitLevel; //ǿ�����Ƶȼ���[1-3]��ʾ�ȼ�
            public byte byGrayLevel; //�Ҷ�ֵ��0-[0-255]��1-[16-235]
        }

        //��������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAIN
        {
            public byte byGainLevel; /*���棺0-100*/
            public byte byGainUserSet; /*�û��Զ������棻0-100������ץ�Ļ�����CCDģʽ�µ�ץ������*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwMaxGainValue;/*�������ֵ����λdB*/
        }

        //��ƽ������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WHITEBALANCE
        {
            public byte byWhiteBalanceMode; /*0-�ֶ���ƽ�⣨MWB��,1-�Զ���ƽ��1��AWB1��,2-�Զ���ƽ��2 (AWB2),3-�Զ����Ƹ���Ϊ������ƽ��(Locked WB)��
                                            4-����(Indoor)��5-����(Outdoor)6-�չ��(Fluorescent Lamp)��7-�Ƶ�(Sodium Lamp)��
                                            8-�Զ�����(Auto-Track)9-һ�ΰ�ƽ��(One Push)��10-�����Զ�(Auto-Outdoor)��
                                            11-�Ƶ��Զ� (Auto-Sodiumlight)��12-ˮ����(Mercury Lamp)��13-�Զ���ƽ��(Auto)��
                                            14-�׳�� (IncandescentLamp)��15-ů���(Warm Light Lamp)��16-��Ȼ��(Natural Light) */
            public byte byWhiteBalanceModeRGain; /*�ֶ���ƽ��ʱ��Ч���ֶ���ƽ�� R����*/
            public byte byWhiteBalanceModeBGain; /*�ֶ���ƽ��ʱ��Ч���ֶ���ƽ�� B����*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        //�ع����
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_EXPOSURE
        {
            public byte byExposureMode; /*0 �ֶ��ع� 1�Զ��ع�*/
            public byte byAutoApertureLevel; /* �Զ���Ȧ������, 0-10 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwVideoExposureSet; /* �Զ�����Ƶ�ع�ʱ�䣨��λus��*//*ע:�Զ��ع�ʱ��ֵΪ�ع�����ֵ ����20-1s(1000000us)*/
            public uint dwExposureUserSet; /* �Զ����ع�ʱ��,��ץ�Ļ���Ӧ��ʱ��CCDģʽʱ��ץ�Ŀ����ٶ�*/
            public uint dwRes;
        }

        //GammaУ��
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAMMACORRECT
        {
            public byte byGammaCorrectionEnabled; /*0 dsibale  1 enable*/
            public byte byGammaCorrectionLevel; /*0-100*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //����̬����
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WDR
        {
            public byte byWDREnabled; /*����̬��0 dsibale  1 enable 2 auto*/
            public byte byWDRLevel1; /*0-F*/
            public byte byWDRLevel2; /*0-F*/
            public byte byWDRContrastLevel; /*0-100*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes;
        }

        //��ҹת����������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DAYNIGHT
        {
            public byte byDayNightFilterType; /*��ҹ�л���0-���죬1-ҹ����2-�Զ���3-��ʱ��4-�������봥��*/
            public byte bySwitchScheduleEnabled; /*0 dsibale  1 enable,(����)*/
            //��ʱģʽ����
            public byte byBeginTime; /*��ʼʱ�䣨Сʱ����0-23*/
            public byte byEndTime; /*����ʱ�䣨Сʱ����0-23*/
            //ģʽ2
            public byte byDayToNightFilterLevel; //0-7
            public byte byNightToDayFilterLevel; //0-7
            public byte byDayNightFilterTime;//(60��)
            //��ʱģʽ����
            public byte byBeginTimeMin; //��ʼʱ�䣨�֣���0-59
            public byte byBeginTimeSec; //��ʼʱ�䣨�룩��0-59
            public byte byEndTimeMin; //����ʱ�䣨�֣���0-59
            public byte byEndTimeSec; //����ʱ�䣨�룩��0-59
            //�������봥��ģʽ����
            public byte byAlarmTrigState; //�������봥��״̬��0-���죬1-ҹ��
        }

        //���ⲹ������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_BACKLIGHT
        {
            public byte byBacklightMode; /*���ⲹ��:0 off 1 UP��2 DOWN��3 LEFT��4 RIGHT��5MIDDLE��6�Զ��壬10-����11-�Զ���12-�����򱳹ⲹ��*/
            public byte byBacklightLevel; /*0x0-0xF*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes1;
            public uint dwPositionX1; //��X����1��
            public uint dwPositionY1; //��Y����1��
            public uint dwPositionX2; //��X����2��
            public uint dwPositionY2; //��Y����2��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        //���ֽ��빦��
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_NOISEREMOVE
        {
            public byte byDigitalNoiseRemoveEnable; /*0-�����ã�1-��ͨģʽ���ֽ��룬2-ר��ģʽ���ֽ���*/
            public byte byDigitalNoiseRemoveLevel; /*��ͨģʽ���ֽ��뼶��0x0-0xF*/
            public byte bySpectralLevel;       /*ר��ģʽ�¿���ǿ�ȣ�0-100*/
            public byte byTemporalLevel;   /*ר��ģʽ��ʱ��ǿ�ȣ�0-100*/
            public byte byDigitalNoiseRemove2DEnable;         /* ץ��֡2D���룬0-�����ã�1-���� */
            public byte byDigitalNoiseRemove2DLevel;            /* ץ��֡2D���뼶��0-100 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
        }

        //CMOSģʽ��ǰ�˾�ͷ����
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CMOSMODECFG
        {
            public byte byCaptureMod;   //ץ��ģʽ��0-ץ��ģʽ1��1-ץ��ģʽ2
            public byte byBrightnessGate;//������ֵ
            public byte byCaptureGain1;   //ץ������1,0-100
            public byte byCaptureGain2;   //ץ������2,0-100
            public uint dwCaptureShutterSpeed1;//ץ�Ŀ����ٶ�1
            public uint dwCaptureShutterSpeed2;//ץ�Ŀ����ٶ�2
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes;
        }

        //͸��
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEFOGCFG
        {
            public byte byMode; //ģʽ��0-�����ã�1-�Զ�ģʽ��2-����ģʽ
            public byte byLevel; //�ȼ���0-100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //���ӷ���
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ELECTRONICSTABILIZATION
        {
            public byte byEnable;//ʹ�� 0- �����ã�1- ����
            public byte byLevel; //�ȼ���0-100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //����ģʽ
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CORRIDOR_MODE_CCD
        {
            public byte byEnableCorridorMode; //�Ƿ���������ģʽ 0�������ã� 1������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] byRes;
        }

        // SMART IR(������)���ò���
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SMARTIR_PARAM
        {
            public byte byMode;//0���ֶ���1���Զ�
            public byte byIRDistance;//�������ȼ�(�ȼ�������������)level:1~100 Ĭ��:50���ֶ�ģʽ�����ӣ�
            public byte byShortIRDistance;// ����ƾ���ȼ�(1~100)
            public byte byInt32IRDistance;// Զ��ƾ���ȼ�(1~100)
        }

        //��byIrisMode ΪP-Iris1ʱ��Ч�����ú����Ȧ��С�ȼ�������ģʽ
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PIRIS_PARAM
        {
            public byte byMode;//0-�Զ���1-�ֶ�
            public byte byPIrisAperture;//�����Ȧ��С�ȼ�(�ȼ�,��Ȧ��С������)level:1~100 Ĭ��:50���ֶ�ģʽ�����ӣ�
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        //����������� 2014-02-25
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_LASER_PARAM_CFG
        {
            //Length = 16
            public byte byControlMode;  //����ģʽ      0-��Ч��1-�Զ���2-�ֶ� Ĭ���Զ�
            public byte bySensitivity;  //�����������  0-100 Ĭ��50
            public byte byTriggerMode;  //����ƴ���ģʽ    0-��Ч��1-��о������2-�������� Ĭ�ϻ�о����
            public byte byBrightness;  //����ģʽΪ�ֶ�ģʽ����Ч�����������  0-255 Ĭ��100
            public byte byAngle;      //����ƽǶ�  0-��Ч����Χ1-36  Ĭ��12����������䷶ΧΪһ��ԲȦ�����ڼ���Ƕ��ǵ������Բ�İ뾶�Ĵ�С
            public byte byLimitBrightness;  //����ģʽΪ�Զ�ģʽ����Ч��������������� 0~100 ��������2014-01-26
            public byte byEnabled;         //�ֶ����Ƽ����ʹ�� 0-�رգ�1-����
            public byte byIllumination;     //�����ǿ������0~100
            public byte byLightAngle;       //����Ƕ� 0~100
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FFC_PARAM
        {
            //1-Schedule Mode,2-Temperature Mode, 3-Off 
            public byte byMode;
            //��ʱ��:��������ʾ����λ���ӣ�ѡ����10,20,30,40,50,60,120,180,240��
            public byte byRes1;
            public ushort wCompensateTime; //��ʱģʽ����Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDE_PARAM   //��sensor�����
        {
            public byte byMode;//1-Off,2-Normal Mode,3-Expert Mode
            public byte byNormalLevel;//��ͨģʽ�ȼ���Χ[1,100]����ͨģʽ����Ч
            public byte byExpertLevel;//ר��ģʽ�ȼ���Χ[1,100]��ר��ģʽ����Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_AGC_PARAM
        {
            public byte bySceneType;//1-Normal Sence,2-Highlight Sence,3-Manual Sence
            public byte byLightLevel;//���ȵȼ�[1,100]���ֶ�ģʽ����Ч
            public byte byGainLevel; //����ȼ�[1,100]���ֶ�ģʽ����Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        //ץ�Ļ�CCD���� ��64�ֽ�
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SNAP_CAMERAPARAMCFG
        {
            public byte byWDRMode;   // ����̬ģʽ;0~�رգ�1~���ֿ���̬ 2~����̬
            public byte byWDRType;    // ����̬�л�ģʽ; 0~ǿ�����ã�1~��ʱ�����ã�2~����������
            public byte byWDRLevel;   // ����̬�ȼ���0~6������Ӧ1-7��Ĭ������2����3������
            public byte byRes1;
            public NET_DVR_TIME_EX struStartTime; //��ʼ����̬ʱ��
            public NET_DVR_TIME_EX struEndTime; //��������̬ʱ��
            public byte byDayNightBrightness; //��ҹת��������ֵ��0-100��Ĭ��50��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
            public byte[] byRes;
        }

        //��ѧ͸������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_OPTICAL_DEHAZE
        {
            public byte byEnable; //0~�����ù�ѧ͸����1~���ù�ѧ͸��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_ISP_CAMERAPARAMCFG = 3255; //��ȡISPǰ�˲�������
        public const int NET_DVR_SET_ISP_CAMERAPARAMCFG = 3256; //����ISPǰ�˲�������
        public const int NET_DVR_GET_CCDPARAMCFG_EX = 3368; //��ȡCCD��������
        public const int NET_DVR_SET_CCDPARAMCFG_EX = 3369; //����CCD��������

        //ǰ�˲�������
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CAMERAPARAMCFG_EX
        {
            public uint dwsize;
            public NET_DVR_VIDEOEFFECT struVideoEffect;/*���ȡ��Աȶȡ����Ͷȡ���ȡ�ɫ������*/
            public NET_DVR_GAIN struGain;/*�Զ�����*/
            public NET_DVR_WHITEBALANCE struWhiteBalance;/*��ƽ��*/
            public NET_DVR_EXPOSURE struExposure; /*�ع����*/
            public NET_DVR_GAMMACORRECT struGammaCorrect;/*GammaУ��*/
            public NET_DVR_WDR struWdr;/*����̬*/
            public NET_DVR_DAYNIGHT struDayNight;/*��ҹת��*/
            public NET_DVR_BACKLIGHT struBackLight;/*���ⲹ��*/
            public NET_DVR_NOISEREMOVE struNoiseRemove;/*���ֽ���*/
            public byte byPowerLineFrequencyMode; /*0-50HZ; 1-60HZ*/
            public byte byIrisMode; /*0-�Զ���Ȧ 1-�ֶ���Ȧ, 2-P-Iris1, 3-Union 3-9mm F1.6-2.7 (T5280-PQ1), 4-Union 2.8-12mm F1.6-2.7 (T5289-PQ1),5-HIK 3.8-16mm F1.5��HV3816P-8MPIR��*/
            public byte byMirror;  /* ����0 off��1- leftright��2- updown��3-center 4-Auto*/
            public byte byDigitalZoom;  /*��������:0 dsibale  1 enable*/
            public byte byDeadPixelDetect;   /*������,0 dsibale  1 enable*/
            public byte byBlackPwl;/*�ڵ�ƽ���� ,  0-255*/
            public byte byEptzGate;// EPTZ���ر���:0-�����õ�����̨��1-���õ�����̨
            public byte byLocalOutputGate;//����������ر���0-��������ر�1-����BNC����� 2-HDMI����ر�  
            //20-HDMI_720P50�����
            //21-HDMI_720P60�����
            //22-HDMI_1080I60�����
            //23-HDMI_1080I50�����
            //24-HDMI_1080P24�����
            //25-HDMI_1080P25�����
            //26-HDMI_1080P30�����
            //27-HDMI_1080P50�����
            //28-HDMI_1080P60�����
            public byte byCoderOutputMode;//������fpga���ģʽ0ֱͨ3���ذ��
            public byte byLineCoding; //�Ƿ����б��룺0-��1-��
            public byte byDimmerMode; //����ģʽ��0-���Զ���1-�Զ�
            public byte byPaletteMode; //��ɫ�壺0-���ȣ�1-���ȣ�2-��ɫ��2������8-��ɫ��8, 9-�ں�1,10-�ʺ�,11-�ں�2,12-����1,13-����2,14-���ɫ,15-ɫ��1,16-ɫ��2,17-����,18-��,19-����,20-����
            public byte byEnhancedMode; //��ǿ��ʽ��̽�������ܱߣ���0-����ǿ��1-1��2-2��3-3��4-4
            public byte byDynamicContrastEN;    //��̬�Աȶ���ǿ 0-1
            public byte byDynamicContrast;    //��̬�Աȶ� 0-100
            public byte byJPEGQuality;    //JPEGͼ������ 0-100
            public NET_DVR_CMOSMODECFG struCmosModeCfg;//CMOSģʽ��ǰ�˲������ã���ͷģʽ����������ȡ
            public byte byFilterSwitch; //�˲����أ�0-�����ã�1-����
            public byte byFocusSpeed; //��ͷ�����ٶȣ�0-10
            public byte byAutoCompensationInterval; //��ʱ�Զ����Ų�����1-120����λ������
            public byte bySceneMode;  //����ģʽ��0-���⣬1-���ڣ�2-Ĭ�ϣ�3-����
            public NET_DVR_DEFOGCFG struDefogCfg;//͸������
            public NET_DVR_ELECTRONICSTABILIZATION struElectronicStabilization;//���ӷ���
            public NET_DVR_CORRIDOR_MODE_CCD struCorridorMode;//����ģʽ
            public byte byExposureSegmentEnable; //0~������,1~����  �ع�ʱ�������ʽ���״�����������ع����ϵ���ʱ��������ع�ʱ�䵽�м�ֵ��Ȼ��������浽�м�ֵ��������ع⵽���ֵ�����������浽���ֵ
            public byte byBrightCompensate;//������ǿ [0~100]
            /*
             0-�رա�1-640*480@25fps��2-640*480@30ps��3-704*576@25fps��4-704*480@30fps��5-1280*720@25fps��6-1280*720@30fps��
             7-1280*720@50fps��8-1280*720@60fps��9-1280*960@15fps��10-1280*960@25fps��11-1280*960@30fps��
             12-1280*1024@25fps��13--1280*1024@30fps��14-1600*900@15fps��15-1600*1200@15fps��16-1920*1080@15fps��
             17-1920*1080@25fps��18-1920*1080@30fps��19-1920*1080@50fps��20-1920*1080@60fps��21-2048*1536@15fps��22-2048*1536@20fps��
             23-2048*1536@24fps��24-2048*1536@25fps��25-2048*1536@30fps��26-2560*2048@25fps��27-2560*2048@30fps��
             28-2560*1920@7.5fps��29-3072*2048@25fps��30-3072*2048@30fps��31-2048*1536@12.5��32-2560*1920@6.25��
             33-1600*1200@25��34-1600*1200@30��35-1600*1200@12.5��36-1600*900@12.5��37-1280*960@12.5fps��38-800*600@25fps��39-800*600@30fps40��
             4000*3000@12.5fps��41-4000*3000@15fps��42-4096*2160@20fps��43-3840x2160@20fps ��44-960*576@25fps��45-960*480@30fps��46-752*582@25fps��
             47-768*494@30fps��48-2560*1440@25fps��49-2560*1440@30fps ��50-720P@100fps��51-720P@120fps��52-2048*1536@50fps��53-2048*1536@60fps��
             54-3840*2160@25fps��55-3840*2160@30fps��56-4096*2160@25fps��57-4096*2160@30fps ��58-1280*1024@50fps��59-1280*1024@60fps��
             60-3072*2048@50fps��61-3072*2048@60fps��62-3072*1728@25fps��63-3072*1728@30fps��64-3072*1728@50fps��65-3072*1728@60fps��66-336*256@50fps��67-336*256@60fps��
             68-384*288@50fps��69-384*288@60fps ��70- 640 * 512@50fps ��71- 640 * 512@60fps��72-2592*1944@25fps��73-2592*1944@30fps��74-2688*1536@25fps��75-2688*1536@30fps 
             76-2592*1944@20fps��77-2592*1944@15fps��78-2688*1520@20fps��79-2688*1520@15fps��80-2688*1520@25fps��81-2688*1520@30fps��82- 2720*2048@25fps�� 83- 2720*2048@30fps��
             84-336*256@25fps��85- 384*288@25fps��86-640*512@25fps��87-1280*960@50fps��88-1280*960@60fps��89-1280*960@100fps��90-1280*960@120fps��91-4000*3000@20fps��
             92-1920*1200@25fps��93-1920*1200@30fps��94-2560*1920@25fps��95-2560*1920@20fps��96-2560*1920@30fps��97-1280*1920@25fps��98-1280*1920@30fps
             99-4000*3000@24fps��100-4000*3000@25fps��101-4000*3000@10fps��102- 384*288@30fps��103-2560*1920@15fps��104-2400*3840@25fps��105-1200*1920@25fps
             106-4096*1800@30fps��107-3840*1680@30fps��108-2560*1120@30fps��109-704*320@30fps��110-1280*560@30fps��111-4096*1800@25fps��112-3840*1680@25fps
             113-2560*1120@25fps��114-704*320@25fps��115-1280*560@25fps��116-2400*3840@24fps��117-3840*2400@24fps��118-3840*2400@25fps��119-2560*1920@12.5fps
             120-2560*2048@12fps��121-2560*2048@15fps��122-2560*1536@25fps��123-2560*1536@30fps
            */
            public byte byCaptureModeN; //��Ƶ����ģʽ��N�ƣ�
            public byte byCaptureModeP; //��Ƶ����ģʽ��P�ƣ�
            public NET_DVR_SMARTIR_PARAM struSmartIRParam; //����Ź���������Ϣ
            public NET_DVR_PIRIS_PARAM struPIrisParam;//PIris������Ϣ��ӦbyIrisMode�ֶδ�2-PIris1��ʼ��Ч
            //2014-02-25 ��������
            public NET_DVR_LASER_PARAM_CFG struLaserParam;    //�������
            public NET_DVR_FFC_PARAM struFFCParam;
            public NET_DVR_DDE_PARAM struDDEParam;
            public NET_DVR_AGC_PARAM struAGCParam;
            public byte byLensDistortionCorrection;//��ͷ����У�� 0-�ر�,1-����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_SNAP_CAMERAPARAMCFG struSnapCCD; //ץ�Ļ�CCD������ֻ����ץ�Ļ�
            public NET_DVR_OPTICAL_DEHAZE struOpticalDehaze;//��ѧ͸������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
            public byte[] byRes2;
        }

        public const int NET_DVR_GET_FOCUSMODECFG = 3305; //��ȡ����۽�ģʽ��Ϣ
        public const int NET_DVR_SET_FOCUSMODECFG = 3306; //���ÿ���۽�ģʽ��Ϣ

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FOCUSMODE_CFG
        {
            public uint dwSize;
            public byte byFocusMode;  /* �۽�ģʽ��0-�Զ���1-�ֶ���2-���Զ� */
            public byte byAutoFocusMode; /* �Զ��۽�ģʽ��0-�أ�1-ģʽA��2-ģʽB��3-ģʽAB��4-ģʽC �Զ��۽�ģʽ����Ҫ�ھ۽�ģʽΪ�Զ�ʱ����ʾ*/
            public ushort wMinFocusDistance; /* ��С�۽����룬��λCM,  0-�Զ���0xffff-����Զ */
            public byte byZoomSpeedLevel;  /* �䱶�ٶȣ�Ϊʵ��ȡֵ��1-3 */
            public byte byFocusSpeedLevel; /* �۽��ٶȣ�Ϊʵ��ȡֵ��1-3 */
            public byte byOpticalZoom;  /* ��ѧ�䱶��0-255 */
            public byte byDigtitalZoom;  /* ���ֱ䱶��0-255 */
            public Single fOpticalZoomLevel; /* ��ѧ�䱶(����ֵ) [1,32], ��С���0.5 ���ڲ��豸������ʱ��*1000 */
            public uint dwFocusPos;/* dwFocusPos ��focusֵ���۽�ֵ������ΧΪ[0x1000,0xC000]�����ֵ��sony����ֵ��ʹ�����ֵ��Ϊ�˶���ͳһ����֤��ͬ�ľ�ͷ����focusֵ��ת���������Χ�� (�ֶ��۽�ģʽ����Ӧ��)*/
            public byte byFocusDefinitionDisplay;// �۽���������ʾ��0~����ʾ��1~��ʾ, ����������������ʾ��ǰ��ͷĿ���������ֵ�����ڰ����ͻ�����ʹ���ץ���ܹ��ﵽ��������Ч������������Խ�������Խ�����������ȷ�ΧΪ��0~100.0000
            public byte byFocusSensitivity; //�۽������ȣ���Χ[0,2]���۽�ģʽΪ�Զ������Զ�ʱ��Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes1;
            public uint dwRelativeFocusPos;//���focusֵ����Χ��0-4000
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
            public byte[] byRes;
        }

        //ǰ�˲�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CAMERAPARAMCFG
        {
            public uint dwSize;
            public NET_DVR_VIDEOEFFECT struVideoEffect;/*���ȡ��Աȶȡ����Ͷȡ���ȡ�ɫ������*/
            public NET_DVR_GAIN struGain;/*�Զ�����*/
            public NET_DVR_WHITEBALANCE struWhiteBalance;/*��ƽ��*/
            public NET_DVR_EXPOSURE struExposure; /*�ع����*/
            public NET_DVR_GAMMACORRECT struGammaCorrect;/*GammaУ��*/
            public NET_DVR_WDR struWdr;/*����̬*/
            public NET_DVR_DAYNIGHT struDayNight;/*��ҹת��*/
            public NET_DVR_BACKLIGHT struBackLight;/*���ⲹ��*/
            public NET_DVR_NOISEREMOVE struNoiseRemove;/*���ֽ���*/
            public byte byPowerLineFrequencyMode; /*0-50HZ; 1-60HZ*/
            public byte byIrisMode; /*0 �Զ���Ȧ 1�ֶ���Ȧ*/
            public byte byMirror;  /* ����0 off��1- leftright��2- updown��3-center */
            public byte byDigitalZoom;  /*��������:0 dsibale  1 enable*/
            public byte byDeadPixelDetect;   /*������,0 dsibale  1 enable*/
            public byte byBlackPwl;/*�ڵ�ƽ���� ,  0-255*/
            public byte byEptzGate;// EPTZ���ر���:0-�����õ�����̨��1-���õ�����̨
            public byte byLocalOutputGate;//����������ر���0-��������ر�1-����BNC����� 2-HDMI����ر�  
            //20-HDMI_720P50�����
            //21-HDMI_720P60�����
            //22-HDMI_1080I60�����
            //23-HDMI_1080I50�����
            //24-HDMI_1080P24�����
            //25-HDMI_1080P25�����
            //26-HDMI_1080P30�����
            //27-HDMI_1080P50�����
            //28-HDMI_1080P60�����
            //40-SDI_720P50,
            //41-SDI_720P60,
            //42-SDI_1080I50,
            //43-SDI_1080I60,
            //44-SDI_1080P24,
            //45-SDI_1080P25,
            //46-SDI_1080P30,
            //47-SDI_1080P50,
            //48-SDI_1080P60
            public byte byCoderOutputMode;//������fpga���ģʽ0ֱͨ3���ذ��
            public byte byLineCoding; //�Ƿ����б��룺0-��1-��
            public byte byDimmerMode; //����ģʽ��0-���Զ���1-�Զ�
            public byte byPaletteMode; //��ɫ�壺0-���ȣ�1-���ȣ�2-��ɫ��2������8-��ɫ��8
            public byte byEnhancedMode; //��ǿ��ʽ��̽�������ܱߣ���0-����ǿ��1-1��2-2��3-3��4-4
            public byte byDynamicContrastEN;    //��̬�Աȶ���ǿ 0-1
            public byte byDynamicContrast;    //��̬�Աȶ� 0-100
            public byte byJPEGQuality;    //JPEGͼ������ 0-100
            public NET_DVR_CMOSMODECFG struCmosModeCfg;//CMOSģʽ��ǰ�˲������ã���ͷģʽ����������ȡ
            public byte byFilterSwitch; //�˲����أ�0-�����ã�1-����
            public byte byFocusSpeed; //��ͷ�����ٶȣ�0-10
            public byte byAutoCompensationInterval; //��ʱ�Զ����Ų�����1-120����λ������
            public byte bySceneMode;  //����ģʽ��0-���⣬1-���ڣ�2-Ĭ�ϣ�3-����
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ATMFINDINFO
        {
            public byte byTransactionType;       //�������� 0-ȫ����1-��ѯ�� 2-ȡ� 3-�� 4-�޸����룬5-ת�ˣ� 6-�޿���ѯ 7-�޿��� 8-�̳� 9-�̿� 10-�Զ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes;
            public uint dwTransationAmount;     //���׽�� ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SPECIAL_FINDINFO_UNION
        {
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            //public byte[] byLenth;
            public NET_DVR_ATMFINDINFO struATMFindInfo;           //ATM��ѯ
        }

        public const int CARDNUM_LEN_OUT = 32;//�ⲿ�ṹ�忨�ų���
        public const int GUID_LEN = 16;//GUID����

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
            public uint dwFileType;//��������¼���ļ�����0xff��ȫ����0����ʱ¼��,1-�ƶ���� ��2������������3-����|�ƶ���� 4-����&�ƶ���� 5-����� 6-�ֶ�¼��7-����¼��10-PIR������11-���߱�����12-���ȱ�����13-ȫ���¼���14-���ܽ�ͨ�¼�
            //������¼�����ͣ� 0xff��ȫ����0����ʱ¼��1-�ƶ���⣬2���ӽ�������3������������4������������5-�������6���ֶ�¼��7���𶯱�����8-����������9-���ܱ�����10-PIR������11-���߱�����12-���ȱ�����13-ȫ���¼���14-���ܽ�ͨ�¼�
            //15-Խ����⣬16-�������֣�17-�����쳣��18-���������⣬19-������⣨Խ�����|��������|��������|�뿪����|����ʶ��20-�������  21-�ź�����22-�ش���23-��Ǩ¼��24-�ڵ� 25-pos¼��
            public uint dwIsLocked;
            public uint dwUseCardNo;//�Ƿ��ATM��Ϣ���в�ѯ��0-����ATM��Ϣ��1-�����׿��Ų�ѯ��2-���������Ͳ�ѯ��3-�����׽���ѯ��4-�����š��������ͼ����׽�����ϲ�ѯ 5-���γ����Ʋ��ң���ʱ���ű�ʾ�γ�����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CARDNUM_LEN_OUT)]
            public byte[] sCardNumber;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public byte byDrawFrame; //0:����֡��1����֡
            public byte byFindType; //0:��ѯ��ͨ����1����ѯ�浵��
            public byte byQuickSearch; //0:��ͨ��ѯ��1�����٣���������ѯ
            public byte bySpecialFindInfoType;    //ר�в�ѯ�������� 0-��Ч�� 1-��ATM��ѯ����  
            public uint dwVolumeNum;  //�浵����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = GUID_LEN)]
            public byte[] byWorkingDeviceGUID;//������GUID��ͨ����ȡN+1�õ�
            public NET_DVR_SPECIAL_FINDINFO_UNION uSpecialFindInfo;   //ר�в�ѯ����
            public byte byStreamType;    //0-��������1-��������2-��������0xff-ȫ��
            public byte byAudioFile;    //��Ƶ�ļ� 0-����Ƶ�ļ���1-��Ƶ�ļ�
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public byte[] byRes2;
        }

        //¼���ļ�����(cvr)
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINDDATA_V40
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;//�ļ���
            public NET_DVR_TIME struStartTime;//�ļ��Ŀ�ʼʱ��
            public NET_DVR_TIME struStopTime;//�ļ��Ľ���ʱ��
            public uint dwFileSize;//�ļ��Ĵ�С
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;//9000�豸֧��,1��ʾ���ļ��Ѿ�������,0��ʾ�������ļ�
            public byte byFileType;  //�ļ�����:0����ʱ¼��,1-�ƶ���� ��2������������
            //3-����|�ƶ���� 4-����&�ƶ���� 5-����� 6-�ֶ�¼��,7���𶯱�����8-����������9-���ܱ�����10-PIR������11-���߱�����12-���ȱ���,14-���ܽ�ͨ�¼�
            public byte byQuickSearch; //0:��ͨ��ѯ�����1�����٣���������ѯ���
            public byte byRes;
            public uint dwFileIndex; //�ļ�������
            public byte byStreamType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            public byte[] byRes1;
        }

        public const int DEV_TYPE_NAME_LEN = 24;

        //DVR�豸����
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG_V40
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] sDVRName;//DVR����
            public uint dwDVRID;    //DVR ID,����ң���� //V1.4(0-99), V1.5(0-255)
            public uint dwRecycleRecord;  //�Ƿ�ѭ��¼��,0:����; 1:��
            //���²��ɸ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN)]
            public byte[] sSerialNumber;//���к�
            public uint dwSoftwareVersion;      //�����汾��,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwSoftwareBuildDate;      //������������,0xYYYYMMDD
            public uint dwDSPSoftwareVersion;      //DSP�����汾,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwDSPSoftwareBuildDate;  // DSP������������,0xYYYYMMDD
            public uint dwPanelVersion;    // ǰ���汾,��16λ�����汾,��16λ�Ǵΰ汾
            public uint dwHardwareVersion;    // Ӳ���汾,��16λ�����汾,��16λ�Ǵΰ汾
            public byte byAlarmInPortNum;  //DVR�����������
            public byte byAlarmOutPortNum;  //DVR�����������
            public byte byRS232Num;      //DVR 232���ڸ���
            public byte byRS485Num;      //DVR 485���ڸ��� 
            public byte byNetworkPortNum;  //����ڸ���
            public byte byDiskCtrlNum;      //DVR Ӳ�̿���������
            public byte byDiskNum;    //DVR Ӳ�̸���
            public byte byDVRType;    //DVR����, 1:DVR 2:ATM DVR 3:DVS ......
            public byte byChanNum;    //DVR ͨ������
            public byte byStartChan;      //��ʼͨ����,����DVS-1,DVR - 1
            public byte byDecordChans;      //DVR ����·��
            public byte byVGANum;    //VGA�ڵĸ��� 
            public byte byUSBNum;    //USB�ڵĸ���
            public byte byAuxoutNum;      //���ڵĸ���
            public byte byAudioNum;      //�����ڵĸ���
            public byte byIPChanNum;      //�������ͨ���� ��8λ����8λ��byHighIPChanNum 
            public byte byZeroChanNum;      //��ͨ���������
            public byte bySupport;        //������λ����Ϊ0��ʾ��֧�֣�1��ʾ֧�֣�
            //bySupport & 0x1, ��ʾ�Ƿ�֧����������
            //bySupport & 0x2, ��ʾ�Ƿ�֧�ֱ���
            //bySupport & 0x4, ��ʾ�Ƿ�֧��ѹ������������ȡ
            //bySupport & 0x8, ��ʾ�Ƿ�֧�ֶ�����
            //bySupport & 0x10, ��ʾ֧��Զ��SADP
            //bySupport & 0x20, ��ʾ֧��Raid������
            //bySupport & 0x40, ��ʾ֧��IPSAN����
            //bySupport & 0x80, ��ʾ֧��rtp over rtsp
            public byte byEsataUseage;  //Esata��Ĭ����;��0-Ĭ�ϱ��ݣ�1-Ĭ��¼��
            public byte byIPCPlug;      //0-�رռ��弴�ã�1-�򿪼��弴��
            public byte byStorageMode;  //0-����ģʽ,1-�������, 2��֡ģʽ
            public byte bySupport1;  //������λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport1 & 0x1, ��ʾ�Ƿ�֧��snmp v30
            //bySupport1 & 0x2, ֧�����ֻطź�����
            //bySupport1 & 0x4, �Ƿ�֧�ֲ������ȼ�    
            //bySupport1 & 0x8, �����豸�Ƿ�֧�ֲ���ʱ�����չ
            //bySupport1 & 0x10, ��ʾ�Ƿ�֧�ֶ������������33����
            //bySupport1 & 0x20, ��ʾ�Ƿ�֧��rtsp over http    
            public ushort wDevType;//�豸�ͺ�
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DEV_TYPE_NAME_LEN)]
            public byte[] byDevTypeName;//�豸�ͺ����� 
            public byte bySupport2; //��������չ��λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport2 & 0x1, ��ʾ�Ƿ�֧����չ��OSD�ַ�����(�ն˺�ץ�Ļ���չ����)
            public byte byAnalogAlarmInPortNum; //ģ�ⱨ���������
            public byte byStartAlarmInNo;    //ģ�ⱨ��������ʼ��
            public byte byStartAlarmOutNo;  //ģ�ⱨ�������ʼ��
            public byte byStartIPAlarmInNo;  //IP����������ʼ��
            public byte byStartIPAlarmOutNo; //IP���������ʼ��
            public byte byHighIPChanNum;      //����ͨ����������8λ 
            public byte byEnableRemotePowerOn;//�Ƿ��������豸���ߵ�״̬��Զ�̿������ܣ�0-�����ã�1-����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            public byte bySupportLock;        //�豸֧���������ܣ����ֶ���SDK�����豸����ֵ����ֵ�ġ�bySupportLockΪ1ʱ��dwSurplusLockTime��byRetryLoginTime��Ч
            public byte byRetryLoginTime;        //ʣ��ɳ��Ե�½�Ĵ������û������������ʱ���˲�����Ч
            public byte byPasswordLevel;      //admin���밲ȫ�ȼ�0-��Ч��1-Ĭ�����룬2-��Ч����,3-���սϸߵ����롣���û�������Ϊ����Ĭ�����루12345�����߷��սϸߵ�����ʱ���ϲ�ͻ�����Ҫ��ʾ�û��������롣      
            public byte byRes1;
            public uint dwSurplusLockTime;    //ʣ��ʱ�䣬��λ�룬�û�����ʱ���˲�����Ч
            public byte byCharEncodeType;     //�ַ���������
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
            public byte byUseUTCTime;    //0-������ת����Ĭ��,1-�ӿ����������ȫ��ʹ��UTCʱ��,SDK���UTCʱ�����豸ʱ����ת��,2-�ӿ����������ȫ��ʹ��ƽ̨����ʱ�䣬SDK���ƽ̨����ʱ�����豸ʱ����ת��
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
            public IntPtr lpCondBuffer;    //[in]��������(���ָ�ʽ),����ͨ���ŵ�.����ΪNULL
            public uint dwCondSize;        //[in] dwCondSizeָ����ڴ��С
            public IntPtr lpOutBuffer;    //[out]�������(XML��ʽ),��ΪNULL
            public uint dwOutSize;        //[in] lpOutBufferָ����ڴ��С
            public IntPtr lpStatusBuffer;    //[out]���ص�״̬����(XML��ʽ),��ȡ�ɹ�ʱ���ḳֵ,�������Ҫ,������NULL
            public uint dwStatusSize;    //[in] lpStatusBufferָ����ڴ��С
            public uint dwRetSize;        //[out]��ȡ�������ݳ���(lpOutBuffer����lpStatusBufferָ���ʵ�����ݳ���)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //�����ֽ�
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSTDAbility(int lUserID, uint dwAbilityType, IntPtr lpAbilityParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_INPUT
        {
            public uint dwSize;                    //�ṹ���С
            public IntPtr lpRequestUrl;                //��������ַ�����ʽ
            public uint dwRequestUrlLen;            //��������ȣ��ַ�������
            public IntPtr lpInBuffer;                //���������������XML��ʽ
            public uint dwInBufferSize;            //���������������С
            public uint dwRecvTimeOut;                //���ճ�ʱʱ�䣬��λ��ms����0��ʹ��Ĭ�ϳ�ʱ5s
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //�����ֽ�
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_OUTPUT
        {
            public uint dwSize;                    //�ṹ���С
            public IntPtr lpOutBuffer;                //���������������XML��ʽ
            public uint dwOutBufferSize;            //���������������С(�ڴ��С)
            public uint dwReturnedXMLSize;            //ʵ�������XML���ݴ�С
            public IntPtr lpStatusBuffer;            //���ص�״̬����(XML��ʽ),��ȡ����ɹ�ʱ���ḳֵ,�������Ҫ,������NULL
            public uint dwStatusSize;                //״̬��������С(�ڴ��С)
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;        //�����ֽ�
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_STDXMLConfig(int lUserID, IntPtr lpInputParam, IntPtr lpOutputParam);

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_STD_CONFIG
        {
            public IntPtr lpCondBuffer;    //[in]��������(�ṹ���ʽ),����ͨ���ŵ�.����ΪNULL
            public uint dwCondSize;        //[in] dwCondSizeָ����ڴ��С
            public IntPtr lpInBuffer;        //[in]byDataType = 0ʱ��Ч,�������(�ṹ���ʽ),����ʱ��ΪNULL����ȡʱΪNULL
            public uint dwInSize;        //[in] lpInBufferָ����ڴ��С
            public IntPtr lpOutBuffer;    //[out]byDataType = 0ʱ��Ч,�������(�ṹ���ʽ),��ȡʱ��ΪNULL,����ʱΪNULL
            public uint dwOutSize;        //[in] lpOutBufferָ����ڴ��С
            public IntPtr lpStatusBuffer;    //[out]���ص�״̬����(XML��ʽ),��ȡ�ɹ�ʱ���ḳֵ,�������Ҫ,������NULL
            public uint dwStatusSize;    //[in] lpStatusBufferָ����ڴ��С
            public IntPtr lpXmlBuffer;    //[in/out]byDataType = 1ʱ��Ч,xml��ʽ����
            public uint dwXmlSize;      //[in/out]lpXmlBufferָ����ڴ��С,��ȡʱͬʱ��Ϊ����������������ȡ�ɹ�����޸Ļ�ʵ�ʳ��ȣ�����ʱ��ʾʵ�ʳ��ȣ������������ڴ��С
            public byte byDataType;     //[in]����/�����������,0-ʹ�ýṹ������lpInBuffer/lpOutBuffer��Ч,1-ʹ��XML����lpXmlBuffer��Ч
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 23)]
            public byte[] byRes1;        //�����ֽ�
            //              uint            dwRetSize;        //��ȡ�������ݳ���(lpOutBuffer����lpStatusBufferָ���ʵ�����ݳ���)
            //              [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            //              public byte[]   byRes2;        //�����ֽ�
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
        public const int NET_DVR_USER_LOCKED = 153;//�û�������

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
            public uint dwStreamType; //�������ͣ�0-��������1-��������2-����3��
            public uint dwLinkMode;//�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-RTP/RTSP,5-RSTP/HTTP
            public bool bPassbackRecord; //0-������¼��ش�,1����¼��ش�
            public uint dwPreviewMode;  //Ԥ��ģʽ 0-����ģʽ 1-��ʱԤ��
            public int iPicResolution;    //resolution
            public int iPicQuality;    //image quality
            public Int32 lRealHandle;          //preview handle
            public bool bLocalManualRec;     //manual record
            public bool bAlarm;    //alarm
            public bool bEnable;      //enable
            public uint dwImageType;  //channel status icon
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string chAccessChanIP;   //ip addr of IP channel
            public uint nPreviewProtocolType;    //ȡ��Э������ 0-˽��Э�� 1-RTSPЭ��
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
            public byte byGetStreamType; //ȡ����ʽGET_STREAM_TYPE��0-ֱ�Ӵ��豸ȡ����1-����ý��ȡ����2-ͨ��IPServer���ip��ַ��ȡ��,3.ͨ��IPServer�ҵ��豸����ͨ����ý��ȥ�豸����
            //4-ͨ����ý����URLȥȡ��,5-ͨ��hkDDNSȡ����6-ֱ�Ӵ��豸ȡ��(��չ)��ʹ��NET_DVR_IPCHANINFO_V40�ṹ, 7-ͨ��RTSPЭ�鷽ʽ����ȡ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_GET_STREAM_UNION uGetStream;    // ��ͬȡ����ʽ�ṹ��
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPSERVER_STREAM
        {
            public byte byEnable;   // �Ƿ�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struIPServer;    //IPServer ��ַ
            public ushort wPort;                  //IPServer �˿�
            public ushort wDvrNameLen;            // DVR ���Ƴ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRName;              // DVR����
            public ushort wDVRSerialLen;          // ���кų���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;      // DVR���кų���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;             // DVR ��½�û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord;             // DVR��½����
            public byte byChannel;              // DVR ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public ushort[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDNS_STREAM_CFG
        {
            public byte byEnable;   // �Ƿ�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struStreamServer;
            public ushort wStreamServerPort;           //��ý��������˿�   
            public byte byStreamServerTransmitType;  //��ý�崫��Э������ 0-TCP��1-UDP
            public byte byRes2;
            public NET_DVR_IPADDR struIPServer;          //IPSERVER��ַ
            public ushort wIPServerPort;        //IPserver�˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;             //DVR���� 
            public ushort wDVRNameLen;            // DVR���Ƴ���
            public ushort wDVRSerialLen;          // ���кų���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRSerialNumber;       // DVR���к�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;              // DVR ��½�û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassWord;              // DVR��½����
            public ushort wDVRPort;   //DVR�˿ں�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
            public byte byChannel;              // DVR ͨ��
            public byte byTransProtocol; //����Э������0-TCP��1-UDP
            public byte byTransMode; //��������ģʽ 0�������� 1��������
            public byte byFactoryType; //ǰ���豸��������,ͨ���ӿڻ�ȡ
        }

        public const int URL_LEN = 240;  //URL����

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_URL
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = URL_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] strURL;
            public byte byTransPortocol; // ����Э������ 0-tcp  1-UDP
            public ushort wIPID;  //�豸ID�ţ�wIPID = iDevInfoIndex + iGroupNO*64 +1
            public byte byChannel;  //ͨ����
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
            public byte[] byDDNSDomain;   // hiDDNS������
            public ushort wPort;                  // hiDDNS �˿�
            public ushort wAliasLen;              // ��������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlias;              // ����
            public ushort wDVRSerialLen;          // ���кų���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;     // DVR���к�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;              // DVR ��½�û���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord;
            public byte byChannel;              // DVRͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO_V40
        {
            public byte byEnable;
            public byte byRes1;
            public ushort wIPID;                  //IP�豸ID
            public uint dwChannel;    //ͨ����
            public byte byTransProtocol;  //����Э������0-TCP��1-UDP
            public byte byTransMode;      //��������ģʽ 0�������� 1��������
            public byte byFactoryType;      /*ǰ���豸��������,ͨ���ӿڻ�ȡ*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 241, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GET_STREAM_UNION
        {
            public NET_DVR_IPCHANINFO struChanInfo;    /*IPͨ����Ϣ*/
            //public NET_DVR_IPSERVER_STREAM struIPServerStream;  // IPServerȥ��
            //public NET_DVR_PU_STREAM_CFG struPUStream;     //  ͨ��ǰ���豸��ȡ��ý��ȥ��
            //public NET_DVR_DDNS_STREAM_CFG struDDNSStream;     //ͨ��IPServer����ý��ȡ��
            //public NET_DVR_PU_STREAM_URL struStreamUrl;        //ͨ����ý�嵽urlȡ��
            //public NET_DVR_HKDDNS_STREAM struHkDDNSStream;   //ͨ��hiDDNSȥȡ��
            //public NET_DVR_IPCHANINFO_V40 struIPChan; //ֱ�Ӵ��豸ȡ������չ��
        }

        public const int MAX_IP_DEVICE_V40 = 64;// ������������IP�豸�� ��������64�� IVMS 2000�����豸
        // ͨ����Դ���� (NET_DVR_IPPARACFG_V40�ṹ)
        public const int NET_DVR_GET_IPPARACFG_V40 = 1062;// ��ȡIP��������
        public const int NET_DVR_SET_IPPARACFG_V40 = 1063;// ����IP��������

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V40
        {
            public uint dwSize;
            public uint dwGroupNum;        //     �豸֧�ֵ�������    
            public uint dwAChanNum;        //���ģ��ͨ������
            public uint dwDChanNum;                  //����ͨ������
            public uint dwStartDChan;              //��ʼ����ͨ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30)]
            public byte[] byAnalogChanEnable;       /* ģ��ͨ���Ƿ����ã��ӵ͵��߱�ʾ1-64ͨ����0��ʾ��Ч 1��Ч */
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
            public uint dwIPID;        /* IP�豸ID */
            public uint dwAlarmIn;    /* IP�豸ID��Ӧ�ı�������� */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        public const int MAX_IP_ALARMIN_V40 = 4096;//�����������౨��������

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG_V40
        {
            public uint dwSize;  //�ṹ�峤��
            public uint dwCurIPAlarmInNum;  //��ǰ�����������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN_V40)]
            public NET_DVR_IPALARMININFO_V40[] struIPAlarmInInfo; /* IP��������*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO_V40
        {
            public uint dwIPID;                 /* IP�豸ID */
            public uint dwAlarmOut;    /* IP�豸ID��Ӧ�ı�������� */
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
            public uint dwChannel; //ͨ����
            public uint dwPatternCmd; //��̨�켣����������,������涨��
            public uint dwPatternID; //��̨�켣ID��ɾ�����й켣ʱ��Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        // �����豸���������ṹ��
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
            ENUM_UPGRADE_DVR = 0, //��ͨ�豸����
            ENUM_UPGRADE_ADAPTER = 1, //DVR����������
            ENUM_UPGRADE_VCALIB = 2,  //���ܿ�����
            ENUM_UPGRADE_OPTICAL = 3, //��˻�����
            ENUM_UPGRADE_ACS = 4, //�Ž�ϵͳ����
            ENUM_UPGRADE_AUXILIARY_DEV = 5,//�����豸����
        }
        public const int MAX_IP_ALARMOUT_V40 = 4096;//�����������౨�������

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG_V40
        {
            public uint dwSize;  //�ṹ�峤��
            public uint dwCurIPAlarmOutNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT_V40)]
            public NET_DVR_IPALARMOUTINFO_V40[] struIPAlarmOutInfo; /* IP�������*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        //2011-12-16 ��������ṹ silujie
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

        public const int NET_DVR_GET_DEVICECFG_V40 = 1100;//��ȡ��չ�豸���� 
        public const int NET_DVR_SET_DEVICECFG_V40 = 1101;//������չ�豸����
        public const int NET_DVR_GET_IPALARMINCFG_V40 = 6183;//��ȡIP�����������������Ϣ

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
            public byte bySupport1;    //������λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            // bySupport1 & 0x1, ��ʾ�Ƿ�֧��snmp v30
            //bySupport1 & 0x2, ֧�����ֻطź�����    
            //bySupport1 & 0x4, �Ƿ�֧�ֲ������ȼ�    
            //bySupport1 & 0x8, �����豸�Ƿ�֧�ֲ���ʱ�����չ
            //bySupport1 & 0x10, ��ʾ�Ƿ�֧�ֶ������������33����
            //bySupport1 & 0x40 ��ʾ�Ƿ�֧���ӳ�Ԥ��
            public byte bySupport2; //��������չ��λ����Ϊ0��ʾ��֧�֣�1��ʾ֧��
            //bySupport2 & 0x1, ��ʾ�������Ƿ�֧��ͨ��URLȡ������
            //bySupport2 & 0x2,  ��ʾ֧��FTPV40
            //bySupport2 & 0x4,  ��ʾ֧��ANR(����¼��)
            public byte byStartIPAlarmOutNo;  //��ʼIP���������
            public byte byMirrorChanNum; //����ͨ��������<¼�����������ڱ�ʾ����ͨ��>
            public ushort wStartMirrorChanNo;  //��ʼ����ͨ����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public STRU_CHANNEL_INFO[] struMirrorChan;
            public byte bySupport5;
            public byte bySupport7;// bySupport7 & 0x2  ��ʾ�豸�Ƿ�֧�� IPC HVT ģʽ��չ
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
        public struct NET_DVR_SERIALSTART_V40   //͸��ͨ��
        {
            public uint dwSize;         //�ṹ���С
            public uint dwSerialType;    //���ںţ�1-232���ڣ�2-485���ڣ�
            public byte bySerialNum;   //���ڱ��
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
            NET_SDK_LOCAL_CFG_TYPE_TCP_PORT_BIND = 0,  //����TCP�˿ڰ����ã���Ӧ�ṹ��NET_DVR_LOCAL_TCP_PORT_BIND_CFG
            NET_SDK_LOCAL_CFG_TYPE_UDP_PORT_BIND,      //����UDP�˿ڰ����ã���Ӧ�ṹ��NET_DVR_LOCAL_UDP_PORT_BIND_CFG
            NET_SDK_LOCAL_CFG_TYPE_MEM_POOL,    //�ڴ�ر������ã���Ӧ�ṹ��NET_DVR_LOCAL_MEM_POOL_CFG
            NET_SDK_LOCAL_CFG_TYPE_MODULE_RECV_TIMEOUT,  //��ģ�����ó�ʱʱ�䣬��Ӧ�ṹ��NET_DVR_LOCAL_MODULE_RECV_TIMEOUT_CFG
            NET_SDK_LOCAL_CFG_TYPE_ABILITY_PARSE,      //�Ƿ�ʹ�������������⣬��Ӧ�ṹ��NET_DVR_LOCAL_ABILITY_PARSE_CFG
            NET_SDK_LOCAL_CFG_TYPE_TALK_MODE,    //�Խ�ģʽ����Ӧ�ṹ��NET_DVR_LOCAL_TALK_MODE_CFG
            NET_SDK_LOCAL_CFG_TYPE_PROTECT_KEY,    //��Կ���ã���Ӧ�ṹ��NET_DVR_LOCAL_PROTECT_KEY_CFG
            NET_SDK_LOCAL_CFG_TYPE_CFG_VERSION,             //���ڲ��԰汾ͷ���豸�˼������, ֻ�������ò���ʱ�������á�
            NET_SDK_LOCAL_CFG_TYPE_RTSP_PARAMS,    //rtsp�������ã����ڽṹ��NET_DVR_RTSP_PARAMS_CFG
            NET_SDK_LOCAL_CFG_TYPE_SIMXML_LOGIN,            //�ڵ�¼ʱʹ��ģ����������support�ֶ�, ��Ӧ�ṹNET_DVR_SIMXML_LOGIN
            NET_SDK_LOCAL_CFG_TYPE_CHECK_DEV,                //�����������ʱ��
            NET_SDK_LOCAL_CFG_TYPE_SECURITY,                  //SDK���ΰ�ȫ���ã�
            NET_SDK_LOCAL_CFG_TYPE_EZVIZLIB_PATH,            //����өʯ��ͨ�ſ��ַ��
            NET_SDK_LOCAL_CFG_TYPE_CHAR_ENCODE,               //13.�����ַ�������ش����ص�
            NET_SDK_LOCAL_CFG_TYPE_PROXYS                     //���û�ȡ��
        }
        //����SDK���ز���
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSDKLocalCfg(NET_SDK_LOCAL_CFG_TYPE enumType, IntPtr lpInBuff);

        public const int CP_UTF8 = 65001;
        public const int CP_ACP = 0;

        // ���ֶ��Ʋ˵����ģʽ�ⲿ����
        public const int NET_DVR_GET_MEMU_OUTPUT_MODE = 155649;// ��ȡ�˵����ģʽ
        public const int NET_DVR_SET_MEMU_OUTPUT_MODE = 155650;// ���ò˵����ģʽ

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MENU_OUTPUT_MODE
        {
            public uint dwSize;
            public byte byMenuOutputMode; //��ͬԴ�豸��0-Auto 1-��CVBS 2-HDMI 3-VGA ͬԴ�豸��0-Auto 1-��CVBS 2-HDMI/VGA
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
            public byte[] szDomain;//��������ַ������ 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StruAddrIP
        {
            public NET_DVR_IPADDR struIp;/*IP��ַ*/        //IPv4 IPv6��ַ, 144�ֽ�
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
            public byte byAddressType;   //0-ʵ��ipv4 ipv6��ַ 1-����
            // ����unionServer��ʹ��ʵ�ʵ�ַ��������
            public ushort wPort;            /*�˿�*/
            public byte byLoginStatus; /*���豸�ĵ�½״̬ 0-logout,1-login*/
            public UnionServer unionServer;//ʹ��������ṹ��ͨ��byAddressType�ֶα�ʾ��IP��ַ�������� 64    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szUserName;/*�û���*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szPassWord;/*����*/
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
            public byte byDrawFrame; //0:����֡��1����֡
            public byte byVolumeType;  //0-��ͨ¼���  1-�浵��
            public byte byVolumeNum;  //���ţ�Ŀǰָ�浵����
            public byte byStreamType;   //�������� 0-�������� 1-��������2-������
            public uint dwFileIndex;      //�浵���ϵ�¼���ļ������������浵��¼��ʱ���ص�ֵ
            public byte byAudioFile;    //��Ƶ�ļ�0-��1-��
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
            public byte byDrawFrame;  //0:����֡��1����֡
            public byte byStreamType; //�������ͣ�0-������ 1-������ 2-������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;    //����
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
            public byte[] byRes1;  //����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindHardStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindChanNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  //����
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
            public byte[] byRes;  //����
        }

        //�¶ȼ�ⱨ��
        public const int COMM_THERMOMETRY_ALARM = 0x5212;  //�¶ȱ����ϴ�  5210Ӧ��Ϊox5212,�ֲ�����5212
        public const int COMM_THERMOMETRY_DIFF_ALARM = 0x5211;  //�²���ϴ�

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_PTZ_INFO
        {
            public float fPan;
            public float fTilt;
            public float fZoom;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        //����ֵ��һ��,������ֵΪ��ǰ����İٷֱȴ�С, ����ΪС�������λ
        //������ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POINT
        {
            public float fX;                                // X������, 0.001~1
            public float fY;                                //Y������, 0.001~1
        }

        //����ͽṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POLYGON
        {
            public uint dwPointNum;                                  //��Ч�� ���ڵ���3������3����һ��������Ϊ����Ч�����߽�����Ϊ����Ч���� 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = CHCNetSDK.VCA_MAX_POLYGON_POINT_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPos; //����α߽��,���ʮ�� 
        }

        //�¶ȱ���������¶Ⱥ������¶ȱȽϱ�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_ALARM
        {
            public uint dwSize;
            public uint dwChannel;//ͨ����
            public byte byRuleID;//����ID
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)
            public ushort wPresetNo; //Ԥ�õ��
            public NET_PTZ_INFO struPtzInfo;//ptz������Ϣ
            public byte byAlarmLevel;//0-Ԥ�� 1-����
            public byte byAlarmType;/*�������� 0-����¶� 1-����¶� 2-ƽ���¶� 3-�²�*/
            public byte byAlarmRule;//0-���ڣ�1-С��
            public byte byRuleCalibType;//����궨���� 0-�㣬1-��2��
            public NET_VCA_POINT struPoint;//��������꣨������궨����Ϊ���ʱ����Ч��
            public NET_VCA_POLYGON struRegion;//���򣨵�����궨����Ϊ���ʱ����Ч��
            public float fRuleTemperature;/*���ù����¶�,��ȷ��С�����һλ(-40-1000),��������+100�� */
            public float fCurrTemperature;/*��ǰ�¶�,��ȷ��С�����һλ(-40-1000),��������+100�� */
            public uint dwPicLen;//�ɼ���ͼƬ����
            public uint dwThermalPicLen;//�ȳ���ͼƬ����
            public uint dwThermalInfoLen;//�ȳ��񸽼���Ϣ����
            public IntPtr pPicBuff; ///�ɼ���ͼƬָ��
            public IntPtr pThermalPicBuff;// �ȳ���ͼƬָ��
            public IntPtr pThermalInfoBuff; //�ȳ��񸽼���Ϣָ��
            public NET_VCA_POINT struHighestPoint;//�ߡ����������¶�λ�����꣨������궨����Ϊ�ߡ����ʱ����Ч��
            public Single fHighestTemperature;//�ߡ����������¶ȣ�������궨����Ϊ�ߡ����ʱ����Ч��,��ȷ��С�����һλ(-40-1000),��������+100��
            public NET_VCA_POINT struLowestPoint;//�ߡ����������¶�λ�����꣨������궨����Ϊ�ߡ����ʱ����Ч��
            public Single fLowestTemperature;//�ߡ����������¶ȣ�������궨����Ϊ�ߡ����ʱ����Ч��,��ȷ��С�����һλ(-40-1000),��������+100��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TEMPERATURE_COLOR
        {
            /*
            ѡ��0~���±�������ʱ��<highTemperature>�ֶ���Ч,�����ڸ��¶�ֵʱ�����н�����ɫ��ע��
            ѡ��1~���±�������ʱ, <lowTemperature>�ֶ���Ч,�����ڸ��¶�ֵʱ�����н�����ɫ��ע��
            ѡ��2~���䱨������ʱ��<highTemperature>��<lowTemperature>�ֶ���Ч�������¶��ڸ��¶�����ʱ�����н�����ɫ��ע��
            ѡ��3~���±�������ʱ��<highTemperature>��<lowTemperature>�ֶ���Ч�����¶Ȳ��ڸ��¶�����ʱ�����н�����ɫ��ע�� 
            ѡ��4~Ϊ�ޱ������ͣ�<nullAlarm>�ֶ���Ч���رձ�����*/
            public byte byType;//���±�����ɫ�������ͣ�0~�ޱ������ͣ��رգ���1~���±������ͣ�2~���±������ͣ�3~���䱨�����ͣ�4~���±�������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint iHighTemperature;//����ֵ��-273~10000
            public uint iLowTemperature;//����ֵ��-273~10000
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_BASICPARAM
        {
            public uint dwSize;//�ṹ���С
            public byte byEnabled;  //�Ƿ�ʹ�ܣ�0- ��1- ��
            public byte byStreamOverlay; //���������¶���Ϣ��0- ��1- ��
            public byte byPictureOverlay;//ץͼ�����¶���Ϣ��0- ��1- ��
            public byte byThermometryRange;//���·�Χ(): 0-Ĭ��ֵ,1-(-20~150),2-(0~550)�����������϶�Ϊ��λ���㣩,3-(���϶�:0-650�棻�����¶�:32-1200�H),4-�����϶�: -40-150�棩,5-(���϶�: 0~1200��)�����������϶�Ϊ��λ���㣬���ݲ��µ�λ�趨��ͬ���·�Χ����ʾ��
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)��
            public byte byThermometryCurve;//��������ģʽ��ʾ��ʽ��0-�رգ�1-ģʽ1�������¶�������ģʽ����2-ģʽ2�������¶�������ģʽ��
            public byte byFireImageModea;
            public byte byShowTempStripEnable;//��ʾ�¶���ʹ�ܣ�0- ��1- ��
            public float fEmissivity;//������(������ ��ȷ��С�������λ)[0.01, 1.00](��������������������ı���)
            public byte byDistanceUnit;//���뵥λ: 0-�ף�m����1-Ӣ�ߣ�feet����
            public byte byEnviroHumidity;//�������ʪ�ȣ�ȡֵ��Χ��0~100%
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            NET_DVR_TEMPERATURE_COLOR struTempColor;//���±�����ɫ
            public uint iEnviroTemperature;//�����¶ȣ�ȡֵ��Χ��-273~10000���϶�
            public uint iCorrectionVolume;//������������ȡֵ��Χ��-100~100
            /*bit0-���ĵ���£�0-����ʾ��1-��ʾ��
            bit1-��ߵ���£�0-����ʾ��1-��ʾ��
            bit2-��͵���£�0-����ʾ��1-��ʾ��
            */
            public byte bySpecialPointThermType;// ������µ���ʾ
            public byte byReflectiveEnabled;//�����¶�ʹ�ܣ�0- ��1- ��
            public ushort wDistance;//����(m)[0, 10000]
            public float fReflectiveTemperature;//�����¶� ��ȷ��С����һλ
            public float fAlert;//Ԥ���¶���ֵ��-100.0-1000.0�ȣ���ȷ��С�����һλ��
            public float fAlarm;//�����¶���ֵ��-100.0-1000.0�ȣ���ȷ��С�����һλ��
            public float fThermalOpticalTransmittance;// ��ѧ͸����, ��ȷ��С�����3λ����Χ0.001-1.000��Ĭ��1.000
            public float fExternalOpticsWindowCorrection;//�ⲿ��ѧ�¶ȣ�Ĭ��ֵ20�棬��ΧΪ-40.0~80.0�棬ʵ����ʾ��λ�Խ�����ʾΪ׼

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_COND
        {
            public uint dwSize;//�ṹ���С
            public uint dwChannel;
            public ushort wPresetNo;//0-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_THERMOMETRY_PRESETINFO_PARAM
        {
            public byte byEnabled;  //�Ƿ�ʹ�ܣ�0- ��1- ��
            public byte byRuleID;//����ID 0-��ʾ��Ч����1��ʼ ��list�ڲ��ж�������Ч�ԣ�
            public ushort wDistance;//����(m)[0, 10000]
            public float fEmissivity;//������(������ ��ȷ��С�������λ)[0.01, 1.00](��������������������ı���)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public byte byReflectiveEnabled;//�����¶�ʹ�ܣ�0- ��1- ��
            public float fReflectiveTemperature;//�����¶� ��ȷ��С����һλ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//��������    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byRuleCalibType;//����궨���� 0-�㣬1-��2-��
            public NET_VCA_POINT struPoint;//��������꣨������궨����Ϊ"��"��ʱ����Ч��
            public NET_VCA_POLYGON struRegion;//�����ߣ�������궨����Ϊ"��"����"��"��ʱ����Ч��
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NET_DVR_THERMOMETRY_PRESETINFO
        {
            public uint dwSize;//�ṹ���С
            public ushort wPresetNo;//0-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_THERMOMETRY_REGION_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_PRESETINFO_PARAM[] struPresetInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential/*, CharSet = CharSet.Unicode*/)]
        public struct NET_DVR_THERMOMETRY_ALARMRULE_PARAM
        {
            public byte byEnable;
            public byte byRuleID;//����ID
            public byte byRule;//�����¶ȱȽϷ�ʽ 0-���´���,1-����С��,2-���´���,3-����С��,4-ƽ���´���,5-ƽ����С��,6-�²����,7-�²�С��    
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//��������
            public float fAlert;//Ԥ���¶�
            public float fAlarm;//�����¶�
            public float fThreshold;//�����¶�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential/*, CharSet = CharSet.Unicode*/)]
        public struct NET_DVR_THERMOMETRY_ALARMRULE
        {
            public uint dwSize;//�ṹ���С
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = THERMOMETRY_ALARMRULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_ALARMRULE_PARAM[] struThermometryAlarmRuleParam;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //Ԥ�õ��²���������ýṹ��
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

        //�²������
        public struct NET_DVR_THERMOMETRY_DIFFCOMPARISON
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = THERMOMETRY_ALARMRULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_THERMOMETRY_DIFFCOMPARISON_PARAM[] struDiffComparison;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //���²���ʱ������
        public struct NET_DVR_EVENT_SCHEDULE
        {
            public uint dwSize;//�ṹ���С
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime; /*����ʱ��*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayAlarmTime; /*���ղ���ʱ��*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�����������������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_TRIGGER_COND
        {
            public uint dwSize;//�ṹ���С
            public uint dwChannel;
            public ushort wPresetNo;//0-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�ֶ����»�����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUALTHERM_BASICPARAM
        {
            public uint dwSize;
            public ushort wDistance;//����(m)[0, 10000]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; //����
            public float fEmissivity;//������(������ ��ȷ��С�������λ)[0.01, 1.00](��������������������ı���)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_POINT_THERMOMETRY
        {
            public float fPointTemperature;/*����µ�ǰ�¶�, ���궨Ϊ0-��ʱ��Ч����ȷ��С�����һλ(-40-1000),��������+100��*10 */
            public NET_VCA_POINT struPoint;//��������꣨������궨����Ϊ���㡱��ʱ����Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_REGION_THERMOMETRY
        {
            public float fMaxTemperature;//����¶�,��ȷ��С�����һλ(-40-1000),��������+100��*10 */
            public float fMinTemperature;//����¶�,��ȷ��С�����һλ(-40-1000),��������+100��*10 */
            public float fAverageTemperature;//ƽ���¶�,��ȷ��С�����һλ(-40-1000),��������+100��*10 */
            public float fTemperatureDiff;//�²�,��ȷ��С�����һλ(-40-1000),��������+100��*10 */
            public NET_VCA_POLYGON struRegion;//�����ߣ�������궨����Ϊ���򡱻��ߡ��ߡ���ʱ����Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUALTHERM_RULE
        {
            public byte byRuleID;//����ID 0-��ʾ��Ч����1��ʼ ��list�ڲ��ж�������Ч�ԣ�
            public byte byEnable;//�Ƿ�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szRuleName;//��������
            public byte byRuleCalibType;//����궨���� 0-�㣬1-��2-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_SDK_POINT_THERMOMETRY struPointTherm;//����£����궨Ϊ0-��ʱ��Ч
            public NET_SDK_REGION_THERMOMETRY struRegionTherm; //������£����궨Ϊ1-��2-��ʱ��Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // �ֶ�����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_SDK_MANUAL_THERMOMETRY
        {
            public uint dwSize;//�ṹ���С
            public uint dwChannel;//ͨ����
            public uint dwRelativeTime; // ���ʱ�ֻ꣨����
            public uint dwAbsTime;      // ����ʱ�ֻ꣨����
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)
            public byte byDataType;//����״̬����:0-����У�1-��ʼ��2-������ֻ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_SDK_MANUALTHERM_RULE struRuleInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // �ֶ���ȡ�����¶���Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRYRULE_TEMPERATURE_INFO
        {
            public float fMaxTemperature;//����£���ȷ��С�����һλ
            public float fMinTemperature;//����£���ȷ��С�����һλ
            public float fAverageTemperature;//ƽ���£���ȷ��С�����һλ
            public NET_VCA_POINT struHighestPoint; //����¶�λ������
            public NET_VCA_POINT struLowestPoint; //����¶�λ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�ȳ������ܹ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMAL_INTELRULE_DISPLAY
        {
            public uint dwSize;//�ṹ���С
            /*
            fontSizeType:Ϊ�����С�������������ſ����ݸñ����Լ�Ԥ�����ڵĿ��ȶ�̬�ı�����Ĵ�С����ʽΪ�����屶��ֵ/8*(0.01*Ԥ�����ڿ���)
            ����������Ӧ���£�
            0~8���ʣ�С��
            1~12���ʣ���׼��
            2~16���ʣ���
            3~20���ʣ�����
            4~24���ʣ��ش�
            */
            public byte byFontSizeType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_RULESLINE_CFG struNormalRulesLineCfg;//����������������Բ���
            public NET_DVR_RULESLINE_CFG struAlertRulesLineCfg;//Ԥ��������������Բ���
            public NET_DVR_RULESLINE_CFG struAlarmRulesLineCfg;//����������������Բ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 640, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RULESLINE_CFG
        {
            public NET_DVR_RGB_COLOR struRGB;// RGB������R����ɫ����G����ɫ����B����ɫ�� ��Χ0-255
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RGB_COLOR
        {
            public byte byRed;        //RGB��ɫ�������еĺ�ɫ
            public byte byGreen;    //RGB��ɫ�������е���ɫ
            public byte byBlue;    //RGB��ɫ�������е���ɫ
            public byte byRes;        //����
        }

        //�ȳ�������㷨��汾��ȡ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMAL_ALGINFO
        {
            public uint dwSize;//�ṹ���С
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_THERMOMETRYALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sThermometryAlgName;//�����㷨��汾����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_SHIPSALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sShipsAlgName;//��ֻ�㷨��汾����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_FIRESALGNAME, ArraySubType = UnmanagedType.I1)]
            public char[] sFireAlgName;//������㷨��汾����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 768, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_BAREDATAOVERLAY_CFG
        {
            public uint dwSize;
            public byte byEnable;//ʹ��
            public byte byIntervalTime;// �ϴ���ʱ���������ã�1 2 3 4 5.��λΪ�룬Ĭ��Ϊ3��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 258, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V41
        {
            public uint dwHandleType;/*������ʽ,������ʽ��"��"���*/
            /*0x00: ����Ӧ*/
            /*0x01: �������Ͼ���*/
            /*0x02: ��������*/
            /*0x04: �ϴ�����*/
            /*0x08: �����������*/
            /*0x10: ����JPRGץͼ���ϴ�Email*/
            /*0x20: �������ⱨ��������*/
            /*0x40: �������ӵ�ͼ(Ŀǰֻ��PCNVR֧��)*/
            /*0x200: ץͼ���ϴ�FTP*/
            public uint dwMaxRelAlarmOutChanNum; //�����ı������ͨ������ֻ�������֧����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelAlarmOut; //��������ͨ��      
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;           //����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PRESETCHAN_INFO
        {
            public uint dwEnablePresetChan;    /*����Ԥ�õ��ͨ��*/
            public uint dwPresetPointNo;        /*����Ԥ�õ�ͨ����Ӧ��Ԥ�õ����, 0xfffffff��ʾ������Ԥ�õ㡣*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISECHAN_INFO
        {
            public uint dwEnableCruiseChan;    /*����Ѳ����ͨ��*/
            public uint dwCruiseNo;        /*Ѳ��ͨ����Ӧ��Ѳ�����, 0xfffffff��ʾ��Ч*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZTRACKCHAN_INFO
        {
            public uint dwEnablePtzTrackChan;    /*������̨�켣��ͨ��*/
            public uint dwPtzTrackNo;        /*��̨�켣ͨ����Ӧ�ı��, 0xfffffff��ʾ��Ч*/
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

        //�²��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_DIFF_ALARM
        {
            public uint dwSize;
            public uint dwChannel;//ͨ����
            public byte byAlarmID1;//����AlarmID1
            public byte byAlarmID2;//����AlarmID2
            public ushort wPresetNo; //Ԥ�õ��
            public byte byAlarmLevel;//0-Ԥ�� 1-����
            public byte byAlarmType;/*�������� 0-����¶� 1-����¶� 2-ƽ���¶�*/
            public byte byAlarmRule;//0-���ڣ�1-С��
            public byte byRuleCalibType;//����궨���� 0-�㣬1-��2��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPoint;//��������꣨������궨����Ϊ���ʱ����Ч�������±�0������AlarmID1�������±�1������AlarmID2.
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POLYGON[] struRegion;//���򣨵�����궨����Ϊ���ʱ����Ч�������±�0������AlarmID1�������±�1������AlarmID2.
            public float fRuleTemperatureDiff;/*���ù����²�,��ȷ��С�����һλ(-40-1000),��������+100��*/
            public float fCurTemperatureDiff;/*��ǰ�²�,��ȷ��С�����һλ(-40-1000),��������+100�� */
            public NET_PTZ_INFO struPtzInfo;//ptz������Ϣ
            public uint dwPicLen;//�ɼ���ͼƬ����
            public uint dwThermalPicLen;//�ȳ���ͼƬ����
            public uint dwThermalInfoLen;//�ȳ��񸽼���Ϣ����
            public IntPtr pPicBuff; ///�ɼ���ͼƬָ��
            public IntPtr pThermalPicBuff;// �ȳ���ͼƬָ��
            public IntPtr pThermalInfoBuff; //�ȳ��񸽼���Ϣָ��
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        //ʵʱ�¶ȼ�������ṹ
        public struct NET_DVR_REALTIME_THERMOMETRY_COND
        {
            public uint dwSize;
            public uint dwChan;
            public byte byRuleID; //����ID 0-������ȡȫ�����򣬾������ID��1��ʼ 
            /*
            1-��ʱģʽ���豸ÿ��һ���ϴ���������������ݵ�����¡�����º�ƽ���¶�ֵ���²�
            2-�²�ģʽ������һ������һ�������»�������»���ƽ���»����²�ֵ���²���ڵ���2���϶ȣ����ϴ�����¡�����º�ƽ���¶�ֵ�������ڵ���һ��Сʱ�²�ֵ��С��2���϶ȣ����ϴ�����¡�����¡�ƽ���º��²�ֵ
            */
            public byte byMode; //������ģʽ�� 0-������Ϊ�������豸����1-��ʱģʽ��2-�²�ģʽ

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
        }

        public struct NET_DVR_POINT_THERM_CFG
        {
            public float fTemperature;//��ǰ�¶�
            public NET_VCA_POINT struPoint;//��������꣨������궨����Ϊ���ʱ����Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
        }

        public struct NET_DVR_LINEPOLYGON_THERM_CFG
        {
            public float fMaxTemperature;//�����
            public float fMinTemperature;//�����
            public float fAverageTemperature;//ƽ����
            public float fTemperatureDiff;//�²�
            public NET_VCA_POLYGON struRegion;//���򣨵�����궨����Ϊ��/�ߵ�ʱ����Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
        }

        public struct NET_DVR_THERMOMETRY_UPLOAD
        {
            public uint dwSize;
            public uint dwRelativeTime;     // ���ʱ��
            public uint dwAbsTime;            // ����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public char[] szRuleName;//��������
            public byte byRuleID;//����ID��
            public byte byRuleCalibType;//����궨���� 0-�㣬1-��2-��
            public ushort wPresetNo; //Ԥ�õ��
            public NET_DVR_POINT_THERM_CFG struPointThermCfg;
            public NET_DVR_LINEPOLYGON_THERM_CFG struLinePolygonThermCfg;
            public byte byThermometryUnit;//���µ�λ: 0-���϶ȣ��棩��1-���϶ȣ��H����2-������(K)
            public byte byDataType;//����״̬����:0-����У�1-��ʼ��2-����
            public byte byRes1;
            public byte bySpecialPointThermType;
            public Single fCenterPointTemperature;
            public Single fHighestPointTemperature;
            public Single fLowestPointTemperature;
            public NET_VCA_POINT struHighestPoint;//�ߡ����������¶�λ�����꣨������궨����Ϊ�ߡ����ʱ����Ч��
            public NET_VCA_POINT struLowestPoint;//�ߡ����������¶�λ�����꣨������궨����Ϊ�ߡ����ʱ����Ч��

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����

            public void Init()
            {
                struPointThermCfg = new HCNetSDK.NET_DVR_POINT_THERM_CFG();
                struLinePolygonThermCfg = new HCNetSDK.NET_DVR_LINEPOLYGON_THERM_CFG();
            }
        }
        // ���ʱ��:һ���漰��ʱ�������紫�ݵĶ�����ʱ�䡢GMTʱ�䡣
        // ����ʱ�꣺��UTCʱ�䣬����ʱ��������1970��1��1��0ʱ0��0�뵽���ڵ��������������һ�㴫�ݵľ���ʱ�궼�������
        //  
        // ����һ����ǰ�����������time_tֵ1400252410 
        // ���ʱ��Ļ�Ӧ���ǣ�2014-05-16 23:00:10
        // ����ʱ��Ļ�Ӧ���ǣ�2014-05-16 15:00:10

        public const int NET_DVR_GET_FIGURE = 6640;

        public struct NET_DVR_GET_FIGURE_COND
        {
            public uint dwLength;         //�ṹ����
            public uint dwChannel;         //ͨ��
            public NET_DVR_TIME_V30 struTimePoint;    //ʱ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_FIGURE_INFO
        {
            public uint dwPicLen;     //ͼƬ����
            public IntPtr pPicBuf;     //ͼƬ����
        }

        public struct NET_DVR_PTZ_MANUALTRACE
        {
            public uint dwSize;
            public uint dwChannel;      // ���ʱ��
            public NET_VCA_POINT struPoint; //��λ����(��ʾ����)
            public byte byTrackType;    //�������ͣ��ֶ�����Ĭ����0�� 0�����Զ�ȡ֤(��ͨȡ֤) 1�����ٵ�·���� 2�����е�·���٣��ֶ�����ȡ֤��3����̬ȡ֤
            public byte byLinkageType;//��������: 0-�ֶ����� 1-����������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
            public NET_VCA_POINT struPointEnd;  //��λ����(��ʾ����)
            public NET_DVR_TIME_V30 struTime;   //�ֶ����ٶ�λ����ǰʱ��
            public uint dwSerialNo;         //���;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; //����
        }

        public struct NET_DVR_SMARTTRACKCFG
        {
            public uint dwSize;
            public byte byEnable;//����ʹ�� 0-��1-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
            public uint dwDuration;//����ʱ�䣺0--300�룬Ĭ��300��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
        }

        //��ȡ����ͨ����������
        public const int NET_DVR_GET_SINGLE_CHANNELINFO = 4360;

        //�����������
        public const int NET_DVR_GET_FACE_DETECT = 3352;
        public const int NET_DVR_SET_FACE_DETECT = 3353;

        public struct NET_DVR_CHANNEL_GROUP
        {
            public uint dwSize;
            public uint dwChannel; //ͨ����
            public uint dwGroup;  //��� 
            public byte byID;//�豸��������ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public uint dwPositionNo;//����λ��������,IPC��0��IPD�Ǵ�1��ʼ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }

        public struct NET_DVR_HANDLEEXCEPTION_V40
        {
            public uint dwHandleType;//�쳣����,�쳣������ʽ��"��"���   
            /*0x00: ����Ӧ*/
            /*0x01: �������Ͼ���*/
            /*0x02: ��������*/
            /*0x04: �ϴ�����*/
            /*0x08: �����������*/
            /*0x10: ����JPRGץͼ���ϴ�Email*/
            /*0x20: �������ⱨ��������*/
            /*0x40: �������ӵ�ͼ(Ŀǰֻ��PCNVR֧��)*/
            /*0x80: ��������¼��(Ŀǰֻ��PCNVR֧��) */
            /*0x100: ����������̨Ԥ�õ� (Ŀǰֻ��PCNVR֧��)*/
            /*0x200: ץͼ���ϴ�FTP*/
            /*0x400: �齻��� ���� �۽�ģʽ���ṩ�������ԭ���豸�Զ���ɣ�IPC5.1.0*/
            /*0x800: PTZ��������(�������Ŀ��)*/
            /*0x1000:ץͼ�ϴ�����*/
            public uint dwMaxRelAlarmOutChanNum;//�����ı������ͨ������ֻ�������֧������
            public uint dwRelAlarmOutChanNum;//�����ı������ͨ���� ʵ��֧����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelAlarmOut;//��������ͨ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;//����
        }

        public struct NET_DVR_DETECT_FACE
        {
            public uint dwSize;
            public byte byEnableDetectFace;//�Ƿ����� 0�������ã� 1������
            public byte byDetectSensitive;//������ 10���ȼ� 1��10
            public byte byEnableDisplay;/*�����ƶ���������ʾ��0-��1-��*/
            public byte byRes;
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType;  /*������ʽ*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_DAYS * CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;  /*����ʱ��*/
            public uint dwMaxRelRecordChanNum;//����������¼��ͨ�� ����ֻ�������֧������
            public uint dwRelRecordChanNum;//����������¼��ͨ���� ʵ��֧����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//����������¼��ͨ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;  /*���ղ���ʱ��*/
            public ushort wDuration;//��������ʱ�� 5s ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes1;//����
        }

        public const int MAX_FACE_PIC_NUM = 30;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_DETECTION
        {
            public uint dwSize;
            public uint dwRelativeTime; //���ʱ��
            public uint dwAbsTime; //����ʱ��
            public uint dwBackgroundPicLen; //����ͼ�ĳ��ȣ�Ϊ0��ʾû��ͼƬ������0��ʾ��ͼƬ
            public NET_VCA_DEV_INFO struDevInfo;   //ǰ���豸��Ϣ
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HCNetSDK.MAX_FACE_PIC_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_RECT[] struFacePic;   //������ͼ����
            public byte byFacePicNum;//��ͼ����
            public byte byRes1;
            public ushort wDevInfoIvmsChannelEx;//��NET_VCA_DEV_INFO���byIvmsChannel������ͬ���ܱ�ʾ�����ֵ���Ͽͻ�����byIvmsChannel�ܼ������ݣ��������255���¿ͻ��˰汾��ʹ��wDevInfoIvmsChannelEx��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 252, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
            public IntPtr pBackgroundPicpBuffer;//����ͼ��ͼƬ����
        }



        public const int NET_DVR_GET_PDC_RESULT = 5089;//���������ݲ�ѯ 
        public const int NET_DVR_REMOVE_FLASHSTORAGE = 3756;    //���������������
        public const int NET_DVR_GET_PDC_RULECFG_V42 = 3405;  //����������ͳ�ƹ���(��չ)
        public const int NET_DVR_SET_PDC_RULECFG_V42 = 3406;  //��ȡ������ͳ�ƹ���(��չ)


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_QUERY_COND
        {
            public uint dwSize;
            public uint dwChannel; //ͨ����
            public NET_DVR_TIME_EX struStartTime; //��ʼʱ��
            public NET_DVR_TIME_EX struEndTime; //����ʱ��
            public byte byReportType; //0-��Чֵ��1-�ձ�����2-�ܱ�����3-�±�����4-�걨��
            public byte byEnableProgramStatistics; //�Ƿ񰴽�Ŀͳ�ƣ�0-��1-��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPlayScheduleNo; //����Ŀͳ��ʱ�������ճ̺�
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PROGRAM_INFO
        {
            public uint dwProgramNo; //��Ŀ���
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sProgramName; //��Ŀ����
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_RESULT
        {
            public uint dwSize;
            public NET_DVR_TIME_EX struStartTime;/*��ʼʱ��*/
            public NET_DVR_TIME_EX struEndTime;/*����ʱ��*/
            public uint dwEnterNum;   //��������
            public uint dwLeaveNum;  //�뿪����
            public NET_DVR_PROGRAM_INFO struProgramInfo;        //��Ŀ��Ϣ
            public uint dwPeoplePassing;        //��������
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 200, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_ALRAM_INFO
        {
            public uint dwSize;          // PDC�����������ϴ��ṹ���С
            public byte byMode;          // 0 ��֡ͳ�ƽ�� 1��Сʱ���ͳ�ƽ��  
            public byte byChannel;       // �����ϴ�ͨ����
            /********* IPC5.1.7 �������� Begin 2014-03-21***********/
            public byte bySmart;         //רҵ���ܷ���0��Smart ���� 1
            public byte byRes1;          // �����ֽ�    
            /********* IPC5.1.7 �������� End 2014-03-21***********/
            public NET_VCA_DEV_INFO struDevInfo;             //ǰ���豸��Ϣ
            public NET_MODE_PARAM uStatModeParam;
            public uint dwLeaveNum;        // �뿪����
            public uint dwEnterNum;        // ��������
            public byte byBrokenNetHttp;     //����������־λ��0-�����ش����ݣ�1-�ش�����
            public byte byRes3;
            public short wDevInfoIvmsChannelEx;     //��NET_VCA_DEV_INFO���byIvmsChannel������ͬ���ܱ�ʾ�����ֵ���Ͽͻ�����byIvmsChannel�ܼ������ݣ��������255���¿ͻ��˰汾��ʹ��wDevInfoIvmsChannelEx
            public uint dwPassingNum;        // ��������������������ǻ�û�д������롢�뿪��������
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        }

        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_MODE_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.U1)]
            [FieldOffsetAttribute(0)]
            public byte[] byLen;//����
            [FieldOffsetAttribute(0)]
            public NET_MODE_FRAME struStatFrame;
            [FieldOffsetAttribute(0)]
            public NET_MODE_START_TIME struStatTime;
        }
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_FRAME
        {
            public uint dwRelativeTime;     // ���ʱ��
            public uint dwAbsTime;          // ����ʱ��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        }


        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_START_TIME
        {
            public NET_DVR_TIME tmStart; // ͳ����ʼʱ�� 
            public NET_DVR_TIME tmEnd;  //  ͳ�ƽ���ʱ�� 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        }

        public struct NET_DVR_FLASHSTORAGE_REMOVE
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byPDCRemoveEnable;//�����������ʹ�� 0-�������1-���
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�;
        }

        public struct NET_DVR_AI_ALGORITHM_MODEL
        {
            public uint dwSize;
            public uint dwDescribeLength;
            public IntPtr pDescribeBuffer;//�����������ʹ�� 0-�������1-���
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�;
        }

        public struct NET_DVR_STD_CONTROL
        {
            public IntPtr lpCondBuffer;    //[in]��������(���ָ�ʽ),����ͨ���ŵ�.����ΪNULL
            public uint dwCondSize;        //[in] dwCondSizeָ����ڴ��С
            public IntPtr lpStatusBuffer;    //[out]���ص�״̬����(XML��ʽ),��ȡ�ɹ�ʱ���ḳֵ,�������Ҫ,������NULL
            public uint dwStatusSize;    //[in] lpStatusBufferָ����ڴ��С
            public IntPtr lpXmlBuffer;    //[in/out]byDataType = 1ʱ��Ч,xml��ʽ����
            public uint dwXmlSize;      //[in/out]lpXmlBufferָ����ڴ��С,��ȡʱͬʱ��Ϊ����������������ȡ�ɹ�����޸Ļ�ʵ�ʳ��ȣ�����ʱ��ʾʵ�ʳ��ȣ������������ڴ��С
            public byte byDataType;     //[in]����/�����������,0-ʹ�ýṹ������lpCondBuffer��Ч,1-ʹ��XML����lpXmlBuffer��Ч
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 55, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        };

        public const int NET_DVR_GET_TRAFFIC_CAP = 6630;
        public const int UPLOAD_VEHICLE_BLOCKALLOWLIST_FILE = 13; 
        public const int UPLOAD_THERMOMETRIC_FILE = 22; // �ϴ����±궨�ļ�
        public const int UPLOAD_AI_ALGORITHM_MODEL = 52; // AI����ƽ̨�������ϴ��㷨ģ�͵��豸
        public const int UPLOAD_FD_DATA = 35;//�����������ݵ�������
        public const int UPLOAD_FACE_DATA = 36;//��������ͼƬ���ݵ�������
        public const int IMPORT_DATA_TO_FACELIB = 39;//�����������ݣ�����ͼƬ+ͼƬ������Ϣ ���豸�����⣩

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
        public const int NET_SDK_DOWNLOAD_THERMOMETRIC_FILE = 15; // ���ز��±궨�ļ�


        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartDownload(int lUserID, uint dwDownloadType, IntPtr lpInBuffer, uint dwInBufferSize, string sFileName);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadState(int lFileHandle, IntPtr pProgress);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadStateInfo(int lFileHandle, IntPtr pStatusInfo);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDownload(int lFileHandle);

        public const int NET_DVR_GET_GUARDCFG = 3134;//��ȡ����ʶ����ƻ� 
        public const int NET_DVR_SET_GUARDCFG = 3135;//���ó���ʶ����ƻ�

        //ץ�Ĵ���ģʽ֧�ֹ�������ʱ��κ��ϴ���������
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_COND
        {
            public uint dwSize;
            public uint dwChannel; //ͨ����
            /*
            0��ʾ��Ч�� 
            1��ʾ���� ץ��VIAģʽ����Ƶ����ץ�� IPCʹ�ã�;
            2 ��ʾ���� ץ�� HVT ģʽ ���쿨IPCʹ�ã�
            */
            public byte byRelateType;
            public byte byGroupNo;  //���
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
            public byte[] byRes;
        };

        public struct NET_DVR_TIME_DETECTION
        {
            public NET_DVR_SCHEDTIME struSchedTime; //ʱ��
            public byte byDetSceneID;//��ⳡ����[1,4],IPCĬ����0
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
            public byte[] byRes;
        };

        //ץ�Ĵ���ģʽ֧�ֹ�������ʱ��κ��ϴ�����
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_CFG
        {
            public uint dwSize;
            //����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_TIME_DETECTION[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //�쳣������ʽ
            public uint dwMaxRelRecordChanNum;  //����������¼��ͨ�� ����ֻ�������֧������
            public uint dwRelRecordChanNum;     //������ʵ�ʴ�����¼��ͨ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;    //����������¼��ͨ��(0xffff ffff��ʾ������Ч��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            NET_DVR_TIME_DETECTION[] struHolidayTime; //���ղ���ʱ�� 
            public byte byDirection;//��������0-������1-ȫ����2-����3-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 87, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        };

        public const int NET_DVR_GET_EVENT_TRIGGERS_CAPABILITIES = 3501;

        #region  ��ȡ��ʼ�������ļ�
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            //����ļ��Ƿ����
            if (File.Exists(iniFilePath))
            {
                //                 StringBuilder temp = new StringBuilder(1024);
                //                 //��ȡ�����ļ���ĳ��key��ֵ
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
                //�������򷵻ؿ�
                return String.Empty;
            }
        }
        #endregion


        //����ͳ�Ʒ���ṹ��
        public struct NET_DVR_PDC_ENTER_DIRECTION
        {
            public NET_VCA_POINT struStartPoint; //����ͳ�Ʒ�����ʼ��
            public NET_VCA_POINT struEndPoint;    // ����ͳ�Ʒ�������� 
        }

        public struct NET_DVR_PDC_RULE_CFG_V41
        {
            public uint dwSize;              //�ṹ��С
            public byte byEnable;             // �Ƿ񼤻����;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;    //�����ֽ�
            public NET_VCA_POLYGON struPolygon;            // �����
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection;    // �������뷽��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            public NET_DVR_TIME_EX struDayStartTime; //���쿪ʼʱ�䣬ʱ������Ч
            public NET_DVR_TIME_EX struNightStartTime; //ҹ����ʼʱ�䣬ʱ������Ч
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ�
        }

        public struct NET_DVR_PDC_RULE_COND
        {
            public uint dwSize; //�ṹ��С
            public uint dwChannel; //ͨ����
            public uint dwID; //����ID����������ೡ����������ϰ汾SDK�������豸ʱ�������ֽ�Ϊ0���������������ֽ�Ϊ0ʱ��ҲĬ��Ϊ����1 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ� 
        }

        //�߽ṹ
        public struct NET_VCA_LINE
        {
            public NET_VCA_POINT struStart;    //��� 
            public NET_VCA_POINT struEnd;      //�յ�
        }


        //���ýṹ
        public struct NET_DVR_PDC_RULE_CFG_V42
        {
            public uint dwSize; //�ṹ��С
            public byte byEnable; //�Ƿ񼤻����;
            public byte byOSDEnable;//����ͳ��OSD��ʾ�Ƿ�����,  0-��0-�ޣ���1-�ǣ�0-������뿪���� 2-���룬 3-�뿪   �������豸�Ĳ�����OSD���Ӷ�Ӧ���ޣ������ö�Ӧ������/�뿪����
            public byte byCurDetectType;//��ǰ����������ͣ�0-����Σ�1-�����
            public byte byInterferenceSuppression; //�������ƣ���λ��ʾ,0-δ��ѡ,1-��ѡ��bit0-��Ӱ��bit1-�ǻ���bit2-�Ƴ�
            NET_VCA_POINT struOSDPoint;//����ͳ����ʾOSD��ʾ���Ͻ�����
            //��������������ϴ����ڣ�0-15��1-1��2-5��3-10��4-20��5-30��6-60����λ������
            public byte byDataUploadCycle;
            //ÿ���ϴ�����ʹ�ܣ�0-�رգ�1-������
            public byte bySECUploadEnable;
            public byte byEmailDayReport;//�����ձ���ʹ�� 0-�رգ�1-����
            public byte byEmailWeekReport;//�����ܱ���ʹ�� 0-�رգ�1-����
            public byte byEmailMonthReport;//�����±���ʹ��0-�رգ�1-����
            public byte byEmailYearReport;//�����걨��ʹ��0-�رգ�1-����
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;    //�����ֽ� 
            public NET_VCA_POLYGON struPolygon; // �����
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection; // �������뷽��
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;//����ʱ��
            public NET_DVR_TIME_EX struDayStartTime; //���쿪ʼʱ�䣬ʱ������Ч
            public NET_DVR_TIME_EX struNightStartTime; //ҹ����ʼʱ�䣬ʱ������Ч
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType; /*������ʽ ��֧���ϴ�����*/
            public byte byDetecteSensitivity;//Ŀ���������ȣ���Χ1-100��Ĭ��50
            public byte byGenerateSpeedSpace;//Ŀ�������ٶȣ����򣩣���Χ1-100��Ĭ��50
            public byte byGenerateSpeedTime;// Ŀ�������ٶȣ�ʱ�򣩣���Χ1-100��Ĭ��50
            public byte byCountSpeed;// �����ٶȣ���Χ1-100��Ĭ��50
            public byte byDetecteType;// Ŀ�������ͣ�0-�Զ���1-��ͷ��2-ͷ�磬Ĭ��0-�Զ����Զ�ģʽ��DSP�����㷨�Ĳ������ø��㷨��
            public byte byTargetSizeCorrect;//Ŀ��ߴ���������Χ1-100��Ĭ��50
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes3;    //�����ֽ� 
            public NET_VCA_LINE struLine;//�����
            public byte byHeightFilterEnable;//�߶ȹ����Ƿ�����0-�رգ�1-����
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes4;    //�����ֽ� 
            float fHeightFilter;//���˸߶ȣ���λ�����ף�Ĭ��ֵ��120���ף���Χ��40-200���ס�byHeightFilterEnableΪ1�ǲ���Ч
            public byte byCalibrateType;//�궨���ͣ�0-δ���б궨��1-�Զ��궨��2-�ֶ��궨
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes6;    //�����ֽ� 
            float fTiltAngle;//������,��λ���ȣ�������Ĭ��ֵ��0�������Ƿ�Χ��0-180��,ֻ��
            float fHeelAngle;//��б��,��λ���ȣ���б��Ĭ��ֵ��0����б�Ƿ�Χ��-90-90��,ֻ��
            float fHeight;//�߶ȣ��߶ȵ�λ�����ף��߶�Ĭ��ֵ300���ף��߶ȷ�Χ��200-500����,��byCalibrateTypeΪ2ʱ������Ч������ʱֻ��
            NET_VCA_POLYGON struCountPolygon;//��������,ֻ������
            NET_VCA_POLYGON struAutoCalibPolygon;//�궨���򣬵�byCalibrateTypeΪ1ʱ��Ч
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ� 
        }

        public const int NET_DVR_SET_POS_INFO_OVERLAY = 3913;//����Pos��Ϣ�������ӿ���
        public const int NET_DVR_GET_POS_INFO_OVERLAY = 3914;//��ȡPos��Ϣ�������ӿ���
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_POS_INFO_OVERLAY
        {
            public uint dwSize; //�ṹ��С
            public byte byPosInfoOverlayEnable;// Pos��Ϣ�������ӿ��ƣ�0-�����ӣ�1-����
            public byte byOverlayType;//0-���ӽ�����뿪��1-���ӽ��롢�뿪��PASS��ID���߶�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;    //�����ֽ� 
        }
        //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]

        //��֡ͳ�ƽ��ʱʹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        // [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct UNION_STATFRAME
        {
            public uint dwRelativeTime;     // ���ʱ��
            public uint dwAbsTime;          // ����ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct UNION_STATTIME
        {
            public NET_DVR_TIME tmStart; // ͳ����ʼʱ�� 
            public NET_DVR_TIME tmEnd;  //  ͳ�ƽ���ʱ�� 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //����֤��Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ID_CARD_INFO
        {
            public uint dwSize;        //�ṹ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName;   //����
            public NET_DVR_DATE struBirth; //��������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAddr;  //סַ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIDNum;   //����֤����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ID_ISSUING_AUTHORITY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIssuingAuthority;  //ǩ������
            public NET_DVR_DATE struStartDate;  //��Ч��ʼ����
            public NET_DVR_DATE struEndDate;  //��Ч��ֹ����
            public byte byTermOfValidity;  //�Ƿ�����Ч�� 0-��1-�ǣ���Ч��ֹ������Ч��
            public byte bySex;  //�Ա�1-�У�2-Ů
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 101, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //����֤��Ϣ����
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ID_CARD_INFO_ALARM
        {
            public uint dwSize;        //�ṹ����
            public NET_DVR_ID_CARD_INFO struIDCardCfg;//����֤��Ϣ
            public uint dwMajor; //���������ͣ��ο��궨��
            public uint dwMinor; //���������ͣ��ο��궨��
            public NET_DVR_TIME_V30 struSwipeTime; //ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetUser;//����������û���
            public NET_DVR_IPADDR struRemoteHostAddr;//Զ��������ַ
            public uint dwCardReaderNo; //��������ţ�Ϊ0��Ч
            public uint dwDoorNo; //�ű�ţ�Ϊ0��Ч
            public uint dwPicDataLen;   //ͼƬ���ݴ�С����Ϊ0�Ǳ�ʾ���������
            public IntPtr pPicData;
            public byte byCardType; 
            public byte byDeviceNo;                             // �豸��ţ�Ϊ0ʱ��Ч����Ч��Χ1-255��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwFingerPrintDataLen;                  // ָ�����ݴ�С����Ϊ0�Ǳ�ʾ���������
            public IntPtr pFingerPrintData;
            public uint dwCapturePicDataLen;                   // ץ��ͼƬ���ݴ�С����Ϊ0�Ǳ�ʾ���������
            public IntPtr pCapturePicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 188, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        //�����¼�����
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_VCA_EVENT_UNION
        {
            [FieldOffsetAttribute(0)]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            public uint[] uLen;//����

            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TRAVERSE_PLANE struTraversePlane;//��Խ��������� 
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_AREA struArea;//����/�뿪�������
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_INTRUSION struIntrusion;//���ֲ���
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LOITER struLoiter;//�ǻ�����
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TAKE_LEFT struTakeTeft;//����/�������
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_PARKING struParking;//ͣ������
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_RUN struRun;//���ܲ���
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_HIGH_DENSITY struHighDensity;//��Ա�ۼ�����  
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_VIOLENT_MOTION struViolentMotion;    //�����˶�
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_REACH_HIGHT struReachHight;      //�ʸ�
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_GET_UP struGetUp;           //��
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LEFT struLeft;            //��Ʒ����
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TAKE struTake;            // ��Ʒ��ȡ
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_HUMAN_ENTER struHumanEnter;      //��Ա����
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_OVER_TIME struOvertime;        //������ʱ
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_STICK_UP struStickUp;//��ֽ��
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_SCANNER struScanner;//����������
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_LEAVE_POSITION struLeavePos;        //��ڲ���
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TRAIL struTrail;           //β�����
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_FALL_DOWN struFallDown;        //���ز���
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_AUDIO_ABNORMAL struAudioAbnormal;   //��ǿͻ��
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_ADV_REACH_HEIGHT struReachHeight;     //�����ʸ߲���
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_TOILET_TARRY struToiletTarry;     //��޳�ʱ����
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_YARD_TARRY struYardTarry;       //�ŷ糡��������
            //[FieldOffsetAttribute(0)]
            //public NET_VCA_ADV_TRAVERSE_PLANE struAdvTraversePlane;//���߾��������            
        }
        //�򻯵Ĺ�����Ϣ, ��������Ļ�����Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_INFO
        {
            public byte byRuleID;//����ID,0-7
            public byte byRes;//����
            public ushort wEventTypeEx;   //��Ϊ�¼�������չ�����ڴ����ֶ�dwEventType���ο�VCA_RULE_EVENT_TYPE_EX
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;//��������
            public VCA_EVENT_TYPE dwEventType;//�����¼�����
            public NET_VCA_EVENT_UNION uEventParam;//�¼�����
        }

        //��Ϊ��������ϱ��ṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RULE_ALARM
        {
            public uint dwSize;//�ṹ����
            public uint dwRelativeTime;//���ʱ��
            public uint dwAbsTime;//����ʱ��
            public NET_VCA_RULE_INFO struRuleInfo;//�¼�������Ϣ
            public NET_VCA_TARGET_INFO struTargetInfo;//����Ŀ����Ϣ
            public NET_VCA_DEV_INFO struDevInfo;//ǰ���豸��Ϣ
            public uint dwPicDataLen;//����ͼƬ�ĳ��� Ϊ0��ʾû��ͼƬ������0��ʾ�ýṹ�������ͼƬ����*/
            public byte byPicType;        //  0-��ͨͼƬ 1-�Ա�ͼƬ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;        // �����ֽ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;//����������Ϊ0
            public IntPtr pImage;//ָ��ͼƬ��ָ��
        }

        //��Ϊ��������DSP��Ϣ���ӽṹ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;//�����Ƿ����Ŀ��
            public byte byDspAddRule;//�����Ƿ���ӹ���
            public byte byDspPicAddTarget;//ץͼ�Ƿ����Ŀ��
            public byte byDspPicAddRule;//ץͼ�Ƿ���ӹ���
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
            public string sStreamID;    ///< ��ID������С��32���ֽ�
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sMonitorIP;  ///< ��ص�ip
            public uint dwChanIndex;  ///< ��ص�ͨ����  
            public uint dwWidth;  ///< ͼ�����
            public uint dwHeight;  ///< ͼ��߶�
            public NET_DVR_TIME struCheckTime;  ///< ���ʱ��(�ϲ����ں�ʱ���ֶ�)����ʽ��2012-08-06 13:00:00
            public byte byResult;  ///0-δ��� 1-���� 2-�쳣 3-��¼ʧ�� 4-ȡ���쳣
            public byte bySignalResult; ///< ��Ƶ��ʧ����� 0-δ��� 1-���� 2-�쳣
            public byte byBlurResult;  ///< ͼ��ģ���������0-δ��� 1-���� 2-�쳣
            public byte byLumaResult;  ///< ͼ������������0-δ��� 1-���� 2-�쳣
            public byte byChromaResult;  ///< ƫɫ�������0-δ��� 1-���� 2-�쳣
            public byte bySnowResult;  ///< �������ż������0-δ��� 1-���� 2-�쳣
            public byte byStreakResult;  ///< ���Ƹ��ż������0-δ��� 1-���� 2-�쳣
            public byte byFreezeResult;  ///< ���涳��������0-δ��� 1-���� 2-�쳣
            public byte byPTZResult;  ///< ��̨�������0-δ��� 1-���� 2-�쳣
            public byte byContrastResult;     //�Աȶ��쳣�������0-δ��⣬1-������2-�쳣
            public byte byMonoResult;         //�ڰ�ͼ��������0-δ��⣬1-������2-�쳣
            public byte byShakeResult;        //��Ƶ�����������0-δ��⣬1-������2-�쳣
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string sSNapShotURL;    
            public byte byFlashResult;        //��Ƶ���������0-δ��⣬1-������2-�쳣
            public byte byCoverResult;        //��Ƶ�ڵ��������0-δ��⣬1-������2-�쳣
            public byte bySceneResult;        //��������������0-δ��⣬1-������2-�쳣
            public byte byDarkResult;         //ͼ������������0-δ��⣬1-������2-�쳣
            public byte byStreamType;        //�������ͣ�0-��Ч��1-δ֪��2-�������ͣ�3-�ǹ�������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 59, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //����ץ����Ϣ
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_INFO_ALARM
        {
            public uint dwRelativeTime;     // ���ʱ��
            public uint dwAbsTime;            // ����ʱ��
            public uint dwSnapFacePicID;       //ץ������ͼID
            public uint dwSnapFacePicLen;        //ץ��������ͼ�ĳ��ȣ�Ϊ0��ʾû��ͼƬ������0��ʾ��ͼƬ
            public NET_VCA_DEV_INFO struDevInfo;   //ǰ���豸��Ϣ
            public byte byFaceScore;        //�������֣�ָ������ͼ������������,0-100
            public byte bySex;//�Ա�0-δ֪��1-�У�2-Ů
            public byte byGlasses;//�Ƿ���۾���0-δ֪��1-�ǣ�2-��
            /*
             * ʶ������������η�Χ[byAge-byAgeDeviation,byAge+byAgeDeviation]
             */
            public byte byAge;//����
            public byte byAgeDeviation;//�������ֵ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;              // �����ֽ�
            public uint dwUIDLen; // �ϴ������ı�ʶ����
            public IntPtr pUIDBuffer;  //��ʶָ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;              // �����ֽ�
            public IntPtr pBuffer1;  //ץ��������ͼ��ͼƬ����
        }
        //��������ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AREAINFOCFG
        {
            public ushort wNationalityID;//����
            public ushort wProvinceID;//ʡ
            public ushort wCityID;//��
            public ushort wCountyID;//��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
        }
        //��Ա��Ϣ�ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_ATTRIBUTE
        {
            public byte bySex;//�Ա�0-�У�1-Ů
            public byte byCertificateType;//֤�����ͣ�0-����֤��1-����֤
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HUMAN_BIRTHDATE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBirthDate;//�������£��磺201106
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName; //����
            public NET_DVR_AREAINFOCFG struNativePlace;//�������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCertificateNumber; //֤����
            public uint dwPersonInfoExtendLen;// ��Ա��ǩ��Ϣ��չ����
            public IntPtr pPersonInfoExtend;  //��Ա��ǩ��Ϣ��չ��Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//����
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_BLOCKLIST_INFO
        {
            public uint dwSize;//�ṹ��С
            public uint dwRegisterID;//����ע��ID�ţ�ֻ����
            public uint dwGroupNo;//�����
            public byte byType;
            public byte byLevel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;//����
            public NET_VCA_HUMAN_ATTRIBUTE struAttribute;//��Ա��Ϣ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemark;//��ע��Ϣ
            public uint dwFDDescriptionLen;//�������������ݳ���
            public IntPtr pFDDescriptionBuffer;//��������������ָ��
            public uint dwFCAdditionInfoLen;//ץ�Ŀ⸽����Ϣ����
            public IntPtr pFCAdditionInfoBuffer;//ץ�Ŀ⸽����Ϣ����ָ�루FCAdditionInfo�а������PTZ���꣩
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;//����
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_BLOCKLIST_INFO_ALARM
        {
            public NET_VCA_BLOCKLIST_INFO struBlockListInfo;
            public uint dwBlockListPicLen;       
            public uint dwFDIDLen;// ������ID����
            public IntPtr pFDID;  //������Idָ��
            public uint dwPIDLen;// ������ͼƬID����
            public IntPtr pPID;  //������ͼƬIDָ��
            public ushort wThresholdValue; //��������ֵ[0,100]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;//����
            public IntPtr pBuffer1;//ָ��ͼƬ��ָ��
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_MATCH_ALARM
        {
            public uint dwSize;             // �ṹ��С
            public float fSimilarity; //���ƶȣ�[0.001,1]
            public NET_VCA_FACESNAP_INFO_ALARM struSnapInfo; //ץ����Ϣ
            public NET_VCA_BLOCKLIST_INFO_ALARM struBlockListInfo; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sStorageIP;        //�洢����IP��ַ
            public ushort wStoragePort;            //�洢����˿ں�
            public byte byMatchPicNum; //ƥ��ͼƬ��������0-���������豸���ֵĬ��0�����豸���ֵΪ0ʱ��ʾ����û��ƥ���ͼƬ��Ϣ��
            public byte byPicTransType;//ͼƬ���ݴ��䷽ʽ: 0-�����ƣ�1-url
            public uint dwSnapPicLen;//�豸ʶ��ץ��ͼƬ����
            public IntPtr pSnapPicBuffer;//�豸ʶ��ץ��ͼƬָ��
            public NET_VCA_RECT struRegion;//Ŀ��߽���豸ʶ��ץ��ͼƬ�У�������ͼ����
            public uint dwModelDataLen;//��ģ���ݳ���
            public IntPtr pModelDataBuffer;// ��ģ����ָ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  // �����ֽ�
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CID_ALARM
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CID_CODE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCIDCode;    //CID�¼���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCIDDescribe;    //CID�¼���
            public NET_DVR_TIME_EX struTriggerTime;            //����������ʱ���
            public NET_DVR_TIME_EX struUploadTime;                //�ϴ�������ʱ���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACCOUNTNUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sCenterAccount;    //�����ʺ�
            public byte byReportType;                    //������NET_DVR_ALARMHOST_REPORT_TYPE
            public byte byUserType;                        //�û����ͣ�0-�����û� 1-�����û�,2-�ֻ��û�,3-ϵͳ�û�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;        //�����û��û���
            public ushort wKeyUserNo;                        //�����û���    0xFFFF��ʾ��Ч
            public byte byKeypadNo;                        //���̺�        0xFF��ʾ��Ч
            public byte bySubSysNo;                        //��ϵͳ��        0xFF��ʾ��Ч
            public ushort wDefenceNo;                        //������        0xFFFF��ʾ��Ч
            public byte byVideoChanNo;                    //��Ƶͨ����    0xFF��ʾ��Ч
            public byte byDiskNo;                        //Ӳ�̺�        0xFF��ʾ��Ч
            public ushort wModuleAddr;                    //ģ���ַ        0xFFFF��ʾ��Ч
            public byte byCenterType;                    //0-��Ч, 1-�����˺�(����6),2-��չ�������˺�(����9)
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACCOUNTNUM_LEN_32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCenterAccountV40;    //�����˺�V40,ʹ�ô��ֶ�ʱsCenterAccount��Ч
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 28, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //��Ϊ�����¼�������չ
        public enum VCA_RULE_EVENT_TYPE_EX : ushort
        {
            ENUM_VCA_EVENT_TRAVERSE_PLANE = 1,   //��Խ������
            ENUM_VCA_EVENT_ENTER_AREA = 2,   //Ŀ���������,֧���������
            ENUM_VCA_EVENT_EXIT_AREA = 3,   //Ŀ���뿪����,֧���������
            ENUM_VCA_EVENT_INTRUSION = 4,   //�ܽ�����,֧���������
            ENUM_VCA_EVENT_LOITER = 5,   //�ǻ�,֧���������
            ENUM_VCA_EVENT_LEFT_TAKE = 6,   //��Ʒ������ȡ,֧���������
            ENUM_VCA_EVENT_PARKING = 7,   //ͣ��,֧���������
            ENUM_VCA_EVENT_RUN = 8,   //�����ƶ�,֧���������
            ENUM_VCA_EVENT_HIGH_DENSITY = 9,   //��������Ա�ۼ�,֧���������
            ENUM_VCA_EVENT_VIOLENT_MOTION = 10,  //�����˶����
            ENUM_VCA_EVENT_REACH_HIGHT = 11,  //�ʸ߼��
            ENUM_VCA_EVENT_GET_UP = 12,  //�������
            ENUM_VCA_EVENT_LEFT = 13,  //��Ʒ����
            ENUM_VCA_EVENT_TAKE = 14,  //��Ʒ��ȡ
            ENUM_VCA_EVENT_LEAVE_POSITION = 15,  //���
            ENUM_VCA_EVENT_TRAIL = 16,  //β��
            ENUM_VCA_EVENT_KEY_PERSON_GET_UP = 17,  //�ص���Ա�������
            ENUM_VCA_EVENT_FALL_DOWN = 20,  //���ؼ��
            ENUM_VCA_EVENT_AUDIO_ABNORMAL = 21,  //��ǿͻ����
            ENUM_VCA_EVENT_ADV_REACH_HEIGHT = 22,  //�����ʸ�
            ENUM_VCA_EVENT_TOILET_TARRY = 23,  //��޳�ʱ
            ENUM_VCA_EVENT_YARD_TARRY = 24,  //�ŷ糡����
            ENUM_VCA_EVENT_ADV_TRAVERSE_PLANE = 25,  //���߾�����
            ENUM_VCA_EVENT_HUMAN_ENTER = 29,  //�˿���ATM,ֻ��ATM_PANELģʽ��֧��   
            ENUM_VCA_EVENT_OVER_TIME = 30,  //������ʱ,ֻ��ATM_PANELģʽ��֧��
            ENUM_VCA_EVENT_STICK_UP = 31,  //��ֽ��,֧���������
            ENUM_VCA_EVENT_INSTALL_SCANNER = 32   //��װ������,֧���������
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TRAVERSE_PLANE
        {
            public NET_VCA_LINE struPlaneBottom;//������ױ�
            public VCA_CROSS_DIRECTION dwCrossDirection;//��Խ����: 0-˫��1-�����ң�2-���ҵ���
            public byte bySensitivity;//�����Ȳ�������Χ[1,5]
            public byte byPlaneHeight;//������߶�
            /*
            ���Ŀ�꣬��֧�ֶ�ѡ�����嶨��Ϊ��
            0~����Ŀ�꣨��ʾ���������Ŀ�꣬����Ŀ�궼�����м�⣩
            0x01 ~ �ˣ�
            0x02 ~ ����
            */
            public byte byDetectionTarget;
            public byte byPriority;//���ȼ�,0~��,1~��,2~��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new NET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }
        //����/�뿪�������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_AREA
        {
            public NET_VCA_POLYGON struRegion;//����Χ
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�����������������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SMART_REGION_COND
        {
            public uint dwSize;
            public uint dwChannel;//ͨ����
            public uint dwRegion;//����ID��
        }

        //���������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGION_ENTRANCE_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//�Ƿ�ʹ��: 0-�� , 1-��
            public byte byEnableHumanMisinfoFilter;//��������ȥ��   0-������ , 1-����
            public byte byEnableVehicleMisinfoFilter;//���ó���ȥ��   0-������ , 1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_REGIONENTRANCE_REGION[] struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //��������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGIONENTRANCE_REGION
        {
            public NET_VCA_POLYGON struRegion;//����Χ
            public byte bySensitivity;//�����Ȳ�������Χ[1-100]
            /*
           ���Ŀ�꣬��֧�ֶ�ѡ�����嶨��Ϊ��
           0~����Ŀ�꣨��ʾ���������Ŀ�꣬����Ŀ�궼�����м�⣩
           0x01 ~ �ˣ�
           0x02 ~ ����
           */
            public byte byDetectionTarget;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //�뿪�����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGION_EXITING_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//�Ƿ�ʹ��: 0-��, 1-��
            public byte byEnableHumanMisinfoFilter;//��������ȥ��   0-������, 1-����
            public byte byEnableVehicleMisinfoFilter;//���ó���ȥ��   0-������, 1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_REGIONEXITING_REGION[] struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //�뿪����������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_REGIONEXITING_REGION
        {
            public NET_VCA_POLYGON struRegion;//����Χ
            public byte bySensitivity;//�����Ȳ�������Χ[1-100]
            /*
           ���Ŀ�꣬��֧�ֶ�ѡ�����嶨��Ϊ��
           0~����Ŀ�꣨��ʾ���������Ŀ�꣬����Ŀ�궼�����м�⣩
           0x01 ~ �ˣ�
           0x02 ~ ����
           */
            public byte byDetectionTarget;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //Խ��������ò���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TRAVERSE_PLANE_DETECTION
        {
            public uint dwSize;
            public byte byEnabled;//�Ƿ�ʹ��: 0-�� , 1-��
            public byte byEnableDualVca;// ����֧�����ܺ���� 0-�����ã�1-����
            public byte byEnableHumanMisinfoFilter;// ��������ȥ�� 0-�����ã�1-����
            public byte byEnableVehicleMisinfoFilter;// ���ó���ȥ�� 0-�����ã�1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_TRAVERSE_PLANE[] struAlertParam;//�����߲���
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //�쳣������ʽ
            public uint dwMaxRelRecordChanNum;  //����������¼��ͨ�� ����ֻ�������֧������
            public uint dwRelRecordChanNum;     //����������¼��ͨ�� �� ʵ��֧�ֵ�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//����¼���ͨ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;//���ղ���ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //��������������ò���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FIELDDETECION
        {
            public uint dwSize;
            public byte byEnabled;//�Ƿ�ʹ��: 0-�� , 1-��
            public byte byEnableDualVca;// ����֧�����ܺ���� 0-�����ã�1-����
            public byte byEnableHumanMisinfoFilter;// ��������ȥ�� 0-�����ã�1-����
            public byte byEnableVehicleMisinfoFilter;// ���ó���ȥ�� 0-�����ã�1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_INTRUSION[] struIntrusion;//ÿ������Ĳ�������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException;  //�쳣������ʽ
            public uint dwMaxRelRecordChanNum;  //����������¼��ͨ�� ����ֻ�������֧������
            public uint dwRelRecordChanNum;     //����������¼��ͨ�� �� ʵ��֧�ֵ�����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;//����¼���ͨ����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;//���ղ���ʱ��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }


        //���ݱ����ӳ�ʱ������ʶ�����д�ͼƬ�����������IO����һ�£�1�뷢��һ����
        //���ֲ���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_INTRUSION
        {
            public NET_VCA_POLYGON struRegion;//����Χ
            public ushort wDuration;//�����ӳ�ʱ��: 1-120�룬����5�룬�ж�����Ч������ʱ��
            public byte bySensitivity;        //�����Ȳ�������Χ[1-100]
            public byte byRate;               //ռ�ȣ�����������δ����Ŀ��ߴ�Ŀ��ռ��������ı��أ���һ��Ϊ����
            /*
            ���Ŀ�꣬��֧�ֶ�ѡ�����嶨��Ϊ��
            0~����Ŀ�꣨��ʾ���������Ŀ�꣬����Ŀ�궼�����м�⣩
            0x01 ~ �ˣ�
            0x02 ~ ����
            */
            public byte byDetectionTarget;
            public byte byPriority;//���ȼ�, 0-��, 1-��, 2-��
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //�ߴ������
        public struct NET_VCA_SIZE_FILTER
        {
            public byte byActive;            //�Ƿ񼤻�ߴ������ 0-�� ��0-��
            public byte byMode;         //������ģʽSIZE_FILTER_MODE
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;        //��������0
            public NET_VCA_RECT struMiniRect;    //��СĿ���,ȫ0��ʾ������
            public NET_VCA_RECT struMaxRect;      //���Ŀ���,ȫ0��ʾ������
        }

        // ����ץ�Ĺ���(����)
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_SINGLE_FACESNAPCFG
        {
            public byte byActive;                //�Ƿ񼤻����0-��1-��
            //�����Զ�ROI����ʹ��
            public byte byAutoROIEnable;//0-�ر�,1-����
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; //����
            public NET_VCA_SIZE_FILTER struSizeFilter;   //�ߴ������
            public NET_VCA_POLYGON struVcaPolygon;        //����ʶ������
        }

        // ����ץ�����ò���
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAPCFG
        {
            public UInt32 dwSize;
            public byte bySnapTime;                    //����Ŀ��������ץ�Ĵ���0-10
            public byte bySnapInterval;                 //ץ�ļ������λ��֡
            public byte bySnapThreshold;               //ץ����ֵ��0-100
            public byte byGenerateRate;         //Ŀ�������ٶ�,��Χ[1, 5]    
            public byte bySensitive;            //Ŀ���������ȣ���Χ[1, 5]
            public byte byReferenceBright; //2012-3-27�ο�����[0,100]
            public byte byMatchType;         //2012-5-3�ȶԱ���ģʽ��0-Ŀ����ʧ�󱨾���1-ʵʱ����
            public byte byMatchThreshold;  //2012-5-3ʵʱ�ȶ���ֵ��0~100
            public NET_DVR_JPEGPARA struPictureParam; //ͼƬ���ṹ
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_SINGLE_FACESNAPCFG[] struRule; //����ץ�Ĺ���
            //�����ع���̳���ʱ�䣨�����ֽڣ�
            public ushort wFaceExposureMinDuration;//��Χ(1~3600�룬Ĭ��60)��Ч���Զ�ģʽ��
            //�����ع�ģʽ
            public byte byFaceExposureMode;//1-�رգ�2-������0-�Զ������������жϣ�
            public byte byBackgroundPic;//����ͼ�ϴ�ʹ�� 0-Ĭ��ֵ����������1-��ֹ
            public UInt32 dwValidFaceTime;    //��Ч������̳���ʱ�䣬��λ����
            public UInt32 dwUploadInterval; //����ץ��ͳ�������ϴ����ʱ�䣬��λ���룬Ĭ��900��
            public UInt32 dwFaceFilteringTime;//����ͣ��ʱ�����,Ĭ��5�룬��Χ0-100�롣0���ʾ������
            public byte bySceneID;     //������,Ŀǰ֧��1~4������0Ϊ��Ч
            public byte byInvalCapFilterEnable;//��Чץ�Ĺ���ʹ�ܣ�0Ϊ�رգ�1Ϊ������Ĭ��Ϊ0
            public byte byInvalCapFilterThreshold;//��Чץ�Ĺ�����ֵ��0~100����byInvalCapFilterEnableΪ1ʱ��Ч
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 81, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int NET_SDK_MAX_FDID_LEN = 256;
        public const int NET_SDK_MAX_INDENTITY_KEY_LEN = 64;//���볤��
        public const int MAX_UPLOADFILE_URL_LEN = 240;//���볤��

        //����������������
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACELIB_COND
        {
            public UInt32 dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_FDID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szFDID;//������ID
            public byte byConcurrent;//�豸�������� 0-��������1-��ʼ
            public byte byCover;//�Ƿ񸲸�ʽ���� 0-��1-��
            public byte byCustomFaceLibID;//FDID�Ƿ����Զ��壬0-���ǣ�1-�ǣ�
            /*����/ISAPI/Intelligent/channels/<ID>/faceContrast/capabilities�������з���isSupportNoSaveUploadPicture�����ڵ�ʱ��
            �����ǲ�������ģʽ�£�֧�ֲ������ϴ�ԭͼ�Ĳ���:���ϴ��ɹ�ͼƬ���豸��ģ�ɹ��󣬻Ὣ�ϴ���ԭͼ����ɾ����
            ע���ò����޷��벢������ͬʱ���С�*/
            public byte byPictureSaveMode;//�ϴ�ԭͼ����ģʽ��0-���棬1-������;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_SDK_MAX_INDENTITY_KEY_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byIdentityKey;//������������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // �������ݽṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SEND_PARAM_IN
        {
            public IntPtr pSendData;             //���͵Ļ�����,PicURL == 1 ��ʱ���ڴ��д洢���� URL �ַ���,byUploadModeling == 1 ��ʱ���ڴ��д洢���� ��ģbase64��������
            public UInt32 dwSendDataLen;         //�������ݳ���,PicURL == 1 ��ʱ�򣬱�ʾ�� URL �ַ����ĳ���,byUploadModeling == 1 ��ʱ�򣬱�ʾΪ��ģ����base64��ļ��ܳ���
            public NET_DVR_TIME_V30 struTime;   //ͼƬʱ��
            public byte byPicType;              //ͼƬ��ʽ,1-jpg,2-bmp,3-png,4-SWF,5-GIF
            public byte byPicURL;               //ͼƬ���ݲ���URL��ʽ 0-������ͼƬ���ݣ�1-ͼƬ������URL��ʽ
            /*�Ƿ��ϴ���ģ���ݣ�
            0-  ������ͼƬ���ݷ�ʽ(pSendDataָ�������ͼƬ����, dwPicDataLenΪͼƬ���������ݳ���)��
            1-  ֱ���ϴ���ģ����(pSendDataָ��ģbase64��������, dwPicDataLenΪ��ģ����base64��ļ��ܳ���)��
            ע����ģ���ݲ���base64���ܷ�ʽ,ѡ��Ϊ��ģ�����ϴ���byPicURL ���衣
            ����/ISAPI/Intelligent/channels/<ID>/faceContrast/capabilities�������з���isSupportUploadModeling�����ڵ�ʱ��֧���ϴ���ģ����. */
            public byte byUploadModeling;
            public byte byRes1;
            public UInt32 dwPicMangeNo;           //ͼƬ������
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPicName;     //ͼƬ����
            public UInt32 dwPicDisplayTime;       //ͼƬ����ʱ������λ��
            public IntPtr pSendAppendData;       //����ͼƬ�ĸ�����Ϣ����������ӦFaceAppendData ��XML������
            public UInt32 dwSendAppendDataLen;    //����ͼƬ�ĸ�����Ϣ���ݳ���  FaceAppendData  XML�ĳ��ȣ�
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // �ļ��ϴ������Ϣ�ṹ��
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_UPLOAD_FILE_RET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_UPLOADFILE_URL_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUrl;   //url
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 260, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        } 

        #region ������ʾ
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