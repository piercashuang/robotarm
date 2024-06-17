using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace XP.TableModel.Test
{
    /// <summary>
    /// 测试表格
    /// </summary>
    public class m3i1table : MonoBehaviour
    {   public GameObject keyboard;
        public string inputGcmd;
        public Button okinputvalue;
        [SerializeField] public InputField inputvalue;
        

        private List<List<string>> _TableData; // 用于存储从文本文件加载的原始数据
        private List<cmdData> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表

        public Table _Table;
        public TMP_InputField _ValueInput;
        public Button _AddRow, _AddColumn, _RemoveRow, _RemoveColumn, _ChangeSelectText, _ClearButton, _TestBindArray;
        private int _IniColumn = 30, _IniRow = 40;
        public string _DefaultTxtPath = "Assets/codetable.txt"; // 修改为你的配置文件路径
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



private void _DefaultArr()
{
    // 生成测试数据
    List<cmdData> _TestDatas = new List<cmdData>
    {
        new cmdData { name = "HMC_OUT" },
        new cmdData { name = "HMC_OUT_0"},
        new cmdData { name = "HMC_OUT_0"},
        new cmdData { name = "HMC_OUT_0"},
        new cmdData { name = "HMC_OUT_0" },
        new cmdData { name = "HMC_OUT_0" },
        new cmdData { name = "HMC_OUT_0" },
        new cmdData { name = "HMC_OUT_0" },
        new cmdData { name = "HMC_OUT_0" },
        new cmdData { name = "HMC_OUT_0" },
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
    // 清空二维列表，以防数据重复加载
    _TableData = new List<List<string>>();

    // 读取文件内容
    string fileContent = System.IO.File.ReadAllText(filePath);

    // 按行拆分文件内容
    string[] lines = fileContent.Split('\n');

    // 遍历每一行，将其作为一个字符串添加到二维列表
    foreach (string line in lines)
    {
        print(line);
        // 将整行文本作为一个字符串添加到二维列表
        _TableData.Add(new List<string> { line });
    }
}






private void Start()
{
    print(_DefaultTxtPath);
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
    _AddRow.onClick.AddListener(() =>
    {
        StartCoroutine(_AddRowClick());
    });
    _AddColumn.onClick.AddListener(() =>
    {
        StartCoroutine(_AddColumnClick());
    });
    // _ClearButton.onClick.AddListener(() =>
    // {
    //    // _Table._ClearTable();
    // });
    _RemoveRow.onClick.AddListener(_RemoveRowClick);
    _RemoveColumn.onClick.AddListener(_RemoveColumnClick);

    _ChangeSelectText.onClick.AddListener(__ChangeSelectText);
    _TestBindArray.onClick.AddListener(_DefaultArr);


    okinputvalue.onClick.AddListener(okinputvalueOnlick);

    // 初始化表格列和行
    for (int i = 0; i < iniColumnCount; i++)
    {
        _Table._AddColumn();
    }
    
    //  _LoadTableDataFromTxtFile(_TableData);
    _TestBindArr();
}

        private void okinputvalueOnlick(){
            inputGcmd = inputvalue.text;
                    _TableData.Add(new List<string> { inputGcmd });
        _Table._Refresh();
        keyboard.SetActive(false);
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
