using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class Game : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT= 48;


    public float speed = 0.1f;

    private float timer = 0;

    public bool simulationEnabled = false;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];
    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
      if(simulationEnabled)
      {
        if (timer >= speed)
        {
          timer = 0f;
          CountNeighbours ();
          PopulationControl();

        } 
        else
        {
          timer += Time.deltaTime;
        }
      }   
      UserInput();
    }

    private void Savepattern()
    {
      string path = "patterns";
      if(!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);

      }
      Pattern pattern = new Pattern();
      string patternString = null;
      for (int y=0; y< SCREEN_HEIGHT; y++)
      {
        for(int x=0; x<SCREEN_WIDTH; x++)
        {
          if(grid[x,y].isAlive==false)
          {
            patternString += "0";
          }
          else
          {
            patternString += "1";

          }
        }
      }
      pattern.patternString = patternString;
      XmlSerializer serializer = new XmlSerializer(typeof(Pattern));
      StreamWriter writer = new StreamWriter(path + "/test.xml");
      serializer.Serialize(writer.BaseStream, pattern);
      writer.Close();

      Debug.Log(pattern.patternString);
    }

    void UserInput()
    {
      if(Input.GetMouseButtonDown(0))
      {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int x= Mathf.RoundToInt(mousePoint.x);
        int y= Mathf.RoundToInt(mousePoint.y);

        if(x>=0 && y>=0 && x<SCREEN_WIDTH && y<SCREEN_HEIGHT)
        {
          //-We are in bounds
          grid[x,y].SetAlive(!grid[x,y].isAlive);
        }

      }
      if(Input.GetKeyUp(KeyCode.P))
      {
        //-Pause Simulation
        simulationEnabled = false;
      }
      if(Input.GetKeyUp(KeyCode.B))
      {
        //-Build  simulation/ resume
        simulationEnabled = true;


      }
      if(Input.GetKeyUp(KeyCode.S))
      {
        //-save pattern
        Savepattern();
      }

    }






    void PlaceCells()
    {
        for(int y = 0; y< SCREEN_HEIGHT; y++)
        {
            for(int x = 0; x< SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x,y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }


    void CountNeighbours()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x =0; x < SCREEN_WIDTH; x++)
            {
              int numNeighbours = 0;
            //-North
              if(y+1 < SCREEN_HEIGHT)
              {
                if(grid[x,y+1].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-east
              if(x+1 < SCREEN_WIDTH)
              {
                if(grid[x+1,y].isAlive)
                {
                    numNeighbours++;
                }
              } 
              //-south
              if (y-1>=0)
              {
                if(grid[x,y-1].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-west
              if (x-1>=0)
              {
                if(grid[x-1,y].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-North east
              if(x+1 < SCREEN_WIDTH && y+1 < SCREEN_HEIGHT)
              {
                if(grid[x+1,y+1].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-northwest
              if(x-1 >=0 && y+1 < SCREEN_HEIGHT)
              {
                if(grid[x-1,y+1].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-southeast
              if(x+1 < SCREEN_WIDTH && y-1 >= 0)
              {
                if(grid[x+1,y-1].isAlive)
                {
                    numNeighbours++;
                }
              }
              //-south west
              if(x-1 >= 0 && y-1 >= 0)
              {
                if(grid[x-1,y-1].isAlive)
                {
                    numNeighbours++;
                }
              }
              
              grid[x,y].numNeighbours = numNeighbours;
            }
        }

    }

    void PopulationControl ()
    {
        for(int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for(int x = 0; x < SCREEN_WIDTH; x++)
            {
                //-Rules
                //-Any live cell with 2 or 3 live neighbours survives 
                //-Any dead cell with 3 live miegbors becomes a live cell
                //-All other live cells die in the next generation and all other dead cells stay dead

                if(grid[x,y].isAlive)
                {
                    //-Cell is alive
                    if(grid[x,y].numNeighbours!=2 && grid[x,y].numNeighbours!=3)
                    {
                        grid[x,y].SetAlive(false);
                    }

                }
                else
                {
                    //-Cell is Dead
                    if(grid[x,y].numNeighbours==3)
                    {
                        grid[x,y].SetAlive(true);
                    }
                }
            }
        }
    }


    bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0,100);

        if(rand > 75)
        {
            return true;
        }

        return false;
    }
}
