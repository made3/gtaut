using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector_DestroyableObject : MonoBehaviour {

    public ParticleSystem tomatensplatter;
    public GameObject tomatenParticles;
    public PlayerAbilities playerAbilities;
    private bool isDead;
    public float obstacleTriggerDistance;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(playerAbilities.gameObject.transform.position, this.transform.position) < obstacleTriggerDistance)
        {
            if (playerAbilities.canAttack)
            {
                if (playerAbilities.isFocusing && !isDead)
                {
                    playerAbilities.Attack();
                    Invoke("moveMe", 0.5f);
                    isDead = true;
                }
            }
            if (playerAbilities.attackSlider.value <= playerAbilities.attackSlider.maxValue / 5 && !isDead)
            {
                Invoke("moveMe", 0.5f);
                isDead = true;
            }
        }
    }

    private void moveMe()
    {
        Instantiate(tomatensplatter, transform).transform.parent = tomatenParticles.transform;
        this.gameObject.SetActive(false);
    }
}
