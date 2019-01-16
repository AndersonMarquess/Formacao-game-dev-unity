using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private JogadorController jogadorController;
    public Slider barraVidaJogador;
    public GameObject painelGameOver;

    /// <summary>
    /// Procura a referência para variável jogadorController 
    /// e atribui o valor máximo da barra, equivalente ao valor inicial da vida do jogador.
    /// </summary>
    void Start() {
        jogadorController = GameObject.FindWithTag("Jogador").GetComponent<JogadorController>();
        barraVidaJogador.maxValue = jogadorController.status.vidaTotal;
        atualizarBarraVidaJogador();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Atualiza o valor atual da barra de vida do jogador, 
    /// com base no valor da vida do jogador presente no script status.
    /// </summary>
    public void atualizarBarraVidaJogador() {
        barraVidaJogador.value = jogadorController.status.vidaAtual;
    }

    /// <summary>
    /// Ativa o painel com a mensagem de game over.
    /// </summary>
    public void mostrarPainelGameOver() {
        painelGameOver.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Carrega a scene no index 0 da build
    /// </summary>
    public void reiniciarScene() {
        SceneManager.LoadScene(0);
    }
}
