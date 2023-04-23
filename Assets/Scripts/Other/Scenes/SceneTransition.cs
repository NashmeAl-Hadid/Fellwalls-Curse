using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;

    public int alive;
    public float health;
    public string characterName;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(LoadSceneNow());
            Debug.Log("DataSaved");
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("DataLoaded");
            LoadData();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    StartCoroutine(LoadSceneNow());
        //    Debug.Log("DataSaved");
        //    SaveData();
        //}
    }

    IEnumerator LoadSceneNow()
    {     
        transitionAnim.Play("start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("inta", alive);
        PlayerPrefs.SetFloat("float", health);
        PlayerPrefs.SetString("string", characterName);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        alive = PlayerPrefs.GetInt("inta");
        health = PlayerPrefs.GetFloat("float");
        characterName = PlayerPrefs.GetString("string");
    }
}
