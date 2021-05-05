using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceParkAPI.Security;
using Xunit;

namespace SpaceParkAPITests
{
    public class SecurityTests
    {
        [Fact]
        public void EnryptPassword_Test()
        {
            string pw = "test123";
            string encrypted = Encryption.Encrypt(pw);

            Assert.Equal("{lz{89:", encrypted);
        }
    }
}
