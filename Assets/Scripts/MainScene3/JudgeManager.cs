using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

public class JudgeManager : MonoBehaviour
{
    public GameObject text;
    public GameObject showObj_gameOver; //gameover

    // Start is called before the first frame update
    void Start()
    {
        showObj_gameOver.SetActive(false); // 消す
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Judge()
    {
        if (text.GetComponent<Text>().text == MainScript.ansDay.ToString() + "日") //正解なら
        {

            //正解エフェクト
            Debug.Log("True");

            //画面遷移
            Invoke("GoToIntervalScene", 3.0f);//秒後に呼び出す

        }
        else
        {
            //不正解エフェクト
            Debug.Log("False");

            MainScript.lifeCount--;

            if (MainScript.lifeCount != 0)
            {
                Invoke("GoToIntervalScene", 3.0f);　//生きてたら秒後に呼び出す
            }
            else
            {
                StartCoroutine(ShowObject(showObj_gameOver, 10f)); //ケームオーバー
                Invoke("ShowRanking", 15f); //ランキング画面
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
