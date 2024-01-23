using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class CustomButton : MonoBehaviour
{
    public delegate void OnClickButton(CustomButton sender, Dictionary<string, object> args = null);
    public TextMeshProUGUI text;
    private OnClickButton onClick;
    private Button buttonReference;
    private Dictionary<string, object> buttonArguments;
    
    void Start()
    {
        buttonReference = GetComponent<Button>();
        text = buttonReference.GetComponentInChildren<TextMeshProUGUI>();

        buttonReference.onClick.AddListener(onClickWrappedButton);
    }

    public void addCallback(OnClickButton onClickCallback, Dictionary<string, object> args = null)
    {
        // A dictionary merge function might be needed later on. For now we're probably fine with this.
        buttonArguments = args;

        // Ensure we never double add a callback.
        onClick -= onClickCallback;
        onClick += onClickCallback;
    }

    public void removeSpecificCallback(OnClickButton callback)
    {
        onClick -= callback;
    }

    public void removeAllCallbacks()
    {
        buttonArguments = null;
        onClick = null;
    }

    private void onClickWrappedButton()
    {
        if (onClick != null)
        {
            onClick(this, buttonArguments);
        }
    }
}
