using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private JogadorController jogadorController;
    public Slider barraVidaJogador;
    public GameObject painelGameOver;
    public Text textoTempoSobrevivido;

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
        Time.timeScale = 0;
        painelGameOver.SetActive(true);
        atualizarTextoTempoSobrevivido();
    }

    /// <summary>
    /// Atualizar o texto com a duração da partida até a hora da morte.
    /// </summary>
    private void atualizarTextoTempoSobrevivido() {
        textoTempoSobrevivido.text = $"Você sobreviveu por {calcularTempoSobrevivido()}";
    }

    /// <summary>
    /// Calcula o tempo desde o carregamento do level, divide por 60 e obtêm os minutos
    /// Divide por 60 e pega apenas o resto da divisão para considerar como segundos.
    /// </summary>
    /// <returns></returns>
    private string calcularTempoSobrevivido() {
        //Conta quanto tempo desde o load da scene.
        var minutos = (int) Time.timeSinceLevelLoad / 60;
        var segundos = (int) Time.timeSinceLevelLoad % 60;
        
        return $"{minutos}m e {segundos}s";
    }

    /// <summary>
    /// Carrega a scene no index 0 da build
    /// </summary>
    public void reiniciarScene() {
        SceneManager.LoadScene(0);
    }
}
