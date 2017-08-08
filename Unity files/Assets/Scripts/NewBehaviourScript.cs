using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    private Vector3 firstPos;
    private Vector3 secondPos;
    private Vector3 varPos;
    public bool isK;

	// Use this for initialization
	void Start () {
        firstPos = transform.position;
        secondPos = transform.position + new Vector3(3, 0, 0);
        varPos = transform.position;
        isK = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isK == false)
            {
                varPos = secondPos;
                isK = true;
            }else
            {
                varPos = firstPos;
                isK = false;
            }
            
        }
        transform.position = Vector3.Lerp(transform.position, varPos, Time.deltaTime);
    }
}
