using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SecurePasswordStorage
{
    public partial class MainWindow : Window
    {
        private PBKDF2 pbkdf2;
        private MockData mockData;
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            pbkdf2 = new PBKDF2();
            mockData = new MockData();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            User user = new User(InputUsername.Text);
            user.Password = InputPassword.Text;
            Validate(user);
        }


        private void Validate(User user)
        {
            List<User> users = mockData.GetMockData();
            foreach (User user1 in users)
            {
                if (user1.Username == user.Username)
                {
                    user.Password = Convert.ToBase64String(pbkdf2.HashPassword(Encoding.UTF8.GetBytes(user.Password), user1.Salt, 100));
                    if (user1.Password == user.Password)
                    {
                        mockData.ResetLoginAttempts(user1);
                        SuccessfulWindow successfulWindow = new SuccessfulWindow();
                        successfulWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        successfulWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        if(mockData.AddLoginAttempt(user1) >= 5)
                        {
                            mockData.LockAccount(user1);
                            LockWindow lockWindow = new LockWindow();
                            lockWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            lockWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Password Attempts left : " + (5 - user1.LoginAttempts));
                        }
                            
                    }
                }
            }
        }
    }
}
