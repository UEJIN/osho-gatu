using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// �V�[���؂�ւ��ɕK�v

public class JudgeManager : MonoBehaviour
{
    public GameObject text;
    public GameObject showObj_gameOver; //gameover
    public GameObject showObj_ok; //gameover
    public GameObject showObj_out; //gameover

    public AudioSource audioSource_OkOut;
    public AudioClip sound_ok;
    public AudioClip sound_out;
    public static bool isOut;
    static bool isFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        showObj_gameOver.SetActive(false); // ����
        showObj_ok.SetActive(false); // ����
        showObj_out.SetActive(false); // ����
    }

    // Update is called once per frame
    void Update()
    {
        if(isOut&& !isFinish)
        {
            isOut = false;
            isFinish = true;
            //�s�����G�t�F�N�g
            Debug.Log("False");
            StartCoroutine(ShowObject(showObj_out, 0f));
            audioSource_OkOut.PlayOneShot(sound_out);

            MainScript.lifeCount--;

            if (MainScript.lifeCount > 0)
            {
                Invoke("GoToIntervalScene", 2.0f);�@//�����Ă���b��ɌĂяo��
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 2f)); //�P�[���I�[�o�[
                Invoke("ShowRanking", 3.5f); //�����L���O���
            }
        }
    }

    public void Judge()
    {
        if (text.GetComponent<Text>().text == MainScript.ansDay.ToString() + "��") //�����Ȃ�
        {
            isFinish = true;
            //�����G�t�F�N�g
            Debug.Log("True");
            StartCoroutine(ShowObject(showObj_ok, 0f));
            audioSource_OkOut.PlayOneShot(sound_ok);

            //��ʑJ��
            Invoke("GoToIntervalScene", 2.0f);//�b��ɌĂяo��

        }
        else
        {
            isFinish = true;
            //�s�����G�t�F�N�g
            Debug.Log("False");
            StartCoroutine(ShowObject(showObj_out, 0f));
            audioSource_OkOut.PlayOneShot(sound_out);

            MainScript.lifeCount--;

            if (MainScript.lifeCount > 0)
            {
                Invoke("GoToIntervalScene", 2.0f);�@//�����Ă���b��ɌĂяo��
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 2f)); //�P�[���I�[�o�[
                Invoke("ShowRanking", 3.5f); //�����L���O���
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
