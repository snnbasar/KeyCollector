using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{

    public static KarakterKontrol instance;

    private Rigidbody rg;
    private Animator anim;

    private float hor;
    private float ver;

    private Vector3 movement;

    [SerializeField] private float speed = 4f;

    private bool canMove;

    private Joystick joystick;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        rg = this.GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<Animator>();
        joystick = Joystick.instance;

        GameManager.instance.OnStartGame += StartMovement;
        GameManager.instance.OnPlayerDied += Die;
        GameManager.instance.OnMissionCompleted += StopMovement;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(canMove)
            Movement();
        SetAnimationProperties();
    }

    private void Movement()
    {
        ver = Input.GetAxis("Vertical") + joystick.InputVector.y;
        hor = Input.GetAxis("Horizontal") + joystick.InputVector.x;



        movement = new Vector3(hor, 0, ver);

        movement = transform.TransformDirection(movement);

        rg.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    private void StartMovement()
    {
        canMove = true;
    }

    private void StopMovement()
    {
        ver = 0f;
        hor = 0f;
        canMove = false;
    }
    private void SetAnimationProperties()
    {
        anim.SetFloat("hor", hor);
        anim.SetFloat("ver", ver);
    }
    public void Die()
    {
        Debug.Log("Player is dead now.");
        StopMovement();
        SoundManager.instance.PlayMusic(Soundlar.Killed);
    }
}
