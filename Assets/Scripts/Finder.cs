using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour {

	//public Transform destinationPoint;
	public UnityEngine.AI.NavMeshAgent agent;

	private MapGenerator env;
	private Destination[] destinations;
	private Vector3 goal = new Vector3(10f,0f,53f);
	private bool visitedAll;
	// Use this for initialization
	void Start () {
		visitedAll = false;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		env = transform.parent.GetComponent<MapGenerator> ();
		destinations = env.destinations;
	}
	
	// Update is called once per frame
	void Update () {
		//agent.destination = destinationPoint.position;
		behaviourTree();	
	}

	void behaviourTree(){
		visitedAll = true;
		for (int i = 0; i < 8; i++) {
			if (System.Math.Abs (transform.position.x - destinations [i].pos.x) < 0.1 &&
				System.Math.Abs (transform.position.z - destinations [i].pos.z) < 0.1) {
				destinations [i].visited = true;
			}

		}
		for (int i = 0; i < 8; i++) {
			if (!destinations [i].visited) {
				visitedAll = false;
				agent.destination = destinations [i].pos;
				break;
			}
		}

		if (visitedAll) {
			agent.destination = goal;
		}
	}

}
