using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageTimeline", menuName = "Game/Stage Timeline")]
public class StageTimeline : ScriptableObject
{
    public List<SpawnEvent> SpawnEvents = new List<SpawnEvent>();
}