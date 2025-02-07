using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class skipButton : MonoBehaviour,IPointerEnterHandler
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition = new Vector3(Random.Range(306, -306), Random.Range(-200, 200), 0);
    }
}
