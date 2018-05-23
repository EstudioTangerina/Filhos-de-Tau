using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
    public GameObject target;
	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("x", target, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
	}
}

