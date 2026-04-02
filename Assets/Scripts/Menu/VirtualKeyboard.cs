
using TMPro;
using UnityEngine;

public class VirtualKeyboard : MonoBehaviour
{
    public TMP_InputField targetInpitField;
    public DeathMenu menu;
    public void TypeKey(string character)
    {
        if (targetInpitField != null)
            targetInpitField.text += character;
    }
    public void Backspace()
    {
        if (targetInpitField != null && targetInpitField.text.Length > 0)
            targetInpitField.text = targetInpitField.text.Substring(0, targetInpitField.text.Length - 1);
    }
    public void CloseKeyboard()
    {
        gameObject.SetActive(false);

        if (targetInpitField.text  != null)
        {
            menu.RecordName();
        }
    }
}
