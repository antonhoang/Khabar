using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLayout : MonoBehaviour
{
    public LayoutRow[] allRows;

    // Matrix representations of gem layouts
    private int[][] matrix0 = new int[][]
    {
        new int[] {1, 0, 1, 0, 0, 1, 1, 1},
        new int[] {1, 1, 0, 0, 1, 0, 1, 0},
        new int[] {0, 0, 1, 0, 1, 1, 1, 0},
        new int[] {0, 1, 0, 1, 1, 0, 1, 1},
        new int[] {1, 1, 1, 0, 0, 0, 1, 0},
        new int[] {1, 0, 0, 0, 0, 1, 0, 0},
        new int[] {0, 1, 1, 0, 0, 1, 0, 0},
        new int[] {0, 0, 1, 1, 1, 1, 1, 1}
    };

    private int[][] matrix1 = new int[][] {
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 1, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0},
    new int[] {0, 0, 0, 0, 0, 0, 0, 0}
    };

    private int[][] matrix2 = new int[][]
    {
        new int[] {0, 1, 1, 0, 1, 1, 1, 1},
        new int[] {0, 1, 1, 1, 1, 1, 0, 0},
        new int[] {1, 0, 0, 1, 1, 1, 0, 1},
        new int[] {0, 1, 0, 1, 0, 1, 0, 1},
        new int[] {1, 1, 1, 1, 1, 1, 0, 0},
        new int[] {1, 1, 0, 1, 1, 1, 1, 1},
        new int[] {1, 0, 0, 1, 1, 0, 1, 0},
        new int[] {1, 1, 0, 1, 0, 0, 1, 1}
    };

    public Gem gemPrefab;

    private Gem[,] gems0;
    private Gem[,] gems1;
    private Gem[,] gems2;

    //private void Start()
    //{
    //    gems0 = ConvertToGems(matrix0);
    //    gems1 = ConvertToGems(matrix1);
    //    gems2 = ConvertToGems(matrix2);
    //}

    Gem[,] ConvertToGems(int[][] matrix)
    {
        int numRows = matrix.Length;
        int numCols = matrix[0].Length;
        Gem[,] gems = new Gem[numRows, numCols];

        for (int y = 0; y < numRows; y++)
        {
            for (int x = 0; x < numCols; x++)
            {
                if (matrix[y][x] == 1)
                {
                    // Place a gem
                    gems[y, x] = Instantiate(gemPrefab, transform.position + new Vector3(x, 0, -y), Quaternion.identity, transform).GetComponent<Gem>();
                }
            }
        }

        return gems;
    }

    public Gem[,] GetLayout()
    {
        gems1 = ConvertToGems(matrix1);
        return gems1;
        //Gem[,] theLayout = new Gem[allRows[0].gemsInRow.Length, allRows.Length];

        //for (int y = 0; y < allRows.Length; y++)
        //{
        //    for (int x = 0; x < allRows[y].gemsInRow.Length; x++)
        //    {
        //        if (x < theLayout.GetLength(0))
        //        {
        //            if (allRows[y].gemsInRow[x] != null)
        //            {
        //                theLayout[x, allRows.Length - 1 - y] = allRows[y].gemsInRow[x];
        //            }
        //        }
        //    }
        //}



        //return theLayout;
    }

    public Gem[,] GetLayout2()
    {
        
        Gem[,] theLayout = new Gem[allRows[0].gemsInRow.Length, allRows.Length];

        for (int y = 0; y < allRows.Length; y++)
        {
            for (int x = 0; x < allRows[y].gemsInRow.Length; x++)
            {
                if (x < theLayout.GetLength(0))
                {
                    if (allRows[y].gemsInRow[x] != null)
                    {
                        theLayout[x, allRows.Length - 1 - y] = allRows[y].gemsInRow[x];
                    }
                }
            }
        }



        return theLayout;
    }
}

[System.Serializable]
public class LayoutRow
{
    public Gem[] gemsInRow;
}
