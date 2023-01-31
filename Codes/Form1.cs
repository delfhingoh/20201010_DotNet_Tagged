using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICT365_A01_33836223
{
    public partial class Form1 : Form
    {
        private PageManager pageManager = PageManager.GetInstance();
        private UserPages.LoginPage loginPage = new UserPages.LoginPage();
        private UserPages.CreateAccount createAccPage = new UserPages.CreateAccount();
        private UserPages.ForgetPassword forgetPassPage = new UserPages.ForgetPassword();
        private MainApplication mainApplication = new MainApplication();

        public Form1()
        {
            InitializeComponent();
            
            loginPage.LoginControls();
            createAccPage.CreateAccountControls();
            createAccPage.CreateAccountSuccessControls();
            forgetPassPage.CreateForgetPasswordControls();
            mainApplication.CreateApplicationControls();
            mainApplication.CreateSidePanelControls();
            mainApplication.CreateEventInfoControls();
            mainApplication.CreateAddEventControls();
            mainApplication.CreateRetrieveEventControls();

            foreach (Panel thisPanel in pageManager.GetPanelList())
            {
                if (thisPanel.Name != "sidePanel" && thisPanel.Name != "eventInfo" && thisPanel.Name != "addEvent" && thisPanel.Name != "retrieveEvent")
                    thisPanel.Parent = this;
            }

            mainApplication.CreateSidePanelControls().Parent = pageManager.GetThisPanel("application");
            mainApplication.CreateEventInfoControls().Parent = pageManager.GetThisPanel("sidePanel");
            mainApplication.CreateAddEventControls().Parent = pageManager.GetThisPanel("sidePanel");
            mainApplication.CreateRetrieveEventControls().Parent = pageManager.GetThisPanel("sidePanel");

            // Check for a REMEMBER ME User
            if (pageManager.GetUserDB().IsThereRememberMe())
            {
                pageManager.GetThisPanel("login").Hide();
                pageManager.GetThisPanel("createAcc").Hide();
                pageManager.GetThisPanel("createAcc_success").Hide();
                pageManager.GetThisPanel("forgetPass").Hide();
                pageManager.GetThisPanel("addEvent").Hide();
                pageManager.GetThisPanel("retrieveEvent").Hide();

                mainApplication.GetUsername().Text = pageManager.GetUserDB().GetCurrentUser().GetUsername();
                mainApplication.LoadEventMarkers(pageManager.GetUserDB().GetThisEventDB(mainApplication.GetUsername().Text));
            }
        }
    }
}
