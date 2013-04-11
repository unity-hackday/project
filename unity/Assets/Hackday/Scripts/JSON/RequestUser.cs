using UnityEngine;
using System.Collections;

public class RequestUser {
	
        private string username;
        private string password;

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
}
