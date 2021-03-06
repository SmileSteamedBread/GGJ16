﻿using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Checks if a float value falls between two other float values.")]
	public class IsFloatBetween : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The float to check.")]
		public FsmFloat value = 0.0f;

		[RequiredField]
		[Tooltip("The first float variable.")]
		public FsmFloat float1 = 0.0f;

		[RequiredField]
		[Tooltip("The second float variable.")]
		public FsmFloat float2 = 1.0f;

		[Tooltip("Event sent if the Value IS between Float 1 and Float 2")]
		public FsmEvent isTrue = null;

		[Tooltip("Event sent if the Value ISN'T between Float 1 and Float 2")]
		public FsmEvent isFalse = null;

		[Tooltip("Is Updated Every Frame.")]
		public bool isUpdatedEveryFrame;

		public override void Reset()
		{
			value				= 0.0f;
			float1				= 0.0f;
			float2				= 1.0f;
			isTrue				= null;
			isFalse				= null;
			isUpdatedEveryFrame = false;
		}

		public override void OnEnter()
		{
			if (isUpdatedEveryFrame == true)
				return;

			IsBetween();
		}

		public override void OnUpdate()
		{
			if (isUpdatedEveryFrame == false)
				return;

			IsBetween();
		}

		private void IsBetween()
		{
			if (float1.Value < value.Value && value.Value < float2.Value)
			{
				Fsm.Event(isTrue);
			}

			Fsm.Event(isFalse);
		}

		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(isTrue) &&
				FsmEvent.IsNullOrEmpty(isFalse))
				return "Action sends no events!";
			return "";
		}
	}
}
