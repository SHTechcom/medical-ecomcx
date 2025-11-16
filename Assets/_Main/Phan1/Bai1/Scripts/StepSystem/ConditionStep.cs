using System;
using _Main.Phan1.Bai1.Scripts.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Main.Phan1.Bai1.StepSystem
{
    public class ConditionStep : Step
    {
        public static ConditionStep Current;

        [Title("STEP SYSTEM")] [SerializeField] protected bool autoEnableWhenStartStep = true;
        [SerializeField] protected bool autoDisableWhenEndStep;
        [SerializeField] protected float delayToNextStep;

        public int conditionCount = 1;
        public UnityEvent onStepStarted;
        public UnityEvent onStepEnded;

        [Title("DESCRIPTION")] [SerializeField] private bool showDescription;
        [SerializeField] private string description;

        protected Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        #region PUBLIC METHOD

        public override void StartStep()
        {
            if (autoEnableWhenStartStep) gameObject.SetActive(true);
            base.StartStep();
            Current = this;
            StepConditionController.SetConditionCountAction?.Invoke(conditionCount);
            onStepStarted?.Invoke();

            if (conditionCount == 0) EndStepAndGoToNextStep();
            else
            {
                BottomUIControl.Instance.Active(showDescription);
                BottomUIControl.Instance.SetDescriptionText(description);
            }
        }

        public override void EndStep()
        {
            onStepEnded?.Invoke();

            if (delayToNextStep > 0)
            {
                _tween = DOVirtual.DelayedCall(delayToNextStep, CallEndStep);
            }
            else CallEndStep();

            void CallEndStep()
            {
                BottomUIControl.Instance.Active(false);
                base.EndStep();
                if (autoDisableWhenEndStep) gameObject.SetActive(false);
            }
        }

        #endregion
    }
}