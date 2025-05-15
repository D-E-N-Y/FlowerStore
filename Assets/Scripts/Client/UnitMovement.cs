using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public void Initialize(Animator animator)
    {
        agent = GetComponent<NavMeshAgent>();
        this.animator = animator;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
        animator.SetBool("isWalk", true);
    }

    public void Stop()
    {
        agent.isStopped = true;
        animator.SetBool("isWalk", false);
    }

    public void Continue()
    {
        agent.isStopped = false;
        animator.SetBool("isWalk", true);
    }

    public void Reset()
    {
        agent.ResetPath();
        animator.SetBool("isWalk", true);
    }
}
