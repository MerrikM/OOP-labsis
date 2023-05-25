using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace OOP8
{
    public class Observerable
    {
        protected List<IObserver> _observers;

        public Observerable()
        {
            _observers = new List<IObserver>();
        }

        public void addObserver(IObserver o)
        {
            _observers.Add(o);
        }

        public List<IObserver> returnList()
        { return _observers; }

        public void notifyTree() // Добавление элементов в storage справа
        {
            foreach (var obj in _observers)
                obj.OnStorageChanged(this);
        }


        public void notifyObjects(int _x, int _y)
        {
            foreach (var obj in _observers)
            {
                obj.OnObjectChanged(this, _x, _y); // При выделении Head дерева, потомки следуют за ним
            }
        }

        public TreeNode gett()
        {
            return ((TreeObserver)_observers[0]).getNode(); // Получение ноды
        }
    }


    public interface IObserver
    {
        void OnStorageChanged(Observerable who);
        void OnObjectChanged(Observerable who, int _x, int _y);

    }



    public class TreeObserver : IObserver
    {
        public TreeNode tn;


        public TreeObserver()
        {
            tn = new TreeNode();
        }


        public void processNode(TreeNode tn, Model m)
        {
            if (m is Group) // Конвертация объекта в string, для отображения в storage справа
            {
                TreeNode child = new TreeNode(); 
                child.Text = m.GetType().ToString();
                tn.Nodes.Add(child);
                tn.LastNode.Checked = m.getselection();
                List<Model> aaa = ((Group)m).getGroup();
                foreach (var obj in aaa)
                    processNode(child, obj);
            }
            else
            {
                tn.Nodes.Add(m.GetType().ToString()); // Если группа не выделена - удаляем и добавляем в Group справа
                tn.LastNode.Checked = m.getselection();
            }

        }

        public void OnStorageChanged(Observerable who)
        {
            tn.Nodes.Clear();
            tn.Text = "Storage";
            for(int i = 0;i<((Storage)who).getSize();i++)
                processNode(tn, ((Storage)who).getObject(i));
        }


        public TreeNode getNode()
        {
            return tn;
        }


        public void OnObjectChanged(Observerable who, int _x, int _y) {}
    }


    public class MoshniyObserver : IObserver
    {
        protected Storage moshniyStorage;
        public MoshniyObserver() { moshniyStorage = new Storage(); }
        public void AddMoshniyObserver(Model _m) // Head дерева на pictureBox
        {
            moshniyStorage.AddObject(_m);
        }
        public void OnObjectChanged(Observerable who,int _x, int _y)
        {
            for(int i = 0;i<moshniyStorage.getSize(); i++)
            {
                if (!moshniyStorage.getObject(i).isMoshniy((Model)who))
                {
                    moshniyStorage.getObject(i).move_Object(_x, _y); // Следование потомков за мощным
                }
            }
        }
        public void OnStorageChanged(Observerable who) { }

    }
}
