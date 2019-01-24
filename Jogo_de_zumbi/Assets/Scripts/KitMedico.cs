using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {
    private int _valorDeCura = 15;

    //Recebe o Trigger do boxCollider
    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag(Tags.Jogador)) {

            other.gameObject.GetComponent<JogadorController>().curarVida(_valorDeCura);
            Destroy(gameObject);
        }
    }

}
