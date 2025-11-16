using _Main.Phan1.Bai1.Scripts.TaskSystem;
using _Main.Phan1.Bai1.Scripts.UI;
using _Main.Phan1.Bai1.StepSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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

        [FoldoutGroup("Xé vỏ kim tiêm")] public BaseActionStep xeVoBomKimTiemStep;

        public void CheckXeVoBomKimTiem(Transform inspectTransform)
        {
            float zAngle = inspectTransform.localEulerAngles.z;
            if (zAngle > 180) zAngle -= 360;

            float yAngle = inspectTransform.localEulerAngles.y;
            if (yAngle > 180) yAngle -= 360;

            if (yAngle >= -20 && yAngle <= 20 &&
                zAngle >= -20 && zAngle <= 20)
            {
                xeVoBomKimTiemStep.CompleteAction();
            }
            else
            {
                WarningUI.Instance?.Show("Xé vỏ sai kỹ thuật");
            }
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}