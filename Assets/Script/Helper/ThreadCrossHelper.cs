using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GJC.Helper
{
    /// <summary>
    /// �߳̽�����������࣬ʹ��ע�������Ҫ�̳е��������������������е���init�����󷽿�ʹ��
    /// </summary>
    public class ThreadCrossHelper : SingleTon<ThreadCrossHelper>
    {
        class Item
        {
            public DateTime time;
            public Action action;
        }
        private List<Item> items;
        public override void init()
        {
            base.init();
            items = new List<Item>();
        }
        private void Update()
        {
            lock (items)
            {
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    if (DateTime.Now >= items[i].time)
                    {
                        items[i].action();
                        items.RemoveAt(i);
                    }
                }
            }
        }
        /// <summary>
        /// ���߳��еĶ�����ӵ����߳��У��߳��޷�����api��������ɣ�
        /// </summary>
        /// <param name="action">����</param>
        /// <param name="delay">�ӳٵ������������ʱ��</param>
        public void AddMainThread(Action action, float delay = 0)
        {
            lock (items)
            {
                items.Add(new Item
                {
                    action = action,
                    time = DateTime.Now.AddSeconds(delay)
                });
            }
        }
    }
}
