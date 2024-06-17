using UnityEngine;
using UnityEngine.UI;

public class filezonectrl : MonoBehaviour
{
    public Button newfile;
    public Canvas inputcanvas; // 注意这里的类型是 Canvas
    public Button close;

    // Start is called before the first frame update
    void Start()
    {
        newfile.onClick.AddListener(() => { ShowInputCanvas(); });
        close.onClick.AddListener(() => { HideInputCanvas(); });
    }

    // 显示 inputcanvas
    void ShowInputCanvas()
    {
        if (inputcanvas != null)
        {
            inputcanvas.gameObject.SetActive(true);
        }
    }

    // 隐藏 inputcanvas
    void HideInputCanvas()
    {
        if (inputcanvas != null)
        {
            inputcanvas.gameObject.SetActive(false);
        }
    }
}
