using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainview4change : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;


    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
    public List<Button> btns = new List<Button>();

    private void Start()
    {
        Init();
        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("mainview4");
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



        btns.Add(btn1);
        btns.Add(btn2);
        btns.Add(btn3);
        btns.Add(btn4);

    }

    private void Init()
    {
        btn1 = this.transform.GetChild(0).GetComponent<Button>();
        btn2 = this.transform.GetChild(1).GetComponent<Button>();
        btn3 = this.transform.GetChild(2).GetComponent<Button>();
        btn4 = this.transform.GetChild(3).GetComponent<Button>();



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
