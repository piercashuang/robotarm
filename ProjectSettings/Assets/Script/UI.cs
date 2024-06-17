using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public List<Slider> sliders = new List<Slider>();
    public List<Button> btns = new List<Button>();
    [SerializeField] public InputField field;
    public Transform[] bones;
    private string sliderName;
    private float value;
    private float arg
    {
        set
        {
            bones[0].rotation = Quaternion.Euler(value * new Vector3(0, 360, 0));
            bones[1].rotation = Quaternion.Euler(0.6F * value * new Vector3(360, 0, 0));
            bones[2].rotation = Quaternion.Euler(0.4F * value * new Vector3(360, 0, 0));
            bones[3].rotation = Quaternion.Euler(0.2F * value * new Vector3(360, 0, 0));
        }
    }


    private void Start()
    {
        //  transform.Find("")  只能找子物体 不能找子子物体
        Init();
        foreach (var slider in sliders)
        {
            slider.onValueChanged.AddListener((f) => { value = f; });
        }

        foreach (var slider in btns)
        {
            slider.onClick.AddListener(() => { print(field.text); });
        }
    }

    private void Update()
    {
        arg = value;
    }

    private void Init()
    {
        var sliderZone = this.transform.GetChild(0);
        var btnZone = this.transform.GetChild(1);
        field = this.transform.GetChild(2).GetComponent<InputField>();
        for (int i = 0; i < sliderZone.childCount; i++)
        {
            var child = sliderZone.GetChild(i); //slider第一个子物体 
            sliders.Add(child.GetComponent<Slider>());
        }

        for (int i = 0; i < btnZone.childCount; i++)
        {
            var child = btnZone.GetChild(i);
            btns.Add(child.GetComponent<Button>());
        }
    }
}