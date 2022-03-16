using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _enemyRb;
    public Transform firepoint;
    public Rigidbody2D bulletPrefab;
    public Transform grounPossition;
    public Transform killPossition;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private float _enemySpeed = 60f;
    private bool _mustFlip;
    private bool _mustFlipGoomas;
    private bool _mustPatrol = true;
    private bool _isFacingRight = false;
    private Animator _animator;
    private float _fireRate = 2f;
    private float _nextFire;
    private void Start()
    {
        _nextFire = Time.time;
        _enemyRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Patrol();
    }

    private void FixedUpdate()
    {
        if (_mustPatrol && this.CompareTag("Bowser"))
        {

            _mustFlip = !Physics2D.OverlapCircle(grounPossition.position, 0.1f, groundLayer);
        }
        else if (_mustPatrol && this.CompareTag("Enemy")) 
        {
            _mustFlipGoomas = Physics2D.OverlapCircle(grounPossition.position, 0.1f, wallLayer);
        }
    }

    void Patrol() {
        if (_mustFlip || _mustFlipGoomas)
        {
            Flip();
        }
        _enemyRb.velocity = new Vector2(-_enemySpeed * Time.deltaTime, _enemyRb.velocity.y);
        _animator.SetTrigger("Walk");
        if (bulletPrefab != null)
        {
            Shoot();
        }
    }

    private void Flip()
    {
        _mustPatrol = false;
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);//add flip
        _enemySpeed *= -1;
        _mustPatrol = true;
    }
    void CreateBullet()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        _animator.SetTrigger("Attack");
        bullet.velocity = new Vector2(-_enemySpeed * 0.1f, 0f);
        
    }

    void Shoot() {
        if (Time.time > _nextFire)
        {
            CreateBullet();
            _nextFire = Time.time + _fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _mustPatrol = false;
            _animator.SetTrigger("Die");
            Destroy(this.gameObject, 0.3f);
            //this.gameObject.SetActive(false);
        }
    }
}
