using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int PlayerID { get; set; }
	public Character Character { get; private set; }

	void Awake()
	{
		Character = new Character();
	}
	
	void Start () {
		
	}

	void FixedUpdate()
	{
		
	}

	void Update () {
		
	}

	private void OnTriggerEnter(Collider coll)
	{
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
	}
}