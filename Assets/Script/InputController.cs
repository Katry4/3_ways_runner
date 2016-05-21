using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	public enum Gestures
	{
		None = -1,

		DragLeft = 0,
		DragRight = 1,
		DragUp = 2,
		DragDown = 3,
		Tap = 4
	}

	public float minDeltaToDrag = 10;
	public Action<Gestures> PlayerInput;

	private Vector2 _startPos;

	public void DragBegin(BaseEventData e)
	{
		PointerEventData data = (PointerEventData)e;
		_startPos = data.position;
	}

	public void Drag(BaseEventData e)
	{

	}

	public void DragEnd(BaseEventData e)
	{
		PointerEventData data = (PointerEventData)e;
		Vector2 delta = data.position - _startPos;
		if (PlayerInput != null)
		{
			PlayerInput(GetGetsture(delta));
		}
	}

	public void Click(BaseEventData e)
	{
		if (!((PointerEventData)e).dragging)
		{
			Click();
		}
	}

	private void Click()
	{
		if (PlayerInput != null)
		{
			PlayerInput(Gestures.Tap);
		}
		//Debug.Log("Gesture after Click " + Gestures.Tap);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			PlayerInput(Gestures.DragLeft);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			PlayerInput(Gestures.DragRight);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			PlayerInput(Gestures.DragUp);
		}
	}

	private Gestures GetGetsture(Vector2 delta)
	{
		if (delta.magnitude > minDeltaToDrag)
		{
			if (Math.Abs(delta.x) > Math.Abs(delta.y))
			{
				if (delta.x > 0)
				{
					return Gestures.DragRight;
				}
				else
				{
					return Gestures.DragLeft;
				}
			}
			else
			{
				if (delta.y > 0)
				{
					return Gestures.DragUp;
				}
				else
				{
					return Gestures.DragDown;
				}
			}
		}
		else
		{
			return Gestures.Tap;
		}
	}
}
