/*
//  Copyright (c) 2015 José Guerreiro. All rights reserved.
//
//  MIT license, see http://www.opensource.org/licenses/mit-license.php
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
*/

using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;


namespace cakeslice
{
    //[DisallowMultipleComponent]
    ////[RequireComponent(typeof(Renderer))]
    public class Outline : MonoBehaviour
    {
        #region
        //    private static HashSet<Mesh> registeredMeshes = new HashSet<Mesh>();

        //    public enum Mode
        //    {
        //        OutlineAll,
        //        OutlineVisible,
        //        OutlineHidden,
        //        OutlineAndSilhouette,
        //        SilhouetteOnly
        //    }

        //    public Mode OutlineMode
        //    {
        //        get { return outlineMode; }
        //        set
        //        {
        //            outlineMode = value;
        //            needsUpdate = true;
        //        }
        //    }

        //    public Color OutlineColor
        //    {
        //        get { return outlineColor; }
        //        set
        //        {
        //            outlineColor = value;
        //            needsUpdate = true;
        //        }
        //    }

        //    public float OutlineWidth
        //    {
        //        get { return outlineWidth; }
        //        set
        //        {
        //            outlineWidth = value;
        //            needsUpdate = true;
        //        }
        //    }

        //    [Serializable]
        //    private class ListVector3
        //    {
        //        public List<Vector3> data;
        //    }

        //    [SerializeField]
        //    private Mode outlineMode;

        //    //[SerializeField]
        //    private Color outlineColor = Color.white;

        //    //[SerializeField, Range(0f, 10f)]
        //    private float outlineWidth = 6f;

        //    [Header("Optional")]

        //    [SerializeField, Tooltip("Precompute enabled: Per-vertex calculations are performed in the editor and serialized with the object. "
        //    + "Precompute disabled: Per-vertex calculations are performed at runtime in Awake(). This may cause a pause for large meshes.")]
        //    private bool precomputeOutline;

        //    [SerializeField, HideInInspector]
        //    private List<Mesh> bakeKeys = new List<Mesh>();

        //    [SerializeField, HideInInspector]
        //    private List<ListVector3> bakeValues = new List<ListVector3>();

        //    private Renderer[] renderers;
        //    private Material outlineMaskMaterial;
        //    private Material outlineFillMaterial;

        //    private bool needsUpdate;

        //    void Awake()
        //    {
        //        outlineColor = Color.red;
        //        outlineWidth = 6f;

        //        // Cache renderers
        //        renderers = GetComponentsInChildren<Renderer>();

        //        // Instantiate outline materials
        //        //outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        //        //outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));
        //        outlineMaskMaterial = Resources.Load<Material>(@"Materials/OutlineMask");
        //        outlineFillMaterial = Resources.Load<Material>(@"Materials/OutlineFill");
        //        //outlineMaskMaterial.name = "OutlineMask (Instance)";
        //        //outlineFillMaterial.name = "OutlineFill (Instance)";

        //        // Retrieve or generate smooth normals
        //        LoadSmoothNormals();

        //        // Apply material properties immediately
        //        needsUpdate = true;
        //    }

        //    void OnEnable()
        //    {
        //        foreach (var renderer in renderers)
        //        {

        //            // Append outline shaders
        //            var materials = renderer.sharedMaterials.ToList();

        //            materials.Add(outlineMaskMaterial);
        //            materials.Add(outlineFillMaterial);

        //            renderer.materials = materials.ToArray();
        //        }
        //    }

        //    void OnValidate()
        //    {

        //        // Update material properties
        //        needsUpdate = true;

        //        // Clear cache when baking is disabled or corrupted
        //        if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
        //        {
        //            bakeKeys.Clear();
        //            bakeValues.Clear();
        //        }

        //        // Generate smooth normals when baking is enabled
        //        if (precomputeOutline && bakeKeys.Count == 0)
        //        {
        //            Bake();
        //        }
        //    }

        //    void Update()
        //    {
        //        if (needsUpdate)
        //        {
        //            needsUpdate = false;

        //            UpdateMaterialProperties();
        //        }
        //    }

        //    void OnDisable()
        //    {
        //        foreach (var renderer in renderers)
        //        {

        //            // Remove outline shaders
        //            var materials = renderer.sharedMaterials.ToList();

        //            materials.Remove(outlineMaskMaterial);
        //            materials.Remove(outlineFillMaterial);

        //            renderer.materials = materials.ToArray();
        //        }
        //    }

        //    void OnDestroy()
        //    {

        //        // Destroy material instances
        //        Destroy(outlineMaskMaterial);
        //        Destroy(outlineFillMaterial);
        //    }

        //    void Bake()
        //    {

        //        // Generate smooth normals for each mesh
        //        var bakedMeshes = new HashSet<Mesh>();

        //        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        //        {

        //            // Skip duplicates
        //            if (!bakedMeshes.Add(meshFilter.sharedMesh))
        //            {
        //                continue;
        //            }

        //            // Serialize smooth normals
        //            var smoothNormals = SmoothNormals(meshFilter.sharedMesh);

        //            bakeKeys.Add(meshFilter.sharedMesh);
        //            bakeValues.Add(new ListVector3() { data = smoothNormals });
        //        }
        //    }

        //    void LoadSmoothNormals()
        //    {

        //        // Retrieve or generate smooth normals
        //        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        //        {

        //            // Skip if smooth normals have already been adopted
        //            if (!registeredMeshes.Add(meshFilter.sharedMesh))
        //            {
        //                continue;
        //            }

        //            // Retrieve or generate smooth normals
        //            var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
        //            var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

        //            // Store smooth normals in UV3
        //            meshFilter.sharedMesh.SetUVs(3, smoothNormals);
        //        }

        //        // Clear UV3 on skinned mesh renderers
        //        foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        //        {
        //            if (registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
        //            {
        //                skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
        //            }
        //        }
        //    }

        //    List<Vector3> SmoothNormals(Mesh mesh)
        //    {

        //        // Group vertices by location
        //        var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);

        //        // Copy normals to a new list
        //        var smoothNormals = new List<Vector3>(mesh.normals);

        //        // Average normals for grouped vertices
        //        foreach (var group in groups)
        //        {

        //            // Skip single vertices
        //            if (group.Count() == 1)
        //            {
        //                continue;
        //            }

        //            // Calculate the average normal
        //            var smoothNormal = Vector3.zero;

        //            foreach (var pair in group)
        //            {
        //                smoothNormal += mesh.normals[pair.Value];
        //            }

        //            smoothNormal.Normalize();

        //            // Assign smooth normal to each vertex
        //            foreach (var pair in group)
        //            {
        //                smoothNormals[pair.Value] = smoothNormal;
        //            }
        //        }

        //        return smoothNormals;
        //    }

        //    void UpdateMaterialProperties()
        //    {

        //        // Apply properties according to mode
        //        //outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

        //        switch (outlineMode)
        //        {
        //            case Mode.OutlineAll:
        //                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        //                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        //                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
        //                break;

        //            case Mode.OutlineVisible:
        //                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        //                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        //                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
        //                break;

        //            case Mode.OutlineHidden:
        //                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        //                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
        //                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
        //                break;

        //            case Mode.OutlineAndSilhouette:
        //                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        //                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
        //                outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
        //                break;

        //            case Mode.SilhouetteOnly:
        //                outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
        //                outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
        //                outlineFillMaterial.SetFloat("_OutlineWidth", 0);
        //                break;
        //        }
        //    }
        #endregion

        public Renderer Renderer { get; private set; }
        public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set; }
        public MeshFilter MeshFilter { get; private set; }

        public int color;
        public bool eraseRenderer;

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();
            SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            MeshFilter = GetComponent<MeshFilter>();
        }

        void OnEnable()
        {
            OutlineEffect.Instance?.AddOutline(this);
        }

        void OnDisable()
        {
            OutlineEffect.Instance?.RemoveOutline(this);
        }

        private Material[] _SharedMaterials;
        public Material[] SharedMaterials
        {
            get
            {
                if (_SharedMaterials == null)
                    _SharedMaterials = Renderer.sharedMaterials;

                return _SharedMaterials;
            }
        }
    }
}
