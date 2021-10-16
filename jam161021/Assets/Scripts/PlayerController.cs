using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

	public Rigidbody2D rb2D;
    public float Speed = 2f;

	// bool BouncyBool = false;
	// float count = 0;
    void Update(){
		// count += Time.deltaTime;
		// if (BouncyBool){
		// 	transform.Translate(Vector3.up*5 * Time.deltaTime);
		// }
		// if (count>= 1.2)
		// {
		// 	count = 0;
		// 	BouncyBool = false;
		// }
		
        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;
        Vector3 move = transform.right * horizontal + transform.up * vertical;
        characterController.Move(move * Speed * Time.deltaTime);
    }
	// void OnCollisionEnter2D(Collision2D col) {
	// 	Debug.Log(col.collider.bounds);
    //     BouncyBool = true;
    // }
}
