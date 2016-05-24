using UnityEngine;
using System.Collections;

public class InvinciblePowerUpItem : MonoBehaviour
{
	public float speed = 1;
	private Vector3 startPosition;

	public void Start()
	{
		startPosition = transform.localPosition - transform.up * 0.5f;
	}
		
	// Update is called once per frame
	void Update()
	{
		transform.localPosition = startPosition + transform.up * -0.3f * Mathf.Cos(Time.time * speed);
	}
}
