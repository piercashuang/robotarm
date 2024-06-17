using System;
using UnityEngine;

namespace XP.TableModel.Test
{
    public class register
    {
        

        [Column(0, "寄存地址", _Width = 300)] // Change the column index to 0 and add a new property named 'name'
        public string addr { get; set; }

        [Column(1, "0", _Width = 100)]
        public string zero { get; set; }

        [Column(2, "1", _Width = 100)]
        public string one { get; set; }

        [Column(3,"2" ,_Width = 100)]
        public string two { get; set; }

        [Column(4,"3" ,_Width = 100)]
        public string three { get; set; }

        [Column(5,"4" ,_Width = 100)]
        public string four { get; set; }        

        [Column(6,"5" ,_Width = 100)]
        public string five { get; set; }        

        [Column(7,"6" ,_Width = 100)]
        public string six { get; set; }        

        [Column(8,"7" ,_Width = 100)]
        public string seven { get; set; }        

        [Column(9,"备注" ,_Width = 396)]
        public string comment { get; set; } 




        public void OnColumnCellClick(CellClickData cellClickData)
        {
            Debug.Log("这是在TestData类中触发的表头点击事件", cellClickData._Selectable);
        }
    }
}
