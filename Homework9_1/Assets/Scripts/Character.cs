using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float Speed = 5;
    [SerializeField]
    float JumpForce = 1;
    [SerializeField] private CoinScript _coinScript;

    public static Character Instance { get;  set; }
    public LayerMask groundLayer;

    public GameObject character;

    private Animator _animator;
    private Rigidbody2D _rb;
    private bool _isGrounded = true;
    private bool _isFacingRight = true;
    private bool _isMoving = true;
    private static int _lifeCount = 3;
    private Vector2 _startPossition = new Vector2(-5.7f, -4.17f);

    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coinScript = GetComponent<CoinScript>();
        _animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    void MovementLogic() 
    {
        if (_isMoving)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(moveHorizontal * Speed, _rb.velocity.y);
            _animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

            if (moveHorizontal > 0 && !_isFacingRight)
                Flip();
            else if (moveHorizontal < 0 && _isFacingRight)
                Flip();
        }
    }
    void JumpLogic() 
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.velocity = Vector2.up * JumpForce;
                _animator.SetTrigger("Jump");
            }
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f,180f,0f);
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            var a = collision.gameObject.transform.root;
            a.transform.GetChild(0).gameObject.SetActive(false);
            a.transform.GetChild(1).gameObject.SetActive(true);
            _coinScript.CreateCoin(a.transform.GetChild(3).gameObject.transform.position);
        }
        if (collision.tag == "Death")
        {
            Die();
        }
        if (collision.tag == "Finish")
        {
            ScoreManager.Instance.Finish();
            _isMoving = false;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision2D collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
    public void Die() {
        _isMoving = false;
        _animator.SetTrigger("Die");
        character.transform.position = _startPossition;
       
        _lifeCount--;
        ScoreManager.Instance.LifeCounter();
        if (_lifeCount == 0)
        {
             Destroy(gameObject, 0.5f);
        }
        _isMoving = true;
    }

    private void AwakeCharacter() 
    {
        Instantiate(character, _startPossition, Quaternion.identity); 
    }
}
