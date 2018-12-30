using UnityEngine;

public class BalaController : MonoBehaviour {

    public float velocidadeDisparo = 20;

    void FixedUpdate() {
        var rb = transform.GetComponent<Rigidbody>();

        //transform.forward é referente ao eixo Z
        rb.MovePosition(rb.position + transform.forward * velocidadeDisparo * Time.deltaTime);
    }
}
