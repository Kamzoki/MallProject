using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCartHandler : MonoBehaviour
{
    [SerializeField]
    PlayerManager pm;
    [SerializeField]
    int maxColCount;

    [SerializeField]
    GameObject _PlayerCartUI;

    [SerializeField]
    float itemsPaddingHorizontal;
    [SerializeField]
    float itemsPaddingVerticel;

    GameObject newPlayerMenu;
    
    [HideInInspector]
    public bool isShowing = false;
    void Start()
    {
        if (!_PlayerCartUI)
        {
            Debug.LogWarning("PlayerCartHandler: Missing player cart UI reference");
        }

        if (!pm)
        {
            Debug.LogWarning("PlayerCartHandler: Missing playermanager reference");
        }
    }

    public void ShowCart()
    {
        isShowing = !isShowing;

        if (isShowing)
        {
            if (pm)
            {
                newPlayerMenu = Instantiate(_PlayerCartUI, GameObject.FindGameObjectWithTag("MainCanvas").transform, false) as GameObject;

                foreach (var item in newPlayerMenu.GetComponentsInChildren<UI>())
                {
                    item.isPlayerScroll = true;
                }

                Vector2 origin = new Vector2(0, 0);
                Vector2 newPos = origin;

                int currentColIndex = 0;

                for (int i = 0; i < pm.m_CartItems.Count; i++)
                {
                    if (currentColIndex > maxColCount)
                    {
                        currentColIndex = 0;
                        newPos = origin + new Vector2(itemsPaddingHorizontal, itemsPaddingVerticel * (i + 1));
                    }
                    else newPos += new Vector2(itemsPaddingHorizontal, 0);

                    Vector3 finalPos = new Vector3(newPos.x, newPos.y, 0);
                    GameObject playerMenuScroller = LocateScroller(newPlayerMenu);
                    
                    if (playerMenuScroller)
                    {
                        GameObject newItem = Instantiate(pm.m_CartItems[i], finalPos, new Quaternion(0, 0, 0, 0), playerMenuScroller.GetComponent<RectTransform>()) as GameObject;
                        newItem.GetComponent<Item>().ShowReturnButton(true);
                    }

                    currentColIndex++;
                }
            }
        }
        
        else
        {
            if (newPlayerMenu)
            {
                GameObject.Destroy(newPlayerMenu);
            }
        }
    }

    private GameObject LocateScroller(GameObject newPlayerCart)
    {
        //Locates the child content inside the scrollview shopmenu
        if (newPlayerMenu)
        {
            foreach (Transform tr in newPlayerMenu.transform)
            {
                if (tr.tag == "ScrollViewPort")
                {
                    foreach (Transform subTr in tr.transform)
                    {
                        if (subTr.tag == "Scroller")
                        {
                           return subTr.gameObject;
                        }
                    }
                }
            }
        }
        return null;
    }
}
