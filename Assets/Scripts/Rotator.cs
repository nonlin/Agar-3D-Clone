using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float rotationSpeed;
    private Vector3 rotationAngle;
    private Renderer renderer;
    byte randR, randG, randB;

	// Use this for initialization
	void Start () {

        rotationAngle = new Vector3(15,30,45);
        renderer = GetComponent<Renderer>();       
       // Random.seed = (int)Time.time;

        randR = (byte)Random.Range(160, 255);
        randG = (byte)Random.Range(100, 255);
        randB = (byte)Random.Range(100, 255);
        renderer.material.color = new Color32(randR, randG, randB, 255);

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(rotationAngle * Time.deltaTime);
        
        if (gameObject.GetComponent<Renderer>().isVisible == true)
        {

            //this.gameObject.GetComponent<Renderer>().enabled = true;
            //Debug.Log(gameObject.transform.position);
        }
        else { 
            //this.gameObject.GetComponent<Renderer>().enabled = false; 
        }
	}
}
