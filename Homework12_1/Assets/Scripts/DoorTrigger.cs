using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private DoorController Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            if (!Door.isOpened)
            {
                Door.Open(other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            if (Door.isOpened)
            {
                Door.Close();
            }
        }
    }
}
