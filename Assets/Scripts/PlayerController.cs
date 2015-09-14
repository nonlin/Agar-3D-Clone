using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody PlayerRigidBody;
    public float speed;
    private float speedScaleFactor;
    public Text scoreText;
    public Text sizeText;
    public Text winText;
    private int collectionCount;
    public Camera MainCam;
    public int scoreLimit;
    private float maxSpeed;
    private Vector3 defaultScale;
    // Use this for initialization
    void Start()
    {

        PlayerRigidBody = GetComponent<Rigidbody>();
        //speed = 1.0f;
        speedScaleFactor = 0.15f;
        collectionCount = 0;
        defaultScale = new Vector3(1f, 1f, 1f);
        SetCountText();
        winText.text = "";
        sizeText.text = "Mass " + PlayerRigidBody.mass.ToString();
        maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        //Decrease in size over time until min
        if (transform.localScale.x >= defaultScale.x)
        {

            transform.localScale -= new Vector3(0.0005f, 0.0005f, 0.0005f);
            PlayerRigidBody.mass -= 0.0005f;

            if (Mathf.RoundToInt(PlayerRigidBody.mass) % 1 == 0) {

                sizeText.text = "Mass " + Mathf.RoundToInt(PlayerRigidBody.mass).ToString();
            }
        }

    }
   
    void LateUpdate()
    {
        scoreText.rectTransform.position = MainCam.WorldToScreenPoint(this.transform.position + new Vector3(0.8f, 0.1f, 0.1f));
        sizeText.rectTransform.position = MainCam.WorldToScreenPoint(this.transform.position + new Vector3(0.5f, 0.5f, 0.1f));
        //sizeText.rectTransform.position = Input.mousePosition;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerticle = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVerticle);

        PlayerRigidBody.AddForce(movement * speed);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {

            collectionCount++;
            //Grow in Size
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            //Scale Speed with size
            //speed = speed - (((transform.localScale.x / maxSpeed) * speed) * 0.005f);
            //add mass 
            PlayerRigidBody.mass += .05f;
            SetCountText();
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }

    }

    void SetCountText()
    {

        scoreText.text = "Score " + collectionCount.ToString();
        if (collectionCount >= scoreLimit)
        {

            winText.text = "You've Won!";
        }
    }
}
