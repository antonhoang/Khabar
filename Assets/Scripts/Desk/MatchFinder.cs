using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gem> currentMatches = new List<Gem>();

    public bool isMatchesAroundThePosition = false;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        currentMatches.Clear();

        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                Gem currentGem = board.allGems[x, y];
                if (currentGem != null) {
                    if (x > 0 && x < board.width - 1)
                    {
                        Gem leftGem = board.allGems[x - 1, y];
                        Gem rightGem = board.allGems[x + 1, y];
                        if(leftGem != null && rightGem != null)
                        {
                            
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type && currentGem.type != Gem.GemType.stone)
                                {
                                currentGem.isMatched = true;
                                leftGem.isMatched = true;
                                rightGem.isMatched = true;

                                currentMatches.Add(currentGem);
                                currentMatches.Add(leftGem);
                                currentMatches.Add(rightGem);
                            }
                        }
                    }

                    if (y > 0 && y < board.height - 1)
                    {
                        Gem aboveGem = board.allGems[x, y + 1];
                        Gem belowGem = board.allGems[x, y - 1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type && currentGem.type != Gem.GemType.stone)
                            {
                                currentGem.isMatched = true;
                                aboveGem.isMatched = true;
                                belowGem.isMatched = true;

                                currentMatches.Add(currentGem);
                                currentMatches.Add(aboveGem);
                                currentMatches.Add(belowGem);
                            }
                        }
                    }

                    if (currentMatches.Count >= 4) {
                        isMatchesAroundThePosition = true;
                    } else
                    {
                        isMatchesAroundThePosition = false;
                    }
                }


            }
        }

        if(currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList();
        }

        CheckForBombs();
    }

    public void CheckForBombs()
    {
        for(int i = 0; i < currentMatches.Count; i++)
        {
            Gem gem = currentMatches[i];

            int x = gem.posIndex.x;
            int y = gem.posIndex.y;

            if(gem.posIndex.x > 0)
            {
                if (board.allGems[x-1, y] != null)
                {
                    if (board.allGems[x-1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x-1, y), board.allGems[x-1, y]);
                    }
                }
            }

            if (gem.posIndex.x < board.width - 1)
            {
                if (board.allGems[x + 1, y] != null)
                {
                    if (board.allGems[x + 1, y].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x + 1, y), board.allGems[x + 1, y]);
                    }
                }
            }

            if (gem.posIndex.y > 0)
            {
                if (board.allGems[x, y - 1] != null)
                {
                    if (board.allGems[x, y - 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x, y - 1), board.allGems[x, y - 1]);
                    }
                }
            }

            if (gem.posIndex.y < board.height - 1)
            {
                if (board.allGems[x, y + 1] != null)
                {
                    if (board.allGems[x, y + 1].type == Gem.GemType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x, y + 1), board.allGems[x, y + 1]);
                    }
                }
            }
        }
    }

    public void MarkBombArea(Vector2Int bombPos, Gem theBomb)
    {
        HashSet<Gem> potentialMatches = new HashSet<Gem>(); // Use a HashSet to avoid duplicates

        int minX = Mathf.Max(0, bombPos.x - theBomb.blastSize);
        int maxX = Mathf.Min(board.width - 1, bombPos.x + theBomb.blastSize);
        int minY = Mathf.Max(0, bombPos.y - theBomb.blastSize);
        int maxY = Mathf.Min(board.height - 1, bombPos.y + theBomb.blastSize);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Gem gem = board.allGems[x, y];
                if (gem != null)
                {
                    gem.isMatched = true;
                    potentialMatches.Add(gem);
                }
            }
        }

        // Add potential matches to currentMatches and remove duplicates
        foreach (var gem in potentialMatches)
        {
            currentMatches.Add(gem);
        }

        // Update currentMatches to remove duplicates
        currentMatches = currentMatches.Distinct().ToList();
    }

}
