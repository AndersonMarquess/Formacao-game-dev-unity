using UnityEngine;

public class JogadorController : MonoBehaviour {

    public int velocidade = 10;

    /// <summary>
    /// Executa quando o script está sendo carregado e atribui a tag Jogador ao gameobject
    /// </summary>
    private void Awake() {
        transform.tag = "Jogador";
    }

    void Update() {
        movimentacao();
    }

    /// <summary>
    /// Movimenta o personagem com base no input recebido
    /// </summary>
    private void movimentacao() {
        var horizontal = Input.GetAxis("Horizontal");//X
        var vertical = Input.GetAxis("Vertical");//Z

        Vector3 direcao = new Vector3(horizontal, 0, vertical);
        transform.Translate(direcao * velocidade * Time.deltaTime);

        //Verifica se o personagem está parado na posição zero com o vector 3
        var andando = direcao != Vector3.zero;
        tocarAnimacaoAndar(andando);
    }

    private void tocarAnimacaoAndar(bool resultado) {
        transform.GetComponent<Animator>().SetBool("Andando", resultado);
    }
}
