using System.Collections;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject zumbi;
    public float tempoRespawn = 2f;
    private float _contadorTempo = 0f;
    public LayerMask layerZumbi;
    private float _diametroPosicaoAleatoria = 3;
    private float _distanciaAteJogadorParaGerarZumbi = 20;
    private GameObject _jogador;
    private int qtdMaximaZumbisEmCena = 3;
    private int qtdDeZumbisEmCena;


    /// <summary>
    /// Chama o método de criar zumbis no inicio do carregamento.
    /// </summary>
    private void Start() {
        _jogador = GameObject.FindWithTag(Tags.Jogador);

        for(int i = 0; i < qtdMaximaZumbisEmCena; i++) {
            StartCoroutine(gerarZumbi());
        }
    }

    private void Update() {
        var distanciaJogador = Vector3.Distance(transform.position, _jogador.transform.position);
        if(distanciaJogador > _distanciaAteJogadorParaGerarZumbi) {
            instanciarZumbi();
        }
    }

    /// <summary>
    /// Sempre que o tempo de respawn (por padrão 2 segundos) for satisfeito,
    /// um novo zumbi é instanciado.
    /// </summary>
    private void instanciarZumbi() {
        _contadorTempo += Time.deltaTime;
        var isZumbiProntoParaRespawn = _contadorTempo >= tempoRespawn;

        if(isZumbiProntoParaRespawn && isPermitidoGerarNovosZumbis()) {
            _contadorTempo = 0f;
            StartCoroutine(gerarZumbi());
        }
    }

    /// <summary>
    /// Gera um zumbi apenas se não houve um outro zumbi muito próximo.
    /// </summary>
    IEnumerator gerarZumbi() {
        var novaPosicao = gerarPosicaoAleatoria();
        var isOcupado = isPosicaoOcupadaPorZumbi(novaPosicao, 2);

        while(isOcupado) {
            novaPosicao = gerarPosicaoAleatoria();
            //Caso a posição não esteja disponível, ele retorna nulo e tenta no próximo quadro.
            yield return null;
        }

        instanciarZumbiELinkarGerador(novaPosicao);
    }

    /// <summary>
    /// Instância um zumbi e faz o link entre este gerador e o zumbi criado.
    /// Incrementa variável com a quantidade de zumbis.
    /// </summary>
    /// <param name="novaPosicao"></param>
    private void instanciarZumbiELinkarGerador(Vector3 novaPosicao) {
        InimigoController inimigoController = Instantiate(zumbi, novaPosicao, transform.rotation)
            .GetComponent<InimigoController>();
        inimigoController.meuGeradorZumbi = this;
        qtdDeZumbisEmCena++;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _diametroPosicaoAleatoria);
    }

    /// <summary>
    /// Na posição especificada cria uma esfera com diâmetro escolhido, com valor padrão de 1, 
    /// pega todos os colliders dentro do diâmetro, considerando apenas os que estiverem na layer de zumbi.
    /// </summary>
    /// <param name="novaPosicao"></param>
    private bool isPosicaoOcupadaPorZumbi(Vector3 novaPosicao, int diametro = 1) {
        Collider[] colliders = Physics.OverlapSphere(novaPosicao, diametro, layerZumbi);

        return colliders.Length > 0;
    }

    /// <summary>
    /// Gera uma posição aleatória, com base em uma área do tamanho de uma esfera * 10;
    /// </summary>
    /// <returns></returns>
    private Vector3 gerarPosicaoAleatoria() {
        //Gera uma posição dentro de uma esfera
        Vector3 posicao = Random.insideUnitSphere * _diametroPosicaoAleatoria;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    /// <summary>
    /// Retorna um boolean com positivo caso o limite máximo de zumbis em cena não tenha sido atingido.
    /// </summary>
    /// <returns></returns>
    private bool isPermitidoGerarNovosZumbis() {
        return qtdDeZumbisEmCena < qtdMaximaZumbisEmCena;
    }

    public void diminuirQtdZumbisEmCena() {
        qtdDeZumbisEmCena--;
    }
}
