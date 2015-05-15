﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrueCraft.Client.Linux.Rendering
{
    public class Mesh
    {
        private bool Empty { get; set; }
        public object Data { get; set; }
        public VertexBuffer Verticies { get; set; }
        public IndexBuffer Indicies { get; set; }

        public Mesh(GraphicsDevice device, VertexPositionNormalTexture[] verticies, int[] indicies)
        {
            Empty = verticies.Length == 0 || indicies.Length == 0;
            if (!Empty)
            {
                Verticies = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration,
                    verticies.Length, BufferUsage.WriteOnly);
                Verticies.SetData(verticies);
                Indicies = new IndexBuffer(device, typeof(int), indicies.Length, BufferUsage.WriteOnly);
                Indicies.SetData(indicies);
            }
        }

        ~Mesh()
        {
            if (Verticies != null)
                Verticies.Dispose();
            if (Indicies != null)
                Indicies.Dispose();
        }

        public void Draw(Effect effect)
        {
            if (Empty)
                return;
            effect.GraphicsDevice.SetVertexBuffer(Verticies);
            effect.GraphicsDevice.Indices = Indicies;
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                effect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                    0, 0, Indicies.IndexCount, 0, Indicies.IndexCount / 3);
            }
        }
    }
}