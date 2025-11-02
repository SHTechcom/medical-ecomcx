using TMPro;
using UnityEngine;

public class PatientRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ageText;

    public PatientRecord SetName(string name)
    {
        nameText?.SetText(name);
        return this;
    }

    public PatientRecord SetAge(string age)
    {
        ageText?.SetText($"{age} tuổi");
        return this;
    }

    public PatientRecord Set(string age)
    {
        ageText?.SetText(age);
        return this;
    }
}
