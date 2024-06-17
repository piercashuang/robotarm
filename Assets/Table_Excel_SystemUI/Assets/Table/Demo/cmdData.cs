using System;
using UnityEngine;

namespace XP.TableModel.Test
{
    public class cmdData
    {
        [Column(0, "name", _Width = 1272, _Alignment = TextAlignment.Left)] // 设置左对齐
        public string name { get; set; }


        public void OnColumnCellClick(CellClickData cellClickData)
        {
            Debug.Log("这是在TestData类中触发的表头点击事件", cellClickData._Selectable);
        }
    }
}
