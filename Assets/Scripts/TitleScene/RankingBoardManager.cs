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
        //再生中の曲のbgmIndexに対応するランキングボードを表示
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(gameScore, rankBoardNum);

    }
}
