using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public class QuadTree
    {
        private QuadTreeNode root;

        private int maxDepth;

        public QuadTree(Rect sceneBounds, int maxDepth)
        {
            this.maxDepth = maxDepth;
            root = new QuadTreeNode(sceneBounds);
            BuildTree(root, 0);
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depth"></param>
        private void BuildTree(QuadTreeNode node, int depth)
        {
            if (depth >= maxDepth)
            {
                return;
            }

            node.Children = new QuadTreeNode[4];

        }

        private Rect GetWorldSpaceBounds(Mesh mesh,Matrix4x4 matrix)
        {
            Vector3[] vertices = mesh.vertices;
            Vector3 min = matrix.MultiplyPoint3x4(vertices[0]);
            Vector3 max = min;

            foreach (Vector3 vertex in vertices)
            {
                Vector3 worldVertex = matrix.MultiplyPoint3x4(vertex);
                min = Vector3.Min(min, worldVertex);
                max = Vector3.Max(max, worldVertex);
            }

            return new Rect((min + max) / 2, max - min);
        }
    }

}

