using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speed = 5.0f;

    private Vector3 _endPosition;
    void Start()
    {
        _endPosition = new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _endPosition) < 0.001f)
        {
            _endPosition *= -1.0f;
        }
    }
}
