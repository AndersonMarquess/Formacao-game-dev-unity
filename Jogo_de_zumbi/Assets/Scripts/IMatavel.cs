/// <summary>
/// Interface para personagens que podem morrer ao sofrer uma certa quantia de dano.
/// </summary>
public interface IMatavel {
    void sofrerDano(int dano);

    void morrer();
}
