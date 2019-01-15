using UnityEngine;

public class InimigoController : MonoBehaviour, IMatavel {

    private GameObject alvo;
    public float distanciaParada = 2.4f;
    public float distanciaDeVisao = 15f;
    private MovimentacaoPersonagemController _movimentacaoController;
    private AnimacaoPersonagemController _animacaoPersonagemController;
    private Status _status;
    public AudioClip somMorte;
    private Vector3 _posicaoAleatoria;
    private float _contadorVagar;
    private float _tempoAteNovaPosicaoAleatoria = 4f;


    private void Start() {
        _movimentacaoController = GetComponent<MovimentacaoPersonagemController>();
        _animacaoPersonagemController = GetComponent<AnimacaoPersonagemController>();
        _status = GetComponent<Status>();
        alvo = GameObject.FindGameObjectWithTag(Tags.Jogador);

        int posicaoSkin = Random.Range(1, 28);//Vai de 1 até 27
        escolherSkin(posicaoSkin);
    }

    private void FixedUpdate() {
        //Se o alvo for visível e estiver mais distante que a parada = seguir
        var isAlvoVisivel = distanciaDeVisao >= distanciaAteOAlvo(alvo.transform.position);
        var isAlvoLonge = distanciaAteOAlvo(alvo.transform.position) > distanciaParada;

        if(isAlvoVisivel && isAlvoLonge) {
            seguirAlvo();
        } else if(!isAlvoVisivel) {
            vagar();
        }

        _movimentacaoController.rotacionarPersonagem(direcaoAteOAlvo());
        _animacaoPersonagemController.tocarAnimAtacar(!isAlvoLonge);
    }

    /// <summary>
    /// Deixa o personagem andado de forma aleatória.
    /// </summary>
    private void vagar() {
        _contadorVagar -= Time.deltaTime;

        if(_contadorVagar <= 0) {
            _posicaoAleatoria = gerarPosicaoAleatoria();
            _contadorVagar = _tempoAteNovaPosicaoAleatoria;
        }

        var distanciaMinima = 0.2f;
        var distanciaAlvo = distanciaAteOAlvo(_posicaoAleatoria);

        //Verifica se o inimigo não chegou até o alvo
        var isAlvoLonge =  distanciaAlvo > distanciaMinima;
        if(isAlvoLonge) {
            var direcao = _posicaoAleatoria - transform.position;
            _movimentacaoController.moverPersonagem(direcao, _status.velocidade);
        }
    }

    /// <summary>
    /// Gera uma posição aleatória, com base em uma área do tamanho de uma esfera * 10;
    /// </summary>
    /// <returns></returns>
    private Vector3 gerarPosicaoAleatoria() {
        //Gera uma posição dentro de uma esfera
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
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
    /// <param name="alvo">Alvo que será comparado</param>
    /// <returns>float com a distância até o alvo</returns>
    private float distanciaAteOAlvo(Vector3 alvo) {
        return Vector3.Distance(transform.position, alvo);
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
    /// Olha dentro do prefab e pega o gameobject na posição escolhida tornando-o visível.
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
