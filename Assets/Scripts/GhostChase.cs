using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{

    public Ghost reference;
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        OpcoesGhosts(other);
    }


    public void OpcoesGhosts(Collider2D other)
    {
        switch (ghost.name)
        {
            case "Ghost_Blinky":
                GulosaBlinky(other);
                break;
            case "Ghost_Pinky":
                MovimentaPinky(other);
                break;
            case "Ghost_Inky":
                MovimentaInky(other);
                break;
            case "Ghost_Clyde":
                MovimentaClyde(other);
                break;
        }
    }

    private void GulosaBlinky(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Find the available direction that moves closet to pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is less than the current
                // min distance then this direction becomes the new closest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }


    private void Heuristica(Collider2D other, Func<Vector3, Vector3, Vector3> calculateTargetPosition)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;
            List<Vector2> availableDirections = new List<Vector2>(node.availableDirections);
            availableDirections.Remove(ghost.movement.direction * -1);

            foreach (Vector2 availableDirection in availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                Vector3 targetPosition = calculateTargetPosition(newPosition, ghost.target.position);

                float distance = (targetPosition - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void MovimentaPinky(Collider2D other)
    {
        Heuristica(other, (newPosition, targetPosition) => {
            return targetPosition + new Vector3(ghost.target.GetComponent<Movement>().direction.x * 6f, ghost.target.GetComponent<Movement>().direction.y * 6f, 0f);
        });
    }

    private void MovimentaInky(Collider2D other)
    {
        Heuristica(other, (newPosition, targetPosition) => {
            float xDistance = targetPosition.x - reference.transform.position.x;
            float yDistance = targetPosition.y - reference.transform.position.y;
            return new Vector3(targetPosition.x + xDistance, targetPosition.y + yDistance, 0f);
        });
    }

    private void MovimentaClyde(Collider2D other)
    {
        Heuristica(other, (newPosition, targetPosition) => {
            if (Vector3.Distance(targetPosition, newPosition) < 8f)
            {
                return ghost.home.transform.position;
            }
            else
            {
                return targetPosition;
            }
        });
    }


}