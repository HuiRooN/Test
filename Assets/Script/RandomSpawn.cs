using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
	public GameObject heart;
	public GameObject coin;
	public GameObject enemy;

	// Start is called before the first frame update
	void Start()
    {
		InvokeRepeating("SpawnHeart", 1, 30);
		InvokeRepeating("SpawnCoin", 1, 8);
		InvokeRepeating("SpawnEnemy", 1, 10);
	}


    // Update is called once per frame
    void Update()
    {
        
    }

	void SpawnHeart()
	{
		float randomX = Random.Range(0f, 48f);
		float randomZ = Random.Range(0f, 48f);
		GameObject Heart = (GameObject)Instantiate(heart, new Vector3(randomX, 1f, randomZ), Quaternion.identity);
	}

	void SpawnCoin()
	{
		float randomX2 = Random.Range(0f, 48f);
		float randomZ2 = Random.Range(0f, 48f);
		GameObject Coin = (GameObject)Instantiate(coin, new Vector3(randomX2, 0f, randomZ2), Quaternion.identity);
	}

	void SpawnEnemy()
	{
		float randomX3 = Random.Range(0f, 48f);
		float randomZ3 = Random.Range(0f, 48f);
		GameObject Enemy = (GameObject)Instantiate(enemy, new Vector3(randomX3, 0f, randomZ3), Quaternion.identity);
	}
}
