using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject bgTilePrefab;

    // Board
    public int width, height;
    public float borderSize;
    public enum BoardState { wait, move }
    public BoardState currentState = BoardState.move;
    public MatchFinder matchFind;
    private BoardLayout boardLayout;
    private Gem[,] layoutStore;

    // Gem 
    public Gem[] gems;
    public Gem[,] allGems;
    public Gem bomb;
    public float gemSpeed;
    public float bombChance = 2f;

    // Round Manager
    public RoundManager roundMan;
    private float bonusMulti;
    public float bonusAmount = .5f;

    private void Awake()
    {
        matchFind = FindObjectOfType<MatchFinder>();
        roundMan = FindObjectOfType<RoundManager>();
        boardLayout = GetComponent<BoardLayout>();
    }

    // Start is called before the first frame update
    void Start()
    {
        allGems = new Gem[width, height];
        layoutStore = new Gem[width, height];
        Setup();
        SetupCamera();
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float verticalSize = (float)height / 2f + borderSize;
        float horizontalSize = ((float)width / 2f + borderSize) / aspectRatio;
        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
    }

    private void Setup()
    {
        if (boardLayout != null)
        {
            layoutStore = boardLayout.GetLayout();
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile - " + x + ", " + y;

                if (layoutStore[x, y] != null)
                {
                    SpawnGem(new Vector2Int(x, y), layoutStore[x, y]);
                }
                else
                {

                    int gemToUse = Random.Range(0, gems.Length);

                    int iterations = 0;
                    while (MatchesAt(new Vector2Int(x, y), gems[gemToUse]) && iterations < 100)
                    {
                        gemToUse = Random.Range(0, gems.Length);
                        iterations++;
                    }

                    SpawnGem(new Vector2Int(x, y), gems[gemToUse]);
                }
            }
        }
    }

    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        if(Random.Range(0f, 100f) < bombChance)
        {
            gemToSpawn = bomb;
        }

        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, height, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
    }

    private IEnumerator SpawnGemWithAnimation(Vector2Int pos, Gem gemToSpawn)
    {
        if (Random.Range(0f, 100f) < bombChance)
        {
            gemToSpawn = bomb;
        }

        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, height - 0.5f, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);

        yield return new WaitForSeconds(0.02f); // Adjust the interval as needed

        matchFind.FindAllMatches();
    }

    bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck)
    {
        if(posToCheck.x > 1)
        {
            if (allGems[posToCheck.x - 1, posToCheck.y].type == gemToCheck.type &&
                allGems[posToCheck.x - 2, posToCheck.y].type == gemToCheck.type)
            {
                return true;
            }
        }

        if (posToCheck.y > 1)
        {
            if (allGems[posToCheck.x, posToCheck.y - 1].type == gemToCheck.type &&
                allGems[posToCheck.x, posToCheck.y - 2].type == gemToCheck.type)
            {
                return true;
            }
        }
        return false;
    }

    private void DestroyMatchedGemAt(Vector2Int pos) {
        if (allGems[pos.x, pos.y] != null)
        {
            if (allGems[pos.x, pos.y].isMatched)
            {
                Instantiate(allGems[pos.x, pos.y].destroyEffect, new Vector2(pos.x, pos.y), Quaternion.identity);

                Destroy(allGems[pos.x, pos.y].gameObject);
                allGems[pos.x, pos.y] = null; 
            }
        }
    }
    public void DestroyMatches() {
        for(int i = 0; i < matchFind.currentMatches.Count; i++)
        {
            if (matchFind.currentMatches[i] != null)
            {
                ScoreCheck(matchFind.currentMatches[i]);
                DestroyMatchedGemAt(matchFind.currentMatches[i].posIndex);
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        yield return new WaitForSeconds(.2f);

        int nullCounter = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    nullCounter++;
                } else if(nullCounter > 0)
                {
                    allGems[x, y].posIndex.y -= nullCounter;
                    allGems[x, y - nullCounter] = allGems[x, y];
                    allGems[x, y] = null;
                }
            }

            nullCounter = 0;
        }

        StartCoroutine(FillBoardCo());
    }

    private IEnumerator FillBoardCo()
    {
        yield return new WaitForSeconds(.1f);
        yield return StartCoroutine(RefillBoardWithAnimationCo());
        

        yield return new WaitForSeconds(.1f);

        matchFind.FindAllMatches();

        if(matchFind.currentMatches.Count > 0)
        {
            bonusMulti++;

            yield return new WaitForSeconds(.1f);
            DestroyMatches();
        } else
        {
            yield return new WaitForSeconds(.1f);
            currentState = BoardState.move;
            bonusMulti = 0f;
            ResultingScore();
        }
    }

    private IEnumerator RefillBoardWithAnimationCo()
    {
        List<Coroutine> destroyedCoroutines = new List<Coroutine>();

        for (int x = 0; x < width; x++)
        {
            
            yield return new WaitForSeconds(0.01f);
            destroyedCoroutines.Add(StartCoroutine(SpawnGemsInColumnWithAnimation(x)));

            //yield return StartCoroutine(SpawnGemWithAnimation(new Vector2Int(x, y), gems[gemToUse]));
            //SpawnGem(new Vector2Int(x, y), gems[gemToUse]);


        }

        foreach (Coroutine coroutine in destroyedCoroutines)
        {
            yield return coroutine;
        }

        CheckMisplacedGems();
    }

    private IEnumerator SpawnGemsInColumnWithAnimation(int column)
    {
        List<Coroutine> spawnCoroutines = new List<Coroutine>();
        for (int y = 0; y < height; y++)
        {
            if (allGems[column, y] == null)
            {
                int gemToUse = Random.Range(0, gems.Length);
                yield return new WaitForSeconds(0.15f);
                Coroutine coroutine = StartCoroutine(SpawnGemWithAnimation(new Vector2Int(column, y), gems[gemToUse]));
                spawnCoroutines.Add(coroutine);
            }
        }
        foreach (Coroutine coroutine in spawnCoroutines)
        {
            yield return coroutine;
        }
    }

    private void CheckMisplacedGems()
    {
        List<Gem> foundGems = new List<Gem>();
        foundGems.AddRange(FindObjectsOfType<Gem>());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (foundGems.Contains(allGems[x, y]))
                {
                    foundGems.Remove(allGems[x, y]);
                }
            }
        }

        foreach(Gem g in foundGems)
        {
            Destroy(g.gameObject);
        }
    }

    public void ShuffleBoard()
    {
        if (currentState != BoardState.wait)
        {
            currentState = BoardState.wait;

            List<Gem> gemsFromBoard = new List<Gem>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gemsFromBoard.Add(allGems[x, y]);
                    allGems[x, y] = null;
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int gemToUse = Random.Range(0, gemsFromBoard.Count);

                    int iterations = 0;

                    while(MatchesAt(new Vector2Int(x, y),
                        gemsFromBoard[gemToUse]) && iterations < 100 && gemsFromBoard.Count > 1)
                    {
                        gemToUse = Random.Range(0, gemsFromBoard.Count);
                        iterations++;
                    }

                    gemsFromBoard[gemToUse].SetupGem(new Vector2Int(x, y), this);
                    allGems[x, y] = gemsFromBoard[gemToUse];
                    gemsFromBoard.RemoveAt(gemToUse);
                }
            }

            StartCoroutine(FillBoardCo());
        }
    }

    public void ScoreCheck(Gem gemToCheck)
    {
        roundMan.currentScore += gemToCheck.scroreValue;
        if(bonusMulti > 0)
        {
            float bonusToAdd = gemToCheck.scroreValue * bonusMulti * bonusAmount;
            roundMan.currentScore += Mathf.RoundToInt(bonusToAdd);
        }
    }

    public void ResultingScore()
    {
        roundMan.ResultingScore();
    }
}
