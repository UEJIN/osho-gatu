using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

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
        showObj_gameOver.SetActive(false); // 消す
        showObj_ok.SetActive(false); // 消す
        showObj_out.SetActive(false); // 消す
    }

    // Update is called once per frame
    void Update()
    {
        if(isOut&& !isFinish)
        {
            isOut = false;
            isFinish = true;
            //不正解エフェクト
            Debug.Log("False");
            StartCoroutine(ShowObject(showObj_out, 0f));
            audioSource_OkOut.PlayOneShot(sound_out);

            MainScript.lifeCount--;

            if (MainScript.lifeCount > 0)
            {
                Invoke("GoToIntervalScene", 2.0f);　//生きてたら秒後に呼び出す
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 2f)); //ケームオーバー
                Invoke("ShowRanking", 3.5f); //ランキング画面
            }
        }
    }

    public void Judge()
    {
        if (text.GetComponent<Text>().text == MainScript.ansDay.ToString() + "日") //正解なら
        {
            isFinish = true;
            //正解エフェクト
            Debug.Log("True");
            StartCoroutine(ShowObject(showObj_ok, 0f));
            audioSource_OkOut.PlayOneShot(sound_ok);

            //画面遷移
            Invoke("GoToIntervalScene", 2.0f);//秒後に呼び出す

        }
        else
        {
            isFinish = true;
            //不正解エフェクト
            Debug.Log("False");
            StartCoroutine(ShowObject(showObj_out, 0f));
            audioSource_OkOut.PlayOneShot(sound_out);

            MainScript.lifeCount--;

            if (MainScript.lifeCount > 0)
            {
                Invoke("GoToIntervalScene", 2.0f);　//生きてたら秒後に呼び出す
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 2f)); //ケームオーバー
                Invoke("ShowRanking", 3.5f); //ランキング画面
            }

                
                                                            
        }

    }

    public void GoToIntervalScene()
    {
        SceneManager.LoadScene("IntervalScene"); //シーン移動
    }

    public IEnumerator ShowObject(GameObject gameObj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObj.SetActive(true); // 消していたものを表示する
    }

    public void ShowRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(MainScript.stageCount, 0);
    }

}
