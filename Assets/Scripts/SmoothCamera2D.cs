using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class SmoothCamera2D : MonoBehaviour
{

    private float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    [SerializeField]
    private GameObject bg;
    [SerializeField]
    private Tilemap bgTilemap;
    private bool freezeX, freezeY;

    private Transform camTransform;
    private float shakeDuration = 0f;
    private float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;
    Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.position;
        camTransform = this.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        IsInsideMap();
    }

    // Update is called once per frame
    private void Update()
    {
        if (target && shakeDuration <= 0)
        {
            Vector3 pos;

            if (freezeX && !freezeY)
            {
                pos = new Vector3(transform.position.x, target.position.y, target.position.z);
            }

            else if (freezeY && !freezeX)
            {
                pos = new Vector3(target.position.x, transform.position.y, target.position.z);
            }

            else if (freezeY && freezeX)
            {
                pos = new Vector3(transform.position.x, transform.position.y, target.position.z);
            }

            else
            {
                pos = target.position;
            }

            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = pos - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void ShakeCamera(float duration, float sAmount)
    {
        originalPos = transform.position;
        shakeDuration = duration;
        shakeAmount = sAmount;
    }

    public void IsInsideMap()
    {
        float camX = target.transform.position.x;
        float camY = target.transform.position.y;

        float leftSide = camX - Camera.main.orthographicSize * Screen.width / Screen.height;
        float rightSide = camX + Camera.main.orthographicSize * Screen.width / Screen.height;
        float bottonSide = camY - Camera.main.orthographicSize;
        float topSide = camY + Camera.main.orthographicSize;

        if (topSide < bg.transform.position.y + bgTilemap.size.y / 2 - 1 && bottonSide > bg.transform.position.y - bgTilemap.size.y / 2 + 1)
        {
            freezeY = false;
        }

        else
            freezeY = true;

        if (leftSide > bg.transform.position.x - bgTilemap.size.x / 2 + 1 && rightSide < bg.transform.position.x + bgTilemap.size.x / 2 - 1)
        {
            freezeX = false;
        }

        else
            freezeX = true;
    }
}
