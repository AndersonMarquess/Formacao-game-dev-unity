using UnityEngine;
/// <summary>
/// Especialização da classe MovimentacaoPersonagemController, para o jogador.
/// </summary>
public class MovimentacaoJogador : MovimentacaoPersonagemController {
    
    /// <summary>
    /// Rotaciona o jogador para direção do ponteiro do mouse
    /// </summary>
    public void rotacionarComMouse(LayerMask mascaraChao) {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Mostra a origem e direção do raio, com tamanho multiplicado por 100.
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //Ao encostar no chão
        RaycastHit impacto;

        //Verifica se encostou no chão, apenas na layer escolhida.
        if(Physics.Raycast(raio, out impacto, 100, mascaraChao)) {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            //Mantem o jogador e a mira com ponto de impacto na mesma altura.
            posicaoMiraJogador.y = transform.position.y;

            base.rotacionarPersonagem(posicaoMiraJogador);
        }
    }

    /// <summary>
    /// Movimenta o personagem com base no input recebido
    /// </summary>
    public Vector3 movimentacao(int velocidade) {
        var horizontal = Input.GetAxis("Horizontal");//X
        var vertical = Input.GetAxis("Vertical");//Z

        Vector3 direcao = new Vector3(horizontal, 0, vertical);

        base.moverPersonagem(direcao, velocidade);

        //Verifica se o personagem está parado na posição zero com o vector 3
        return direcao;
        //tocarAnimacaoAndar(andando);
    }
}
