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
        PlaceCells(1);
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
    private void LoadPattern()
    {
        string path = "patterns";
        if(!Directory.Exists(path))
        {
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(Pattern));
        path += "/test.xml";

        StreamReader reader = new StreamReader(path);
        Pattern pattern = (Pattern)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        bool isAlive;
        int x=0, y=0;
        Debug.Log(pattern.patternString);


        foreach (char c in pattern.patternString)
        {
            if(c.ToString()=="1")
            {
                isAlive = true;
            }
            else
            {
                isAlive = false;
            }
            grid[x,y].SetAlive(isAlive);
            x++;
            if(x==SCREEN_WIDTH)
            {
                x=0;
                y++;
            }
        }
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
      if(Input.GetKeyUp(KeyCode.L))
      {
        //-Load pattern
        LoadPattern();
      }

    }






    void PlaceCells(int type)
    {
        if(type==1)
        {
            //-center width = 64/32
            //-that means there is no true center so we will have to be offset 1 to the right or to the left
            //center height= 48/2=24
            //-that means ther is no true center so we will have to be offset 1 to the top or bottom 
            for(int y=0; y<SCREEN_HEIGHT; y++)
            {

                for(int x=0;x<SCREEN_WIDTH; x++)
                {
                    Cell cell= Instantiate(Resources.Load("Prefabs/cell",typeof(Cell)), new Vector2(x,y), Quaternion.identity) as Cell;
                    grid[x,y] = cell;
                    grid[x,y].SetAlive(false);
                }
            }
            for (int y=21;y<24;y++)
            {
                for(int x=0; x<32; x++)
                {
                    //-nothing at x=34
                    //-nothing at x=32 and x=22
                    //-nothing at x=36 and y=22
                    if(x!=34)
                    {
                        if(y==21 || y==23)
                        {
                            grid[x,y].SetAlive(true);
                        }
                        else if(y==22 && ((x!=32) && (x!=36)))
                        {
                            grid[x,y].SetAlive(true);
                        }
                    }
                }

                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x,y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                //grid[x, y].SetAlive(RandomAliveCell());
                grid[x, y].SetAlive(false); // change 

            }
        }
        else if(type == 2)
        {
            for (int y=0; y<SCREEN_HEIGHT; y++)
            {
                for (int x=0; x<SCREEN_WIDTH; x++)
                {
                    Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x,y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(false);
                }
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