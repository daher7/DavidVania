using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portada : MonoBehaviour {

    [SerializeField] RectTransform[] rts;
    [SerializeField] float speed = 1f;

    private void Update() { 
        // Vamos a hacer que la niebla se mueva ciclicamente
        for (int i = 0; i < rts.Length; i++) {
            float xPos = -1 * Time.deltaTime * speed;
            if (rts[i].position.x + rts[i].rect.width < 0) {
                xPos = 1000;
            }
            rts[i].Translate(xPos, 0, 0);
        }
    }

    public void StartGame() {
        // Para cargar la escena principal
        SceneManager.LoadScene(1);
    }
}
