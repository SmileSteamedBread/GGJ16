﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Gets an Audio Clip from a specified Ballard, Sequence and Chord.")]
	public class GetChordAudioClip : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Ballard Index.")]
		public FsmInt ballardCurrent = -1;

		[RequiredField]
		[Tooltip("The Sequence Index.")]
		public FsmInt sequenceCurrent = -1;

		[RequiredField]
		[Tooltip("The Chord Index.")]
		public FsmInt chordCurrent = -1;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Clip variable to be assigned to.")]
		public FsmObject clip = null;

		[Tooltip("Is Updated Every Frame.")]
		public bool isUpdatedEveryFrame;

		public override void Reset()
		{
			ballardCurrent		= -1;
			sequenceCurrent		= -1;
			chordCurrent		= -1;
			clip				= null;
			isUpdatedEveryFrame = false;
		}

		public override void OnEnter()
		{
			if (isUpdatedEveryFrame == true)
				return;

			AssignChord();
		}

		public override void OnUpdate()
		{
			if (isUpdatedEveryFrame == false)
				return;

			AssignChord();
		}

		private void AssignChord()
		{
			if (ballardCurrent.Value == -1
			|| sequenceCurrent.Value == -1
			|| chordCurrent.Value == -1)
			{
				return;
			}

			BallardData ballard = null;
			if (BallardDatabase.Instance.GetBallard(ballardCurrent.Value, out ballard) == false)
				return;

			SequenceData sequence = null;
			if (ballard.GetSequence(sequenceCurrent.Value, out sequence) == false)
				return;

			ChordData chord = null;
			if (sequence.GetChord(chordCurrent.Value, out chord) == false)
				return;

			if (chord.clip == null)
			{
				Debug.LogError("Chord Clip is null!");
				return;
			}

			clip.Value = chord.clip;
		}

		
	}
}