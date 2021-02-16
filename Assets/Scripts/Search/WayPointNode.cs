using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointNode : SearchNode
{
    public WayPointNode nextWayPoint;

	private void OnTriggerEnter(Collider other)
	{
		Agent agent = other.GetComponent<Agent>();
		if (agent != null)
        {
			SearchPath searchPath = agent.GetComponent<SearchPath>();
			if (searchPath.Node == this)
			{
				searchPath.Node = nextWayPoint;
			}
        }
	}
}
