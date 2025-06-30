#if SCOR_ENABLE_CINEMACHINE
using UnityEngine;
using Unity.Cinemachine;
using StudioScor.Utilities;

namespace StudioScor.GameplayCueSystem
{
    public class CinemachineImpulseCue : GameplayCueComponent
    {
        [Header(" [ Cinemachine Impulse Cue ] ")]
        [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
        [SerializeField] private Vector3 _direction = Vector3.up;
        [SerializeField] private float _force = 1f;
        [SerializeField] private float _durataion = 0.2f;
        [SerializeField] private bool _useLegacy = false;
        [SerializeField][SCondition(nameof(_useLegacy))] private float _impactPointRatio = 1f;
        [SerializeField][SCondition(nameof(_useLegacy))] private float _dissipationDistanceRatio = 100f;

        private float _remainTime = 0f;


        private void OnValidate()
        {
#if UNITY_EDITOR
            if(!_cinemachineImpulseSource)
                _cinemachineImpulseSource = GetComponentInChildren<CinemachineImpulseSource>();
#endif
        }
        public override void Pause()
        {
            
        }

        public override void Play()
        {
            Vector3 position = Position;
            Vector3 shakeDirection = Rotation * _direction;
            float cueScale = Scale.x;

            float shakeForce = cueScale * _force;
            Vector3 velocity = shakeDirection * shakeForce;

            if(_useLegacy)
            {
                var impulseDefinition = _cinemachineImpulseSource.ImpulseDefinition;

                impulseDefinition.ImpactRadius = cueScale * _impactPointRatio;
                impulseDefinition.DissipationDistance = cueScale * _dissipationDistanceRatio;
                impulseDefinition.TimeEnvelope.SustainTime = Mathf.Max(0, (cueScale * _durataion) - impulseDefinition.TimeEnvelope.DecayTime);
            }
            else
            {
                _remainTime = cueScale * _durataion;
                _cinemachineImpulseSource.ImpulseDefinition.ImpulseDuration = _remainTime;
            }

            _cinemachineImpulseSource.GenerateImpulseAtPositionWithVelocity(position, velocity);

            Finish();
        }


        public override void Resume()
        {
        }

        public override void Stop()
        {
        }
    }
}
#endif
 