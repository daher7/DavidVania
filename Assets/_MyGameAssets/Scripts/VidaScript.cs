using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaScript : MonoBehaviour {

     [SerializeField] int vida = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().RecibirVida(vida);
            Destroy(gameObject);
        }
    }
}
