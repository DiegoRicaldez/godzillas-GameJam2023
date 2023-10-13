using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 1;
    public float Speed = 4f;

    public float LifeTime = 10f;
    public Rigidbody rb;
    Transform player;


    void Start()
    {
        RotateToPlayer();
    }

    void Update()
    {

	}

	void RotateToPlayer()
	{
		GameObject obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null)
		{
			player = obj.GetComponent<PlayerBase>().transform;
			transform.LookAt(player.position);
			rb.AddForce(transform.forward * Speed, ForceMode.Impulse);
			Destroy(gameObject, LifeTime);
		}
		else
		{
			Destroy(gameObject);
		}
	}

    public void AlterRotation()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            player = obj.GetComponent<PlayerBase>().transform;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-45, 46), transform.rotation.eulerAngles.y + Random.Range(-45, 46), transform.rotation.eulerAngles.z + Random.Range(-45, 46));

            rb.Sleep();
            rb.WakeUp();
            
            rb.AddForce(transform.forward * Speed, ForceMode.Impulse);
            Destroy(gameObject, LifeTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBase p = collision.GetComponent<PlayerBase>();
            p.TakeDamage(Damage);
            Destroy(gameObject);
		}
        else if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
	}
}
