using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : TickedCell
{
    protected int rotationAmount = 1;

    int[][] rotationOffsetsY = new int[][] {
        new int[] { 0, -1},
        new int[] { 0, 1}
    };
    int[][] rotationOffsetsX = new int[][] {
        new int[] { 1, 0},
        new int[] { -1, 0}
    };

    void rotateCell(int xOffset, int yOffset)
    {

        if (this.position.x + xOffset >= CellFunctions.gridWidth || this.position.y + yOffset >= CellFunctions.gridHeight)
            return;
        if (this.position.x + xOffset < 0 || this.position.y + yOffset < 0)
            return;
        if (CellFunctions.cellGrid[(int)this.position.x + xOffset, (int)this.position.y + yOffset] == null)
            return;

        CellFunctions.cellGrid[(int)this.position.x + xOffset, (int)this.position.y + yOffset].Rotate(2);
        
    }
    public override void Step()
    {
        int[][] rotationOffsets;
        if (rotation == 0 || rotation == 2)
        {
            rotationOffsets = rotationOffsetsY;
        }
        else
        {
            rotationOffsets = rotationOffsetsX;
        }
        foreach (int[] offset in rotationOffsets)
        {
            rotateCell(offset[0], offset[1]);
        }
    }
    public override (bool, bool) Push(Direction_e dir, int bias, bool pulled)
    {
        if(dir == this.getDirection() || ((int)dir + 2) % 4 == (int)this.getDirection())
            return base.Push(dir, bias, pulled);
        return (false, false);
    }
}
