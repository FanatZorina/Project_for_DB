using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_for_DB
{
    public class FromBigAuditClass
    {
        public int number { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string sname { get; set; }
        public string departament { get; set; }
        public System.DateTime date { get; set; }
        public int iddoor { get; set; }
        public string fulladress { get; set; }
        public bool closed { get; set; }
        public string street { get; set; }
        public int numberst { get; set; }
        public int? building { get; set; }
        public int idadress { get; set; }
    }

    public class FromAuditClass
    {
        public int number { get; set; }
        public System.DateTime date { get; set; }
        public int iddoor { get; set; }
        public int idstreet { get; set; }
        public string login { get; set; }
        public bool closed { get; set; }
        public string? fulladress { get; set; }
    }
    public class FromAdressClass
    {
        public int Id { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public int? building { get; set; }
        public string? fulladress { get; set; }
    }

    public class FromLockClass
    {
        public int Id { get; set; }
        public string? fulladress { get; set; }
        public int Id_street { get; set; }
        public int Level { get; set; }
        public bool Closed { get; set; }
    }

    public class FromDeptClass
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int number { get; set; }

    }
    public class FromUserClass
    {
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string sname { get; set; }
        public int level { get; set; }
        public string departament_name { get; set; }
        public int departament_id { get; set; }

    }
}
