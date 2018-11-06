using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    enum EstadoPlayer { Pausa, AndandoDer, AndandoIzq, Saltando, Sufriendo };
    EstadoPlayer estado = EstadoPlayer.Pausa;

    [SerializeField] LayerMask floorLayer;
    [SerializeField] Text textPuntuacion;
    [SerializeField] float speed = 5f;
    [SerializeField] float xPos;
    int vidasMaximas = 3;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] int vidas;
    [SerializeField] int puntos = 0;
    [SerializeField] int salud;
    int saludMaxima = 100;


    [SerializeField] float fuerzaPinchos;
    [SerializeField] Transform posPies;
    [SerializeField] float radioOverlap = 0.01f;
    Animator playerAnimator;
    Rigidbody2D rb2D;

    public float fuerzaImpactoX = 0.2f;
    public float fuerzaImpactoY = 0.3f;

    void Start() {

        vidas = vidasMaximas;
        salud = saludMaxima;
        rb2D = GetComponent<Rigidbody2D>();
        textPuntuacion.text = "Score: " + puntos.ToString();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            estado = EstadoPlayer.Saltando;
        }
        print(estado + ":" + EstaEnSuelo());
        if (estado == EstadoPlayer.Sufriendo && EstaEnSuelo()) {

            estado = EstadoPlayer.Pausa;
        }
    }

    void FixedUpdate() {
        float xPos = Input.GetAxis("Horizontal");
        float ySpeedActual = rb2D.velocity.y;

        if (estado == EstadoPlayer.Sufriendo) {
            return;
        }

        // Si sufre no puede hacer nada de esto
        if (Mathf.Abs(xPos) > 0.01f) {
            playerAnimator.SetBool("Andando", true);
        } else {
            playerAnimator.SetBool("Andando", false);
        }

        // Vamos a comprobar si el personaje esta en el suelo o en el aire
        if (estado == EstadoPlayer.Saltando) {
            estado = EstadoPlayer.Pausa;
            if (EstaEnSuelo()) {
                rb2D.velocity = new Vector2(xPos * speed, jumpForce);
            } else {
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            }
            // Nos movemos a la derecha
        } else if (xPos > 0.01f) {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);

            transform.localScale = new Vector2(1, 1);
            estado = EstadoPlayer.AndandoDer;
            // Nos movemos a la izquierda
        } else if (xPos < -0.01f) {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);

            transform.localScale = new Vector2(-1, 1);
            estado = EstadoPlayer.AndandoIzq;
        } else {
            estado = EstadoPlayer.Pausa;
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

    public void QuitarVida(int danyo) {
        salud -= danyo;
        if (salud <= 0) {
            vidas--;
            salud = saludMaxima;
        }
        if(estado == EstadoPlayer.AndandoDer) {
            print("aplicando fuerza 1");
            estado = EstadoPlayer.Sufriendo;
            GetComponent<Rigidbody2D>().transform.Translate(new Vector2(0, 0.1f));
            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
        } else if(estado == EstadoPlayer.AndandoIzq) {
            print("aplicando fuerza 2");
            estado = EstadoPlayer.Sufriendo;
            print(fuerzaImpactoX + ":" + fuerzaImpactoY);
            GetComponent<Rigidbody2D>().transform.Translate(new Vector2(0, 0.1f));
            GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
        }
        
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

