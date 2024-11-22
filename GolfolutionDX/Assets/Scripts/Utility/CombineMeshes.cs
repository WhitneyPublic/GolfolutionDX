using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        CombineObjectMeshes();
    }

    void CombineObjectMeshes()
    {
        // Get all MeshFilters in the children
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        // Create a new mesh
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        // Assign the combined mesh to the parent object
        GetComponent<MeshFilter>().mesh = combinedMesh;
        GetComponent<MeshCollider>().sharedMesh = combinedMesh;
    }
}
