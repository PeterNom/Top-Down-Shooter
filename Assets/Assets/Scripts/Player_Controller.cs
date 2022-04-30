using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private const string In_motion = "Moving";
    private const string Fire = "Firing";
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;
    private float _fireInput;
    private float shootDelay = .5f;
    public Transform firePoint; //Here can be the transform of the weapon
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    Animator animator;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        animator = GetComponent<Animator>();

        _rbody = GetComponent<Rigidbody2D>();
        if(_rbody is null)
        {
            Debug.LogError("RigidBody2D is missing.");
        }
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void FixedUpdate()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _fireInput = _playerActions.Player_Map.Fire.ReadValue<float>();
        _rbody.velocity = _moveInput * _speed;

        shootDelay -= Time.deltaTime;

        if ( _rbody.velocity.x != 0 || _rbody.velocity.y != 0 )
        {
            animator.SetBool(In_motion, true);
        }
        else
        {
            animator.SetBool(In_motion, false);
        }

        if (_fireInput==1 && shootDelay <= 0)
        {
            animator.SetBool(Fire, true);
            Shoot();
            shootDelay = 0.5f;
        }
        else
        {
            
            animator.SetBool(Fire, false);
        }

        MouseL();
    }

    //Mouse Look script
    void MouseL()
    {
        //Gets mouse position, you can define Z to be in the position you want the weapon to be in
        //mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousePos = _playerActions.Player_Map.MousePosition.ReadValue<Vector2>();
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90.0f;
        _rbody.rotation = angle; //Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
