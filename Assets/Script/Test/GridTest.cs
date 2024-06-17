using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("SingleRow");
        for (int i = 0; i < 10; i++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.SetParent(this.transform);
        }
    
    }
}
