using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody2D rb;
	private Animator _animator;
	void Start()
	{
		_animator = GetComponent<Animator>();
		Destroy(gameObject, 5f);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.CompareTag("Player")) 
		{
			Character.Instance.Die();
		}
	}
}
