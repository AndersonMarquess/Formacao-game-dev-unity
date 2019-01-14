using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorController : MonoBehaviour, IMatavel {

    public LayerMask mascaraChao;
    public GameObject txtPerdeu;
    public UIController uIController;
    public AudioClip somDeDano;
    private MovimentacaoJogador _movimentacaoJogador;
    private AnimacaoPersonagemController _animacaoPersonagemController;
    public Status status;

    /// <summary>
    /// Executa quando o script está sendo carregado e atribui a tag "Jogador" ao gameobject.
    /// </summary>
    private void Awake() {
        transform.tag = "Jogador";
        Time.timeScale = 1;
    }

    private void Start() {
        _movimentacaoJogador = GetComponent<MovimentacaoJogador>();
        _animacaoPersonagemController = GetComponent<AnimacaoPersonagemController>();
        status = GetComponent<Status>();
    }

    private void FixedUpdate() {
        var andando = _movimentacaoJogador.movimentacao(status.velocidade);
        _animacaoPersonagemController.tocarAnimAndar(andando.magnitude);

        _movimentacaoJogador.rotacionarComMouse(mascaraChao);
    }

    private void Update() {
        if(status.vidaAtual <= 0) {
            if(Input.GetButtonDown("Fire1")) {
                reiniciarScene();
            }
        }
    }

    /// <summary>
    /// Carrega a scene no index 0 da build
    /// </summary>
    private void reiniciarScene() {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Subtrai um valor da vida do jogador, quando o personagem sofrer algum dano.
    /// Atualiza a barra de vida do jogador.
    /// Ativa o som de sofrer dano.
    /// Se a vida for igual ou menor que zero, o método de morrer é chamado.
    /// </summary>
    public void sofrerDano(int dano) {
        status.vidaAtual -= dano;
        uIController.atualizarBarraVidaJogador();
        AudioController.audioSourceGeral.PlayOneShot(somDeDano);

        if(status.vidaAtual <= 0) {
            morrer();
        }
    }

    /// <summary>
    /// Ativa o texto de morte e pausa o tempo do jogo.
    /// </summary>
    public void morrer() {
        txtPerdeu.SetActive(true);
        Time.timeScale = 0;
    }
}
