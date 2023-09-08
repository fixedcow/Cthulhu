using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

    public class InventoryOwner : MonoBehaviour, IOwnInventory
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
        #endregion

        #region PublicMethod
        #endregion

        #region PrivateMethod
        #endregion
        public virtual void AddItem(Func<ItemData, int, int> AddItemFunc) { }

        public virtual void OnUseItem(ItemData item, int quantity) { }
    }

}