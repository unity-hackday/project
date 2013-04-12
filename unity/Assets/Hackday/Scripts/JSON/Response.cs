using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Response {
	
	public class Self {
		private string Type;
        private string Uri;
        private string Href;
        private string MaxAge;

        public string type
        {
            get { return this.Type; }
            set { this.Type = value; }
        }
	
	    public string uri
        {
            get { return this.Uri; }
            set { this.Uri = value; }
        }
		
		public string href
        {
            get { return this.Href; }
            set { this.Href = value; }
        }

        public string maxage
        {
            get { return this.MaxAge; }
            set { this.MaxAge = value; }
        }
	}
	
	public class Links {
		private string Type;
        private string Uri;
        private string Href;
        private string Rel;
        private string Rev;

        public string type
        {
            get { return this.Type; }
            set { this.Type = value; }
        }
	
	    public string uri
        {
            get { return this.Uri; }
            set { this.Uri = value; }
        }
		
		public string href
        {
            get { return this.Href; }
            set { this.Href = value; }
        }

        public string rel
        {
            get { return this.Rel; }
            set { this.Rel = value; }
        }

        public string rev
        {
            get { return this.Rev; }
            set { this.Rev = value; }
        }
	}
	
        private Self _self;
        private List<Links> _links;

        public Self self
        {
            get { return this._self; }
            set { this._self = value; }
        }
	
	    public List<Links> links
        {
            get { return this._links; }
            set { this._links = value; }
        }
}
