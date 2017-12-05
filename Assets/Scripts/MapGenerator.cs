using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public GameObject wall;

	private GameObject wallHolder;

	private int[,] map = new int[21, 76];
	// Use this for initialization
	void Start () {
		generateWallPosition ();
		generateWall ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//generate wall
	void generateWall(){
		wallHolder = new GameObject ("walls");
		wallHolder.transform.parent = gameObject.transform;	
		GameObject tempWall;
		for (int i = 0; i < 21; i++) {
			for (int j = 0; j <= 56; j++) {
				if (map [i, j] == 1) {
					tempWall = Instantiate (wall, new Vector3 (i, 0.5f, j), Quaternion.identity);
					tempWall.tag = "wall";
					tempWall.transform.parent = wallHolder.transform;
				}
			}
		}

	}

	//this method is used to generate the position of the wall
	void generateWallPosition(){
		for (int i = 0; i < 21; i++) {
			if (i == 0 || i == 20) {
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9] = 1;
					map [i, j * 9 + 1] = 1;
					map [i, j * 9 + 2] = 1;
					map [i, j * 9 + 3] = 1;
					map [i, j * 9 + 4] = 1;
				}
			}
			if (i < 6 || i > 15) {
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9] = 1;
					map [i, j * 9 + 4] = 1;
				}
			}
			if (i == 5 || i == 16 ){
				for (int j = 0; j <= 56; j++) {
					map [i, j] = 1;
				}
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9 + 1] = 0;
					map [i, j * 9 + 2] = 0;
					map [i, j * 9 + 3] = 0;
				}
			}

		}
	}
}
