using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    private Camera cam;
    private NavMeshAgent agent;
    [SerializeField]
    private Vector3 _cameraOfset = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
               
            }
        }
    }
    private void LateUpdate()
    {
        cam.transform.position = agent.transform.position + _cameraOfset;
    }

    private void OnTriggerEnter(Collider other)
    {
       
            if (other.CompareTag("SlowPlatform"))
            {
                agent.speed = 1;
            }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("SlowPlatform"))
        {
            agent.speed = 8;
        }
    }
}
