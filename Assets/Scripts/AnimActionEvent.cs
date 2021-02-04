using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimActionEvent : MonoBehaviour
{
	[System.Serializable]
	public struct Action
	{
		public string id;
		public UnityEvent actionEvent;
	}

	public Action[] actions;


	Dictionary<string, UnityEvent> registry = new Dictionary<string, UnityEvent>();

	public void Start()
	{
		foreach (Action action in actions)
		{
			registry.Add(action.id, action.actionEvent);
		}
	}

	public void OnAction(AnimationEvent animationEvent)
	{
		string id = animationEvent.stringParameter;
		if (registry.ContainsKey(id))
		{
			registry[id].Invoke();
		}
	}
}