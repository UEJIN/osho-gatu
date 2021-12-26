using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// �V�[���؂�ւ��ɕK�v

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
        text_stageCounter.GetComponent<Text>().text = "�� " + MainScript.stageCount.ToString() + " ��";
        text_lifeCounter.GetComponent<Text>().text = "��: " + MainScript.lifeCount.ToString();
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene3"); //�V�[���ړ�
    }
}
