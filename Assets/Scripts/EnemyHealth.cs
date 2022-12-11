using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Image _healthFill;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _bulletDamage;

    private float _currentHealth;
    private GameManager _gameManager;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Damage()
    {
        _currentHealth -= _bulletDamage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _healthFill.fillAmount = _currentHealth / _maxHealth;

        if (_currentHealth <= 0)
        {
            _gameManager.EnemyDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Damage();
        }
    }
}
