using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;


public class ViewChange : MonoBehaviour
{   
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public List<GameObject> views = new List<GameObject>();
    public List<Button> btns = new List<Button>();
    private int defalutID = 1;//默认显示窗口
    private void Start()
    {   
        Init();
        views  = GameObject.FindGameObjectsWithTag("MainView").ToList();
        btns = transform.GetComponentsInChildren<Button>().ToList();
        
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }
        views[0].gameObject.SetActive(true);


        btn1.onClick.AddListener(() => { print("btn1"); showscene1();});
        btn2.onClick.AddListener(() => { print("btn2"); showscene2();});
        btn3.onClick.AddListener(() => { print("btn3"); showscene3();});
        btn4.onClick.AddListener(() => { print("btn4"); });
        btn5.onClick.AddListener(() => { print("btn5"); });
    }

    private void Init(){
        btn1 = this.transform.GetChild(0).GetComponent<Button>();
        btn2 = this.transform.GetChild(1).GetComponent<Button>();
        btn3 = this.transform.GetChild(2).GetComponent<Button>();
        btn4 = this.transform.GetChild(3).GetComponent<Button>();
        btn5 = this.transform.GetChild(4).GetComponent<Button>();
    }


    private void showscene1(){
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }
        views[1].gameObject.SetActive(true);
    }


        private void showscene2(){
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }
        views[0].gameObject.SetActive(true);
        
    }

        private void showscene3(){
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }
        views[3].gameObject.SetActive(true);
        
    }

    
}
