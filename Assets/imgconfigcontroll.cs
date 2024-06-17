using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imgconfigcontroll : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;

        public Button btn6;
    public Button btn7;
    public Button btn8;
    public Button btn9;
    public Button btn10;

    public Button btn11;
    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
    public List<Button> btns = new List<Button>();

    private void Start()
    {
        Init();
        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("imgconfig");
        foreach (var view in viewsArray)
        {
            string viewID = view.name; // Assuming the view's name is the ID.
            views.Add(viewID, view);
            view.SetActive(false);
        }
        views["Image1"].SetActive(true); // Set the default view.

        btn1.onClick.AddListener(() => { print("btn1");ChangeButtonHeight(btn1); showScene("Image1"); });
        btn2.onClick.AddListener(() => { print("btn2");ChangeButtonHeight(btn2); showScene("Image2"); });
        btn3.onClick.AddListener(() => { print("btn3");ChangeButtonHeight(btn3); showScene("Image3"); });
        btn4.onClick.AddListener(() => { print("btn4");ChangeButtonHeight(btn4); showScene("Image4"); });
        btn5.onClick.AddListener(() => { print("btn5");ChangeButtonHeight(btn5); showScene("Image5"); });

        btn6.onClick.AddListener(() => { print("btn6");ChangeButtonHeight(btn6); showScene("Image6"); });
        btn7.onClick.AddListener(() => { print("btn7");ChangeButtonHeight(btn7); showScene("Image7"); });
        btn8.onClick.AddListener(() => { print("btn8");ChangeButtonHeight(btn8); showScene("Image8"); });
        btn9.onClick.AddListener(() => { print("btn9");ChangeButtonHeight(btn9); showScene("Image9"); });
        btn10.onClick.AddListener(() => { print("btn10");ChangeButtonHeight(btn10); showScene("Image10"); });
        btn11.onClick.AddListener(() => { print("btn11");ChangeButtonHeight(btn11); showScene("Image11"); });

        btns.Add(btn1);
        btns.Add(btn2);
        btns.Add(btn3);
        btns.Add(btn4);
        btns.Add(btn5);
        btns.Add(btn6);
        btns.Add(btn7);
        btns.Add(btn8);
        btns.Add(btn9);
        btns.Add(btn10);
        btns.Add(btn11);
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
        btn8 = this.transform.GetChild(7).GetComponent<Button>();
        btn9 = this.transform.GetChild(8).GetComponent<Button>();
        btn10 = this.transform.GetChild(9).GetComponent<Button>();

        btn11 = this.transform.GetChild(10).GetComponent<Button>();

    }

    private void showScene(string viewID)
    {
        foreach (var view in views.Values)
        {
            view.SetActive(false);
        }
        if (views.ContainsKey(viewID))
        {
            views[viewID].SetActive(true);
        }
        else
        {
            Debug.LogError("View with ID " + viewID + " not found.");
        }
    }


    private void ChangeButtonHeight(Button clickedButton)
    {
        foreach (Button button in btns)
        {
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, button == clickedButton ? 80f : 60f);
            }
        }
    }
}
