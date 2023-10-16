using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class House : MonoBehaviour
{
    public bool isHouse = true;
    public float height = 1f;
    public Transform houseBody;
    public GameObject spawnEffect;

    [Header("Spawn Person")]
    public List<Transform> spawnPoints;
    public GameObject PersonPrefab;
    public GameObject ExplosionPrefab;
	public GameObject NuclearObjPrefab;
	public float minTimer = 5f;
    public float maxTimer = 10f;
    float ActualTimer;
    float timer = 0f;

    [Header("anim Spawn house")]
    float scaleY = 0f;
    public float maxScaleY = 1f;
	float posX = -0.2f;
	public float maxPosX = 0.5f;
	public bool inSpawn = true;

	[Header("Sounds")]
	public AudioClip DestroySound;

	// Start is called before the first frame update
	void Start()
    {
        Instantiate(spawnEffect, transform.position, Quaternion.Euler(-90,0,0));
        newTimer();

		houseBody.localScale = new Vector3(houseBody.localScale.x, scaleY, houseBody.localScale.z);
        houseBody.position = new Vector3(houseBody.position.x, posX, houseBody.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAnim();
        Spawner();
    }
    void SpawnAnim()
    {
        if (inSpawn)
        {
			if (scaleY < maxScaleY)
			{
				scaleY += Time.deltaTime;
				houseBody.localScale = new Vector3(houseBody.localScale.x, scaleY, houseBody.localScale.z);
			}

			if (posX < maxPosX)
			{
				posX += Time.deltaTime / 2;
				houseBody.position = new Vector3(houseBody.position.x, posX, houseBody.position.z);
			}

			if (scaleY >= maxScaleY && posX >= maxPosX) inSpawn = false;
		}
       
    }

	void Spawner()
	{
		if (!inSpawn && isHouse)
        {
			if (timer < ActualTimer)
			{
				timer += Time.deltaTime;
			}
			else
			{
				timer = 0f;
				newTimer();

				if (spawnPoints != null && spawnPoints.Count > 0)
				{
					int spawnpoint = Random.Range(0, spawnPoints.Count);
					GameObject p = Instantiate(PersonPrefab, spawnPoints[spawnpoint].position, Quaternion.identity);
                    p.transform.parent = houseBody;
					spawnPoints.RemoveAt(spawnpoint);
				}
			}
		}
	}

    public void SpawnNuclearObj()
    {
        if (!isHouse)
        {
			Instantiate(ExplosionPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
			Instantiate(NuclearObjPrefab, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
		}
    }
	public void DestroyEffect()
	{
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
		AudioManager.instance.PlaySFX(DestroySound);
	}

	void newTimer()
    {
        ActualTimer = Random.Range(minTimer, maxTimer) + 1;
    }
}
