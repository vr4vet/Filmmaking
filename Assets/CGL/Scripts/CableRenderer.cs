using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableRenderer : MonoBehaviour
{
    public List<Transform> points;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        /*// Set the color of the line to black
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        // Set the width of the line
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // If you have a large number of points, set the material to null to use a default material.
        // Otherwise, you can set a specific material if needed.
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
*/
        // Update the line with the points
        UpdateLine();
    }

    void UpdateLine()
    {
        if (points == null || points.Count == 0)
        {
            return;
        }

        // Set the number of points in the line
        lineRenderer.positionCount = points.Count;

        // Set the positions of the points in the line
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    // Optional: Update the line dynamically in the Update method
    void Update()
    {
        UpdateLine();
    }
}
