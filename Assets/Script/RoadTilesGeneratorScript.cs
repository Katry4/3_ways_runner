using UnityEngine;
using System.Collections;

public class RoadTilesGeneratorScript : MonoBehaviour
{

	[SerializeField] private GameObject roadTilePref;
	private Vector3 tileSize;

	// Use this for initialization
	void Start()
	{
		float tileSizeX = roadTilePref.GetComponent<BoxCollider>().size.x;
		tileSize = new Vector3(tileSizeX, 0, 0);

		SimplePool.Preload(roadTilePref, 5);
		for (int i = 0; i < 5; i++)
		{
			SpawnTile();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		SpawnTile();
		DespawnTile();
	}

	void SpawnTile()
	{
		
		Vector3 prevTilePos;
		if (transform.childCount > 0)
			prevTilePos = transform.GetChild(transform.childCount - 1).localPosition;
		else
			prevTilePos = Vector3.zero - tileSize;
		
		Vector3 nextPos = prevTilePos + tileSize;
		GameObject tile = SimplePool.Spawn(roadTilePref, nextPos, roadTilePref.transform.rotation);
		tile.transform.parent = transform;
	}

	void DespawnTile()
	{
		GameObject tile = transform.GetChild(0).gameObject;
		tile.transform.parent = null;
		SimplePool.Despawn(tile);
	}
}
