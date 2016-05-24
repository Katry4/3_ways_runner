using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	
	public Text scoreText, timeText;
	private int _currentScore = 0;
	private float _currentTime = 0;

	[Header("GamePlay Balance")]
	[Header("Player")]
	[SerializeField] private float _playerSpeed = 1f;
	[SerializeField] private float _playerJumpPower = 1f;

	[Header("Coin")]
	[SerializeField] private int _scorePerCoin = 20;

	[Header("PowerUps")]
	[Header("Boost")]
	[SerializeField] private float _boostMaxSpeed = 3f;
	[SerializeField] private float _boostDurationInSec = 4f;

	[Header("Invincible")]
	[SerializeField] private float _invncblDurationInSec = 4f;



	// Update is called once per frame
	void Update()
	{
		_currentTime += Time.deltaTime;
		UpdateUI();

		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			GameOver();

		}
	}

	public void AddPoints(int points)
	{
		_currentScore += points;
	}

	private void UpdateUI()
	{
		scoreText.text = _currentScore.ToString();

		int minutes = (int)_currentTime / 60;
		int seconds = (int)(_currentTime - (minutes * 60));
		timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	public void GameOver()
	{

		Debug.Log("GameOver!!");


		int score = PlayerPrefs.GetInt(IDs.PlayerSettings.score, 0);
		if (_currentScore > score)
		{
			PlayerPrefs.SetInt(IDs.PlayerSettings.score, _currentScore);
		}

		float time = PlayerPrefs.GetFloat(IDs.PlayerSettings.time, 0);
		if (_currentTime > time)
		{
			PlayerPrefs.SetFloat(IDs.PlayerSettings.time, _currentTime);
		}


		AudioManager.Instance.PlayGameOverSound();
		SceneManager.LoadScene("MainMenu");
	}

	#region Player balance
	public float PlayerDefaultSpeed
	{
		get
		{
			return _playerSpeed;
		}
	}

	public float PlayerJumpPower
	{
		get
		{
			return _playerJumpPower;
		}
	}
	#endregion

	#region Bonuses
	public int ScorePerCoin
	{
		get
		{
			return _scorePerCoin;
		}
	}

	public float BoostDuration
	{
		get
		{
			return _boostDurationInSec;
		}
	}

	public float BoostMaxSpeed
	{
		get
		{
			return _boostMaxSpeed;
		}
	}

	public float InvinciblePowerupDuration
	{
		get
		{
			return _invncblDurationInSec;
		}
	}

	#endregion
}
