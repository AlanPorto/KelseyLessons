using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public class Monster : Character
    {
        public Monster()
        {
            Symbol = 'M';
            MyRow = Map.MaxRow - 1;
            MyCol = Map.MaxCol - 1;
        }
    }
}
