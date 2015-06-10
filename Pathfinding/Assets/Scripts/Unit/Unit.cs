﻿using UnityEngine;
using System;
using System.Collections;

public class Unit : MonoBehaviour {

	public Transform target;
	public float speed;
	public bool procesing;
	Vector3[] path;
	public Vector3 oldTargetPos;
	int targetIndex;
	Agent agent;
	PathRequestManager.PathRequest queueIndex;
	Action<string> currentCallback;
	Action pathFinished;


	public void PathTry(Vector3 tPosition, Action<string> callback, Action finished){
		queueIndex = PathRequestManager.RequestPath(transform.position,tPosition, OnPathFound);
		currentCallback = callback;
		pathFinished = finished;
	}
	public void OnPathFound(Vector3[] newPath,bool pathSuccessful){
		if(pathSuccessful){
			path = newPath;
			targetIndex = 0;
			currentCallback("Working");
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
			procesing = true;
		}
	}
	IEnumerator FollowPath(){

			
			Vector3 currentWayPoint = path[0];
		Debug.Log(path.Length);
			while(true){
				if(transform.position == currentWayPoint){
					targetIndex++;
					if(targetIndex >= path.Length){
						procesing = false;
						pathFinished();
						yield break;
					}
					currentWayPoint = path[targetIndex];
				}
				transform.position = Vector3.MoveTowards(transform.position,currentWayPoint,speed * Time.fixedDeltaTime);
				yield return null;
			}
		
	}



}
