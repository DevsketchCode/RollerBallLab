using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    private GameObject levelDetailsPanel;
    private GameObject defaultLevelDetails;
    private GameObject[] levels;
    private GameObject levelDetails;

    public void Start()
    {
        levelDetails = null;
        levelDetailsPanel = GameObject.Find("LevelDetailsPnl");
    }

    public void LoadLevelDetails(string levelName)
    {

        hideAllLevelDetails();

        // Show the level details panel
        levelDetailsPanel.SetActive(true);

        // Get the default level details object and hide it
        defaultLevelDetails = GameObject.Find("LevelPlaceHolderDetails");
        if (defaultLevelDetails != null && defaultLevelDetails.activeSelf)
        {
            defaultLevelDetails.SetActive(false);
        }

        // Get the selected level details object and show it
        levelDetails = GameObject.Find(levelName).transform.Find(levelName + "Details").gameObject;

        if(levelDetails != null && !levelDetails.activeSelf)
        {
            levelDetails.SetActive(true);
        }
    }

    private void hideAllLevelDetails()
    {
        GameObject eachLevel;
        // Get number of levels
        levels = GameObject.FindGameObjectsWithTag("Levels");

        // Loop through each of the levels that are tagged with "Levels" and get the child level#Details object
        // If it exists and is active, deactivate it
        for (int i = 0; i < levels.Length; i++)
        {
            eachLevel = GameObject.Find("Level" + i + "Details");
            if (eachLevel != null && eachLevel.activeSelf)
            {
                eachLevel.SetActive(false);
            }
        }
    }

    public void LoadLevelByName(string levelName)
    {
        // Unpause game if was paused before from another level.
        Time.timeScale = 1;

        // Load the new scene
        SceneManager.LoadScene(levelName);

    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void loadLevelSettings()
    {
        GameObject menu = GameObject.Find("InGameMenu");
        GameObject parentCanvas = menu.transform.parent.gameObject;
        GameObject settingMenu = parentCanvas.transform.Find("SettingsPnl").gameObject;
        menu.SetActive(false);
        settingMenu.SetActive(true);
    }
}
