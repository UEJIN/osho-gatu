using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// �V�[���؂�ւ��ɕK�v

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainScript.stageCount = 0;
        MainScript.lifeCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick_GameStart()
    {
            SceneManager.LoadScene("IntervalScene"); //�V�[���ړ�

    }

}
