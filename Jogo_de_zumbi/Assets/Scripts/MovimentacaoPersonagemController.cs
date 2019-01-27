using UnityEngine;

public class MovimentacaoPersonagemController : MonoBehaviour {

    private Rigidbody _rigidbody;

    /// <summary>
    /// Atribui o rigidbody antes do inicio do método Start.
    /// </summary>
    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Move o personagem para a direção especificada, com a velocidade escolhida, 
    /// adicionando força ao rigidbody.
    /// </summary>
    /// <param name="direcao"></param>
    /// <param name="velocidade"></param>
    public void moverPersonagem(Vector3 direcao, float velocidade) {
        _rigidbody.MovePosition(_rigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }

    /// <summary>
    /// Rotaciona o personagem na direção especificada.
    /// </summary>
    /// <param name="direcao"></param>
    public void rotacionarPersonagem(Vector3 direcao) {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        _rigidbody.MoveRotation(rotacao);
    }

    /// <summary>
    ///Desabilita as travas do rigitbody
    /// </summary>
    public void morrer() {
        //_rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
}
