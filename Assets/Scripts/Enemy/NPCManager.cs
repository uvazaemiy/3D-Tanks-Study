using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private float targetDistance = 4f;
    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        bool isChasing = false;

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= targetDistance)
            {
                isChasing = true;
                agent.destination = target.position;
            }
            else
                isChasing = false;
        }

        if (agent.remainingDistance < 0.5f && !agent.pathPending && !isChasing)
        {
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
                currentPointIndex = 0;
            
            // аналог - currentPointIndex = (currentPointIndex + 1) % waypoints.Length;
            
            agent.SetDestination(waypoints[currentPointIndex].position);
        }
    }
}