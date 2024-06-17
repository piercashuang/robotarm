using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // 导入UI命名空间

public class DragHandler : MonoBehaviour,IDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas; 


    void Start()
    {
        
    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
