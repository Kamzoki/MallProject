using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuUI; //defining the prefab of the menu

    [SerializeField]
    private List<itemSpecifier> _itemsImages; //a set of images of each item

    [SerializeField]
    private float itemsPaddingHorizontal; 
    [SerializeField]
    private float itemsPaddingVerticel;

    [SerializeField]
    private GameObject _ItemPrefab; //defining the prefab of an empty item

    [SerializeField]
    int maxRowCount = 4; //Max number of items in a single row (zero is counted)

    private GameObject shopMenu; // Ref for the newely instantiated menu

    private GameObject shopScroller; // Ref to the content object inside shopmenu (shop menu must be of type ScrollView)
    
    private void Start()
    {
        if (_menuUI)
        {
            shopMenu = Instantiate(_menuUI, GameObject.FindGameObjectWithTag("MainCanvas").transform, false) as GameObject;
            LocateScroller();
            InitializeMenu();
            shopMenu.SetActive(false);
        }
    }
    public void OpenShopMenu(bool isOpening)
    {
        if (shopMenu)
        {
            shopMenu.SetActive(isOpening);
        }
    }

    private void InitializeMenu()
    {
        //Construct a square matrix of items using maxRowCount and padding parameters and adding them as children to content shopscroller inside the given scrollview shopmenu
        if (_itemsImages.Count > 0)
        {
            if (shopMenu && _ItemPrefab && shopScroller)
            {
                Vector2 origin = new Vector2(0, 0);
                Vector2 newPos = origin;

                int currentRowIndex = 0;

                for (int i = 0; i < _itemsImages.Count; i++)
                {
                    _ItemPrefab.GetComponent<Item>().SetItemImage(_itemsImages[i].itemSprite);
                    _ItemPrefab.GetComponent<Item>().m_Price = _itemsImages[i].itemPrice;
                    _ItemPrefab.GetComponent<Item>().SetUIPrices();
                    _ItemPrefab.GetComponent<Item>().ShowReturnButton(false);

                    if (currentRowIndex > 4)
                    {
                        currentRowIndex = 0;
                        newPos = origin + new Vector2(itemsPaddingHorizontal, itemsPaddingVerticel * (i + 1));
                    }
                    else newPos += new Vector2(itemsPaddingHorizontal, 0);

                    Vector3 finalPos = new Vector3(newPos.x, newPos.y, 0);
                    GameObject newItem = Instantiate(_ItemPrefab, finalPos, new Quaternion(0, 0, 0, 0), shopScroller.GetComponent<RectTransform>()) as GameObject;

                    currentRowIndex++;
                }
            }
        }
    }

    private void LocateScroller()
    {
        //Locates the child content inside the scrollview shopmenu
        if (shopMenu)
        {
            foreach (Transform tr in shopMenu.transform)
            {
                if (tr.tag == "ScrollViewPort")
                {
                    foreach (Transform subTr in tr.transform)
                    {
                        if (subTr.tag == "Scroller")
                        {
                            shopScroller = subTr.gameObject;
                            return;
                        }
                    }
                }
            }
        }
    }
}

[System.Serializable]
public struct itemSpecifier{
    public Sprite itemSprite;
    public float itemPrice;
}