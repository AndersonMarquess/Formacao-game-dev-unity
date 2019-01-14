using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private JogadorController jogadorController;
    public Slider barraVidaJogador;

    /// <summary>
    /// Procura a referência para variável jogadorController 
    /// e atribui o valor máximo da barra, equivalente ao valor inicial da vida do jogador.
    /// </summary>
    void Start() {
        jogadorController = GameObject.FindWithTag("Jogador").GetComponent<JogadorController>();
        barraVidaJogador.maxValue = jogadorController.status.vidaTotal;
        atualizarBarraVidaJogador();
    }

    /// <summary>
    /// Atualiza o valor atual da barra de vida do jogador, 
    /// com base no valor da vida do jogador presente no script status.
    /// </summary>
    public void atualizarBarraVidaJogador() {
        barraVidaJogador.value = jogadorController.status.vidaAtual;
    }
}
