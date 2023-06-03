using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : Cell
{
    public override (bool, bool) Push(Direction_e dir, int bias, bool pulled)
    {
        if(!pulled)
        {
            Debug.Log("U");
            if (dir == this.getDirection() || ((int)dir + 2) % 4 == (int)this.getDirection())
                return base.Push(dir, bias, pulled);
            
        }
        return (false, false);
    }
        
}
