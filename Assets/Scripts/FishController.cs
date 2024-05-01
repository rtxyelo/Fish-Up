using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private CircleCollider2D _collider;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private float _duration = 1.0f;

    private void Awake()
    {
        if (transform.position.x < 0f)
        {
            _spriteRenderer.flipX = true;
            _duration *= -1;
        }
    }

    private void Start()
    {
        _rb.velocity = Vector2.left * _duration;
    }
}
