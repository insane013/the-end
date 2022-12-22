using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("AI SETUP")]
    [SerializeField] private int agr_radius = 15;
    [SerializeField] private float attack_radius = 2;
    [SerializeField] private float x = 18;
    [SerializeField] private float z = 18;

    private GameObject player;
    private NavMeshAgent navAgent;
    private Animator anim;

    public float hp = 100f;

    private bool dead = false;

    private bool isSET = false;

    private int a;

    [SerializeField] private float dist_to_player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.isStopped = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!dead)
        {
            dist_to_player = Vector3.Distance(transform.position, player.transform.position);
            if (dist_to_player < agr_radius)
            {
                navAgent.enabled = true;
                navAgent.SetDestination(player.transform.position);
                anim.SetTrigger("walk");
            }
            if (dist_to_player < attack_radius)
            {
                navAgent.enabled = false;
                anim.SetTrigger("attack");
            }
            if (dist_to_player > agr_radius)
            {
                if (isSET == false)
                {
                    a = Random.Range(0, 100);
                    if (a <= 40)
                    {
                        navAgent.enabled = false;
                        anim.SetTrigger("idle");
                        StartCoroutine(Wait(5f));
                    }
                    if (a > 40)
                    {
                        navAgent.enabled = true;
                        anim.SetTrigger("walk");
                        StartCoroutine(GO());
                    }
                }
            }
        }

        if (hp <= 0)
        {
            dead = true;
        }
        if (dead)
        {
            hp = 0;
            anim.SetTrigger("die");
        }
    }
    private IEnumerator Wait(float time)
    {
        isSET = true;
        yield return new WaitForSeconds(time);
        isSET = false;
    }
    private IEnumerator GO()
    {
        isSET = true;

        navAgent.SetDestination(new Vector3(Random.Range(-x,x), transform.position.y, Random.Range(-z,z)));
        if (navAgent.remainingDistance <=.01f)
        {
            isSET = false;
            yield return null;
        }

        isSET = false;
    }

    public void DMG(float dmg)
    {
        hp -= dmg;
    }
}