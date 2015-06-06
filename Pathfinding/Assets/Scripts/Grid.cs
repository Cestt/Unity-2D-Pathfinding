using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	Node[,] grid;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeSize;

	int gridSizeX, gridSizeY;

	void Awake(){
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeSize);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeSize);

		CreateGrid();
	}

	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridWorldSize.y/2;


		for(int i = 0; i < gridSizeX; i++){

			for(int j = 0; j < gridSizeY; j++){
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeSize + (nodeSize/2)) + Vector3.up * (j * nodeSize + (nodeSize/2));
				bool walkable = !(Physics2D.OverlapCircle(worldPoint,nodeSize/2,unwalkableMask));
				grid[i,j] = new Node(walkable,worldPoint);
			}

		}
	}

	public Node NodeFromWorldPosition(Vector3 worldPosition){

		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percetnY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percetnY = Mathf.Clamp01(percetnY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percetnY);

		return grid[x,y];
	}

	void OnDrawGizmos(){

		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,gridWorldSize.y,1));

		if(grid != null){
		
			foreach(Node n in grid){
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeSize-.1f));
			}
		}
	}
}
