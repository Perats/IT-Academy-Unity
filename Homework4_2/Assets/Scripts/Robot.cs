using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private float _movementSpeed;
    private float _rotationSpeed;
    public float shootForce = 30f;


    public GameObject bulletPlace;
    public GameObject granadePlace;
    public GameObject plantPlace;
    public GameObject pingPongPlace;
    public GameObject bullet;
    public GameObject granade;
    public GameObject pingPong;
    public GameObject Rocket;
    private GameObject weaponOnRobot;
    public GameObject Rifle;
    public ParticleSystem Flash;
    private Camera _mainCamera;
    public GameObject metalDecalPrefab;

    private Rigidbody robot;

  
    void Start()
    {
        _mainCamera = Camera.main;
        robot = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveRobot();
        if (weaponOnRobot != null && Input.GetMouseButtonDown(0))
        {
            Fire(weaponOnRobot);
        }
    }

    void Fire(GameObject weaponOnRobot)
    {

       if (weaponOnRobot.name == "Bullet")
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                BulletsMovement bullet = BulletsManager.Instance.GetPoolObject("Bullet",this.transform.position, this.transform.rotation);
                if (bullet != null)
                {
                    AudioManager.Instance.PlayClip("Bullet", this.transform.position);//вызов манагера звука
                    bullet.gameObject.SetActive(true);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    bullet.transform.position = hit.point;
                    bullet.transform.rotation = Quaternion.identity;
                  // rb.AddForce(rb.transform.forward * shootForce, ForceMode.Impulse);
                    Flash.Play();
                    SpawnDecal(hit);
                }
            }
        }
        else
        if (weaponOnRobot.name == "Rocket")
        {
            BulletsMovement granade = BulletsManager.Instance.GetPoolObject("Rocket", this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
            if (granade != null)
            {
                granade.gameObject.SetActive(true);
                Rigidbody rigidBody = granade.GetComponent<Rigidbody>();
                rigidBody.AddForce(rigidBody.transform.forward * shootForce, ForceMode.Impulse);
                Flash.Play();
                AudioManager.Instance.PlayClip("Rocket", this.transform.position);//вызов манагера звука

            }
        }
        else if (weaponOnRobot.name == "Granade")
        {
            BulletsMovement granade = BulletsManager.Instance.GetPoolObject("Granade", this.transform.position, this.transform.rotation);
            if (granade != null)
            {
                granade.gameObject.SetActive(true);
                Rigidbody rigidBody = granade.GetComponent<Rigidbody>();
                rigidBody.AddForce(rigidBody.transform.forward * shootForce, ForceMode.Impulse);
                Flash.Play();
                AudioManager.Instance.PlayClip("Granade", this.transform.position);//вызов манагера звука
            
            }
        }
    }

    private void SpawnDecal(RaycastHit hit)
    {
        GameObject spawndecal = Instantiate(metalDecalPrefab, hit.point, Quaternion.identity);
        spawndecal.transform.SetParent(hit.collider.transform);
        spawndecal.transform.localScale = new Vector3(0.2f, 0.1f, 0.2f);
        spawndecal.transform.eulerAngles = new Vector3(0,0,90);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(bulletPlace.gameObject))
        {
            changeWeaponForPlace(bullet);
            Rifle.SetActive(true);
        }
        else if (collision.gameObject.Equals(granadePlace.gameObject))
        {
            changeWeaponForPlace(granade);
            Rifle.SetActive(false);
        }
        else if (collision.gameObject.Equals(pingPongPlace.gameObject))
        {
            changeWeaponForPlace(Rocket);
            Rifle.SetActive(false);
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
        float sideForce = Input.GetAxis("Horizontal") * _rotationSpeed;
        if (sideForce != 0.0f)
        {
            robot.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }
        float forwardForce = Input.GetAxis("Vertical") * _movementSpeed;
        if (forwardForce != 0.0f)
        {
            robot.velocity = transform.forward * forwardForce;
        }
    }
}