using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;
using GJC.Helper;
using PimDeWitte.UnityMainThreadDispatcher;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using UnityEngine;

namespace XP.TableModel.Test
{
public class comconnect : MonoBehaviour
{
    public Text test;
    private bool cmdstatus = false;
    private bool stopstatus = false;
    public Table M3i1_table;
    public Button executem3i1cmd;

    public Button upcamera;
    public Button leftcamera;
    public Button rightcamera;
    public Button downcamera;

    [SerializeField] InputField m3i2input;
    public Button m3i2sendcmd;
    //public Button testrotate;
    public static string _DefaultTxtPath = ""; // 修改为你的配置文件路径
    public static string m3i1value = "testm3i1";
    public static string m3i1_Ivalue = "";
    public static string m3i1_Ovalue = "";
    public Text posx,posy,posz,posa,posb,posc;

    private bool running = false;

    public GameObject viewverify;
public Transform[] bones;
public Transform[] bones2; //协作
        public GameObject rbt1;
    public GameObject rbt2;
    public Dropdown drprobottype;
    
    string rownum = "";
    string closeiorownum = "";
    public Table _Table;
    public Table _Tablem2i4;
    private string accumulatedData = "";
    private TcpClient tcpClient;
    private string previousData = "";
    private NetworkStream networkStream;
    public List<Slider> sliders = new List<Slider>();
    public List<Button> btns = new List<Button>();
    public List<Button> btnscoms = new List<Button>();
    [SerializeField] public InputField field;
    [SerializeField] public InputField fieldx;
    [SerializeField] public InputField fieldy;
    [SerializeField] public InputField fieldz;

    
    public Button closeverifywin;
    public Button verifyendcom;
    public Button cancelendcom;

    private int speed = 1;
    public GameObject[] imgspeeds;
    public Button imgspeedadd;
    public Button imgspeedminus;

    private int precision = 1;
    public GameObject[] imgprecisions;
    public Button imgprecisionadd;
    public Button imgprecisionminus;

    
    public Button btncom,_openio,closeio;
    public Button btnfresh;
    // public Button btnoffbrake;
    private string sliderName;
    public Dropdown dropdownMenu;
    public Dropdown drp3Component;
    public Button btnFreshComponent;
    public Button btngetconfig;
    private bool _continue = true;

    public string portName = "COM5"; //串口名
    private int baudRate = 1000000; //波特率 0是1000000，1是512000，2是230400，3是115200，4是9600
    public Parity parity = Parity.None; //效验位
    public int dataBits = 8; //数据位
    public StopBits stopBits = StopBits.One; //停止位
    SerialPort sp = null;
    private Thread readThread;


    public Button btnconnect;
    public Button btngcode;
    [SerializeField] public InputField inputaddr;
    [SerializeField] public InputField inputport;
                        
    [SerializeField] public InputField inputovertime;
    [SerializeField] public InputField inputBaudRateComponent;

    [SerializeField] private float value;
    [SerializeField] private float value1;
    [SerializeField] private float value2;
    [SerializeField] private float value3;
    [SerializeField] public InputField inputgcode;
    private Action action;


    public Transform originBone0;
    public Transform originBone1;
    public Transform originBone2;
    public Transform originBone3;

    private Quaternion oriAngle0;
    private Quaternion oriAngle1;
    private Quaternion oriAngle2;
    private Quaternion oriAngle3;

    private float bone0_x_rotation = 103;
    private float bone1_z_rotation = -4;
    private float bone2_z_rotation = -66;
    private float bone4_x_rotation = 0;
    private float bone4_z_rotation = -13;


            public struct JPOStyp  //关节坐标
        {
            public double J1;
            public double J2;
            public double J3;
            public double J4;
            public double J5;
            public double J6;
        }

        public struct HCOORDtyp  //直角坐标或变换矩阵（齐次）
        {
            public double X;
            public double Y;
            public double Z;
            public double R11;
            public double R12;
            public double R13;
            public double R21;
            public double R22;
            public double R23;
            public double R31;
            public double R32;
            public double R33;
        }


    
    public Button btnteach;

    private void Awake()
{
    ThreadCrossHelper.Instance.init();

    // 获取所有带有 "comconnect" 标签的游戏对象并转换为列表
    List<GameObject> views = GameObject.FindGameObjectsWithTag("comconfig").ToList();

    // 初始化组件变量
    inputBaudRateComponent = null;
    btnFreshComponent = null;
    drp3Component = null;
    inputovertime = null;

    // 遍历列表中的每个游戏对象
    foreach (GameObject view in views)
    {
        // 根据游戏对象的名称获取相应组件
        switch (view.name)
        {
            case "inputbaudrate":
                inputBaudRateComponent = view.GetComponent<InputField>();
                break;

            case "inputovertime":
                inputovertime = view.GetComponent<InputField>();
                break;

            case "btnfresh":
                btnFreshComponent = view.GetComponent<Button>();
                break;

            case "btngetconfig":
                btngetconfig = view.GetComponent<Button>();
                break;


            case "drp3":
                drp3Component = view.GetComponent<Dropdown>();
                break;


                

            // 添加其他可能的组件...

            default:
                // 如果有其他游戏对象，可以在此添加处理逻辑
                break;
        }
    }
    InitDropdownMenu();
}


    private void testrotateClick(){

        bones[0].localRotation = Quaternion.Euler(1F * new Vector3(bone0_x_rotation-50, -90, -90));
        bones[1].localRotation = Quaternion.Euler(1F * new Vector3(0, -13, 20+bone1_z_rotation ));
        bones[2].localRotation = Quaternion.Euler(1F * new Vector3(0, 0, -20+bone2_z_rotation));
        bones[3].localRotation = Quaternion.Euler(1F * new Vector3(bone4_x_rotation+20, -0, bone4_z_rotation+60));
        // bones[0].localRotation = Quaternion.Euler(0.5F * new Vector3(0, 360, 0)); //底座X轴动 (骨骼
        // bones[1].localRotation = Quaternion.Euler(0.6F  * new Vector3(360, 0, 0));
        // bones[2].localRotation = Quaternion.Euler(0.4F * new Vector3(360, 0, 0));
        // bones[3].localRotation = Quaternion.Euler(0.2F * new Vector3(360, 0, 0)); //动Z轴是前后旋转, 或者XY一起动是横向动 (骨骼004)


    }
    private void Start()
    {
//        rbt2.SetActive(false);

            for(int i = 0 ; i <5 ; i++){
        imgprecisions[i].SetActive(false);
    }
    imgprecisions[precision-1].SetActive(true);

        for(int i = 0 ; i <5 ; i++){
        imgspeeds[i].SetActive(false);
    }
    imgspeeds[speed-1].SetActive(true);

    

        oriAngle0 = originBone0.rotation;
        oriAngle1 = originBone1.rotation;
        oriAngle2 = originBone2.rotation;
        oriAngle3 = originBone3.rotation;

        // _DefaultTxtPath=Application.persistentDataPath+"ioconfig.txt";
        // 获取Dropdown组件
        // dropdownMenu = this.transform.GetChild(2).GetChild(4).GetComponent<Dropdown>();

        // 添加下拉菜单选项
        // AddDropdownOptions();

        viewverify.SetActive(false);

        //  transform.Find("")  只能找子物体 不能找子子物体
        Init();
        
            _openio.onClick.AddListener(() =>
            {
                openio_printrow();
            });


                closeio.onClick.AddListener(() =>
                {
                    closeio_printrow();
                });





    


        // sliders[0].onValueChanged.AddListener((f) => { value = f; });
        // sliders[1].onValueChanged.AddListener((f) => { value1 = f; });
        // sliders[2].onValueChanged.AddListener((f) => { value2 = f; });
        // sliders[3].onValueChanged.AddListener((f) => { value3 = f; });
        // dropdownMenu.onValueChanged.AddListener((f) => { OnDropdownValueChanged(); });


        // btns[0].onClick.AddListener(() => { print(fieldx.text + fieldy.text + fieldz.text); });

        // 连接串口
        btncom.onClick.AddListener(() => { ConnectButtonClick(); });


        //确定断开串口
        verifyendcom.onClick.AddListener(() => { verifyendcomClick(); });


        cancelendcom.onClick.AddListener(()=> { cancelendcomClick(); });
        
        // testrotate.onClick.AddListener(()=> { testrotateClick(); });

        closeverifywin.onClick.AddListener(()=> { closeverifywinonClick(); });

        imgspeedadd.onClick.AddListener(()=> { imgspeedaddonClick(); });

        imgspeedminus.onClick.AddListener(()=> { imgspeedminusonClick(); });

        imgprecisionadd.onClick.AddListener(()=> { imgprecisionaddonClick(); });

        imgprecisionminus.onClick.AddListener(()=> { imgprecisionminusonClick(); });
        
        
        // //刷新
        btnFreshComponent.onClick.AddListener(() => { RefreshSerialPorts(); });

        // btnconnect.onClick.AddListener(() => { ConnectTcp(); });

        // Debug.Log("btngcode: " + (btngcode != null ? "Not null" : "Null"));
        // Debug.Log("inputgcode: " + (inputgcode != null ? "Not null" : "Null"));

        // btngcode.onClick.AddListener(() => { SendSerialData(inputgcode.text); });
        btngetconfig.onClick.AddListener(() => { SendDollar(); });
        // btnoffbrake.onClick.AddListener(() => { offbrake(); });


        m3i2sendcmd.onClick.AddListener(() => { m3i2sendcmdonlick(); });


        executem3i1cmd.onClick.AddListener(() => { m3i1executecmdonlick(); });

        // btnteach.onClick.AddListener(() => { btnteachonlick(); });

        ///////////////////btnteach.onClick.AddListener(() => { Motion_Interpolation(); });
        
    }

    // public void m3i1executecmdonlick(){

    //     //发一条 读下位机信息 ok则继续  error则报错 ，啥都没有则等待


    //     // print(M3i1_table._HeaderRow._HeaderCellDatas);
    //     foreach(var r in M3i1_table._GetColumCellDatas(0)){
    //         print(r._ShowData);
            
    //                if (sp != null && sp.IsOpen)
    //     {
    //         //  sp.Write("\r\n"+"m12.16"+"\r\n");
    //         sp.Write("\r\n"+r._ShowData+"\r\n");

    //         // sp.Write("\r\n"+"m12.16"+"\r\n");
    //         // sp.Write("\r\n"+"G00X0.000Y0.000Z30.000A20.000B10.000C0.000"+"\r\n");
            
  
    //     }


    //     }


    // }

    public void m3i1executecmdonlick()
    {
        foreach (var r in M3i1_table._GetColumCellDatas(0))
        {
            string myString = (string)r._ShowData;  
        string[] parameters = myString.Split(' ');
        // 初始化变量  
        float A = 0, B = 0, C = 0, X = 0, Y = 0, Z = 0;  
        
        // 循环遍历数组元素  
        foreach (string parameter in parameters)  
        {  
            // 去除字符串前后的空白字符  
            string trimmedParameter = parameter.Trim();  
  
            // 判断字符串是否以'A'开始  
            if (trimmedParameter.StartsWith("A"))  
            {  
                // 提取'A'后面的数值，并赋给变量A  
                A = float.Parse(trimmedParameter.Substring(1));  
            }  
            // 判断字符串是否以'B'开始  
            else if (trimmedParameter.StartsWith("B"))  
            {  
                // 提取'B'后面的数值，并赋给变量B  
                B = float.Parse(trimmedParameter.Substring(1));  
            }  
            // 判断字符串是否以'C'开始  
            else if (trimmedParameter.StartsWith("C"))  
            {  
                // 提取'C'后面的数值，并赋给变量C  
                C = float.Parse(trimmedParameter.Substring(1));  
            }  
                        else if (trimmedParameter.StartsWith("X"))  
            {  
                // 提取'X'后面的数值，并赋给变量C  
                X = float.Parse(trimmedParameter.Substring(1));  
            }  
                        else if (trimmedParameter.StartsWith("Y"))  
            {  
                // 提取'Y'后面的数值，并赋给变量C  
                Y = float.Parse(trimmedParameter.Substring(1));  
            }  
                        else if (trimmedParameter.StartsWith("Z"))  
            {  
                // 提取'Z'后面的数值，并赋给变量C  
                Z = float.Parse(trimmedParameter.Substring(1));  
            }  
        }  






                        // 使用 UpdateRotation 方法更新物体的旋转
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                bones[0].localRotation = Quaternion.Euler(1F * new Vector3(bone0_x_rotation+C, -90, -90)); //对应c
                bones[1].localRotation = Quaternion.Euler(1F * new Vector3(0, -13, B+bone1_z_rotation )); //对应b
                bones[2].localRotation = Quaternion.Euler(1F * new Vector3(0, 0, A+bone2_z_rotation)); //对应a
                bones[3].localRotation = Quaternion.Euler(1F * new Vector3(bone4_x_rotation+Y, -0, bone4_z_rotation+Z)); //横着的是y 竖着的是z


                    // 执行需要在主线程中完成的操作
                    // Debug.Log("This is executed on the main thread.");
                    // UpdateRotation(x, y, z, a);
                });

        }
        stopstatus = false;
        StartCoroutine(SendAndReceiveData());
    }

    private IEnumerator SendAndReceiveData()
    {
        foreach (var r in M3i1_table._GetColumCellDatas(0))
        {
            print(r._ShowData);

            if (sp != null && sp.IsOpen)
            {
               print("cmdstatus状态: "+cmdstatus);
                print("stopstatus状态: "+stopstatus);
                sp.Write("\r\n" + r._ShowData + "\r\n");
                cmdstatus = false;
                float startTime = Time.time;
                float timeout = 5f; // 超时时间

                // 等待 cmdstatus 变为 true 或者超时
                while (!cmdstatus && Time.time - startTime < timeout)
                {
                    yield return new WaitForSeconds(1f); // 每秒等待一次
                }

                // 处理 cmdstatus
                if (!cmdstatus)
                {
                    // cmdstatus 是 false，等待5秒
                    yield return new WaitForSeconds(5f);

                    // 检查 stopstatus
                    if (stopstatus)
                    {
                        print("Stopped operation due to stopstatus being true");
                        yield break; // 结束协程
                    }
                }

                // cmdStatus 是 true，继续执行
                // 例如，执行下一个命令
            }

            yield return null; // 等待下一帧
        }
    }





    public void m3i2sendcmdonlick(){
        if (sp != null && sp.IsOpen)
        {
            //  sp.Write("\r\n"+"m12.16"+"\r\n");
            sp.Write("\r\n"+m3i2input.text+"\r\n");

            // sp.Write("\r\n"+"m12.16"+"\r\n");
            // sp.Write("\r\n"+"G00X0.000Y0.000Z30.000A20.000B10.000C0.000"+"\r\n");
            
            print(m3i2input.text);
            
  
        }
    }
    public void verifyendcomClick(){
        print("关闭系统");
        

                if (sp != null && sp.IsOpen)
        {
            sp.Close();
            viewverify.SetActive(false);
            
  
        }

        // if(sendQuestion != null){
        //     sendQuestion.Close();
        // }

        //         if(readDollar != null){
        //     readDollar.Close();
        // }



    }

    private void imgprecisionaddonClick(){
                if(precision <= 4){
            precision += 1;
        }
        print("precision is: "+precision);
    for(int i = 0 ; i <5 ; i++){
        imgprecisions[i].SetActive(false);
    }
    imgprecisions[precision-1].SetActive(true);

    }

    private void imgprecisionminusonClick(){
        if(precision >= 2){
            precision -= 1;
        }
        print("precision is: "+precision);
    for(int i = 0 ; i <5 ; i++){
        imgprecisions[i].SetActive(false);
    }
    imgprecisions[precision-1].SetActive(true);
    }




    private void imgspeedaddonClick(){
        if(speed <= 4){
            speed += 1;
        }
        print("speed is: "+speed);
    for(int i = 0 ; i <5 ; i++){
        imgspeeds[i].SetActive(false);
    }
    imgspeeds[speed-1].SetActive(true);
    }


        private void imgspeedminusonClick(){
        if(speed >= 2){
            speed -= 1;
        }
        print("speed is: "+speed);
    for(int i = 0 ; i <5 ; i++){
        imgspeeds[i].SetActive(false);
    }
    imgspeeds[speed-1].SetActive(true);
    }



    private void closeverifywinonClick(){
        viewverify.SetActive(false);
    }

    private void Init()

    {
        
        System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
        // string[] ports = SerialPort.GetPortNames();
        // print(ports);
        // if (ports.Length > 0)
        // {
        //     Debug.Log("Available Serial Ports:");

        //     foreach (string port in ports)
        //     {
        //         Debug.Log(port);
        //     }
        // }
        // else
        // {
        //     Debug.Log("No serial ports available.");
        // }

        print(sp);
        // var sliderZone = this.transform.GetChild(0);
        // var btnZone = this.transform.GetChild(1);
        // var tcpZone = this.transform.GetChild(4);
        // btnoffbrake = this.transform.GetChild(5).GetComponent<Button>();  //comconnect
        // fieldx = this.transform.GetChild(1).GetChild(1).GetComponent<InputField>();
        // fieldy = this.transform.GetChild(1).GetChild(2).GetComponent<InputField>();
        // fieldz = this.transform.GetChild(1).GetChild(3).GetComponent<InputField>();
        // btncom = this.transform.GetChild(5).GetComponent<Button>();
        // btnfresh = this.transform.GetChild(2).GetChild(5).GetComponent<Button>();
        // inputaddr = this.transform.GetChild(4).GetChild(1).GetComponent<InputField>();
        // inputport = this.transform.GetChild(4).GetChild(2).GetComponent<InputField>();
        // btnconnect = this.transform.GetChild(4).GetChild(0).GetComponent<Button>();

        // btngcode = this.transform.GetChild(3).GetChild(0).GetComponent<Button>();
        // inputgcode = this.transform.GetChild(3).GetChild(1).GetComponent<InputField>();
        // for (int i = 0; i < sliderZone.childCount; i++)
        // {
        //     var child = sliderZone.GetChild(i); //slider第一个子物体 
        //     sliders.Add(child.GetComponent<Slider>());
        // }

        // for (int i = 0; i < btnZone.childCount; i++)
        // {
        //     var child = btnZone.GetChild(i);
        //     btns.Add(child.GetComponent<Button>());
        // }
    }

    private void cancelendcomClick(){
        viewverify.SetActive(false);
    }

    private void InitDropdownMenu()
    {
        // Get the Dropdown component
        // dropdownMenu = this.transform.GetChild(2).GetChild(4).GetComponent<Dropdown>();

        // Add listener to handle dropdown value changes
        // dropdownMenu.onValueChanged.AddListener(OnDropdownValueChanged);

        // Refresh serial ports when the script starts
        RefreshSerialPorts();
    }

    private void OnDropdownValueChanged()
    {
        print("Selected Serial Port: " + dropdownMenu.GetComponent<Dropdown>().value);

        // You can use the selected port as needed, e.g., set ComPort variable.
    }

    private void RefreshSerialPorts()
    {
        try
        {
            // 创建临时的 SerialPort 对象以检索端口名称
            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
            print(sp);

            // 获取端口名称
            string[] str = System.IO.Ports.SerialPort.GetPortNames();
            print(str);
            if (str.Length > 0)
            {
                Debug.Log("Available Serial Ports:");

                foreach (string port in str)
                {
                    Debug.Log(port);
                }
            }

            // 将端口名称打印到控制台
            // foreach (string port in str)
            // {
            //     print(port);
            // }

            // 清除并更新下拉菜单选项
            List<string> options = new List<string>(str);
            drp3Component.ClearOptions();
            drp3Component.AddOptions(options);
        }
        catch (Exception e)
        {
            Debug.LogError("刷新串口时出现错误：" + e.Message);
        }
    }



    private void config(){

    }

    private void ConnectButtonClick()

    {

            if (sp != null && sp.IsOpen)
            {
                
                viewverify.SetActive(true);

                // sp.Close();
            }else{
                



            print(inputovertime.text);
            print(inputBaudRateComponent.text);

            //find the selected index
            int menuIndex = drp3Component.GetComponent<Dropdown>().value;

            //find all options available within the dropdown menu
            List<Dropdown.OptionData> menuOptions = drp3Component.GetComponent<Dropdown>().options;

            //get the string value of the selected index
            string value = menuOptions[menuIndex].text;
            print(value);

            portName = value;
            print("public baudrate is :" + inputBaudRateComponent.text);
            
            sp = new SerialPort(portName, Int32.Parse(inputBaudRateComponent.text), parity, dataBits, stopBits);
            sp.ReadTimeout = Int32.Parse(inputovertime.text)*1000;


            sp.Open();
            print("connect com");
            // StartCoroutine(SendQuestionCoroutine());

                // 在主线程中执行的代码
                // 操作 Unity 对象等
                Thread sendQuestion = new Thread(new ThreadStart(SendQuestionCoroutine));
                //_continue = true;
                sendQuestion.Start();





            // print("loopsend");
            ThreadCrossHelper.Instance.AddMainThread(() =>
            {
                // 在主线程中执行的代码
                // 操作 Unity 对象等
                Thread readThread = new Thread(new ThreadStart(Read));
                //_continue = true;
                readThread.Start();
            });

                
            }

    }


    // void AddDropdownOptions()
    // {
    //     // 创建一个选项列表
    //     List<string> options = new List<string>();
    //     options.Add("Option 1");
    //     options.Add("Option 2");
    //     options.Add("Option 3");

    //     // 清空原有的选项
    //     dropdownMenu.ClearOptions();

    //     // 添加新的选项
    //     dropdownMenu.AddOptions(options);
    // }

    public void SendQuestion()
    {
        sp.Write("?");
    }


            private void openio_printrow()
        {
            
            // 将选中单元格的显示数据设置为输入框的文本
            foreach (var item in _Tablem2i4._CurrentSelectedCellDatas)
            {
                print(item._Row);
                            if (item._Row >7){
                rownum = (item._Row-7).ToString();
                            if(rownum.Length == 1){
                rownum= "0"+rownum;
            }

            rownum = "M12." + rownum;
            print("send openio cmd: "+ rownum);
            sp.Write("\r\n"+rownum+"\r\n");

            }
            }

            // updateio();
        }


                    private void closeio_printrow()
        {
            
            // 将选中单元格的显示数据设置为输入框的文本
            foreach (var item in _Tablem2i4._CurrentSelectedCellDatas)
            {
                print(item._Row);
                            if (item._Row >7){
                closeiorownum = (item._Row-7).ToString();
                            if(closeiorownum.Length == 1){
                closeiorownum= "0"+closeiorownum;
            }

            closeiorownum = "M11." + closeiorownum;
            print("send closeio cmd: "+ closeiorownum);
            sp.Write("\r\n"+closeiorownum+"\r\n");

            }
            }
        }






    

    public void SendDollar()
    {


                ThreadCrossHelper.Instance.AddMainThread(() =>
        {
            // 在主线程中执行的代码
            // 操作 Unity 对象等
            Thread readDollar = new Thread(new ThreadStart(readdollar));
            //_continue = true;
            readDollar.Start();
        });


        // 在要发送的数据末尾添加换行符
        string dataWithNewLine = "$$" + "\r\n";
        
        // 发送带有换行符的数据
        sp.Write(dataWithNewLine);

        print(dataWithNewLine);


    }


        public void readdollar()
    {
        byte[] buffer = new byte[8192]; // 或者使用更大的大小


        print("before while");

       while (true){

            if (sp != null && sp.IsOpen)
            {
                try
                {
                    int bytesRead = sp.Read(buffer, 0, buffer.Length);

                    // 将读取到的数据拼接到 accumulatedData 变量
                    accumulatedData += Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    print(accumulatedData);

                    if(string.IsNullOrEmpty(accumulatedData)){
                        
                        break;
                    }


                }
                catch (TimeoutException ex)
                {
                    UnityEngine.Debug.LogError($"TimeoutException: {ex.Message}");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Serial port is not open!");
            }
            // Thread.Sleep(1000);
        }
    }
    

    public void SendSerialData(string data)
    {
        // 在要发送的数据末尾添加换行符
        string dataWithNewLine = data + "\r\n";
        sp.Write("\r\n");
        // 发送带有换行符的数据
        sp.Write(dataWithNewLine);

        print(dataWithNewLine);
    }

    public void offbrake()
    {
        sp.Write("\r\n" + "m12.16" + "\r\n");
    }


    private void SendQuestionCoroutine()
    {
        while (true)
        {
            // 发送问号
            SendQuestion();
            Thread.Sleep(1000);

        }
    }


    public void OpenSerialPort()
    {
        // // Initialise the serial port
        // SerialPort = new SerialPort(ComPort, BaudRate, Parity, DataBits, StopBits);

        // SerialPort.ReadTimeout = ReadTimeout;
        // SerialPort.WriteTimeout = WriteTimeout;

        // SerialPort.DtrEnable = DtrEnable;
        // SerialPort.RtsEnable = RtsEnable;

        // // Open the serial port
        // SerialPort.Open();
    }


    public void Read()
    {
        byte[] buffer = new byte[5120]; // 或者使用更大的大小


        print("before while");

        while (true)
        {
            if (sp != null && sp.IsOpen)
            {
                try
                {
                    int bytesRead = sp.Read(buffer, 0, buffer.Length);

                    // 将读取到的数据拼接到 accumulatedData 变量
                    accumulatedData += Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // 检查是否收到了字符 ">"
                    int endIndex = accumulatedData.IndexOf(">");
                    if (endIndex != -1)
                    {
                        // 截取 ">" 之前的数据作为完整消息
                        string completeMessage = accumulatedData.Substring(0, endIndex);

                        // 处理完整的消息
                        ProcessReceivedData(completeMessage);

                        UpdateIO(completeMessage);
                        // 清空 accumulatedData，准备接收下一条消息
                        accumulatedData = "";

                        // 如果 ">" 后还有数据，继续处理
                        if (endIndex + 1 < accumulatedData.Length)
                        {
                            accumulatedData = accumulatedData.Substring(endIndex + 1);
                        }
                    }


                    // Thread.Sleep(1000);
                    // int bytes = sp.Read(buffer, 0, buffer.Length);
                    // print(bytes);
                    // if (bytes == 0)
                    // {
                    //     continue;
                    // }
                    // else
                    // {   
                    //     string receivedData = Encoding.ASCII.GetString(buffer, 0, bytes);
                    //     print(receivedData);
                    //     // 检查字符串中是否包含"ABSPos:"
                    //     // 检查字符串中是否包含"ABSPos:"
                    //     if (receivedData.Length > 70)
                    //     {   
                    //         //print("start find");
                    //         // 找到"ABSPos:"的位置
                    //         int absPosIndex = receivedData.IndexOf("ABSPos:");
                    //         print(absPosIndex);
                    //         // 提取"ABSPos:"后的子字符串
                    //         string remainingString = receivedData.Substring(absPosIndex + "ABSPos:".Length);
                    //         print(remainingString);
                    //         // 检查字符串中是否包含" | "，表示结尾
                    //         int endIndex = remainingString.IndexOf("|");

                    //         // 提取满足条件的部分
                    //         string absPosData = remainingString.Substring(0, endIndex);
                    //         print(endIndex);
                    //         // 输出结果
                    //         print("ABSPos Data: " + absPosData);

                    //         // 将 absPosData 根据逗号分隔成字符串数组
                    //         string[] values = absPosData.Split(',');

                    //         if (values.Length == 6)
                    //         {
                    //             // 将字符串数组的元素解析为浮点数
                    //             float x = float.Parse(values[0]);
                    //             float y = float.Parse(values[1]);
                    //             float z = float.Parse(values[2]);
                    //             float a = float.Parse(values[3]);
                    //             float b = float.Parse(values[4]);
                    //             float c = float.Parse(values[5]);

                    //             // 使用 UpdateRotation 方法更新物体的旋转


                    //                 UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    //                 {
                    //                     // 执行需要在主线程中完成的操作
                    //                     Debug.Log("This is executed on the main thread.");
                    //                     UpdateRotation(x, y, z, a);
                    //                 });


                    //         }
                    //         else
                    //         {
                    //             Debug.LogError("Invalid number of values in ABSPos data.");
                    //         }

                    //     }else{
                    //         print("less than 70");
                    //     }
                    //     // string strbytes = Encoding.Default.GetString(buffer, 0, bytes);

                    //     // // 检查接收到的数据是否与先前的数据不同
                    //     // if (strbytes != previousData)
                    //     // {
                    //     //     previousData = strbytes;
                    //     //     action = () => { ReceiveMsg(strbytes); };
                    //     // }
                    // }
                }
                catch (TimeoutException ex)
                {
                    UnityEngine.Debug.LogError($"TimeoutException: {ex.Message}");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Serial port is not open!");
            }
            // Thread.Sleep(1000);
        }
    }


    static void UpdateIO(string input)
    {
        // 查找 "Pn:O" 的位置
        int pnIndex = input.IndexOf("Pn:O");

        if (pnIndex != -1)
        {
            // 去掉该位置之前的字符串
            input = input.Substring(pnIndex + "Pn:O".Length);

            // 找到更新后字符串中的第一个 "|"
            int firstPipeIndex = input.IndexOf("|");

            if (firstPipeIndex != -1)
            {
                // 截取 "I" 后面的字符串
                string modifiedString = input.Substring(0, firstPipeIndex);

                // 去掉剩下字符串里的 "O" 和 "I"
                modifiedString = modifiedString.Replace("Pn:O", "").Replace("I", "");

                // 逗号分隔存入两个变量
                string[] values = modifiedString.Split(',');

                if (values.Length >= 2)
                {
                    string variable1 = values[0];
                    string variable2 = values[1];
                    m3i1_Ivalue = variable1;
                    m3i1_Ovalue = variable2;
                    // 在这里进行相应的处理
                    print("Variable 1: " + variable1);
                    print("Variable 2: " + variable2);
                }
            }
        }
    }



    private void ProcessReceivedData(string receivedData)
    {
        print(receivedData);
        print("执行ing");
        if (receivedData.Contains("ok")){
            cmdstatus = true;
        }

                if (receivedData.Contains("error")){
            stopstatus = true;
        }

        

        // 检查是否包含MPos
        if (receivedData.Length > 70 && receivedData.Contains("MPos:"))
        {
            // if(receivedData.Contains("Pn:")){
            //     _Table._SetCellData(2,4,"testagain");
            // }

            
            int absPosIndex = receivedData.IndexOf("MPos:");
            string remainingString = receivedData.Substring(absPosIndex + "MPos:".Length);
            int endIndex = remainingString.IndexOf("|");

            // 提取满足条件的部分
            string absPosData = remainingString.Substring(0, endIndex);
            print("MPos Data: " + absPosData);

            // 将 absPosData 根据逗号分隔成字符串数组
            string[] values = absPosData.Split(',');

            if (values.Length == 6)
            {
                // 将字符串数组的元素解析为浮点数
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                float a = float.Parse(values[3]);
                float b = float.Parse(values[4]);
                float c = float.Parse(values[5]);







                // 使用 UpdateRotation 方法更新物体的旋转
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                bones[0].localRotation = Quaternion.Euler(1F * new Vector3(bone0_x_rotation+c, -90, -90)); //对应c
                bones[1].localRotation = Quaternion.Euler(1F * new Vector3(0, -13, b+bone1_z_rotation )); //对应b
                bones[2].localRotation = Quaternion.Euler(1F * new Vector3(0, 0, a+bone2_z_rotation)); //对应a
                bones[3].localRotation = Quaternion.Euler(1F * new Vector3(bone4_x_rotation+y, -0, bone4_z_rotation+z)); //横着的是y 竖着的是z


                posx.text = "X"+x.ToString() + " Y"+y.ToString() + " Z"+z.ToString()+" A"+a.ToString()+" B"+b.ToString()+" C"+c.ToString();
                print("底边条值为: "+posx.text);


                    // 执行需要在主线程中完成的操作
                    // Debug.Log("This is executed on the main thread.");
                    // UpdateRotation(x, y, z, a);
                });
            }
            else
            {
                Debug.LogError("Invalid number of values in MPos data.");
            }
        }
        else
        {
            print("less than 70 or does not contain MPos");
        }
    }


    private void ReceiveMsg(string obj)
    {
        // Debug.Log(obj);
        // GameObject prefab = Resources.Load<GameObject>("字体");
        // var textShow = GameObject.Instantiate(prefab).GetComponent<TextShow>();
        // textShow.showContent = obj;
        // textShow.Init();
    }


    private void ConnectTcp()
    {
        try
        {
            // 创建 TcpClient 对象并连接到服务器
            tcpClient = new TcpClient();
            tcpClient.Connect(new IPAddress(new byte[] { 127, 0, 0, 1 }), 6069);

            // 如果连接成功，启动一个协程来处理数据传输
            StartCoroutine(HandleTcpConnection());
        }
        catch (Exception ex)
        {
            Debug.LogError("Error connecting to server: " + ex.Message);
        }
    }

    private IEnumerator HandleTcpConnection()
    {
        // 获取 NetworkStream 对象
        networkStream = tcpClient.GetStream();

        // 发送数据的例子
        string messageToSend = "A,0,0,1.57,0,0,0,4";
        byte[] dataToSend = Encoding.UTF8.GetBytes(messageToSend);
        networkStream.Write(dataToSend, 0, dataToSend.Length);

        // // 接收数据的例子
        // byte[] dataReceived = new byte[256];
        // int bytesRead = networkStream.Read(dataReceived, 0, dataReceived.Length);
        // string receivedMessage = Encoding.UTF8.GetString(dataReceived, 0, bytesRead);
        // Debug.Log("Received message from server: " + receivedMessage);

        // 关闭连接
        tcpClient.Close();
        return null;
    }


    // 在程序结束时关闭 TCP 连接
    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            tcpClient.Close();
        }
    }

    private void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
        {
            sp.Close();
            // readDollar.Close();
            
        }

        // Add any other cleanup code as needed
    }


    //     private void ProcessAccumulatedData()
    // {
    //     const string ABSPosMarker = "ABSPos:";
    //     int absPosIndex = accumulatedData.IndexOf(ABSPosMarker);

    //     while (absPosIndex != -1)
    //     {
    //         // 找到了 ABSPos 的位置
    //         int pipeIndex = accumulatedData.IndexOf("|", absPosIndex);

    //         if (pipeIndex != -1)
    //         {
    //             // 找到了 | 的位置
    //             string messageToProcess = accumulatedData.Substring(absPosIndex, pipeIndex - absPosIndex + 1);

    //             // 处理完整的消息
    //             ProcessReceivedData(messageToProcess);

    //             // 从 accumulatedData 中移除已处理的消息
    //             accumulatedData = accumulatedData.Substring(pipeIndex + 1);
    //         }
    //         else
    //         {
    //             // 如果没有找到 |，表示消息不完整，跳出循环等待下一次读取
    //             break;
    //         }

    //         // 继续查找下一个 ABSPos
    //         absPosIndex = accumulatedData.IndexOf(ABSPosMarker);
    //     }
    // }


    // 更新物体的旋转方法
    private void UpdateRotation(float rotationValue, float rotationValue1, float rotationValue2, float rotationValue3)
    {
        print("调用更新旋转函数");
        bones[0].localRotation = Quaternion.Euler(rotationValue * new Vector3(0, 360, 0));
        bones[1].localRotation = Quaternion.Euler(0.6F * rotationValue1 * new Vector3(360, 0, 0));
        bones[2].localRotation = Quaternion.Euler(0.4F * rotationValue2 * new Vector3(360, 0, 0));
        bones[3].localRotation = Quaternion.Euler(0.2F * rotationValue3 * new Vector3(360, 0, 0));
    }

public class MathFunctions
{
    private const string MathPath = "MathLibT.dll";

    // 声明外部函数
    [DllImport(MathPath)]
    public static extern int Kinematics(double[] JPOS, ref double PO, ref double PA, ref double PB, ref int FLAG, ref double POS);  

    [DllImport(MathPath)]
    public static extern int KinematicsA(double[] JPOS, ref double PO, ref double PA, ref double PB, ref int FLAG, ref double POS);  

    [DllImport(MathPath)]
    public static extern int invKinematics(double[] PO, double[] PA, double[] PB, int FLAG, ref double JPOS);  

    [DllImport(MathPath)]
    public static extern int setParam(double[] SIZE, double[] LIMIT);  

    [DllImport(MathPath)]
    public static extern int getParam(ref double[] SIZE, ref double[] LIMIT);  
}

public void btnteachonlick(){ //测试数据
         // 创建MathFunctions类的实例（虽然方法是静态的，但通常不需要实例）  
        MathFunctions mathFuncs = new MathFunctions();  
        JointPositions jpos = new JointPositions(45.0, 45.0, 0.0, -30.0, 5.0, 60.0);

        // JointPositions jpos2 = new JointPositions(1,2,3,4,5,6);
        // 创建一个整数数组，并初始化为(0, 0, 100)  
        double[] po = new double[3] { 0, 0, 0 };  
        double[] pa = new double[3] { 0, 0, 100 };  
        double[] pb = new double[3] { -100, 0, 0 };  

        int FLAG = 0;
        double[] Q = new double[6];
            double[] PO = new double[3];
            double[] PA = new double[3];
            double[] PB = new double[3];
            double[,] POS = new double[3,3];

        //测试数据
            Q[0] = -88 * Math.PI / 180;
            Q[1] = -70 * Math.PI / 180;
            Q[2] = 35;
            Q[3] = -13;
            Q[4] = -17;
            Q[5] = 161 * Math.PI / 180;



        int flag = 2;
        double pos = 1;
        double jointPositionValue = jpos.J1;   
 try  
    {  
        MathFunctions.Kinematics(Q, ref PO[0], ref PA[0], ref PB[0], ref FLAG, ref POS[0, 0]);
        print(POS[0, 0].ToString());
        print(POS[0, 1].ToString());
        print(POS[0, 2].ToString());
        print(POS[1, 0].ToString());
        print(POS[1, 1].ToString());
        print(POS[1, 2].ToString());
        print(POS[2, 0].ToString());
        print(POS[2, 1].ToString());
        print(POS[2, 2].ToString());
        print(PO[0].ToString());
        print(PO[1].ToString());
        print(PO[2].ToString());
        print(PA[0].ToString());
        print(PA[1].ToString());
        print(PA[2].ToString());
        print(PB[0].ToString());
        print(PB[1].ToString());
        print(PB[2].ToString());



        //执行逆解
         FLAG = 0;
        
        int rtn = MathFunctions.invKinematics(PO, PA, PB, FLAG, ref Q[0]); 
  
        // // 检查返回值以确定调用是否成功  
        // print(result);
        test.text = rtn.ToString(); 
        // result -= 1;
        posx.text = rtn.ToString();
        
        print((Q[0] * 180 / Math.PI).ToString());
        print((Q[1] * 180 / Math.PI).ToString());
        print(Q[2].ToString());
        print(Q[3].ToString());
        print(Q[4].ToString());
        print((Q[5] * 180 / Math.PI).ToString());


    }  
    catch (Exception ex)  
    {  
        // 捕获任何异常，并处理它们  
        print("An error occurred: " + ex.Message);  
        // 这里可以添加额外的错误处理逻辑，如日志记录、重置状态等  
    }  
}





public class JointPositions  
{  
    public double J1;  
    public double J2;  
    public double J3;  
    public double J4;  
    public double J5;  
    public double J6;  
  
    // 构造函数，可以用来初始化关节坐标  
    public JointPositions(double j1, double j2, double j3, double j4, double j5, double j6)  
    {  
        J1 = j1;  
        J2 = j2;  
        J3 = j3;  
        J4 = j4;  
        J5 = j5;  
        J6 = j6;  
    }  
  
    // 可以添加其他方法来操作关节坐标，比如转换、计算等  
}



// public void Motion_Interpolation(TMotionData md,string gcd , string repos){
    public void Motion_Interpolation(){
    //1.设置起点j1到j6  设置笛卡尔坐标系1和姿态1
    //2.设置终点j1到j6  设置笛卡尔坐标系2和姿态2
    double[] Q = new double[6];
                double[] PO = new double[3];
            double[] PA = new double[3];
            double[] PB = new double[3];
            double[,] POS = new double[3,3];
            int FLAG = 0;

            //测试数据
            Q[0] = 0 * Math.PI / 180;
            Q[1] = 0 * Math.PI / 180;
            Q[2] = 0;
            Q[3] = 0;
            Q[4] = 0;
            Q[5] = 0 * Math.PI / 180;

     MathFunctions.Kinematics(Q, ref PO[0], ref PA[0], ref PB[0], ref FLAG, ref POS[0, 0]);

        // print(PO[0].ToString()); //起点原点xyz数据
        // print(PO[1].ToString());
        // print(PO[2].ToString());

        double vx1,vy1,vz1;//起点xyz
        double vx2,vy2,vz2;//终点xyz
        //现在已知起点 x353.968542494924 y0 z553  终点 x346.75 y-61.14 z468.78 （10 0 10 0 0 0 J1-J6）
        vx1 = 353.96;
        vy1 = 0;
        vz1 = 553;
        vx2 = 346.75;
        vy2 = -61.14;
        vz2 = 468.78;
            PA[0] = 453.968542494924;
            PA[1] = 0;
            PA[2] = 553.168542494919;
            PB[0] = 353.968542494923;
            PB[1] = 4.7;
            PB[2] = 453.168542494932;
            

        print(vx1);
        print(vy1);
        print(vz1);

        float distance = (float)Math.Sqrt(Math.Pow(vx2 - vx1, 2) + Math.Pow(vy2 - vy1, 2) + Math.Pow(vz2 - vz1, 2));
        print("distance: "+ distance);

        int hd = 10; // 插值步数，这里设置为10作为示例   //hd步数  应该由总距离除精度计算过来 此为测试
  
        // 计算每步的增量  
        double dx = (vx2 - vx1) / (hd - 1);  
        double dy = (vy2 - vy1) / (hd - 1);  
        double dz = (vz2 - vz1) / (hd - 1);  

 //初始化二维数组来存储插值点的位置  
        double[,] posarraysq = new double[hd, 3];  
  
        // 进行插值并打印每一步的位置，同时将位置存储到数组中  
        for (int i = 0; i < hd; i++)  
        {  
            double x = vx1 + i * dx;  
            double y = vy1 + i * dy;  
            double z = vz1 + i * dz;  
  
            // 将插值点的坐标存储到数组中  
            posarraysq[i, 0] = x;  
            posarraysq[i, 1] = y;  
            posarraysq[i, 2] = z;  }


        //int rtn = MathFun.invKinematics(PO, PA, PB, FLAG, ref Q[0]);

        //已获得每一步的位置 then在把每一步位置逆解发送给单片机运动控制器
        

}



public struct TMotionData  
{  
    public int mtype;  
    public int mmode;  
    public int mp_count;  
    public float[] mp0;  
    public float[] mp1;  
    public float[] mp2;  
    public float mg;  
    public float mm;  
    public float mf;  
    public float mk;  
    public float mu;  
    public int mo;  
    public int mp;  
    public int ms;  
    public int mt;  
    public int mr;  
    public int mv;  
    public int me;  
    public int mi;  
    public int mn;  
  
    public TMotionData(int axisNum)  
    {  
        mtype = 0;  
        mmode = 0;  
        mp_count = 0;  
        mp0 = new float[axisNum];  
        mp1 = new float[axisNum];  
        mp2 = new float[axisNum];  
        mg = 0.0f;  
        mm = 0.0f;  
        mf = 0.0f;  
        mk = 0.0f;  
        mu = 0.0f;  
        mo = 0;  
        mp = 0;  
        ms = 0;  
        mt = 0;  
        mr = 0;  
        mv = 0;  
        me = 0;  
        mi = 0;  
        mn = 0;  
    }  
}




}
}