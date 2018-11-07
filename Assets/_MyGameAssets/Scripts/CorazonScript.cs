using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonScript : MonoBehaviour {

    [SerializeField] int salud = 50;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().RecibirSalud(salud);
            Destroy(gameObject);
        }
    }

}
