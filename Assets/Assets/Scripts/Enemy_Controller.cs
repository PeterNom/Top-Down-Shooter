using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Controller : MonoBehaviour
{
    public GameObject[] Target;
    public float step = 1.0f;
    public float attack_speed = 2.0f;

    private const string In_motion = "In_motion";
    private const string Attacking = "Attacking";
    private const string Alive = "Alive";
    private Rigidbody2D _rbody;
    private bool alive = true;

    public float time_elapsed= 0.0f;
    public Vector3 pad;
    public Vector3 target;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        pad = new Vector3(0.3f, 0.0f, 0.0f);
        _rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool(In_motion, true);
        animator.SetBool(Alive, true);
        Target = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        time_elapsed += Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(alive)
        {
            target = Target[0].transform.position - pad;
            // move sprite towards the target location
            transform.position = Vector2.MoveTowards(transform.position, Target[0].transform.position - pad, step * Time.deltaTime);
        }              
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            UnityEngine.Debug.Log("Dead");
            alive = false;
            animator.SetBool(Alive, false);
            Destroy(gameObject,1);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Attack");
            animator.SetBool(Attacking, true);
            time_elapsed = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Attack");
            animator.SetBool(Attacking, false);
            time_elapsed = 0;
        }
    }
}
