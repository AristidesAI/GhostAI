using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Creates a dynamic mesh that represents the player's line of sight, which is
/// blocked by objects on the specified obstacle layer. This simulates the "Fog of War"
/// or "vision cone" effect from games like "Among Us".
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    // ==============
    // PUBLIC VARIABLES (CONFIGURABLE IN INSPECTOR)
    // ==============

    [Header("View Settings")]
    [Tooltip("The maximum radius of the vision cone.")]
    public float viewRadius = 10f;
    [Tooltip("The angle of the vision cone in degrees. 360 for a full circle.")]
    [Range(0, 360)]
    public float viewAngle = 360f;
    [Tooltip("The layer(s) that will block the character's vision.")]
    public LayerMask obstacleMask;

    [Header("Mesh Settings")]
    [Tooltip("The number of rays to cast. Higher numbers create a smoother, more accurate vision mesh but cost more performance.")]
    public int rayCount = 50;
    [Tooltip("How many times the mesh update logic runs per second. Lower values improve performance.")]
    public float meshUpdateFrequency = 0.1f;


    // ==============
    // PRIVATE VARIABLES
    // ==============

    private Mesh viewMesh;
    private MeshFilter viewMeshFilter;
    private Vector3 origin;


    // ==============
    // UNITY LIFECYCLE METHODS
    // ==============

    void Start()
    {
        // Initialize the mesh components
        viewMeshFilter = GetComponent<MeshFilter>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        origin = Vector3.zero; // Start at the object's local center

        // Start a coroutine to update the mesh periodically instead of every frame
        StartCoroutine(UpdateFovMesh(meshUpdateFrequency));
    }


    // ==============
    // COROUTINE FOR MESH UPDATES
    // ==============

    private System.Collections.IEnumerator UpdateFovMesh(float delay)
    {
        while (true)
        {
            DrawFieldOfView();
            yield return new WaitForSeconds(delay);
        }
    }

    // ==============
    // CUSTOM METHODS
    // ==============

    /// <summary>
    /// Main logic to construct the field of view mesh by casting rays.
    /// </summary>
    void DrawFieldOfView()
    {
        float currentAngle = 0f;
        float angleIncrease = viewAngle / rayCount;

        List<Vector3> viewPoints = new List<Vector3>();

        // Cast a ray for each step of the angle
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, AngleToDirection(currentAngle), viewRadius, obstacleMask);
            
            // If the ray hits an obstacle, the vertex is at the hit point.
            if (hit.collider != null)
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            // Otherwise, the vertex is at the maximum view radius in the ray's direction.
            else
            {
                vertex = origin + AngleToDirection(currentAngle) * viewRadius;
            }
            viewPoints.Add(vertex);

            currentAngle += angleIncrease;
        }

        // --- Build the Mesh ---
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        // The first vertex is the origin (the player's position)
        vertices[0] = origin;

        // Add all the calculated view points as vertices
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = viewPoints[i];
        }

        // Create the triangles that form the mesh
        for (int i = 0; i < vertexCount - 2; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // --- Update Mesh Data ---
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals(); // Important for lighting if you use lit shaders
    }
    
    /// <summary>
    /// Helper function to convert an angle in degrees to a normalized direction vector.
    /// </summary>
    private Vector3 AngleToDirection(float angleInDegrees)
    {
        // Convert angle to radians for trigonometric functions
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);
    }
}
