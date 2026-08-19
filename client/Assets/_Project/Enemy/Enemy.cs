using System;
using TopdownRPG.Combat;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    [SerializeField] private float health = 3;

   
    public GameObject GameObject { get; }
    
    private GameObject _player;
    private Animator _animator;

    private void Start() {
        _player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}