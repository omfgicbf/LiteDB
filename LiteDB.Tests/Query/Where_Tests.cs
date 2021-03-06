﻿using System.Linq;
using Xunit;

namespace LiteDB.Tests.Query
{
    public class Where_Tests : Person_Tests
    {
        [Fact]
        public void Query_Where_With_Parameter()
        {
            var r0 = local
                .Where(x => x.Address.State == "FL")
                .ToArray();

            var r1 = collection.Query()
                .Where(x => x.Address.State == "FL")
                .ToArray();

            AssertEx.ArrayEqual(r0, r1, true);
        }

        [Fact]
        public void Query_Multi_Where_With_Like()
        {
            var r0 = local
                .Where(x => x.Age >= 10 && x.Age <= 40)
                .Where(x => x.Name.StartsWith("Ge"))
                .ToArray();

            var r1 = collection.Query()
                .Where(x => x.Age >= 10 && x.Age <= 40)
                .Where(x => x.Name.StartsWith("Ge"))
                .ToArray();

            AssertEx.ArrayEqual(r0, r1, true);
        }

        [Fact]
        public void Query_Single_Where_With_And()
        {
            var r0 = local
                .Where(x => x.Age == 25 && x.Active)
                .ToArray();

            var r1 = collection.Query()
                .Where("age = 25 AND active = true")
                .ToArray();

            AssertEx.ArrayEqual(r0, r1, true);
        }

        [Fact]
        public void Query_Single_Where_With_Or_And_In()
        {
            var r0 = local
                .Where(x => x.Age == 25 || x.Age == 26 || x.Age == 27)
                .ToArray();

            var r1 = collection.Query()
                .Where("age = 25 OR age = 26 OR age = 27")
                .ToArray();

            var r2 = collection.Query()
                .Where("age IN [25, 26, 27]")
                .ToArray();

            AssertEx.ArrayEqual(r0, r1, true);
            AssertEx.ArrayEqual(r1, r2, true);
        }
    }
}