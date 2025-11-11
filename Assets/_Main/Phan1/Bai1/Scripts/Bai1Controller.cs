using _Main.Phan1.Bai1.Scripts.TaskSystem;
using _Main.Phan1.Bai1.Scripts.UI;
using _Main.Phan1.Bai1.StepSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts
{
    public class Bai1Controller : MonoBehaviour
    {
        public void ShowCheckListCheckDrug(CheckListData data)
        {
            CheckListUI.Instance.Show(data, OnCheckDrug);
        }

        private void OnCheckDrug(bool isCorrect)
        {
            if (isCorrect)
            {
                CheckListUI.Instance.Hide();
                StepConditionController.MinusConditionCount();
            }
            else
            {
            }
        }

        public BaseActionStep duaKimVaoOngStep;
        public BaseActionStep rutThuocStep;
        public BaseActionStep xoayTrucVatStep;
        public GameObject trucVatCheckImage;

        public void CheckDuaKimVaoOng(Slider slider)
        {
            if (slider.value / (slider.maxValue - slider.minValue) >= 0.9f)
            {
                duaKimVaoOngStep.CompleteAction();
            }
        }

        public void CheckRutThuoc(Slider slider)
        {
            if (slider.value / (slider.maxValue - slider.minValue) <= 0.1f)
            {
                rutThuocStep.CompleteAction();
            }
        }

        public void XoayTrucVat(float value)
        {
            trucVatCheckImage.SetActive(value >= 0.4f && value <= 0.6f);
        }

        public void CheckTrucVatTrue(Slider slider)
        {
            if (slider.value >= 0.4f && slider.value <= 0.6f)
            {
                xoayTrucVatStep.CompleteAction();
            }
        }
    }
}