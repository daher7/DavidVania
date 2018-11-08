using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mosca : MonoBehaviour {

    Slider slider;
    bool haciaDerecha = false;
    public Transform limiteDerecho;
    public Transform limiteIzquierdo;
    [SerializeField] int danyo = 20;
    [SerializeField] int vida = 20;

    private void Start() {

        
        // La mosca va estar inicialmente en el limite derecho
        transform.position = limiteDerecho.position;
        slider = GetComponentInChildren<Slider>();
        QuitarVida(50);
        print(slider.gameObject.name);
    }

    void Update () {

        if (haciaDerecha) {
            transform.Translate(Vector2.right * Time.deltaTime);
            if(transform.position.x > limiteDerecho.position.x) {
                haciaDerecha = false;
                CambiarSentido();
            }
        } else {
            transform.Translate(Vector2.left * Time.deltaTime);
            // cuando llegue al limite izdo la mosca dara la vuelta
            if(transform.position.x < limiteIzquierdo.position.x) {
                haciaDerecha = true;
                CambiarSentido();
            }
        }
	}

    void CambiarSentido() {
        if (haciaDerecha) {
            transform.localScale = new Vector2(-1, 1);
        } else {
            transform.localScale = new Vector2(1, 1);
        }    
    }

  
    private void OnCollisionEnter2D(Collision2D collision) {
        print("OnEnter2D");
        if(collision.gameObject.CompareTag("Player")) {
            // La mosca le inglinge un daño al jugador de danyo
            collision.gameObject.GetComponent<Player>().QuitarVida(danyo);
        }
    }

    public void QuitarVida(int vida) {
        slider.value -= vida;
    }
}
