using UnityEngine;

public class GeradorChefao : MonoBehaviour {

    private float _tempoParaProximoRespawn = 0;
    public float tempoEntreRespawn = 60f;
    public GameObject chefaoPrefab;
    private GameObject _jogador;
    private float _distanciaAteJogadorParaGerarZumbi = 40f;

    private void Start() {
        _tempoParaProximoRespawn = tempoEntreRespawn;
        _jogador = GameObject.FindWithTag(Tags.Jogador);
    }

    private void Update() {
        var distanciaJogador = Vector3.Distance(transform.position, _jogador.transform.position);
        if(distanciaJogador > _distanciaAteJogadorParaGerarZumbi) {
            gerarChefao();
        }
    }

    /// <summary>
    /// Instância um novo chefe a cada N segundos especificados na variável tempoEntreRespawn, 
    /// que por padrão é 60 segundos.
    /// </summary>
    private void gerarChefao() {
        if(Time.timeSinceLevelLoad > _tempoParaProximoRespawn) {
            Instantiate(chefaoPrefab, transform.position, Quaternion.identity);
            _tempoParaProximoRespawn += Time.timeSinceLevelLoad;
        }
    }
}
