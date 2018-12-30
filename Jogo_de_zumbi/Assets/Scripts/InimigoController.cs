using UnityEngine;

public class InimigoController : MonoBehaviour {

    private GameObject alvo;
    private Rigidbody rb;
    public float velocidadeMovimentacao = 3;
    public float distanciaParada = 2.3f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        alvo = GameObject.FindGameObjectWithTag("Jogador");
    }

    void FixedUpdate() {
        rotacionarInimigo(direcaoAteOAlvo());

        var isAlvoLonge = distanciaDoAlvo() > distanciaParada;
        GetComponent<Animator>().SetBool("Atacando", !isAlvoLonge);

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
        jogadorController.isJogadorVivo = false;
        jogadorController.txtPerdeu.SetActive(true);

        Time.timeScale = 0;
    }
}
