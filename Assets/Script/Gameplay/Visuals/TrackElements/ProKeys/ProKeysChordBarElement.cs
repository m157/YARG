﻿using UnityEngine;
using YARG.Core.Chart;
using YARG.Core.Logging;
using YARG.Gameplay.Player;

namespace YARG.Gameplay.Visuals
{
    public class ProKeysChordBarElement : TrackElement<ProKeysPlayer>
    {
        public ProKeysNote NoteRef { get; set; }

        public override double ElementTime => NoteRef.Time;

        [SerializeField]
        private float _middlePadding;
        [SerializeField]
        private float _endOffsets;

        [Space]
        [SerializeField]
        private Transform _middleModel;
        [SerializeField]
        private Transform _leftModel;
        [SerializeField]
        private Transform _rightModel;

        protected override void InitializeElement()
        {
            // Get the min and max keys
            int? min = null;
            int? max = null;
            foreach (var note in NoteRef.AllNotes)
            {
                if (min is null || note.Key < min)
                {
                    min = note.Key;
                }

                if (max is null || note.Key > max)
                {
                    max = note.Key;
                }
            }

            var minPos = Player.GetNoteX(min!.Value) - _middlePadding;
            var maxPos = Player.GetNoteX(max!.Value) + _middlePadding;

            var size = maxPos - minPos;
            var mid = (minPos + maxPos) / 2f;

            // Transform the middle model
            var cachedTransform = _middleModel.transform;
            cachedTransform.localScale = new Vector3(size, 1f, 1f);
            cachedTransform.localPosition = new Vector3(mid, 0f, 0f);

            // Transform the end models
            _leftModel.localPosition = _leftModel.localPosition.WithX(minPos - _endOffsets);
            _rightModel.localPosition = _rightModel.localPosition.WithX(maxPos + _endOffsets);
        }

        protected override void UpdateElement()
        {
        }

        protected override void HideElement()
        {
        }
    }
}