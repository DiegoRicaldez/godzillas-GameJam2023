using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public SpecialType Type = SpecialType.Acid;
    public Rigidbody rb;
    public float speed = 4f;

    public float duration = 3f;
    float timer = 0f;

    void Start()
    {
        if (Type == SpecialType.Acid) Acid();
        Destroy(gameObject, duration);
	}

    void Update()
    {
        if (Type == SpecialType.lash) Lash();
		else if (Type == SpecialType.ray) Ray();
	}

    void Acid()
    {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-45, 46), transform.rotation.eulerAngles.y + Random.Range(-15, 46), transform.rotation.eulerAngles.z + Random.Range(-45, 46));
		rb.AddForce(transform.forward * Random.Range(speed, speed + 3), ForceMode.Impulse);
    }
    void Lash()
    {
		Vector3 v = new Vector3(transform.localScale.x + speed * Time.deltaTime, transform.localScale.y + speed * Time.deltaTime, transform.localScale.z + speed * Time.deltaTime);
		transform.localScale = v;
	}
    void Ray()
    {
        timer += Time.deltaTime;

        if (timer > duration / 3)
        {
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + speed * Time.deltaTime, transform.localScale.z);

			transform.position = new Vector3(transform.position.x,transform.position.y + speed * Time.deltaTime,transform.position.z);
		}
    }

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("House"))
		{
			House n = collision.gameObject.GetComponent<HouseCollider>().parent.GetComponent<House>();
			if (n != null) n.DestroyEffect();
			Destroy(collision.gameObject);

			Manager.instance.HousesCount(-1);
			Manager.instance.AddHouseDestroyed();
		}
	}
}

public enum SpecialType
{
    Acid,
	lash,
	ray
}
