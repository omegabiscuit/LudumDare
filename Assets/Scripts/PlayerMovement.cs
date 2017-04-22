using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMovement : MonoBehaviour {
    Animator anim;
    public float maxSpeed = 10f;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += move * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += move * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += move * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += move * maxSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            
        }

    }
    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Adult"))
        {
            print("you lose");
        }
   }
}
