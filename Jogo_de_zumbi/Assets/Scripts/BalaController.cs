using UnityEngine;

public class BalaController : MonoBehaviour {

    public float velocidadeDisparo = 20;
    private Rigidbody _rb;
    private int _qtdDano = 1;

    private void Start() {
        _rb = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        //transform.forward é referente ao eixo Z
        _rb.MovePosition(_rb.position + transform.forward * velocidadeDisparo * Time.deltaTime);
    }

    /// <summary>
    /// Ao colidir com outro objeto a bala é destruída, se o outro objeto for um inimigo ele também é destruído.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(Tags.Inimigo)) {
            other.GetComponent<InimigoController>().sofrerDano(_qtdDano);
        }

        Destroy(gameObject);
    }
}
