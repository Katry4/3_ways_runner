using UnityEngine;
using System.Collections;

public class RoadTilesGeneratorScript : MonoBehaviour
{
	[SerializeField] private int _roadIndex;
	[SerializeField] private GameObject roadTilePref;
	[SerializeField] private ObstacleGenerator _obstacleGenerator;
	[SerializeField] private CoinsGenerator _coinsGenerator;
	private Vector3 tileSize;
	private Transform lastTile;
	[SerializeField] private PlayerScript _player;

	bool _isInited = false;
	private int _tileGeneration = 0;

	// Use this for initialization
	void OnEnable()
	{
		ClearChildren();

		float tileSizeX = roadTilePref.GetComponent<BoxCollider>().size.x;
		tileSize = new Vector3(tileSizeX, 0, 0);
		lastTile = null;


		if (_player == null)
		{
			_player = FindObjectOfType<PlayerScript>();
		}

		SimplePool.Preload(roadTilePref, 5);
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (_isInited)
		{
			CheckDistanceToGenerateTile();
		}
	}

	private void CheckDistanceToGenerateTile()
	{
		
		if (lastTile == null || _player.transform.position.x + PlayerScript.VisibleDistance > lastTile.position.x)
		{
			SpawnTile();
		}
		Transform firstTile = transform.GetChild(0);
		if (_player.transform.position.x - 1 > firstTile.position.x)
		{
			DespawnTile(firstTile);
		}
	}

	void SpawnTile()
	{
		Vector3 prevTilePos;
		if (transform.childCount > 0)
			prevTilePos = transform.GetChild(transform.childCount - 1).position;
		else
			prevTilePos = transform.position - tileSize;

		Vector3 nextPos = prevTilePos + tileSize;
		GameObject tile = SimplePool.Spawn(roadTilePref, nextPos, roadTilePref.transform.rotation);
		lastTile = tile.transform;
		lastTile.parent = transform;
		_obstacleGenerator.TryToAddObstacle(lastTile, _roadIndex ,_tileGeneration);
		_coinsGenerator.TryToAddObstacle(lastTile, _roadIndex ,_tileGeneration++);
	}

	void DespawnTile(Transform firstTile)
	{
		firstTile.parent = null;
		if (firstTile.childCount > 0)
		{
			foreach (Transform child in firstTile)
			{
				foreach (Transform childChild in child)
				{
					SimplePool.Despawn(childChild.gameObject);
				}
				SimplePool.Despawn(child.gameObject);
			}
		}
		SimplePool.Despawn(firstTile.gameObject);
	}

	private void ClearChildren()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		_isInited = true;
	}
}
