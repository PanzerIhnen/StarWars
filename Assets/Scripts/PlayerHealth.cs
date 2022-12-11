using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Image _healthFill;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _bulletDamage;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Damage()
    {
        _currentHealth -= _bulletDamage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _healthFill.fillAmount = _currentHealth / _maxHealth;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletEnemy"))
        {
            Damage();
        }
    }
}
