using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;

    Rigidbody rb;
    NavMeshAgent agent;
    Animator anim;

    Ray ray;
    RaycastHit hit;
    public LayerMask layerFloor;

    public bool cambioMovPlayer; //true: dificultad pesadilla, false: dificultad media

    GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(cambioMovPlayer == false) transform.Rotate(Vector3.up * turnSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        else if (cambioMovPlayer == true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, layerFloor))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    agent.SetDestination(hit.point);
                }
            }
            if (agent.velocity.magnitude != 0) anim.SetBool("isMoving", true);
            else anim.SetBool("isMoving", false);
        }

    }
    private void FixedUpdate()
    {
        if(cambioMovPlayer == false)
        {
            Vector3 direction = new Vector3(0, 0, Input.GetAxis("Vertical")).normalized;

            rb.velocity = transform.forward * direction.z * speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Goal"))
        {
            gameManager.YouWin();
        }
    }
}
