using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoardManager : MonoBehaviour
{

    public int gameScore = 100;
    public int rankBoardNum = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        //�Đ����̋Ȃ�bgmIndex�ɑΉ����郉���L���O�{�[�h��\��
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(gameScore, rankBoardNum);

    }
}
