using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObjectTypes;

public class SystemLocator : MonoBehaviour
{
	#region Data Members

	private static SystemLocator _instance;
	private List<ISystemElement> systemsCollection;

	#endregion

	#region Setters & Getters

	public static SystemLocator Instance
	{
		get 
		{ 
			if(!_instance)
			{
				_instance = FindObjectOfType(typeof(SystemLocator)) as SystemLocator;

				if(!_instance)
				{
					throw new UnityException("No SystemLocator GameObject detected in scene!");
				}
				else
				{
					Debug.Log("Init() called");
					_instance.Init();
				}
			}

			return _instance;
		}
	}

	#endregion

	#region Built-in Unity Methods

	#endregion

	#region Public Methods

	/// <summary>
	/// Adds the system to service list.
	/// </summary>
	/// <param name="toAdd">To add.</param>
	public void AddSystemToServiceList(ISystemElement toAdd)
	{
		systemsCollection.Add(toAdd);
	}

	/// <summary>
	/// Gets the service.
	/// </summary>
	/// <returns>The service.</returns>
	/// <param name="type">Type.</param>
	public ISystemElement GetService(SystemDataType type)
	{
		//Declaring local variables
		ISystemElement toReturn; 

		toReturn = systemsCollection.Find(x => x.System_Type == type);

		if(toReturn == null)
		{
			throw new UnityException("System Type not found.");
		}

		return toReturn;
	}

	#endregion

	#region Private Methods

	private void Init()
	{
		systemsCollection = new List<ISystemElement>();
		gameObject.GetComponents<ISystemElement>(systemsCollection);
	}

	#endregion
}
