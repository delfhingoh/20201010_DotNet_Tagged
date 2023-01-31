using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ICT365_A01_33836223.UserPages
{
    public class ForgetPassword
    {
        private PageManager pageManager = PageManager.GetInstance();

        // CONTROLS for FORGET PASSWORD PAGE
        private Panel forgetPasswordPanel = new Panel();
        private Label forgatPassTitleL = new Label();
        private Label usernameL = new Label();
        private Label securityQnsL = new Label();
        private Label securityAnsL = new Label();
        private Label newPasswordL = new Label();
        private TextBox usernameTXT = new TextBox();
        private TextBox securityAnsTXT = new TextBox();
        private TextBox newPasswordTXT = new TextBox();
        private ComboBox securityQnsDD = new ComboBox();
        private Button resetPasswordBTN = new Button();
        private Button goBackToLognBTN = new Button();

        
        // Error Provider
        private ErrorProvider errorProvider = new ErrorProvider();

        // The FORM DESIGN CONTROLS for FORGET PASSWORD PANEL
        public Control CreateForgetPasswordControls()
        {
            // Create Forget Password Panel
            // Set up Panel
            forgetPasswordPanel.Name = "forgetPass";
            forgetPasswordPanel.Size = new Size(800, 600);

            // Title
            forgatPassTitleL.Text = "FORGET PASSWORD";
            forgatPassTitleL.Font = new Font("Microsoft Sans Serif", 20);
            forgatPassTitleL.Size = new Size(300, 50);
            forgatPassTitleL.Location = new Point(150, 50);

            // USER INPUTS
            // Username
            usernameL.Text = "Username";
            usernameL.Location = new Point(180, 120);
            usernameTXT.Size = new Size(usernameL.Size.Width, usernameL.Size.Height);
            usernameTXT.Location = new Point(usernameL.Location.X + 100, usernameL.Location.Y - 3);
            // Security Question
            securityQnsL.Text = "Security Question";
            securityQnsL.Location = new Point(usernameL.Location.X, usernameL.Location.Y + 40);
            securityQnsDD.Size = new Size(usernameTXT.Size.Width + 50, usernameTXT.Size.Height);
            securityQnsDD.DropDownStyle = ComboBoxStyle.DropDownList;
            securityQnsDD.DataSource = Enum.GetValues(typeof(Users.User.SECURITY_QUESTION));
            securityQnsDD.Location = new Point(securityQnsL.Location.X + 100, securityQnsL.Location.Y - 3);
            // Security Answer
            securityAnsL.Text = "Security Answer";
            securityAnsL.Location = new Point(securityQnsL.Location.X, securityQnsL.Location.Y + 40);
            securityAnsTXT.Size = new Size(securityQnsDD.Size.Width, securityQnsDD.Size.Height);
            securityAnsTXT.Location = new Point(securityAnsL.Location.X + 100, securityAnsL.Location.Y - 3);
            // Reset Password
            newPasswordL.Text = "New Password";
            newPasswordL.Location = new Point(securityAnsL.Location.X, securityAnsL.Location.Y + 40);
            newPasswordTXT.Size = new Size(securityAnsTXT.Size.Width, securityAnsTXT.Size.Height);
            newPasswordTXT.Location = new Point(newPasswordL.Location.X + 100, newPasswordL.Location.Y - 3);

            // BUTTONS
            resetPasswordBTN.Text = "Reset";
            resetPasswordBTN.Location = new Point(250, 300);

            goBackToLognBTN.Text = "Back";
            goBackToLognBTN.Location = new Point(resetPasswordBTN.Location.X, resetPasswordBTN.Location.Y + 40);

            // Add ALL controls
            forgetPasswordPanel.Controls.Add(forgatPassTitleL);
            forgetPasswordPanel.Controls.Add(usernameL);
            forgetPasswordPanel.Controls.Add(usernameTXT);
            forgetPasswordPanel.Controls.Add(securityQnsL);
            forgetPasswordPanel.Controls.Add(securityQnsDD);
            forgetPasswordPanel.Controls.Add(securityAnsL);
            forgetPasswordPanel.Controls.Add(securityAnsTXT);
            forgetPasswordPanel.Controls.Add(newPasswordL);
            forgetPasswordPanel.Controls.Add(newPasswordTXT);
            forgetPasswordPanel.Controls.Add(resetPasswordBTN);
            forgetPasswordPanel.Controls.Add(goBackToLognBTN);

            pageManager.AddThisPanel(forgetPasswordPanel);

            CreateForgetPasswordEvents();

            return forgetPasswordPanel;
        }

        public void CreateForgetPasswordEvents()
        {
            resetPasswordBTN.Click += new EventHandler(resetPasswordBTN_Click);
            goBackToLognBTN.Click += new EventHandler(goBackToLognBTN_Click);
        }

        private void resetPasswordBTN_Click(object _sender, EventArgs _e)
        {
            errorProvider.Clear();

            // Make sure ALL fields are filled in
            // Check for any EMPTY FIELDS
            if (usernameTXT.Text == "" || securityAnsTXT.Text == "" || newPasswordTXT.Text == "")
                errorProvider.SetError(resetPasswordBTN, "There are empty fields!");
            // Check for any SPACE in USERNAME and NEW PASSWORD
            else if (usernameTXT.Text.Contains(" "))
                errorProvider.SetError(usernameTXT, "No space for username!");
            else if (newPasswordTXT.Text.Contains(" "))
                errorProvider.SetError(newPasswordTXT, "No space for password!");
            // Check that there is an existing user with that name
            else if (pageManager.GetUserDB().GetThisUser(usernameTXT.Text) == null)
                errorProvider.SetError(usernameTXT, "There is no user with this name");
            // Check that the SECURITY QNS and ANS are correct
            else if (!pageManager.GetUserDB().IsTheSecurityPartCorrect(pageManager.GetUserDB().GetThisUser(usernameTXT.Text), securityQnsDD.SelectedIndex, securityAnsTXT.Text))
                errorProvider.SetError(resetPasswordBTN, "The security details are wrong!");
            // Once ALL are FILLED IN and CORRECT
            else
            {
                pageManager.GetUserDB().ResetPassword(pageManager.GetUserDB().GetThisUser(usernameTXT.Text), newPasswordTXT.Text);
                pageManager.GetThisPanel("createAcc_success").Show();

                Reset();
            }
        }


        private void goBackToLognBTN_Click(object _sender, EventArgs _e)
        {
            pageManager.GetThisPanel("login").Show();
            pageManager.GetThisPanel("createAcc").Show();
            pageManager.GetThisPanel("createAcc_success").Show();

            Reset();
        }

        public void Reset()
        {
            usernameTXT.Text = "";
            securityAnsTXT.Text = "";
            newPasswordTXT.Text = "";
        }
    }
}
