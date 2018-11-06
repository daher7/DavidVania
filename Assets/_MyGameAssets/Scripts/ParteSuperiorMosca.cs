using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteSuperiorMosca : MonoBehaviour {

	
    private void OnCollisionEnter2D(Collision2D collision) {
        print("ParteSuperior");
        Destroy(transform.parent.gameObject);
    }
}
