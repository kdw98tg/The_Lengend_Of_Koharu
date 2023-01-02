using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn 1 - can spawn 2 - HAS to spawn

            if (x>= minPosition.x && x<=maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    public Vector2Int size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;
    public float generationsStepDelay;

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

   

    void  MazeGenerator()
    {
      
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());    
            }
        }

        int currentCell = startPos;
        
        Stack<int> path = new Stack<int>();
        
        int k = 0;

        while (k < 1000)
        {
           
            k++;
            board[currentCell].visited = true;
            
            // Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                
                if (newCell > currentCell)
                {
                    // down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    // up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }

        }

        StartCoroutine(GenerateDungeon());

    }
    
    
    IEnumerator GenerateDungeon()
    {
        WaitForSeconds delay = new WaitForSeconds(generationsStepDelay);
            
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                yield return delay;
                
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity,
                        transform).GetComponent<RoomBehaviour>();  
                    //newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                    newRoom.UpdateRoom(currentCell.status);

                    
                    if (currentCell == board[0] )
                    {
                        newRoom.RoomStart();
                    }
                    if (currentCell == board[15] )
                    {
                        newRoom.RoomEnd();
                    }
                    if (currentCell == board[8] )
                    {
                        newRoom.RoomKey();
                    }
                        

                }
            }
        }
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        // check up neighbor
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }
        
        // check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        // check right neighbor
        if ((cell +1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }
        
        // check left neighbor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;

    }
}