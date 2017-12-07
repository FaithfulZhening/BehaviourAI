using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public GameObject wall;
	public GameObject agent;

	private GameObject wallHolder;

	private int[,] map = new int[24, 76];

	public Destination[] destinations= new Destination[8];
	// Use this for initialization
	void Start () {
		generateWallPosition ();
		generateWall ();
		generateAgent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//generate wall
	void generateWall(){
		wallHolder = new GameObject ("walls");
		wallHolder.transform.parent = gameObject.transform;	
		GameObject tempWall;
		int desIndex = 0;
		for (int i = 0; i < 24; i++) {
			for (int j = 0; j <= 56; j++) {
				if (map [i, j] == 1) {
					tempWall = Instantiate (wall, new Vector3 (i, 0.5f, j), Quaternion.identity);
					tempWall.tag = "wall";
					tempWall.transform.parent = wallHolder.transform;
				}
				if (map [i, j] == 2) {
					destinations [desIndex] = new Destination(i, j);
					desIndex++;
				}
			}
		}

	}

	void generateAgent(){
		int index = Random.Range(0,8);
		agent = Instantiate (agent, destinations [index].pos, Quaternion.identity);
		agent.transform.parent = gameObject.transform;
	}

	//this method is used to generate the position of the wall
	void generateWallPosition(){
		for (int i = 0; i < 24; i++) {
			if (i == 0 || i == 23) {
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9] = 1;
					map [i, j * 9 + 1] = 1;
					map [i, j * 9 + 2] = 1;
					map [i, j * 9 + 3] = 1;
					map [i, j * 9 + 4] = 1;
				}
			}
			if (i == 1 || i == 22){
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9 + 2] = 2;
				}
				
			}
			if (i < 6 || i > 18) {
				for (int j = 1; j <= 4; j++) {
					map [i, j * 9] = 1;
					map [i, j * 9 + 4] = 1;
				}
			}
			if (i == 5 || i == 18 ){
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
public class Destination{
	public Vector3 pos;
	public bool visited = false;
	public Destination(int xPos, int yPos){
		this.pos = new Vector3 ((float)xPos, 0, (float)yPos);
	}
}