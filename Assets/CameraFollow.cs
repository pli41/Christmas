using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    private float offset;

	// Use this for initialization
	void Start () {

        offset = transform.position.x - player.position.x;
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 cameraPos = transform.position;
        cameraPos.x = player.position.x + offset;
        transform.position = cameraPos;
	
	}
}
