using UnityEngine;

public class EagleAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public Transform player;
    public float chaseDistance = 5f;
    public float chaseSpeed = 4f;

    private int currentPatrol = 0;
    private bool chasing = false;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseDistance)
        {
            chasing = true;
        }
        else if (distance > chaseDistance * 1.5f)
        {
            chasing = false;
        }

        if (chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentPatrol];
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPatrol = (currentPatrol + 1) % patrolPoints.Length;
        }
    }
}