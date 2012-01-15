using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.MediaCenter.UI;

namespace ESPN3WMC
{
    public class TreeNode : ModelItem
    {
        public event EventHandler ChildNodesRequestCompleted;
        public event EventHandler Collapsed;
        public event EventHandler Expanded;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public ArrayListDataSet ChildNodes
        {
            get { return _childNodes; }
            set { _childNodes = value; }
        }
        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public bool HasChildNodes
        {
            get { return _hasChildNodes; }
            set { _hasChildNodes = value; }
        }
        public BooleanChoice Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public TreeView TreeView
        {
            get { return _treeView; }
            set { _treeView = value; }
        }
        public Inset LevelMargin
        {
            get { return new Inset((50 * Level), 0, 0, 0); }
        }

        public TreeNode(String title)
        {
            Title = title;
            Checked.ChosenChanged += new EventHandler(delegate(object sender, EventArgs e)
            {
                if (Checked.Value) TreeView.CheckedNode = this;
            });
        }

        public virtual void OnCollapsed()
        {
            if (Collapsed != null)
                Collapsed(this, EventArgs.Empty);
        }
        public virtual void OnExpanded()
        {
            if (Expanded != null)
                Expanded(this, EventArgs.Empty);
        }
        public virtual void GetChildNodes()
        {
            if (ChildNodesRequestCompleted != null)
                ChildNodesRequestCompleted(this, EventArgs.Empty);
        }

        #region Fields

        private string _title = string.Empty;
        private ArrayListDataSet _childNodes = new ArrayListDataSet();
        private bool _hasChildNodes = false;
        private int _level = 0;
        private BooleanChoice _checked = new BooleanChoice();
        private TreeView _treeView = null;

        #endregion

    }

}
