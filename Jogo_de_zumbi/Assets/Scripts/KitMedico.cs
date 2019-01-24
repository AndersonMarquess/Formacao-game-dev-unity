using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {
    private int _valorDeCura = 15;
    private int _tempoDeDestruicao = 5;

    /// <summary>
    /// Destrói o kit médico após o exceder o tempo especificado.
    /// </summary>
    private void Start() {
        Destroy(gameObject, _tempoDeDestruicao);
    }

    //Recebe o Trigger do boxCollider
    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag(Tags.Jogador)) {

            other.gameObject.GetComponent<JogadorController>().curarVida(_valorDeCura);
            Destroy(gameObject);
        }
    }

}
