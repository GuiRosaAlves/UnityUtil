using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
	[System.Serializable]
	public struct Stats
	{
		public float Strenght;
		public float Agility;
		public float Resistance;
	}

	[SerializeField] private Stats _playerStats;
//	public Weapon currWeapon;

	public bool Jump()
	{
		return false;
	}

	public bool Walk()
	{
		return false;
	}

	public bool Run()
	{
		return false;
	}

	public bool Climb()
	{
		return false;
	}

	public bool Attack()
	{
		return false;
	}
}
