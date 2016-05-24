using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemsGeneratorManager : MonoBehaviour
{

	public SimpleGenerator obstacleGenerator;
	public List<SimpleGenerator> bonusesGenerators;

	public void TryToAddObstacle(Transform tile, int roadIndex, int generation)
	{
		obstacleGenerator.TryToAddObstacle(tile, roadIndex, generation);

		foreach (SimpleGenerator generator in bonusesGenerators)
		{
			if (TileCanBeModified(tile))
			{
				if (generator.TryToAddObstacle(tile, roadIndex, generation))
				{
					return;
				}
			}
		}
	}

	private bool TileCanBeModified(Transform tile)
	{
		//if has obstacle
		if (tile.childCount > 0)
		{
			//if obstacle has bonus on top of it
			if (tile.GetChild(0).childCount > 0)
			{
				return false;
			}	
		} 
		return true;
	}
}
