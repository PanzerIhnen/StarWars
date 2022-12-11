using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _distanceToPlayer;

    [Header("Attack")]
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform[] _posRotBullet;
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private float _cadence;

    private Transform _player;
    private float _attackTimer;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        _attackTimer = _cadence;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_player != null)
        {
            transform.LookAt(_player);
            if (Vector3.Distance(transform.position, _player.position) > _distanceToPlayer)
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (_attackTimer >= _cadence)
        {
            _attackTimer = 0;
            _audio.Play();

            for (int i = 0; i < _posRotBullet.Length; i++)
            {
                Instantiate(_bulletPrefab, _posRotBullet[i].position, _posRotBullet[i].rotation);
            }
        }
        else
        {
            _attackTimer += Time.deltaTime;
        }
    }
}
