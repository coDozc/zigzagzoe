using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public TMP_Text HiScoreTxt;
    public TMP_Text ScoreTxt;

    public int score { get; set; }
    public int HScore { get; set; } 
    void Start()
    {
        LoadHighScore();    
    }

    void LoadHighScore() 
    {
        HScore = PlayerPrefs.GetInt("Hiscore");
        HiScoreTxt.text = HScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
