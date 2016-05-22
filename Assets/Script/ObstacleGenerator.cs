using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public GameObject obstaclePrefab;
	private int _nextRoadIndex = 99;
	private int _nextGeneration = 0;

	public int obstacleMinStep = 3;
	public int obstacleMaxStep = 6;

	// Use this for initialization
	void Start () {
		SimplePool.Preload(obstaclePrefab, 3);
		UpdateNextIndexes();
	}


	public void TryToAddObstacle(GameObject tile, int roadIndex, int generation)
	{
		if (generation > _nextGeneration)
		{
			if (roadIndex == _nextRoadIndex)
			{
				GameObject obstacle = SimplePool.Spawn(obstaclePrefab, obstaclePrefab.transform.position, obstaclePrefab.transform.rotation);
				obstacle.transform.parent = tile.transform;
				obstacle.transform.localPosition = obstaclePrefab.transform.localPosition;
				UpdateNextIndexes();
			}
		}
	}

	private void UpdateNextIndexes()
	{
		_nextRoadIndex = getNextRoadIndex(_nextRoadIndex);
		_nextGeneration += Random.Range(obstacleMinStep, obstacleMaxStep);
	}

	public int getNextRoadIndex(int currentIndex)
	{
		int res = Random.Range(0, 3);
		while (res == currentIndex)
		{
			res = Random.Range(0, 3);
		}
		return res;
	}
}
