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
using System.IO;
// using static SFB.StandaloneFileBrowser;
using SFB;
public class UIFileCommand : MonoBehaviour
{   


    public InputField inputField;
    // public Text gCodeText;    
    private TcpClient tcpClient;
    private string previousData = "";
    private NetworkStream networkStream;
    public List<Slider> sliders = new List<Slider>();

    [SerializeField] public InputField field;
    [SerializeField] public InputField fieldx;
    [SerializeField] public InputField fieldy;
    [SerializeField] public InputField fieldz;

    public Transform[] bones;

    private string sliderName;
    public Dropdown dropdownMenu;
    private bool _continue = true;




    //按钮
    public Button btnsave;
    public Button btnopen;
    public Button btnsaveas;
    public Button btnimport;
    public Button btnorigin;
    public Button btninsert;
    public Button btnchange;
    public Button btncopy;
    public Button btndelete;
    public Button btnpaste;
    public Button btnclear;

    public Text textshow;

    [SerializeField] public InputField gcode;
    

    [SerializeField] public InputField inputaddr;
    [SerializeField] public InputField inputport;

    [SerializeField] private float value;
    [SerializeField] private float value1;
    [SerializeField] private float value2;
    [SerializeField] private float value3;

    // private Action action;
    //     private void Awake()
    // {
    //     ThreadCrossHelper.Instance.init();
    // }
    // private float arg
    // {
    //     set
    //     {
    //         bones[0].localRotation = Quaternion.Euler(this.value * new Vector3(0, 360, 0));
    //         bones[1].localRotation = Quaternion.Euler(0.6F * value1 * new Vector3(360, 0, 0));
    //         bones[2].localRotation = Quaternion.Euler(0.4F * value2 * new Vector3(360, 0, 0));
    //         bones[3].localRotation = Quaternion.Euler(0.2F * value3 * new Vector3(360, 0, 0));
    //     }
    // }


    private void Start()
    {
        Init();
       // 设置InputField的行为
        inputField.lineType = InputField.LineType.MultiLineNewline;
        btnorigin.onClick.AddListener(() => { AddGCodeOrigin(); }); // 添加按钮点击事件   
        // 注册输入变化事件
        // inputField.onValueChanged.AddListener(OnInputValueChanged);        
         btnsave.onClick.AddListener(() => { OpenFileExplorerForSave(); });
         btnsaveas.onClick.AddListener(() => { OpenFileExplorerForSaveas(); });   
         btnopen.onClick.AddListener(() => { OpenFileExplorerForOpen(); }); 
        // //刷新
        // btnfresh.onClick.AddListener(() => { RefreshSerialPorts(); });

        // btnconnect.onClick.AddListener(() => { ConnectTcp(); });

        // btngcode.onClick.AddListener(() => { SendSerialData("here"); });
        LoadDefaultFileContent();
    }

    private void Update()
    {
        // arg = value;
        // if (action != null)
        // {
        //     action();
        //     action = null;
        // }
    }

    //     private void OnInputValueChanged(string newValue)
    // {
        
    //     // 当用户输入变化时更新显示的G代码文本

    //     gCodeText.text = newValue;
    // }

private void AddGCodeOrigin()
{
    // 获取当前InputField的文本
    string currentText = inputField.text;

    // 在当前文本的最后一行添加字符串 "G00 X0.0 Y0.0 Z0.0 A0.0 B0.0 C0.0"，并换行
    string newText = currentText + "\nG00 X0.0 Y0.0 Z0.0 A0.0 B0.0 C0.0";

    // 将新文本设置为InputField的文本
    inputField.text = newText;

    // 移动光标到新插入的文本末尾
    inputField.caretPosition = inputField.text.Length;
}



    public void InsertText(string newText)
{
    int caretPosition = inputField.caretPosition;
    inputField.text = inputField.text.Insert(caretPosition, newText);
}

public void DeleteSelectedText()
{
    inputField.text = inputField.text.Remove(inputField.selectionAnchorPosition, inputField.selectionFocusPosition - inputField.selectionAnchorPosition);
}




    private void Init()

    {
        // textshow = this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        // textshow.text = "test";

        // gcode = this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<InputField>();
        
        var filezone = this.transform.GetChild(0);
        var commandzone = this.transform.GetChild(1);
        var scrollzone = this.transform.GetChild(2);
        btnsave = this.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        btnopen = this.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        btnsaveas = this.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        btnimport = this.transform.GetChild(0).GetChild(3).GetComponent<Button>();
        btnorigin = this.transform.GetChild(0).GetChild(4).GetComponent<Button>();
        // btninsert = this.transform.GetChild(1).GetChild(1).GetComponent<Button>();
        // btnchange = this.transform.GetChild(1).GetChild(2).GetComponent<Button>();
        // btncopy = this.transform.GetChild(1).GetChild(3).GetComponent<Button>();
        // btndelete = this.transform.GetChild(1).GetChild(4).GetComponent<Button>();
        // btnpaste = this.transform.GetChild(1).GetChild(5).GetComponent<Button>();
        // btnclear = this.transform.GetChild(1).GetChild(6).GetComponent<Button>();

        inputField = this.transform.GetChild(1).GetComponent<InputField>();
        // gCodeText = this.transform.GetChild(2).GetComponent<Text>();

    }


private void OpenFileExplorerForSaveas()
{
    ExtensionFilter[] extensions = new[] { new ExtensionFilter("Text Files", "txt") };

    SFB.StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "newFile", extensions, (string path) =>
    {
        if (!string.IsNullOrEmpty(path))
        {
            // 获取InputField中的文本
            string inputFieldText = inputField.text;

            // 保存文本到文件
            System.IO.File.WriteAllText(path, inputFieldText);

            Debug.Log("File saved successfully.");
        }
        else
        {
            Debug.Log("File selection cancelled.");
        }
    });
}

        private void OpenFileExplorerForSave()
{
    // 定义默认保存路径为Unity项目的Assets目录下
    string defaultPath = Application.dataPath + "/SavedFile.txt";


    string inputFieldText = inputField.text;

    // 保存文本到文件
    File.WriteAllText(defaultPath, inputFieldText);
    print(inputFieldText + "saved in" + defaultPath);
}

    private void LoadDefaultFileContent()
{
    // 定义默认保存路径为Unity项目的Assets目录下
    string defaultPath = Application.dataPath + "/SavedFile.txt";

    // 检查文件是否存在
    if (File.Exists(defaultPath))
    {
        // 读取文件内容
        string fileContent = File.ReadAllText(defaultPath);

        // 将文件内容加载到InputField的text中
        inputField.text = fileContent;

        Debug.Log("File content loaded successfully.");
    }
    else
    {
        Debug.Log("Default file not found.");
    }
}


    private void OpenFileExplorerForOpen()
    {
        ExtensionFilter[] extensions = new[] { new ExtensionFilter("Text Files", "txt") };

        // 打开文件选择面板
        SFB.StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensions, false, (string[] paths) =>
        {
            if (paths != null && paths.Length > 0)
            {
                // 读取选择的文件内容逐行
                using (StreamReader reader = new StreamReader(paths[0]))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // 处理每一行的内容
                        Debug.Log("Line: " + line);

                        // // 在UI上显示每一行的内容（如果有需要）
                        // if (textshow != null)
                        // {
                        //     textshow.text += line + "\n";
                        // }
                    }
                }
            }
            else
            {
                Debug.Log("File selection cancelled.");
            }
        });
    }
}