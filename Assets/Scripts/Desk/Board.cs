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
    public Gem judge;
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
                    SpawnGemWithoutBombs(new Vector2Int(x, y), layoutStore[x, y]);
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

        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
    }

    public Gem SpawnBombGem(Vector2Int pos)
    {
        Gem gem = Instantiate(bomb, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
        return gem;
    }

    public Gem SpawnJudgeGem(Vector2Int pos)
    {
        Gem gem = Instantiate(judge, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
        return gem;
    }

    private void SpawnGemWithoutBombs(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        gem.SetupGem(pos, this);
    }

    // This coroutine spawns a gem with drop animation
    private IEnumerator SpawnGemWithAnimation(Vector2Int pos, Gem gemToSpawn)
    {
        if (Random.Range(0f, 100f) < bombChance)
        {
            gemToSpawn = bomb;
        }

        // Instantiate the gem
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, height - 0.5f, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y;
        allGems[pos.x, pos.y] = gem;

        // Setup the gem
        gem.SetupGem(pos, this);
        yield return null;
        //if (gem == null)
        //    yield break;
        //Vector3 startPosition = gem.transform.position;
        //Vector3 endPosition = new Vector3(pos.x, pos.y, 0f); // Destination position

        //float duration = 0.3f; // Duration of the drop animation
        //float elapsed = 0f;

        //while (elapsed < duration)
        //{
        //    if (gem == null)
        //        yield break;
        //    elapsed += Time.deltaTime;
        //    float t = elapsed / duration;
        //    //t = Mathf.SmoothStep(0f, 1f, t);
        //    t = 1f - Mathf.Pow(1f - t, 3f); // Ease-out function
        //    gem.transform.position = Vector3.Lerp(startPosition, endPosition, t);
        //    yield return null;
        //}

        //if (gem == null)
        //    yield break;
        //gem.transform.position = endPosition; // Ensure it reaches the final position exactly

        //// Start jump animation
        //if(gem != null)
        //{
        //    StartCoroutine(JumpAnimation(gem));
        //}
    }

    // This coroutine performs the jump animation
    private IEnumerator JumpAnimation(Gem gem)
    {
        // Jump animation
        if (gem == null)
            yield break;
        Vector3 originalPosition = gem.transform.position;
        Vector3 jumpOffset = new Vector3(0, 0.03f, 0); // Adjust the jump amount as needed
        float jumpDuration = 0.1f; // Duration of the jump animation
        float jumpElapsed = 0f;

        while (jumpElapsed < jumpDuration)
        {
            if (gem == null)
                yield break;
            jumpElapsed += Time.deltaTime;
            float t = jumpElapsed / jumpDuration;
            gem.transform.position = Vector3.Lerp(originalPosition, originalPosition + jumpOffset, Mathf.Sin(t * Mathf.PI));
            yield return null;
        }

        // Ensure it returns to the original position exactly
        if (gem == null)
            yield break;
        gem.transform.position = originalPosition;
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

    public void DestroyGemsByRowAndColumn(Vector2Int pos)
    {
        // Destroy gems in the same row
        for (int x = 0; x < width; x++)
        {
            if (allGems[x, pos.y] != null)
            {
                Instantiate(allGems[x, pos.y].destroyEffect, new Vector2(x, pos.y), Quaternion.identity);
                Destroy(allGems[x, pos.y].gameObject);
                allGems[x, pos.y] = null;
            }
        }

        // Destroy gems in the same column
        for (int y = 0; y < height; y++)
        {
            if (allGems[pos.x, y] != null)
            {
                Instantiate(allGems[pos.x, y].destroyEffect, new Vector2(pos.x, y), Quaternion.identity);
                Destroy(allGems[pos.x, y].gameObject);
                allGems[pos.x, y] = null;
            }
        }
        StartCoroutine(DecreaseRowCo());
    }


    private void DestroyMatchedGemAt(Vector2Int pos) {
        if (allGems[pos.x, pos.y] != null)
        {
            if (allGems[pos.x, pos.y].isMatched)
            {
                if (allGems[pos.x, pos.y].type == Gem.GemType.bomb)
                {
                    SFXManager.instance.PlayExplode();
                }
                else if (allGems[pos.x, pos.y].type == Gem.GemType.stone)
                {
                    SFXManager.instance.PlayStoneBreak();
                }
                else
                {
                    SFXManager.instance.PlayGemBreak();
                }

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
        yield return new WaitForSeconds(0.18f);

        DecreaseRow();
        
        StartCoroutine(FillBoardCo());
    }

    private IEnumerator FillBoardCo()
    {
        yield return StartCoroutine(RefillBoardWithAnimationCo());
    }

    private IEnumerator RefillBoardWithAnimationCo()
    {
        bool matchesFound = true;

        while (matchesFound)
        {
            matchesFound = false; // Reset matchesFound flag

            // List to store coroutine references for destroyed columns
            List<Coroutine> destroyedCoroutines = new List<Coroutine>();

            // Start a coroutine for each column
            for (int x = 0; x < width; x++)
            {
                yield return new WaitForSeconds(0.005f);
                destroyedCoroutines.Add(StartCoroutine(SpawnGemsInColumnWithAnimation(x)));
            }

            // Wait for all coroutines for destroyed columns to finish first
            foreach (Coroutine coroutine in destroyedCoroutines)
            {
                yield return coroutine;

            }

            matchFind.FindAllMatches();


            // Check if matches are found
            if (matchFind.currentMatches.Count > 0)
            {
                bonusMulti++;
                matchesFound = true;

                // Score matches and destroy gems
                foreach (var match in matchFind.currentMatches)
                {
                    ScoreCheck(match);
                    DestroyMatchedGemAt(match.posIndex);
                }

                // Adjust gem positions
                yield return new WaitForSeconds(0.18f);
                DecreaseRow();
            } else
            {
                currentState = BoardState.move;
                bonusMulti = 0f;
                ResultingScore();
            }
        }

        // No more matches found, perform final operations
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
                yield return new WaitForSeconds(0.1f);
                Coroutine coroutine = StartCoroutine(SpawnGemWithAnimation(new Vector2Int(column, y), gems[gemToUse]));
                spawnCoroutines.Add(coroutine);
            }
        }

        // Wait for all coroutines to finish before continuing
        foreach (Coroutine coroutine in spawnCoroutines)
        {
            yield return coroutine;
        }
    }

    private void DecreaseRow()
    {
        int nullCounter = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    allGems[x, y].posIndex.y -= nullCounter;
                    allGems[x, y - nullCounter] = allGems[x, y];
                    allGems[x, y] = null;
                }
            }
            nullCounter = 0;
        }
    }

    public void CheckMisplacedGems()
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
