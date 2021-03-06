﻿using System;
using System.IO;
using System.Linq;
using Xunit;

namespace LiteDB.Tests.Query
{
    public class GroupBy_Tests : IDisposable
    {
        private Person[] local;

        private LiteDatabase db;
        private LiteCollection<Person> collection;

        public GroupBy_Tests()
        {
            local = DataGen.Person(1, 1000).ToArray();

            db = new LiteDatabase(new MemoryStream());
            collection = db.GetCollection<Person>();

            collection.Insert(local);
            collection.EnsureIndex(x => x.Age);
        }

        [Fact(Skip = "Commented out")]
        public void Query_GroupBy_State_With_Count()
        {
            //** var r0 = local
            //**     .GroupBy(x => x.State)
            //**     .Select(x => new { State = x.Key, Count = x.Count() })
            //**     .OrderBy(x => x.State)
            //**     .ToArray();
            //** 
            //** var r1 = collection.Query()
            //**     .GroupBy(x => x.State)
            //**     .Select(x => new { x.State, Count = Sql.Count(x) })
            //**     .OrderBy(x => x.State)
            //**     .ToArray();
            //** 
            //** foreach (var r in r0.Zip(r1, (l, r) => new { left = l, right = r }))
            //** {
            //**     Assert.Equal(r.left.State, r.right.State);
            //**     Assert.Equal(r.left.Count, r.right.Count);
            //** }
        }

        [Fact(Skip = "Commented out")]
        public void Query_GroupBy_State_With_Sum_Age()
        {
            //** var r0 = local
            //**     .GroupBy(x => x.State)
            //**     .Select(x => new { State = x.Key, Sum = x.Sum(q => q.Age) })
            //**     .OrderBy(x => x.State)
            //**     .ToArray();
            //** 
            //** var r1 = collection.Query()
            //**     .GroupBy(x => x.State)
            //**     .Select(x => new { x.State, Sum = Sql.Sum(x.Age) })
            //**     .OrderBy(x => x.State)
            //**     .ToArray();
            //** 
            //** foreach (var r in r0.Zip(r1, (l, r) => new { left = l, right = r }))
            //** {
            //**     Assert.Equal(r.left.State, r.right.State);
            //**     Assert.Equal(r.left.Sum, r.right.Sum);
            //** }
        }

        [Fact(Skip = "Commented out")]
        public void Query_GroupBy_Func()
        {
            //** var r0 = local
            //**     .GroupBy(x => x.Date.Year)
            //**     .Select(x => new { Year = x.Key, Count = x.Count() })
            //**     .OrderBy(x => x.Year)
            //**     .ToArray();
            //** 
            //** var r1 = collection.Query()
            //**     .GroupBy(x => x.Date.Year)
            //**     .Select(x => new { x.Date.Year, Count = Sql.Count(x) })
            //**     .OrderBy(x => x.Year)
            //**     .ToArray();
            //** 
            //** foreach (var r in r0.Zip(r1, (l, r) => new { left = l, right = r }))
            //** {
            //**     Assert.Equal(r.left.Year, r.right.Year);
            //**     Assert.Equal(r.left.Count, r.right.Count);
            //** }
        }

        [Fact(Skip = "Commented out")]
        public void Query_GroupBy_With_Array_Aggregation()
        {
            //** // quite complex group by query
            //** var r = collection.Query()
            //**     .GroupBy(x => x.Email.Substring(x.Email.IndexOf("@") + 1))
            //**     .Select(x => new
            //**     {
            //**         Domain = x.Email.Substring(x.Email.IndexOf("@") + 1),
            //**         Users = Sql.ToArray(new
            //**         {
            //**             Login = x.Email.Substring(0, x.Email.IndexOf("@")).ToLower(),
            //**             x.Name,
            //**             x.Age
            //**         })
            //**     })
            //**     .Limit(10)
            //**     .ToArray();
            //** 
            //** // test first only
            //** Assert.Equal(5, r[0].Users.Length);
            //** Assert.Equal("imperdiet.us", r[0].Domain);
            //** Assert.Equal("delilah", r[0].Users[0].Login);
            //** Assert.Equal("Dahlia Warren", r[0].Users[0].Name);
            //** Assert.Equal(24, r[0].Users[0].Age);
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}