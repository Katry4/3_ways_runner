using UnityEngine;
using System.Collections;

public class CoinItem : MonoBehaviour
{
	public float rotationSpeed = 70;
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);
	}
}
