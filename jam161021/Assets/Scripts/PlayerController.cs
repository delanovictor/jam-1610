using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

	public Rigidbody2D rb2D;
    public float Speed = 2f;

    void Update(){
		
        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;
        Vector3 move = transform.right * horizontal + transform.up * vertical;
        characterController.Move(move * Speed * Time.deltaTime);
    }
}
