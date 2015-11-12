using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Base
{
    public abstract class Entity
    {
        public Entity()
        {
            this.HasName = true;
            this.HasDateCreated = true;
            this.HasDateUpdated = true;
            this.HasUserCreated = true;
            this.HasUserUpdated = true;
            this.HasDescription = true;
            this.CanBeDeleted = false;
            this.Configuration();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserCreated { get; set; }

        public int UserUpdated { get; set;}

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        string table_name = "";
        public string DB_TableName
        {
            get
            {
                if (table_name == "")
                {
                    string[] labels = this.GetType().FullName.Split('.');
                    for (int i = 2; i < labels.Length; i++)
                    {
                        table_name += labels[i];
                        if (i != labels.Length - 1)
                            table_name += "_";
                    }
                }
                return table_name;
            }
        }

        private Orvis.Application.Facede app;
        public Orvis.Application.Facede Application
        {
            get
            {
                if (app == null) app = new Facede();
                return app;
            }
        }

        public abstract void Configuration();

        public bool HasName { get; set; }
        public bool HasDescription { get; set; }
        public bool HasUserCreated { get; set; }
        public bool HasUserUpdated { get; set; }
        public bool HasDateCreated { get; set; }
        public bool HasDateUpdated { get; set; }
        public bool CanBeDeleted { get; set; }

    }
}
