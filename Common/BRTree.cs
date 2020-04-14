using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Black_Red_tree
{
    public partial class RBTree
    {
        /// <summary> 
        /// Корневой узел 
        /// </summary> 
        public Node Root;
        public RBTree() { }
        private Node Grandpa(Node key)
        {
            if (key != null && key.Parent != null)
                return key.Parent.Parent;
            else return null;
        }

        private Node Uncle(Node key)
        {
            var root = Grandpa(key);
            if (root == null)
                return null;
            if (key.Parent == root.Left)
                return root.Right;
            else return root.Left;
        }
        #region Повороты 
        //смещаем правого сына наверх
        private void LeftRotate(Node n)
        {
            var isRoot = false;
            if (n.Parent == null)
                isRoot = true;
            var pivot = n.Right;
            //pivot может стать корнем дерева 
            pivot.Parent = n.Parent;
            if (n.Parent != null)
            {
                if (n.Parent.Left == n)
                    n.Parent.Left = pivot;
                else n.Parent.Right = pivot;
            }
            n.Right = pivot.Left;
            if (pivot.Left != null)
                pivot.Left.Parent = n;
            n.Parent = pivot;
            pivot.Left = n;
            if (isRoot)
                Root = pivot;
        }
        //смещаем левого сына наверх
        private void RightRotate(Node n)
        {
            var isRoot = false;
            if (n.Parent == null)
                isRoot = true;
            var pivot = n.Left;
            //pivot может стать корнем дерева 
            pivot.Parent = n.Parent;
            if (n.Parent != null)
            {
                if (n.Parent.Left == n)
                    n.Parent.Left = pivot;
                else n.Parent.Right = pivot;
            }
            n.Left = pivot.Right;
            if (pivot.Right != null)
                pivot.Right.Parent = n;
            n.Parent = pivot;
            pivot.Right = n;
            if (isRoot)
                Root = pivot;
        }
        #endregion
        #region Вспомогательные методы
        public Color GetColorNodeByKey(double key)
        {
            var isExist = FindNodeByKey(key);
            return isExist == null ? Color.NaN : isExist.Colour;
        }

        public Node FindNodeByKey(double key)
        {
            bool isFound = false;
            Node temp = Root;

            while (!isFound)
            {
                if (temp == null) break;

                if (temp != null && key < temp.Value)
                    temp = temp.Left;

                if (temp != null && key > temp.Value)
                    temp = temp.Right;

                if (temp != null && key == temp.Value)
                    isFound = true;
            }
            if (isFound) return temp;
            else return null;
        }

        private void InsertInTree(double key)
        {
            if (Root == null)
                Root = new Node(key);
            else
            {
                var node = Root;
                while (node != null)
                {
                    var nextNode = key < node.Value ? node.Left : node.Right;
                    if (nextNode == null)
                        if (key < node.Value)
                        {
                            node.Left = new Node(key);
                            node.Left.Parent = node;
                        }
                        else
                        {
                            node.Right = new Node(key);
                            node.Right.Parent = node;
                        }
                    node = nextNode;
                }
            }
        }

        private Node FindMin(double key)
        {
            var node = FindNodeByKey(key);
            if (node == null)
                return null;
            if (node.Left == null)
                return node;
            var nextNode = node.Left;
            if (nextNode == null)
                return null;
            while (nextNode != null)
            {
                node = nextNode;
                nextNode = nextNode.Left;
            }
            return node;
        }

        private Node FindMax(double key)
        {
            var node = FindNodeByKey(key);
            if (node == null)
                return null;
            if (node.Right == null)
                return node;
            var nextNode = node.Right;
            if (nextNode == null)
                return node.Right;

            while (nextNode != null)
            {
                node = nextNode;
                nextNode = nextNode.Right;
            }
            return node;
        }
        #endregion
        #region Добавление Node 
        public void AddNode(double key)
        {
            if (FindNodeByKey(key) != null) return; // проверка содержимого в дереве
            InsertInTree(key);
            Node newItem = FindNodeByKey(key);
            newItem.Colour = Color.R;
            Add1(newItem);
        }
        //проверка на корень
        private void Add1(Node newItem)
        {
            if (newItem.Parent == null)
            {
                newItem.Colour = Color.B;
                Root = newItem;
            }
            else Add2(newItem);
        }

        private void Add2(Node newItem)
        {
            if (newItem.Parent.Colour == Color.B)
                return;
            else Add3(newItem);
        }

        private void Add3(Node newItem)
        {
            if (Uncle(newItem) != null && Uncle(newItem).Colour == Color.R)
            {
                newItem.Parent.Colour = Color.B;
                Uncle(newItem).Colour = Color.B;
                Grandpa(newItem).Colour = Color.R;
                Add1(Grandpa(newItem));//проверка красного деда на корень
            }
            else Add4(newItem);
        }

        private void Add4(Node newItem)
        {
            var P = newItem.Parent;
            if (newItem == P.Right && P == P.Parent.Left)
            {
                LeftRotate(P);
                newItem = newItem.Left;
            }
            else if (newItem == newItem.Parent.Left && newItem.Parent == P.Parent.Right)
            {
                RightRotate(P);
                newItem = newItem.Right;
            }
            Add5(newItem);
        }

        private void Add5(Node newItem)
        {
            var grandpa = Grandpa(newItem);
            newItem.Parent.Colour = Color.B;
            grandpa.Colour = Color.R;
            if (newItem == newItem.Parent.Left && newItem.Parent == grandpa.Left)

                RightRotate(grandpa);
            else
                LeftRotate(grandpa);
        }
        #endregion
        #region Поиск минимального и максимального Node 
        public Node MinNode()
        {
            if (Root == null)
                return null;
            var node = Root;
            var nextNode = node.Left;
            while (nextNode != null)
            {
                node = nextNode;
                nextNode = nextNode.Left;
            }
            return node;
        }

        public Node MaxNode()
        {
            if (Root == null)
                return null;
            var node = Root;
            var nextNode = node.Right;
            while (nextNode != null)
            {
                node = nextNode;
                nextNode = nextNode.Right;
            }
            return node;
        }
        #endregion
        #region Поиск следующего и предыдущего Node
        
        //ближайшее большее
        public Node FindNextNode(double key)
        {
            var node = FindNodeByKey(key);
            if (node == null)
                return null;
            if (node.Right != null)
                node = FindMin(node.Right.Value);
            else
            {
                if (node.Parent != null)
                {
                    while (node.Parent != null && node != node.Parent.Left)
                    {
                        node = node.Parent;
                    }
                    if (node.Parent != null)
                        node = node.Parent;
                    else
                        return null;
                }
            }
            return node;
        }
        //ближайшее меньшее
        public Node FindPrevNode(double key)
        {
            var node = FindNodeByKey(key);
            if (node == null)
                return null;
            if (node.Left != null)
                node = FindMax(node.Left.Value);
            else
            {
                if (node.Parent != null)
                {
                    while (node.Parent != null && node != node.Parent.Right)
                    {
                        node = node.Parent;
                    }
                    if (node.Parent != null)
                        node = node.Parent;
                    else
                        return null;
                }
            }
            return node;
        }

        #endregion
        #region Удаление Node
        private Node FindBrother(Node n)
        {
            if (n == n.Parent.Left)
                return n.Parent.Right;
            else
                return n.Parent.Left;
        }
        
        private void Replace(Node item, Node Y)
        {
            item.Value = Y.Value;
        }

        public double RemoveNode(double key)
        {
            Node item = FindNodeByKey(key);
            Node Y = null;
            if (item == null)
            {
                return double.NaN;
            }

            if (item.Left == null || item.Right == null)
            {
                Y = item;
            }
            else
            {
                Y = FindNextNode(item.Value);
            }

            Replace(item, Y);//меняем значение без разрыва связи

            if (Y.Colour == Color.R)
            {
                if (Y == Y.Parent.Left)
                {
                    Y.Parent.Left = Y.Right;
                    Y.Left.Parent = Y.Parent;
                }
                else
                {
                    Y.Parent.Right = Y.Right;
                    Y.Right.Parent = Y.Parent;
                }
            }
            else 
                if (Y.Colour == Color.B && Y.Right != null && Y.Right.Colour == Color.R)
                {
                    if (Y == Y.Parent.Left)
                        Y.Parent.Left = Y.Right;
                    else
                        Y.Parent.Right = Y.Right;
                    Y.Right.Parent = Y.Parent;
                    Y.Right.Colour = Color.B;
                }
                else
                {
                    Delete(Y);//смена цвета
                    //удаляем узел
                    Node X = null;
                    if (Y.Left != null)
                    {
                        X = Y.Left;
                    }
                    else
                    {
                        X = Y.Right;
                    }
                    if (X != null)
                    {
                        X.Parent = Y;
                    }
                    if (Y.Parent == null)
                    {
                        Root = X;
                    }
                    else if (Y == Y.Parent.Left)
                    {
                        Y.Parent.Left = X;
                    }
                    else
                    {
                        Y.Parent.Right = X;
                    }
                    if (Y != item)
                    {
                        item.Value = Y.Value;
                    }
                }
            return Root.Value;
        }

        private void Delete(Node n)
        {
            if (n.Parent != null)
                DeleteCase2(n);
        }
        
        private void DeleteCase2(Node n)
        {
            var bro = FindBrother(n);

            if (bro.Colour == Color.R)
            {
                n.Parent.Colour = Color.R;
                bro.Colour = Color.B;
                if (n == n.Parent.Left)
                    LeftRotate(n.Parent);
                else RightRotate(n.Parent);
            }
            DeleteCase3(n);
        }
        
        private void DeleteCase3(Node n)
        {
            var bro = FindBrother(n);
            if (n.Parent.Colour == Color.B && bro.Colour == Color.B && ((bro.Left == null && bro.Right == null) 
                || (bro.Left != null && bro.Left.Colour == Color.B
            && bro.Right != null &&

            bro.Right.Colour == Color.B)))
            {
                bro.Colour = Color.R;
                Delete(n.Parent);
            }
            else DeleteCase4(n);
        }
        
        private void DeleteCase4(Node n)
        {
            var bro = FindBrother(n);
            if (((bro.Left == null && bro.Right == null) || ((bro.Left != null && bro.Left.Colour == Color.B)
            && (bro.Right != null && bro.Right.Colour == Color.B))) 
            && n.Parent.Colour == Color.R && bro.Colour == Color.B)
            {
                bro.Colour = Color.R;
                n.Parent.Colour = Color.B;
            }
            else DeleteCase5(n);
        }
        
        private void DeleteCase5(Node n)
        {
            var bro = FindBrother(n);
            if (bro.Colour == Color.B)
            {
                if (n == n.Parent.Left && bro.Right.Colour == Color.B && bro.Left.Colour == Color.R)
                {
                    bro.Colour = Color.R;
                    bro.Left.Colour = Color.B;
                    RightRotate(bro);
                }
                else if (n == n.Parent.Right && bro.Left.Colour == Color.B && bro.Right.Colour == Color.R)
                {
                    bro.Colour = Color.R;
                    bro.Right.Colour = Color.B;
                    LeftRotate(bro);
                }
            }
            DeleteCase6(n);
        }
        
        private void DeleteCase6(Node n)
        {
            var bro = FindBrother(n);
            bro.Colour = n.Parent.Colour;
            n.Parent.Colour = Color.B;
            if (n == n.Parent.Left)
            {
                bro.Right.Colour = Color.B;
                LeftRotate(n.Parent);
            }
            else
            {
                bro.Left.Colour = Color.B;
                RightRotate(n.Parent);
            }
            #endregion
        }
    }
}