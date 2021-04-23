using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controlador;
    private Animator animacao;

    public float vel;
    public bool run;
    public bool giro;
    public bool andando;
    private float rot;
    public float velrot;
    private Vector3 movedir;
    public float gravidade;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Move();

        if (run == true)
        {
            vel = 12;
        }

        if (run == false)
        {
            vel = 5;   
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            giro = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            giro = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            giro = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            giro = false;
        }

        if (giro == true && andando ==false)
        {
            animacao.SetInteger("IDLE", 3);
        }

        if (giro == false && andando == false && run == false)
        {
            animacao.SetInteger("IDLE", 0);
        }
    }

    void Move() 
    {
        if (controlador.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                andando = true;
                movedir = Vector3.forward * vel;
                animacao.SetInteger("IDLE", 1);
                if ( vel == 12)
                {
                    animacao.SetInteger("IDLE", 2);
                }
            }
            
            if (Input.GetKeyUp(KeyCode.W))
            {
                andando = false;
                movedir = Vector3.zero;
                animacao.SetInteger("IDLE", 0);
            }
            
            
            if (Input.GetKey(KeyCode.S))
            {
                andando = true;
                vel = -5;
                movedir = Vector3.forward * vel;
                animacao.SetInteger("IDLE", 1);
            }
            
            if (Input.GetKeyUp(KeyCode.S))
            {
                andando = false;
                movedir = Vector3.zero;
                animacao.SetInteger("IDLE", 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                andando = false;
                run = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                andando = false;
                run = false;
            }
            
        }

        rot += Input.GetAxis("Horizontal") * velrot * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);


        movedir.y -= gravidade * Time.deltaTime;
        movedir = transform.TransformDirection(movedir);


        controlador.Move(movedir * Time.deltaTime);
    }
}
