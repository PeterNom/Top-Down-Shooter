using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;
    public Transform controlThisObject; //Here can be the transform of the weapon
    private Vector3 mousePos;

    private void Awake()
    {
        _playerActions = new PlayerActions();

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
        _rbody.velocity = _moveInput * _speed;

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
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
