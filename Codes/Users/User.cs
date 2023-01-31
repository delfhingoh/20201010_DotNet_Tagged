using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT365_A01_33836223.Users
{
    // The class with attributes of ONE USER
    public class User
    {
        public enum SECURITY_QUESTION
        {
            FAVOURITE_ANIMAL = 0,
            FAVOURITE_COLOUR
        }

        private string username;
        private string password;
        private string securityAns;
        private int securityQns;
        
        public User(string _username, string _password, string _securityAns, int _securityQns)
        {
            username = _username;
            password = _password;
            securityAns = _securityAns;
            securityQns = _securityQns;
        }

        public string GetUsername() { return username; }
        public string GetPassword() { return password; }
        public string GetSecurityAns() { return securityAns; }
        public int GetSecurityQns() { return securityQns; }
    }
}
