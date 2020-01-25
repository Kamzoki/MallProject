using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToInteract : MonoBehaviour
{
    [SerializeField]
    private TouchPhase _touchPhase = TouchPhase.Began;

    private Shop _previousOpenedShop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            if (Input.touchCount == 1)
            {
                GameObject g = GetTouchedShopOnSingleTouch();
                OpenShopMenu(g, true);
            }
            else
            {
                GameObject g = GetTouchedShopOnMultipleTouchs();
                OpenShopMenu(g, true);
            }
        }
    }

    GameObject GetTouchedShopOnMultipleTouchs()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == _touchPhase)
            {
                Vector3 touchPos = Input.touches[i].position;

                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Shop"))
                    {
                        return hit.transform.gameObject;
                    }
                }
            }
        }
        return null;
    }

    GameObject GetTouchedShopOnSingleTouch()
    {
        if (Input.touches[0].phase == _touchPhase)
        {
            Vector3 touchPos = Input.touches[0].position;

            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Shop"))
                {
                    return hit.transform.gameObject;
                }
            }
        }
        return null;
    }

    void OpenShopMenu(GameObject shop ,bool isOpening)
    {
        if (shop)
        {
            Shop comp = shop.GetComponent<Shop>();

            if (comp)
            {
                ClosePreviousShopMenu();

                comp.OpenShopMenu(isOpening);
                _previousOpenedShop = comp;
            }
        }
    }

    void ClosePreviousShopMenu()
    {
        if (_previousOpenedShop)
        {
            _previousOpenedShop.OpenShopMenu(false);
        }
    }
}
