using UnityEngine;
using System.Collections;

public class CameraTouchController : MonoBehaviour 
{
	#region Data Members

	private Ray screenRay;
	private RaycastHit hit;
	private Touch touchInput;
	private TouchPhase touchPhase;
	private int touchCount;

	[SerializeField]
	private float cameraSpeed = 2.0f;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
