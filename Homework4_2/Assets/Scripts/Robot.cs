using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float shootForce;


    public GameObject bulletPlace;
    public GameObject granadePlace;
    public GameObject plantPlace;
    public GameObject pingPongPlace;
    public GameObject bullet;
    public GameObject granade;
    public GameObject pingPong;
    private GameObject weaponOnRobot;

    private Rigidbody robot;

    // Start is called before the first frame update
    void Start()
    {
        robot = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveRobot();

        if (weaponOnRobot != null && Input.GetKeyDown("space"))
        {
            Fire(weaponOnRobot);
        }
    }

    void Fire(GameObject weaponOnRobot)
    {
        if (weaponOnRobot != null)
        {
            GameObject newFireObject = Instantiate(weaponOnRobot, robot.transform.position, robot.transform.rotation) as GameObject;
            Rigidbody rigidBody = newFireObject.GetComponent<Rigidbody>();
            rigidBody.AddForce(rigidBody.transform.forward * shootForce, ForceMode.Impulse);
            if (weaponOnRobot == pingPong) Destroy(rigidBody, 6f);
            else Destroy(rigidBody, 1f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(bulletPlace.gameObject))
        {
            changeWeaponForPlace(bullet);
        }
        else if (collision.gameObject.Equals(granadePlace.gameObject))
        {
            changeWeaponForPlace(granade);
        }
        else if (collision.gameObject.Equals(pingPongPlace.gameObject))
        {
            changeWeaponForPlace(pingPong);
        }
        else
        if (collision.gameObject.Equals(plantPlace.gameObject))
        {
            weaponOnRobot = null;
        }
    }

    void changeWeaponForPlace(GameObject weapon)
    {
        Destroy(weaponOnRobot);
        weaponOnRobot = weapon;
    }

    void moveRobot()
    {
        float sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        if (sideForce != 0.0f)
        {
            robot.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }
        float forwardForce = Input.GetAxis("Vertical") * movementSpeed;
        if (forwardForce != 0.0f)
        {
            robot.velocity = transform.forward * forwardForce;
        }
    }
}
