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
        imagenesVida = new Image[5];
        // Recorremos el array con las vidas
        for (int i = 0; i < imagenesVida.Length; i++) {
            imagenesVida[i] = Instantiate(prefabImagenVida, panelVidas.transform);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
