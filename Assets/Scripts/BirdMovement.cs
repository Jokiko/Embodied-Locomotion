using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;
using UnityEngine.UI;

public class BirdMovement : MonoBehaviour
{
    private FaceTracker faceTracker;

    private GameObject collisionCounter;
    private Text textComponent;
    private float collisions = 0f;

    public float initialMovementSpeed = 1f;
    private float movementSpeed;

    private float maxHeight = 10f;

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

                //Debug.Log("MovementSpeed: "+movementSpeed);


                // Basierend auf getrackten Koordinaten des Gesichts Werte mappen
                float mappedX = Mathf.Lerp(xMapMin, xMapMax, Mathf.InverseLerp(xRangeMin, xRangeMax, targetX)) * -1;
                //Debug.Log(mappedX);  
                float mappedY = Mathf.Lerp(yMapMin, yMapMax, Mathf.InverseLerp(yRangeMin, yRangeMax, targetY)) * -1;             
                
                Vector3 movement = new Vector3(mappedX, mappedY, initialMovementSpeed); 
                movement = movement.normalized * movementSpeed * Time.deltaTime;

                //verhindern,d ass der Vogel in den Boden fliegt
                if (transform.position.y + movement.y < 0f)
                {
                    movement.y = Mathf.Max(0f - transform.position.y, 0f); 
                }
                //verhindern, dass der Vogel ueber die maximalhohe fliegt
                else if (transform.position.y + movement.y > maxHeight)
                {
                    movement.y = Mathf.Min(maxHeight - transform.position.y, 0f); 
                }

                transform.Translate(movement);
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
