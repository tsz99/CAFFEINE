using CAFFEINE.Data;
using System.Collections.Generic;

namespace CAFFEINE.Services
{
    public class CaffService
    {
        public Caff ParseCaff()
        {
            Tag tag = new Tag() { Text = "newTag" };
            Comment comment = new Comment() { Text = "newComment" };
            Ciff ciff = new Ciff() { Pixels = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },Tags = new List<Tag>() { tag }, Caption="newCaption", Width =200, Height=200,Duration=10 }; 
            Caff caff = new Caff() { Ciffs = new List<Ciff>() { ciff}, Creator = "Ábel", Year = 2022, Month = 11, Day = 30, Hour = 21, Minute = 42, Comments = new List<Comment>() { comment} };
            return caff;
        }
    }
}