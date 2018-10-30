using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float speed = 5f;
    Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {

        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float xPos = Input.GetAxis("Horizontal");
        float ySpeed = rb2D.velocity.y;
        rb2D.velocity = new Vector3(xPos, ySpeed);
		
	}
}
