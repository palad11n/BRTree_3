﻿namespace Black_Red_tree
{
    public partial class RBTree
    {
        public class Node
        {
            public Color Colour;
            public Node Left;
            public Node Right;
            public Node Parent;
            public double Value;
            public Node() { }
            public Node(double value)
            {
                this.Value = value;
                this.Colour = Color.R;
                this.Left = null;
                this.Right = null;
            }
        }
    }
}