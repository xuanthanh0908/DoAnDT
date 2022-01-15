using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnDT.Models
{
    public class GiaoDienModel
    {
        private DBDTConnect db = new DBDTConnect();

        internal IQueryable<GiaoDien> GetDD()
        {
            return db.GiaoDiens;
        }

        internal IQueryable<Link> GetSlideShow()
        {
            return db.Links.Where(m => m.Group.Contains("SlideShow"));
        }
    }
}