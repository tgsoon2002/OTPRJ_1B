using UnityEngine;
using System.Collections;

public class SkillSet : MonoBehaviour
{

	public SkillNode endNode;
	public SkillNode startNode;

	public void ResetSkill (Material baseMaterial)
	{
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<MeshRenderer> ().material = baseMaterial;
		}

	}
}
