using UnityEngine;

public class InimigoController : MonoBehaviour, IMatavel {

    private GameObject alvo;
    public float distanciaParada = 2.4f;
    private MovimentacaoPersonagemController _movimentacaoController;
    private AnimacaoPersonagemController _animacaoPersonagemController;
    private Status _status;
    public AudioClip somMorte;

    private void Start() {
        _movimentacaoController = GetComponent<MovimentacaoPersonagemController>();
        _animacaoPersonagemController = GetComponent<AnimacaoPersonagemController>();
        _status = GetComponent<Status>();
        alvo = GameObject.FindGameObjectWithTag("Jogador");

        int posicaoSkin = Random.Range(1, 28);//Vai de 1 até 27
        escolherSkin(posicaoSkin);
    }

    private void FixedUpdate() {
        _movimentacaoController.rotacionarPersonagem(direcaoAteOAlvo());
        var isAlvoLonge = distanciaDoAlvo() > distanciaParada;
        _animacaoPersonagemController.tocarAnimAtacar(!isAlvoLonge);

        if(isAlvoLonge) {
            seguirAlvo();
        }
    }

    /// <summary>
    /// Move o personagem até a direção do alvo.
    /// </summary>
    private void seguirAlvo() {
        _movimentacaoController.moverPersonagem(direcaoAteOAlvo(), _status.velocidade);
        _movimentacaoController.rotacionarPersonagem(direcaoAteOAlvo());
    }

    /// <summary>
    /// Com base na posição do alvo é retornado o valor em Vector3 até sua posição.
    /// </summary>
    /// <returns>distância até o alvo em Vector3</returns>
    private Vector3 direcaoAteOAlvo() {
        return alvo.transform.position - transform.position;
    }

    /// <summary>
    /// Verifica a distância entre o inimigo e o alvo, retorna o valor da distância.
    /// </summary>
    /// <returns>float com a distância até o alvo</returns>
    private float distanciaDoAlvo() {
        return Vector3.Distance(transform.position, alvo.transform.position);
    }

    /// <summary>
    /// Este método é chamado por meio de um evento na animação do inimigo.
    /// Habilita o gameobject de informação presente no player e pausa o jogo.
    /// </summary>
    private void atacouJogador() {
        var jogadorController = alvo.GetComponent<JogadorController>();
        int valorDoDano = Random.Range(20, 31);
        jogadorController.sofrerDano(valorDoDano);
    }

    /// <summary>
    /// Olha dentro do prefab e pega o gameobject na posição escolhida tornando visível.
    /// </summary>
    private void escolherSkin(int posicaoSkin) {
       transform.GetChild(posicaoSkin).gameObject.SetActive(true);
    }

    /// <summary>
    /// Subtrai o valor do dano da vida atual, se o resultado for igual ou menor que zero,
    /// chama o método de morrer.
    /// </summary>
    /// <param name="dano"></param>
    public void sofrerDano(int dano) {
        _status.vidaAtual -= dano;

        if(_status.vidaAtual <= 0) {
            morrer();
        }
    }

    /// <summary>
    /// Ativa o som de morte e destrói o gameobject
    /// </summary>
    public void morrer() {
        AudioController.audioSourceGeral.PlayOneShot(somMorte);
        Destroy(gameObject);
    }
}
