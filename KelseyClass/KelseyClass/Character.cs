using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public class Character
    {
        public char Symbol { get; private set; }

        public int MyRow = 0;
        public int MyCol = 0;

        public Character()
        {
            Symbol = 'C';
        }

    }
}
