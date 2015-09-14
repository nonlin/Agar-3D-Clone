using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawner : MonoBehaviour {

    public int spawnAmount;
    public GameObject PickUpPrefab;
    public Camera MainCam;
    float posX, posY, posZ;
    float randSide;
    Vector3 randomPosition;
    private GameObject ground;
    public GameObject currentGO;
    private Plane[] planes;
    public List<GameObject> SpawnedList = new List<GameObject>();
    public List<Vector3> SpawnPositions = new List<Vector3>();
	// Use this for initialization
	void Start () {

        ground = GameObject.FindGameObjectWithTag("Ground");
        SpawnPickUpObject();
        planes = GeometryUtility.CalculateFrustumPlanes(MainCam);
	}

	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(MainCam.velocity, Vector3.zero) > 5.0f)
        {
            for (int i = 0; i < SpawnPositions.Count - 1; i++)
            {
                /*if (GeometryUtility.TestPlanesAABB(planes, SpawnedList[i].GetComponent<Collider>().bounds))
                {

                    SpawnedList[i].GetComponent<Renderer>().enabled = true;
                }
                else {

                    SpawnedList[i].GetComponent<Renderer>().enabled = false;
                }*/
                //if random position is ever in view, spawn object
                //if (MainCam.WorldToViewportPoint(SpawnPositions[i]).x <= 1 && MainCam.WorldToViewportPoint(SpawnPositions[i]).x >= 0 && MainCam.WorldToViewportPoint(SpawnPositions[i]).y <= 1 && MainCam.WorldToViewportPoint(SpawnPositions[i]).y >= 0)
                if (InfiniteCameraCanSeePoint(MainCam, SpawnPositions[i]))
                {
                    //Debug.Log("Got 1");
                    currentGO = Instantiate(PickUpPrefab, SpawnPositions[i], Quaternion.identity) as GameObject;
                    currentGO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    SpawnedList.Add(currentGO);
                    //Remove pos added as to not add there again
                    SpawnPositions.RemoveAt(i);
                }
            }
        }
        else {

            for (int i = 0; i < SpawnedList.Count - 1; i++)
            {
                if (!InfiniteCameraCanSeePoint(MainCam, SpawnedList[i].transform.position)) {

                    Destroy(SpawnedList[i]);
                    SpawnPositions.Add(SpawnedList[i].transform.position);
                    SpawnedList.RemoveAt(i);
                    
                }

            }

        }
	}

    public bool InfiniteCameraCanSeePoint(Camera camera, Vector3 point)
    {
        Vector3 viewportPoint = camera.WorldToViewportPoint(point);
        return (viewportPoint.z > 0 && (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
    }

    public void SpawnPickUpObject() { 
    
        for (int i = 0; i < spawnAmount; i++) {

            RandomSpawnLocation();
            //currentGO = Instantiate(PickUpPrefab, randomPosition, Quaternion.identity) as GameObject;
            //currentGO.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            //SpawnedList.Add(currentGO);
            //Debug.Log(randomPosition);

        }
	
    }
    public void RandomSpawnLocation() {

        //X might be pos or neg
        randSide = Random.Range(0, 10);
        if (randSide <= 5)
        {
            posX = Random.Range(1, ((ground.GetComponent<Renderer>().bounds.size.x / 2) - 1));
        }
        else
        {
            posX = Random.Range(((ground.GetComponent<Renderer>().bounds.size.x / 2)) * -1, -1);
        }
        //z might be pos or neg
        randSide = Random.Range(0, 10);
        if (randSide <= 5)
        {
            posZ = Random.Range(1, ((ground.GetComponent<Renderer>().bounds.size.z / 2) - 1));
        }
        else
        {
            posZ = Random.Range(((ground.GetComponent<Renderer>().bounds.size.z / 2)) * -1, -1);
        }

        posY = 0.5f;
        randomPosition = new Vector3(posX, posY, posZ);
        SpawnPositions.Add(randomPosition);
        //transform.position = randomPosition;
    
    }
}
