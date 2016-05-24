using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

	public GameObject achievementsPanel;
	public GameObject settingsPanel;

	// Use this for initialization
	void Start()
	{
		achievementsPanel.SetActive(false);
		settingsPanel.SetActive(false);
	}


	public void StartGame()
	{
		AudioManager.Instance.PlayButtonPressSound();
		SceneManager.LoadScene("MainGame");
	}

	public void ShowAchievements()
	{

		AudioManager.Instance.PlayButtonPressSound();
		achievementsPanel.SetActive(true);
	}

	public void ShowSettingsPanel()
	{

		AudioManager.Instance.PlayButtonPressSound();
		settingsPanel.SetActive(true);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			OnBackButtonPressed();

		}
	}

	public void OnBackButtonPressed()
	{

		AudioManager.Instance.PlayButtonPressSound();
		if (achievementsPanel.activeSelf)
		{
			achievementsPanel.SetActive(false);
		}
		else if (settingsPanel.activeSelf)
		{
			settingsPanel.SetActive(false);
		}
		else
		{
			Application.Quit();
		}
	}
}
