using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public float jumpSpeed = 200.0f;
    CharacterController controller;
    private Camera camera;

    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");

        Vector3 movement = new Vector3(horizontal*speed, gravity,vertical*speed);
        if (Input.GetKeyDown(KeyCode.Space)) movement.y = jumpSpeed;
        if (movement.y > gravity) movement.y += gravity * Time.deltaTime;
        Controller.Move(transform.TransformDirection(movement) * Time.deltaTime);
        Controller.transform.Rotate(Vector3.up, rotationX);
        camera.transform.Rotate(Vector3.right, -rotationY);
    }
}