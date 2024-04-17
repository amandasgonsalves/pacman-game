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
        ChaseLogic(other);
    }

    private void ChaseLogic(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;
            List<Vector2> availableDirections = new List<Vector2>(node.availableDirections);
            availableDirections.Remove(ghost.movement.direction * -1);

            switch (ghost.name)
            {
                case "Ghost_Blinky":
                    direction = ChaseDefault(other);
                    break;
                case "Ghost_Pinky":
                    direction = ChaseFront(other);
                    break;
                case "Ghost_Inky":
                    direction = ChaseTangent(other);
                    break;
                case "Ghost_Clyde":
                    direction = ChaseRadius(other);
                    break;
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private Vector2 ChaseFront(Collider2D other)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        Node node = other.GetComponent<Node>();

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection != -ghost.movement.direction)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                Vector3 frontOfTarget = ghost.target.position + new Vector3(ghost.target.GetComponent<Movement>().direction.x * 6f, ghost.target.GetComponent<Movement>().direction.y * 6f, 0f);
                float distance = (frontOfTarget - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }
        }

        return direction;
    }

    private Vector2 ChaseTangent(Collider2D other)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        Node node = other.GetComponent<Node>();

        if (reference != null)
        {
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                if (availableDirection != -ghost.movement.direction)
                {
                    Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                    Vector3 frontOfTarget = ghost.target.position + new Vector3(ghost.target.GetComponent<Movement>().direction.x * 6f, ghost.target.GetComponent<Movement>().direction.y * 6f, 0f);
                    float xDistance = frontOfTarget.x - reference.transform.position.x;
                    float yDistance = frontOfTarget.y - reference.transform.position.y;
                    Vector3 point = new Vector3(frontOfTarget.x + xDistance, frontOfTarget.y + yDistance, 0f);
                    float distance = (point - newPosition).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        direction = availableDirection;
                    }
                }
            }
        }

        return direction;
    }

    private Vector2 ChaseDefault(Collider2D other)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        Node node = other.GetComponent<Node>();

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection != -ghost.movement.direction)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }
        }

        return direction;
    }

    private Vector2 ChaseRadius(Collider2D other)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        Node node = other.GetComponent<Node>();

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            if (availableDirection != -ghost.movement.direction)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                float distanceToPacman = Vector3.Distance(ghost.target.position, newPosition);
                float distance = (distanceToPacman < 8f) ? (ghost.home.transform.position - newPosition).sqrMagnitude : (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    direction = availableDirection;
                }
            }
        }

        return direction;
    }
}
