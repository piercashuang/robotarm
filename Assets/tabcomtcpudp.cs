using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tabcomtcpudp : MonoBehaviour
{
    public Dropdown drp;
    public Button btn1;
    [SerializeField] public InputField inpt1;
    [SerializeField] public InputField inptudp;
    public Button btn2;


    public Dictionary<string, GameObject> views = new Dictionary<string, GameObject>();
    public List<Button> btns = new List<Button>();

    private void Start()
    {


        drp.onValueChanged.AddListener((f) => { OnDropdownValueChanged(); });
        // Init();
        GameObject[] viewsArray = GameObject.FindGameObjectsWithTag("threecanvas");
        foreach (var view in viewsArray) 
        {
            string viewID = view.name; // Assuming the view's name is the ID.
            views.Add(viewID, view);
            view.SetActive(false);
        }
        views["canvasa"].SetActive(true); // Set the default view.

        btn1.onClick.AddListener(() => { clearinput(); });
        btn2.onClick.AddListener(() => { udpclearinput(); });
        // btn2.onClick.AddListener(() => { print("btn2");ChangeButtonHeight(btn2); showScene("comzone"); });
        // btn3.onClick.AddListener(() => { print("btn3");ChangeButtonHeight(btn3); showScene("remotectrl"); });
        // btn4.onClick.AddListener(() => { print("btn4");ChangeButtonHeight(btn4); showScene("channelzone"); });
        // btn5.onClick.AddListener(() => { print("btn5");ChangeButtonHeight(btn5); showScene("dataformatzone"); });





    }


    private void clearinput(){
        print("clear inpt");
        inpt1.text="";

    }

        private void udpclearinput(){
        print("clear inpt");
        inptudp.text="";

    }

    private void Init()
    {
        // btn1 = this.transform.GetChild(0).GetComponent<Button>();
        // btn2 = this.transform.GetChild(1).GetComponent<Button>();
        // btn3 = this.transform.GetChild(2).GetComponent<Button>();
        // btn4 = this.transform.GetChild(3).GetComponent<Button>();
        // btn5 = this.transform.GetChild(4).GetComponent<Button>();



    }

        private void OnDropdownValueChanged()
    {
        print("Selected drp menu: " + drp.GetComponent<Dropdown>().value);

        // You can use the selected port as needed, e.g., set ComPort variable.

        if(drp.GetComponent<Dropdown>().value == 0){
            print("show0");
            showScene("canvasa");
        }

                if(drp.GetComponent<Dropdown>().value == 1){
            print("show1");
            showScene("canvasb");
            
        }


                if(drp.GetComponent<Dropdown>().value == 2){
            print("show2");
            showScene("canvasc");
        }
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
