using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {

    [SerializeField] Text textPuntuacion;
    [SerializeField] float speed = 5f;
    int vidasMaximas = 3;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] int vidas;
    [SerializeField] int puntos = 0;
    Rigidbody2D rb2D;
    [SerializeField] Transform posPies;
    [SerializeField] float radioOverlap = 0.01f;
    	
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        textPuntuacion.text = "Score: " + puntos.ToString();
	}
		
	void FixedUpdate () {   
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        float ySpeedActual = rb2D.velocity.y;

        // Lo movemos en Vertical, es decir, que salte
        if (yPos > 0) {
            // Vamos a comprobar si el personaje esta en el suelo o en el aire
            if (EstaEnSuelo()) {
                rb2D.velocity = new Vector2(xPos * speed, jumpForce);
            } else {
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            }
        } else {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
        }  
	}

    private bool EstaEnSuelo() {
        bool enSuelo = false;
        Collider2D[] cols = Physics2D.OverlapCircleAll(posPies.position, radioOverlap);
        for(int i=0; i < cols.Length; i++) {
            if(cols[i].gameObject.tag == "Suelo") {
                enSuelo = true;
                break;
            }
        }
        return enSuelo;
    }

    public void IncrementarPuntuacion(int puntosAIncrementar) {
        puntos += puntosAIncrementar;
        textPuntuacion.text = "Score: " + puntos.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Moneda")) {
           
            IncrementarPuntuacion(1);
            Destroy(collision.gameObject);
        }
    }
}
