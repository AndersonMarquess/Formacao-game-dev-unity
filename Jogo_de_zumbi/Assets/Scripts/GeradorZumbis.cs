using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject zumbi;
    public float tempoRespawn = 2f;
    private float contadorTempo = 0f;

    private void Update() {
        instanciarZumbi();
    }

    /// <summary>
    /// Sempre que o tempo de respawn (por padrão 2 segundos) for satisfeito,
    /// um novo zumbi é instanciado.
    /// </summary>
    private void instanciarZumbi() {
        contadorTempo += Time.deltaTime;
        if(contadorTempo >= tempoRespawn) {
            contadorTempo = 0f;
            Instantiate(zumbi, transform.position, transform.rotation);
        }
    }
}
