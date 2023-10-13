using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
	[Header("stats")]
	public int maxLife = 1000;
	public int life;
	public float moveSpeed = 10f;
	protected bool isAttacking = true;


	[Header("specialAttackStats")]
	public float SpecialAttackCooldown = 0f;
	protected bool isUsingSpecialAttack = false;
	protected float SpecialAttactTimer = 0;
	protected float SpecialAttactDuration = 0f;
	protected bool canUseSpecialAttack = false;

	[Header("dead")]
	protected bool isDead = false;
	public float deadTime = 5f;
	float deadTimer = 0f;

	[Header("references")]
	public Rigidbody rb;

	public GameObject animatiorContainer;
	protected Animator bodyAnim;

	[Header("UI")]
	TextMeshProUGUI PlayerLifeUI;

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

	protected void StartMethod()
	{
		life = maxLife;
		isAttacking = false;
		canUseSpecialAttack = true;

		PlayerLifeUI = GameObject.FindGameObjectWithTag("UIPlayerLife").GetComponent<TextMeshProUGUI>();
		PlayerLifeUI.text = $"Life: {life}/{maxLife}";
	}

	protected void Move()
	{
		if (!isUsingSpecialAttack)
		{
			rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

			MoveAnim();
        }
	}

	void MoveAnim()
	{
		if (bodyAnim != null)
		{
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				bodyAnim.SetBool("Move", true);
			}
			else
			{
				bodyAnim.SetBool("Move", false);
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

		if (life <= 0)
		{
			PlayerLifeUI.text = $"Life: 0/{maxLife}";
			if (bodyAnim != null) bodyAnim.SetBool("dead", true);
			isDead = true;
        }
		else
		{
			PlayerLifeUI.text = $"Life: {life}/{maxLife}";
		}
	}
	protected void DeadAnim()
	{
		if (isDead && !Manager.instance.isGameOver)
		{
			if (deadTimer < deadTime)
			{
				deadTimer += Time.deltaTime;
			}
			else
			{
				Manager.instance.GameOver();
			}
		}
	}

	

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("LevelUp"))
		{
			Manager.instance.LevelUp();
		}
		else if (collision.gameObject.CompareTag("House"))
		{
			if (isAttacking)
			{
				House n = collision.gameObject.GetComponent<HouseCollider>().parent.GetComponent<House>();
				if (n != null) n.DestroyEffect();

				Destroy(collision.gameObject);
				Manager.instance.HousesCount(-1);
				Manager.instance.AddHouseDestroyed();
				if (bodyAnim != null) bodyAnim.SetTrigger("Attack");
            }
		}
		else if (collision.gameObject.CompareTag("Nuclear"))
		{
			if (isAttacking)
			{
				House n = collision.gameObject.GetComponent<HouseCollider>().parent.GetComponent<House>();
				if (n != null) n.SpawnNuclearObj();
				if (bodyAnim != null) bodyAnim.SetTrigger("Attack");


				Manager.instance.HousesCount(-1);
				Destroy(collision.gameObject);
			}
		}
	}
}
