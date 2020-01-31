using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject scrollViewObject;
    public bool isPlayerScroll;
    private void Start()
    {
        if (gameObject.CompareTag("CheckOut"))
        {
            if (isPlayerScroll)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void CloseUI()
    {
        if (scrollViewObject)
        {
            if (isPlayerScroll)
            {
                GameObject.Destroy(scrollViewObject);
                PlayerManager pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
                if (pm)
                {
                    pm.PCH.isShowing = !pm.PCH.isShowing;
                }
            }
            else
            {
                scrollViewObject.SetActive(false);
            }
        }
    }

    public void CheckOut()
    {
        PlayerManager pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        if (pm)
        {
            pm.CheckOut();
        }
    }

    public void ShowCheckOutForm(bool isShowing)
    {
        PlayerManager pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        if (pm)
        {
            if (isShowing)
            {
                if (scrollViewObject)
                {
                    Destroy(scrollViewObject);
                    pm.PCH.isShowing = !pm.PCH.isShowing;
                }
            }
            pm.CheckoutForm(isShowing);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
