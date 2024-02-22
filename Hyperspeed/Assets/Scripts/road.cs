using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineRoad : MonoBehaviour
{
    public int resolution;
    public SplineContainer m_splineContainer;
    public float m_width;
    public MeshFilter m_meshFilter;

    float3 position;
    float3 upVector;
    float3 forward;

    List<UnityEngine.Vector3> m_vertsP1 = new List<UnityEngine.Vector3>();
    List<UnityEngine.Vector3> m_vertsP2 = new List<UnityEngine.Vector3>();

    void Update()
    {
        GetVerts();
        BuildMesh();
    }

    private void SampleSplineWidth(float t, out UnityEngine.Vector3 p1, out UnityEngine.Vector3 p2)
    {
        m_splineContainer.Evaluate(0, t, out position, out forward, out upVector);

        float3 right = UnityEngine.Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * m_width);
        p2 = position + (-right * m_width);
    }

    private void GetVerts()
    {
        m_vertsP1 = new List<UnityEngine.Vector3>();
        m_vertsP2 = new List<UnityEngine.Vector3>();

        float step = 1f / (float)resolution;
        for (int i = 0; i < resolution; i++)
        {
            float t = step * i;
            SampleSplineWidth(t, out UnityEngine.Vector3 p1, out UnityEngine.Vector3 p2);
            m_vertsP1.Add(p1);
            m_vertsP2.Add(p2);
        }
    }

    private void BuildMesh()
    {
        Mesh m = new Mesh();
        List<UnityEngine.Vector3> verts = new List<UnityEngine.Vector3>();
        List<int> tris = new List<int>();
        int offset = 0;

        int length = m_vertsP2.Count;

        for (int i = 1; i < length; i++)
        {
            Vector3 p1 = m_vertsP1[i - 1];
            Vector3 p2 = m_vertsP2[i - 1];
            Vector3 p3 = m_vertsP1[i];
            Vector3 p4 = m_vertsP2[i];

            offset = 4 * (i - 1);

            verts.AddRange(new List<Vector3> { p1, p2, p3, p4 });
            tris.AddRange(new List<int> { offset, offset + 2, offset + 3, offset + 3, offset + 1, offset });
        }

        m.SetVertices(verts);
        m.SetTriangles(tris, 0);
        m_meshFilter.mesh = m;
    }
}
