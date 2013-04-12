using UnityEngine;
using System.Collections;

public class ResponseAuthentication {
	
        private string AuthToken;
        private string TokenType;
        private string Expiry;

        public string access_token
        {
            get { return this.AuthToken; }
            set { this.AuthToken = value; }
        }
	
	    public string token_type
        {
            get { return this.TokenType; }
            set { this.TokenType = value; }
        }

        public string expires_in
        {
            get { return this.Expiry; }
            set { this.Expiry = value; }
        }
}

