using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

public class StartManager : MonoBehaviour
{
    public GameObject showObj_HowTo;

    // Start is called before the first frame update
    void Start()
    {
        MainScript.stageCount = 0;
        MainScript.lifeCount = 3;
        showObj_HowTo.SetActive(false); // 消す
        MainScript.timeSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick_GameStart()
    {
            SceneManager.LoadScene("IntervalScene"); //シーン移動

    }
    
    public void OnClick_HowTo()
    {
        showObj_HowTo.SetActive(true);

    }

    public void OnClick_Back()
    {
        showObj_HowTo.SetActive(false);
    }
}
