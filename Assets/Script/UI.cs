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

public class UI : MonoBehaviour
{
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

    public Transform[] bones;
    public Button btncom;
    public Button btnfresh;
    public Button btnoffbrake;
    private string sliderName;
    public Dropdown dropdownMenu;
    private bool _continue = true;

    public string portName = "COM5"; //串口名
    public int baudRate = 1000000; //波特率 0是1000000，1是512000，2是230400，3是115200，4是9600
    public Parity parity = Parity.None; //效验位
    public int dataBits = 8; //数据位
    public StopBits stopBits = StopBits.One; //停止位
    SerialPort sp = null;
    private Thread readThread;


    public Button btnconnect;
    public Button btngcode;
    [SerializeField] public InputField inputaddr;
    [SerializeField] public InputField inputport;

    [SerializeField] private float value;
    [SerializeField] private float value1;
    [SerializeField] private float value2;
    [SerializeField] private float value3;
    [SerializeField] public InputField inputgcode;
    private Action action;

    private void Awake()
    {
        ThreadCrossHelper.Instance.init();
    }


    private void Start()
    {
        // 获取Dropdown组件
        // dropdownMenu = this.transform.GetChild(2).GetChild(4).GetComponent<Dropdown>();

        // 添加下拉菜单选项
        // AddDropdownOptions();

        InitDropdownMenu();

        //  transform.Find("")  只能找子物体 不能找子子物体
        Init();
        sliders[0].onValueChanged.AddListener((f) => { value = f; });
        sliders[1].onValueChanged.AddListener((f) => { value1 = f; });
        sliders[2].onValueChanged.AddListener((f) => { value2 = f; });
        sliders[3].onValueChanged.AddListener((f) => { value3 = f; });
        dropdownMenu.onValueChanged.AddListener((f) => { OnDropdownValueChanged(); });


        btns[0].onClick.AddListener(() => { print(fieldx.text + fieldy.text + fieldz.text); });

        // 连接串口
        btncom.onClick.AddListener(() => { ConnectButtonClick(); });

        //刷新
        btnfresh.onClick.AddListener(() => { RefreshSerialPorts(); });

        btnconnect.onClick.AddListener(() => { ConnectTcp(); });

        Debug.Log("btngcode: " + (btngcode != null ? "Not null" : "Null"));
        Debug.Log("inputgcode: " + (inputgcode != null ? "Not null" : "Null"));

        btngcode.onClick.AddListener(() => { SendSerialData(inputgcode.text); });

        btnoffbrake.onClick.AddListener(() => { offbrake(); });
    }

    private void Update()
    {
        bones[0].localRotation = Quaternion.Euler(value * new Vector3(0, 360, 0));
        bones[1].localRotation = Quaternion.Euler(0.6F * value1 * new Vector3(360, 0, 0));
        bones[2].localRotation = Quaternion.Euler(0.4F * value2 * new Vector3(360, 0, 0));
        bones[3].localRotation = Quaternion.Euler(0.2F * value3 * new Vector3(360, 0, 0));
        if (action != null)
        {
            action();
            action = null;
        }
    }

    private void Init()

    {
        System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
        string[] ports = SerialPort.GetPortNames();
        print(ports);
        if (ports.Length > 0)
        {
            Debug.Log("Available Serial Ports:");

            foreach (string port in ports)
            {
                Debug.Log(port);
            }
        }
        else
        {
            Debug.Log("No serial ports available.");
        }

        print(sp);
        var sliderZone = this.transform.GetChild(0);
        var btnZone = this.transform.GetChild(1);
        var tcpZone = this.transform.GetChild(4);
        btnoffbrake = this.transform.GetChild(5).GetComponent<Button>();
        fieldx = this.transform.GetChild(1).GetChild(1).GetComponent<InputField>();
        fieldy = this.transform.GetChild(1).GetChild(2).GetComponent<InputField>();
        fieldz = this.transform.GetChild(1).GetChild(3).GetComponent<InputField>();
        btncom = this.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        btnfresh = this.transform.GetChild(2).GetChild(5).GetComponent<Button>();
        inputaddr = this.transform.GetChild(4).GetChild(1).GetComponent<InputField>();
        inputport = this.transform.GetChild(4).GetChild(2).GetComponent<InputField>();
        btnconnect = this.transform.GetChild(4).GetChild(0).GetComponent<Button>();

        btngcode = this.transform.GetChild(3).GetChild(0).GetComponent<Button>();
        inputgcode = this.transform.GetChild(3).GetChild(1).GetComponent<InputField>();
        for (int i = 0; i < sliderZone.childCount; i++)
        {
            var child = sliderZone.GetChild(i); //slider第一个子物体 
            sliders.Add(child.GetComponent<Slider>());
        }

        for (int i = 0; i < btnZone.childCount; i++)
        {
            var child = btnZone.GetChild(i);
            btns.Add(child.GetComponent<Button>());
        }
    }

    private void InitDropdownMenu()
    {
        // Get the Dropdown component
        dropdownMenu = this.transform.GetChild(2).GetChild(4).GetComponent<Dropdown>();

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
            dropdownMenu.ClearOptions();
            dropdownMenu.AddOptions(options);
        }
        catch (Exception e)
        {
            Debug.LogError("刷新串口时出现错误：" + e.Message);
        }
    }


    private void ConnectButtonClick()

    {
        //find the selected index
        int menuIndex = dropdownMenu.GetComponent<Dropdown>().value;

        //find all options available within the dropdown menu
        List<Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<Dropdown>().options;

        //get the string value of the selected index
        string value = menuOptions[menuIndex].text;
        print(value);

        portName = value;
        print("public baudrate is :" + baudRate);
        sp = new SerialPort(portName, 1000000, parity, dataBits, stopBits);
        sp.ReadTimeout = 5000;


        sp.Open();
        print("connect com");
        // StartCoroutine(SendQuestionCoroutine());
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


    private IEnumerator SendQuestionCoroutine()
    {
        while (true)
        {
            // 发送问号
            SendQuestion();

            // 等待一秒
            yield return new WaitForSeconds(1f);
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


    private void ProcessReceivedData(string receivedData)
    {
        print(receivedData);

        // 检查是否包含ABSPos
        if (receivedData.Length > 70 && receivedData.Contains("ABSPos:"))
        {
            int absPosIndex = receivedData.IndexOf("ABSPos:");
            string remainingString = receivedData.Substring(absPosIndex + "ABSPos:".Length);
            int endIndex = remainingString.IndexOf("|");

            // 提取满足条件的部分
            string absPosData = remainingString.Substring(0, endIndex);
            print("ABSPos Data: " + absPosData);

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
                    // 执行需要在主线程中完成的操作
                    Debug.Log("This is executed on the main thread.");
                    UpdateRotation(x, y, z, a);
                });
            }
            else
            {
                Debug.LogError("Invalid number of values in ABSPos data.");
            }
        }
        else
        {
            print("less than 70 or does not contain ABSPos");
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
}