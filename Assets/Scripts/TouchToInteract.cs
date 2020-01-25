using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToInteract : MonoBehaviour
{
    [SerializeField]
    private TouchPhase _touchPhase = TouchPhase.Began;

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
               
                if (g)
                {
                    //TODO open shop menu
                }
            }
            else
            {
                GameObject g = GetTouchedShopOnMultipleTouchs();
                
                if (g)
                {
                    //open shop menu
                }
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
}
