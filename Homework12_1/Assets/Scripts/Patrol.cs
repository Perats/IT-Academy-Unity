using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _patrolPoints;
    NavMeshAgent _navMeshAgent;
    private bool _isPatrol;
    private static int _currentPatrolPoint;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_navMeshAgent.remainingDistance <= 0.5f)
        {
            _isPatrol = false;
            SetDestination();
        }
    }

    private void SetDestination() 
    {
        _currentPatrolPoint = Random.Range(0, _patrolPoints.Count);
        Vector3 target = _patrolPoints[_currentPatrolPoint].transform.position;
        _navMeshAgent.SetDestination(target);
        _isPatrol = true;
    }
}
