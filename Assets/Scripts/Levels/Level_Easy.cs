using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class Level_Easy : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_HEIGHT= 48;
    public int CellPlacedCounter = 0;

    public float speed = 0.1f;

    private float timer = 0;

    public bool simulationEnabled = false;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];
    // Start is called before the first frame update
    void Start()
    {
        PlaceCells(2);
    }

    // Update is called once per frame
    void Update()
    {
      if(simulationEnabled)
      {
        if (timer >= speed)
        {
          timer = 0f;
          CountNeighbours();
          PopulationControl();

        } 
        else
        {
          timer += Time.deltaTime;
        }
      }   
      UserInput();
      if(Savepattern()){
          EndGame();
        };
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
    private bool Savepattern()
    { bool flag = true; //if empty then true
      
      for (int y=0; y< SCREEN_HEIGHT; y++)
      {
        for(int x=0; x<SCREEN_WIDTH; x++)
        {
          if(grid[x,y].isAlive==false)
          {
            
          }
          else
          {
            
            flag = false;

          }
        }
      }
    
      return flag;
    }

    void UserInput()
    {
       if(Input.GetMouseButtonDown(0)) //agar mouse button se input milta hai to 
      {
        
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int x= Mathf.RoundToInt(mousePoint.x);
        int y= Mathf.RoundToInt(mousePoint.y);

        if(x>=0 && y>=0 && x<SCREEN_WIDTH && y<SCREEN_HEIGHT)
        {
          //-We are in bounds yupp
          
          grid[x,y].SetAlive(!grid[x,y].isAlive,true);
          if(!grid[x,y].locked){CellPlacedCounter++;}
          //grid[x,y].CellPlacedCounter = CellPlacedCounter;
          Debug.Log("Cell Placed Counter: " + CellPlacedCounter);
          UIManager.Instance.UpdateCellCount(CellPlacedCounter); // Update UI
        }
        
      }
      
      if(Input.GetKeyUp(KeyCode.Space))
      {
        //-Pause Simulation
        simulationEnabled = !simulationEnabled;
      }
      
      if(Input.GetKeyUp(KeyCode.S))
      {
        //-save pattern
        if(Savepattern()){
          EndGame();
        };
      }
      if(Input.GetKeyUp(KeyCode.L))
      {
        //-Load pattern
        LoadPattern();
      }

    }

    void PlaceCells(int type)
    {
        for(int y=0; y<SCREEN_HEIGHT; y++)
            {

                for(int x=0;x<SCREEN_WIDTH; x++)
                {
                    Cell cell= Instantiate(Resources.Load("Prefabs/cell",typeof(Cell)), new Vector2(x,y), Quaternion.identity) as Cell;
                    grid[x,y] = cell;
                    grid[x,y].SetAlive(false); // initiate all the grid cells as black
                }
            }
        if(type==1)
        {
            //-center width = 64/32
            //-that means there is no true center so we will have to be offset 1 to the right or to the left
            //center height= 48/2=24
            //-that means ther is no true center so we will have to be offset 1 to the top or bottom 
           
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

                

            }
        }
        if(type == 2)
        {
            grid[32,23].SetAlive(true);
            grid[33,23].SetAlive(true);
            grid[32,24].SetAlive(true);
            grid[33,24].SetAlive(true);
            grid[32,26].SetAlive(true);
            grid[33,26].SetAlive(true);
            grid[32,27].SetAlive(true);
            grid[33,27].SetAlive(true);
        }
        if(type==4){
            grid[32,25].SetAlive(true);
            grid[32,26].SetAlive(true);
            grid[32,23].SetAlive(true);
            grid[32,22].SetAlive(true);
            grid[32,28].SetAlive(true);
            grid[32,29].SetAlive(true);
            grid[32,30].SetAlive(true);
            grid[32,31].SetAlive(true);
            grid[32,17].SetAlive(true);
            grid[32,18].SetAlive(true);
            grid[32,19].SetAlive(true);
            grid[32,20].SetAlive(true);
            grid[33,29].SetAlive(true);
            grid[33,19].SetAlive(true);
            grid[34,29].SetAlive(true);
            grid[34,19].SetAlive(true);
            grid[35,25].SetAlive(true);
            grid[35,23].SetAlive(true);
            grid[36,24].SetAlive(true);
            grid[37,24].SetAlive(true);
            grid[31,24].SetAlive(true);
            grid[31,26].SetAlive(true);
            grid[31,31].SetAlive(true);
            grid[31,32].SetAlive(true);
            grid[31,22].SetAlive(true);
            grid[31,17].SetAlive(true);
            grid[31,16].SetAlive(true);
            grid[30,24].SetAlive(true);
            grid[30,33].SetAlive(true);
            grid[30,27].SetAlive(true);
            grid[30,15].SetAlive(true);
            grid[30,21].SetAlive(true);
            grid[29,26].SetAlive(true);
            grid[29,27].SetAlive(true);
            grid[29,28].SetAlive(true);
            grid[29,31].SetAlive(true);
            grid[29,32].SetAlive(true);
            grid[29,22].SetAlive(true);
            grid[29,21].SetAlive(true);
            grid[29,20].SetAlive(true);
            grid[29,17].SetAlive(true);
            grid[29,16].SetAlive(true);
            grid[28,26].SetAlive(true);
            grid[28,29].SetAlive(true);
            grid[28,22].SetAlive(true);
            grid[28,19].SetAlive(true);
            grid[27,27].SetAlive(true);
            grid[27,29].SetAlive(true);
            grid[27,21].SetAlive(true);
            grid[27,19].SetAlive(true);
            grid[26,27].SetAlive(true);
            grid[26,29].SetAlive(true);
            grid[26,21].SetAlive(true);
            grid[26,19].SetAlive(true);
                       
        }
        else if(type==3)
        {
            grid[26,20].SetAlive(true);
            grid[26,21].SetAlive(true);
            grid[26,27].SetAlive(true);
            grid[26,28].SetAlive(true);
            grid[27,21].SetAlive(true);
            grid[27,22].SetAlive(true);
            grid[27,26].SetAlive(true);
            grid[27,27].SetAlive(true);
            grid[28,18].SetAlive(true);
            grid[28,21].SetAlive(true);
            grid[28,23].SetAlive(true);
            grid[28,25].SetAlive(true);
            grid[28,27].SetAlive(true);
            grid[28,30].SetAlive(true);
            grid[29,18].SetAlive(true);
            grid[29,19].SetAlive(true);
            grid[29,20].SetAlive(true);
            grid[29,22].SetAlive(true);
            grid[29,23].SetAlive(true);
            grid[29,25].SetAlive(true);
            grid[29,26].SetAlive(true);
            grid[29,28].SetAlive(true);
            grid[29,29].SetAlive(true);
            grid[29,30].SetAlive(true);
            grid[29,25].SetAlive(true);
            grid[29,26].SetAlive(true);
            grid[29,28].SetAlive(true);
            grid[29,29].SetAlive(true);
            grid[29,30].SetAlive(true);
            grid[30,19].SetAlive(true);
            grid[30,21].SetAlive(true);
            grid[30,23].SetAlive(true);
            grid[30,25].SetAlive(true);
            grid[30,27].SetAlive(true);
            grid[30,29].SetAlive(true);
            grid[31,20].SetAlive(true);
            grid[31,21].SetAlive(true);
            grid[31,22].SetAlive(true);
            grid[31,26].SetAlive(true);
            grid[31,27].SetAlive(true);
            grid[31,28].SetAlive(true);
            grid[33,20].SetAlive(true);
            grid[33,21].SetAlive(true);
            grid[33,22].SetAlive(true);
            grid[33,26].SetAlive(true);
            grid[33,27].SetAlive(true);
            grid[33,28].SetAlive(true);
            grid[34,19].SetAlive(true);
            grid[34,21].SetAlive(true);
            grid[34,23].SetAlive(true);
            grid[34,25].SetAlive(true);
            grid[34,27].SetAlive(true);
            grid[34,29].SetAlive(true);

            grid[35,19].SetAlive(true);
            grid[35,20].SetAlive(true);
            grid[35,22].SetAlive(true);
            grid[35,26].SetAlive(true);
            grid[35,28].SetAlive(true);
            grid[35,29].SetAlive(true);

            grid[36,18].SetAlive(true);
            grid[36,21].SetAlive(true);
            grid[36,23].SetAlive(true);
            grid[36,25].SetAlive(true);
            grid[36,27].SetAlive(true);
            grid[36,30].SetAlive(true);
            
            grid[37,17].SetAlive(true);
            grid[37,21].SetAlive(true);
            grid[37,22].SetAlive(true);
            grid[37,24].SetAlive(true);
            grid[37,26].SetAlive(true);
            grid[37,27].SetAlive(true);
            grid[37,31].SetAlive(true);
            grid[38,16].SetAlive(true);
            grid[38,18].SetAlive(true);
            grid[38,20].SetAlive(true);
            grid[38,21].SetAlive(true);
            grid[38,23].SetAlive(true);
            grid[38,25].SetAlive(true);
            grid[38,27].SetAlive(true);
            grid[38,28].SetAlive(true);
            grid[38,30].SetAlive(true);
            grid[38,32].SetAlive(true);
            grid[39,22].SetAlive(true);
            grid[39,26].SetAlive(true);
            grid[40,23].SetAlive(true);
            grid[40,24].SetAlive(true);
            grid[40,25].SetAlive(true);
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
    private void EndGame(){
      GameData.Instance.playerScore = CellPlacedCounter;

        // Load the end screen scene
        SceneManager.LoadScene("EndScene"); // Replace with your actual end screen scene name
    }
    
}