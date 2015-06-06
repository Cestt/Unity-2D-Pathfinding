﻿using UnityEngine;
using System.Collections;

public class Node{

	public bool walkable;
	public Vector2 worldPosition;
	public int gCost;
	public int hCost;
	public int gridX;
	public int gridY;

	public Node(bool _walkable, Vector2 _worldPosition, int _gridX, int _gridY){

		walkable = _walkable;
		worldPosition = _worldPosition;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fcost{
		get{
			return gCost + hCost;
		}
	}

 }
