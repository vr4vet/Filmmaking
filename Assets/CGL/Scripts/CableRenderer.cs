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
