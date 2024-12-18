using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour{
    public Entity_Statistics EntityStatistics;

    protected virtual void Awake(){
        EntityStatistics = new Entity_Statistics();
    }
}
