using UnityEngine;

public class InimigoController : MonoBehaviour {

    private GameObject alvo;
    private Rigidbody rb;
    public float velocidadeMovimentacao = 3;
    public float distanciaParada = 2.3f;
    private Animator animator;
    private MovimentacaoPersonagemController _movimentacaoController;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _movimentacaoController = GetComponent<MovimentacaoPersonagemController>();
        alvo = GameObject.FindGameObjectWithTag("Jogador");

        int posicaoSkin = Random.Range(1, 28);//Vai de 1 até 27
        escolherSkin(posicaoSkin);
    }

    private void FixedUpdate() {
        _movimentacaoController.rotacionarPersonagem(direcaoAteOAlvo());

        var isAlvoLonge = distanciaDoAlvo() > distanciaParada;
        animator.SetBool("Atacando", !isAlvoLonge);

        if(isAlvoLonge) {
            seguirAlvo();
        }
    }

    /// <summary>
    /// Move o personagem até a direção do alvo.
    /// </summary>
    private void seguirAlvo() {
        _movimentacaoController.moverPersonagem(direcaoAteOAlvo(), velocidadeMovimentacao);
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
}
