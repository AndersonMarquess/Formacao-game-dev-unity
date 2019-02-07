using UnityEngine;
using UnityEngine.AI;

public class ChefeController : MonoBehaviour, IMatavel {

    private Transform _jogador;
    private NavMeshAgent _agent;
    private Status _status;
    private AnimacaoPersonagemController _animPersonagem;
    private MovimentacaoPersonagemController _moviPersonagem;
    public GameObject kitMedico;

    private void Start() {
        transform.tag = Tags.Chefe;

        _jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<Status>();
        _animPersonagem = GetComponent<AnimacaoPersonagemController>();
        _moviPersonagem = GetComponent<MovimentacaoPersonagemController>();
        configurarNavMesh();
    }

    /// <summary>
    /// Configura os valores da navmesh.
    /// </summary>
    private void configurarNavMesh() {
        _agent.speed = _status.velocidade;
    }

    private void Update() {
        definirDestino();
        //Se o agente já possui um caminho.
        if(_agent.hasPath) {
            ajusteAnimacao();
        }
    }

    /// <summary>
    /// Chama as animações do personagem.
    /// </summary>
    private void ajusteAnimacao() {
        var isJogadorPerto = _agent.remainingDistance <= _agent.stoppingDistance;
        _animPersonagem.tocarAnimAtacar(isJogadorPerto);

        if(isJogadorPerto) {
            Vector3 direcao = _jogador.position - transform.position;
            _moviPersonagem.rotacionarPersonagem(direcao);
        }
    }

    private void atacouJogador() {
        causarDanoAoJogador();
    }

    private void causarDanoAoJogador() {
        int dano = Random.Range(30, 41);
        _jogador.GetComponent<JogadorController>().sofrerDano(dano);
    }

    /// <summary>
    /// Faz o chefão seguir a posição do jogador.
    /// </summary>
    private void definirDestino() {
        _agent.SetDestination(_jogador.position);
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
    /// Toca animação de morrer e desativas os scripts dependentes do chefão.
    /// </summary>
    public void morrer() {
        _animPersonagem.tocarAnimMorrer();
        _moviPersonagem.morrer();
        Instantiate(kitMedico, transform.position, Quaternion.identity);
        this.enabled = false;
        _agent.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
