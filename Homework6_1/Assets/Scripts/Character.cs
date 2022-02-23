using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 10.0f;
    public float rotationSpeed = 0.2f;
    public float animationBlendSpeed = 0.2f;

    CharacterController controller;
    Animator animator;
    Camera characterCamera;
    float rotationAngle = 0.2f;
    float targetAnimationSpeed = 0.0f;
    bool isSprint = false;

    float speedY = 0.0f;
    float gravity = -9.8f;
    bool isJumping = false;
    float jumpSpeed = 7.0f;
    bool isDead = false;
    public CharacterController Controller
    {
        get { return controller = controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }
    }

    public Animator CharacterAnimation
    {
        get { return animator = animator ?? GetComponent<Animator>(); }
    }

    void Update()
    {
        if (!isDead)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            Move(horizontal, vertical);
            Dead();
            Defend();
            Jump();
            Attack();

            if (!Controller.isGrounded)
            {
                speedY += gravity * Time.deltaTime;
            }
            else if (speedY < 0.0f)
            {
                speedY = 0.0f;
            }
            CharacterAnimation.SetFloat("SpeedY", speedY / jumpSpeed);
            if (isJumping && speedY < 0.0f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
                {
                    isJumping = false;
                    CharacterAnimation.SetTrigger("Land");
                }
            }
        }
        Alive();
    }

    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            CharacterAnimation.SetTrigger("Attack1");
        }
    }
    void Defend()
    {
        if (Input.GetMouseButton(1))
        {
            CharacterAnimation.SetTrigger("Defend");
        }
    }
    void Dead()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            CharacterAnimation.SetTrigger("Dead");
            isDead = true;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            CharacterAnimation.SetTrigger("Jump");
            speedY += jumpSpeed;
        }
    }
    void Alive()
    {
        if (Input.GetKey(KeyCode.CapsLock))
        {
            CharacterAnimation.SetTrigger("Alive");
            isDead = false;
        }
    }
    private void Move(float horizontal, float vertical)
    {
        isSprint = Input.GetKey(KeyCode.LeftShift);
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        Vector3 roratedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;
        float currentSpeed = isSprint ? sprintSpeed : movementSpeed;
        Controller.Move((verticalMovement + roratedMovement) * currentSpeed * Time.deltaTime);

        if (roratedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(roratedMovement.x, roratedMovement.z) * Mathf.Rad2Deg;
            targetAnimationSpeed = isSprint ? 1.0f : 0.5f;
        }
        else
        {
            targetAnimationSpeed = 0.0f;
        }
        CharacterAnimation.SetFloat("Speed", Mathf.Lerp(CharacterAnimation.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }
}
