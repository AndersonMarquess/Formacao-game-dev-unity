using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject jogador;
    public Vector3 compensarDistancia;

    /// <summary>
    /// Ao iniciar procura o gameobject com nome jogador para ajustar a posição da câmera,
    /// com base no valor da variável compensarDistancia.
    /// </summary>
    void Start() {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        compensarDistancia = new Vector3(0, 15, -10);
    }

    void Update() {
        atualizarPosicao();
        olharPlayer();
    }

    /// <summary>
    /// Coloca a câmera na posição do jogador mais a distância de compensação, para que a câmera não entre dentro do jogador.
    /// </summary>
    private void atualizarPosicao() {
        transform.position = jogador.transform.position + compensarDistancia;
    }

    /// <summary>
    /// Mantem a câmera olhando fixamente para o jogador.
    /// </summary>
    private void olharPlayer() {
        transform.LookAt(jogador.transform);
    }
}
