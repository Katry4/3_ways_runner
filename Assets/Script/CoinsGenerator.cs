using UnityEngine;
using System.Collections;

public class CoinsGenerator : MonoBehaviour {

	public GameObject coinsPrefab;
	private int _nextRoadIndex = 99;
	private int _nextGeneration = 0;

	public int obstacleMinStep = 3;
	public int obstacleMaxStep = 6;

	// Use this for initialization
	void Start () {
		SimplePool.Preload(coinsPrefab, 3);
		UpdateNextIndexes();
	}


	public void TryToAddObstacle(Transform tile, int roadIndex, int generation)
	{
		if (generation > _nextGeneration)
		{
			if (roadIndex == _nextRoadIndex)
			{
				if (tile.childCount == 1)
				{
					tile = tile.GetChild(0);
				}
				GameObject obstacle = SimplePool.Spawn(coinsPrefab, coinsPrefab.transform.position, coinsPrefab.transform.rotation);
				obstacle.transform.parent = tile;
				obstacle.transform.localPosition = coinsPrefab.transform.localPosition;
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
