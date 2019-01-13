using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource audioSource;
    public static AudioSource audioSourceGeral;

    void Awake() {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSourceGeral = audioSource;
    }
}
