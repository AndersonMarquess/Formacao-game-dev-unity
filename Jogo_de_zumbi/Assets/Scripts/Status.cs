using UnityEngine;

/// <summary>
/// Classe que representa os valores comuns a todos os personagem, sendo estes,
/// os valores da vida total e vida atual e a velocidade de movimento.
/// </summary>
public class Status : MonoBehaviour {

    public int velocidade = 10;
    public int vidaTotal = 100;
    [HideInInspector]
    public int vidaAtual;

    private void Awake() {
        vidaAtual = vidaTotal;
    }
}
