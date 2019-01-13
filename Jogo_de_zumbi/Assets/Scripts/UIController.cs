using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private JogadorController jogadorController;
    public Slider barraVidaJogador;

    /// <summary>
    /// Procura a referência para variável jogadorController 
    /// e atribui o valor máximo da barra de vida do jogador.
    /// </summary>
    void Start() {
        jogadorController = GameObject.FindWithTag("Jogador").GetComponent<JogadorController>();
        barraVidaJogador.maxValue = jogadorController.vida;
    }

    /// <summary>
    /// Atualizar o valor atual da barra de vida do jogador, 
    /// com base na vida do jogador presente no script de JogadorController.
    /// </summary>
    public void atualizarBarraVidaJogador() {
        barraVidaJogador.value = jogadorController.vida;
    }
}
