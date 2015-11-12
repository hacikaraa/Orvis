using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orvis.Application.Member
{
    public class User : Base.Entity
    {
        public User()
        {

        }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }//0=Bay--1=Bayan
        public string Photo { get; set; }

        public override void Configuration()
        {
            
        }
    }
}