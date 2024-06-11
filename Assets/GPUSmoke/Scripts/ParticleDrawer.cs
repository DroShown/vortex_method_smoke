using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUSmoke
{
    public class ParticleDrawer<W, T> where W : unmanaged where T : IStruct<W>, new()
    {
        private readonly Material _material;
        private readonly Bounds _bounds;

        public Material Material { get => _material; }

        public ParticleDrawer(Material material, ParticleCluster<W, T> cluster, Bounds bounds)
        {
            _material = material;
            _bounds = bounds;
            cluster.SetMaterialBuffer(_material);
            cluster.SetMaterialStaticUniform(_material);
        }

        public void Draw(bool flip, int count)
        {
            ParticleCluster<W, T>.SetMaterialDynamicUniform(_material, flip, count);
            Graphics.DrawProcedural(_material, _bounds, MeshTopology.Triangles, count * 6);
        }
    }

}