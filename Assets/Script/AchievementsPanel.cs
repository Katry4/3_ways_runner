using UnityEngine;
using System.Collections;

public class AchievementsPanel : MonoBehaviour
{

	public AchievementItem scoreItem, timeItem;
	public int scoreAchievement = 500;
	public float timeAchievement = 3;
	// Use this for initialization
	void Start()
	{
		scoreItem.isOn = PlayerPrefs.GetInt(IDs.PlayerSettings.score, 0) > scoreAchievement;
		timeItem.isOn = PlayerPrefs.GetFloat(IDs.PlayerSettings.time, 0) > timeAchievement;
	}
}
