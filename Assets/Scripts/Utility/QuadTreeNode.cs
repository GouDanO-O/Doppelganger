using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    /// <summary>
    /// 四叉树节点
    /// </summary>
    public class QuadTreeNode
    {
        public Rect Bounds; // 该节点表示的区域

        public Matrix4x4 Matrices; // 该节点中存储的矩阵

        public QuadTreeNode[] Children; // 四个子节点

        private const int MaxMatrices = 4; // 每个节点中最多存储的矩阵数量

        public QuadTreeNode(Rect bounds)
        {
            Bounds = bounds;
            Matrices = new Matrix4x4();
            Children = null;
        }

        public bool IsLeaf()
        {
            return Children == null;
        }

        public void Subdivide()
        {
            float halfWidth = Bounds.width / 2f;
            float halfHeight = Bounds.height / 2f;

            Children = new QuadTreeNode[4];
            Children[0] = new QuadTreeNode(new Rect(Bounds.x, Bounds.y, halfWidth, halfHeight));
            Children[1] = new QuadTreeNode(new Rect(Bounds.x + halfWidth, Bounds.y, halfWidth, halfHeight));
            Children[2] = new QuadTreeNode(new Rect(Bounds.x, Bounds.y + halfHeight, halfWidth, halfHeight));
            Children[3] = new QuadTreeNode(new Rect(Bounds.x + halfWidth, Bounds.y + halfHeight, halfWidth, halfHeight));
        }
    }
}

