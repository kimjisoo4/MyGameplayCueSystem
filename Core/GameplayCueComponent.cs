﻿using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayCueSystem
{
    public abstract class GameplayCueComponent : BaseMonoBehaviour
    {
        private Vector3 _positionOffset;
        private Quaternion _rotationOffset;
        private Vector3 _scaleOffset;

        public Cue Cue { get; set; }

        public void SetOffset(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            _positionOffset = position;
            _rotationOffset = Quaternion.Euler(rotation);
            _scaleOffset = scale;
        }

        public Vector3 Position => Cue.Position + _positionOffset;
        public Quaternion Rotation => Cue.Rotation * _rotationOffset;
        public Vector3 Scale => Cue.Scale.Multiply(_scaleOffset);

        /// <summary>
        /// Cue 를 재생합니다. 
        /// </summary>
        public abstract void Play();
        /// <summary>
        /// Cue 를 정지시킵니다.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Cue 를 일시 정지 시킵니다.
        /// </summary>
        public abstract void Pause();

        /// <summary>
        /// Cue 가 일시 정지 중이면 해제합니다.
        /// </summary>
        public abstract void Resume();

        protected virtual void Finish()
        {
            if (Cue is null)
                return;

            Cue.Remove(this);
            
            Cue = null;
        }
    }
}
