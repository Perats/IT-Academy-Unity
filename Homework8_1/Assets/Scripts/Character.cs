using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public float speed = 10f;
    Rigidbody2D rb;
    Animator animator;
    bool directionRight = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = animator ?? GetComponent<Animator>();
    }

    void Update()
    {
        Run();
    }
    void Run() {
        var horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal * speed));
        if (horizontal > 0f && !directionRight)
        {
            Rotate();
        }
        else if (horizontal < 0f && directionRight)
        {
            Rotate();
        }
    }

    void Rotate() {
        directionRight = !directionRight;
        Vector3 currentDirection = transform.localScale;
        currentDirection.x *= -1;
        transform.localScale = currentDirection;
    }
}
