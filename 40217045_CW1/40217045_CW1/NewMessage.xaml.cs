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
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.RegularExpressions;

namespace _40217045_CW1
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        string msgType = "";
        string user = "";
        string username = "";
        string name = "";
        double MyPhoneNo;
        string twitterhandle = "";
        string email = "";

        string Message;



        // Lists for storing data these will be read from when the window is opened and written to after a message is sent
        List<Sms> SmsList = new List<Sms>();
        List<Tweet> TweetList = new List<Tweet>();
        List<Email> EmailList = new List<Email>();
        List<User> UserList = new List<User>();

        // arrays for storing text speak 
        private string[] TextspeakArray = new string[] { };
        private string[] expandedArray = new string[] { };

        //List to store Incedents, Mentions, Hashtags and Quarantines
        List<Hashtags> Hashtag_List = new List<Hashtags>();
        List<Mentions> Trend_List = new List<Mentions>();
        List<Incidents> Incident_List = new List<Incidents>();
        List<string> Quarantine = new List<string>();

        public NewMessage(string messageID, string messageType, string username)
        {
            user = username;

            LoadLists(user);//Loads lists of sent messages 
            LoadUser(user);// loads users Details
            LoadTextWord(); //Loads method for text speak
            InitializeComponent();
            txtEmail.MaxLength = 1028;
            txtSms.MaxLength = 144;
            txtTweet.MaxLength = 140;
            msgType = messageType;
            //selects what canvas to display
            if (msgType == "SMS")
            {
                Title = "Send New SMS";
                lblSmsMessageID.Content = messageID;
                cvsSms.Visibility = Visibility.Visible;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
            }
            else if (msgType == "Email")
            {
                Title = "Send New Email";
                lblMessageID.Content = messageID;
                cvsEmail.Visibility = Visibility.Visible;
                txtCentreNumber1.MaxLength = 2;
                txtCentreNumber2.MaxLength = 3;
                txtCentreNumber3.MaxLength = 2;
                cvsTweet.Visibility = Visibility.Hidden;
                cvsSms.Visibility = Visibility.Hidden;
            }
            else if (msgType == "Tweet")
            {
                Title = "Send New Tweet";
                lblTweetMessageID.Content = messageID;
                cvsTweet.Visibility = Visibility.Visible;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsSms.Visibility = Visibility.Hidden;
            }
            else
            {
                cvsSms.Visibility = Visibility.Hidden;
                cvsEmail.Visibility = Visibility.Hidden;
                cvsTweet.Visibility = Visibility.Hidden;
            }
        }

        private void LoadUser(string user)
        {

            foreach (User U in UserList)
            {
                if (user == U.Username)
                {
                    username = U.Username;
                    name = U.Name;
                    email = U.EmailAdd;
                    twitterhandle = U.Twitter;
                    MyPhoneNo = U.Phonenumber;
                    break;

                }
            }
        }

        private void LoadLists(string user)
        {
            try
            {
                //loads sms messages from user
                string FileLocSms = @"Resources\User Messages\" + user + "-sms.json";
                string jsonSms = File.ReadAllText(FileLocSms);
                SmsList = JsonConvert.DeserializeObject<List<Sms>>(jsonSms);
            }
            catch (Exception) { }
            try
            {
                //loads tweets from user
                string FileLocTweet = @"Resources\User Messages\" + user + "-tweet.json";
                string jsonTweet = File.ReadAllText(FileLocTweet);
                TweetList = JsonConvert.DeserializeObject<List<Tweet>>(jsonTweet);
            }
            catch (Exception) { }
            try
            {
                //loads emails from user
                string FileLocEmail = @"Resources\User Messages\" + user + "-email.json";
                string jsonEmail = File.ReadAllText(FileLocEmail);
                EmailList = JsonConvert.DeserializeObject<List<Email>>(jsonEmail);
            }
            catch (Exception) { }
            try
            {
                //Loads users from file
                string FileLocUser = @"Resources\Users.json";
                string jsonUser = File.ReadAllText(FileLocUser);
                UserList = JsonConvert.DeserializeObject<List<User>>(jsonUser);
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



        private void txtRecipient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSendEmail_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            valid = EmailErrorCheck(valid);
            
            if (valid == true)
            {
                newEmail();
                SaveEmail(user);
                MessageBox.Show("Email Sent");
                this.Close();
            }
        }

        private bool EmailErrorCheck(bool v)
        {
            bool subjctValid = false;
            bool RecipientValid = false;
            //Email address validation
            if (txtRecipient.Text.Contains('@'))
            {
                if (txtRecipient.Text.Contains(".uk") || txtRecipient.Text.Contains(".com"))
                {
                    RecipientValid = true;
                }
                else
                {
                   // MessageBox.Show("Recipient is Invalid");
                    RecipientValid = false;

                }
            }
            else
            {
               // MessageBox.Show("Recipient is Invalid");
                RecipientValid = false;

            }

            if (txtSubject.Text.Length >= 1 && !txtSubject.Text.ToLower().Contains("sir"))
            {
                subjctValid = true;
            }
            else if (txtSubject.Text.Length >= 1 && txtSubject.Text.ToLower().Contains("sir"))
            {
                    if (txtCentreNumber1.Text.Length >=1 && txtCentreNumber2.Text.Length >= 1&& txtCentreNumber3.Text.Length >= 1 && !cmbNature.Text.Contains(""))
                {
                    subjctValid = true;
                }
                else
                {
                    subjctValid = false;
                }
            }
            else
            {
                subjctValid = false;
                //MessageBox.Show("Subject Required");
            }


            if (subjctValid == false && RecipientValid == false)
            {
                MessageBox.Show("Recipient and Subject are Invalid");
                return v = false;
            }
            else if (subjctValid == false && RecipientValid == true)
            {
                MessageBox.Show("Subject Required");
                return v = false;
            }
            else if (subjctValid == true && RecipientValid == false)
            {
                MessageBox.Show("Recipient is Invalid");
                return v = false;
            }
            else
            {
                v = true;
                return v;
            }
        }

        private void SaveEmail(string user)
        {
            // Saves Email
            string FileLoc = @"Resources\User Messages\" + user + "-Email.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(EmailList));
            Console.WriteLine("All data saved to " + FileLoc);

            // Saves Serious Incident Report
            string FileLocIncident = @"Resources\Incidents.json"; //filename where data will be stored
            File.WriteAllText(FileLocIncident, JsonConvert.SerializeObject(Incident_List));


        }

        private void newEmail()
        {
            ConvertTextWord();
            UrlCheck(Message);
            string CentreNumber = "N/A";
            string Incident = "N/A";
            //Serious Incident Report check
            if (txtSubject.Text.ToLower().Contains("sir"))
            {
                CentreNumber = txtCentreNumber1.Text + "-" + txtCentreNumber2.Text + "-" + txtCentreNumber3.Text;
                Incident = cmbNature.Text;
                Message = "Serious Incident Report \n Centre Number: " + CentreNumber + "\n Nature Of Incident: " + Incident + "\nMessage Reads:\n" + Message;

                //Checks if incedent has happened before

                try
                {
                    bool PreviousIncident = false;


                    foreach (Incidents I in Incident_List)
                    {

                        if (Incident == I.Incident)
                        {

                            I.Occurances = I.Occurances + 1;

                            PreviousIncident = true;
                            break;

                        }
                    }

                    if (PreviousIncident == false)
                    {
                        Incidents NewI = new Incidents(Incident, 1);
                        Incident_List.Add(NewI);
                    }
                    PreviousIncident = false;
                }
                catch (Exception) { }
            }



            Email E = new Email();
            E.MessageID = lblMessageID.Content.ToString();
            E.Sender = email;
            E.Recipient = txtRecipient.Text;
            E.Subject = txtSubject.Text;
            E.Message = Message;

            EmailList.Add(E);

        }

        private void btnCancelTweet_Click(object sender, RoutedEventArgs e)
        {
            txtTweet.Clear();
        }

        private void btnCancelSms_Click(object sender, RoutedEventArgs e)
        {
            txtSms.Clear();
        }

        private void btnSendTweet_Click(object sender, RoutedEventArgs e)
        {
            newTweet();
            SaveTweet(user);
            MessageBox.Show("Tweet Sent");
            this.Close();
        }

        private void SaveTweet(string user)
        {
            string FileLoc = @"Resources\User Messages\" + user + "-Tweet.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(TweetList));
            Console.WriteLine("All data saved to " + FileLoc);
        }

        private void newTweet()
        {
            ConvertTextWord();
            Tweet T = new Tweet();
            HashtagTrending(Message);
            MentionsTrend(Message);

            T.Message = Message;

            T.TweetID = lblTweetMessageID.Content.ToString();
            T.From = twitterhandle;
            TweetList.Add(T);
        }

        private void btnSendSms_Click(object sender, RoutedEventArgs e)
        {
            newSms();
            SaveSMS(user);
            MessageBox.Show("Sms Sent to " + txtTo.Text);
            this.Close();
        }

        private void newSms()
        {


            double PhoneNumber;
            try
            {
                PhoneNumber = double.Parse(txtTo.Text);
            }
            catch (Exception)
            {

                throw;
            }
            ConvertTextWord();
            Sms S = new Sms();
            S.Message = Message;
            S.To = PhoneNumber;
            S.SmsID = lblSmsMessageID.Content.ToString();
            S.From = MyPhoneNo;
            SmsList.Add(S);
        }

        private void SaveSMS(string user)
        {
            string FileLoc = @"Resources\User Messages\" + user + "-sms.json"; //filename where data will be stored
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(SmsList));
            Console.WriteLine("All data saved to " + FileLoc);
        }


        //methods for converting text speak
        private void LoadTextWord()
        {
            try
            {
                List<string> Abbreviations = new List<string>();
                List<string> Phrases = new List<string>();
                string filename = @"Resources/textwords.csv";
                StreamReader reader = new StreamReader(File.OpenRead(filename));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Abbreviations.Add(values[0]);
                    Phrases.Add(values[1]);

                }

                TextspeakArray = Abbreviations.ToArray();
                expandedArray = Phrases.ToArray();

            }

            catch (Exception e)
            {

                Console.WriteLine("Error: " + e);
            }

        }

        private void ConvertTextWord()
        {
            string text = "";
            if (msgType == "SMS")
            {
                text = " " + txtSms.Text + " ";
                ConvertTextWord(text);

            }
            else if (msgType == "Tweet")
            {
                text = " " + txtTweet.Text + " ";
                ConvertTextWord(text);
            }
            else if (msgType == "Email")
            {
                text = " " + txtEmail.Text + " ";
                ConvertTextWord(text);

            }

        }

        private void ConvertTextWord(string text)
        {

            int total = TextspeakArray.Count();
            for (int i = 0; i < total; i++)
            {
                string str = " " + TextspeakArray[i] + " ";

                //checks for the phrase is uppercase and expands it
                if (text.Contains(str))
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string expanded = expandedArray[i];
                    string Converted = Regex.Replace(text, str, str + " <" + expanded + "> ");
                    text = Converted;

                }
                // cheacks for the phrase is in lowercase and expands it
                else if (text.Contains(str.ToLower()) && str.ToLower() != "at")
                {
                    string filename = @"Resources/textwords.csv";
                    StreamReader reader = new StreamReader(File.OpenRead(filename));
                    string expanded = expandedArray[i];

                    string Converted = Regex.Replace(text, str.ToLower(), str.ToLower() + " <" + expanded + "> ");
                    text = Converted;

                }


            }

            Message = text;
        }

        //Checks if subject contains sir
        private void txtSubject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSubject.Text.ToLower().Contains("sir "))
            {
                this.Width = 500;

            }
            else
            {
                this.Width = 300;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Quarintine emails
        private void UrlCheck(string m)
        {
            string text = m;
            if (m.Contains("http://") || m.Contains("https://"))
            {
                WriteQuarantineList(m);
                string Cleaned = Regex.Replace(text, @"http[^\s]+", "<URL>");
                Message = Cleaned;
                if (m.Contains("www."))
                {
                    WriteQuarantineList(Cleaned);
                    Cleaned = Regex.Replace(Cleaned, @"www.[^\s]+", "<URL>");
                    Message = Cleaned;
                }


            }
            else
            {
                Message = text;
            }

        }

        private void WriteQuarantineList(string m)
        {
            var regex = new Regex(@"http[^\s]+");

            if (m.Contains("<URL>"))
            {
                regex = new Regex(@"www.[^\s]+");
            }

            var matches = regex.Matches(m);

            foreach (Match match in matches)
            {
                string Q = match.Value;
                Console.WriteLine(m);
                Quarantine.Add(Q);
            }

            // Saves Qurantined URL
            string FileLoc = @"Resources\Quarantine.json";
            File.WriteAllText(FileLoc, JsonConvert.SerializeObject(Quarantine));
            Console.WriteLine("All data saved to " + FileLoc);

        }
        //mmakes sure that only numbers are entered in to the centre code
        private void txtCentreNumber1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber1.Text = txtCentreNumber1.Text.Remove(txtCentreNumber1.Text.Length - 1);
            }
        }

        private void txtCentreNumber2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber2.Text = txtCentreNumber2.Text.Remove(txtCentreNumber2.Text.Length - 1);
            }
        }

        private void txtCentreNumber3_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(txtCentreNumber3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtCentreNumber3.Text = txtCentreNumber3.Text.Remove(txtCentreNumber3.Text.Length - 1);
            }
        }

        //Tweet Hash tags trending
        private void HashtagTrending(string m)
        {


            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(m);

            foreach (Match matched in matches)
            {
                string Htag = "#" + matched.Value;

                try
                {
                    bool hashtagexists = false;
                    foreach (Hashtags H in Hashtag_List)
                    {
                        if (Htag == H.Tag)
                        {

                            H.Trending = H.Trending + 1;
                            Console.WriteLine("Hashtag = " + H.Tag
                                             + " is trending " + H.Trending
                                             + " retweet(s)");

                            hashtagexists = true;
                            break;

                        }
                    }//for loop ends
                    if (hashtagexists == false)
                    {
                        Hashtags NewH = new Hashtags(Htag, 1);
                        Hashtag_List.Add(NewH);

                    }
                    hashtagexists = false;
                }
                catch (Exception) { }

            }

            string FileLoct = @"Resources\Hashtags.json"; //filename where data will be stored
            File.WriteAllText(FileLoct, JsonConvert.SerializeObject(Hashtag_List));


        }


        private void MentionsTrend(string m)
        {


            var regex = new Regex(@"(?<=@)\w+");
            var matches = regex.Matches(m);

            foreach (Match matched in matches)
            {
                string Mentiontag = "@" + matched.Value;

                try
                {
                    bool Mentionexists = false;
                    foreach (Mentions M in Trend_List)
                    {
                        if (Mentiontag == M.Handle)
                        {

                            M.Trending = M.Trending + 1;


                            Mentionexists = true;
                            break;

                        }
                    }//for loop ends
                    if (Mentionexists == false)
                    {
                        Mentions NewM = new Mentions(Mentiontag, 1);
                        Trend_List.Add(NewM);

                    }
                    Mentionexists = false;
                }
                catch (Exception) { }

            }

            string FileLoct = @"Resources\M-Trending.json"; //filename where data will be stored
            File.WriteAllText(FileLoct, JsonConvert.SerializeObject(Trend_List));


        }
    }


}
