using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
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
public class m3i1keys : MonoBehaviour
{
    // private List<List<string>> _TableData = new List<List<string>>();
    // public GameObject keyboard;
    // private List<cmdData> _TestDatas;
      
    // private List<cmdData> _TestDataList; // 用于将原始数据转换为 TestData 对象的列表
    private bool xstatus = false;
    private bool ystatus = false;
    private bool zstatus = false;
    private bool astatus = false;
    private bool bstatus = false;
    private bool cstatus = false;

    public Button btnxopen;
    public Button btnyopen;
    public Button btnzopen;
    public Button btnaopen;
    public Button btnbopen;
    public Button btncopen;

    public Text txtx;
    public Text txty;
    public Text txtz;
    public Text txta;
    public Text txtb;
    public Text txtc;


    // public Table _Table;
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public Button btn6;
    public Button btn7;
    public Button btn8;
    public Button btn9;
    public Button btn0;
    // [SerializeField] public InputField inputGcmd;
    private string inputGcmd = "";
    [SerializeField] public InputField input1;


    public Button btnx;
    public Button btny;
    public Button btnz;
    public Button btna;
    public Button btnb;
    public Button btnc;
    public Button btnm;
    public Button btnp;
    public Button btnv;
    public Button btnr;
    public Button btnk;
    public Button btnf;
    public Button btno;
    public Button btns;
    public Button btne;
    public Button btni;
    public Button btnt;

    public Button G00;
    public Button G01;
    public Button G02;
    public Button G03;
    public Button G04;
    public Button G05;
    public Button G06;
    public Button G07;
    public Button G08;
    public Button G09;

    public Button G30;
    public Button G31;
    public Button G32;
    public Button G33;
    public Button G36;
    public Button G37;
    public Button G39;

    public Button G40;
    public Button G41;
    public Button G42;
    public Button G43;
    public Button G46;
    public Button G47;
    public Button G49;


    public Button G50;
    public Button G51;
    public Button G52;
    public Button G53;
    public Button G56;
    public Button G57;
    public Button G59;


    public Button G60;
    public Button G61;
    public Button G62;
    public Button G63;
    public Button G66;
    public Button G67;
    public Button G69;

    public Button space;
    public Button delete;
    public Button clear;
    public Button paste;


    public Button dot;
    public Button signDecrease;
    public Button nowpos;
    public Button originpos; // G00 X0.0 Y0.0 Z0.0 A0.0 B0.0 C0.0
    public Button safepos; //G00 X0.000 Y0.000 Z0.000 A0.000 B0.000 C0.000

    public Button enter;
    public Button cancel;


    // Start is called before the first frame update
    void Start()
    {
        btnxopen.onClick.AddListener(() => {offStatus();xstatus=true; });
        btnyopen.onClick.AddListener(() => {offStatus();ystatus=true; });
        btnzopen.onClick.AddListener(() => {offStatus();zstatus=true; });
        btnaopen.onClick.AddListener(() => {offStatus();astatus=true; });
        btnbopen.onClick.AddListener(() => {offStatus();bstatus=true; });
        btncopen.onClick.AddListener(() => {offStatus();cstatus=true; });
        btn1.onClick.AddListener(() => { print("btn1"); input("1"); });
        btn2.onClick.AddListener(() => { print("btn2"); input("2"); });
        btn3.onClick.AddListener(() => { print("btn3"); input("3"); });
        btn4.onClick.AddListener(() => { print("btn4"); input("4"); });
        btn5.onClick.AddListener(() => { print("btn5"); input("5"); });
        btn6.onClick.AddListener(() => { print("btn6"); input("6"); });
        btn7.onClick.AddListener(() => { print("btn7"); input("7"); });
        btn8.onClick.AddListener(() => { print("btn8"); input("8"); });
        btn9.onClick.AddListener(() => { print("btn9"); input("9"); });
        btn0.onClick.AddListener(() => { print("btn0"); input("0"); });

        btnx.onClick.AddListener(() => { print("btn1"); input("X"); });
        btny.onClick.AddListener(() => { print("btn1"); input("Y"); });
        btnz.onClick.AddListener(() => { print("btn1"); input("Z"); });
        btna.onClick.AddListener(() => { print("btn1"); input("A"); });
        btnb.onClick.AddListener(() => { print("btn1"); input("B"); });
        btnc.onClick.AddListener(() => { print("btn1"); input("C"); });
        btnm.onClick.AddListener(() => { print("btn1"); input("M"); });
        btnp.onClick.AddListener(() => { print("btn1"); input("P"); });
        btnv.onClick.AddListener(() => { print("btn1"); input("V"); });
        btnr.onClick.AddListener(() => { print("btn1"); input("R"); });
        btnk.onClick.AddListener(() => { print("btn1"); input("K"); });
        btnf.onClick.AddListener(() => { print("btn1"); input("F"); });
        btno.onClick.AddListener(() => { print("btn1"); input("O"); });
        btns.onClick.AddListener(() => { print("btn1"); input("S"); });
        btne.onClick.AddListener(() => { print("btn1"); input("E"); });      
        btni.onClick.AddListener(() => { print("btn1"); input("I"); });
        btnt.onClick.AddListener(() => { print("btn1"); input("T"); });    



        space.onClick.AddListener(() => { SpaceButtonClicked(); });
        delete.onClick.AddListener(() => { DeleteButtonClicked(); });
        clear.onClick.AddListener(() => { ClearButtonClicked(); });
        paste.onClick.AddListener(() => { PasteButtonClicked(); });

        dot.onClick.AddListener(() => { DotButtonClicked(); });
        signDecrease.onClick.AddListener(() => { SignDecreaseButtonClicked(); });
        originpos.onClick.AddListener(() => { OriginPosButtonClicked(); });
        nowpos.onClick.AddListener(() => { nowposButtonClicked(); });
        
        safepos.onClick.AddListener(() => { SafePosButtonClicked(); });
        G00.onClick.AddListener(() => { GButtonClicked("G00"); });

        G01.onClick.AddListener(() => { GButtonClicked("G01"); });
        G02.onClick.AddListener(() => { GButtonClicked("G02"); });
        G03.onClick.AddListener(() => { GButtonClicked("G03"); });
        G04.onClick.AddListener(() => { GButtonClicked("G04"); });
        G05.onClick.AddListener(() => { GButtonClicked("G05"); });
        G06.onClick.AddListener(() => { GButtonClicked("G06"); });
        G07.onClick.AddListener(() => { GButtonClicked("G07"); });
        G08.onClick.AddListener(() => { GButtonClicked("G08"); });
        G09.onClick.AddListener(() => { GButtonClicked("G09"); });


        G30.onClick.AddListener(() => { GButtonClicked("G30"); });
        G31.onClick.AddListener(() => { GButtonClicked("G31"); });
        G32.onClick.AddListener(() => { GButtonClicked("G32"); });
        G33.onClick.AddListener(() => { GButtonClicked("G33"); });
        G36.onClick.AddListener(() => { GButtonClicked("G36"); });
        G37.onClick.AddListener(() => { GButtonClicked("G37"); });
        G39.onClick.AddListener(() => { GButtonClicked("G39"); });

        G40.onClick.AddListener(() => { GButtonClicked("G40"); });
        G41.onClick.AddListener(() => { GButtonClicked("G41"); });
        G42.onClick.AddListener(() => { GButtonClicked("G42"); });
        G43.onClick.AddListener(() => { GButtonClicked("G43"); });
        G46.onClick.AddListener(() => { GButtonClicked("G46"); });
        G47.onClick.AddListener(() => { GButtonClicked("G47"); });
        G49.onClick.AddListener(() => { GButtonClicked("G49"); });

        G50.onClick.AddListener(() => { GButtonClicked("G50"); });
        G51.onClick.AddListener(() => { GButtonClicked("G51"); });
        G52.onClick.AddListener(() => { GButtonClicked("G52"); });
        G53.onClick.AddListener(() => { GButtonClicked("G53"); });
        G56.onClick.AddListener(() => { GButtonClicked("G56"); });
        G57.onClick.AddListener(() => { GButtonClicked("G57"); });
        G59.onClick.AddListener(() => { GButtonClicked("G59"); });

        
        G60.onClick.AddListener(() => { GButtonClicked("G60"); });
        G61.onClick.AddListener(() => { GButtonClicked("G61"); });
        G62.onClick.AddListener(() => { GButtonClicked("G62"); });
        G63.onClick.AddListener(() => { GButtonClicked("G63"); });
        G66.onClick.AddListener(() => { GButtonClicked("G66"); });
        G67.onClick.AddListener(() => { GButtonClicked("G67"); });
        G69.onClick.AddListener(() => { GButtonClicked("G69"); });
        
        // enter.onClick.AddListener(() => { enteronclick(); });
    }

    // void enteronclick(){
    //     if(input1.text!=null){
    //         inputGcmd = input1.text;
    //         keyboard.SetActive(false);
    //         // _Table.Add(new List<string> { inputGcmd });  
            
    //         _TableData = new List<List<string>>();
    //         _TableData.Add(new List<string> { inputGcmd });

    //     }
    
    // }

// void enteronclick()
// {
//     Debug.Log("enteronclick called");  // 添加这行进行调试

//     if(input1.text != null)
//     {
//         inputGcmd = input1.text;
        

//         // 添加新行并设置数据
//         _TableData.Add(new List<string> { inputGcmd });
//         _Table._Refresh();
//         keyboard.SetActive(false);
//     }
// }





        //     private IEnumerator _AddRowClick()
        // {
        //     var _count = 1;
        //     for (int i = 0; i < _count; i++)
        //     {
        //         yield return null;
        //         // 添加行并设置每个单元格的数据
        //         var _row = _Table._AddRow();
        //         for (int c = 0; c < _Table._HeaderColumn._HeaderCellDatas.Count; c++)
        //         {
        //             //设置单元格数据为列索引+行索引
        //             _Table._SetCellData(c, _row._Index, c + "," + _row._Index);
        //         }
        //     }
        // }



    // Update is called once per frame
    // void Update()
    // {
        
    // }
    void offStatus(){
            xstatus = false;
            ystatus = false;
            zstatus = false;
            astatus = false;
            bstatus = false;
            cstatus = false;
        
    }
    void input(string s){
        print(xstatus);
        print(ystatus);
        print(zstatus);
        print(astatus);
        print(bstatus);
        print(cstatus);
        if(xstatus == true){
            txtx.text += s;
        }

                if(ystatus == true){
            txty.text += s;
        }


            if(zstatus == true){
            txtz.text += s;
        }


        if(astatus == true){
            txta.text += s;
        }

        if(bstatus == true){
            txtb.text += s;
        }

        if(cstatus == true){
            txtc.text += s;
        }


        input1.text += s;
        print("input: " + s);
    }


        void SpaceButtonClicked()
    {
        input(" ");
        print("Space button clicked");
    }

    void DeleteButtonClicked()
    {

                if(xstatus == true){
                    //input1.text = input1.text.Substring(0, input1.text.Length - 1);
            txtx.text = txtx.text.Substring(0, txtx.text.Length - 1);
        }

                if(ystatus == true){
            txty.text = txty.text.Substring(0, txtx.text.Length - 1);
        }


            if(zstatus == true){
            txtz.text = txtz.text.Substring(0, txtx.text.Length - 1);
        }


        if(astatus == true){
            txta.text = txta.text.Substring(0, txtx.text.Length - 1);
        }

        if(bstatus == true){
            txtb.text = txtb.text.Substring(0, txtx.text.Length - 1);
        }

        if(cstatus == true){
          txtc.text = txtc.text.Substring(0, txtx.text.Length - 1);
        }




        if (input1.text.Length > 0)
        {
            input1.text = input1.text.Substring(0, input1.text.Length - 1);
        }
        print("Delete button clicked");
    }

    void ClearButtonClicked()
    {
        input1.text = "";
        print("Clear button clicked");
    }

 void PasteButtonClicked()
    {
        string clipboardText = GUIUtility.systemCopyBuffer;

        if (!string.IsNullOrEmpty(clipboardText))
        {
            input1.text += clipboardText;
            print("Paste button clicked");
        }
        else
        {
            print("Clipboard is empty");
        }
    }

     void DotButtonClicked()
    {

        
        input(".");
        print("Dot button clicked");
    }

    void SignDecreaseButtonClicked()
    {
        input("-");
        print("Sign Decrease button clicked");
    }


        void nowposButtonClicked()
    {
        input1.text = "G00 X0.0 Y0.0 Z0.0 A0.0 B0.0 C0.0";
        print("Origin Pos button clicked");
    }



    void OriginPosButtonClicked()
    {
        input1.text = "G00 X0.0 Y0.0 Z0.0 A0.0 B0.0 C0.0";
        print("Origin Pos button clicked");
    }

    void SafePosButtonClicked()
    {
        input1.text = "G00 X0.000 Y0.000 Z0.000 A0.000 B0.000 C0.000";
        print("Safe Pos button clicked");
    }

    // ... (existing code)

    void G00ButtonClicked()
    {
        // 获取当前输入框的文本
        string currentText = input1.text;

        // 使用正则表达式检查是否符合 "G数字数字空格" 的格式
        if (Regex.IsMatch(currentText, @"^G\d{2}\s"))
        {
            // 如果是，替换成 "G00"
            input1.text = "G00 " + currentText.Substring(4);
        }
        else
        {
            // 如果不是，添加 "G00 "
            input1.text = "G00 " + currentText;
        }

        print("G00 button clicked");
    }



//         public void _BindArr()
// {

//     // 调用 BindArr2 方法
//     _BindArr2(_TableData);
// }


// public void _BindArr2(List<List<string>> data)
// {
//     // 清空表格
//     // _Table._ClearTable();

//     // 创建 TestData 列表
 
//         List<cmdData> _TestDatas = new List<cmdData>();
  
    

//     // 循环创建 TestData 对象并添加到列表
//     foreach (var row in data)
//     {
//         _TestDatas.Add(new cmdData { name = row[0] });
//         // Add more properties if you have additional columns
//     }

//     // 绑定到表格
    
//     _Table._BindArray(_TestDatas);

//     // 刷新表格
//     _Table._Refresh();
// }




     void GButtonClicked(string gCommand)
    {
        // 获取当前输入框的文本
        string currentText = input1.text;

        // 使用正则表达式检查是否符合 "G数字数字空格" 的格式
        if (Regex.IsMatch(currentText, @"^G\d{2}\s"))
        {
            // 如果是，替换成对应的 G 指令
            input1.text = Regex.Replace(currentText, @"^G\d{2}\s", gCommand + " ");
        }
        else
        {
            // 如果不是，添加对应的 G 指令和空格
            input1.text = gCommand + " " + currentText;
        }

        print(gCommand + " button clicked");
    }





}
}