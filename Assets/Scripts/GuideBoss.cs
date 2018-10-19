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
    public bool startMove = true;
    public Transform[] area1WayP;
    public Transform[] area2WayP;
    public Transform[] area2_1WayP;
    public Transform[] area3WayP;
    public Transform[,] pointsAreas = new Transform[6, 3];
    public int stoppedArea;
    public GameObject sayOrder;
    public GameObject sayPickBow;
    //public Transform area3Stop;
    private float x, y;
    private Animator anim;

    public float timer = 0;
    public float limite;
    public bool ataque = false;
    public Transform[] meio;
    public int pontoParado;

    public GameObject peixe;
    public GameObject jatoDeAgua;

    public GameObject TiroCima;
    public GameObject TiroDireita;
    public GameObject TiroEsquerda;

    bool atacou = false;
    public float tempoParado;
    public float tempoParado2;
    public bool instanciado;

    public GameObject Player;

    public Collider2D[] cols;

    public float[] possibleLimits;
    public Transform middle;
    public GameObject shadow;

    public bool beated;

    public Transform middleZone;

    bool controle;
    bool col;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        x = 0;
        y = -1;
        anim = GetComponent<Animator>();
        lineRenderer.positionCount = numPoints;

        pointsAreas[1, 0] = area1WayP[0];
        pointsAreas[1, 1] = area1WayP[1];
        pointsAreas[1, 2] = area1WayP[2];

        pointsAreas[2, 0] = area2WayP[0];
        pointsAreas[2, 1] = area2WayP[1];
        pointsAreas[2, 2] = area2WayP[2];

        pointsAreas[0, 0] = area2_1WayP[0];
        pointsAreas[0, 1] = area2_1WayP[1];
        pointsAreas[0, 2] = area2_1WayP[2];

        pointsAreas[3, 0] = area3WayP[0];
        pointsAreas[3, 1] = area3WayP[1];
        pointsAreas[3, 2] = area3WayP[2];

        anim.SetInteger("lado", 0);

        DrawQuadraticCurve();

        limite = possibleLimits[Random.Range(0, 3)];
    }

    public void Update()
    {
        if (controle)
            return;

        if(beated)
        {
            GoToPoint(middleZone);
            Player.GetComponent<Collider2D>().enabled = false;
        }

        else
        { 
            if (ataque == false && startMove == true)
            {
                timer += Time.deltaTime;
                anim.SetInteger("lado", 30);

                if (cols[0].enabled == true)
                {
                    cols[0].enabled = false;
                    cols[1].enabled = false;
                    cols[2].enabled = true;
                }
            }
            /*
            if (ataque == false && timer > 1)
            {
                shadow.SetActive(false);
                GetComponent<ReOrderLayer>().enabled = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = 1;
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
            else if (ataque == true)
            {
                shadow.SetActive(true);
                shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                shadow.GetComponent<SpriteRenderer>().sortingOrder = -98;
                GetComponent<ReOrderLayer>().enabled = true;
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
            }*/

            if(col)
            {
                shadow.SetActive(false);
                GetComponent<ReOrderLayer>().enabled = false;
                this.GetComponent<SpriteRenderer>().sortingOrder = 1;
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }

            else
            {
                shadow.SetActive(true);
                shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                shadow.GetComponent<SpriteRenderer>().sortingOrder = -98;
                GetComponent<ReOrderLayer>().enabled = true;
                this.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
            }

            if (timer >= limite)
            {
                pontoParado = Random.Range(0, 3);
                timer = 0;
                ataque = true;
                limite = possibleLimits[Random.Range(0, 3)];
            }

            if (ataque == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, meio[pontoParado].position, vel * Time.deltaTime);
                startMove = false;
                //atacou = false;
            }

            if (transform.position.x == meio[pontoParado].position.x && transform.position.y == meio[pontoParado].position.y)
            {
                tempoParado += Time.deltaTime;
                tempoParado2 += Time.deltaTime;
                anim.SetInteger("lado", pontoParado);

                if (cols[0].enabled == false)
                {
                    cols[0].enabled = true;
                    cols[1].enabled = true;
                    cols[2].enabled = false;
                }

                if (tempoParado > 1 && tempoParado < 1.5f)
                {
                    if (atacou == false)
                    {
                        Ataque(Random.Range(1, 4));
                        atacou = true;
                    }
                }

                else if (tempoParado > 1.5f && tempoParado < 2f)
                {
                    atacou = false;
                }

                else if (tempoParado > 2 && tempoParado < 2.5f)
                {
                    if (atacou == false)
                    {
                        Ataque(Random.Range(1, 4));
                        atacou = true;
                    }
                }

                else if (tempoParado > 2.5f && tempoParado < 3f)
                {
                    atacou = false;
                }

                else if (tempoParado > 3 && tempoParado < 3.5f)
                {
                    if (atacou == false)
                    {
                        Ataque(Random.Range(1, 4));
                        atacou = true;
                    }
                }

                if (tempoParado2 >= 4.5f)
                {
                    atacou = false;
                    tempoParado = 0;
                    ataque = false;
                    startMove = true;
                    anim.SetInteger("lado", 30);
                    cols[0].enabled = false;
                    cols[1].enabled = false;
                    cols[2].enabled = true;
                    tempoParado2 = 0;
                }

            }


            //anim.SetFloat("x", x);
            //anim.SetFloat("y", y);
            DrawQuadraticCurve();
            if (startMove && GetComponent<Animator>().GetBool("Hurt") == false)
                move();

            if (stoppedArea == 1 && !startMove)
            {
                ChangeWaypoints(1);
            }
            if (stoppedArea == 2 && !startMove)
            {
                ChangeWaypoints(2);
            }
            if (stoppedArea == 3 && !startMove)
            {
                ChangeWaypoints(0);
            }
            if (stoppedArea == 4 && !startMove)
            {
                ChangeWaypoints(3);
            }
            if (stoppedArea == 5 && !startMove)
            {
                ChangeWaypoints(4);
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



            if (index == positions.Length && startMove)
            {
                startMove = false;
                stoppedArea++;
                if (stoppedArea > 4)
                {
                    stoppedArea = 1;
                }
                index = 0;
            }
        }
    }

    public void MoveAgain()
    {
        tempoParado = 0;
        ataque = false;
        startMove = true;
        anim.SetInteger("lado", 30);
    }

    public void Ataque(int x)
    {
        

        if(x == 1)
        {
            Debug.Log("Peixada");
            if (pontoParado == 0)
            {
                peixe.transform.position = new Vector2(middle.position.x, transform.position.y - 0.5f);
                //topo
            }
            else if (pontoParado == 1)
            {
                peixe.transform.position = new Vector2(middle.position.x + 0.5f, middle.position.y);
                //direita
            }
            else if (pontoParado == 2)
            {
                peixe.transform.position = new Vector2(middle.position.x - 0.5f, middle.position.y);
                //esquerda
            }
            /*
            else if (pontoParado == 3)
            {
                
                peixe.transform.position = new Vector2(transform.position.x , transform.position.y);
                //baixo
            }*/
            Instantiate(peixe);
            
            //atacou = true;
        }
        if (x == 2)
        {
            jatoDeAgua.transform.position = new Vector2(middle.position.x, middle.position.y);
            Instantiate(jatoDeAgua);
        }
        if (x == 3)
        {
            if (pontoParado == 0)
            {
                TiroCima.transform.position = new Vector2(middle.position.x, middle.position.y);
                Instantiate(TiroCima);/*
                if(tempoParado > 450 && tempoParado < 455)
                {
                    Instantiate(TiroCima);
                }*/
            }
            else if (pontoParado == 1)
            {
                TiroDireita.transform.position = new Vector2(middle.position.x, middle.position.y);
                Instantiate(TiroDireita);/*
                if (tempoParado > 450 && tempoParado < 455)
                {
                    Instantiate(TiroDireita);
                }*/
            }
            else if (pontoParado == 2)
            {
                TiroEsquerda.transform.position = new Vector2(middle.position.x, middle.position.y);
                Instantiate(TiroEsquerda);/*
                if (tempoParado > 450 && tempoParado < 455)
                {
                    Instantiate(TiroEsquerda);
                }*/
            }
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
                //anim.SetBool("isWalking", true);
            }
            else
                index++;
        }
    }

    public void GoToPoint(Transform point)
    {
        float dis = Vector2.Distance(transform.position, point.position);

        if (dis > 0)
        {
            GetComponent<ReOrderLayer>().enabled = true;
            GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
            anim.SetInteger("lado", 30);
            cols[0].enabled = false;
            cols[1].enabled = false;
            cols[2].enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, point.position, vel * Time.deltaTime);
        }

        else
        {
            shadow.SetActive(true);
            shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
            shadow.GetComponent<SpriteRenderer>().sortingOrder = -98;
            GetComponent<ReOrderLayer>().enabled = false;

            if (Player.transform.position.y < transform.position.y)
                GetComponent<SpriteRenderer>().sortingOrder = Player.GetComponent<SpriteRenderer>().sortingOrder - 1;

            else
                GetComponent<SpriteRenderer>().sortingOrder = Player.GetComponent<SpriteRenderer>().sortingOrder + 1;

            anim.SetInteger("lado", 0);
            cols[0].enabled = true;
            cols[1].enabled = true;
            cols[2].enabled = false;
            gameObject.tag = "Untagged";

            FindObjectOfType<TutorialManager>().StartDialogue(2);
            FindObjectOfType<DialogueManager>().lastDialogue = true;
            controle = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossBattleCol")
            col = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossBattleCol")
            col = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossBattleCol")
            col = false;
    }
}
