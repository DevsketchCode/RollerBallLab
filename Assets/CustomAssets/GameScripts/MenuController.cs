using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	[SerializeField]
	private RectTransform mainMenu = null;

	[SerializeField]
	private RectTransform levelSelect = null;

	[SerializeField]
	private RectTransform settingsMenu = null;

	[SerializeField]
	private RectTransform creditsMenu = null;

	private void Start()
	{
		hideAll();
		showMainMenu();
	}

	private void hideAll()
	{
		mainMenu.gameObject.SetActive(false);
		levelSelect.gameObject.SetActive(false);
		if (creditsMenu != null) { creditsMenu.gameObject.SetActive(false); }
		if (settingsMenu != null) { settingsMenu.gameObject.SetActive(false); }
	}

	public void showMainMenu()
	{
		hideAll();
		mainMenu.gameObject.SetActive(true);
	}

	public void showLevelSelectMenu()
	{
		hideAll();
		levelSelect.gameObject.SetActive(true);
	}

	public void showSettingsMenu()
	{
		hideAll();
		if (settingsMenu != null)
		{
			settingsMenu.gameObject.SetActive(true);
		}
	}

	public void showCreditsMenu()
	{
		hideAll();
		if (creditsMenu != null)
		{
			creditsMenu.gameObject.SetActive(true);
		}
	}
}
