using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;


public class ScoreUI : MonoBehaviour
{

    [SerializeField] public ScoreValue ScoreScript;
    //public Canvas Score;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LinesText;
    public TextMeshProUGUI LevelText;


    // Start is called before the first frame update
    public void Start()
    {
        //Score = GetComponent<Canvas>();
      
        ScoreText.text = "Score: " + ScoreScript.CurrentScore;
        LinesText.text = "Lines: " + ScoreScript.Lines;
        LevelText.text = "Level: " + ScoreScript.Level;
    }

    // Update is called once per frame
    public void Update()
    {
        
        UpdateScoreUI();  
    }
    public void UpdateScoreUI()
    {
        
        ScoreText.text = "Score: " + ScoreScript.CurrentScore;
        LinesText.text = "Lines: " + ScoreScript.Lines;
        LevelText.text = "Level: " + ScoreScript.Level;
    }
}
