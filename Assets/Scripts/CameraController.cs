using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
                                   
    void Start () {
        offset = transform.position - player.transform.position;
    }
	
	
	void Update () {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //player.
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        //transform.position = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f) + new Vector3(0, 0, -10);
    }
}
