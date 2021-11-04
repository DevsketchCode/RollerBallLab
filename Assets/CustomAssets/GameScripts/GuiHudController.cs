using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiHudController : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI timerGUIText = null;

    [SerializeField]
    private Timer gameTimer = null;

    [SerializeField]
    private RectTransform inGameMenuPnl = null;

    [SerializeField]
    private RectTransform settingsPnl = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inGameMenuPnl.gameObject.SetActive(false);
        settingsPnl.gameObject.SetActive(false);
        // Clear any preset values
        timerGUIText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("In Game Menu"))
        {
            ToggleInGameMenu();
        }
    }


    public void ToggleInGameMenu()
    {
        // Toggle inGameMenu
        if (inGameMenuPnl.gameObject.activeSelf || settingsPnl.gameObject.activeSelf)
        {
            // Turn Off
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            inGameMenuPnl.gameObject.SetActive(false);
            settingsPnl.gameObject.SetActive(false);
        }
        else
        {
            // Turn On
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            inGameMenuPnl.gameObject.SetActive(true);
        }

    }

    private void OnGUI()
    {
        if(gameTimer.isRunning())
        {
            // time, format mm : ss.mmm
            float roundedTime = Mathf.Round(gameTimer.getTimerTime() * 1000) / 1000.0f;

            // display time in mm:ss
            if (roundedTime > 60)
            {
                int minutes = Mathf.FloorToInt(roundedTime / 60.0f);
                float seconds = roundedTime - (minutes * 60);
                timerGUIText.text = string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", (Mathf.RoundToInt(seconds)));
            }
            else
            {
                timerGUIText.text = "00:" + string.Format("{0:00}", roundedTime);
            }



        }
    }
}
