using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownScript : MonoBehaviour
{
    public float timer = 4;

    // Start is called before the first frame update
    public Behaviour Board;
    public Behaviour Piece;
    public Behaviour ScoreValue;
    public Behaviour ScoreUI;

    //Audios
    public AudioSource Countdown3;
    public AudioSource Countdown2;
    public AudioSource Countdown1;

    public AudioSource Go;

    public bool IsStart = false;

    public bool C1 = false;
    public bool C2 = false;
    public bool C3 = false;

    public GameObject Row;
    public GameObject Combo;

    public GameObject SunsetImg;
    public GameObject FireImg;
    public GameObject DiscoImg;

    public TextMeshProUGUI Countdown;
    public GameObject CountdownVisibility;

    void Start()
    {
        CountdownVisibility.SetActive(true);

        Board.enabled = false;
        Piece.enabled = false;
        ScoreValue.enabled = false;
        ScoreUI.enabled = false;

        SunsetImg.SetActive(false);
        FireImg.SetActive(false);
        DiscoImg.SetActive(false);

        Row.SetActive(false);
        Combo.SetActive(false);

        int randomIndex = Random.Range(1, 3);

        if (randomIndex == 1)
        {
            SunsetImg.SetActive(true);
            FireImg.SetActive(false);
            DiscoImg.SetActive(false);
        }
        else if (randomIndex == 2)
        {
            SunsetImg.SetActive(true);
            FireImg.SetActive(false);
            DiscoImg.SetActive(false);
        }
        else if (randomIndex == 3) {
            SunsetImg.SetActive(true);
            FireImg.SetActive(false);
            DiscoImg.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            if (timer >= 3 && C3 == false)
            {
                C3 = true;
                Countdown3.Play();
                Countdown.text = "3";
            } else if (timer >= 2 && C2 == false && timer < 3)
            {
                C2 = true;
                Countdown2.Play();
                Countdown.text = "2";
            }
            else if (timer >= 1 && C1 == false && timer < 2)
            {
                C1 = true;
                Countdown1.Play();
                Countdown.text = "1";
            }

            timer -= Time.deltaTime;

        } 
        
        
        else if (timer <= 0 && IsStart == false)
        {
           
            IsStart = true;
            Go.Play();
            Countdown.text = "Go!";
            Board.enabled = true;
            Piece.enabled = true;
            ScoreValue.enabled = true;
            ScoreUI.enabled = true;
            Invoke("HideCountdownText", .5f);
            
        }
    }

    public void HideCountdownText()
    {
        CountdownVisibility.SetActive(false);
    }
}
