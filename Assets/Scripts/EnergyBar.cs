using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {
    public float curEnergy;
    public float maxEnergy;

    [SerializeField]
    private GameObject energyBar;

    private float calcEnergy;

    private bool recharge;
    private float rechargeTimer;
    private float rechargeMaxTimer;
    // Use this for initialization
    void Start()
    {
        maxEnergy = 50;
        curEnergy = maxEnergy;

        rechargeMaxTimer = 1f;
    }

    private void FixedUpdate()
    {
        calcEnergy = curEnergy / maxEnergy;
        energyBar.GetComponent<Image>().fillAmount = calcEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (curEnergy < 0)
            curEnergy = 0;

        if (curEnergy > maxEnergy)
            curEnergy = maxEnergy;

        else if (curEnergy < maxEnergy)
        {
            recharge = true;
        }

        else
        {
            recharge = false;
            rechargeTimer = 0;
        }

        if(recharge)
        {
            rechargeTimer += Time.deltaTime;

            if(rechargeTimer >= rechargeMaxTimer && curEnergy < maxEnergy)
            {
                curEnergy += 10 * Time.deltaTime;
            }
        }

        if(Input.GetKey(GetComponent<PlayerMovement>().runButton) && GetComponent<PlayerMovement>().canRun && GetComponent<Animator>().GetBool("isWalking") || GetComponent<Animator>().GetBool("Roll"))
        {
            rechargeTimer = 0;
        }
    }

}
