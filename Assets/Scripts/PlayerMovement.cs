using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMovement : MonoBehaviour {
    Animator anim;
    public float maxSpeed = 10f;
    private GameObject currentObject;
    ArrayList Inventory = new ArrayList();
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
            if (currentObject != null)
            {
                Inventory.Add(currentObject);
                currentObject.SetActive(false);
            }
        }

    }
    //OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            currentObject = other.gameObject;
     
        }
        if (other.gameObject.CompareTag("Adult"))
        {
            print("you lose");
        }
   }

    void OnTriggerEnd2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            currentObject = null;
        }
    }
}
