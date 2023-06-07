using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : TrackedCell
{
    public bool isActive() {
        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        //Array index error prevention
        if (this.position.x - offsetX < 0 || this.position.y - offsetY < 0)
            return false;
        if (this.position.x - offsetX >= CellFunctions.gridWidth || this.position.y - offsetY >= CellFunctions.gridHeight)
            return false;
        if (this.position.x + offsetX < 0 || this.position.y + offsetY < 0)
            return false;
        if (this.position.x + offsetX >= CellFunctions.gridWidth || this.position.y + offsetY >= CellFunctions.gridHeight)
            return false;
        //If we don't have a refrence cell return
        if (CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY] == null)
            return false;
        return true;
    }
    public override (bool, bool) Push(Direction_e dir, int bias)
    {

        return base.Push(dir, bias);
    }

    public override void Step()
    {
        
        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        
        if (this.position.x - offsetX < 0 || this.position.y - offsetY < 0)
            return;     
        if (this.position.x - offsetX >= CellFunctions.gridWidth || this.position.y - offsetY >= CellFunctions.gridHeight)
            return;
        if (this.position.x + offsetX < 0 || this.position.y + offsetY < 0)
            return;
        if (this.position.x + offsetX >= CellFunctions.gridWidth || this.position.y + offsetY >= CellFunctions.gridHeight)
            return;

        if (CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY] == null)
            return;

        



        Generate(CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY]);
        
        
    }
    public void Generate(Cell cell)
    {

        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        if (cell.cellType == CellType_e.TELEPORT)
        {
            return;
        }
        if (this.position.x - offsetX < 0 || this.position.y - offsetY < 0)
            return;
        if (this.position.x - offsetX >= CellFunctions.gridWidth || this.position.y - offsetY >= CellFunctions.gridHeight)
            return;
        if (this.position.x + offsetX < 0 || this.position.y + offsetY < 0)
            return;
        if (this.position.x + offsetX >= CellFunctions.gridWidth || this.position.y + offsetY >= CellFunctions.gridHeight)
            return;
        if (CellFunctions.cellGrid[(int)this.position.x - offsetX, (int)this.position.y - offsetY] == null)
            return;

        

        if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY] != null)
        {
            //if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TRASH)
            //    return;
            if (CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].cellType == CellType_e.TELEPORT&& CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].getDirection() == this.getDirection())
            {
                
                    CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].GetComponent<Teleport>().Generate(cell);
                    return;
                
            }
            else {
                (bool, bool) pushResult = CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY].Push(this.getDirection(), 1);
                if (pushResult.Item2 || !pushResult.Item1)
                    return;
                
            }
        }
        AudioManager.i.PlaySound(GameAssets.i.place);
        Cell refrenceCell = cell;
        Cell newCell = GridManager.instance.SpawnCell(
            refrenceCell.cellType,
            new Vector2((int)this.position.x + offsetX, (int)this.position.y + offsetY),
            refrenceCell.getDirection(),
            true
            );
        newCell.oldPosition = this.position;
        cell.Delete(false);

    }
        
        
        
}
