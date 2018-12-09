using UnityEngine;

public class BalaController : MonoBehaviour {

    public float velocidadeDisparo = 20;

    void FixedUpdate() {
        var rb = transform.GetComponent<Rigidbody>();

        //transform.forward é relativo ao eixo Z do componente
        rb.MovePosition(rb.position + transform.forward * velocidadeDisparo * Time.deltaTime);
    }


}
