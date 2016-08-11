using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour
{
	public Camera cam;
	Ray ray;
	RaycastHit hit;
	bool drawing = false;


	public Material before;
	public Material after;

	public int skillActiveID;
	public Transform skillContainer;
	public SkillSet[] skillSetA;
	public SkillNode endNode;
	public SkillNode nextNode;
	public bool close = false;
	public SkillSet currentSet;
	// Use this for initialization
	void Start ()
	{
		if (skillSetA [skillActiveID] != null) {
			GameObject temp = Instantiate (skillSetA [skillActiveID].gameObject);
			temp.transform.SetParent (skillContainer);
			currentSet = temp.GetComponent<SkillSet> ();
			skillSetA [skillActiveID].gameObject.SetActive (true);
			nextNode = temp.GetComponent<SkillSet> ().startNode;
			endNode = temp.GetComponent<SkillSet> ().endNode;
			temp.transform.localPosition = Vector3.zero;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.GetMouseButtonDown (0)) {
			GetComponent<TrailRenderer> ().Clear ();

		}
			
		if (Input.GetMouseButton (0)) {
			
			drawing = true;
			transform.position = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 2f));
			ray = cam.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 5f));
			if (Physics.Raycast (ray, out hit, 20f)) {
				if (hit.collider.tag == "skillNode") {
					if (hit.collider.GetComponent<SkillNode> () == nextNode && nextNode != endNode && hit.collider.gameObject.activeSelf == true) {
						Debug.Log ("hit the node");
						hit.collider.GetComponent<MeshRenderer> ().material = after;
						nextNode = hit.collider.GetComponent<SkillNode> ().nexNode;
					} else if (hit.collider.GetComponent<SkillNode> () == nextNode && nextNode == endNode) {
						
						ActiveSkillSuccess ();

						hit.collider.GetComponent<MeshRenderer> ().material = after;
					}

				}
			}
		}
		if (Input.GetMouseButtonUp (0) && drawing == true) {
			if (!close) {
				ActiveSkillFail ();

			}
		}
	}

	void ActiveSkillSuccess ()
	{
		GetComponent<TrailRenderer> ().Clear ();
		close = true;
		Debug.Log ("Finish skill");
	}

	void ActiveSkillFail ()
	{
		currentSet.GetComponent<SkillSet> ().ResetSkill (before);
		GetComponent<TrailRenderer> ().Clear ();
		Debug.Log ("Fail skill");
	}
}
