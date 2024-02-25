using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using WinFormsApp1.Model;
using WinFormsApp1.Persistance;
using BrightIdeasSoftware;
using System.Collections.Generic;
using static BrightIdeasSoftware.TreeListView;
using System.Windows.Forms;
using System.Collections;
using WinFormsApp1.Infrastructure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.ListView;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private static Repository<Entities> repository = new();
        private static ApplicationDbContext appDbContext = new();
        private List<Entities> data = new();


        public Form1()
        {
            InitializeComponent();
            GetTreeFromBd();
            FillTreeView();

            SimpleDropSink sink = (SimpleDropSink)treeListView1.DropSink;
            sink.AcceptExternal = true;
            sink.CanDropBetween = true;
            sink.CanDropOnBackground = true;

            treeListView1.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
        }

        public void GetTreeFromBd()
        {
            treeListView1.Clear();
            data = appDbContext.GetTree();
        }

        private void FillTreeView()
        {
            this.treeListView1.CanExpandGetter = x => (x as Entities).Childrens.Count > 0;
            this.treeListView1.ChildrenGetter = x => (x as Entities).Childrens;

            AddColumnsTreeListView();
            RefreshTreeListView();
        }

        private void AddColumnsTreeListView()
        {
            OLVColumn IDEntity = new OLVColumn("ID", "IDEntity");
            IDEntity.AspectGetter = delegate (object x) { return ((Entities)x).Id; };
            OLVColumn NameEntity = new OLVColumn("Name", "NameEntity");
            NameEntity.AspectGetter = delegate (object x) { return ((Entities)x).Name; };
            OLVColumn PathEntity = new OLVColumn("Path", "PathEntity");
            PathEntity.AspectGetter = delegate (object x) { return ((Entities)x).Path; };
            OLVColumn TypeEntity = new OLVColumn("Type", "TypeEntity");
            TypeEntity.AspectGetter = delegate (object x) { return ((Entities)x).Type; };
            OLVColumn ParentId = new OLVColumn("Parent Id", "ParentId");
            ParentId.AspectGetter = delegate (object x) { return ((Entities)x).ParentId; };

            this.treeListView1.Columns.Add(IDEntity);
            this.treeListView1.Columns.Add(NameEntity);
            this.treeListView1.Columns.Add(PathEntity);
            this.treeListView1.Columns.Add(TypeEntity);
            this.treeListView1.Columns.Add(ParentId);

            treeListView1.Columns[0].Width = 100;
            treeListView1.Columns[1].Width = 142;
            treeListView1.Columns[2].Width = 279;
            treeListView1.Columns[3].Width = 100;
            treeListView1.Columns[4].Width = 80;
        }

        private void RefreshTreeListView()
        {
            this.treeListView1.Roots = data;
            treeListView1.ExpandAll();
        }

        private void treeListView1_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            e.Handled = true;
            e.Effect = DragDropEffects.None;
            Entities target = e.TargetModel as Entities;

            if (e.SourceModels.Contains(target))
            {
                e.InfoMessage = "Невозможно переместить в самого себя";
            }
            else
            {
                var sourceModel = e.SourceModels.Cast<Entities>();
                try
                {
                    if (target != null)
                    {
                        if (sourceModel.Any(x => target.IsAncestor(x) != null ? target.IsAncestor(x) : true))
                        {
                            e.InfoMessage = "Невозможно переместить объект";
                        }
                        else if (target.Type == "File")
                        {
                            e.InfoMessage = "Невозможно переместить объект в файл";
                        }
                        else if (sourceModel.Any(x => x.Childrens.Contains(target)))
                        {
                            e.InfoMessage = "Нельзя поместить объект в дочерний объект";
                        }
                        else if (sourceModel.Any(x => x.ParentId == target.Id))
                        {
                            e.InfoMessage = "Объект уже содержится в каталоге";
                        }
                        else
                        {
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                    else
                    {
                        e.InfoMessage = "Невозможно переместить объект";
                    }
                }
                catch
                {
                    e.InfoMessage = "Невозможно переместить объект";
                }

            }
        }

        private void HandleModelDropped(object sender, ModelDropEventArgs e)
        {
            switch (e.DropTargetLocation)
            {
                case DropTargetLocation.AboveItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (Entities)e.TargetModel,
                        e.SourceModels,
                        0);
                    break;
                case DropTargetLocation.BelowItem:
                    MoveObjectsToSibling(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (Entities)e.TargetModel,
                        e.SourceModels,
                        1);
                    break;
                case DropTargetLocation.Item:
                    MoveObjectsToChildren(
                        e.ListView as TreeListView,
                        e.SourceListView as TreeListView,
                        (Entities)e.TargetModel,
                        e.SourceModels);
                    break;
                default:
                    return;
            }
            GetTreeFromBd();
            FillTreeView();
            //e.RefreshObjects();
        }

        private void MoveObjectsToSibling(TreeListView targetTree, TreeListView sourceTree, Entities target, IList toMove, int siblingOffset)
        {
            ArrayList sourceRoots = sourceTree.Roots as ArrayList;
            ArrayList targetRoots = targetTree == sourceTree ? sourceRoots : targetTree.Roots as ArrayList;

            foreach (Entities x in toMove)
            {
                if (x.ParentId == null)
                {
                    sourceRoots.Remove(x);
                }
                else
                {
                    x.Childrens.Remove(x);
                }
                x.ParentId = target.ParentId;
                x.SetNewPath(target, x);
                appDbContext.Move(x);
            }

            if (target.ParentId == null)
            {
                targetRoots.InsertRange(targetRoots.IndexOf(target) + siblingOffset, toMove);
            }
            else
            {
                target.Childrens.InsertRange(target.Childrens.IndexOf(target) + siblingOffset, toMove.Cast<Entities>());
            }
            if (targetTree == sourceTree)
            {
                sourceTree.Roots = sourceRoots;
            }
            else
            {
                sourceTree.Roots = sourceRoots;
                targetTree.Roots = targetRoots;
            }
        }

        private void MoveObjectsToChildren(TreeListView targetTree, TreeListView sourceTree, Entities target, IList toMove)
        {
            foreach (Entities x in toMove)
            {
                if (x.ParentId == null)
                {
                    sourceTree.RemoveObject(x);
                }
                else
                {
                    x.Childrens.Remove(x);
                }
                x.ParentId = target.Id;
                x.SetNewPath(target, x);
                target.Childrens.Add(x);

                appDbContext.Move(x);
            }
        }

        private void treeListView1_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (e.Model != null && e.Item != null)
            {
                var hit = treeListView1.GetItemAt(e.Location.X, e.Location.Y);
                if (hit != null && hit.Index >= 0)
                {
                    treeListView1.ContextMenuStrip = ModesMenuStrip;
                    treeListView1.ContextMenuStrip.Show(treeListView1, e.Location);
                }
            }
        }

        private void TreeListView_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Column.Index == 1)
            {
                e.Control.Enabled = true;
            }
            else
            {
                e.Control.Enabled = false;
            }
        }

        private void TreeListView_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            if (e.Column.Index == 1)
            {
                Entities obj = (Entities)e.RowObject;
                Entities parent = (Entities)treeListView1.GetParent(obj);

                var regexItem = new Regex("^[a-zA-Z0-9_.!() ]*$");
                if (regexItem.IsMatch(e.Control.Text))
                {
                    if (parent.Childrens.Where(x => x.Id != obj.Id).Any(x => x.Name.Contains(e.Control.Text)) == false)
                    {
                        string lastName = obj.Name;
                        obj.Name = e.Control.Text;
                        obj.SetNewPath(lastName, obj.Name);

                        appDbContext.Rename(obj);

                        treeListView1.RefreshObject(obj);
                        treeListView1.GetChildren(obj);
                    }
                    else
                    {
                        MessageBox.Show("Данное имя уже существует");
                    }
                }
                else
                    MessageBox.Show("Введены не допустимые символы");
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string numberPattern = " ({0})";

            if (this.treeListView1.SelectedObject == null)
            {
                MessageBox.Show("Перед выполнение необходимо выбрать каталог для добавления.");
                return;
            }

            var parentNode = (Entities)this.treeListView1.SelectedObject;

            if (parentNode.Type == "File")
            {
                MessageBox.Show("Невозможно создать объект внутри файла.");
            }
            else
            {
                AddChildItemForm form = new AddChildItemForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    
                    int count = this.treeListView1.GetItemCount();

                    string name = form.ItemName;
                    string type = form.ItemType;

                    if (string.IsNullOrEmpty(type))
                        добавитьToolStripMenuItem_Click(добавитьToolStripMenuItem, new EventArgs());

                    Entities newItem = new Entities(parentNode, count, name, type);

                    while (treeListView1.GetChildren(parentNode).Cast<Entities>().Any(x => x.Name.Contains(newItem.Name)) == true)
                    {
                        newItem.Name += GetNextFilename(parentNode, newItem, numberPattern);
                    }

                    parentNode.Add(newItem);
                    appDbContext.Create(newItem);

                    GetTreeFromBd();
                    FillTreeView();
                }
            }
        }

        private string GetNextFilename(Entities parentNode, Entities newItem, string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!treeListView1.GetChildren(parentNode).Cast<Entities>().Any(x => x.Name.Contains(newItem.Name)))
                return tmp;

            int min = 1, max = 2;

            while (treeListView1.GetChildren(parentNode).Cast<Entities>().Any(x => x.Name.Contains(string.Format(pattern, max))))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.treeListView1.SelectedObject == null)
            {
                MessageBox.Show("Перед выполнение необходимо выбрать каталог для добавления.");
                return;
            }
            var entities = (Entities)this.treeListView1.SelectedObject;

            DialogResult result = MessageBox.Show(
                "Данную операцию невозможно отменить! Вы уверены, что хотите удалить данный объект?",
                "Внимание!",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information
                );
            if (result == DialogResult.OK)
            {
                try
                {
                    appDbContext.Delete(entities);

                    MessageBox.Show(
                            "Операция выполнена успешно!",
                            "Информационное сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );

                    GetTreeFromBd();
                    FillTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                            ex.Message,
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                            );
                }
            }
        }
    }
}