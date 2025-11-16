using System;
using UnityEngine;

namespace _Main.Phan1.Bai1.StepSystem
{
    public abstract class Step : MonoBehaviour
    {
        public event Action OnStartStep;
        public event Action OnEndStep;

        protected bool _isStepCompleted = false;

        public bool IsStepCompleted() => _isStepCompleted;

        public virtual void InitStep()
        {
        }

        public virtual void StartStep()
        {
            Debug.Log($"Start {name}");
            OnStartStep?.Invoke();
        }

        public virtual void EndStep()
        {
            Debug.Log($"End {name}");
            OnEndStep?.Invoke();
        }
        
        public void EndStepAndGoToNextStep()
        {
            _isStepCompleted = true;
            EndStep();
        }
    }
}