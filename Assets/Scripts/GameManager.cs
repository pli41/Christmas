using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameObject player;
	public static bool running = true;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
