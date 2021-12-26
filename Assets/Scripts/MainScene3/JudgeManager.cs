using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// �V�[���؂�ւ��ɕK�v

public class JudgeManager : MonoBehaviour
{
    public GameObject text;
    public GameObject showObj_gameOver; //gameover

    // Start is called before the first frame update
    void Start()
    {
        showObj_gameOver.SetActive(false); // ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Judge()
    {
        if (text.GetComponent<Text>().text == MainScript.ansDay.ToString() + "��") //�����Ȃ�
        {

            //�����G�t�F�N�g
            Debug.Log("True");

            //��ʑJ��
            Invoke("GoToIntervalScene", 3.0f);//�b��ɌĂяo��

        }
        else
        {
            //�s�����G�t�F�N�g
            Debug.Log("False");

            MainScript.lifeCount--;

            if (MainScript.lifeCount != 0)
            {
                Invoke("GoToIntervalScene", 3.0f);�@//�����Ă���b��ɌĂяo��
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 10f)); //�P�[���I�[�o�[
                Invoke("ShowRanking", 15f); //�����L���O���
            }

                
                                                            
        }

    }

    public void GoToIntervalScene()
    {
        SceneManager.LoadScene("IntervalScene"); //�V�[���ړ�
    }

    public IEnumerator ShowObject(GameObject gameObj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObj.SetActive(true); // �����Ă������̂�\������
    }

    public void ShowRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(MainScript.stageCount, 0);
    }

}
