using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour {
    private Animator anim;
    private bool pickUp;
    private bool col;

    [SerializeField]
    private AnimationClip pickUpItemClip;

    [SerializeField]
    private Sprite facingUp;

    private GameObject colGO;

    [SerializeField]
    private GameObject pressE;

    [SerializeField]
    private GameObject inventoryUI;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        pressE.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (col && GetComponent<SpriteRenderer>().sprite == facingUp && anim.GetBool("isWalking") == false)
            pressE.SetActive(true);

        else
            pressE.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E) && anim.GetBool("isWalking") == false && anim.GetBool("PickUp") == false &&anim.GetBool("isAttacking") == false && anim.GetBool("isAiming") == false && anim.GetBool("isMagicActive") == false && pickUp == false)
        {
            if(col && GetComponent<SpriteRenderer>().sprite == facingUp)
                StartCoroutine("PickUpItem");
        }
	}

    IEnumerator PickUpItem()
    {
        pickUp = true;
        anim.SetBool("PickUp", pickUp);

        yield return new WaitForSeconds(pickUpItemClip.length);

        //Pick up the item here
        inventoryUI.SetActive(true);
        colGO.GetComponent<Chest>().showItens = true;
        colGO.GetComponent<Chest>().opened = true;
        colGO.GetComponent<Chest>().recentlyOpened = true;
        col = false;
        pickUp = false;
        anim.SetBool("PickUp", pickUp);
        colGO = null;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Chest")
        {
            if (collision.gameObject.GetComponent<Chest>().opened == false)
            {
                col = true;
                colGO = collision.gameObject;
            }

            else
            {
                //invManager.inventory.SetActive(true);
                collision.gameObject.GetComponent<Chest>().showItens = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            if (collision.gameObject.GetComponent<Chest>().recentlyOpened)
            {
                inventoryUI.SetActive(false);
                collision.gameObject.GetComponent<Chest>().recentlyOpened = false;
            }
            collision.gameObject.GetComponent<Chest>().showItens = false;
            col = false;
            colGO = null;
        }
    }
}
