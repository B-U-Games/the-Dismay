using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float value = 100;
    private EnemyAI enemyAI;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Animator animator;
    private CapsuleCollider collider;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }
    public void DealDamage(float damage)
    {
        value -= damage;
        if (value <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        enemyAI.enabled = false;
        navMeshAgent.enabled = false;
        collider.enabled = false;
        animator.enabled = false;
    }
}
