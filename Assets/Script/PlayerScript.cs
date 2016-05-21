using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public static float VisibleDistance = 5;

	[SerializeField] private float _speed = 2;
	[SerializeField] InputController _inputController;

	public RoadTilesGeneratorScript[] roads;
	private int currentRoad = 1;

	// Use this for initialization
	void Start () {
		_inputController.PlayerInput = ProcessPlayerInput;	
	}

	// Update is called once per frame
	void Update () {
		Vector3 dVector = transform.position + transform.forward * _speed * Time.deltaTime;
		if (transform.position.z != roads[currentRoad].transform.position.z)
		{
			Vector3 sideOffset = roads[currentRoad].transform.position - transform.position;

			//dVector = new Vector3(dVector.x, dVector.y, roads[currentRoad].transform.position.z);
			dVector = new Vector3(dVector.x, dVector.y, (transform.position + sideOffset.normalized).z);
		}

		transform.position = dVector;
	}

	void ProcessPlayerInput(InputController.Gestures obj)
	{
		switch (obj)
		{
			case InputController.Gestures.DragLeft:
				MovePlayer(true);
				break;
			case InputController.Gestures.DragRight:
				MovePlayer(false);
				break;
			case InputController.Gestures.DragUp:
				break;
			default:
				break;
		}
	}

	public void MovePlayer(bool toLeft)
	{
		//Debug.Log("try to move "+toLeft+" while on "+currentRoad+ " of " + roads.Length);
		if (toLeft)
		{
			if (currentRoad > 0)
			{
				currentRoad--;
			}
		}
		else
		{
			if (currentRoad < roads.Length - 1)
			{
				currentRoad++;
			}
		}
	}
}
