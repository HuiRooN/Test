using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject gameCam;
	public GameObject GamePanel;
	public GameObject PausePanel;

	public Text GPplayTimeTxt;
	public Text playerHeartTxt;
	public Text playerCoinTxt;

	public Player player;
	public Enemy enemy;

	public float playTime;

	public bool isPlay = true;
	
	// Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		if (isPlay)
			playTime += Time.deltaTime;

		if (player.heart <= 0)
			GameOver();

		if (player.isClear)
			GameClear();
    }

	public void GameOver()
	{
		Time.timeScale = 0;
		SceneManager.LoadScene("Result_Fail");
		isPlay = false;
	}

	public void GamePause()
	{
		PausePanel.SetActive(true);
		GamePanel.SetActive(false);
		Time.timeScale = 0;
	}

	public void GameResume()
	{
		PausePanel.SetActive(false);
		GamePanel.SetActive(true);
		Time.timeScale = 1;
	}

	public void GameRestart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Main");
	}

	public void GameMain()
	{
		SceneManager.LoadScene("Title");
	}

	public void GameClear()
	{
		Time.timeScale = 0;
		isPlay = false;
		SceneManager.LoadScene("Result_Clear");
	}

	void LateUpdate()
	{
		int hour = (int)(playTime / 3600);
		int min = (int)((playTime - hour * 3600) / 60);
		int second = (int)(playTime % 60);

		GPplayTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
		playerHeartTxt.text = player.heart + " / " + player.maxHeart;
		playerCoinTxt.text = player.coin + " / " + player.maxCoin;
	}
}
