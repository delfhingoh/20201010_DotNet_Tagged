using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ICT365_A01_33836223.UserPages
{
    public class LoginPage
    {
        private PageManager pageManager = PageManager.GetInstance();

        // Controls for LOGIN PAGE
        private Panel loginPanel = new Panel();
        private Label loginTitleL = new Label();
        private Label usernameL = new Label();
        private Label passwordL = new Label();
        private RadioButton rememberMeRBTN = new RadioButton();
        private Button loginBTN = new Button();
        private Button createAccountBTN = new Button();
        private Button forgetPasswordBTN = new Button();
        private TextBox usernameTXT = new TextBox();
        private TextBox passwordTXT = new TextBox();

        // Error Provider
        private ErrorProvider errorProvider = new ErrorProvider();

        // All of the DESIGN FORM CONTROLS for ths LOGIN PANEL
        public Control LoginControls()
        {
            // Set up Panel
            loginPanel.Name = "login";
            loginPanel.Size = new Size(800, 600);
            
            // Title
            loginTitleL.Text = "LOGIN";
            loginTitleL.Font = new Font("Microsoft Sans Serif", 20);
            loginTitleL.Size = new Size(100, 50);
            loginTitleL.Location = new Point(240, 50);

            // USER INPUTS
            // Username 
            usernameL.Text = "Username";
            usernameL.Location = new Point(180, 120);
            usernameTXT.Size = new Size(usernameL.Size.Width, usernameL.Size.Height);
            usernameTXT.Location = new Point(usernameL.Location.X + 100, usernameL.Location.Y - 3);
            // Password
            passwordL.Text = "Password";
            passwordL.Location = new Point(usernameL.Location.X, usernameL.Location.Y + 40);
            passwordTXT.Size = usernameTXT.Size;
            passwordTXT.Location = new Point(passwordL.Location.X + 100, passwordL.Location.Y - 3);

            // RADIO BUTTON
            rememberMeRBTN.Text = "Remember Me?";
            rememberMeRBTN.Location = new Point(passwordL.Location.X, passwordL.Location.Y + 42);

            // BUTTONS
            loginBTN.Text = "Login";
            loginBTN.Location = new Point(250, 260);

            createAccountBTN.Text = "Create Account";
            createAccountBTN.Size = new Size(loginBTN.Size.Width + 20, loginBTN.Size.Height);
            createAccountBTN.Location = new Point(loginBTN.Location.X - 10, loginBTN.Location.Y + 40);

            forgetPasswordBTN.Text = "Forget Password";
            forgetPasswordBTN.Size = createAccountBTN.Size;
            forgetPasswordBTN.Location = new Point(createAccountBTN.Location.X, createAccountBTN.Location.Y + 40);

            // Add ALL controls
            loginPanel.Controls.Add(loginTitleL);

            loginPanel.Controls.Add(usernameL);
            loginPanel.Controls.Add(usernameTXT);
            loginPanel.Controls.Add(passwordL);
            loginPanel.Controls.Add(passwordTXT);
            loginPanel.Controls.Add(rememberMeRBTN);

            loginPanel.Controls.Add(loginBTN);
            loginPanel.Controls.Add(createAccountBTN);
            loginPanel.Controls.Add(forgetPasswordBTN);

            LoginPageEvents();

            pageManager.AddThisPanel(loginPanel);

            return loginPanel;
        }

        // Add RELEVANT EventHandler for LOGIN PAGE
        public void LoginPageEvents()
        {
            loginBTN.Click += new EventHandler(loginBTN_Click);
            createAccountBTN.Click += new EventHandler(createAccountBTN_Click);
            forgetPasswordBTN.Click += new EventHandler(forgetPasswordBTN_Click);
        }

        private void loginBTN_Click(object _sender, System.EventArgs _e)
        {
            errorProvider.Clear();

            // Make sure that USERNAME and PASSWORD are entered
            if (usernameTXT.Text == "")
                errorProvider.SetError(usernameTXT, "Cannot Be Empty!");
            else if (usernameTXT.Text.Contains(" "))
                errorProvider.SetError(usernameTXT, "Cannot Have Space!");
            else if (passwordTXT.Text == "")
                errorProvider.SetError(passwordTXT, "Cannot Be Empty!");
            else
            {
                // Check that the USERNAME and PASSWORD are CORRECT
                bool isCorrect = pageManager.GetUserDB().IsLoginCorrect(usernameTXT.Text, passwordTXT.Text);
                if (isCorrect)
                {
                    pageManager.GetThisPanel(loginPanel).Hide();
                    pageManager.GetThisPanel("createAcc").Hide();
                    pageManager.GetThisPanel("createAcc_success").Hide();
                    pageManager.GetThisPanel("forgetPass").Hide();
                    pageManager.GetThisPanel("addEvent").Hide();


                    // Set Current User
                    pageManager.GetUserDB().SetCurrentUser(usernameTXT.Text);
                    foreach (Control control in pageManager.GetThisPanel("sidePanel").Controls)
                    {
                        if (control.GetType() == typeof(Label))
                            if (control.Name == "usernameL")
                                control.Text = usernameTXT.Text;
                    }

                    // Check if REMEMBER ME is CHECKED
                    if (rememberMeRBTN.Checked)
                    {
                        pageManager.GetUserDB().RememberThisUser(usernameTXT.Text);
                    }
                }
                else
                {
                    errorProvider.SetError(loginBTN, "Wrong Username or Password!");
                }

                Reset();
            }
        }

        private void createAccountBTN_Click(object _sender, EventArgs _e)
        {
            pageManager.GetThisPanel(loginPanel).Hide();

            Reset();
        }

        private void forgetPasswordBTN_Click(object _sender, EventArgs _e)
        {
            pageManager.GetThisPanel(loginPanel).Hide();
            pageManager.GetThisPanel("createAcc").Hide();
            pageManager.GetThisPanel("createAcc_success").Hide();

            Reset();
        }

        private bool IsRememberMeOn()
        {
            if (rememberMeRBTN.Checked)
                return true;
            else
                return false;
        }

        public void Reset()
        {
            usernameTXT.Text = "";
            passwordTXT.Text = "";
        }
    }
}
