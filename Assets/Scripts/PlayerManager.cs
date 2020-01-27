using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float _maxBudget = 50;

    private float _spending = 0;

    private List<GameObject> _CartItems = new List<GameObject>();

    [SerializeField]
    private GameObject _EndCanvas;

    enum State { Buying, CheckingOutFailed}
    State _playerState = State.Buying;

    [SerializeField]
    Text _budgetUI;
    [SerializeField]
    Text _spendingUI;

    private void Start()
    {
        if (_EndCanvas)
        {
            _EndCanvas.SetActive(false);
        }

        else
        {
            Debug.LogWarning("PlayerManager: End canvas reference is missing");
        }

        if (_budgetUI && _spendingUI)
        {
            _budgetUI.text = _maxBudget.ToString();
            _spendingUI.text = _spending.ToString();
        }
        else
        {
            Debug.LogWarning("Missing budget and spending text element reference");
        }
    }


    public void AddToCart(GameObject item)
    {
        _playerState = State.Buying;
        _CartItems.Add(item);
        _spending += item.GetComponent<Item>().m_Price;
        _spendingUI.text = _spending.ToString();
    }

    public void RemoveFromCart(GameObject item)
    {
        _playerState = State.Buying;
        _CartItems.Remove(item);
        _spending -= item.GetComponent<Item>().m_Price;
        _spendingUI.text = _spending.ToString();
    }

    public void CheckOut()
    {
        if (_maxBudget >= _spending)
        {
            if (_EndCanvas)
            {
                _EndCanvas.SetActive(true);
            }
            
            {
                Debug.LogWarning("End canvas reference missing");
            }
            _maxBudget -= _spending;
            _spending = 0;
            Time.timeScale = 0;
        }

        else
        {
            _playerState = State.CheckingOutFailed;
        }
    }

    private void OnGUI()
    {
        if (_playerState == State.CheckingOutFailed)
        {
            float XPos = Screen.width / 2;
            float YPOS = Screen.height + 50;

            GUI.Label(new Rect(XPos, YPOS, 200, 30), "You Exceeded your budget, please return some Items");
        }
    }


}
