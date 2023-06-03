using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : TrackedCell
{
    int[][] Offsets = new int[][]
    {
       new int[] {-1, 0},
       new int[] { 0, 1},
       new int[] { 1, 0},
       new int[] { 0, -1} };
    public override void Step()
    {

        this.Pull(this.getDirection(), 0);
        this.Push(this.getDirection(), 0, false);
        
        //Suppressed will get set to true so we have to reset it.
        this.suppresed = false;
    }

    public override (bool, bool) Push(Direction_e dir, int bias, bool pulled)
    {
        if (this.suppresed)
            return base.Push(dir, bias, pulled);
        if (this.getDirection() == dir)
        {
            bias += 1;
        }

        //if bias is opposite our direction
        else if ((int)(dir + 2) % 4 == (int)this.getDirection())
        {
            bias -= 1;
        }

        return base.Push(dir, bias, pulled);

        
    }
    public (bool,bool) Pull(Direction_e dir, int bias)
    {
        int xOffset = Offsets[(int)dir][0];
        int yOffset = Offsets[(int)dir][1];
        if (this.position.x + xOffset >= CellFunctions.gridWidth || this.position.y + yOffset >= CellFunctions.gridHeight)
        {
            
            return (false, false);
        }


        if (this.position.x + xOffset < 0 || this.position.y + yOffset < 0)
        {
            
            return (false, false);
        }

        if (CellFunctions.cellGrid[(int)this.position.x + xOffset, (int)this.position.y + yOffset] == null)
        {
            
            return (false, false);
        }


        if (this.suppresed)
        {
            
            
            return CellFunctions.cellGrid[(int)this.position.x + xOffset, (int)this.position.y + yOffset].Push(dir, bias,true);
        }
        
        if (this.getDirection() == dir)
        {
            bias += 1;
        }

        //if bias is opposite our direction
        else if ((int)(dir + 2) % 4 == (int)this.getDirection())
        {
            bias -= 1;
        }
        
        return CellFunctions.cellGrid[(int)this.position.x + xOffset, (int)this.position.y + yOffset].Push(dir, bias,true);
    }
}
