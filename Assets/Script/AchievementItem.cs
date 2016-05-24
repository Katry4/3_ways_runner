using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{

	public bool isOn = false;

	public Color achievedColor = Color.white, unachievedColor = Color.white;
	private Image image;

	void Start()
	{
		image = GetComponent<Image>();
	}
	// Update is called once per frame
	void Update()
	{
		image.color = isOn ? achievedColor : unachievedColor;
	}
}
