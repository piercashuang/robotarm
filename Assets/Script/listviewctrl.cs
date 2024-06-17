using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class listviewctrl : MonoBehaviour
{
    public Transform Contentpanel;
    public GameObject listItemPrefab; // Reference to the Text Prefab
    
    public List<string> dataList = new List<string>(); // Your data source with 10 items

    void Start()
    {
        // Simulate 10 data items
        for (int i = 0; i < 10; i++)
        {
            dataList.Add("Item " + i.ToString());
        }

        PopulateList();
      
    }

    void PopulateList()
    {
        foreach (string data in dataList)
        {
            // Instantiate the prefab
            GameObject listItem = Instantiate(listItemPrefab, Contentpanel);
            
            // Customize listItem UI based on data
            Text textComponent = listItem.GetComponentInChildren<Text>();
            if (textComponent != null)
            {
                textComponent.text = data;
            }
        }
    }
}
