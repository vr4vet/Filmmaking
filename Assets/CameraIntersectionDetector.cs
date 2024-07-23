using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

[ExecuteAlways]
public class CameraIntersectionDetector : ObjectiveHandler
{
    public Camera cam;
    public List<MeshCollider> meshColliders;
    Plane[] camFrustum;




    new void  Start()
    {
        if (cam == null)
        {
            cam = Camera.main;  // Use the main camera if none is specified
        }
        UpdateFrustumMesh();
        base.Start();
    }

    new private void Update()
    {
        if (thingsOverlaping > 0)
        {
            ObjectiveOn();
        }
        else
        {
            ObjectiveOff();
        }
        base.Update();

    }
    int thingsOverlaping;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoomMic")|| other.CompareTag("Player")|| other.CompareTag("Stand") )
        {
            Debug.Log("Mic in View");
           
           
            thingsOverlaping++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoomMic") || other.CompareTag("Player") || other.CompareTag("Stand"))
        {
            Debug.Log("Mic in View");
            thingsOverlaping--;
        }
    }
    private void FixedUpdate()
    {
        UpdateFrustumMesh();
    }
    void UpdateFrustumMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        Mesh frustumMesh = CreateFrustumMesh(cam);

        meshFilter.mesh = frustumMesh;
        meshCollider.sharedMesh = frustumMesh;
    }

    Mesh CreateFrustumMesh(Camera cam)
    {
        Matrix4x4 frustumMatrix = cam.projectionMatrix * cam.worldToCameraMatrix;
        Matrix4x4 inverseFrustumMatrix = frustumMatrix.inverse;

        Vector3[] frustumCorners = new Vector3[8];
        int[] frustumCornerIndices = new int[8]
        {
            0, 1, 2, 3, // near plane
            4, 5, 6, 7  // far plane
        };

        // Define the eight corners of the frustum in normalized device coordinates
        Vector4[] ndcCorners = new Vector4[8]
        {
            new Vector4(-1,  1, -1, 1),  // near top left
            new Vector4( 1,  1, -1, 1),  // near top right
            new Vector4( 1, -1, -1, 1),  // near bottom right
            new Vector4(-1, -1, -1, 1),  // near bottom left
            new Vector4(-1,  1,  1, 1),  // far top left
            new Vector4( 1,  1,  1, 1),  // far top right
            new Vector4( 1, -1,  1, 1),  // far bottom right
            new Vector4(-1, -1,  1, 1)   // far bottom left
        };

        // Transform the NDC corners to world space
        for (int i = 0; i < 8; i++)
        {
            Vector4 worldCorner = inverseFrustumMatrix * ndcCorners[i];
            frustumCorners[i] = worldCorner / worldCorner.w;
        }

        // Define the triangles of the frustum mesh
        int[] triangles = new int[]
        {
            // Near plane
            0, 1, 2,
            2, 3, 0,
            // Far plane
            4, 5, 6,
            6, 7, 4,
            // Left plane
            0, 3, 7,
            7, 4, 0,
            // Right plane
            1, 5, 6,
            6, 2, 1,
            // Top plane
            0, 4, 5,
            5, 1, 0,
            // Bottom plane
            3, 2, 6,
            6, 7, 3
        };

        Mesh mesh = new Mesh
        {
            vertices = frustumCorners,
            triangles = triangles
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
  
}
