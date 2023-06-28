using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{
    private FaceTracker faceTracker;

    private GameObject collisionCounter;
    private Text textComponent;
    private float collisions = 0f;

    public float initialMovementSpeed = 1.5f;
    private float movementSpeed;

    private float minHeight = 0f;
    private float maxHeight = 5f;
    private float maxLeft = -15f;
    private float maxRight = 15f;

    private float maxDepth= 84.5f;

    private Rigidbody rb;

    OpenCVForUnity.CoreModule.Rect lastLegitRect;

    private float xRangeMin = 0f; // Der minimale Wert des x-Bereichs 
    private float xRangeMax = 700f; // Der maximale Wert des x-Bereichs
    private float xMapMin = -3f; // Der minimale Wert des x-Bereichs nach der Abbildung 
    private float xMapMax = 3f; // Der maximale Wert des x-Bereichs nach der Abbildung 

    private float yRangeMin = 0f; // Der minimale Wert des y-Bereichs 
    private float yRangeMax = 300f; // Der maximale Wert des y-Bereichs 
    private float yMapMin = -3f; // Der minimale Wert des y-Bereichs nach der Abbildung 
    private float yMapMax = 3f; // Der maximale Wert des y-Bereichs nach der Abbildung 

    // Start is called before the first frame update
    void Start()
    {
        faceTracker = FindObjectOfType<FaceTracker>();
        collisionCounter = GameObject.Find("CollisionCounter");
        textComponent = collisionCounter.GetComponent<Text>();
        textComponent.text = "Anzahl Kollisionen: 0";
        rb = GetComponent<Rigidbody>();

        string currentSceneName = SceneManager.GetActiveScene().name;
        //Zylinderparcour ist deutlich groesser skaliert, daher werden Werte angepasst
        if (currentSceneName == "Parcour_Test")
        {
            initialMovementSpeed = 10f;
            maxHeight = 40f;
            minHeight = -40f;
            maxLeft = -40f;
            maxRight = 40f;
            xMapMin = -8f;
            xMapMax = 8f;
            yMapMin = -8f;  
            yMapMax = 8f;
            maxDepth = 1780f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (faceTracker != null)
        {
            OpenCVForUnity.CoreModule.Rect latestRect = faceTracker.GetLatestFaceRect();
            
            //Debug.Log("Latest rectangle position: " + latestRect.width +", "+latestRect.height);
           
            if (latestRect != null)
            {
                //Ueberpruefe, ob Gesicht erkannt wurde. Wenn nicht, nimm zuletzt erkanntes
                if(latestRect.x > 0){
                    lastLegitRect = latestRect;
                }
                else{
                    if(lastLegitRect != null){
                        latestRect = lastLegitRect;
                    }
                }
                
                float targetX = latestRect.x;
                float targetY = latestRect.y;

                movementSpeed = initialMovementSpeed * (latestRect.width/250f) * (latestRect.width/250f) ;

                // Basierend auf getrackten Koordinaten des Gesichts Werte mappen
                float mappedX = Mathf.Lerp(xMapMin, xMapMax, Mathf.InverseLerp(xRangeMin, xRangeMax, targetX)) * -1;  
                float mappedY = Mathf.Lerp(yMapMin, yMapMax, Mathf.InverseLerp(yRangeMin, yRangeMax, targetY)) * -1;             
                
                Vector3 movement = new Vector3(mappedX, mappedY, initialMovementSpeed); 
                movement = movement.normalized * movementSpeed * Time.deltaTime;

                //verhindern, dass der Vogel in den Boden fliegt
                if (transform.position.y + movement.y < minHeight)
                {
                    movement.y = Mathf.Max(minHeight - transform.position.y, minHeight); 
                }
                //verhindern, dass der Vogel ueber die maximalhohe fliegt
                else if (transform.position.y + movement.y > maxHeight)
                {
                    movement.y = Mathf.Min(maxHeight - transform.position.y, 0f); 
                }
                //verhindern, dass der Vogel rechts rausfliegen kann
                if (transform.position.x + movement.x < maxLeft)
                {
                    movement.x = Mathf.Max(maxLeft - transform.position.x, maxLeft);
                }
                else if(transform.position.x + movement.x > maxRight)
                {
                    movement.x = Mathf.Min(maxRight - transform.position.x, maxRight); 
                }
                //Ende erreicht?
                if(transform.position.z < maxDepth){
                    transform.Translate(movement);
                }
            }

        }
    }

    void OnTriggerEnter(Collider collision)
{
    //Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
    //Update Schriftzug bei Kollision mit Hindernis
    if (collision.gameObject.CompareTag("Baum"))
    {   
        collisions++;
        textComponent.text = "Anzahl Kollisionen: "+collisions;
    } 
}

}
