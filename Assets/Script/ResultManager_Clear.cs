using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager_Clear : MonoBehaviour
{
	public GameObject GameClearPanel;

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		
	}

	public void GameRestart()
	{
		SceneManager.LoadScene("Main");
		Time.timeScale = 1;
	}

	public void GameMain()
	{
		SceneManager.LoadScene("Title");
	}
}
