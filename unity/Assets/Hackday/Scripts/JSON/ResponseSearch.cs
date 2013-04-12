using UnityEngine;
using System.Collections;

public class ResponseSearch : Response {

	
	
	public class Pagination {
		private int Current;
        private int PageSize;
        private int Pages;
        private int Results;
        private int ResultsOnPage;

        public int current
        {
            get { return this.Current; }
            set { this.Current = value; }
        }
	
	    public int pageSize
        {
            get { return this.PageSize; }
            set { this.PageSize = value; }
        }
		
		public int pages
        {
            get { return this.Pages; }
            set { this.Pages = value; }
        }

        public int results
        {
            get { return this.Results; }
            set { this.Results = value; }
        }
		
        public int resultsOnPage
        {
            get { return this.ResultsOnPage; }
            set { this.ResultsOnPage = value; }
        }
	}
}
