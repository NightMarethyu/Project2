using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public List<Transform> goals;
    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentGoal;
    public float goalThreshold = 0.5f;
    public float minPauseTime = 0.5f;
    public float maxPauseTime = 3f;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetNewGoal();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (!isWaiting && !agent.pathPending && agent.remainingDistance <= goalThreshold)
        {
            StartCoroutine(PauseAtGoal());
        }
    }

    private IEnumerator PauseAtGoal()
    {
        isWaiting = true;

        float waitTime = Random.Range(minPauseTime, maxPauseTime);
        yield return new WaitForSeconds(waitTime);

        SetNewGoal();
        isWaiting = false;
    }

    private void SetNewGoal()
    {
        currentGoal = goals[Random.Range(0, goals.Count)];
        agent.destination = currentGoal.position;
    }

}
