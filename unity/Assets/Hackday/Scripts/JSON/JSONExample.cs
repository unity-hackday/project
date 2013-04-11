using UnityEngine;
using System.Collections;
using JsonExSerializer;

public class JSONExample : MonoBehaviour {
	
	public class Customer {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        public string PhoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }
    }
	
	void Start() {
		// customer
	    Customer customer = new Customer();
	    customer.Id = 1;
	    customer.FirstName = "Bob";
	   	customer.LastName = "Smith";
	    customer.PhoneNumber = "(222)444-9987";
	
	    // serialize to a string
	    Serializer serializer = new Serializer(typeof(Customer));
		//**** JSON Text **** 
	    string jsonText = serializer.Serialize(customer);
	    Debug.Log(jsonText);
		
		// **** JSON to object 
		Customer deserializedCustomer = (Customer) serializer.Deserialize(jsonText);
	}
}
