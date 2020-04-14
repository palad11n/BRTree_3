using static Black_Red_tree.RBTree;

namespace Black_Red_tree.Common
{
    public interface ITree
    {
        Node Root { get; set; }
        void AddNode(double key);
        double RemoveNode(double key);
        Node FindNodeByKey(double key);
        Node MinNode();
        Node MaxNode();
        Color GetColorNodeByKey(double key);
        Node FindNextNode(double key);
        Node FindPrevNode(double key);
    }
}
