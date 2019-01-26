using UnityEngine;
using UnityEngine.AI;

public class ChefeController : MonoBehaviour {

    private Transform _jogador;
    private NavMeshAgent _agent;

    private void Start() {
        _jogador = GameObject.FindWithTag(Tags.Jogador).transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        definirDestino();
    }

    /// <summary>
    /// Faz o chefão seguir a posição do jogador.
    /// </summary>
    private void definirDestino() {
        _agent.SetDestination(_jogador.position);
    }
}
