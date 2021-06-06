using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public class Node
    {
        // --- Public
        public Node Parent;

        public int G_Cost = 0;
        public int H_Cost = 0;
        public int F_Cost { get { return G_Cost + H_Cost; } }

        public Tile Position;

        public Node(Node parent, Tile tile)
        {
            Parent = parent;
            Position = tile;
        }

    }
}
