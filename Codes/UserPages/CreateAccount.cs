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
    public class CreateAccount
    {
        private PageManager pageManager = PageManager.GetInstance();

        // CONTROLS for CREATE ACCOUNT PAGE
        private Panel createAccPanel = new Panel();
        private Label createAccTitleL = new Label();
        private Label usernameL = new Label();
        private Label passwordL = new Label();
        private Label repeatPasswordL = new Label();
        private Label securityQnsL = new Label();
        private Label securityAnsL = new Label();
        private TextBox usernameTXT = new TextBox();
        private TextBox passwordTXT = new TextBox();
        private TextBox repeatPasswordTXT = new TextBox();
        private TextBox securityAnsTXT = new TextBox();
        private ComboBox securityQnsDD = new ComboBox();
        private Button createAccountBTN = new Button();
        private Button backToLoginBTN = new Button();

        // CONTROLS for SUCCESSFUL CREATION PAGE
        private Panel successfulCreationPanel = new Panel();
        private Label successL = new Label();
        private Button goToLoginPageBTN = new Button();

        // Error Provider
        private ErrorProvider errorProvider = new ErrorProvider();

        // The DESIGN FORM CONTROLS for CREATE ACCOUNT PANEL
        public Control CreateAccountControls()
        {
            // Create Account Panel
            // Set up Panel
            createAccPanel.Name = "createAcc";
            createAccPanel.Size = new Size(800, 600);

            // Title
            createAccTitleL.Text = "CREATE ACCOUNT";
            createAccTitleL.Font = new Font("Microsoft Sans Serif", 20);
            createAccTitleL.Size = new Size(300, 50);
            createAccTitleL.Location = new Point(180, 50);

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
            // Repeat Password
            repeatPasswordL.Text = "Repeat Password";
            repeatPasswordL.Location = new Point(passwordL.Location.X, passwordL.Location.Y + 40);
            repeatPasswordTXT.Size = passwordL.Size;
            repeatPasswordTXT.Location = new Point(repeatPasswordL.Location.X + 100, repeatPasswordL.Location.Y - 3);
            // Security Question
            securityQnsL.Text = "Security Question";
            securityQnsL.Location = new Point(repeatPasswordL.Location.X, repeatPasswordL.Location.Y + 40);
            securityQnsDD.Size = new Size(repeatPasswordTXT.Size.Width + 50, repeatPasswordTXT.Size.Height);
            securityQnsDD.DropDownStyle = ComboBoxStyle.DropDownList;
            securityQnsDD.DataSource = Enum.GetValues(typeof(Users.User.SECURITY_QUESTION));
            securityQnsDD.Location = new Point(securityQnsL.Location.X + 100, securityQnsL.Location.Y - 3);
            // Security Answer
            securityAnsL.Text = "Security Answer";
            securityAnsL.Location = new Point(securityQnsL.Location.X, securityQnsL.Location.Y + 40);
            securityAnsTXT.Size = new Size(repeatPasswordTXT.Size.Width, repeatPasswordTXT.Size.Height);
            securityAnsTXT.Location = new Point(securityAnsL.Location.X + 100, securityAnsL.Location.Y - 3);

            // BUTTONS
            createAccountBTN.Text = "Create";
            createAccountBTN.Location = new Point(250, 350);

            backToLoginBTN.Text = "Back";
            backToLoginBTN.Location = new Point(createAccountBTN.Location.X, createAccountBTN.Location.Y + 40);

            createAccPanel.Controls.Add(createAccTitleL);
            createAccPanel.Controls.Add(usernameL);
            createAccPanel.Controls.Add(usernameTXT);
            createAccPanel.Controls.Add(passwordL);
            createAccPanel.Controls.Add(passwordTXT);
            createAccPanel.Controls.Add(repeatPasswordL);
            createAccPanel.Controls.Add(repeatPasswordTXT);
            createAccPanel.Controls.Add(securityQnsL);
            createAccPanel.Controls.Add(securityQnsDD);
            createAccPanel.Controls.Add(securityAnsL);
            createAccPanel.Controls.Add(securityAnsTXT);
            createAccPanel.Controls.Add(createAccountBTN);
            createAccPanel.Controls.Add(backToLoginBTN);

            CreateAccountEvents();

            pageManager.AddThisPanel(createAccPanel);

            return createAccPanel;
        }

        // The DESIGN FORM CONTROLS for Successful Creation of Account PANEL
        public Control CreateAccountSuccessControls()
        {
            // Successful Panel
            successfulCreationPanel.Name = "createAcc_success";
            successfulCreationPanel.Size = new Size(800, 600);
            successL.Text = "Success! Go back to Login page and try!";
            successL.Font = new Font("Microsoft Sans Serif", 10);
            successL.Size = new Size(500, 100);
            successL.Location = new Point(180, 100);

            // USER INPUTS
            // BUTTON
            goToLoginPageBTN.Text = "Back To Login Page";
            goToLoginPageBTN.AutoSize = true;
            goToLoginPageBTN.Location = new Point(successL.Location.X + 40, successL.Location.Y + 100);

            // ADD CONTROLS
            successfulCreationPanel.Controls.Add(successL);
            successfulCreationPanel.Controls.Add(goToLoginPageBTN);

            // Add this panel into pageManager
            pageManager.AddThisPanel(successfulCreationPanel);

            return successfulCreationPanel;
        }

        // Add the RELEVANT EVENT HANDLER
        public void CreateAccountEvents()
        {
            createAccountBTN.Click += new EventHandler(createAccountBTN_Click);
            backToLoginBTN.Click += new EventHandler(backToLoginBTN_Click);
            goToLoginPageBTN.Click += new EventHandler(goToLoginPageBTN_Click);
        }

        private void createAccountBTN_Click(object _sender, EventArgs _e)
        {
            errorProvider.Clear();

            // Make sure ALL fields are filled in
            // Check for any EMPTY FIELDS
            if (usernameTXT.Text == "" || passwordTXT.Text == "" || repeatPasswordTXT.Text == "" || securityAnsTXT.Text == "")
                errorProvider.SetError(createAccountBTN, "There are empty fields!");
            // Check for any SPACE in USERNAME and PASSWORD
            else if (usernameTXT.Text.Contains(" "))
                errorProvider.SetError(usernameTXT, "No space for username!");
            // Check to see if the username has been used
            else if(!pageManager.GetUserDB().IsThisUsernameNew(usernameTXT.Text))
                errorProvider.SetError(usernameTXT, "Username is used by someone else!");
            else if (passwordTXT.Text.Contains(" "))
                errorProvider.SetError(passwordTXT, "No space for password!");
            // Check that Username only have 20 letters
            else if(usernameTXT.Text.Length > 20)
                errorProvider.SetError(usernameTXT, "Username can only be less than 20 characters!");
            // Check that the PASSWORD and REPEAT PASSWORD are correct
            else if (passwordTXT.Text != repeatPasswordTXT.Text)
                errorProvider.SetError(repeatPasswordTXT, "Password is different!");
            // Once ALL are FILLED IN and CORRECT
            else
            {
                // CREATE NEW USER
                Users.User newUser = new Users.User(usernameTXT.Text, passwordTXT.Text, securityAnsTXT.Text, securityQnsDD.SelectedIndex);
                pageManager.GetUserDB().CreateUser(newUser);

                pageManager.GetThisPanel(createAccPanel).Hide();
                Reset();
            }
        }

        private void backToLoginBTN_Click(object _sender, EventArgs _e)
        {
            pageManager.GetThisPanel("login").Show();
            Reset();
        }

        private void goToLoginPageBTN_Click(object _sender, EventArgs _e)
        {

            pageManager.GetThisPanel(createAccPanel).Show();
            pageManager.GetThisPanel("login").Show();
            Reset();
        }

        public void Reset()
        {
            usernameTXT.Text = "";
            passwordTXT.Text = "";
            repeatPasswordTXT.Text = "";
            securityAnsTXT.Text = "";
        }
    }
}
