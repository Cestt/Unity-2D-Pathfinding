using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	Grid grid;

	void Awake(){
		grid = GetComponent<Grid>();
	}

	void FindPath(Vector3 startPosition, Vector3 targetPosition){
		Node startNode = grid.NodeFromWorldPosition(startPosition);
		Node targetNode = grid.NodeFromWorldPosition(targetPosition);

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		openSet.Add(startNode);

		while(openSet.Count >0){
			Node currentNode = openSet[0];

			for(int i = 1; i < openSet.Count; i++){

				if(openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost & openSet[i].hCost < currentNode.hCost){
					currentNode = openSet[i];
				}

				openSet.Remove(currentNode);
				closedSet.Add(currentNode);

				if(currentNode == targetNode){
					return;
				}

				foreach(Node neighbour in grid.GetNeighbours(currentNode)){
					if(!neighbour.walkable || closedSet.Contains(neighbour)){
						continue;
					}
				}
			}
		}

	}
}
