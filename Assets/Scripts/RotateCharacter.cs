using UnityEngine;
using System.Collections;

public class RotateCharacter : MonoBehaviour 
{
    #region Data Members
    private Rigidbody2D phys;
    Touch touch;
    Transform cachedTransform;
    Vector2 startingSecondTouchPosition;
    Vector3 startingPosition;
    Collider2D col;
    bool isMoving = false;
    bool isRotating = false;

    private float baseAngle = 0.0f;
    float _horizontalLimit = Screen.height * 0.9f , _verticalLimit = Screen.width * 0.9f;
    public GameObject orientationRef;

    [SerializeField]
    float dragSpeed = 0.001f;

    #endregion

    #region Setters & Getters

    #endregion

    #region Built-In Unity Methods

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    #endregion

    #region Helper Classes/Structs

    #endregion
    // Use this for initialization
    void Start () 
    {
        //phys = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        cachedTransform = transform;
        startingPosition = cachedTransform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    if (Input.touchCount == 1)
                    {
                        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 firstTouchPosition = new Vector2(worldPoint.x, worldPoint.y);
                        //startingSecondTouchPosition = Input.GetTouch(1).position;

                        if (col == Physics2D.OverlapPoint(firstTouchPosition))
                        {
                            isMoving = true;
                            isRotating = true;
                            Debug.Log("Rotating");
                        }
                    }
                    else if (Input.touchCount == 2)
                    {
                        startingSecondTouchPosition = Input.GetTouch(1).position;
                    }

                    break;
                case TouchPhase.Moved:
                    
                        if (Input.touchCount == 2)
                        {
                            Vector2 secondTouchPos = Input.GetTouch(1).position;

                            float yDelta = secondTouchPos.y - startingSecondTouchPosition.y;

                            Debug.Log(yDelta);
                            RotateChar(startingSecondTouchPosition, secondTouchPos);

                            startingSecondTouchPosition = secondTouchPos;
                        }

                    break;
                case TouchPhase.Ended:
                    isMoving = false;
                    isRotating = false;
                    break;
            }
        }
    }

    void RotateChar(Vector2 start, Vector2 end)
    {        
        if (end.y > start.y)
        {
            Debug.Log("Increase angle");
            gameObject.transform.Rotate(Vector3.forward, -10.0f);
        }
        else if (end.y < start.y)
        {
            Debug.Log("Decrease angle");
            gameObject.transform.Rotate(Vector3.forward, 10.0f);
        }
    }
}
