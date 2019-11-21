using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/Add New Level", order = 1)]
class Level : ScriptableObject
{
    public string objectName = "New Level";
    public int level;
    public int amountToReload;
    public float multiplier;
}
