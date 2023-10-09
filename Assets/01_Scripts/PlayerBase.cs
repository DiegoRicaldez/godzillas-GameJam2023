using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	[Header("stats")]
	protected int maxLife = 1000;
	public int life;
	public float moveSpeed = 10f;
	protected bool isAttacking = true;

	[Header("specialAttackStats")]
	public float SpecialAttackCooldown = 0f;
	protected bool isUsingSpecialAttack = false;
	protected float SpecialAttactTimer = 0;
	protected float SpecialAttactDuration = 0f;
	protected bool canUseSpecialAttack = false;


	[Header("references")]
	public Rigidbody rb;
	//public Animator bodyAnim;

	[Header("others")]
	public float PositionY = 1f; //unity

	void Start()
    {
		life = maxLife;
    }

    void Update()
    {
		Move();
		Rotate();
    }

	protected void Move()
	{
		if (!isUsingSpecialAttack)
		{
			rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

			if (rb.velocity.x != 0 || rb.velocity.y != 0)
			{
				// anim walk true
			}
			else
			{
				//anim walk false
			}
		}
	}

	// hacer un metodo de keyDown para mejor precision si es que hay tiempo
	protected void Rotate()
	{
		if (!isUsingSpecialAttack)
		{
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");

			if (x > 0 && y == 0) transform.rotation = Quaternion.Euler(0, 0, 0);

			else if (x > 0 && y < 0) transform.rotation = Quaternion.Euler(0, 45, 0);
			else if (x == 0 && y < 0) transform.rotation = Quaternion.Euler(0, 90, 0);
			else if (x < 0 && y < 0) transform.rotation = Quaternion.Euler(0, 135, 0);
			else if (x < 0 && y == 0) transform.rotation = Quaternion.Euler(0, 180, 0);

			else if (x < 0 && y > 0) transform.rotation = Quaternion.Euler(0, 225, 0);
			else if (x == 0 && y > 0) transform.rotation = Quaternion.Euler(0, 270, 0);
			else if (x > 0 && y > 0) transform.rotation = Quaternion.Euler(0, 315, 0);
		}
	}

	public void TakeDamage(int cant) // la persona y el proyectil usan
	{
		life -= cant;
		// interfaz en manager

		if (life <= 0)
		{
			Debug.Log("falta la muerte del jugador");
			Manager.instance.GameOver();
			//animacion de muerte si es que hay
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("LevelUp"))
		{
			Destroy(collision.gameObject);
			Manager.instance.LevelUp();
		}
		else if (collision.gameObject.CompareTag("House"))
		{
			if (isAttacking)
			{
				Destroy(collision.gameObject);
				Manager.instance.HousesCount(-1);
				Manager.instance.AddHouseDestroyed();
			}
		}
		else if (collision.gameObject.CompareTag("Nuclear"))
		{
			if (isAttacking)
			{
				House n = collision.gameObject.GetComponent<HouseCollider>().parent.GetComponent<House>();
				if (n != null) n.SpawnNuclearObj();



				Manager.instance.HousesCount(-1);
				Destroy(collision.gameObject);
			}
		}
	}
}
