using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public bool isMoving { get; private set; }

    public void Initialize(Animator animator)
    {
        agent = GetComponent<NavMeshAgent>();
        this.animator = animator;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
        Continue();
    }

    public void Stop()
    {
        agent.isStopped = true;
        animator.SetBool("isWalk", false);

        isMoving = false;
    }

    public void Continue()
    {
        agent.isStopped = false;
        animator.SetBool("isWalk", true);

        isMoving = true;
    }

    public void Reset()
    {
        agent.ResetPath();
        animator.SetBool("isWalk", false);

        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            if (!(agent.pathPending || agent.remainingDistance > agent.stoppingDistance))
            {
                Stop();
            }
        }
    }
}
