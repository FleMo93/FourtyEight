using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_ButtonFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BtnClick_LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void BtnClick_QuitGame()
    {
        Application.Quit();
    }
    public void BtnClick_DisableGameObject(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
    }
    public void BtnClick_EnableGameObject(GameObject _gameObject)
    {
        _gameObject.SetActive(true);
    }
    public void BtnClick_ResetGame(so_DataSetGlobal DataSet)
    {
        DataSet.ResetStats();
    }
}
