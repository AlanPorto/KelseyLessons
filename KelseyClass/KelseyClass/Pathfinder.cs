using System;
using System.Collections.Generic;
using System.Text;

namespace KelseyClass
{
    public static class Pathfinder
    {
        public static List<Tile> GetPath(Tile startTile, Tile endTile, Tile[,] map)
        {
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();

            Node start = new Node(null, startTile);

            openNodes.Add(start);

            while (openNodes.Count > 0)
            {
                Node currentNode = openNodes[0];
                int currentIndex = 0;
                
                // Get the lowest F cost open node
                for (int i = 0; i < openNodes.Count; i++)
                {
                    Node openNode = openNodes[i];
                    if (openNode.F_Cost < currentNode.F_Cost)
                    {
                        currentNode = openNode;
                        currentIndex = i;
                    }
                }

                openNodes.RemoveAt(currentIndex);
                closedNodes.Add(currentNode);

                // Did we find the goal?
                if (currentNode.Position == endTile)
                {
                    List<Tile> path = new List<Tile>();

                    Node pathNode = currentNode;
                    while (pathNode != null)
                    {
                        path.Add(pathNode.Position);
                        pathNode = pathNode.Parent;
                    }

                    return path;
                }

                // Else


                // Get all child nodes
                List<Node> childNodes = new List<Node>();

                var directionList = Enum.GetValues(typeof(Directions));
                foreach (Directions dir in directionList)
                {
                    int row;
                    int col;
                    Map.GetRowAndColForDirection(dir, out row, out col);

                    int childRow = currentNode.Position.MyRow + row;
                    int childCol = currentNode.Position.MyCol + col;

                    if (Map.IsPositionEmptyAndValid(childRow, childCol, map))
                    {
                        Tile childPosition = map[childRow, childCol];
                        Node child = new Node(currentNode, childPosition);
                        childNodes.Add(child);
                    }
                }

                // Loop through the children and try to add them to the open nodes
                foreach (Node child in childNodes)
                {
                    // If the current child is already closed, we skip it
                    if (ContainsNode(child, closedNodes))
                    {
                        continue;
                    }

                    // Set the G and H values
                    child.G_Cost = currentNode.G_Cost + 1;
                    child.H_Cost = GetSqrDistance(child.Position, endTile);

                    // Check if the child is in the open list
                    foreach (Node open in openNodes)
                    {
                        // If our G cost is higher, we just skip the child
                        if ((open.Position == child.Position) && (child.G_Cost >= open.G_Cost))
                        {
                            continue;
                        }
                    }

                    // Otherwise, we add the child in the open list
                    openNodes.Add(child);
                }
            }

            return new List<Tile>(); // Return an empty list
        }

        private static bool ContainsNode(Node node, List<Node> list)
        {
            foreach (Node n in list)
            {
                if (node.Position == n.Position)
                {
                    return true;
                }
            }

            return false;
        }

        private static int GetSqrDistance(Tile a, Tile b)
        {
            int deltaX = a.MyRow - b.MyRow;
            int deltaY = a.MyCol - b.MyCol;

            return (deltaX * deltaX) + (deltaY * deltaY);
        }
    }
}
