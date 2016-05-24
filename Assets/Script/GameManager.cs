using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

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
