using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	public static float VisibleDistance = 5;
	private Rigidbody _rigidBody;
	private float _defaultSpeed;
	private float _speed = 0;
	private float _jumpPower;
	[SerializeField] InputController _inputController;
	private GameManager _gameManager;

	public RoadTilesGeneratorScript[] roads;
	private int currentRoad = 1;

	private MeshRenderer _meshRenderer;
	public Material defaultPlayerMaterial, boostedPlayerMaterial, invinciblePlayerMaterial;
	private Color defaultPlayerColor = Color.white;

	bool _grounded = true;

	// Use this for initialization
	void Start()
	{
		_inputController.PlayerInput = ProcessPlayerInput;	
		_rigidBody = GetComponent<Rigidbody>();
		_gameManager = FindObjectOfType<GameManager>();
		_defaultSpeed = _gameManager.PlayerDefaultSpeed;
		_speed = _defaultSpeed;
		_jumpPower = _gameManager.PlayerJumpPower;

		_meshRenderer = GetComponent<MeshRenderer>();
		defaultPlayerMaterial = _meshRenderer.material;
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

	protected void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Coin")
		{
			_gameManager.AddPoints(_gameManager.ScorePerCoin);
		}
		else if (col.gameObject.tag == "SpeedPowerUp")
		{
			SpeedUp();
		}
		else if (col.gameObject.tag == "InvinciblePowerUp")
		{
			BecameInvincible();
		}

		col.gameObject.transform.parent = null;
		SimplePool.Despawn(col.gameObject);
	}

	Coroutine lastBoostCourotine, lastInvincibleCourotine;

	private void SpeedUp()
	{
		lastBoostCourotine = StartCoroutine(SpeedUpEnumerator(_gameManager.BoostMaxSpeed, _gameManager.BoostDuration));
	}

	private IEnumerator SpeedUpEnumerator(float targetSpeed, float duration)
	{
		//_meshRenderer.material = boostedPlayerMaterial;
		if (lastBoostCourotine != null)
			StopCoroutine(lastBoostCourotine);

		float step = (targetSpeed - _defaultSpeed) / 20; 
		while (_speed < targetSpeed)
		{
			_speed += step;
			yield return 0;
		}

		_speed = targetSpeed;

		yield return new WaitForSeconds(duration);


		while (_speed > _defaultSpeed)
		{
			_speed -= step;
			yield return 0;
		}

		//_meshRenderer.material = defaultPlayerMaterial;
		_speed = _defaultSpeed;
	}

	private void BecameInvincible()
	{
		lastInvincibleCourotine = StartCoroutine(InvincibleEnumerator(_gameManager.InvinciblePowerupDuration));

	}

	private IEnumerator InvincibleEnumerator( float duration)
	{
		_meshRenderer.material = invinciblePlayerMaterial;
		gameObject.layer = LayerMask.NameToLayer("InvinciblePlayer");

		if (lastInvincibleCourotine != null)
			StopCoroutine(lastInvincibleCourotine);

		yield return new WaitForSeconds(duration);


		gameObject.layer = LayerMask.NameToLayer("Player");
		_meshRenderer.material = defaultPlayerMaterial;
		_speed = _defaultSpeed;
	}
}
