using UnityEngine;

public class JogadorController : MonoBehaviour, IMatavel, ICuravel {

    public LayerMask mascaraChao;
    public UIController uIController;
    public AudioClip somDeDano;
    private MovimentacaoJogador _movimentacaoJogador;
    private AnimacaoPersonagemController _animacaoPersonagemController;
    public Status status;

    /// <summary>
    /// Executa quando o script está sendo carregado e atribui a tag "Jogador" ao gameobject.
    /// </summary>
    private void Awake() {
        transform.tag = Tags.Jogador;
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
    /// Chama o método de informações do gameover.
    /// </summary>
    public void morrer() {
        uIController.mostrarPainelGameOver();
    }

    /// <summary>
    /// Recupera a vida do player.
    /// Não permite ultrapassar o valor da vida inicial.
    /// </summary>
    /// <param name="qtdDeCura"></param>
    public void curarVida(int qtdDeCura) {
        status.vidaAtual += qtdDeCura;

        if(status.vidaAtual > status.vidaTotal) {
            status.vidaAtual = status.vidaTotal;
        }

        uIController.atualizarBarraVidaJogador();
    }
}
