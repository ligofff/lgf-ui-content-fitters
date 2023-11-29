using UnityEngine;

namespace Layout
{
    public class SyncGameobjectActiveState : MonoBehaviour
    {
        public Transform syncTo;

        public bool syncDisable = true;
        public bool syncEnable = true;

        private void OnEnable()
        {
            if (syncTo == null) return;

            if (syncEnable)
            {
                syncTo.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            if (syncTo == null) return;

            if (syncDisable)
            {
                syncTo.gameObject.SetActive(false);
            }
        }
    }
}