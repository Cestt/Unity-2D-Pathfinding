﻿using UnityEngine;
using System.Collections;

public class Node{

	public bool walkable;
	public Vector2 worldPosition;

	public Node(bool _walkable, Vector2 _worldPosition){

		walkable = _walkable;
		worldPosition = _worldPosition;
	}

 }
