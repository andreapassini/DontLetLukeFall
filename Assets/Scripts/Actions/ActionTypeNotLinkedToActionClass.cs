using System;
using UnityEngine;

namespace DLLF
{
    public class ActionTypeNotLinkedToActionClass : Exception
    {
        public ActionTypeNotLinkedToActionClass()
        {
        }

        public ActionTypeNotLinkedToActionClass(string message) : base(message)
        {
            Debug.LogError(message);
        }
    }
}