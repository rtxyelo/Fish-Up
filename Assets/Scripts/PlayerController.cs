using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private MoveDirection _moveDirection = MoveDirection.None;

    [SerializeField] private float _speed = 100f;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    private AudioController _audioController;

    private bool _isCanMove = true;

    [HideInInspector]
    public UnityEvent<GameObject> IsBallHit;

    private void Awake()
    {
        IsBallHit = new UnityEvent<GameObject>();

        if (TryGetComponent(out Rigidbody2D rb))
        {
            _rb = rb;
        }
        else
        {
            new NullReferenceException("Check Player RigidBody!");
        }

        _audioController = FindObjectOfType<AudioController>();
    }

    private void FixedUpdate()
    {
        if (_moveDirection != MoveDirection.None && _isCanMove)
        {
            switch (_moveDirection)
            {
                case MoveDirection.Up:
                    MovePlayer(1);
                    break;

                case MoveDirection.Down:
                    MovePlayer(-1);
                    break;

                default:
                    break;
            }
        }
    }

    private void MovePlayer(int moveDir)
    {
        _rb.velocity = new Vector2(0f, moveDir * Time.deltaTime * _speed);
    }

    public void ChangeMoveSide(int moveDirection)
    {
        _moveDirection = (MoveDirection)moveDirection;
        _rb.velocity = Vector2.zero;

        if (_audioController != null)
            _audioController.PlayRodSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Fish"))
            {
                if (_audioController != null)
                    _audioController.PlayFishSound();

                _scoreCounter.IncreaseScore();
                Destroy(collision.gameObject);
            }
        }
    }
}
