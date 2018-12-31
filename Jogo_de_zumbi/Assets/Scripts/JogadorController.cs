using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorController : MonoBehaviour {

    public int velocidade = 10;
    public LayerMask mascaraChao;
    public GameObject txtPerdeu;
    public bool isJogadorVivo = true;
    private Rigidbody rb;
    private Animator animator;

    /// <summary>
    /// Executa quando o script está sendo carregado e atribui a tag Jogador ao gameobject
    /// </summary>
    private void Awake() {
        transform.tag = "Jogador";
        Time.timeScale = 1;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        movimentacao();
        rotacionarComMouse();
    }

    private void Update() {
        if(isJogadorVivo == false) {
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
    /// Rotaciona o jogador para direção do ponteiro do mouse
    /// </summary>
    private void rotacionarComMouse() {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Mostra a origem e direção do raio, com tamanho multiplicado por 100.
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //Ao encostar no chão
        RaycastHit impacto;

        //Verifica se encostou no chão, apenas na layer escolhida.
        if(Physics.Raycast(raio, out impacto, 100, mascaraChao)) {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            //Mantem o jogador e a mira com ponto de impacto na mesma altura.
            posicaoMiraJogador.y = transform.position.y;

            atualizaRotacao(posicaoMiraJogador);
        }
    }

    /// <summary>
    /// Atualiza a rotação do jogador
    /// </summary>
    /// <param name="posicaoParaRotacionar"></param>
    private void atualizaRotacao(Vector3 posicaoParaRotacionar) {
        Quaternion novaRotacao = Quaternion.LookRotation(posicaoParaRotacionar);
        rb.MoveRotation(novaRotacao);
    }

    /// <summary>
    /// Movimenta o personagem com base no input recebido
    /// </summary>
    private void movimentacao() {
        var horizontal = Input.GetAxis("Horizontal");//X
        var vertical = Input.GetAxis("Vertical");//Z

        Vector3 direcao = new Vector3(horizontal, 0, vertical);

        rb.MovePosition(rb.position +(direcao * velocidade * Time.deltaTime));

        //Verifica se o personagem está parado na posição zero com o vector 3
        var andando = direcao != Vector3.zero;
        tocarAnimacaoAndar(andando);
    }

    private void tocarAnimacaoAndar(bool resultado) {
        animator.SetBool("Andando", resultado);
    }
}
