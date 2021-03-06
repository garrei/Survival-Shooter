﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour 
{
	//Player Properties

	public float rotationSpeedRun = 5000;
	public float rotationSpeedWalk = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;

	private Quaternion targetRotation;
	private CharacterController controller;
	private Camera cam;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController> ();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Uncommented choice enables that for player control.
		ControlMouse();
		//ControlWASD();
	}

	void ControlMouse ()
	{
		//Gives more speed and faster aim response
		if (Input.GetButton ("Run")) 
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
			targetRotation = Quaternion.LookRotation (mousePos - new Vector3 (transform.position.x, 0, transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeedRun * Time.deltaTime);

			Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			Vector3 motion = input;
			motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.x) == 1) ? .7f : 1;
			motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
			motion += Vector3.up * -8;

			controller.Move (motion * Time.deltaTime);
		} 

		//Uses normal walking speed and slower aim
		else 
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = cam.ScreenToWorldPoint (new Vector3 (mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
			targetRotation = Quaternion.LookRotation (mousePos - new Vector3 (transform.position.x, 0, transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeedWalk * Time.deltaTime);
			
			Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			Vector3 motion = input;
			motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.x) == 1) ? .7f : 1;
			motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
			motion += Vector3.up * -8;
			
			controller.Move (motion * Time.deltaTime);
		}
	}
	
	void ControlWASD ()
	{
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
		
		if (input != Vector3.zero) 
		{
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeedWalk * Time.deltaTime);
		} 
		
		if (Input.GetButton ("Run")&&(input != Vector3.zero))
		{
			targetRotation = Quaternion.LookRotation (input);
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeedRun * Time.deltaTime);
		}
		
		Vector3 motion = input;
		motion *= (Mathf.Abs (input.x) == 1 && Mathf.Abs (input.x) == 1) ? .7f : 1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move (motion * Time.deltaTime);
	}
}
