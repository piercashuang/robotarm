using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

namespace XP.TableModel.Test
{
    /// <summary>
    /// 测试表格
    /// </summary>
    public class m2i4zone : MonoBehaviour
    {   
        
        public Button btnfresh,_openallio, _closeallio, _closeio;
        public comconnect comConnectScript;
        
        private List<List<string>> _TableData; // 用于存储从文本文件加载的原始数据
        private List<ioData> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表

        public Table _Table;
        public TMP_InputField _ValueInput;
        
        private int _IniColumn = 30, _IniRow = 40;
        public string _DefaultTxtPath = "Assets/ioconfig.txt"; // 修改为你的配置文件路径
        private int iniColumnCount, iniRowCount;
        // Start is called before the first frame update
        /// <summary>
        /// 测试绑定数组
        /// </summary>
        // Declare four arrays to store values from each column
        string arrayColumn1 = "";
        string arrayColumn2 = "";
        string arrayColumn3 = "";
        string arrayColumn4 = "";

        // 声明4个链表
        LinkedList<string> listColumn1 = new LinkedList<string>();
        LinkedList<string> listColumn2 = new LinkedList<string>();
        LinkedList<string> listColumn3io = new LinkedList<string>();
        LinkedList<string> listColumn4 = new LinkedList<string>();








private void _TestBindArr()
{
    foreach (var row in _TableData)
    {
        // Check if the row is empty or contains only whitespace
        if (!string.IsNullOrWhiteSpace(string.Join("", row)))
        {
            Debug.Log($"{row[0]}, {row[1]}, {row[2]}");
        }
    }

    // 调用 _TestBindArr2 方法
    _TestBindArr2(_TableData);
}


private void _TestBindArr2(List<List<string>> data)
{
    // 清空表格
    _Table._ClearTable();

    // 创建 ioData 列表
    List<ioData> _TestDatas = new List<ioData>();

    // 循环创建 TestData 对象并添加到列表
    foreach (var row in data)
    {
        _TestDatas.Add(new ioData { name = row[0], number =row[1], status =row[2],comment = row[3] });
        // Add more properties if you have additional columns
    }

    // 绑定到表格
    _Table._BindArray(_TestDatas);

    // 刷新表格
    _Table._Refresh();
}



public void updateio()
{
    // 访问静态字段
    print("m3i1value: " + comconnect.m3i1_Ivalue);

    // 将字符串转换为整数
    int intIValue = int.Parse(comconnect.m3i1_Ivalue);

    // 将整数转换为二进制字符串
    string binaryIString = Convert.ToString(intIValue, 2);

    // 检查 binaryIString 是否小于8个字符
    if (binaryIString.Length < 8)
    {
        // 在字符串后面补0，直到达到8个字符
        binaryIString = binaryIString.PadRight(8, '0');
    }

    print("二进制I值: " + binaryIString);

    // 访问静态字段
    print("m3i1value: " + comconnect.m3i1_Ovalue);

    // 将字符串转换为整数
    int intOValue = int.Parse(comconnect.m3i1_Ovalue);

    // 将整数转换为二进制字符串
    string binaryOString = Convert.ToString(intOValue, 2);

    // 检查 binaryOString 是否小于16个字符
    if (binaryOString.Length < 16)
    {
        // 在字符串后面补0，直到达到16个字符
        binaryOString = binaryOString.PadRight(16, '0');
    }

    print("二进制O值: " + binaryOString);

    // 进行其他操作，如拼接字符串、遍历链表等

    string linkedlistconcat = binaryIString + binaryOString;

    PrintLinkedList(listColumn3io);

    // 遍历 linkedlistconcat，替换每一位
    var node = listColumn3io.First;

    foreach (char bit in linkedlistconcat)
    {
        if (node != null)
        {
            // 替换当前节点的值
            node.Value = bit.ToString();

            // 移动到下一个节点
            node = node.Next;
        }
        else
        {
            // 处理 listColumn3io 的长度小于 linkedlistconcat 的情况
            break;
        }
    }

    // 输出更新后的 listColumn3io
    PrintLinkedList(listColumn3io);

    // 获取列的索引，假设为2
    int columnIndex = 2;

    // 获取链表的第一个节点
    var nodevalue = listColumn3io.First;

    // 获取行的起始索引，假设为0
    int rowIndex = 0;

    // 循环遍历链表，将每个节点的值赋值给表格的相应单元格
    while (nodevalue != null)
    {
        // 使用 _Table._SetCellData 方法设置单元格数据
        _Table._SetCellData(columnIndex, rowIndex, nodevalue.Value);

        // 移动到下一个节点
        nodevalue = nodevalue.Next;

        // 增加行索引
        rowIndex++;
        }



    // 进行其他操作，如生成测试数据、清空表格、绑定到表格、刷新表格等
    // ...

    // 最后刷新表格
    //_Table._Refresh();





    // // 生成测试数据
    // List<ioData> _TestDatas = new List<ioData>
    // {
    //     new ioData { name = "HMC_OUT", number = "10", status = "(stp pulse)", comment = "(stp pulse)" },
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(delay)", comment = "(stp pulse)" },
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)", comment = "(stp pulse)" },
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)", comment = "(stp pulse)" },
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)" , comment = "(stp pulse)"},
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)", comment = "(stp pulse)" },
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)" , comment = "(stp pulse)"},
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)" , comment = "(stp pulse)"},
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)" , comment = "(stp pulse)"},
    //     new ioData { name = "HMC_OUT_0", number = "10", status = "(inv)", comment = "(stp pulse)" },
    // };

    // // 清空表格
    // _Table._ClearTable();

    // // 绑定到表格
    // _Table._BindArray(_TestDatas);

    // // Refresh the table
    // _Table._Refresh();
}



private void LoadTxtFile(string filePath)
{
    // Clear the two-dimensional list to prevent duplicate data loading
    _TableData = new List<List<string>>();

    try
    {
        // Read all lines from the file
        string[] lines = System.IO.File.ReadAllLines(filePath);

        arrayColumn1 = "";
        arrayColumn2 = "";
        arrayColumn3 = "";
        arrayColumn4 = "";
        foreach (string line in lines)
        {
            

                // Split the line by '=' and remove any leading or trailing spaces
                string[] keyValue = line.Split(',');

                // Check if the split resulted in two parts
                if (keyValue.Length == 4)
                {
                    // Extract values from the first column
                    string column1 = keyValue[0];
                    listColumn1.AddLast(column1);

                    // Extract values from the second column
                    string column2 = keyValue[1];
                    listColumn2.AddLast(column2);

                    string column3 = keyValue[2];
                    listColumn3io.AddLast(column3);

                    string column4 = keyValue[3];
                    listColumn4.AddLast(column4);
                   
                    _TableData.Add(new List<string> { column1, column2, column3 ,column4});
                }
            
        }
        // arrayColumn1 = arrayColumn1.Trim();
        // arrayColumn2 = arrayColumn2.Trim();
        // arrayColumn3 = arrayColumn3.Trim();
        // arrayColumn4 = arrayColumn4.Trim();


    }
    catch (Exception e)
    {
        Debug.LogError($"Error reading file {filePath}: {e.Message}");
    }
}



private void PrintLinkedList<T>(LinkedList<T> linkedList)
{
    string output = "Linked List: ";
    var currentNode = linkedList.First;
    
    while (currentNode != null)
    {
        output += currentNode.Value + " ";
        currentNode = currentNode.Next;
    }

    Debug.Log(output);
}





private void Start()
{
    string dataString = "HMC_IN_0,HMC DIN,0,0\n" +
                    "HMC_IN_1,HMC DIN,0,0\n" +
                    "HMC_IN_2,HMC DIN,0,0\n" +
                    "HMC_IN_3,HMC DIN,0,0\n" +
                    "HMC_IN_4,HMC DIN,0,0\n" +
                    "HMC_IN_5,HMC DIN,0,0\n" +
                    "HMC_IN_6,HMC DIN,0,0\n" +
                    "HMC_IN_7,HMC DIN,0,0\n" +
                    "HMC_OUT_1,HMC DOUT,0,0\n" +
                    "HMC_OUT_2,HMC DOUT,0,0\n" +
                    "HMC_OUT_3,HMC DOUT,0,0\n" +
                    "HMC_OUT_4,HMC DOUT,0,0\n" +
                    "HMC_OUT_5,HMC DOUT,0,0\n" +
                    "HMC_OUT_6,HMC DOUT,0,0\n" +
                    "HMC_OUT_7,HMC DOUT,0,0\n" +
                    "HMC_OUT_8,HMC DOUT,0,0\n" +
                    "HMC_OUT_9,HMC DOUT,0,0\n" +
                    "HMC_OUT_10,HMC DOUT,0,0\n" +
                    "HMC_OUT_11,HMC DOUT,0,0\n" +
                    "HMC_OUT_12,HMC DOUT,0,0\n" +
                    "HMC_OUT_13,HMC DOUT,0,0\n" +
                    "HMC_OUT_14,HMC DOUT,0,0\n" +
                   
                    // Add more data lines here
                    "HMC_OUT_15,HMC DOUT,0,0";


    _DefaultTxtPath= Application.persistentDataPath+"/ioconfig.txt";
    print("ioconfig: "+_DefaultTxtPath);
    // 记录初始化时的列和行的数量
    iniColumnCount = _IniColumn;
    iniRowCount = _IniRow;

    // 如果配置文件不存在，创建默认配置文件
    // if (!File.Exists(_DefaultTxtPath))
    // {
    //     CreateDefaultTxtFile(_DefaultTxtPath);
    // }

    // 加载配置文件
    LoadTxtFile(_DefaultTxtPath);

    // 添加按钮点击事件
    // _openio.onClick.AddListener(() =>
    // {
    //     openio_printrow();
    // });
    // _AddColumn.onClick.AddListener(() =>
    // {
    //     StartCoroutine(_AddColumnClick());
    // });
    // _ClearButton.onClick.AddListener(() =>
    // {
    //     _Table._ClearTable();
    // });
    // _RemoveRow.onClick.AddListener(_RemoveRowClick);
    // _RemoveColumn.onClick.AddListener(_RemoveColumnClick);

    // _ChangeSelectText.onClick.AddListener(__ChangeSelectText);
    btnfresh.onClick.AddListener(updateio);

    // 初始化表格列和行
    for (int i = 0; i < iniColumnCount; i++)
    {
        _Table._AddColumn();
    }
    
    //  _LoadTableDataFromTxtFile(_TableData);
    _TestBindArr();


    LoadTxtString(dataString);
    //_Table._SetCellData(2,1,"test");
}




        // private void openio_printrow()
        // {
            
        //     // 将选中单元格的显示数据设置为输入框的文本
        //     foreach (var item in _Table._CurrentSelectedCellDatas)
        //     {
        //         print(item._Row);
        //                     if (item._Row >7){
        //         rownum = (item._Row-7).ToString();
        //                     if(rownum.Length == 1){
        //         rownum= "0"+rownum;
        //     }

        //     rownum = "M12." + rownum;
        //     print("send openio cmd: "+ rownum);


        //     }
        //     }
        // }

        private int _GetIndex()
        {
            // 从输入框获取索引
            int x;
            int.TryParse(_ValueInput.text, out x);
            return x;
        }

        private IEnumerator _AddRowClick()
        {
            var _count = 1;
            for (int i = 0; i < _count; i++)
            {
                yield return null;
                // 添加行并设置每个单元格的数据
                var _row = _Table._AddRow();
                for (int c = 0; c < _Table._HeaderColumn._HeaderCellDatas.Count; c++)
                {
                    //设置单元格数据为列索引+行索引
                    _Table._SetCellData(c, _row._Index, c + "," + _row._Index);
                }
            }
        }

        private IEnumerator _AddColumnClick()
        {
            var _count = _GetIndex();
            for (int i = 0; i < _count; i++)
            {
                yield return null;
                // 添加列并设置每个单元格的数据
                var _column = _Table._AddColumn();
                for (int r = 0; r < _Table._HeaderRow._HeaderCellDatas.Count; r++)
                {
                    //设置单元格数据为列索引+行索引
                    _Table._SetCellData(_column._Index, r, _column._Index + "," + r);
                }
            }
        }

        private void _RemoveRowClick()
        {
            // 移除选中行
            _Table.RemoveSelectedRow();
        }

        private void _RemoveColumnClick()
        {
            // 移除选中列
            _Table.RemoveSelectedColumn();
        }


    private void LoadTxtString(string dataString)
    {
        // Clear the lists to prevent duplicate data loading
        _TableData.Clear();
        listColumn1.Clear();
        listColumn2.Clear();
        listColumn3io.Clear(); // Use listColumn3io instead of listColumn3
        listColumn4.Clear();

        // Split the dataString into lines
        string[] lines = dataString.Split('\n');

        foreach (string line in lines)
        {
            // Split the line by ',' and remove any leading or trailing spaces
            string[] keyValue = line.Split(',');

            // Check if the split resulted in four parts
            if (keyValue.Length == 4)
            {
                string column1 = keyValue[0];
                string column2 = keyValue[1];
                string column3 = keyValue[2];
                string column4 = keyValue[3];

                // Add values to the respective linked lists
                listColumn1.AddLast(column1);
                listColumn2.AddLast(column2);
                listColumn3io.AddLast(column3); // Use listColumn3io instead of listColumn3
                listColumn4.AddLast(column4);

                // Add the row to _TableData
                _TableData.Add(new List<string> { column1, column2, column3, column4 });
            }
            else
            {
                Debug.LogError("Invalid data format: " + line);
            }
        }
    }

    }

    
}
