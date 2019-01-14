using UnityEngine;

public class BalaController : MonoBehaviour {

    public float velocidadeDisparo = 20;
    private Rigidbody rb;

    private void Start() {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        //transform.forward é referente ao eixo Z
        rb.MovePosition(rb.position + transform.forward * velocidadeDisparo * Time.deltaTime);
    }

    /// <summary>
    /// Ao colidir com outro objeto a bala é destruída, se o outro objeto for um inimigo ele também é destruído.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Inimigo")) {
            other.GetComponent<InimigoController>().sofrerDano(1);
        }

        Destroy(gameObject);
    }
}
