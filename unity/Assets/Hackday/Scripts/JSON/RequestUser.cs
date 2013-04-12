using UnityEngine;
using System.Collections;

public class RequestUser {
	
        private string Username;
        private string Password;

        public string username
        {
            get { return this.Username; }
            set { this.Username = value; }
        }

        public string password
        {
            get { return this.Password; }
            set { this.Password = value; }
        }
}
