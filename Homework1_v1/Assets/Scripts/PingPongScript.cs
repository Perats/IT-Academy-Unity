using UnityEngine;

public class PingPongScript : MonoBehaviour
{
    public float speed = 5.0f;

    private Vector3 endPosition;
    void Start()
    {
        endPosition = new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, endPosition) < 0.001f)
        {
            endPosition *= -1.0f;
        }
    }
}
