using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public int GameLevel = 0;
	public bool isGameOver = false;

    public int maxHouses = 20;
	public int totalHouses;
    HouseSpawner houseSpawner;
	PlayerBase player;

	public int HousesDestroyed = 0;
	public int pointsToLvlUp = 5;
	public int pointsToAdd = 20;

	public List<GameObject> playerLevelsPrefabs;

	public GameObject MenuPause;
	bool paused = false;
	public GameObject MenuGameOver;

	float timerLevelUp = 0f;
	bool canLevelUpGame = true;

	[Header("UI")]
	public TextMeshProUGUI PointsUI;
	public TextMeshProUGUI NuclearPointsUI;
	public GameObject SpecialUI;



	public static Manager instance;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
    {
		Instantiate(playerLevelsPrefabs[GameLevel], new Vector3(0, playerLevelsPrefabs[GameLevel].GetComponent<PlayerBase>().PositionY, 0), Quaternion.identity);

		GameObject obj = GameObject.FindGameObjectWithTag("Spawner");
		if (obj != null) houseSpawner = obj.GetComponent<HouseSpawner>();

		obj = GameObject.FindGameObjectWithTag("Player");
		if (obj != null) player = obj.GetComponent<PlayerBase>();
		

		MenuPause.SetActive(false);
		MenuGameOver.SetActive(false);

		PointsUI.text = $"Points : 0";
		NuclearPointsUI.text = $"Points to Nuclear : {pointsToLvlUp}";
		if (GameLevel < 2) SpecialUI.SetActive(false);
	}

    void Update()
    {
		pauseMenu();
		canLevelUp();
    }

	void pauseMenu()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;
		}

		if (paused)
		{
			Time.timeScale = 0;
			MenuPause.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			MenuPause.SetActive(false);
		}
	}
	public void pauseMenu2()
	{
		paused = !paused;

		if (paused)
		{
			Time.timeScale = 0;
			MenuPause.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			MenuPause.SetActive(false);
		}
	}

	#region HouseSpawner

	public void NuclearPicked()
	{
		houseSpawner.nuclearSpawned = false;
	}
	public void HousesCount(int count)
	{
		totalHouses += count;

		if (totalHouses < maxHouses)
		{
			houseSpawner.canSpawn = true;
		}
		else
		{
			houseSpawner.canSpawn = false;
		}
	}
	public void AddHouseDestroyed()
	{
		if (HousesDestroyed < pointsToLvlUp)
		{
			HousesDestroyed++;
			PointsUI.text = $"Points : {HousesDestroyed}";
		}
		else
		{
			if (!houseSpawner.nuclearSpawned)
			{
				houseSpawner.spawnNuclear = true;
			}
		}
	}
	#endregion

	#region level
	public void LevelUp() //cambiar para que spawnee el nuevo personaje segun el nivel y ponerle efecto de spawn
	{
		destroyLevelsUps();

		if (canLevelUpGame)
		{
			UpgradeStats();

			if (GameLevel >= 2)
				SpecialUI.SetActive(true);

			switch (GameLevel)
			{
				case 1:
					PlayerSpawn<Player1>();
					houseSpawner.LevelUp(GameLevel);
					break;
				case 2:
					PlayerSpawn<Player2>();
					houseSpawner.LevelUp(GameLevel);
					break;
				case 3:
					PlayerSpawn<Player3>();
					houseSpawner.LevelUp(GameLevel);
					break;
				case 4:
					PlayerSpawn<Player4>();
					houseSpawner.LevelUp(GameLevel);
					break;
				case 5:
					PlayerSpawn<Player5>();
					houseSpawner.LevelUp(GameLevel);
					break;
				default:
					player.TakeDamage(-player.maxLife / 5);
					break;
			}

            canLevelUpGame = false;

			PointsUI.text = $"Points : {HousesDestroyed}";
			NuclearPointsUI.text = $"Points to Nuclear : {pointsToLvlUp}";
		}
	}

	void UpgradeStats()
	{
		// sube de nivel
		if (GameLevel < 5) GameLevel++;
		pointsToLvlUp += pointsToAdd;
		pointsToAdd += 10;
		maxHouses += 10;
		houseSpawner.nuclearSpawned = false;
	}

	void destroyLevelsUps()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("LevelUp");
        foreach (GameObject item in objects)
        {
			Destroy(item);
        }
    }

	protected void canLevelUp()
	{
		if (!canLevelUpGame)
		{
			if (timerLevelUp < 2f)
			{
				timerLevelUp += Time.deltaTime;
			}
			else
			{
				timerLevelUp = 0f;
				canLevelUpGame = true;
			}
		}
	}
	#endregion

	void PlayerSpawn<T>() where T : PlayerBase
	{
		if (GameLevel >= 0 && GameLevel < playerLevelsPrefabs.Count)
		{
			GameObject newPlayer = Instantiate(playerLevelsPrefabs[GameLevel], player.transform.position, player.transform.rotation);
			Destroy(player.gameObject);
			player = newPlayer.GetComponent<T>();
			player.transform.position = new Vector3(player.transform.position.x, player.PositionY, player.transform.position.z);
			
		}
		else
		{
			Debug.LogError("error al cambiar al player");
		}
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
		foreach (Rigidbody rb in rigidbodies)
		{
			rb.isKinematic = true;
		}

		MenuGameOver.SetActive(true);
	}
}
