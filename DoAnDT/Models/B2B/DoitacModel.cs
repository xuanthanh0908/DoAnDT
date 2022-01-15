using DoAnDT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnDT.Models
{
    public class DoitacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
            using(DBDTConnect db = new DBDTConnect())
            {
                var ds = (from p in db.NhaCungCaps where p.Net_user == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}