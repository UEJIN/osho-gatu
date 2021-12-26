using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

public class IntervalManager : MonoBehaviour
{

    public GameObject text_stageCounter;
    public GameObject text_lifeCounter;
    public AudioSource mainAudioSourse;

    // Start is called before the first frame update
    void Start()
    {
        MainScript.stageCount++;
        TextApply();
        Invoke("GoToMainScene", 3f);
        Time.timeScale = MainScript.timeSpeed;
        mainAudioSourse.pitch = MainScript.timeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void TextApply()
    {
        text_stageCounter.GetComponent<Text>().text = "第 " + MainScript.stageCount.ToString() + " 問";
        text_lifeCounter.GetComponent<Text>().text = "命: " + MainScript.lifeCount.ToString();
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene3"); //シーン移動
    }
}
