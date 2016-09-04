using UnityEngine;
using System.Collections;
using ObjectTypes;

public class TestSystemCaller : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		//Debug.Log(SystemLocator.Instance.GetService(SystemDataType.ITEMUSEMANAGER).System_Type);

		SquadManager test = (SquadManager)SystemLocator.Instance.GetService(SystemDataType.SQUADMANAGER);

		Debug.Log(test.Get_String);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
