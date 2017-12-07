using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedestrianGenerator : MonoBehaviour {
	public GameObject pedestrian;
	public float speed;
	public int numberOfPedestrians;
	public Text failureUI;
	private GameObject pedestrianHolder;
	private GameObject[] pedestrianObjectHolder;
	private MapGenerator env;
	public Pedestrian[] pedestrians;
	// Use this for initialization
	void Start () {
		env = transform.GetComponent<MapGenerator> ();
		pedestrians = new Pedestrian[numberOfPedestrians];
		pedestrianObjectHolder = new GameObject[numberOfPedestrians];
		pedestrianHolder = new GameObject ("pedestrians");
		pedestrianHolder.transform.parent = gameObject.transform;	
	}

	// Update is called once per frame
	void Update () {
		generatePedestrain ();
		managePedestrainMove ();
		destroyPedestrain ();
		manageFailure ();
		//Debug.Log (env.agent.transform.position.x);
	}

	void generatePedestrain(){
		
		for(int i = 0; i < numberOfPedestrians; i++) {
			if (pedestrians[i] == null) {
				Pedestrian tempPed;
				GameObject tempPedObject;
				int pos = Random.Range (0, 4);
				Vector3 tempGoal;
				switch (pos) {
				case 0:
					tempPed = new Pedestrian (7, 52);
					tempPed.lane = 1;
					tempGoal = new Vector3 (7f, 1f, 5f);
					break;
				case 1:
					tempPed = new Pedestrian (10,52);
					tempPed.lane = 2;
					tempGoal = new Vector3 (10f, 1f, 5f);
					break;
				case 2:
					tempPed = new Pedestrian (13,52);
					tempPed.lane = 3;
					tempGoal = new Vector3 (13f, 1f, 5f);
					break;
				case 3:
					tempPed = new Pedestrian (16,52);
					tempPed.lane = 4;
					tempGoal = new Vector3 (16f, 1f, 5f);
					break;
				default:
					tempPed = null;
					tempGoal = new Vector3(0f, 0f , 0f);
					break;
				}
				List<Move> pattern = generatePattern (0, 47, 0, 2);
				tempPedObject = Instantiate (pedestrian, tempPed.position, Quaternion.identity);
				tempPed.goal = tempGoal;
				tempPed.pattern = pattern;
					foreach (var j in pattern) {
						Debug.Log (j.pattern);
					}
				tempPed.speed = speed;
				pedestrians [i] = tempPed;
				pedestrianObjectHolder [i] = tempPedObject;
				tempPedObject.tag = "wall";
				tempPedObject.transform.parent = pedestrianHolder.transform;
				//Pedestrain tempPed = new Pedestrain ();
			}
		}
	}

	//this function controls the movement of pedestrain
	/*
	 * pattern include 0 : move
	 * 	Activity: 1	   2 : ChangeToLine1, 3: ChangeToLine2, 4: ChangeToLine3, 5:ChangeToLine4,
	 * 				   6 : Wait, 7 : change speed, 8 : change direction in a lane
	 * 				   9 : turn around
	 * 				   
	*/
	void managePedestrainMove(){
		
		for (int i = 0; i < numberOfPedestrians; i++) {
			float step = pedestrians [i].speed * Time.deltaTime;
			int curPattern;
			if (pedestrians [i] != null) {
				bool finished = true;
				foreach (var j in pedestrians [i].pattern) {
					if (!j.finished) {
						finished = false;
						curPattern = j.pattern;
						Vector3 tempV3 = pedestrianObjectHolder [i].transform.position;
						if (curPattern == 0 && j.started == false) {
							float change = Random.Range (-0.2f, 0.2f);
							if (pedestrians [i].direction) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + change, tempV3.y, tempV3.z - j.length);
							} else {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + change, tempV3.y, tempV3.z + j.length);
							}
							j.started = true;
						}
						// change to lane 1
						if (curPattern == 2 && j.started == false) {
							if (pedestrians [i].lane == 2) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 3f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 3) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 6f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 4) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 9f ,tempV3.y,tempV3.z);
							}
							pedestrians [i].lane = 1;
							j.started = true;
						}
						// change to lane 2
						if (curPattern == 3 && j.started == false) {
							if (pedestrians [i].lane == 1) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 3f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 3) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 3f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 4) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 6f ,tempV3.y,tempV3.z);
							}
							pedestrians [i].lane = 2;
							j.started = true;
						}
						// change to lane 3
						if (curPattern == 4 && j.started == false) {
							if (pedestrians [i].lane == 1) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 6f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 2) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 3f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 4) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x - 3f ,tempV3.y,tempV3.z);
							}
							pedestrians [i].lane = 3;
							j.started = true;
						}
						// change to lane 4
						if (curPattern == 5 && j.started == false) {
							if (pedestrians [i].lane == 1) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 9f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 2) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 6f ,tempV3.y,tempV3.z);
							}
							if (pedestrians [i].lane == 3) {
								pedestrians [i].tempGoal = new Vector3 (tempV3.x + 3f ,tempV3.y,tempV3.z);
							}
							pedestrians [i].lane = 4;
							j.started = true;
						}
						if (curPattern == 6 && pedestrians [i].wait == false) {
							StartCoroutine(stop (pedestrians [i] , j));
						}
						if (curPattern == 7 && j.started == false) {
							pedestrians [i].speed = Random.Range (6.0f, 8.0f);
							j.finished = true;
							j.started = true;
						}
						if (curPattern == 8) {
							//this part is now implemented with pattern 0
							//float change = Random.Range (-1.0f, 1.0f)
							//tempGoal = new Vector3 (tempV3.x, tempV3.y,tempV3.z);
							j.finished = true;
						}
						if (curPattern == 9) {
							pedestrians [i].direction = !pedestrians [i].direction;
							j.finished = true;
						}
						if (!pedestrians [i].wait) {
							pedestrianObjectHolder [i].transform.position = Vector3.MoveTowards (pedestrianObjectHolder [i].transform.position, pedestrians [i].tempGoal,step);
							if (Vector3.Distance (pedestrianObjectHolder [i].transform.position, pedestrians [i].tempGoal) < 0.01f) {
								j.finished = true;
							} 
							break;
						}
					}
				}
				if (finished){
					pedestrians [i].tempGoal = new Vector3 (pedestrianObjectHolder [i].transform.position.x.1f,5f);
					pedestrianObjectHolder [i].transform.position = Vector3.MoveTowards (pedestrianObjectHolder [i].transform.position, pedestrians [i].tempGoal,step);
				}
			}
		}
	}

	IEnumerator stop(Pedestrian p, Move j){
		p.wait = true;
		int waitTime = Random.Range (1, 4);
		yield return new WaitForSeconds(waitTime);
		p.wait = false;
		j.finished = true;
	}

	//if the agent get too close to pedestrians outside a room, indicates failure
	void manageFailure(){
		for (int i = 0; i < numberOfPedestrians; i++) {
			if (pedestrians [i] != null) {
				if ( (System.Math.Abs(env.agent.transform.position.x - pedestrianObjectHolder [i].transform.position.x) < 3.01 &&
					System.Math.Abs(env.agent.transform.position.z - pedestrianObjectHolder [i].transform.position.z) < 3.01) && 
					( env.agent.transform.position.x > 5 && env.agent.transform.position.x < 18) ) {
					failureUI.text = "Failure! Agent too close to prdestrian. Please Restart";
				}
			}
		}

	}

	void destroyPedestrain(){
		for (int i = 0; i < numberOfPedestrians; i++) {
			if (pedestrians [i] != null ) {
				if (System.Math.Abs(pedestrianObjectHolder [i].transform.position.z - pedestrians [i].goal.z) < 0.1) {
					Destroy (pedestrianObjectHolder [i]);
					pedestrians [i] = null;
				}
			}
		}
	}
	//generate pattern
	public List<Move> generatePattern(int s ,float length, int depth, int maxDepth){
		List<Move> returnList = new List<Move>();
		if (depth < maxDepth) {
			if (s == 0) {
				int patternIndex = Random.Range (0, 2);
				if (patternIndex == 0) {
					returnList.AddRange (generatePattern (0,(float)3*length/5,depth+1,maxDepth));
					returnList.AddRange (generatePattern (1));
					returnList.AddRange (generatePattern (0,(float)2*length/5,depth+1,maxDepth));
				}
				if (patternIndex == 1) {
					returnList.AddRange (generatePattern (0,(float)length/2,depth+1,maxDepth));
					returnList.Add (new Move(0f,9));
					returnList.AddRange (generatePattern (0,(float)length/2,depth+1,maxDepth));
					returnList.Add (new Move(0f,9));
					returnList.AddRange (generatePattern (0,(float)length,depth+1,maxDepth));
				}
			}
		} else {
			//Debug.Log (le);
			returnList.Add (new Move(length,s));
		}
		return returnList;
	}

	public List<Move> generatePattern (int s){
		int patternIndex = Random.Range (2, 9);
		List<Move> returnList = new List<Move>();
		switch (patternIndex) {
		case 2:
			returnList.Add (new Move(0f,2));
			break;
		case 3:
			returnList.Add (new Move(0f,3));
			break;
		case 4:
			returnList.Add (new Move(0f,4));
			break;
		case 5:
			returnList.Add (new Move(0f,5));
			break;
		case 6:
			returnList.Add (new Move(0f,6));
			break;
		case 7:
			returnList.Add (new Move(0f,7));
			break;
		case 8:
			returnList.Add (new Move(0f,8));
			break;
		default:
			Debug.Log ("error");
			break;
		}
		return returnList;
	}

}



public class Pedestrian{
	/*
	 * pattern include 0 : move
	 * 	Activity: 1	   2 : ChangeToLine1, 3: ChangeToLine2, 4: ChangeToLine3, 5:ChangeToLine4,
	 * 				   6 : Wait, 7 : change speed, 8 : change direction in a lane
	 * 				   9 : turn around
	 * 				   
	*/
	public List<Move> pattern = new List<Move>();			//moving pattern
	public Vector3 position;
	public Vector3 goal;
	public Vector3 tempGoal;
	public int lane;
	public float speed;
	public bool wait;
	public bool direction;         //true means forward, false means backward
	public Pedestrian(int xPos, int yPos){
		this.position = new Vector3 ((float)xPos, 1, (float)yPos);
		lane = Random.Range (1, 5);
		this.wait = false;
		this.direction = true;
	}
}

public class Move{
	public float length;
	public int pattern;
	public bool finished;
	public bool started;

	public Move(float length, int pattern){
		this.length = length;
		this.pattern = pattern;
		this.finished = false;
		this.started = false;
	}
}
