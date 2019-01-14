using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorController : MonoBehaviour {

    public int velocidade = 10;
    public LayerMask mascaraChao;
    public GameObject txtPerdeu;
    public int vida;
    public UIController uIController;
    public AudioClip somDeDano;
    private MovimentacaoJogador _movimentacaoJogador;
    private AnimacaoPersonagemController _animacaoPersonagemController;

    /// <summary>
    /// Executa quando o script está sendo carregado e atribui a tag "Jogador" ao gameobject.
    /// Atribui o valor inicial da vida do jogador.
    /// </summary>
    private void Awake() {
        transform.tag = "Jogador";
        vida = 100;
        Time.timeScale = 1;
    }

    private void Start() {
        _movimentacaoJogador = GetComponent<MovimentacaoJogador>();
        _animacaoPersonagemController = GetComponent<AnimacaoPersonagemController>();
    }

    private void FixedUpdate() {
        var andando = _movimentacaoJogador.movimentacao(velocidade);
        _animacaoPersonagemController.tocarAnimAndar(andando.magnitude);

        _movimentacaoJogador.rotacionarComMouse(mascaraChao);
    }

    private void Update() {
        if(vida <= 0) {
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
    /// Verifica se a vida é menor que zero, e informa que o jogador perdeu.
    /// </summary>
    public void sofrerDano(int dano) {
        vida -= dano;
        uIController.atualizarBarraVidaJogador();
        AudioController.audioSourceGeral.PlayOneShot(somDeDano);

        if(vida <= 0) {
            txtPerdeu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
