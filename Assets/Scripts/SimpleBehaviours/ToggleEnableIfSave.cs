using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToggleEnableIfSave : MonoBehaviour
{
    [SerializeField] private GameObject objToToggle;
    [SerializeField] private bool _toggleValue;

    private void Start() 
    {
        objToToggle.SetActive(_toggleValue == File.Exists(Application.dataPath + "/Save/saveFile.json"));
    }
}
