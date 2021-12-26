using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// シーン切り替えに必要

public class MainScript : MonoBehaviour
{
    public int year;
    public int month;
    public int day;
    public static int ansDay;
    bool leapYear;

    public int notAnsDay1;
    public int notAnsDay2;
    public int notAnsDay3;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject yearMonth;
    public GameObject today;
    public GameObject text_stageCounter;
    public GameObject text_lifeCounter;
    public GameObject text_timeCounter;

    bool isFinish;

    public static int stageCount;
    public static int lifeCount;
    public static float timeSpeed;

    public AudioSource mainAudioSourse;

    int remainTime;


    // Start is called before the first frame update
    void Start()
    {
        mainAudioSourse.pitch = timeSpeed;


        TodaySet();

        Debug.Log("★Today="+year.ToString() +"/"+ month.ToString()+"/"+day.ToString());
        Debug.Log("★LeapYear=" + leapYear);

        AnsCalc();

        Debug.Log("★ansDay="+ ansDay);
        
        //①正解
        //②正解 + -5日ランダム
        //③正解 + -10日ランダム
        //④正解 + -1日ランダム
        
        notAnsDay1 = ansDay + Random.Range(-1, 2);
        notAnsDay2 = ansDay + Random.Range(-5, 6);
        notAnsDay3 = ansDay + Random.Range(-10, 11);
        
        if(notAnsDay1 == ansDay)
        {
            notAnsDay1= notAnsDay1 + 11;
        }
        if (notAnsDay2 == ansDay)
        {
            notAnsDay2 = notAnsDay2 + 12;
        }
        if (notAnsDay3 == ansDay)
        {
            notAnsDay3 = notAnsDay3 + 13;
        }

        Debug.Log("ansDay=" + ansDay);
        Debug.Log("notAnsDay1=" + notAnsDay1);
        Debug.Log("notAnsDay2=" + notAnsDay2);
        Debug.Log("notAnsDay3=" + notAnsDay3);

        TextApply();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("★time"+mainAudioSourse.time);

        if (remainTime >= 0)
        {
            remainTime = 34 - (int)mainAudioSourse.time;
        }
        else
        {
            remainTime = 0;
        }

        
        if (remainTime >= 0)
        {
            text_timeCounter.GetComponent<Text>().text = "残り時間: " + remainTime.ToString();
            remainTime = 34 - (int)mainAudioSourse.time;
        }
        else
        {
            text_timeCounter.GetComponent<Text>().text = "残り時間: " + 0; 
        }

        if (mainAudioSourse.time > 34 && !isFinish)
        {
            isFinish = true;
            Debug.Log("★Fin");

            //不正解エフェクト
            JudgeManager.isOut=true;

        }

    }

    void OnDestroy()
    {
        if (stageCount % 5 == 0)
        {
            timeSpeed = timeSpeed + 0.25f;
        }

    }


    // 引数として受け取った配列の要素番号を並び替える 
    void Shuffle(int[] num)
    {
        for (int i = 0; i < num.Length; i++)
        {
            //（説明１）現在の要素を預けておく
            int temp = num[i];
            //（説明２）入れ替える先をランダムに選ぶ
            int randomIndex = Random.Range(0, num.Length);
            //（説明３）現在の要素に上書き
            num[i] = num[randomIndex];
            //（説明４）入れ替え元に預けておいた要素を与える
            num[randomIndex] = temp;
        }
    }

    void TodaySet()
    {
        //        現在の年月日決定
        year = Random.Range(0, 10) * 1000 + Random.Range(0, 10) * 100 + Random.Range(0, 10) * 10 + Random.Range(0, 10) * 1;
        month = Random.Range(1, 13);

        if (month == 2)
        {
            if (year % 4 == 0)        //閏年判定
            {
                if (year % 100 == 0 && year % 400 != 0)
                {
                    leapYear = false;//うるう年以外
                }
                else
                {
                    leapYear = true;//うるう年
                }
            }
            else
            {
                leapYear = false;//うるう年以外
            }

            if (leapYear)//うるう年考慮した日数
            {
                day = Random.Range(1, 30);
            }
            else
            {
                day = Random.Range(1, 29);
            }
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            day = Random.Range(1, 31);
        }
        else
        {
            day = Random.Range(1, 32);
        }
    }

    void AnsCalc()
    {
        //正月までの日数確認
        ansDay = (12 - month) * 30 + (30 - day) + 1;

        if (leapYear && month <= 2)//うるう年またぐなら
        {
            ansDay = ansDay - 1;
        }
        else if (leapYear == false && month <= 2)//うるう年じゃない２月またぐなら
        {
            ansDay = ansDay - 2;
        }

        if (month == 1)//31日処理
        {
            ansDay = ansDay + 7;
        }
        else if (month <= 3)
        {
            ansDay = ansDay + 6;
        }
        else if (month <= 5)
        {
            ansDay = ansDay + 5;
        }
        else if (month <= 7)
        {
            ansDay = ansDay + 4;
        }
        else if (month <= 8)
        {
            ansDay = ansDay + 3;
        }
        else if (month <= 10)
        {
            ansDay = ansDay + 2;
        }
        else if (month <= 12)
        {
            ansDay = ansDay + 1;
        }

    }

    void TextApply()
    {
        int[] days = new int[4];
        days[0] = ansDay;
        days[1] = notAnsDay1;
        days[2] = notAnsDay2;
        days[3] = notAnsDay3;

        Shuffle(days);

        text1.GetComponent<Text>().text = days[0].ToString() + "日";
        text2.GetComponent<Text>().text = days[1].ToString() + "日";
        text3.GetComponent<Text>().text = days[2].ToString() + "日";
        text4.GetComponent<Text>().text = days[3].ToString() + "日";
        yearMonth.GetComponent<Text>().text = year.ToString() + "年" + month.ToString() + "月";
        today.GetComponent<Text>().text = day.ToString();
        text_stageCounter.GetComponent<Text>().text = "第 " + stageCount.ToString() + " 問目";
        text_lifeCounter.GetComponent<Text>().text = "命: " + lifeCount.ToString();

    }

    public void GoToStart()
    {

        SceneManager.LoadScene("TitleScene"); //シーン移動
    }

}
