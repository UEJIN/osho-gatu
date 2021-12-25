using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public int year;
    public int month;
    public int day;
    public int ansDay;
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


    // Start is called before the first frame update
    void Start()
    {
        //        ���݂̔N��������
        year = Random.Range(0, 10) * 1000 + Random.Range(0, 10) * 100 + Random.Range(0, 10) * 10 + Random.Range(0, 10) * 1;
        month = Random.Range(1, 13);

        if (month == 2)
        {
            if (year%4 == 0)        //�[�N����
            {
                if (year % 100 == 0 && year % 400 != 0)
                {
                    leapYear = false;//���邤�N�ȊO
                }
                else
                {
                    leapYear = true;//���邤�N
                }
            }
            else
            {
                leapYear = false;//���邤�N�ȊO
            }

            if (leapYear)//���邤�N�l����������
            {
                day = Random.Range(1, 30);
            }
            else
            {
                day = Random.Range(1, 29);
            }
        }
        else if(month == 4 || month == 6 || month == 9 || month == 11)
        {
            day = Random.Range(1, 31);
        }
        else
        {
            day = Random.Range(1, 32);
        }
        Debug.Log("��Today="+year.ToString() +"/"+ month.ToString()+"/"+day.ToString());
        Debug.Log("��LeapYear=" + leapYear);

        //�����܂ł̓����m�F
        ansDay = (12 - month) * 30 + (30 - day) + 1;

        if (leapYear && month <= 2)//���邤�N�܂����Ȃ�
        {
            ansDay = ansDay - 1;
        }
        else if (leapYear==false && month <= 2)//���邤�N����Ȃ��Q���܂����Ȃ�
        {
            ansDay = ansDay - 2;
        }

        if (month == 1)//31������
        {
            ansDay = ansDay + 7;
        }
        else if (month<=3)
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
        
        Debug.Log("��ansDay="+ ansDay);
        //�@����
        //�A���� + -5�������_��
        //�B���� + -10�������_��
        //�C���� + -1�������_��
        //  �������בւ�

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



        text1.GetComponent<Text>().text = ansDay.ToString() + "��";
        text2.GetComponent<Text>().text = notAnsDay1.ToString()�@+ "��";
        text3.GetComponent<Text>().text = notAnsDay2.ToString() + "��";
        text4.GetComponent<Text>().text = notAnsDay3.ToString() + "��";
        yearMonth.GetComponent<Text>().text = year.ToString() + "/" + month.ToString();
        today.GetComponent<Text>().text = day.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
