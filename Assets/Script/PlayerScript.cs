using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	public static float VisibleDistance = 5;
	private Rigidbody _rigidBody;
	[SerializeField] private float _speed = 2;
	[SerializeField] private float _jumpPower = 2;
	[SerializeField] InputController _inputController;
	private GameManager _gameManager;

	public RoadTilesGeneratorScript[] roads;
	private int currentRoad = 1;

	bool _grounded = true;

	// Use this for initialization
	void Start()
	{
		_inputController.PlayerInput = ProcessPlayerInput;	
		_rigidBody = GetComponent<Rigidbody>();
		_gameManager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 dVector = transform.position + transform.forward * _speed * Time.deltaTime;
		if (transform.position.z != roads[currentRoad].transform.position.z)
		{
			Vector3 sideOffset = new Vector3(transform.position.x, transform.position.y, roads[currentRoad].transform.position.z) - transform.position;

			if (sideOffset.magnitude < 0.1f)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, roads[currentRoad].transform.position.z);
			}
			else
			{
				dVector = new Vector3(dVector.x, dVector.y, (transform.position + sideOffset.normalized * Time.deltaTime * _speed).z);
				transform.position = Vector3.Lerp(transform.position, dVector, Time.deltaTime);
			}
		}
			
		transform.position = dVector;
	}

	public void OnCollisionStay(Collision collisionInfo)
	{
		_grounded = true;
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
				Jump();
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

	public void Jump()
	{
		if (_grounded)
		{
			transform.position += transform.up * 0.1f;
			_grounded = false;
			_rigidBody.AddForce(transform.up * _jumpPower * 130);
		}
	}


	protected void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Obstacle")
		{
			Debug.Log("GameOver!!");
		}
		
	}

	protected void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Coin")
		{
			_gameManager.AddPoints(20);
			col.gameObject.transform.parent = null;
			SimplePool.Despawn(col.gameObject);
		}
	}
}
