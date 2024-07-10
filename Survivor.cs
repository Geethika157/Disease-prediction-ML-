using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Survivor : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform bus;
    public Door doorScript;
    public bool mainSurvivor;
    public bool nextRound;
    public Animator busAnimator;
    public Animator survivorAnim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    public void Follow()
    {
        if (!doorScript.didDoorOpened)
            return;
        int currwave = ZombieManager.instance.currentRoundNumber;
        if (ZombieManager.instance.waveData.zombieDetails[currwave].waveCompleted)
        {
            agent.SetDestination(bus.position);
            survivorAnim.SetBool("iswalking", true);
        }
        if(Vector3.Distance(transform.position,bus.position)<=4&&!mainSurvivor)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bus"))
        {
            if (mainSurvivor)
            {
                if (ZombieManager.instance.currentRoundNumber == 2) // Assuming wave 3 is indexed as 2
                {
                    GameManager.instance.ShowEndPage(true); // Show win panel
                }
                else if (!nextRound)
                {
                    nextRound = true;
                    ZombieManager.instance.StartNextRound();
                    busAnimator.SetBool("StartBus", true);
                }
            }
            gameObject.SetActive(false);
        }
    }

}