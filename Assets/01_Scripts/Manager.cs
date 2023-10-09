using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public int GameLevel = 0;

    public int maxHouses = 20;
	public int totalHouses;
    HouseSpawner houseSpawner;
	PlayerBase player;
	Camera Usedcamera;

	public int HousesDestroyed = 0;
	public int pointsToLvlUp = 5;

	public List<GameObject> playerLevelsPrefabs;

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

		obj = GameObject.FindGameObjectWithTag("Camera");
		if (obj != null) Usedcamera = obj.GetComponent<Camera>();
	}

    void Update()
    {

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
			//killedEnemiesText.text = totalKilledEnemies.ToString(); // interfaz
		}
		else
		{
			houseSpawner.spawnNuclear = true;
		}
	}
	#endregion

	#region level
	public void LevelUp() //cambiar para que spawnee el nuevo personaje segun el nivel y ponerle efecto de spawn
	{
		// sube de nivel
		if (GameLevel < 5) GameLevel++;
		pointsToLvlUp += pointsToLvlUp * 2;
		maxHouses += 10;
		houseSpawner.nuclearSpawned = false;

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
		}
		Usedcamera.updatePlayer(player);
	}
	#endregion

	void PlayerSpawn<T>() where T : PlayerBase
	{
		if (GameLevel >= 0 && GameLevel < playerLevelsPrefabs.Count)
		{
			GameObject newPlayer = Instantiate(playerLevelsPrefabs[GameLevel], player.transform.position, player.transform.rotation);
			Debug.Log(player.transform.position);
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
		Debug.Log("falta detener el juego al morir");
		// sera que necesita un if para que sea inmune mientras cambia de player?
	}
}
