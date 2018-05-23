using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBoss : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2;

    public int numPoints = 50;
    public Vector3[] positions = new Vector3[50];
    private int index;
    public float vel;
    public bool startMove;
    public Transform[] area1WayP;
    public Transform[] area2WayP;
    public Transform[] area2_1WayP;
    public Transform[] area3WayP;
    public Transform[,] pointsAreas = new Transform[4, 3];
    public int stoppedArea;
    public GameObject sayOrder;
    public GameObject sayPickBow;
    public Transform area3Stop;
    private float x, y;
    private Animator anim;
    private void Start()
    {
        x = 0;
        y = -1;
        anim = GetComponent<Animator>();
        //lineRenderer.positionCount = numPoints;

        pointsAreas[0, 0] = area2_1WayP[0];
        pointsAreas[0, 1] = area2_1WayP[1];
        pointsAreas[0, 2] = area2_1WayP[2];

        pointsAreas[1, 0] = area1WayP[0];
        pointsAreas[1, 1] = area1WayP[1];
        pointsAreas[1, 2] = area1WayP[2];

        pointsAreas[2, 0] = area2WayP[0];
        pointsAreas[2, 1] = area2WayP[1];
        pointsAreas[2, 2] = area2WayP[2];

        pointsAreas[3, 0] = area3WayP[0];
        pointsAreas[3, 1] = area3WayP[1];
        pointsAreas[3, 2] = area3WayP[2];

        sayOrder.SetActive(false);
        sayPickBow.SetActive(false);
       
    }

    public void Update()
    {
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);
        DrawQuadraticCurve();
        if (startMove)
            move();

        if (stoppedArea == 2 && !startMove)
        {
            x = -1;
            y = 0;
            sayOrder.SetActive(true);
        }

        if (stoppedArea == 1 && !startMove)
        {
            x = -1;
            y = 0;
        }

        if (stoppedArea == 4 && !startMove || stoppedArea == 5 && !startMove)
        {
            x = 0;
            y = -1;
        }

        if (stoppedArea == 3 && !sayPickBow.activeSelf)
        {
            float r = Vector2.Distance(transform.position, area3Stop.position);

            if (r > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, area3Stop.position, vel * Time.deltaTime);
                SetFacingToPath(area3Stop.position);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
                sayPickBow.SetActive(true);
                x = 0;
                y = -1;
            }

        }


        if (index == positions.Length && startMove)
        {
            startMove = false;
            stoppedArea++;
            if (stoppedArea != 3)
                anim.SetBool("isWalking", false);
            index = 0;
        }
    }

    public void ChangeWaypoints(int x)
    {
        point0 = pointsAreas[x, 0];
        point1 = pointsAreas[x, 1];
        point2 = pointsAreas[x, 2];
        startMove = true;
        DrawQuadraticCurve();
    }

    private void SetFacingToPath(Vector3 path)
    {
        if (path.x > transform.position.x && Mathf.Abs(path.x - transform.position.x) > Mathf.Abs(path.y - transform.position.y))
        {
            //Debug.Log("Right");
            //GetComponent<SpriteRenderer>().flipX = true; // Remove after put a new animation with all 4 way move
            x = 1;
            y = 0;
        }

        else if (path.x < transform.position.x && Mathf.Abs(path.x - transform.position.x) > Mathf.Abs(path.y - transform.position.y))
        {
            //Debug.Log("Left");
            //GetComponent<SpriteRenderer>().flipX = false; // Remove after put a new animation with all 4 way move
            x = -1;
            y = 0;
        }

        else if (path.y > transform.position.y && Mathf.Abs(path.y - transform.position.y) > Mathf.Abs(path.x - transform.position.x))
        {
            //Debug.Log("Up");
            y = 1;
            x = 0;
        }

        else if (path.y < transform.position.y && Mathf.Abs(path.y - transform.position.y) > Mathf.Abs(path.x - transform.position.x))
        {
            //Debug.Log("Down");
            y = -1;
            x = 0;
        }
    }

    private void move()
    {
        if (index < positions.Length)
        {
            float range = Vector2.Distance(transform.position, positions[index]);

            if (range >= 0.3f)
            {
                transform.position = Vector2.MoveTowards(transform.position, positions[index], vel * Time.deltaTime);
                SetFacingToPath(positions[index]);
                anim.SetBool("isWalking", true);
            }
            else
                index++;
        }
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        }
        //lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateLinearBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
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
