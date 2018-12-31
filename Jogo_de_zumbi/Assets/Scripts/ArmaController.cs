using UnityEngine;

public class ArmaController : MonoBehaviour {

    public GameObject bala;
    public GameObject canoArma;

    private void Update() {
        atirar();
    }

    private void atirar() {
        var btnEsquerdo = Input.GetButtonDown("Fire1");

        if(btnEsquerdo) {
            Instantiate(bala, canoArma.transform.position, canoArma.transform.rotation);
        }
    }
}
