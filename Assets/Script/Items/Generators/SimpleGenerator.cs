using UnityEngine;
using System.Collections;

public class SimpleGenerator : MonoBehaviour
{

	[Header("Basic gen info")]

	public GameObject targetPrefab;
	protected int _nextRoadIndex = 99;
	protected int _nextGeneration = 0;

	public int obstacleMinStep = 3;
	public int obstacleMaxStep = 6;

	// Use this for initialization
	void Start()
	{
		SimplePool.Preload(targetPrefab, 3);
		UpdateNextIndexes();
	}


	public virtual bool TryToAddObstacle(Transform tile, int roadIndex, int generation)
	{
		if (generation > _nextGeneration)
		{
			if (roadIndex == _nextRoadIndex)
			{
				bool isParentSwap = false;
				if (tile.childCount == 1)
				{
					tile = tile.GetChild(0);
					isParentSwap = true;
				}
				GameObject obstacle = SimplePool.Spawn(targetPrefab, targetPrefab.transform.position, targetPrefab.transform.rotation);
				obstacle.transform.parent = tile;
				obstacle.transform.localScale = targetPrefab.transform.localScale / tile.transform.localScale.x;
				obstacle.transform.localPosition = targetPrefab.transform.localPosition;
				UpdateNextIndexes();
				if (isParentSwap)
				{
					tile = tile.transform.parent;
				}
				return true;
			}
		}
		return false;
	}

	protected void UpdateNextIndexes()
	{
		_nextRoadIndex = GetNextRoadIndex(_nextRoadIndex);
		_nextGeneration += Random.Range(obstacleMinStep, obstacleMaxStep);
	}

	protected int GetNextRoadIndex(int currentIndex)
	{
		int res = Random.Range(0, 3);
		while (res == currentIndex)
		{
			res = Random.Range(0, 3);
		}
		return res;
	}
}
