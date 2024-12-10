using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGolemDeath : MonoBehaviour
{
	private void Start()
	{
		if (!QuestManager.Instance._altarIsActive)
		{
			gameObject.SetActive(false);
		}
	}
}
