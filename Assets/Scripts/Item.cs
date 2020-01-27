using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public float m_Price = 0;

    public void SetItemImage(Sprite img)
    {
        var imgComp = GetComponent<Image>();

        if (imgComp)
        {
            imgComp.sprite = img;
        }
    }
}
