using UnityEngine;
using System.Collections;

public class BoostPowerUpItem : MonoBehaviour
{
	public float speed = 40;
	private Vector3 startPosition;

	public void Start()
	{
		startPosition = transform.localPosition;
	}
		
	// Update is called once per frame
	void Update()
	{
		transform.localPosition = startPosition + transform.forward * 0.1f * Mathf.Cos(Time.time * speed);
	}
}
