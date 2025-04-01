using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    //скорость персонажа
    public float speed = 4.0f;
    //скорость прыжка персонажа
    public float jumpSpeed = 8.0f;
    //переменная гравитации персонажа
    public float gravity = 20.0f;
    //переменная движения персонажа
    private Vector3 moveDir = Vector3.zero;
    //переменная содержущая компонент CharacterController
    private CharacterController controller;

    //Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if(controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        if( Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            moveDir.y = jumpSpeed;
        }

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move (moveDir * Time.deltaTime);
    }
}