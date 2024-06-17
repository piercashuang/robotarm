using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasctrl : MonoBehaviour
{

    public Button btn1;
    public Button btn2;
    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        

        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("canvasm3i2");
        foreach (var view in viewsArray)
        {
            string viewID = view.name; // Assuming the view's name is the ID.
            views.Add(viewID, view);
            view.SetActive(false);
        }
        views["Canvas"].SetActive(true); // Set the default view.

        btn1.onClick.AddListener(() => { print("btn1"); showAll("Canvaskey"); });
        btn2.onClick.AddListener(() => { print("btn2"); showScene("Canvas"); });
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            private void showAll(string viewID)
    {
        foreach (var view in views.Values)
        {
            view.SetActive(true);
        }

    }
}
