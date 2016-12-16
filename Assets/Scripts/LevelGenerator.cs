using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	[SerializeField]
	GameObject house_prefab;
	[SerializeField]
	float yPos_min_houses;
	[SerializeField]
	float yPos_max_houses;
	[SerializeField]
	float xPos_min_houses;
	[SerializeField]
	float xPos_max_houses;
	[SerializeField]
	Transform ground;

	GameObject player_obj;
	Transform player_transform;

	bool inited;
	bool generating;

	IEnumerator generatingCoroutine;

	public GameObject frontMostHouse_obj;


	// Use this for initialization
	void Start () {

	}

	void Init()
	{
		inited = true;
		player_obj = GameManager.player;
		player_transform = player_obj.transform;
		generatingCoroutine = GenerateLevel ();
	}

	// Update is called once per frame
	void Update () {
		if (!inited)
			Init ();

		if (GameManager.running) {
			if(!generating){
				generating = true;
				SetGenerating (generating);
			}
		}
		else {
			if(generating){
				generating = false;
				SetGenerating (generating);
			}
		}
	}

	void SetGenerating(bool state){
		if (state) {
			StartCoroutine (generatingCoroutine);
		}
		else {
			StopCoroutine (generatingCoroutine);
		}
	}

	IEnumerator GenerateLevel(){
		while(true){
			House frontMostHouse_house = frontMostHouse_obj.GetComponent<House> ();
			while(!frontMostHouse_house.isSeen){
				Debug.Log ("Waiting for front most house to be seen");
				yield return null;
			}
			float newY = UnityEngine.Random.Range (yPos_min_houses, yPos_max_houses);
			float newX = UnityEngine.Random.Range (xPos_min_houses, xPos_max_houses) + frontMostHouse_obj.transform.position.x;
			Vector3 newPos = new Vector3 (newX, newY, 0);


			frontMostHouse_obj = GameObject.Instantiate (house_prefab, newPos, Quaternion.identity);
			frontMostHouse_obj.transform.parent = ground;
			Debug.Log ("New House Created");

			yield return null;
		}
	}
}
