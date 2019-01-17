using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private JogadorController jogadorController;
    public Slider barraVidaJogador;
    public GameObject painelGameOver;
    public Text textoTempoSobrevivido;
    public Text textoTempoRecorde;
    private float tempoRecorde;

    /// <summary>
    /// Procura a referência para variável jogadorController 
    /// e atribui o valor máximo da barra, equivalente ao valor inicial da vida do jogador.
    /// </summary>
    void Start() {
        jogadorController = GameObject.FindWithTag("Jogador").GetComponent<JogadorController>();
        barraVidaJogador.maxValue = jogadorController.status.vidaTotal;
        atualizarBarraVidaJogador();
        Time.timeScale = 1;
        tempoRecorde = recuperarTempoRecorde();
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
    /// Chama o método para verificar novo recorde.
    /// </summary>
    private void atualizarTextoTempoSobrevivido() {
        var tempoSobrevivido = calcularTempoSobrevivido(Time.timeSinceLevelLoad);
        textoTempoSobrevivido.text = $"Você sobreviveu por {tempoSobrevivido}";
        verificarNovoRecorde();
    }

    /// <summary>
    /// Calcula o tempo desde o carregamento do level, divide por 60 e obtêm os minutos
    /// Divide por 60 e pega apenas o resto da divisão para considerar como segundos.
    /// </summary>
    /// <param name="tempo"></param>
    /// <returns>String no formato MM e ss -> 14m e 53s.</returns>
    private string calcularTempoSobrevivido(float tempo) {
        var minutos = (int) tempo / 60;
        var segundos = (int) tempo % 60;

        return $"{minutos}m e {segundos}s.";
    }

    /// <summary>
    /// Verifica se um novo recorde foi estabelecido. 
    /// Chama método de atualizar texto com recorde anterior.
    /// </summary>
    void verificarNovoRecorde() {
        //Conta quanto tempo desde o load da scene.
        var tempoSobrevivido = (int) Time.timeSinceLevelLoad;

        if( tempoSobrevivido > tempoRecorde) {
            guardarTempoRecorde(tempoSobrevivido);
        }
        atualizarTextRecorde();
    }

    /// <summary>
    /// Guarda o tempo recorde do jogador.
    /// Persiste a informação, semelhante ao localstorage, independente da plataforma.
    /// </summary>
    /// <param name="tempoSobrevivido"></param>
    private void guardarTempoRecorde(float tempoSobrevivido) {
        PlayerPrefs.SetFloat("tempoRecorde", tempoSobrevivido);
    }

    /// <summary>
    /// Recupera e retorna o valor do tempo recorde.
    /// </summary>
    /// <returns>float com tempo</returns>
    private float recuperarTempoRecorde() {
        return PlayerPrefs.GetFloat("tempoRecorde", tempoRecorde);
    }

    /// <summary>
    /// Atualiza o texto com o tempo recorde.
    /// </summary>
    private void atualizarTextRecorde() {
        textoTempoRecorde.text = $"Recorde anterior {calcularTempoSobrevivido(recuperarTempoRecorde())}";
    }

    /// <summary>
    /// Carrega a scene no index 0 da build
    /// </summary>
    public void reiniciarScene() {
        SceneManager.LoadScene(0);
    }
}
