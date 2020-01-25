using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    private void Start()
    {
        if (_menu)
        {
            _menu.SetActive(false);
        }
    }
    public void OpenShopMenu(bool isOpening)
    {
        _menu.SetActive(isOpening);
    }
}
