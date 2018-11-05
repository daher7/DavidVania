﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField] LayerMask floorLayer;
    [SerializeField] Text textPuntuacion;
    [SerializeField] float speed = 5f;
    [SerializeField] float xPos;
    int vidasMaximas = 3;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] int vidas;
    [SerializeField] int vidaActual = 100;
    [SerializeField] int vidaMaxima = 100;
    [SerializeField] int puntos = 0;
    
    [SerializeField] float fuerzaPinchos;
    [SerializeField] Transform posPies;
    [SerializeField] float radioOverlap = 0.01f;
    Animator playerAnimator;
    Rigidbody2D rb2D;
    public bool saltando = false;
    bool estoyLanzado = false;

    void Start() {

        rb2D = GetComponent<Rigidbody2D>();
        textPuntuacion.text = "Score: " + puntos.ToString();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {
        //xPos = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb2D.AddForce(new Vector3(0, jumpForce));
        }
    }
    /*
    private void FixedUpdate()
    {
        saltando = Physics2D.OverlapCircle(posPies.position, radioOverlap, floorLayer);
        print(saltando);
        rb2D.velocity = new Vector2(xPos * speed, rb2D.velocity.y);
    }
    */

    void FixedUpdate() {
        float xPos = Input.GetAxis("Horizontal");
        float ySpeedActual = rb2D.velocity.y;

        if (!estoyLanzado) {

            if(Mathf.Abs(xPos) > 0.01f) {
                playerAnimator.SetBool("Andando", true);
            } else {
                playerAnimator.SetBool("Andando", false);
            }
                                

            // Vamos a comprobar si el personaje esta en el suelo o en el aire
            if (saltando) {
                saltando = false;
                if (EstaEnSuelo()) {
                    rb2D.velocity = new Vector2(xPos * speed, jumpForce);
                } else {
                    rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
                }
            } else if ((Mathf.Abs(xPos) > 0.01f)) {
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            }
        }
    }

    private bool EstaEnSuelo() {
        bool enSuelo = false;
        Collider2D col = Physics2D.OverlapCircle(posPies.position, radioOverlap, floorLayer);
        if (col != null) {
            enSuelo = true;
        }
        return enSuelo;
    }


    public void IncrementarPuntuacion(int puntosAIncrementar) {
        puntos += puntosAIncrementar;
        textPuntuacion.text = "Score: " + puntos.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        print(collision.gameObject.name);
        //estoyLanzado = false;
        if (collision.gameObject.CompareTag("Moneda")) {
            IncrementarPuntuacion(1);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Pinchos")) {
            QuitarVida(2);
            print("Te has pinchado");
        }

    }

    public void QuitarVida(int danyo) {
        vidaActual -= danyo;
        if (vidaActual <= 0) {
            vidaActual = 0;
        }
        estoyLanzado = true;
        rb2D.AddRelativeForce(new Vector3(fuerzaPinchos * -1, fuerzaPinchos), ForceMode2D.Impulse);
    }
    /*
     * Version basada en el TAG utilizando OverlapCircleAll
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
    }*/

}

