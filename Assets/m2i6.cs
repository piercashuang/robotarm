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
    public class m2i6 : MonoBehaviour
    {   
        private List<List<string>> _TableData; // 用于存储从文本文件加载的原始数据
        private List<register> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表

        public Table _Table;
        public TMP_InputField _ValueInput;
        public Button _openallio, _closeallio, _openio, _closeio, _TestBindArray;
        private int _IniColumn = 30, _IniRow = 40;
        public string _DefaultTxtPath = "Assets/ioconfig.txt"; // 修改为你的配置文件路径
        private int iniColumnCount, iniRowCount;
        // Start is called before the first frame update
        /// <summary>
        /// 测试绑定数组
        /// </summary>






private void _TestBindArr()
{


    // 调用 _TestBindArr2 方法
    _TestBindArr2(_TableData);
}


private void _TestBindArr2(List<List<string>> data)
{
    // 清空表格
    _Table._ClearTable();

    // 创建 register 列表
    List<register> _TestDatas = new List<register>();

    // 循环创建 TestData 对象并添加到列表
    foreach (var row in data)
    {
        _TestDatas.Add(new register { addr = row[0], zero =row[1], one =row[2],two = row[3] });
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
    List<register> _TestDatas = new List<register>
    {
        new register { addr = "0", zero = "00", one = "00", two = "00", three = "00", four = "00", five = "00", six = "00", seven = "00", comment = "tcpip" },
        new register { addr = "0", zero = "00", one = "00", two = "00", three = "00", four = "00", five = "00", six = "00", seven = "00", comment = "udp" },

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

                // Split the line by '=' and remove any leading or trailing spaces
                string[] keyValue = line.Split(',');

                // Check if the split resulted in two parts
                if (keyValue.Length == 4)
                {
                    // Extract values from the first column
                    string column1 = keyValue[0];

                    // Extract values from the second column
                    string column2 = keyValue[1];

                    string column3 = keyValue[2];

                    string column4 = keyValue[3];

                    keyValue = column2.Split(" ");
                    // Add the values to the two-dimensional list
                    _TableData.Add(new List<string> { column1, column2, column3 ,column4});
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

    // 加载配置文件
    // LoadTxtFile(_DefaultTxtPath);

    // 添加按钮点击事件
    // _openio.onClick.AddListener(() =>
    // {
    //     printrow();
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
    _TestBindArray.onClick.AddListener(_DefaultArr);

    // 初始化表格列和行
    for (int i = 0; i < iniColumnCount; i++)
    {
        _Table._AddColumn();
    }
    
    //  _LoadTableDataFromTxtFile(_TableData);
    //_TestBindArr();
    _DefaultArr();


    //_Table._SetCellData(2,1,"test");
}




        private void printrow()
        {
            // 将选中单元格的显示数据设置为输入框的文本
            foreach (var item in _Table._CurrentSelectedCellDatas)
            {
                print(item._Row);
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
