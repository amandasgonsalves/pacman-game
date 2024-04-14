using UnityEngine;

public class GhostChase : GhostBehavior
{
    public bool useGreedySearch = false;

    private void Update()
    {
        // Lógica de perseguição
        if (useGreedySearch)
        {
            GreedySearch();
        }
        else
        {
            // Outra lógica de perseguição (por exemplo, perseguição direta)
            
        }
    }

    private void GreedySearch()
    {
        // Verifica se o Pacman ja existe
        if (ghost.target == null)
        {
            return;
        }

        // Calcula a distância euclidiana entre o fantasma e o jogador
        float distanceToTarget = Vector2.Distance(transform.position, ghost.target.position);

        // Calcula a direção para o jogador (Pac-Man)
        Vector2 directionToTarget = (ghost.target.position - transform.position).normalized;

        // Define a direção de movimento do fantasma baseada na direção para o jogador
        ghost.movement.SetDirection(directionToTarget);
    }
}
