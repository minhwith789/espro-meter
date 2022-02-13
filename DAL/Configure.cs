using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionApp.DAL
{
    internal class Configure
    {
        RegistryKey createRegistry = Registry.CurrentUser.CreateSubKey(@"Software\MyCompany");
        RegistryKey readRegistry = Registry.CurrentUser.OpenSubKey(@"Software\MyCompany");
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
       
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _database;

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }


        private string _server;

        public string ServerName
        {
            get { return _server; }
            set { _server = value; }
        }

        public Configure()
        {
            _userName = "";
            _password = "";
            _database = "";
            _server = "";
        }
        public Configure(string server,string database, string username,string password):this()
        {
            this._server = server;
            this._userName= username;
            this._password = password;
            this._database = database;
        }

        //public string Connection()
        //{
        //    return CreateConnectionString();
        //}


        public void RegisterServer()
        {
            try
            {
                createRegistry.SetValue("server", _server);
                createRegistry.SetValue("database", _database);
                createRegistry.SetValue("username", _userName);
                createRegistry.SetValue("password", _password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                createRegistry.Close();
            }

        }
        public void RegisterServer(string server, string database, string username, string password)
        {
            try
            {
                createRegistry.SetValue("server", server);
                createRegistry.SetValue("database", database);
                createRegistry.SetValue("username", username);
                createRegistry.SetValue("password", password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                createRegistry.Close();
            }
        }

        private bool CreateConnectionString()
        {
            var userName = readRegistry.GetValue("username",string.Empty).ToString();
            var password = readRegistry.GetValue("password",string.Empty).ToString();
            var database = readRegistry.GetValue("database",string.Empty).ToString();
            var server = readRegistry.GetValue("server",string.Empty).ToString();
            var connectionString = $"serever={server};initial catalog={database};User Id={userName};password={password};";
            return false;
        }
    }
}
