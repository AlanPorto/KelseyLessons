using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public class Tile
    {
        // --- Public
        public char Symbol
        {
            get
            {
                if (CharRef != null)
                {
                    return CharRef.Symbol;
                }
                else
                {
                    return mSymbol;
                }
            }
        }

        public int MyRow = 0;
        public int MyCol = 0;

        public Character CharRef;

        // --- Private
        private char mSymbol;

        public Tile(char symbol)
        {
            mSymbol = symbol;
        }
    }
}
