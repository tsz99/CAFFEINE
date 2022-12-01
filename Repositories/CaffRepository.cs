﻿using CAFFEINE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CAFFEINE.Repositories
{
    public class CaffRepository
    {
        private ApplicationDbContext _db;

        public CaffRepository(ApplicationDbContext _context)
        {
            _db = _context;
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

        public List<UserData> GetUsers()
        {
            var userdatas = _db.Users.Select(x => new UserData (){UserName = x.Email, PhoneNumber = x.PhoneNumber}).ToList();
            return userdatas;
        }
      
    }
}