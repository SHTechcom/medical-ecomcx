using Sirenix.Utilities;
using UnityEngine;

namespace Bai11
{
    public enum Gender
    {
        Male,
        Female
    }

    public class LessonController : MonoBehaviour
    {
        private static LinkItem[] linkItems =
        {
            new LinkItem("A", "https://www.youtube.com/watch?v=TpQ2QhPMKpA"),
            new LinkItem("B", "https://www.youtube.com/watch?v=tXWms88tKTY"),
            new LinkItem("C", "https://www.youtube.com/watch?v=kQVYRlKwkpM"),
            new LinkItem("D", "https://www.youtube.com/watch?v=TZaTngS64Bg"),
        };

        public GameObject malePrefab;
        public GameObject femalePrefab;
        private GameObject genderSpawned;
        private Gender gender;
        private bool isShowingInfo = true;

        private UIListLink UIListLink => GameViewManager.Instance.GetView<UIListLink>();
        private UIMain UIMain => GameViewManager.Instance.GetView<UIMain>();
        private UISelectGender UISelectGender => GameViewManager.Instance.GetView<UISelectGender>();
        public Gender Gender => gender;

        private void Start()
        {
            UIListLink.SetListLinks(linkItems);
            UIListLink.OnCloseButton(() =>
            {
                UISelectGender.Show();
                UIListLink.Hide();
            });
            UIListLink.Show();
            //---------------------
            UISelectGender.OnClickSelectMale(() =>
            {
                SelectGender(Gender.Male);
                UISelectGender.Hide();
                UIMain.Show();
            });
            UISelectGender.OnClickSelectFemale(() =>
            {
                SelectGender(Gender.Female);
                UISelectGender.Hide();
                UIMain.Show();
            });
            //---------------------
            UIMain.OnClickedShowInfoButton(ShowHideInfo);
        }

        public void ShowHideInfo()
        {
            var btns = FindObjectsOfType<InfoButton>(true);
            isShowingInfo = !isShowingInfo;
            btns.ForEach(i => i.Display(isShowingInfo));
        }

        public void SelectGender(Gender gender)
        {
            this.gender = gender;
            if (genderSpawned != null)
            {
                Destroy(genderSpawned.gameObject);
            }
            if (gender == Gender.Male)
            {
                genderSpawned = Instantiate(malePrefab);
            }
            else
            {
                genderSpawned = Instantiate(femalePrefab);
            }
        }
    }
}
