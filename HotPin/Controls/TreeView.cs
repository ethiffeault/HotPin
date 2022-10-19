using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HotPin.Controls
{
    public class TreeView : System.Windows.Forms.TreeView
    {
        private int nodeImageWidth = 0;
        private string nodeMap = "";
        private StringBuilder newNodeMap = new StringBuilder(128);
        private DragDropEffects dragDropEffects = DragDropEffects.Move;

        private CopyCallback copyCallback;
        private IsLeafCallback isLeakCallback;
        private CanInsertAtRoot canInsertAtRoot;

        public delegate TreeNode CopyCallback(TreeNode source);
        public delegate bool IsLeafCallback(TreeNode parent, TreeNode child);
        public delegate bool CanInsertAtRoot(TreeNode node);

        public Action OnChanges;

        public TreeView()
        {
            AllowDrop = true;
            ShowPlusMinus = true;
            ShowRootLines = true;

            MouseDown += TreeView_MouseDown;
            DragOver += TreeView_DragOver;
            DragEnter += TreeView_DragEnter;
            ItemDrag += TreeView_ItemDrag;
            DragDrop += TreeView_DragDrop;
            KeyDown += TreeView_KeyDown;
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedNode != null)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    SelectedNode.Remove();
                    OnChanges?.Invoke();
                }
            }
        }

        public void SetCopyCallback(CopyCallback callback)
        {
            copyCallback = callback;
        }

        public void SetIsLeakCallback(IsLeafCallback callback)
        {
            isLeakCallback = callback;
        }

        public void SetCanInsertAtRoot(CanInsertAtRoot callback)
        {
            canInsertAtRoot = callback;
        }


        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.nodeMap != "")
            {
                TreeNode movingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                string[] nodeIndexes = this.nodeMap.Split('|');
                TreeNodeCollection insertCollection = Nodes;
                for (int i = 0; i < nodeIndexes.Length - 1; i++)
                {
                    insertCollection = insertCollection[Int32.Parse(nodeIndexes[i])].Nodes;
                }

                if (Nodes == insertCollection)
                {
                    if (canInsertAtRoot != null && !canInsertAtRoot(movingNode))
                    {
                        Invalidate();
                        return;
                    }
                }

                if (insertCollection != null)
                {
                    TreeNode cloneNode = null;

                    if (copyCallback != null)
                        cloneNode = copyCallback(movingNode);
                    else
                        cloneNode = (TreeNode)movingNode.Clone();

                    if (movingNode.IsExpanded)
                        cloneNode.ExpandAll();
                    insertCollection.Insert(Int32.Parse(nodeIndexes[nodeIndexes.Length - 1]), cloneNode);
                    SelectedNode = insertCollection[Int32.Parse(nodeIndexes[nodeIndexes.Length - 1])];
                    if (dragDropEffects == DragDropEffects.Move)
                        movingNode.Remove();
                }

                OnChanges?.Invoke();
            }
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                dragDropEffects = DragDropEffects.Copy;
            }
            else
            {
                dragDropEffects = DragDropEffects.Move;
            }
            DoDragDrop(e.Item, dragDropEffects);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = dragDropEffects;
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedNode = GetNodeAt(e.X, e.Y);
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode NodeOver = GetNodeAt(PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            // A bit long, but to summarize, process the following code only if the nodeover is null
            // and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
            // to be the last node in the branch (so we can allow drag & drop below a parent branch)
            if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
            {
                int OffsetY = PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
                int NodeOverImageWidth = nodeImageWidth + 8;
                Graphics g = CreateGraphics();


                bool leaf = isLeakCallback == null ? false : !isLeakCallback(NodeOver, NodeMoving);

                // Image index of 1 is the non-folder icon
                if (leaf)
                {
                    #region Standard Node
                    if (OffsetY < (NodeOver.Bounds.Height / 2))
                    {
                        //this.lblDebug.Text = "top";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.nodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawLeafTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "bottom";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.nodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Allow drag drop to parent branches
                        TreeNode ParentDragDrop = null;
                        // If the node the mouse is over is the last node of the branch we should allow
                        // the ability to drop the "nodemoving" node BELOW the parent node
                        if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
                        {
                            int XPos = PointToClient(Cursor.Position).X;
                            if (XPos < NodeOver.Bounds.Left)
                            {
                                ParentDragDrop = NodeOver.Parent;

                                if (XPos < (ParentDragDrop.Bounds.Left - nodeImageWidth))
                                {
                                    if (ParentDragDrop.Parent != null)
                                        ParentDragDrop = ParentDragDrop.Parent;
                                }
                            }
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        // Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
                        SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Folder Node
                    if (OffsetY < (NodeOver.Bounds.Height / 3))
                    {
                        //this.lblDebug.Text = "folder top";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.nodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawFolderTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else if ((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
                    {
                        //this.lblDebug.Text = "folder bottom";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.nodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, true);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        DrawFolderTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "folder over";

                        if (NodeOver.Nodes.Count > 0)
                        {
                            NodeOver.Expand();
                            //this.Refresh();
                        }
                        else
                        {
                            #region Prevent the node from being dragged onto itself
                            if (NodeMoving == NodeOver)
                                return;
                            #endregion
                            #region If NodeOver is a child then cancel
                            TreeNode tnParadox = NodeOver;
                            while (tnParadox.Parent != null)
                            {
                                if (tnParadox.Parent == NodeMoving)
                                {
                                    this.nodeMap = "";
                                    return;
                                }

                                tnParadox = tnParadox.Parent;
                            }
                            #endregion
                            #region Store the placeholder info into a pipe delimited string
                            SetNewNodeMap(NodeOver, false);
                            newNodeMap = newNodeMap.Insert(newNodeMap.Length, "|0");

                            if (SetMapsEqual() == true)
                                return;
                            #endregion
                            #region Clear placeholders above and below
                            this.Refresh();
                            #endregion
                            #region Draw the "add to folder" placeholder
                            DrawAddToFolderPlaceholder(NodeOver);
                            #endregion
                        }
                    }
                    #endregion
                }
            }
        }

        #region Helper Methods
        private void DrawLeafTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = CreateGraphics();

            int NodeOverImageWidth = nodeImageWidth + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }

        private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
        {
            Graphics g = CreateGraphics();

            int NodeOverImageWidth = nodeImageWidth + 8;
            // Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
            int LeftPos, RightPos;
            if (ParentDragDrop != null)
                LeftPos = ParentDragDrop.Bounds.Left - (nodeImageWidth + 8);
            else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
        }

        private void DrawFolderTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = CreateGraphics();
            int NodeOverImageWidth = nodeImageWidth + 8;

            int LeftPos, RightPos;
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }

        private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
        {
            Graphics g = CreateGraphics();
            int RightPos = NodeOver.Bounds.Right + 6;
            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

            this.Refresh();
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
        }

        private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
        {
            newNodeMap.Length = 0;

            if (boolBelowNode)
                newNodeMap.Insert(0, (int)tnNode.Index + 1);
            else
                newNodeMap.Insert(0, (int)tnNode.Index);
            TreeNode tnCurNode = tnNode;

            while (tnCurNode.Parent != null)
            {
                tnCurNode = tnCurNode.Parent;

                if (newNodeMap.Length == 0 && boolBelowNode == true)
                {
                    newNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
                }
                else
                {
                    newNodeMap.Insert(0, tnCurNode.Index + "|");
                }
            }
        }

        private bool SetMapsEqual()
        {
            if (this.newNodeMap.ToString() == this.nodeMap)
                return true;
            else
            {
                this.nodeMap = this.newNodeMap.ToString();
                return false;
            }
        }

        #endregion

    }
}
