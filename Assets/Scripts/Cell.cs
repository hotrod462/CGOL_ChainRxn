using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
   public bool isAlive = false;
   public bool locked = false;
   public int numNeighbours = 0;
   

   public void SetAlive(bool alive,bool byUser =false)
   {
    if(!locked || !byUser){isAlive = alive;}//if deadd can change always if alive can change only if by user is false
    // a user cannot kill a locked cell --note that locked cells are always alive
    if(isAlive)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        //if final state is alive but not done by user lock it
        if(!byUser){locked = true;}
    }
    else
    {   //if final state is dead free it always 
        GetComponent<SpriteRenderer>().enabled = false;
        locked = false;
    }
   }
}
