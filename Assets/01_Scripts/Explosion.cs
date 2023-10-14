using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (effect == null)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            Vector3 v = new Vector3 (transform.localScale.x + speed*Time.deltaTime, transform.localScale.y + speed * Time.deltaTime, transform.localScale.z + speed * Time.deltaTime);
            transform.localScale = v;
        }
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("House"))
		{
			Destroy(collision.gameObject);
			Manager.instance.HousesCount(-1);
		}
	}
}
