using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PRSErrorMonitor
{
    // Created this class to decouple the dependencies some of the classes had on the config file
    public class ConfigFileHelper : PRSErrorMonitor.IConfigFileHelper
    {
        private int _prsErrorCheckFrequency;
        private DateTime _timeLastPrsErrorCheckWasRun;
        private int _prsUnavailableErrorLimit;
        private int _prsTimeoutErrorLimit;
        private int _prsTotalErrorLimit;
        private string _emailSubject;
        private string _unavailableErrorLimitExceededBodyText;
        private string _timeoutErrorLimitExceededBodyText;
        private string _totalErrorLimitExceededBodyText;
        private string _emailToAddress;
        private string _emailFromAddress;
        private string _emailCCAddress;
        private string _emailReplyToAddress;
        private int _emailSentFlag;

        public ConfigFileHelper()
        {
            _prsErrorCheckFrequency = Convert.ToInt32(ConfigurationManager.AppSettings["PrsErrorCheckFrequency"]);
            _timeLastPrsErrorCheckWasRun = Convert.ToDateTime(ConfigurationManager.AppSettings["TimeLastPrsErrorCheckWasRun"]);
            _prsUnavailableErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsUnavailableErrorLimit"]);
            _prsTimeoutErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsTimeoutErrorLimit"]);
            _prsTotalErrorLimit = Convert.ToInt32(ConfigurationManager.AppSettings["PrsTotalErrorLimit"]);
            _emailSubject = ConfigurationManager.AppSettings["EmailSubject"];
            _unavailableErrorLimitExceededBodyText = ConfigurationManager.AppSettings["UnavailableErrorLimitExceededBodyText"];
            _timeoutErrorLimitExceededBodyText = ConfigurationManager.AppSettings["TimeoutErrorLimitExceededBodyText"];
            _totalErrorLimitExceededBodyText = ConfigurationManager.AppSettings["TotalErrorLimitExceededBodyText"];
            _emailToAddress = ConfigurationManager.AppSettings["EmailToAddress"];
            _emailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            _emailCCAddress = ConfigurationManager.AppSettings["EmailCCAddresses"];
            _emailReplyToAddress = ConfigurationManager.AppSettings["EmailReplyToAddress"];
            _emailSentFlag = Convert.ToInt32(ConfigurationManager.AppSettings["EmailSentFlag"]);
        }

        public int PrsErrorCheckFrequency
        {
            get { return _prsErrorCheckFrequency; }
            set { ; }
        }
        public DateTime TimeLastPrsErrorCheckWasRun
        {
            get { return _timeLastPrsErrorCheckWasRun; }
            set { ; }
        }
        public int PrsUnavailableErrorLimit
        {
            get { return _prsUnavailableErrorLimit; }
            set { ; }
        }
        public int PrsTimeoutErrorLimit
        {
            get { return _prsTimeoutErrorLimit; }
            set { ; }
        }
        public int PrsTotalErrorLimit
        {
            get { return _prsTotalErrorLimit; }
            set { ; }
        }
        public string EmailSubject
        {
            get { return _emailSubject; }
            set { ; }
        }
        public string UnavailableErrorLimitExceededBodyText
        {
            get { return _unavailableErrorLimitExceededBodyText; }
            set { ; }
        }
        public string TimeoutErrorLimitExceededBodyText
        {
            get { return _timeoutErrorLimitExceededBodyText; }
            set { ; }
        }
        public string TotalErrorLimitExceededBodyText
        {
            get { return _totalErrorLimitExceededBodyText; }
            set { ; }
        }
        public string EmailToAddress
        {
            get { return _emailToAddress; }
            set { ; }
        }
        public string EmailFromAddress
        {
            get { return _emailFromAddress; }
            set { ; }
        }
        public string EmailCCAddresses
        {
            get { return _emailCCAddress; }
            set { ; }
        }
        public string EmailReplyToAddress
        {
            get { return _emailReplyToAddress; }
            set { ; }
        }
        public int EmailSentFlag
        {
            get { return _emailSentFlag; }
            set { ; }
        }

        public void UpdateTimeLastPrsErrorCheckWasRun()
        {
            _timeLastPrsErrorCheckWasRun = DateTime.Now;
            Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _configFile.AppSettings.Settings.Remove("TimeLastPrsErrorCheckWasRun");
            _configFile.AppSettings.Settings.Add("TimeLastPrsErrorCheckWasRun", _timeLastPrsErrorCheckWasRun.ToString());
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void RecordEmailHavingBeenSent()
        {
            _emailSentFlag = 1;
            Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _configFile.AppSettings.Settings.Remove("EmailSentFlag");
            _configFile.AppSettings.Settings.Add("EmailSentFlag", _emailSentFlag.ToString());
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
