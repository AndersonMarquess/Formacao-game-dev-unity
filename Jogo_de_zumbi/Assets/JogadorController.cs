using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorController : MonoBehaviour {

    public int velocidade = 10;

    void Update() {
        movimentacao();
    }


    /// <summary>
    /// Movimenta o personagem com base no input recebido
    /// </summary>
    private void movimentacao() {
        var horizontal = Input.GetAxis("Horizontal");//X
        var vertical = Input.GetAxis("Vertical");//Z

        Vector3 direcao = new Vector3(horizontal, 0, vertical);
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }
}
