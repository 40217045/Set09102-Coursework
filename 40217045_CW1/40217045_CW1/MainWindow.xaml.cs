using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color SelectedColour = (Color)ColorConverter.ConvertFromString("#4E98FE");
        Color UnselectedColour = (Color)ColorConverter.ConvertFromString("#F0F0F0");
        List<Sms> SmsList = new List<Sms>();
        List<Tweet> TweetList = new List<Tweet>();
        List<Email> EmailList = new List<Email>();

        //List to store Incedents, Mentions, Hashtags and Quarantines
        List<Hashtags> Hashtag_List = new List<Hashtags>();
        List<Mentions> Trend_List = new List<Mentions>();
        List<Incidents> Incident_List = new List<Incidents>();
        List<string> Quarantine = new List<string>();

        string messageID = "";
        string messageType = "";
        string Username = "";
        public MainWindow(string user)
        {
            InitializeComponent();
            txtMessageID.MaxLength = 10;
            //MessageIDSelection();
            Username = user;
            Title = "Welcome " + user;
            LoadLists(user);
        }

        private void LoadLists(string user)
        {
            try
            {
                string FileLocSms = @"Resources\User Messages\" + user + "-sms.json";
                string jsonSms = File.ReadAllText(FileLocSms);
                SmsList = JsonConvert.DeserializeObject<List<Sms>>(jsonSms);
            }
            catch (Exception)
            {


            }
            try
            {
                string FileLocTweet = @"Resources\User Messages\" + user + "-tweet.json";
                string jsonTweet = File.ReadAllText(FileLocTweet);
                TweetList = JsonConvert.DeserializeObject<List<Tweet>>(jsonTweet);
            }
            catch (Exception)
            {


            }
            try
            {
                string FileLocEmail = @"Resources\User Messages\" + user + "-email.json";
                string jsonEmail = File.ReadAllText(FileLocEmail);
                EmailList = JsonConvert.DeserializeObject<List<Email>>(jsonEmail);

            }
            catch (Exception)
            {


            }

            try
            {
                //Loads Incident List
                string FileLocSIR = @"Resources\Incidents.json";
                string jsonSIR = File.ReadAllText(FileLocSIR);
                Incident_List = JsonConvert.DeserializeObject<List<Incidents>>(jsonSIR);
            }
            catch (Exception) { }

            try
            {
                //Loads Quarantine URLS
                string FileLocQuarantine = @"Resources\Quarantine.json";
                string jsonQuarantine = File.ReadAllText(FileLocQuarantine);
                Quarantine = JsonConvert.DeserializeObject<List<string>>(jsonQuarantine);
            }
            catch (Exception) { }

            try
            {
                //Loads  Hashtag
                string FileLocHash = @"Resources\Hashtags.json";
                string jsonHash = File.ReadAllText(FileLocHash);
                Hashtag_List = JsonConvert.DeserializeObject<List<Hashtags>>(jsonHash);
            }
            catch (Exception) { }

            try
            {
                //Loads Mentions
                string FileLocTrends = @"Resources\M-Trending.json";
                string jsonTrends = File.ReadAllText(FileLocTrends);
                Trend_List = JsonConvert.DeserializeObject<List<Mentions>>(jsonTrends);
            }
            catch (Exception) { }
        }

        private void MessageIDSelection()
        {
            messageID = txtMessageID.Text.ToUpper();


            if (messageID.StartsWith("S"))
            {
                //MessageBox.Show("SMS messageID = " + messageID);
                messageType = "SMS";
                NewMessage NewWin = new NewMessage(messageID, messageType, Username);
                NewWin.ShowDialog();
            }
            else if (messageID.StartsWith("E"))
            {
                //MessageBox.Show("Email messageID = " + messageID);
                messageType = "Email";
                NewMessage NewWin = new NewMessage(messageID, messageType, Username);
                NewWin.ShowDialog();
            }
            else if (messageID.StartsWith("T"))
            {
                //MessageBox.Show("Tweet messageID = " + messageID);
                messageType = "Tweet";
                NewMessage NewWin = new NewMessage(messageID, messageType, Username);
                NewWin.ShowDialog();
            }
            else
            {
                MessageBox.Show("MessageID = " + messageID + " is not a valid MessageID");
                messageType = "invaild";
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (cvsInbox.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(SelectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Visible;
                cvsSend.Visibility = Visibility.Hidden;
                cvsReports.Visibility = Visibility.Hidden;
                cvsSent.Visibility = Visibility.Hidden;
                cvsSettings.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Hidden;

            }
            //MessageBox.Show("Inbox", "Inbox Canvas", MessageBoxButton.OK);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (cvsSend.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(SelectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Visible;
                cvsReports.Visibility = Visibility.Hidden;
                cvsSent.Visibility = Visibility.Hidden;
                cvsSettings.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsSend.Visibility = Visibility.Hidden;
            }
            //MessageBox.Show("Send", "Send Canvas", MessageBoxButton.OK);
        }

        private void BtnReports_Click(object sender, RoutedEventArgs e)
        {
            if (cvsReports.Visibility == Visibility.Hidden)
            {
                BtnReports.Background = new SolidColorBrush(SelectedColour);
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsReports.Visibility = Visibility.Visible;
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Hidden;
                cvsSettings.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsReports.Visibility = Visibility.Hidden;
                cvsSent.Visibility = Visibility.Hidden;
            }
        }
        private void btnSms_Click(object sender, RoutedEventArgs e)
        {
            if (cvsSms.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(SelectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsSms.Visibility = Visibility.Visible;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;

                lstSmsInbox.Items.Clear();

            }
            else
            {
                lstSmsInbox.Items.Clear();
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsSms.Visibility = Visibility.Hidden;
            }

            lstSmsInbox.Items.Add("Hello World");
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (cvsEmail.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(SelectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsEmail.Visibility = Visibility.Visible;
                cvsSms.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;
                lstEmailInbox.Items.Clear();
            }
            else
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsEmail.Visibility = Visibility.Hidden;
                lstEmailInbox.Items.Clear();
            }
            lstEmailInbox.Items.Add("Hello World");
        }

        private void btnTweet_Click(object sender, RoutedEventArgs e)
        {
            if (cvsTweet.Visibility == Visibility.Hidden)
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(SelectedColour);
                cvsTweet.Visibility = Visibility.Visible;
                cvsSms.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
                lstTweetInbox.Items.Clear();

            }
            else
            {
                btnSms.Background = new SolidColorBrush(UnselectedColour);
                btnEmail.Background = new SolidColorBrush(UnselectedColour);
                btnTweet.Background = new SolidColorBrush(UnselectedColour);
                cvsTweet.Visibility = Visibility.Hidden;
                lstTweetInbox.Items.Clear();
            }
            lstTweetInbox.Items.Add("Hello World");
        }

        private void BtnMessageID_Click(object sender, RoutedEventArgs e)
        {
            MessageIDSelection();
        }

        private void BtnSent_Click(object sender, RoutedEventArgs e)
        {
            LoadLists(Username);
            if (cvsSent.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(SelectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Hidden;
                cvsReports.Visibility = Visibility.Hidden;
                cvsSettings.Visibility = Visibility.Hidden;
                cvsSent.Visibility = Visibility.Visible;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                cvsSent.Visibility = Visibility.Hidden;

            }
        }

        private void btnSmsSent_Click(object sender, RoutedEventArgs e)
        {
            if (cvsSmsSent.Visibility == Visibility.Hidden)
            {
                btnSmsSent.Background = new SolidColorBrush(SelectedColour);
                btnEmailSent.Background = new SolidColorBrush(UnselectedColour);
                btnTweetSent.Background = new SolidColorBrush(UnselectedColour);
                cvsSmsSent.Visibility = Visibility.Visible;
                cvsEmailSent.Visibility = Visibility.Hidden;
                cvsTweetSent.Visibility = Visibility.Hidden;
                lstSmsSent.Items.Clear();
            }
            else
            {
                btnSmsSent.Background = new SolidColorBrush(UnselectedColour);
                btnEmailSent.Background = new SolidColorBrush(UnselectedColour);
                btnTweetSent.Background = new SolidColorBrush(UnselectedColour);
                cvsSmsSent.Visibility = Visibility.Hidden;
                lstSmsSent.Items.Clear();
            }
            if (SmsList.Count() != 0)
            {
                foreach (Sms S in SmsList)
                {
                    lstSmsSent.Items.Add("SmsID: " + S.SmsID +
                                            "\nSms Sender: " + S.From +
                                             "\nSms Recipient: " + S.To +
                                             "\nSms Message: " + S.Message +
                                             "\n----------------");
                }
            }
            else
            {
                lstSmsSent.Items.Add("There are no SMS,  You have not sent any Sms");
            }

        }

        private void btnEmailSent_Click(object sender, RoutedEventArgs e)
        {
            if (cvsEmailSent.Visibility == Visibility.Hidden)
            {
                btnSmsSent.Background = new SolidColorBrush(UnselectedColour);
                btnEmailSent.Background = new SolidColorBrush(SelectedColour);
                btnTweetSent.Background = new SolidColorBrush(UnselectedColour);
                cvsEmailSent.Visibility = Visibility.Visible;
                cvsSmsSent.Visibility = Visibility.Hidden;
                cvsTweetSent.Visibility = Visibility.Hidden;
                lstEmailSent.Items.Clear();
            }
            else
            {
                btnSmsSent.Background = new SolidColorBrush(UnselectedColour);
                btnEmailSent.Background = new SolidColorBrush(UnselectedColour);
                btnTweetSent.Background = new SolidColorBrush(UnselectedColour);
                cvsEmailSent.Visibility = Visibility.Hidden;
                lstEmailSent.Items.Clear();
            }

            if (EmailList.Count() != 0)
            {
                foreach (Email E in EmailList)
                {
                    lstEmailSent.Items.Add("EmailID: " + E.MessageID +
                                            "\nEmail Sender: " + E.Sender +
                                             "\nEmail Recipient: " + E.Recipient +
                                             "\nEmail Subject: " + E.Subject +
                                             "\nEmail Message: " + E.Message +
                                             "\n----------------");
                }
            }
            else
            {
                lstEmailSent.Items.Add("There are no Emails, You have not sent any emails");
            }
        }

        private void btnTweetSent_Click(object sender, RoutedEventArgs e)
        {
            if (cvsTweetSent.Visibility == Visibility.Hidden)
            {
                btnSmsSent.Background = new SolidColorBrush(UnselectedColour);
                btnEmailSent.Background = new SolidColorBrush(UnselectedColour);
                btnTweetSent.Background = new SolidColorBrush(SelectedColour);
                cvsTweetSent.Visibility = Visibility.Visible;
                cvsSmsSent.Visibility = Visibility.Hidden;
                cvsEmailSent.Visibility = Visibility.Hidden;
                lstTweetSent.Items.Clear();
            }
            else
            {
                btnSmsSent.Background = new SolidColorBrush(UnselectedColour);
                btnEmailSent.Background = new SolidColorBrush(UnselectedColour);
                btnTweetSent.Background = new SolidColorBrush(UnselectedColour);
                cvsTweetSent.Visibility = Visibility.Hidden;
                lstTweetSent.Items.Clear();
            }

            if (TweetList.Count() != 0)
            {
                foreach (Tweet T in TweetList)
                {
                    lstTweetSent.Items.Add("TweetID: " + T.TweetID +
                                            "\nTwitter Handle: " + T.From +
                                             "\nTweet: " + T.Message +
                                             "\n----------------");
                }
            }
            else
            {
                lstTweetSent.Items.Add("There are no Tweets,  You have not sent any Tweets");
            }
        }

        private void mnuLogOut_Click(object sender, RoutedEventArgs e)
        {
            Login newWin = new Login();
            newWin.Show();
            this.Close();
        }

        private void mnuSettings_Click(object sender, RoutedEventArgs e)
        {
            if (cvsSettings.Visibility == Visibility.Hidden)
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(SelectedColour);
                cvsInbox.Visibility = Visibility.Hidden;
                cvsSend.Visibility = Visibility.Hidden;
                cvsReports.Visibility = Visibility.Hidden;
                cvsSent.Visibility = Visibility.Hidden;
                cvsSettings.Visibility = Visibility.Visible;
            }
            else
            {
                BtnInbox.Background = new SolidColorBrush(UnselectedColour);
                BtnSend.Background = new SolidColorBrush(UnselectedColour);
                BtnReports.Background = new SolidColorBrush(UnselectedColour);
                BtnSent.Background = new SolidColorBrush(UnselectedColour);
                mnuSettings.Background = new SolidColorBrush(UnselectedColour);
                cvsSettings.Visibility = Visibility.Hidden;

            }
        }

        private void btnHash_Click(object sender, RoutedEventArgs e)
        {
            if (cvsHash.Visibility == Visibility.Hidden)
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(SelectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Visible;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstHash.Items.Clear();
            }

            else
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                lstHash.Items.Clear();
            }

            if (Hashtag_List.Count() != 0)
            {
                foreach (Hashtags H in Hashtag_List)
                {
                    lstHash.Items.Add("Hashtag: " + H.Tag +
                                            "\nNumber Times used : " + H.Trending +
                                             "\n----------------");
                }
            }
            else
            {
                lstHash.Items.Add("There are no Hashtags");
            }

           
        }

        private void btnTrends_Click(object sender, RoutedEventArgs e)
        {
            if (cvsTrends.Visibility == Visibility.Hidden)
            {
                btnTrends.Background = new SolidColorBrush(SelectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Visible;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstTrends.Items.Clear();
            }

            else
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstTrends.Items.Clear();
            }

            if (Trend_List.Count() != 0)
            {
                foreach (Mentions M in Trend_List)
                {
                    lstTrends.Items.Add("Twitter Handle: " + M.Handle +
                                            "\nNumber Times used : " + M.Trending +
                                             "\n----------------");
                }
            }
            else
            {
                lstTrends.Items.Add("There are no Trends to be shown");
            }
        }

        private void btnURLs_Click(object sender, RoutedEventArgs e)
        {
            if (cvsURLs.Visibility == Visibility.Hidden)
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(SelectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Visible;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstURLs.Items.Clear();
            }

            else
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstURLs.Items.Clear();
            }

            if (Quarantine.Count() != 0)
            {
                foreach (string s in Quarantine)
                {
                    lstURLs.Items.Add("Quarintined URL: " + s +
                                            
                                             "\n----------------");
                }
            }
            else
            {
                lstURLs.Items.Add("There are no Quarintined URLs");
            }
        }

        private void btnIncidents_Click(object sender, RoutedEventArgs e)
        {
            if (cvsIncidents.Visibility == Visibility.Hidden)
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Visible;
                lstIncidents.Items.Clear();
            }

            else
            {
                btnTrends.Background = new SolidColorBrush(UnselectedColour);
                btnHash.Background = new SolidColorBrush(UnselectedColour);
                btnURLs.Background = new SolidColorBrush(UnselectedColour);
                btnIncidents.Background = new SolidColorBrush(UnselectedColour);
                cvsHash.Visibility = Visibility.Hidden;
                cvsTrends.Visibility = Visibility.Hidden;
                cvsURLs.Visibility = Visibility.Hidden;
                cvsIncidents.Visibility = Visibility.Hidden;
                lstIncidents.Items.Clear();
            }

            if (Incident_List.Count() != 0)
            {
                foreach (Incidents I in Incident_List)
                {
                    lstIncidents.Items.Add("Incident Type: " + I.Incident +
                                            "\nNumber Occurances : " + I.Occurances +
                                             "\n----------------");
                }
            }
            else
            {
                lstHash.Items.Add("No Incidents have occured");
            }
        }
    }
}
