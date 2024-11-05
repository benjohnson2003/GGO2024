using UnityEngine;
using UnityEngine.UI;

namespace yiikes.UI
{
    public class Menu : MonoBehaviour
    {
        public GameObject FirstButton()
        {
            return GetComponentInChildren<Button>().gameObject;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
