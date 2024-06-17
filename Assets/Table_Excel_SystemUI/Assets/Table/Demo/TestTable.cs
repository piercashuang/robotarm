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
    public class TestTable : MonoBehaviour
    {   
        private List<List<string>> _TableData; // 用于存储从文本文件加载的原始数据
private List<TestData> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表

        public Table _Table;
        public TMP_InputField _ValueInput;
        public Button _AddRow, _AddColumn, _RemoveRow, _RemoveColumn, _ChangeSelectText, _ClearButton, _TestBindArray;
        private int _IniColumn = 30, _IniRow = 40;
        public string _DefaultTxtPath = ""; // 修改为你的配置文件路径
        
        private int iniColumnCount, iniRowCount;
        // Start is called before the first frame update
        /// <summary>
        /// 测试绑定数组
        /// </summary>






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

    // 创建 TestData 列表
    List<TestData> _TestDatas = new List<TestData>();

    // 循环创建 TestData 对象并添加到列表
    foreach (var row in data)
    {
        _TestDatas.Add(new TestData { name = row[0], number =row[1], comment = row[2] });
        // Add more properties if you have additional columns
    }

    // 绑定到表格
    _Table._BindArray(_TestDatas);

    // 刷新表格
    _Table._Refresh();
}



private void _DefaultArr()
{
    // 生成测试数据
    List<TestData> _TestDatas = new List<TestData>
    {
new TestData { name = "$0", number = "10", comment = "(stp pulse)" },
new TestData { name = "$1", number = "25", comment = "(idl delay)" },
new TestData { name = "$2", number = "0", comment = "(stp inv)" },
new TestData { name = "$3", number = "5", comment = "(dir inv)" },
new TestData { name = "$4", number = "0", comment = "(stp en inv)" },
new TestData { name = "$5", number = "0", comment = "(io inv)" },
new TestData { name = "$6", number = "0", comment = "(prb inv)" },
new TestData { name = "$7", number = "0", comment = "(baud rate)" },
new TestData { name = "$8", number = "0", comment = "(a/d en)" },
new TestData { name = "$9", number = "63", comment = "(motors en)" },
new TestData { name = "$10", number = "255", comment = "(rpt)" },
new TestData { name = "$11", number = "0.010", comment = "(jnc dev)" },
new TestData { name = "$12", number = "40.000", comment = "(arc tol)" },
new TestData { name = "$13", number = "0", comment = "(rpt inch)" },
new TestData { name = "$14", number = "20", comment = "(motors min rpm)" },
new TestData { name = "$15", number = "32678", comment = "(err off out)" },
new TestData { name = "$20", number = "0", comment = "(sft lim)" },
new TestData { name = "$21", number = "1", comment = "(check data)" },
new TestData { name = "$22", number = "0", comment = "(hm cyc)" },
new TestData { name = "$23", number = "0", comment = "(hm dir inv)" },
new TestData { name = "$24", number = "25.000", comment = "(hm feed)" },
new TestData { name = "$25", number = "500.000", comment = "(hm seek)" },
new TestData { name = "$26", number = "250", comment = "(hm delay)" },
new TestData { name = "$27", number = "0.000", comment = "(hm pulloff)" },
new TestData { name = "$30", number = "1000", comment = "(sp rpm max)" },
new TestData { name = "$31", number = "0", comment = "(sp rpm min)" },
new TestData { name = "$32", number = "0", comment = "(laser)" },
new TestData { name = "$33", number = "3", comment = "(abs enable)" },
new TestData { name = "$34", number = "1", comment = "(sync pos enable)" },
new TestData { name = "$35", number = "30000", comment = "(max pos err)" },
new TestData { name = "$36", number = "0", comment = "(min pos err)" },
new TestData { name = "$100", number = "150.000", comment = "(X:stp/unit)" },
new TestData { name = "$101", number = "150.000", comment = "(Y:stp/unit)" },
new TestData { name = "$102", number = "150.000", comment = "(Z:stp/unit)" },
new TestData { name = "$103", number = "150.000", comment = "(A:stp/unit)" },
new TestData { name = "$104", number = "150.000", comment = "(B:stp/unit)" },
new TestData { name = "$105", number = "150.000", comment = "(C:stp/unit)" },
new TestData { name = "$110", number = "10000.000", comment = "(X:unit/min)" },
new TestData { name = "$111", number = "10000.000", comment = "(Y:unit/min)" },
new TestData { name = "$112", number = "10000.000", comment = "(Z:unit/min)" },
new TestData { name = "$113", number = "10000.000", comment = "(A:unit/min)" },
new TestData { name = "$114", number = "10000.000", comment = "(B:unit/min)" },
new TestData { name = "$115", number = "10000.000", comment = "(C:unit/min)" },
new TestData { name = "$120", number = "100.000", comment = "(X:unit/s^2)" },
new TestData { name = "$121", number = "100.000", comment = "(Y:unit/s^2)" },
new TestData { name = "$122", number = "100.000", comment = "(Z:unit/s^2)" },
new TestData { name = "$123", number = "100.000", comment = "(A:unit/s^2)" },
new TestData { name = "$124", number = "100.000", comment = "(B:unit/s^2)" },
new TestData { name = "$125", number = "100.000", comment = "(C:unit/s^2)" },
new TestData { name = "$130", number = "200.000", comment = "(X:unit max)" },
new TestData { name = "$131", number = "200.000", comment = "(Y:unit max)" },
new TestData { name = "$132", number = "200.000", comment = "(Z:unit max)" },
new TestData { name = "$133", number = "200.000", comment = "(A:unit max)"},

    };

    // 清空表格
    _Table._ClearTable();

    // 绑定到表格
    _Table._BindArray(_TestDatas);

    // Refresh the table
    _Table._Refresh();
}



private void LoadTxtFile(string filePath)
{
    // Clear the two-dimensional list to prevent duplicate data loading
    _TableData = new List<List<string>>();

    try
    {
        // Read all lines from the file
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            // Check if the line starts with "$"
            if (line.StartsWith("$"))
            {
                // Split the line by '=' and remove any leading or trailing spaces
                string[] keyValue = line.Split('=').Select(s => s.Trim()).ToArray();

                // Check if the split resulted in two parts
                if (keyValue.Length == 2)
                {
                    // Extract values from the first column
                    string column1 = keyValue[0].Substring(1).Trim();

                    // Extract values from the second column
                    string column2 = keyValue[1].Trim();

                    // Extract values from the third column (if it exists)
                    int openParenthesisIndex = column2.IndexOf('(');
                    string column3 = openParenthesisIndex != -1
                        ? column2.Substring(openParenthesisIndex )
                        : string.Empty;

                    keyValue = column2.Split(" ");
                    // Add the values to the two-dimensional list
                    _TableData.Add(new List<string> { column1, keyValue[0], column3 });
                }
            }
        }
    }
    catch (Exception e)
    {
        Debug.LogError($"Error reading file {filePath}: {e.Message}");
    }
}






private void Start()
{
    // 记录初始化时的列和行的数量
    iniColumnCount = _IniColumn;
    iniRowCount = _IniRow;

    // 如果配置文件不存在，创建默认配置文件
    // if (!File.Exists(_DefaultTxtPath))
    // {
    //     CreateDefaultTxtFile(_DefaultTxtPath);
    // }
    _DefaultTxtPath = Application.persistentDataPath+"/ctrlConfig.txt";
    // 加载配置文件
    LoadTxtFile(_DefaultTxtPath);

    // 添加按钮点击事件
    _AddRow.onClick.AddListener(() =>
    {
        StartCoroutine(_AddRowClick());
    });
    _AddColumn.onClick.AddListener(() =>
    {
        StartCoroutine(_AddColumnClick());
    });
    _ClearButton.onClick.AddListener(() =>
    {
        _Table._ClearTable();
    });
    _RemoveRow.onClick.AddListener(_RemoveRowClick);
    _RemoveColumn.onClick.AddListener(_RemoveColumnClick);

    _ChangeSelectText.onClick.AddListener(__ChangeSelectText);
    _TestBindArray.onClick.AddListener(_DefaultArr);

    // 初始化表格列和行
    for (int i = 0; i < iniColumnCount; i++)
    {
        _Table._AddColumn();
    }
    
    //  _LoadTableDataFromTxtFile(_TableData);
    _TestBindArr();

    _DefaultArr();
}




        private void __ChangeSelectText()
        {
            // 将选中单元格的显示数据设置为输入框的文本
            foreach (var item in _Table._CurrentSelectedCellDatas)
            {
                item._ShowData = _ValueInput.text;
            }
        }

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
    }
}
