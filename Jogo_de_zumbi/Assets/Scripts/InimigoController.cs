using UnityEngine;

public class InimigoController : MonoBehaviour {

    private GameObject alvo;
    private Rigidbody rb;
    public float velocidadeMovimentacao = 3;
    public float distanciaParada = 2.3f;
    private Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        alvo = GameObject.FindGameObjectWithTag("Jogador");

        int posicaoSkin = Random.Range(1, 28);//Vai de 1 até 27
        escolherSkin(posicaoSkin);
    }

    private void FixedUpdate() {
        rotacionarInimigo(direcaoAteOAlvo());

        var isAlvoLonge = distanciaDoAlvo() > distanciaParada;
        animator.SetBool("Atacando", !isAlvoLonge);

        if(isAlvoLonge) {
            seguirAlvo();
        }
    }

    /// <summary>
    /// Adiciona força ao rigidbody para mover-se até o alvo.
    /// </summary>
    private void seguirAlvo() {
        rb.MovePosition(rb.position + direcaoAteOAlvo().normalized * velocidadeMovimentacao * Time.deltaTime);

        rotacionarInimigo(direcaoAteOAlvo());
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
    /// Rotaciona o inimigo na direção do player
    /// </summary>
    /// <param name="direcao"></param>
    private void rotacionarInimigo(Vector3 direcao) {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        rb.MoveRotation(rotacao);
    }

    /// <summary>
    /// Este método é chamado por meio de um evento na animação do inimigo.
    /// Habilita o gameobject de informação presente no player e pausa o jogo.
    /// </summary>
    private void atacouJogador() {
        var jogadorController = alvo.GetComponent<JogadorController>();
        jogadorController.sofrerDano();
    }

    /// <summary>
    /// Olha dentro do prefab e pega o gameobject na posição escolhida tornando visível.
    /// </summary>
    private void escolherSkin(int posicaoSkin) {
       transform.GetChild(posicaoSkin).gameObject.SetActive(true);
    }
}
