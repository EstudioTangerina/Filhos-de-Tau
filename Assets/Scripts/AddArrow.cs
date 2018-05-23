using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArrow : MonoBehaviour {
    private PlayerMovement player;
    public int amount;
    public int index;
    public Transform path;
    public bool isTutorial;
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;

    public int numPoints = 10;
    public float vel = 10;
    public Vector3[] positions = new Vector3[10];
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (isTutorial)
        {
            lineRenderer.positionCount = numPoints;
        }
    }

    private void Update()
    {
        if (isTutorial && path != null)
        {
            DrawQuadraticCurve();
            Move(path);
        }
    }

    public void Change()
    {
        player.ammo += amount;
        if (isTutorial)
            GameObject.FindObjectOfType<Area3>().control = false;
        Destroy(gameObject);
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
            transform.rotation = new Quaternion(0, 0, 0, 0);
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
