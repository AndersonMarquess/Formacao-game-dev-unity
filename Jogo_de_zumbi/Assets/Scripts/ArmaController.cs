using UnityEngine;

public class ArmaController : MonoBehaviour {

    public GameObject bala;
    public GameObject canoArma;
    public AudioClip somTiro;

    private void Update() {
        atirar();
    }

    /// <summary>
    /// Método responsável por instanciar a bala ao pressionar o botão "Fire1".
    /// Em conjunto ativa o som de tiro.
    /// </summary>
    private void atirar() {
        var btnEsquerdo = Input.GetButtonDown("Fire1");

        if(btnEsquerdo) {
            AudioController.audioSourceGeral.PlayOneShot(somTiro);
            Instantiate(bala, canoArma.transform.position, canoArma.transform.rotation);
        }
    }
}
