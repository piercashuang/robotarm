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

public class btndelta1 : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    [SerializeField] public InputField valueInput;



    private void Start()
    {
        Init();

        btn1.onClick.AddListener(() => { Increase(); }); // 添加按钮点击事件          
         btn2.onClick.AddListener(() => {  Decrease();});
    }


        private void Init()

    {
        valueInput = this.transform.GetChild(0).GetComponent<InputField>();
        btn1 = this.transform.GetChild(1).GetComponent<Button>();
        btn2 = this.transform.GetChild(2).GetComponent<Button>();

        

    }

        private void Decrease()
    {
        // Parse the current value from the input field
        if (int.TryParse(valueInput.text, out int currentValue))
        {
            // Decrease the value by 10 and update the input field
            valueInput.text = (currentValue - 1).ToString();
        }
        else
        {
            Debug.LogError("Invalid input value");
        }
    }

    private void Increase()
    {
        // Parse the current value from the input field
        if (int.TryParse(valueInput.text, out int currentValue))
        {
            // Increase the value by 10 and update the input field
            valueInput.text = (currentValue + 1).ToString();
        }
        else
        {
            Debug.LogError("Invalid input value");
        }
    }

}
