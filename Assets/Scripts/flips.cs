using UnityEngine;

public class DoubleSidedMesh : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh = CreateDoubleSidedMesh(meshFilter.mesh);
        }
    }

    Mesh CreateDoubleSidedMesh(Mesh originalMesh)
    {
        int vertexCount = originalMesh.vertexCount;
        int[] originalTriangles = originalMesh.triangles;
        Vector3[] originalVertices = originalMesh.vertices;
        Vector3[] originalNormals = originalMesh.normals;
        Vector2[] originalUVs = originalMesh.uv;

        // Create new arrays with double the size
        Vector3[] newVertices = new Vector3[vertexCount * 2];
        Vector3[] newNormals = new Vector3[vertexCount * 2];
        Vector2[] newUVs = new Vector2[vertexCount * 2];
        int[] newTriangles = new int[originalTriangles.Length * 2];

        // Copy original mesh data
        for (int i = 0; i < vertexCount; i++)
        {
            newVertices[i] = originalVertices[i];
            newNormals[i] = originalNormals[i];
            newUVs[i] = originalUVs[i];

            // Create flipped version
            newVertices[i + vertexCount] = originalVertices[i];
            newNormals[i + vertexCount] = -originalNormals[i]; // Flip normals
            newUVs[i + vertexCount] = originalUVs[i];
        }

        // Copy original triangles
        for (int i = 0; i < originalTriangles.Length; i++)
        {
            newTriangles[i] = originalTriangles[i];
        }

        // Create flipped triangles
        for (int i = 0; i < originalTriangles.Length; i += 3)
        {
            newTriangles[i + originalTriangles.Length] = originalTriangles[i] + vertexCount;
            newTriangles[i + 1 + originalTriangles.Length] = originalTriangles[i + 2] + vertexCount;
            newTriangles[i + 2 + originalTriangles.Length] = originalTriangles[i + 1] + vertexCount;
        }

        Mesh doubleSidedMesh = new Mesh();
        doubleSidedMesh.vertices = newVertices;
        doubleSidedMesh.normals = newNormals;
        doubleSidedMesh.uv = newUVs;
        doubleSidedMesh.triangles = newTriangles;

        doubleSidedMesh.RecalculateBounds();
        return doubleSidedMesh;
    }
}
