using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMaterialProperty : MonoBehaviour
{
	private Renderer renderer;
	private MaterialPropertyBlock materialProperties;
	[SerializeField] private string propertyName;
	[SerializeField] private float value = 0;

	private void Start()
	{
		materialProperties = new MaterialPropertyBlock();
		renderer = GetComponent<Renderer>();
		
		renderer.GetPropertyBlock(materialProperties);
		materialProperties.SetFloat(propertyName, value);
		renderer.SetPropertyBlock(materialProperties);
	}

	private void Update()
	{
		renderer.GetPropertyBlock(materialProperties);
		materialProperties.SetFloat(propertyName, value);
		renderer.SetPropertyBlock(materialProperties);
	}
}
