using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace yiikes.UI
{
    public class MenuManager : MonoBehaviour
    {
        List<Menu> menus = new List<Menu>();

        private void Start()
        {
            menus = GetComponentsInChildren<Menu>().ToList();
            OpenMenu(0);
        }

        #region MenuManagement

        public void OpenMenu(int id)
        {
            CloseMenus();
            menus[id].Open();
            EventSystem.current.SetSelectedGameObject(menus[id].FirstButton());
        }

        public void OpenMenu(string menuName)
        {
            int id = 0;
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].name == menuName)
                {
                    id = i; break;
                }
            }
            OpenMenu(id);
        }

        public void CloseMenus()
        {
            for (int i = 0; i < menus.Count; i++)
                menus[i].Close();

            EventSystem.current.SetSelectedGameObject(null);
        }

        #endregion

        #region Menu Buttons

        public void StartGame()
        {
            SceneManager.instance.LoadNextScene();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}
