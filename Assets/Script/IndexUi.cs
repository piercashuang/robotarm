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


public class IndexUi : MonoBehaviour
{   
    public Button btnconfig;
    public Button btncommand;
    public Button btnposition;
    public Button btnhelp;
    // public List<GameObject> views = new List<GameObject>();
    public Dictionary<int, GameObject> views = new Dictionary<int, GameObject>();
    public List<Button> btns = new List<Button>();
    private int defalutID = 1;//默认显示窗口
    private void Start()
    {   
        Init();
       var viewsList = GameObject.FindGameObjectsWithTag("MainView").ToList();
       foreach (var VARIABLE in viewsList)
       {
           string str = VARIABLE.gameObject.name.Replace("MainView", "");
           views.Add(int.Parse(str)-1,VARIABLE);
       }
        btns = transform.GetComponentsInChildren<Button>().ToList();
        
        foreach (var view in views.Values)
        {   print(view);
            view.gameObject.SetActive(false);
        }
        views[0].gameObject.SetActive(true);


        btnconfig.onClick.AddListener(() => { print("btnconfig"); showscene1();});
        btncommand.onClick.AddListener(() => { print("btncommand"); showscene2();});
        btnposition.onClick.AddListener(() => { print("btnposition"); showscene3();});
        btnhelp.onClick.AddListener(() => { print("btnhelp");showscene4(); });
  
    }

    private void Init(){
        btnconfig = this.transform.GetChild(0).GetComponent<Button>();
        btncommand = this.transform.GetChild(1).GetComponent<Button>();
        btnposition = this.transform.GetChild(2).GetComponent<Button>();
        btnhelp = this.transform.GetChild(3).GetComponent<Button>();
    }


    private void showscene1(){
        foreach (var view in views.Values)
        {
            view.gameObject.SetActive(false);
        }
        views[1].gameObject.SetActive(true);
    }


        private void showscene2(){
        foreach (var view in views.Values)
        {
            view.gameObject.SetActive(false);
        }
        views[2].gameObject.SetActive(true);
        
    }

        private void showscene3(){
        foreach (var view in views.Values)
        {
            view.gameObject.SetActive(false);
        }
        views[3].gameObject.SetActive(true);
        
    }

            private void showscene4(){
        foreach (var view in views.Values)
        {
            view.gameObject.SetActive(false);
        }
        views[6].gameObject.SetActive(true);
        
    }
    
}
