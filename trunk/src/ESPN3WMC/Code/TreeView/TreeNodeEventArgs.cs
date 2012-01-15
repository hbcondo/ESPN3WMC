using System;

namespace ESPN3WMC
{
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(TreeNode node)
        {
            Node = node;
        }

        public TreeNode Node
        {
            get { return _node; }
            set { _node = value; }
        }

        #region Fields

        private TreeNode _node = null;

        #endregion

    }
}
