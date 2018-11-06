using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchoScript : MonoBehaviour {

    [SerializeField] int danyo = 20;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            // El pincho le inflinge un daño al jugador de danyo
            collision.gameObject.GetComponent<Player>().QuitarVida(danyo);
        }
    }
}
