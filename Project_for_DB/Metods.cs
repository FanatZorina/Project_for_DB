using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_for_DB
{
    public static class Metods
    {
        public static System.Linq.IQueryable<Project_for_DB.FromBigAuditClass> ViewBigAudit()
        {
            System.Linq.IQueryable<Project_for_DB.FromBigAuditClass> q;
            DoorContext db = new DoorContext();
            q = from audit in db.Audits.AsNoTracking()
                join user in db.Users.AsNoTracking()
                on audit.Login equals user.Login
                join departament in db.Departaments.AsNoTracking()
                on user.DepartamentId equals departament.Id
                join locks in db.Locks.AsNoTracking()
                on new { IdDoor = audit.IdDoor, IdStreet = audit.IdStreet } equals new { IdDoor = locks.Id, IdStreet = locks.IdStreet }
                join adress in db.Adresses.AsNoTracking()
                on locks.IdStreet equals adress.Id
                select new FromBigAuditClass()
                {
                    number = audit.Number,
                    login = audit.Login,
                    name = user.Name,
                    sname = user.Sname,
                    departament = departament.Name,
                    date = audit.Date,
                    iddoor = locks.Id,
                    idadress = locks.IdStreet,
                    closed = audit.Closed,
                    street = adress.Street,
                    numberst = adress.Number,
                    building = adress.Building,
                };
            return q;
        }
        public static System.Linq.IQueryable<Project_for_DB.FromUserClass> ViewUser()
        {
            System.Linq.IQueryable<Project_for_DB.FromUserClass> q;
            DoorContext db = new DoorContext();
            q = from user in db.Users.AsNoTracking()
                    join Departament in db.Departaments.AsNoTracking()
                    on user.DepartamentId equals Departament.Id
                    select new FromUserClass()
                    {
                        login = user.Login,
                        password = user.Password,
                        name = user.Name,
                        sname = user.Sname,
                        level = user.Level,
                        departament_name = Departament.Name
                    };
            return q;
        }

        public static System.Linq.IQueryable<Project_for_DB.FromAdressClass> ViewAdress()
        {
            System.Linq.IQueryable<Project_for_DB.FromAdressClass> q;
            DoorContext db = new DoorContext();
            q = from adress in db.Adresses.AsNoTracking()
                    select new FromAdressClass()
                    {
                        Id = adress.Id,
                        street = adress.Street,
                        number = adress.Number,
                        building = adress.Building,
                        fulladress = ""
                    };
            return q;
        }

        public static System.Linq.IQueryable<Project_for_DB.FromDeptClass> ViewDept()
        {
            System.Linq.IQueryable<Project_for_DB.FromDeptClass> q;
            DoorContext db = new DoorContext();
            q = from departament in db.Departaments.AsNoTracking()
                    select new FromDeptClass()
                    {
                        Id = departament.Id,
                        name = departament.Name,
                        number = departament.Number,

                    };
            return q;
        }
        public static System.Linq.IQueryable<Project_for_DB.FromLockClass> ViewLock()
        {
            System.Linq.IQueryable<Project_for_DB.FromLockClass> q;
            DoorContext db = new DoorContext();
            q = from Lock in db.Locks.AsNoTracking()
                    join Adress in db.Adresses.AsNoTracking()
                    on Lock.IdStreet equals Adress.Id
                    select new FromLockClass()
                    {
                        Id = Lock.Id,
                        Level = Lock.Level,
                        Closed = Lock.Closed,
                        Id_street = Lock.IdStreet,
                        fulladress = Convert.ToString("ул." + Adress.Street + " дом." + Adress.Number + " " + Adress.Building)
                    };
            return q;
        }
        public static System.Linq.IQueryable<Project_for_DB.FromAuditClass> ViewRecord()
        {
            System.Linq.IQueryable<Project_for_DB.FromAuditClass> q;
            DoorContext db = new DoorContext();
            q = from audit in db.Audits.AsNoTracking()
                    select new FromAuditClass()
                    {
                        number = audit.Number,
                        date = audit.Date,
                        iddoor = audit.IdDoor,
                        idstreet = audit.IdStreet,
                        login = audit.Login,
                        closed = audit.Closed
                    };
            return q;
        }
    }

}
