using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public GameObject Target;
    public float speed = 0.01f;
    public float attack_speed = 2.0f;

    private const string In_motion = "In_motion";
    private const string Attacking = "Attacking";
    private Rigidbody2D _rbody;
    public float time_elapsed= 0.0f;
    public Vector3 pad;
    public Vector3 target;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        pad = new Vector3(0.8f, 0.0f, 0.0f);
        _rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool(In_motion, true);
    }

    private void Update()
    {
        time_elapsed += Time.deltaTime;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        target = Target.transform.position - pad;
        //target -= pad;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position - pad, speed);

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (time_elapsed> attack_speed && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attack");
            animator.SetBool(Attacking, true);
            time_elapsed = 0;
        }
        else
        {
            animator.SetBool(Attacking, false);
        }
    }
}
