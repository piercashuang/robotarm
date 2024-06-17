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
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace XP.TableModel.Test
{
    public class m3i1filecmd : MonoBehaviour
    {

        
        public Text txt_x;
        public Text txt_y;
        public Text txt_z;
        public Text txt_a;
        public Text txt_b;
        public Text txt_c;
        public GameObject imgtablecover;
        public Button yessave;
        public Button cancelsave;
        public Text Name;
        public Text Toolpos;
        public Text Comment;
        public Dropdown drptoolpos;
        public Button btnadd;
        public Button btnclosenewfile;
        public Button newfile;
        public Button editfile;
        public Button btncloseadd;
        public Button btncancel;
        public Button btnok;
        public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
        public List<List<string>> _TableData;
        public Table _Table;
        private List<cmdData> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表
        public InputField inputField;
        public InputField inputcontent;

        public InputField inputName;

        public InputField inputComment;

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


        public Button btnenter;
        [SerializeField] public InputField input1;

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


        private string filename = "09";
        private string filecomment = "焊接";
        private string filetoolpos = "00";


        [SerializeField] public InputField gcode;


        [SerializeField] public InputField inputaddr;
        [SerializeField] public InputField inputport;
        private bool unsavedfile = false;
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
            unsavedfile = false;
            GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("canvasm3i1");
            foreach (var view in viewsArray)
            {
                string viewID = view.name; // Assuming the view's name is the ID.
                views.Add(viewID, view);
                view.SetActive(false);
            }
            views["Canvas1"].SetActive(true); // Set the default view.
            btncloseadd.onClick.AddListener(() =>
            {
                print("btncloseadd");
                showScene("Canvas1");
            });
            btnadd.onClick.AddListener(() =>
            {
                print("btnadd");
                showScene("Canvaskeym3i1");
                print("btnaddfinish");
            });
            btnclosenewfile.onClick.AddListener(() =>
            {
                print("btnclosenewfile");
                showScene("Canvas1");
            });

            btncancel.onClick.AddListener(() =>
            {
                print("btncancel");
                showScene("Canvas1");
            });
            btnok.onClick.AddListener(() =>
            {
                print("btncloseadd");
                showSceneAndOk1("Canvas1");
            });

            newfile.onClick.AddListener(() =>
            {
                print("newfile");
                newornotnew();
            });
            editfile.onClick.AddListener(() =>
            {
                print("edit file");
                showtwoScene("Canvas1", "canvnewfile");
            });
            cancelsave.onClick.AddListener(() =>
            {
                print("edit file");
                showtwoScene("Canvas1", "canvnewfile");
            });
            yessave.onClick.AddListener(() =>
            {
                print("edit file");
                OpenFileExplorerForSaveas();
                showtwoScene("Canvas1", "canvnewfile");
            });
            // Init();
            // 设置InputField的行为
            // inputField.lineType = InputField.LineType.MultiLineNewline;
            // btnorigin.onClick.AddListener(() => { AddGCodeOrigin(); }); // 添加按钮点击事件   
            // 注册输入变化事件
            // inputField.onValueChanged.AddListener(OnInputValueChanged);        
            //Name.onValueChanged.AddListener(OnFileChanged); 


            btnsave.onClick.AddListener(() => { OpenFileExplorerForSaveas();closetablecover(); });
            btnsaveas.onClick.AddListener(() => { OpenFileExplorerForSaveas();closetablecover(); });
            btnopen.onClick.AddListener(() => { OpenFileExplorerForOpen();closetablecover(); });
            btnimport.onClick.AddListener(() => { AppendFileExplorerForOpen();closetablecover(); });
            

            
            // //刷新
            // btnfresh.onClick.AddListener(() => { RefreshSerialPorts(); });

            // btnconnect.onClick.AddListener(() => { ConnectTcp(); });

            // btngcode.onClick.AddListener(() => { SendSerialData("here"); });
            // LoadDefaultFileContent();
            btnenter.onClick.AddListener(() => { enteronclick();closetablecover(); });
            _TableData = new List<List<string>>();
                        // 绑定到表格
            // _Table._BindArray(_TestDatas);

            // 刷新表格
            _Table._ClearTable();
        }

        void closetablecover(){
            imgtablecover.SetActive(false);
        }
        void enteronclick()
        {
            if (input1.text != null)
            {
                
                _TableData.Add(new List<string> { input1.text });
                
                // _Table.Add(new List<string> { inputGcmd });  

                _Table._ClearTable();
                _BindArr();
                _Table._ClearTable();
                
                _BindArr();
                closeScene("Canvaskeym3i1");
            }

            // if (txt_x.text != null){
            //     print("closeCanvaskeym3i1");
            //      _TableData.Add(new List<string> {"G00"+"X"+ txt_x.text + "Y"+txt_y.text+ "Z"+txt_z.text+ "A"+txt_a.text+ "B"+txt_b.text+ "C"+txt_c.text });
                
            //     // _Table.Add(new List<string> { inputGcmd });  

            //     _Table._ClearTable();
            //     _BindArr();
            //     _Table._ClearTable();
                
            //     _BindArr();
            //     closeScene("Canvaskeym3i1");

            // }


        }


        private void OnFileChanged()
        {
            unsavedfile = true;
        }

        private void newornotnew()
        {
            print("unsavedfile is: " + unsavedfile);
            if (unsavedfile == false)
            {
                showtwoScene("Canvas1", "canvnewfile");
            }

            if (unsavedfile == true)
            {
                print("new file directly");
                showSceneAndOk("Canvas1", "canvverify");
            }
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
            inputField.text = inputField.text.Remove(inputField.selectionAnchorPosition,
                inputField.selectionFocusPosition - inputField.selectionAnchorPosition);
        }


        private void Init()

        {
            // textshow = this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
            // textshow.text = "test";

            // gcode = this.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<InputField>();

            // var filezone = this.transform.GetChild(0);
            // var commandzone = this.transform.GetChild(1);
            // var scrollzone = this.transform.GetChild(2);
            // btnsave = this.transform.GetChild(0).GetChild(0).GetComponent<Button>();
            // btnopen = this.transform.GetChild(0).GetChild(1).GetComponent<Button>();
            // btnsaveas = this.transform.GetChild(0).GetChild(2).GetComponent<Button>();
            // btnimport = this.transform.GetChild(0).GetChild(3).GetComponent<Button>();
            // btnorigin = this.transform.GetChild(0).GetChild(4).GetComponent<Button>();
            // btninsert = this.transform.GetChild(1).GetChild(1).GetComponent<Button>();
            // btnchange = this.transform.GetChild(1).GetChild(2).GetComponent<Button>();
            // btncopy = this.transform.GetChild(1).GetChild(3).GetComponent<Button>();
            // btndelete = this.transform.GetChild(1).GetChild(4).GetComponent<Button>();
            // btnpaste = this.transform.GetChild(1).GetChild(5).GetComponent<Button>();
            // btnclear = this.transform.GetChild(1).GetChild(6).GetComponent<Button>();

            // inputField = this.transform.GetChild(1).GetComponent<InputField>();
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
                    // string inputFieldText = "inputField.text";
                    // string filename = Name.text;
                    // string filecomment = Comment.text;
                    // string filetoolpos = Toolpos.text;

                    // string concattext = "";
                    // concattext+= filename+"/n"+filecomment+"/n"+filetoolpos+"/n";
                    // 保存文本到文件
                    // System.IO.File.WriteAllText(path, concattext);
                    // print("write: "+ concattext);
                    // System.IO.File.WriteAllText(path, filecomment);
                    // System.IO.File.WriteAllText(path, filetoolpos);
                    System.IO.File.WriteAllText(path, inputField.text);


                    Debug.Log("File saved successfully.");
                    unsavedfile = false;
                }
                else
                {
                    Debug.Log("File selection cancelled.");
                    unsavedfile = true;
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
                // inputField.text = fileContent;

                Debug.Log("File content loaded successfully.");
            }
            else
            {
                Debug.Log("Default file not found.");
            }
        }


        private void OpenFileExplorerForOpen()
        {
            _Table._ClearTable();
            _Table._Refresh();
            _TableData = new List<List<string>>();
            ExtensionFilter[] extensions = new[] { new ExtensionFilter("Text Files", "txt") };

            // 打开文件选择面板
            SFB.StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", extensions, false, (string[] paths) =>
            {
                if (paths != null && paths.Length > 0)
                {
                    // 读取选择的文件内容逐行
                    using (StreamReader reader = new StreamReader(paths[0]))
                    {
                        inputField.text = "";
                        inputField.text += (filename + "\n");
                        inputField.text += (filecomment + "\n");
                        inputField.text += (filetoolpos + "\n");

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // 处理每一行的内容
                            Debug.Log("Line: " + line);

                            _TableData.Add(new List<string> { line });
                            inputField.text += (line + "\n");
                            // // 在UI上显示每一行的内容（如果有需要）
                            // if (textshow != null)
                            // {
                            //     textshow.text += line + "\n";
                            // }
                        }
                    }

                    print("binding arr");
                    _BindArr();
                }
                else
                {
                    Debug.Log("File selection cancelled.");
                }
            });
        }


        private void AppendFileExplorerForOpen()
        {
            if (_TableData == null)
            {
                _TableData = new List<List<string>>();
            }

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
                            inputField.text += (line + "\n");
                            // 处理每一行的内容
                            Debug.Log("Line: " + line);
                            _TableData.Add(new List<string> { line });
                            // // 在UI上显示每一行的内容（如果有需要）
                            // if (textshow != null)
                            // {
                            //     textshow.text += line + "\n";
                            // }
                        }
                    }

                    print("binding arr");
                    _BindArr();
                }
                else
                {
                    Debug.Log("File selection cancelled.");
                }
            });
        }


        private void _BindArr()
        {
            // 调用 BindArr2 方法
            _BindArr2(_TableData);
        }


        private void _BindArr2(List<List<string>> data)
        {
            // 清空表格
            _Table._ClearTable();

            // 创建 TestData 列表
            List<cmdData> _TestDatas = new List<cmdData>();

            // 循环创建 TestData 对象并添加到列表
            foreach (var row in data)
            {
                _TestDatas.Add(new cmdData { name = row[0] });
                // Add more properties if you have additional columns
            }

            // 绑定到表格
            _Table._BindArray(_TestDatas);

            // 刷新表格
            _Table._Refresh();
        }


        private void showScene(string viewID)
        {
            views[viewID].SetActive(true);
        }


        private void showSceneAndOk1(string viewID)
        {
            foreach (var view in views.Values)
            {
                view.SetActive(false);
            }

            if (views.ContainsKey(viewID))
            {
                views[viewID].SetActive(true);
            }

            else
            {
                Debug.LogError("View with ID " + viewID + " not found.");
            }

            print(inputName.text);
            print(inputComment.text);
            print(drptoolpos.options[drptoolpos.value].text);
            Name.text = inputName.text;
            Comment.text = inputComment.text;
            Toolpos.text = drptoolpos.options[drptoolpos.value].text.ToString();
            unsavedfile = true;
            filename = Name.text;
            filecomment = Comment.text;
            filetoolpos = Toolpos.text;

            inputField.text += (filename + "\n");
            inputField.text += (filecomment + "\n");
            inputField.text += (filetoolpos + "\n");
        }


        private void showSceneAndOk(string viewID, string viewID2)
        {
            foreach (var view in views.Values)
            {
                view.SetActive(false);
            }

            if (views.ContainsKey(viewID))
            {
                views[viewID].SetActive(true);
            }

            if (views.ContainsKey(viewID2))
            {
                views[viewID2].SetActive(true);
            }

            else
            {
                Debug.LogError("View with ID " + viewID + " not found.");
            }

            print(inputName.text);
            print(inputComment.text);
            print(drptoolpos.options[drptoolpos.value].text);
            Name.text = inputName.text;
            Comment.text = inputComment.text;
            Toolpos.text = drptoolpos.options[drptoolpos.value].text.ToString();
            unsavedfile = true;
        }


        private void editshowSceneAndOk(string viewID)
        {
            foreach (var view in views.Values)
            {
                view.SetActive(false);
            }

            if (views.ContainsKey(viewID))
            {
                views[viewID].SetActive(true);
            }

            else
            {
                Debug.LogError("View with ID " + viewID + " not found.");
            }

            print(inputName.text);
            print(inputComment.text);
            print(drptoolpos.options[drptoolpos.value].text);
            Name.text = inputName.text;
            Comment.text = inputComment.text;
            Toolpos.text = drptoolpos.options[drptoolpos.value].text.ToString();
        }


        private void showtwoScene(string viewID, string viewID2)
        {
            views[viewID].SetActive(true);
            views[viewID2].SetActive(true);


            // }
            // if (views.ContainsKey(viewID))
            // {
            //     views[viewID].SetActive(true);
            // }
            // if(views.ContainsKey(viewID2)){
            //     views[viewID2].SetActive(true);
            // }
            // else
            // {
            //     Debug.LogError("View with ID " + viewID + " not found.");
            // }
        }

        private void closeScene(string viewID)
        {
            views[viewID].SetActive(false);


            // }
            // if (views.ContainsKey(viewID))
            // {
            //     views[viewID].SetActive(true);
            // }
            // if(views.ContainsKey(viewID2)){
            //     views[viewID2].SetActive(true);
            // }
            // else
            // {
            //     Debug.LogError("View with ID " + viewID + " not found.");
            // }
        }


        private void showAll(string viewID)
        {
            foreach (var view in views.Values)
            {
                view.SetActive(true);
            }
        }
    }
}