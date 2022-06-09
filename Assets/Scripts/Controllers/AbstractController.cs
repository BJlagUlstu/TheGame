using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    [Serializable]
    abstract class AbstractController: MonoBehaviour
    {
        protected bool endWork = false;
        public abstract bool Click();
        public abstract bool ClickUp();
        public abstract bool ObserveMousePosition();
        public virtual void PrepareToWork()
        {
            endWork = false;
        }    
        public abstract void StopWork();
    }
}