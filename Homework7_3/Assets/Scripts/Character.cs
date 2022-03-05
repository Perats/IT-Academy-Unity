using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;

public class Character : MonoBehaviour
{
    public float speed = 100f;
    private float rotationAngle = 0.0f;
    public float rotationSpeed = 0.10f;
    private float gravity = -9.8f;

    public AudioMixerSnapshot firstBackground;
    public AudioMixerSnapshot secondBackground;
    public AudioSource[] clips;
    private bool currentSound = true;
    GameObject lightGameObject;

    public GameObject currentFloor;
    private GameObject previousFloor;
    public GameObject nextFloor;
    private Vector3 floorDist = new Vector3(0f, 20.66f, 0f);
    private Vector3 lightDist = new Vector3(2f, 5f, 0f);

    private CharacterController controller;
    private Camera characterCamera;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public Camera CharacterCamera { get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); } }
  
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float hotizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(hotizontal, gravity, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                                  movement.normalized;
        
        Controller.Move( rotatedMovement * speed * Time.deltaTime);
        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
        Flicker();
    }
    private void OnTriggerEnter(Collider other)
    {
        CreateFloor();
        ChangeSound();
        CreateLightsEffects();
        PlayScareSound();
    }

    private void CreateFloor()
    {
        Destroy(previousFloor);
        GameObject floor = Instantiate(nextFloor, nextFloor.transform.position + floorDist, Quaternion.identity);
        previousFloor = currentFloor;
        currentFloor = nextFloor;
        nextFloor = floor;
    }

    private void ChangeSound()
    {
        if (currentSound)
        {
            firstBackground.TransitionTo(1f);
        }
        else
        {
            secondBackground.TransitionTo(1f);
        }
        currentSound = !currentSound;
    }

    private void CreateLightsEffects()
    {
        Destroy(lightGameObject);
        lightGameObject = new GameObject("The Light");
        Light lightComp = lightGameObject.AddComponent<Light>();
        lightComp.color = Color.red;
        lightComp.intensity = 0.69f;
        lightComp.type = UnityEngine.LightType.Directional;
        lightGameObject.transform.position = transform.position + lightDist;
    }

    private void Flicker()
    {
        var rnd = Random.Range(0, 3);
        if (lightGameObject != null)
        {
            if (rnd <= 2)
            {
                lightGameObject.SetActive(true);
            }
            else
            {
                lightGameObject.SetActive(false);
            }
        }
    }

    void PlayScareSound() 
    {
        var rnd = Random.Range(0, 3);
        clips[rnd].Play();
    }
}
