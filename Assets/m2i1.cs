using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m2i1 : MonoBehaviour
{
    public Dropdown drpdownfullscreen;

    void Start()
    {
        // 添加Dropdown值变化的监听器
        drpdownfullscreen.onValueChanged.AddListener((value) => { OnDropdownFullscreenValueChanged(value); });
    }

    void Update()
    {
        // 在Update中可以添加任何需要持续检查的逻辑
    }

    // Dropdown值变化时调用的方法
    void OnDropdownFullscreenValueChanged(int value)
    {
        if (value == 0)
        {
            // 选择窗口模式
            SetWindowedMode();
        }
        else if (value == 1)
        {
            // 选择全屏模式
            SetFullScreenMode();
        }
    }

    // 设置窗口模式
    void SetWindowedMode()
    {
        // 这里可以设置窗口模式的分辨率，例如：
        Screen.SetResolution(1400, 1200, false);
    }

    // 设置全屏模式
    void SetFullScreenMode()
    {
        // 这里可以设置全屏模式的分辨率，例如：
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }
}
