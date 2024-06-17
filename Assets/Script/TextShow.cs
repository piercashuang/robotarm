using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    public string showContent;
    public Transform contentTr;
    public Text text;
    public void Init()
    {
       var canvas = GameObject.FindObjectOfType<MainCanvas>();
       contentTr =  canvas.transform.FindTheTfByName("MainView2Content");
       text = GetComponentInChildren<Text>();
       transform.SetParent(contentTr);
       text.text = showContent;
    }
}
