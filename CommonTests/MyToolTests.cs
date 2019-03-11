using NUnit.Framework;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tests
{
    [TestFixture()]
    public class MyToolTests
    {
        [Test()]
        public void GetSerialNoTest()
        {
            string str = MyTool.GetSerialNo(6);
            string str01 = MyTool.GetSerialNo(0, "sn");
        }
    }
}