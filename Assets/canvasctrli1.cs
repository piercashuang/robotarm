using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasctrli1 : MonoBehaviour
{
    public Text textname;
    public Button btnadd;
    public Button btnok;
    public Button btnclosenewfile;
    public Button btncancelnewfile;
    
    public Button newfile;
    public Button btncloseadd;
    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {


        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("canvasm3i1");
        foreach (var view in viewsArray)
        {
            string viewID = view.name; // Assuming the view's name is the ID.
            views.Add(viewID, view);
            view.SetActive(false);
        }
        views["Canvas"].SetActive(true); // Set the default view.
        btnclosenewfile.onClick.AddListener(() => { print("btnclosenewfile"); showScene("Canvas"); });
        btncancelnewfile.onClick.AddListener(() => { print("btncancelnewfile"); showScene("Canvas"); });
        btnadd.onClick.AddListener(() => { print("btnadd"); showtwoScene("Canvas","Canvaskey"); });
        btncloseadd.onClick.AddListener(() => { print("btncloseadd"); showScene("Canvas"); });
        newfile.onClick.AddListener(() => { print("newfile"); showtwoScene("Canvas","canvnewfile"); });

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

            private void ok()
    {
        print(textname.text);

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
        else{
            Debug.Log("not found: "+viewID );
        }

    }

            private void closeScene()
    {
        foreach (var view in views.Values)
        {
            view.SetActive(false);
        }


    }



    

            private void showtwoScene(string viewID,string viewID2)
    {
        foreach (var view in views.Values)
        {
            view.SetActive(false);
        }
        if (views.ContainsKey(viewID))
        {
            views[viewID].SetActive(true);
        }
        if(views.ContainsKey(viewID2)){
            views[viewID2].SetActive(true);
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
