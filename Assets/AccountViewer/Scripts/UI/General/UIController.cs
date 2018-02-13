using System.Collections;
using System.Collections.Generic;
using AccountViewer.Controller;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace AccountViewer.UI
{
    public class UIController : MonoBehaviour
    {
        private static UIController instance;
        public MainController mainController;
        
        private Dictionary<System.Type, UIModule> modules = new Dictionary<System.Type, UIModule>();

        private void Awake()
        {
            SetInstance();
        }

        #region Instance
        private void SetInstance()
        {
            if (instance == null)
            {
                instance = this;
            }

            else
            {
                Debug.LogWarning("Deleting duplicated 'UIController' instance");
                Destroy(this);
            }
        }

        public static UIController GetInstance()
        {
            return instance;
        }
        #endregion

        #region Modules
        public void AddModule(UIModule module) 
        {
            if(!modules.ContainsKey(module.GetType())) 
            {
                modules.Add(module.GetType(), module);
            }
        }

        public T GetModule<T>() where T : UIModule
        {
            System.Type type = typeof(T);
            UIModule module = null;
            modules.TryGetValue(type, out module);
            return (T)module;
        }
        #endregion
    }
}