using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnPlayCard", order = 1)]
public class SpawnCards : ScriptableObject
{
    public string prefabName;
    public Vector3 spawnPoint;
}
