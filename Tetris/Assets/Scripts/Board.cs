using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using TMPro;

public class Board : MonoBehaviour
{
    [SerializeField] public ScoreValue ScoreScript;
    [SerializeField] public Piece PieceScript;
    [SerializeField] public CountdownScript CountdownScript;

    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominoes;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    //public Vector3Int previewNextTetromino = new Vector3Int(11,5,0);

    public int scoreOneLines = 100;
    public int scoreTwoLines = 400;
    public int scoreThreeLines = 1200;
    public int scoreFourLines = 2000;

    public int numberOfRowsThisTurn = 0;
    public bool IsStarted = false;

    public int streak = 0;
    public int streakCal = 0;
    public bool AddStreak = false;
    public bool BreakStreak = false;

    public float timer = 3;

    //Audios
    public AudioSource TetrisMusic1;
    public AudioSource TetrisMusic2;
    public AudioSource TetrisMusic3;
    public AudioSource TetrisMusic4;
    public AudioSource TetrisMusic5;

    public AudioSource ClearLine;

    public AudioSource Combo1;
    public AudioSource Combo2;
    public AudioSource Combo3;
    public AudioSource Combo4;
    public AudioSource Combo5;

    public AudioSource Countdown1;
    public AudioSource Countdown2;
    public AudioSource Countdown3;
    public AudioSource Go;

    //Streaks
    public bool Streak1S = false;
    public bool Streak2S = false;
    public bool Streak3S = false;
    public bool Streak4S = false;
    public bool Streak5S = false;

    public GameObject ComboTextVisibility;
    public GameObject RowTextVisibility;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI RowText;

   //public GameObject NextTetrominoShow;

    //Images
    public GameObject SunsetImg;
    public GameObject FireImg;
    public GameObject DiscoImg;

    public Vector3 desireScale = new Vector3(1,1,1);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominoes.Length; i++) { 
        
            this.tetrominoes[i].Initialize();
        }
    }

    public void Start()
    {
            int randomIndex = Random.Range(1, 5);

            if (randomIndex == 1)
            {
                TetrisMusic1.Play();
            }
            else if (randomIndex == 2)
            {

                TetrisMusic2.Play();
            }
            else if (randomIndex == 3)
            {

                TetrisMusic3.Play();
            }
            else if (randomIndex == 4)
            {

                TetrisMusic4.Play();
            }
            else
            {

                TetrisMusic5.Play();
            }

            SpawnNextTetromino();
            ScoreScript.CurrentScore = 0;
            ScoreScript.Lines = 0;
            ScoreScript.Level = 1;
            PieceScript.stepDelay = 1f;
        

    }
    
    public void Update()
    {
        //if (timer > 0)
        //{
            //timer -= Time.deltaTime;
            //if (timer == 3)
            //{
                //Countdown3.Play();
            //}
            //else if (timer == 2)
            //{
                //Countdown2.Play();
            //}
            //else if (timer == 1)
            //{
                //Countdown1.Play();
            //}
        //} else if (timer < 0 && IsStarted == false)
        //{
            //IsStarted = true;
            //Go.Play();
            
        //} 
    }

    public void SpawnPiece(int random, int randomNextTetromino)
    {
        //int random = Random.Range(0, this.tetrominoes.Length);
        
        random = randomNextTetromino;
        TetrominoData data = this.tetrominoes[random]; 

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        } else
        {
            
            Invoke("GameOver", 0);
        }

        Set(this.activePiece);

    }

    public void SpawnNextTetromino()
    {
        int random = Random.Range(0, this.tetrominoes.Length);
        int randomNextTetromino = Random.Range(0, this.tetrominoes.Length);

        SpawnPiece(random, randomNextTetromino);

        if (randomNextTetromino == 0) 
        {
            //NextTetrominoShow =  Instantiate(Tetromino.I, new Vector3(11,5,0), Quaternion.identity);
            //NextTetrominoShow.GetComponent<Tetromino>.enabled = false;
        } else if (randomNextTetromino == 1)
        {

        }
        else if (randomNextTetromino == 2)
        {

        }
        else if (randomNextTetromino == 3)
        {

        }
        else if (randomNextTetromino == 4)
        {

        }
        else if (randomNextTetromino == 5)
        {

        }
        else if (randomNextTetromino == 6)
        {

        }



    }

    public void GameOver()
    {
        this.tilemap.ClearAllTiles();
        ScoreScript.CurrentScore = 0;
        ScoreScript.Lines = 0;
        ScoreScript.Level = 1;
        numberOfRowsThisTurn = 0;
        PieceScript.stepDelay = 1f;

    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }

    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
            
        }

    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {

                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {

                return false;
            }
        }
        
        return true;
    } 

    public void ClearLines()
    {

        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

           
        while (row < bounds.yMax)
        {
            
             if (IsLineFull(row)) {
                numberOfRowsThisTurn += 1;
                
                LineClear(row);
                ClearLine.Play();
                UpdateScore(numberOfRowsThisTurn);
                
             } else {
                row++;
                
            }

            if (numberOfRowsThisTurn > 0)
            {
                //Increment the combo run.
                AddStreak = true;
                //Then do the combo display.
            }
            else if (numberOfRowsThisTurn == 0)
            {
                //if not full rows, this will reset the combo run.
                
                
                AddStreak = false;
            }
            
        }

        if (AddStreak == true)
        {
            
            streakCal+=21;
            UpdateStreak(streakCal);
            UpdateLevel(ScoreScript.Lines);
            UpdateRowText(numberOfRowsThisTurn); 
            
        } else if (AddStreak == false)
        {
            streakCal = 0;
            streak = 0;
        }


        numberOfRowsThisTurn = 0;
        AddStreak = false;


    }

    private void UpdateStreak(int comboCal)
    {
        
        if (comboCal >= 10)
        {
            

            if (comboCal >= 42 && comboCal<63)
            {
                Streak1S = true;
                Streak1(Streak1S);
                
            }
            else if (comboCal >= 63 && comboCal < 84)
            {
                Streak2S = true;
                Streak1S = false;
                Streak2(Streak2S);
                
                
            }
            else if (comboCal >= 84 && comboCal < 105)
            {
                Streak3S = true;
                Streak2S = false;
                Streak1S = false;
                Streak3(Streak3S);
                
            }
            else if (comboCal >= 105 && comboCal < 126)
            {
                Streak4S = true;
                Streak3S = false;
                Streak2S = false;
                Streak1S = false;
                Streak4(Streak4S);
                
            }
            else if (comboCal >= 126)
            {
                Streak5S = true;
                Streak4S = false;
                Streak3S = false;
                Streak2S = false;
                Streak1S = false;
                Streak5(Streak5S);
                
            }
        }
    }

    public void UpdateRowText(int row)
    {
        if (row ==1)
        {
            RowText.text = "Single";
            RowTextVisibility.SetActive(true);
            LeanTween.scaleX(RowTextVisibility, 1.25f, .75f);

            //LeanTween.textAlpha(RowText.rectTransform, 100, .75f).setEase(LeanTweenType.easeInOutSine);
            Invoke("HideRowTextAfterDelay", 0.5f);

        } else if (row == 2) 
        {
            RowText.text = "Double";
            RowTextVisibility.SetActive(true);
            LeanTween.scaleX(RowTextVisibility, 1.25f, .75f);
            Invoke("HideRowTextAfterDelay", 0.75f);
        }
        else if (row == 3)
        {
            RowText.text = "Triple";
            RowTextVisibility.SetActive(true);
            LeanTween.scaleX(RowTextVisibility, 1.25f, .75f);
            Invoke("HideRowTextAfterDelay", 0.75f);
        }
        else if (row == 4)
        {
            RowText.text = "Quadraple";
            RowTextVisibility.SetActive(true);
            LeanTween.scaleX(RowTextVisibility, 1.25f, .75f);
            Invoke("HideRowTextAfterDelay", 0.75f);
        }
    }

    public void Streak1(bool st)
    {
        if (st == true)
        {

            streak += 1;
            Combo1.Play();
            AddStreak = false;
            Text.text = streak + " Combo!";
            ComboTextVisibility.SetActive(true);
            Invoke("HideComboTextAfterDelay", 0.75f);
        }
    }

    public void Streak2(bool st)
    {
        if (st == true)
        {
            streak += 1;
            Combo2.Play();
            AddStreak = false;
            Text.text = streak + " Combo!";
            ComboTextVisibility.SetActive(true);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -125f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -175f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            Invoke("HideComboTextAfterDelay", 0.75f);
        }
    }

    public void Streak3(bool st)
    {
        if (st == true)
        {
            streak += 1;
            Combo3.Play();
            AddStreak = false;
            Text.text = streak + " Combo!";
            ComboTextVisibility.SetActive(true);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -125f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -175f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            Invoke("HideComboTextAfterDelay", 0.75f);
        }

    }

    public void Streak4(bool st)
    {
        if (st == true)
        {
            streak += 1;
            Combo4.Play();
            AddStreak = false;
            Text.text = streak + " Combo!";
            ComboTextVisibility.SetActive(true);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -125f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -175f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            Invoke("HideComboTextAfterDelay", 0.75f);
        }

    }

    public void Streak5(bool st)
    {
        if (st == true)
        {
            streak += 1;
            Combo5.Play();
            AddStreak = false;
            Text.text = streak + " Combo!";
            ComboTextVisibility.SetActive(true);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -125f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);
            //LeanTween.moveLocal(ComboTextVisibility, new Vector3(-400f, -175f, 0f), 0.2f).setEase(LeanTweenType.easeInOutCubic);

            Invoke("HideComboTextAfterDelay", 0.75f);
        }
    }

    public void HideComboTextAfterDelay()
    {

        // Hide ComboText
        ComboTextVisibility.SetActive(false);
    }

    public void HideRowTextAfterDelay()
    {

        // Hide ComboText
        RowTextVisibility.SetActive(false);
        RowText.transform.localScale = desireScale;
        LeanTween.scaleX(RowTextVisibility, 1, 0f);
        LeanTween.alpha(RowTextVisibility, 255, 0f);
    }

    private void UpdateScore(int row)
    {
        if (row == 1) {
            clearOneLine();     
        }

        if (row == 2){
            clearTwoLine();
        }

        if (row == 3)
        {
            clearThreeLine();

        }

        if (row == 4)
        {
            clearFourLine();
        }
        
    }

    public void clearOneLine()
    {
        ScoreScript.CurrentScore += scoreOneLines;
        ScoreScript.Lines += 1;
        
    }

    public void clearTwoLine()
    {
        ScoreScript.CurrentScore += scoreTwoLines;
        ScoreScript.CurrentScore -= scoreOneLines;
        ScoreScript.Lines += 2-1;

    }

    public void clearThreeLine()
    {
        ScoreScript.CurrentScore += scoreThreeLines;
        ScoreScript.CurrentScore -= scoreOneLines+scoreTwoLines;
        ScoreScript.Lines += 3-3;

    }

    public void clearFourLine()
    {
        ScoreScript.CurrentScore += scoreFourLines;
        ScoreScript.CurrentScore -= scoreOneLines + scoreTwoLines+scoreThreeLines;
        ScoreScript.Lines += 4-6;

    }

    private void UpdateLevel(int lines)
    {
        if (lines >= 10 && lines < 20)
        {
            ScoreScript.Level = 2;
            PieceScript.stepDelay = 0.9f;
        }
        else if (lines >= 20 && lines < 30)
        {
            ScoreScript.Level = 3;
            PieceScript.stepDelay = 0.8f;
        }
        else if (lines >= 30 && lines < 40)
        {
            ScoreScript.Level = 4;
            PieceScript.stepDelay = 0.7f;
        }
        else if (lines >= 30 && lines < 40)
        {
            ScoreScript.Level = 5;
            PieceScript.stepDelay = 0.6f;
        }
        else if (lines >= 40 && lines < 50)
        {
            ScoreScript.Level = 6;
            PieceScript.stepDelay = 0.5f;
        }
        else if (lines >= 50 && lines < 60)
        {
            ScoreScript.Level = 7;
            PieceScript.stepDelay = 0.4f;
        }
        else if (lines >= 60 && lines < 70)
        {
            ScoreScript.Level = 8;
            PieceScript.stepDelay = 0.3f;
        }
        else if (lines >= 70 && lines < 80)
        {
            ScoreScript.Level = 8;
            PieceScript.stepDelay = 0.2f;
        }
        else if (lines >= 80 && lines < 90)
        {
            ScoreScript.Level = 9;
            PieceScript.stepDelay = 0.1f;
        } else if (lines >= 90)
        {
            ScoreScript.Level = 10;
            PieceScript.stepDelay = 0.05f;
        }
    } 

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        
        }

        while (row < bounds.yMax)
        {
            

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
                
            }


            row++;
        }
    }
}
