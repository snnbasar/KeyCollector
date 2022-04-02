using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Nothing,
    Petrolling,
    Kill
}
public class AIController : MonoBehaviour
{

    private AIState aiState;

    public GameObject patrolPoints;
    public List<Transform> checkPoints = new List<Transform>();

    private int currentPointIndex;

    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.enabled = false;
        for (int i = 0; i < patrolPoints.transform.childCount; i++)
        {
            checkPoints.Add(patrolPoints.transform.GetChild(i));
        }
        GameManager.instance.OnStartGame += StartAgent;
        GameManager.instance.OnPlayerDied += StopAgent;
        GameManager.instance.OnMissionCompleted += StopAgent;

    }

    // Update is called once per frame
    void Update()
    {
        switch (aiState)
        {
            case AIState.Nothing:

                break;
            case AIState.Petrolling:

                DestinationReachedCheck();
                break;
            case AIState.Kill:

                KillPlayer();
                break;
            default:
                break;
        }
    }

    public void ChangeAIStateToKillPlayer()
    {
        aiState = AIState.Kill;
    }
    private void KillPlayer()
    {
        //agent.SetDestination(KarakterKontrol.instance.transform.position);
        //KarakterKontrol.instance.Die();
        GameManager.instance.KillPlayer();
        aiState = AIState.Petrolling;
    }

    private void AIPatrolTo(int index)
    {
        agent.SetDestination(checkPoints[index].position);

    }

    private void GoNextDestination()
    {
        if (currentPointIndex < checkPoints.Count)
            AIPatrolTo(currentPointIndex);
        else
        {
            currentPointIndex = 0;
            AIPatrolTo(0);

        }
    }

    private void DestinationReachedCheck()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            currentPointIndex++;
            GoNextDestination();
        }
    }

    private void StartAgent()
    {
        aiState = AIState.Petrolling;
        anim.enabled = true;
        AIPatrolTo(0);
    }

    private void StopAgent()
    {
        aiState = AIState.Nothing;
        anim.enabled = false;
        agent.isStopped = true;
    }
}
