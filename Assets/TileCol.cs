using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCol : MonoBehaviour {
    private Vector3Int pos ;
    public Tilemap tilemap;
    public TileBase tileBase;
    public string colTag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.gameObject.tag == colTag)
        {
            pos = tilemap.WorldToCell(collision.GetComponent<Rigidbody2D>().position);
            if(tilemap.HasTile(pos))
                tilemap.SetTile(pos, tileBase);

            if (tilemap.HasTile(pos - new Vector3Int(1, 1, 0)))
                tilemap.SetTile(pos - new Vector3Int(1, 1, 0), tileBase);

            if (tilemap.HasTile(pos - new Vector3Int(1, 0, 0)))
                tilemap.SetTile(pos - new Vector3Int(1, 0, 0), tileBase);

            if (tilemap.HasTile(pos - new Vector3Int(0, 1, 0)))
                tilemap.SetTile(pos - new Vector3Int(0, 1, 0), tileBase);

            if (tilemap.HasTile(pos + new Vector3Int(1, 1, 0)))
                tilemap.SetTile(pos + new Vector3Int(1, 1, 0), tileBase);

            if (tilemap.HasTile(pos + new Vector3Int(1, 0, 0)))
                tilemap.SetTile(pos + new Vector3Int(1, 0, 0), tileBase);

            if (tilemap.HasTile(pos + new Vector3Int(0, 1, 0)))
                tilemap.SetTile(pos + new Vector3Int(0, 1, 0), tileBase);
        }

    }
}
