using UnityEngine;
using System.Collections;

public class RequestItemSearch {

	
        private string Keywords;
        private int PageSize = 5;

        public string keywords
        {
            get { return this.Keywords; }
            set { this.Keywords = value; }
        }

        public int pagesize
        {
            get { return this.PageSize; }
            set { this.PageSize = value; }
        }
}
