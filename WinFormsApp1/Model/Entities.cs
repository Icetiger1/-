using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Model
{
    public class Entities : IEnumerable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public int? ParentId { get; set; }
        public List<Entities> Childrens { get; set; } = new();

        public Entities() { }

        public Entities(Entities parent, int id, string name, string type)
        {
            this.Id = id;
            this.Name = name;
            this.Path = parent.Path + "/" + Name;
            this.Type = type;
            this.ParentId = parent.Id;
        }

        public Entities(int id, string name, string path, string type, int? parentId)
        {
            this.Id = id;
            this.Name = name;
            this.Path = path;
            this.Type = type;
            this.ParentId = parentId;
        }

        public void Add(Entities entities)
        {
            Childrens.Add(entities);
        }

        public void SetNewName(string newName)
        {
            Name = newName;
            Path = Path.Replace(Path.Split('\\').Last(), Name);
        }

        public void SetNewPath(Entities parent, Entities child)
        {
            child.Path = parent.Path + "/" + child.Name;

            if (Childrens.Count > 0)
            {
                foreach (Entities entities in child.Childrens)
                {
                    SetNewPath(child, entities);
                }
            }
        }

        public void SetNewPath(string lastName, string name)
        {
            string newPath = this.Path.Replace(lastName, name);
            this.Path = newPath;

            if (Childrens.Count > 0)
            {
                foreach (Entities entities in this.Childrens)
                {
                    entities.SetNewPath(lastName, name);
                }
            }
        }

        public bool IsAncestor(Entities entities)
        {
            try
            {
                if (entities.ParentId != null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }

        public void RecursFillData(List<Entities> data)
        {
            foreach (Entities entities in data)
            {
                if (entities.Id == this.ParentId)
                {
                    entities.Childrens.Add(this);
                    break;
                }
                else
                {
                    RecursFillData(entities.Childrens);
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Childrens.GetEnumerator();
        }
    }
}
