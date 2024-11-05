using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITransitionController : MonoBehaviour
{
    List<UIAnimator> animators = new List<UIAnimator>();

    void Start()
    {
        animators = transform.GetComponentsInChildren<UIAnimator>().ToList();
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            SceneManager.instance.LoadScene(1);
    }

    public void Open(int id = 0)
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].Open(true);
        animators[id].Close(true);
        animators[id].Open();
    }

    public void Open(string name)
    {
        name += " Transition";
        int id = 0;
        for (int i = 0; i < animators.Count; i++)
        {
            if (animators[i].name == name)
                id = i;
        }
        Open(id);
    }

    public void Close(int id = 0)
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].Open(true);
        animators[id].Close();
    }

    public void Close(string name)
    {
        name += " Transition";
        int id = 0;
        for (int i = 0; i < animators.Count; i++)
        {
            if (animators[i].name == name)
                id = i;
        }
        Close(id);
    }
}
