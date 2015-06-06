using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	Grid grid;

	public Transform Seeker,Target;

	void Awake(){
		grid = GetComponent<Grid>();
		Debug.Log("Starting");

	}
	void Update(){
		FindPath(Seeker.position,Target.position);
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
			}

				openSet.Remove(currentNode);
				closedSet.Add(currentNode);

				if(currentNode == targetNode){
					RetracePath(startNode,targetNode);
					return;
				}

				foreach(Node neighbour in grid.GetNeighbours(currentNode)){
					if(!neighbour.walkable || closedSet.Contains(neighbour)){
						continue;
					}

					int newMovementCostToNeighbour = currentNode.gCost + Getdistance(currentNode,neighbour);

					if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = Getdistance(neighbour,targetNode);
						neighbour.parent =currentNode;

						if(!openSet.Contains(neighbour))
							openSet.Add(neighbour);

					}
				}
			}
	}

	void RetracePath(Node startNode, Node endNode){

		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while(currentNode != startNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse();

		grid.path = path;
	}

	int Getdistance(Node nodeA, Node nodeB){
		int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if(disX > disY)
			return 14*disY + 10*(disX - disY);
		return 14*disX + 10*(disY - disX);
	}
}
