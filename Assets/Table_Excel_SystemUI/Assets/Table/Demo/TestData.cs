using System;
using UnityEngine;

namespace XP.TableModel.Test
{
    public class TestData
    {
        

        [Column(0, "ID", _Width = 300)] // Change the column index to 0 and add a new property named 'name'
        public string name { get; set; }

        [Column(1, "数值", _Width = 300)]
        public string number { get; set; }

        [Column(2,"说明", _Width = 798)] //表头列遮罩 - 纵向滚动条
        public string comment { get; set; }




        public void OnColumnCellClick(CellClickData cellClickData)
        {
            Debug.Log("这是在TestData类中触发的表头点击事件", cellClickData._Selectable);
        }
    }
}
