using CAFFEINE.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAFFEINE.Repositories
{
    public class CaffRepository
    {
        private ApplicationDbContext _db;
        UserManager<IdentityUser> userManager;

        public CaffRepository(ApplicationDbContext _context, UserManager<IdentityUser> userManager)
        {
            _db = _context;
            this.userManager = userManager;
        }

        public int SaveCaff(Caff caff)
        {
            var ret = _db.Caffs.Add(caff);
            _db.SaveChanges();
            return ret.Entity.DB_ID;
        }

        public int AddCommentToCaff(int caffId, string creator, string text)
        {
            var ret = _db.Comments.Add(new Comment() { Creator=creator, Text=text, CaffDB_ID=caffId, DT_Created = DateTime.Now});

            _db.SaveChanges();
            return ret.Entity.DB_ID;
        }

        public Caff GetCaffFromId(int id)
        {
            return _db.Caffs.Include(x => x.Comments).Include(x=>x.Ciffs).ThenInclude(x=>x.Tags).FirstOrDefault(x=>x.DB_ID==id);
        }

        public List<Caff> GetAllCaff()
        {
            return _db.Caffs.Include(x => x.Comments).Include(x => x.Ciffs).ThenInclude(x => x.Tags).ToList();
        }

        public List<Comment> GetAllCommentToCaff(int id)
        {
            return _db.Comments.Where(x=>x.CaffDB_ID== id).ToList();
        }

        public void DeleteCaff(int id)
        {
            var caff = _db.Caffs.Find(id);
            _db.Caffs.Remove(caff);
            _db.SaveChanges();
        }

        public List<Caff> GetCaffsByFilter(string creator, string caption)
        {
            return _db.Caffs
                           .Where(x => x.Creator.ToLower().Contains(creator.ToLower())).Include(x => x.Ciffs)
                           .Where(x => x.Ciffs.Any(y => y.Caption.ToLower().Contains(caption.ToLower())))
                           .ToList();

        }

        public async Task<List<UserData>> GetUsers()
        {
            var userdatas = _db.Users.ToList();
            List<UserData>  users = new List<UserData>();
            foreach (var user in userdatas)
            {
                bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                users.Add(new UserData()
                {
                    isAdmin = isAdmin,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                });

            }
            return users;
        }

        public async Task UpdateUsers(List<UserData> users)
        {
            foreach (var item in users)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserName == item.UserName);
                if(user != null)
                {
                    user.UserName = item.UserName;
                    _db.Users.Update(user);
                    var userRoles = await userManager.GetRolesAsync(user);
                    if (item.isAdmin && !userRoles.Contains("Admin"))
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (!item.isAdmin && userRoles.Contains("Admin"))
                    {
                        await userManager.RemoveFromRoleAsync(user, "Admin");
                    }
                    _db.SaveChanges();
                }
            }
        }
      
    }
}
