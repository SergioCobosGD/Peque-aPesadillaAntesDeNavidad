using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fantasmas : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Ray ray;
    RaycastHit hit;
    public LayerMask layerWalls;
    public float distanceToDetect;
    public float angleToDetect;
    public bool isEvil; //Si esta en false es un fantasma bueno, si esta en true es un fantasma malo
    public bool isFollow; //Si esta siguiendo al jugador pasa a true
    public float timer;
    public float cooldownPatrol;
    public bool patrolSwitch;
    public Transform pos1;
    public Transform pos2;
    GameManager gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        isFollow = false;
        patrolSwitch = false;
        timer = cooldownPatrol;
    }

    void Update()
    {
        ISeeYou();
        if(isFollow == true && isEvil == false)
        {
            IFollowYou();
        }
        else if (isEvil == true)
        {
            if (timer >= cooldownPatrol)
            {
                if(patrolSwitch == false)
                {
                    agent.SetDestination(pos1.position);
                    timer = 0;
                    patrolSwitch = true;
                }
                else if(patrolSwitch == true)
                {
                    agent.SetDestination(pos2.position);
                    timer = 0;
                    patrolSwitch = false;
                }
            }
        }
        if(timer <= cooldownPatrol)
        {
            timer += Time.deltaTime;
        }
    }
    void ISeeYou()
    {
        Vector3 direction = player.transform.position - transform.position;
        ray = new Ray(transform.position, direction);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerWalls)) //Golpea las paredes
        {
            if (hit.transform.CompareTag("Player"))
            {
                if(hit.distance < distanceToDetect)
                {
                    //Angulo entre la direccion del personaje y el forward del enemigo
                    float angleOfVision = Vector3.Angle(hit.point - transform.position, transform.forward);
                    //Debug.Log("El player es detectado en un angulo de: " + angleOfVision);
                    if (angleOfVision <= angleToDetect)
                    {
                        if (isEvil == true) gameManager.GameOver();
                        else isFollow = true;
                    }
                }
            }
        }
    }
    void IFollowYou()
    {
        agent.SetDestination(player.transform.position);
    }
}
