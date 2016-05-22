using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public Text scoreText, timeText;
	private int _currentScore = 0;
	private float _currentTime = 0;
	
	// Update is called once per frame
	void Update () {
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

		int minutes = (int) _currentTime/60;
		int seconds =(int) ( _currentTime - (minutes * 60));
		timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}
}
