﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public float m_Price = 0;

    [SerializeField]
    private Text priceText;

    public void SetUIPrices()
    {
        //Called in Shop.cs when item initialization is complete inside InitializeMenu

        if (priceText)
        {
            priceText.text = m_Price + " $";
        }
    }
    public void SetItemImage(Sprite img)
    {
        var imgComp = GetComponent<Image>();

        if (imgComp)
        {
            imgComp.sprite = img;
        }
    }

    public void AddToCart()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().AddToCart(gameObject);
    }
}
