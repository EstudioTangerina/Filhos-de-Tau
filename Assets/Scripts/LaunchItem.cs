using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItem : MonoBehaviour {
    public int index;
    public Transform path;
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;

    public int numPoints = 10;
    public float vel = 10;
    public Vector3[] positions = new Vector3[10];
    // Use this for initialization
    void Start () {
            lineRenderer.positionCount = numPoints;
    }
	
	// Update is called once per frame
	void Update () {
        if (path != null)
        {
            DrawQuadraticCurve();
            Move(path);
        }
    }

    public void Move(Transform point)
    {
        if (index < positions.Length)
        {
            float range = Vector2.Distance(transform.position, positions[index]);

            if (range > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, positions[index], vel * Time.deltaTime);
                GetComponent<SpriteRenderer>().sortingLayerName = "GroundTop";
            }
            else
            {
                index++;
                transform.Rotate(0, 0, transform.rotation.z + 35.3f);
                vel += 0.5f;
            }
        }

        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            transform.eulerAngles = new Vector3(0, 0, 69.34f);
        }
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        }
        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}

