using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public List<Transform> patrolPoints;
    public GameObject player;
    private NavMeshAgent _navMeshAgent;
    public float viewAngle;
    public float damage = 30f;
    private bool _isPlayerNoticed = false;
    private PlayerHealth _playerHealth;
    private Animator animator;
    private bool _attackPlayer = false;
    public AudioClip enemySFX;
    public AudioClip attackSFX;
    private AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        NoticePlayerUpdate();
        ChaseUpdate();
        PatrolUpdate();
        AttackUpdate();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reaction") && !_played)
        {
            _played = true;
            audioSource.PlayOneShot(enemySFX);
        }
        else if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Patrol") || animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) && _played)
        {
            _played = false;
        }
    }
    private void PatrolUpdate()
    {
        if(!_isPlayerNoticed)
        {
            animator.SetBool("Attack", false);
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                animator.SetBool("Patrol", false);
                Invoke("PickNewPatrolPoint", 2f);
            }
        }
        
    }
    private void PickNewPatrolPoint()
    {
        _navMeshAgent.speed = 1.5f;
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
        animator.SetBool("Patrol", true);
    }
    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }
    private bool _played = false;
    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 25f))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    if (!_attackPlayer)
                    {
                        animator.SetBool("Patrol", false);
                        animator.SetBool("Attack", true);
                        transform.rotation = new Quaternion(transform.rotation.x, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hit.collider.transform.position - transform.position), 50f * Time.deltaTime).y, transform.rotation.z, transform.rotation.w);
                        _isPlayerNoticed = true;
                        _navMeshAgent.speed = 7f;
                        Invoke("AttackPlayer", 2.7f);
                    }
                }
            }
        }
        else
        {
            _isPlayerNoticed = false;
            _attackPlayer = false;
        }
    }
    private void AttackPlayer()
    {
        _attackPlayer = true;
        player.GetComponent<PhraseScript>().PlayFirstEncounter();
    }
    private void ChaseUpdate()
    {
        if(_attackPlayer)
        {
            _navMeshAgent.speed = 7f;
            _navMeshAgent.destination = player.transform.position;
        }
    }
    private void AttackUpdate()
    {
        if(_attackPlayer)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= _navMeshAgent.stoppingDistance)
            {
                _playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }
    }
}