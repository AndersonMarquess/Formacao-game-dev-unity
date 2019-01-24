using UnityEngine;

public class AnimacaoPersonagemController : MonoBehaviour {

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void tocarAnimAtacar(bool estado) {
        _animator.SetBool("Atacando", estado);
    }

    public void tocarAnimAndar(float velocidadeMovimento) {
        _animator.SetFloat("Andando", velocidadeMovimento);
    }

    public void tocarAnimMorrer() {
        _animator.SetTrigger("Morrer");
    }
}
