using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public static float VisibleDistance = 5;

	private float _speed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * _speed * Time.deltaTime;
	}
}
