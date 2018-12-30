using UnityEngine;

public class BalaController : MonoBehaviour {

    public float velocidadeDisparo = 20;

    void FixedUpdate() {
        var rb = transform.GetComponent<Rigidbody>();

        //transform.forward é referente ao eixo Z
        rb.MovePosition(rb.position + transform.forward * velocidadeDisparo * Time.deltaTime);
    }

    /// <summary>
    /// Ao colidir com outro objeto a bala é destruída, se o outro objeto for um inimigo ele também é destruído.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Inimigo")
            Destroy(other.gameObject);

        Destroy(gameObject);
    }
}
