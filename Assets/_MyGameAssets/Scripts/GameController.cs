using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Declaramos las variables como constantes para evitar errores
    private const string XPOS = "xPos";
    private const string YPOS = "yPos";

    // Guardar Posicion
    public static void StorePosition(Vector2 position) {
        PlayerPrefs.SetFloat(XPOS, position.x);
        PlayerPrefs.SetFloat(YPOS, position.y);
        PlayerPrefs.Save();
    }

    public static Vector2 GetPosition() {
        // Validamos que haya habido antes un guardado en un CheckPoint
        Vector2 position;
        if (PlayerPrefs.HasKey(XPOS) && PlayerPrefs.HasKey(YPOS)) {
            float x = PlayerPrefs.GetFloat(XPOS);
            float y = PlayerPrefs.GetFloat(YPOS);
            position = new Vector2(x, y);
            print(x + ":" + y);
        } else {
            position = Vector2.zero;
        }
        return position;
    }
}
