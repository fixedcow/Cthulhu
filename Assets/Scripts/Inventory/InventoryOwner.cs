using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace TH.Core {

    public class InventoryOwner : MonoBehaviour, IOwnInventory
    {
		#region PublicVariables
		#endregion

		#region PrivateVariables
		private Player _player;
        #endregion

        #region PublicMethod
        #endregion

        #region PrivateMethod
        #endregion
        public virtual void AddItem(Func<ItemData, int, int> AddItemFunc) { }

		public virtual int AddItem(ItemData item, int quantity) { 
			return InventorySystem.Instance.GetInventory(this).AddItem(item, quantity);
		}

		public virtual bool IsItemAvailableToInventory(ItemData item, int quantity) {
			return InventorySystem.Instance.GetInventory(this).IsItemAvailableToInventory(item, quantity);
		}
        public virtual void OnUseItem(ItemData item, int quantity) { }

		public void OnSelectItem(int idx)
		{
			_player ??= GameManager.Instance.GetPlayer();
			_player.HandleItem(idx);
		}
	}
}