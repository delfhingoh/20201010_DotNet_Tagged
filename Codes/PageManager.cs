using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ICT365_A01_33836223
{
    public class PageManager
    {
        private List<Panel> panelList = new List<Panel>();
        private Users.UserDB userDB = new Users.UserDB();
        
        public static PageManager obj;

        private PageManager()
        {

        }
        public static PageManager GetInstance()
        {
            if (obj == null)
                obj = new PageManager();

            return obj;
        }

        public void AddThisPanel(Panel _this)
        {
            panelList.Add(_this);
        }

        public Users.UserDB GetUserDB() { return userDB; }

        public List<Panel> GetPanelList() { return panelList; }

        public Panel GetThisPanel(string _name)
        {
            foreach(Panel temp in panelList)
            {
                if (temp.Name == _name)
                    return temp;
            }

            return null;
        }

        public Panel GetThisPanel(Panel _panel)
        {
            foreach (Panel temp in panelList)
            {
                if (temp == _panel)
                    return temp;
            }

            return null;
        }
    }
}
