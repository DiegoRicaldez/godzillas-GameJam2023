using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [Header("attributes")]
	public int level = 0;
	public float timeBtwSpawn = 1f;
	public int housesToSpawn = 2;
    public bool canSpawn = true;
    public bool spawnNuclear = false;
	public bool nuclearSpawned = false;
	public float timer = 0f;

	[Header("references")]
    public Transform TopLeft;
    public Transform BottomRight;

    [Header("Prefabs")]
	public List<GameObject> HousesLvl1;
    public List<GameObject> HousesLvl2;
    public List<GameObject> HousesLvl3;
    public List<GameObject> HousesLvl4;
    public List<GameObject> HousesLvl5;
    public GameObject Nuclear;

	List<GameObject> actualHouses;


    void Start()
    {
		LevelUp(0);
	}

    void Update()
    {
		if (level > 0) Spawn();
    }

	void Spawn()
	{
		if (timer < timeBtwSpawn)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0f;

			if (!nuclearSpawned && canSpawn)
			{
				if (!spawnNuclear) // normal
				{
					for (int i = 0; i < housesToSpawn; i++)
					{
						if (!canSpawn) break;

						int house = Random.Range(0, actualHouses.Count);
						House h = actualHouses[house].GetComponent<House>();

						// Calcula la mitad del tamaño del área cuadrada
						Vector3 mitadDelTamano = h.houseBody.localScale * 0.5f;
						Vector3 position = new Vector3(Random.Range(TopLeft.position.x, BottomRight.position.x), 0f, Random.Range(BottomRight.position.z, TopLeft.position.z));

						// Verifica si hay colisión en el área cuadrada
						Collider[] colliders = Physics.OverlapBox(position, mitadDelTamano);
						colliders = colliders.Where(collider => collider.gameObject.CompareTag("Floor") == false).ToArray();

						if (colliders.Length <= 0)
						{
							Instantiate(actualHouses[house], position, Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
							Manager.instance.HousesCount(1);
						}
					}
				} 
				else// nuclear
				{
					House n = Nuclear.GetComponent<House>();

					// Calcula la mitad del tamaño del área cuadrada
					Vector3 mitadDelTamano = n.houseBody.localScale * 0.5f;
					Vector3 position = new Vector3(Random.Range(TopLeft.position.x, BottomRight.position.x), 0, Random.Range(BottomRight.position.z, TopLeft.position.z));
					// Verifica si hay colisión en el área cuadrada
					Collider[] colliders = Physics.OverlapBox(position, mitadDelTamano);
					colliders = colliders.Where(collider => collider.gameObject.CompareTag("Floor") == false).ToArray();

					if (colliders.Length <= 0)
					{
						Instantiate(Nuclear, position, Quaternion.Euler(0, Random.Range(0, 4) * 90, 0));
						Manager.instance.HousesCount(1);

						spawnNuclear = false;
						nuclearSpawned = true;
					}
				}

			}
		}
	}

	public void LevelUp(int lvl)
	{
		level = lvl;

		switch (level)
		{
			case 0:
				actualHouses = new List<GameObject>();
				break;
			case 1:
				actualHouses = HousesLvl1;
				break;
			case 2:
				actualHouses = HousesLvl2;
				break;
			case 3:
				actualHouses = HousesLvl3;
				break;
			case 4:
				actualHouses = HousesLvl4;
				break;
			case 5:
				actualHouses = HousesLvl5;
				break;
		}

		// destruir la casa nuclear si aparece 
	}
}
