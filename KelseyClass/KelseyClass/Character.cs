using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public abstract class Character
    {
        public char Symbol { get; protected set; }

        public int MyRow = 0;
        public int MyCol = 0;
    }
}
