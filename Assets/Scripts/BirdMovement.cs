using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    private FaceTracker faceTracker;

    public float movementSpeed = 1f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (faceTracker != null)
        {
            OpenCVForUnity.CoreModule.Rect latestRect = faceTracker.GetLatestFaceRect();
            
            //Debug.Log("Latest rectangle position: " + latestRect.y);
           
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

                // Basierend auf getrackten Koordinaten des Gesichts Werte mappen
                float mappedX = Mathf.Lerp(xMapMin, xMapMax, Mathf.InverseLerp(xRangeMin, xRangeMax, targetX)) * -1;
                //Debug.Log(mappedX);  
                float mappedY = Mathf.Lerp(yMapMin, yMapMax, Mathf.InverseLerp(yRangeMin, yRangeMax, targetY)) * -1;             

                Vector3 movement = new Vector3(mappedX, mappedY, movementSpeed); 
                movement = movement.normalized * movementSpeed * Time.deltaTime;

                transform.Translate(movement);
            }

        }
    }
}
