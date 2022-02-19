using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    private float shootForce = 30f;


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

        if (weaponOnRobot != null && Input.GetKeyDown(KeyCode.Space))
        {
            Fire(weaponOnRobot);
        }
    }

    void Fire(GameObject weaponOnRobot)
    {
        GameObject newFireObject = Instantiate(weaponOnRobot, robot.transform.position, robot.transform.rotation) as GameObject;
        Rigidbody rigidBody = newFireObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(rigidBody.transform.forward * shootForce, ForceMode.Impulse);
        if (weaponOnRobot.name == "Ping")
        {
            Destroy(newFireObject, 5f);
        }
        else
        {
            Destroy(newFireObject, 1f);
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
