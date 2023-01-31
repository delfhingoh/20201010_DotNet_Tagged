using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ICT365_A01_33836223.Users
{
    // The class with attributes to STORE MANY USERS
    public class UserDB
    {
        private List<User> userList = new List<User>();
        private Dictionary<User, Events.EventDB> usersEventDBs = new Dictionary<User, Events.EventDB>();    // 1 user can only have 1 eventDB
        private User currentUser;
        private string filePath = "userDB/userDB.xml";
        private bool isThereRememberMe = false;

        public UserDB()
        {
            // If userDB.xml does not exists
            if (!File.Exists(filePath))
            {
                // Create new file
                XDocument newFile = new XDocument(new XDeclaration("1.0", "utf-8", null));
                // Prepare the PARENT of the new file
                XElement newParent = new XElement("accounts");
                newFile.Add(newParent);

                // Create a new user for admin
                User adminUser = new User("admin", "123", "cat", 0);
                CreateUser(adminUser, newFile);

                Events.EventDB thisEventDB = new Events.EventDB();
                thisEventDB.CreateEventDB(adminUser.GetUsername());

                usersEventDBs.Add(adminUser, thisEventDB);

            }
            // If userDB.xml exists
            else
            {
                XDocument thisFile = new XDocument(XDocument.Load(filePath));

                // Check for any accounts
                IEnumerable<XElement> accounts = thisFile.Element("accounts").Elements("account");
                int tempCount = 0;

                foreach (XElement thisAccount in accounts) { tempCount++; }

                // Load the DATA from the XML file into a DICTIONARY
                if (tempCount > 0)
                {
                    foreach (XElement thisAccount in accounts)
                    {
                        string username = thisAccount.Element("username").Value;
                        string password = thisAccount.Element("password").Value;
                        string securityAns = thisAccount.Element("securityAns").Value;
                        int securityQns = Convert.ToInt16(thisAccount.Element("securityQns").Value);

                        User thisUser = new User(username, password, securityAns, securityQns);
                        userList.Add(thisUser);

                        if (Convert.ToInt16(thisAccount.Element("remember").Value) == 0)
                        {
                            isThereRememberMe = true;
                            SetCurrentUser(username);
                        }

                        Events.EventDB thisEventDB = new Events.EventDB();
                        thisEventDB.CreateEventDB(username);
                        usersEventDBs.Add(thisUser, thisEventDB);
                    }
                }
            }
        }

        public void CreateUser(User _user, XDocument thisFile)
        {
            // Add user into userList
            userList.Add(_user);

            // Add this user into XML file
            XElement newAccount = new XElement("account",
                                            new XElement("username", _user.GetUsername()),
                                            new XElement("password", _user.GetPassword()),
                                            new XElement("remember", "1"),
                                            new XElement("securityAns", _user.GetSecurityAns()),
                                            new XElement("securityQns", _user.GetSecurityQns()));

            thisFile.Element("accounts").Add(newAccount);
            thisFile.Save(filePath);

            // Create an EVENTDB for this user
            Events.EventDB thisEventDB = new Events.EventDB();
            thisEventDB.CreateEventDB(_user.GetUsername());
            usersEventDBs.Add(_user, thisEventDB);
        }

        public void CreateUser(User _user)
        {
            // Open File
            XDocument thisFile = new XDocument(XDocument.Load(filePath));

            // Add user into userList
            userList.Add(_user);

            // Add this user into XML file
            XElement newAccount = new XElement("account",
                                            new XElement("username", _user.GetUsername()),
                                            new XElement("password", _user.GetPassword()),
                                            new XElement("remember", "1"),
                                            new XElement("securityAns", _user.GetSecurityAns()),
                                            new XElement("securityQns", _user.GetSecurityQns()));

            thisFile.Element("accounts").Add(newAccount);
            thisFile.Save(filePath);

            // Create an EVENTDB for this user
            Events.EventDB thisEventDB = new Events.EventDB();
            thisEventDB.CreateEventDB(_user.GetUsername());
            usersEventDBs.Add(_user, thisEventDB);
        }

        // If the USER click on 'REMEMBER ME' RADIO BTN
        public void RememberThisUser(string _username)
        {
            XDocument thisFile = new XDocument(XDocument.Load(filePath));
            IEnumerable<XElement> accounts = thisFile.Element("accounts").Elements("account");

            // Set ALL users 'remember' to false
            foreach (XElement thisAcc in accounts) { thisAcc.Element("remember").SetValue("1"); }

            // Set this user to 'remember' 
            foreach(XElement thisUser in accounts)
            {
                if (thisUser.Element("username").Value == _username)
                    thisUser.Element("remember").SetValue("0");
            }

            // Save the XML File
            thisFile.Save(filePath);
        }

        // If the USER LOGOUT from the MAIN APPLICATION PAGE
        public void RemoveFromRememberMe()
        {
            XDocument thisFile = new XDocument(XDocument.Load(filePath));
            IEnumerable<XElement> accounts = thisFile.Element("accounts").Elements("account");

            // Set ALL users 'remember' to false
            foreach (XElement thisAcc in accounts) { thisAcc.Element("remember").SetValue("1"); }

            // Save the XML File
            thisFile.Save(filePath);
        }

        // If USER forget his or her own password and is RESETTING it
        public void ResetPassword(User _user, string _newPassword)
        {
            XDocument thisFile = new XDocument(XDocument.Load(filePath));
            IEnumerable<XElement> accounts = thisFile.Element("accounts").Elements("account");

            foreach(XElement thisAcc in accounts)
            {
                if (thisAcc.Element("username").Value == _user.GetUsername())
                    thisAcc.Element("password").SetValue(_newPassword);
            }

            thisFile.Save(filePath);
        }

        // ALL USERNAMES WILL BE UNIQUE hence there's a need to check
        public bool IsThisUsernameNew(string _username)
        {
            XDocument thisFile = new XDocument(XDocument.Load(filePath));
            IEnumerable<XElement> accounts = thisFile.Element("accounts").Elements("account");

            foreach(XElement thisAcc in accounts)
            {
                if (thisAcc.Element("username").Value == _username)
                    return false;
            }

            return true;
        }

        // ENSURE that the USERNAME and PASSWORD ARE CORRECT
        public bool IsLoginCorrect(string _username, string _password)
        {
            foreach(User tempUser in userList)
            {
                if (tempUser.GetUsername() == _username)
                    if (tempUser.GetPassword() == _password)
                        return true;
            }

            return false;
        }

        // THE USER MUST ANSWER THE SECURITY QNS
        public bool IsTheSecurityPartCorrect(User _user, int _securityQns, string _securityAns)
        {
            if (_user.GetSecurityQns() == _securityQns && _user.GetSecurityAns() == _securityAns)
                return true;

            return false;
        }

        public bool IsThereRememberMe() { return isThereRememberMe; }

        public User GetThisUser(string _username)
        {
            foreach(User tempUser in userList)
            {
                if (tempUser.GetUsername() == _username)
                    return tempUser;
            }

            return null;
        }

        public void SetCurrentUser(string _username)
        {
            foreach(User tempUser in userList)
            {
                if (tempUser.GetUsername() == _username)
                    currentUser = tempUser;
            }
        }

        // RETURN the EVENTDB OF THIS USER
        public Events.EventDB GetThisEventDB(string _username)
        {
            User tempUser = null;

            // Get the chosen user
            foreach(User thisUser in userList)
                if (thisUser.GetUsername() == _username)
                    tempUser = thisUser;

            // Return the EVENTDB of this USER
            if(usersEventDBs.ContainsKey(tempUser))
            {
                usersEventDBs.TryGetValue(tempUser, out Events.EventDB value);
                return value;
            }

            return null;
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }

        public List<User> GetUserList()
        {
            return userList;
        }
    }
}
