using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Image prefabImagenVida;
    public GameObject panelVidas;
    public Player script;
    Image[] imagenesVida;
    private int numeroVidas;
    //Image nuevaImagen;

    void Start() {
        // Para saber las vidas que tiene el personaje necesitamos acceder al script del Player
        numeroVidas = script.GetVidas();
        imagenesVida = new Image[numeroVidas];
        // Recorremos el array con las vidas
        for (int i = 0; i < imagenesVida.Length; i++) {
            imagenesVida[i] = Instantiate(prefabImagenVida, panelVidas.transform);
        }
    }

    public void RestarVida() {
        numeroVidas = script.GetVidas();
        for (int i = numeroVidas; i < imagenesVida.Length; i++) {
            imagenesVida[i].color = new Color32(160, 160, 160, 128);
            if (numeroVidas == 0) {
                print("MUERTO");
            }
        }
        // Version mala para destruir las vidas
        /*
        numeroVidas = script.GetVidas();
        for (int i = numeroVidas; i < imagenesVida.Length; i++) {
            if (imagenesVida[i] != null) {
                Destroy(imagenesVida[i].gameObject);
            }
        }
        */
    }
    public void SumarVida() {
        numeroVidas = script.GetVidas();
        print("SUMAR VIDA:" + numeroVidas);
        for (int i = 0; i < numeroVidas; i++) {
            imagenesVida[i].color = new Color32(255, 255, 255, 255);
        }
    }
}
