using UnityEngine;

public class GhostChase : GhostBehavior
{
    public bool useGreedySearch = false;

    private void Update()
    {
        // L�gica de persegui��o
        if (useGreedySearch)
        {
            GreedySearch();
        }
        else
        {
            // Outra l�gica de persegui��o (por exemplo, persegui��o direta)
            
        }
    }

    private void GreedySearch()
    {
        // Verifica se o Pacman ja existe
        if (ghost.target == null)
        {
            return;
        }

        // Calcula a dist�ncia euclidiana entre o fantasma e o jogador
        float distanceToTarget = Vector2.Distance(transform.position, ghost.target.position);

        // Calcula a dire��o para o jogador (Pac-Man)
        Vector2 directionToTarget = (ghost.target.position - transform.position).normalized;

        // Define a dire��o de movimento do fantasma baseada na dire��o para o jogador
        ghost.movement.SetDirection(directionToTarget);
    }
}
