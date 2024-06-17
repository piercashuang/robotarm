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


public class verController : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public Button btn6;
    public Button btn7;
    public List<GameObject> views = new List<GameObject>();
    public List<Button> btns = new List<Button>();

    private void Awake()
    {
        Init();
        views = GameObject.FindGameObjectsWithTag("MainView").ToList();
        btns = transform.GetComponentsInChildren<Button>().ToList();


        btn1.onClick.AddListener(() =>
        {
            print("btn1");
            showscene1();
        });
        btn2.onClick.AddListener(() =>
        {
            print("btn2");
            showscene2();
        });
        btn3.onClick.AddListener(() =>
        {
            print("btn3");
            showscene3();
        });
        btn4.onClick.AddListener(() =>
        {
            print("btn4");
            showscene4();
        });
        btn5.onClick.AddListener(() =>
        {
            print("btn5");
            showscene5();
        });
        btn6.onClick.AddListener(() =>
        {
            print("btn6");
            showscene6();
        });
        btn7.onClick.AddListener(() =>
        {
            print("btn7");
            showscene7();
        });
    }

    private void Init()
    {
        btn1 = this.transform.GetChild(0).GetComponent<Button>();
        btn2 = this.transform.GetChild(1).GetComponent<Button>();
        btn3 = this.transform.GetChild(2).GetComponent<Button>();
        btn4 = this.transform.GetChild(3).GetComponent<Button>();
        btn5 = this.transform.GetChild(4).GetComponent<Button>();
        btn6 = this.transform.GetChild(5).GetComponent<Button>();
        btn7 = this.transform.GetChild(6).GetComponent<Button>();
    }


    private void showscene1()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[3].gameObject.SetActive(true);
    }


    private void showscene2()
    {
        foreach (var view in views)
        {
            print(view);
            view.gameObject.SetActive(false);
        }

        views[1].gameObject.SetActive(true);
    }

    private void showscene3()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[6].gameObject.SetActive(true);
    }

    private void showscene4()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[5].gameObject.SetActive(true);
    }


    private void showscene5()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[2].gameObject.SetActive(true);
    }

    private void showscene6()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[0].gameObject.SetActive(true);
    }

    private void showscene7()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);
        }

        views[4].gameObject.SetActive(true);
    }
}