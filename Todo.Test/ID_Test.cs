using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

namespace Todo.Test
{
    public class ID_Test
    {
        [Fact]
        public void Empty_Defaults_Correct() =>
            Assert.Equal(ID.Empty.ToString(), string.Empty);

        [Fact]
        public void NewID_IsUnique()
        {
            ID a = new ID();
            ID b = new ID();

            Assert.NotEqual(a, b);
            Assert.NotEqual(a.ToString(), b.ToString());
        }

        [Fact]
        public void ToString_ReturnsKey()
        {
            ID id = new ID();

            Assert.False(string.IsNullOrEmpty(id.ToString()));
        }
    }
}
