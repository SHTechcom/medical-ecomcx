using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class LinkItem
    {
        public string name;
        public string link;

        public LinkItem(string name, string link)
        {
            this.name = name;
            this.link = link;
        }
    }

    public class UIListLink : BaseView
    {
        public UILinkItem itemPrefab;
        public Transform container;
        private List<UILinkItem> list = new List<UILinkItem>();
        [SerializeField] private Button closeButton;

        private void Start()
        {
            OnCloseButton(Hide);
        }

        public void OnCloseButton(Action callback)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void SetListLinks(LinkItem[] items)
        {
            if (items.Length > list.Count)
            {
                int currentCount = list.Count;
                for (int i = currentCount; i < items.Length; i++)
                {
                    var item = Instantiate(itemPrefab, container);
                    list.Add(item);
                }
            }
            else if (items.Length < list.Count)
            {
                for (int i = items.Length; i < list.Count; i++)
                {
                    list[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < items.Length; i++)
            {
                list[i].Set(items[i].name, items[i].link);
                list[i].gameObject.SetActive(true);
            }
        }
    }
}
