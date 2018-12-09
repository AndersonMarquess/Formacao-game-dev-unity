using UnityEngine;

public class InimigoController : MonoBehaviour {

    private GameObject alvo;
    public float velocidadeMovimentacao = 3;
    public float distanciaParada = 2.3f;

    void Start() {
        alvo = GameObject.FindGameObjectWithTag("Jogador");
    }

    void FixedUpdate() {
        if(distanciaDoAlvo() > distanciaParada) {
            seguirAlvo();
        }
    }

    /// <summary>
    /// Adiciona força ao rigidbody para mover-se até o alvo.
    /// </summary>
    private void seguirAlvo() {
        var direcao = alvo.transform.position - transform.position;
        var rb = GetComponent<Rigidbody>();
        rb.MovePosition(rb.position + direcao.normalized * velocidadeMovimentacao * Time.deltaTime);

        rotacionarInimigo(rb, direcao);
    }

    /// <summary>
    /// Verifica a distância entre o inimigo e o alvo, retorna o valor da distância.
    /// </summary>
    /// <returns></returns>
    private float distanciaDoAlvo() {
        return Vector3.Distance(transform.position, alvo.transform.position);
    }

    private void rotacionarInimigo(Rigidbody rb, Vector3 direcao) {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        rb.MoveRotation(rotacao);
    }
}
