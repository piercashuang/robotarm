using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabm3i1 : MonoBehaviour
{
    public Button btn1;
    public Button btn2;

    public Image image4;
    public Image image5;

    private Color btn1InitialColor;
    private Color btn2InitialColor;

    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
    public List<Button> btns = new List<Button>();

    private void Start()
    {
        Init();
        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("m3i1zone");
        foreach (var view in viewsArray)
        {
            string viewID = view.name; // Assuming the view's name is the ID.
            views.Add(viewID, view);
            view.SetActive(false);
        }
        views["filezone"].SetActive(true); // Set the default view.

        // Get the initial colors of the buttons
        btn1InitialColor = btn1.colors.normalColor;
        btn2InitialColor = btn2.colors.normalColor;

        btn1.onClick.AddListener(() => { print("btn1"); ChangeButtonHeight(btn1); showScene("filezone"); HideImage(image4);showImage(image5); });
        btn2.onClick.AddListener(() => { print("btn2"); ChangeButtonHeight(btn2); showScene("editzone"); HideImage(image5);showImage(image4); });

        btns.Add(btn1);
        btns.Add(btn2);
    }

    private void Init()
    {
        btn1 = this.transform.GetChild(0).GetComponent<Button>();
        btn2 = this.transform.GetChild(1).GetComponent<Button>();
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
            Graphic graphic = button.GetComponent<Graphic>();

            if (rectTransform != null && graphic != null)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, button == clickedButton ? 80f : 60f);

                // Change the button color based on the clicked button
                graphic.color = (button == clickedButton) ? Color.white : GetInitialColor(button);
            }
        }
    }

    private Color GetInitialColor(Button button)
    {
        if (button == btn1)
        {
            return btn1InitialColor;
        }
        else if (button == btn2)
        {
            return btn2InitialColor;
        }
        else
        {
            return Color.white; // Default color if the button is not recognized
        }
    }

    private void HideImage(Image image)
    {
        if (image != null)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void showImage(Image image)
    {
        if(image != null)
        {
            image.gameObject.SetActive(true);
        }
    }
}
