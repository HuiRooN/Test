using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
	public GameObject TitlePanel;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GameStart()
	{
		SceneManager.LoadScene("Main");
	}

	public void Explain()
	{
		SceneManager.LoadScene("Explain");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
