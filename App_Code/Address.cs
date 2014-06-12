using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Address
/// </summary>
public class Address
{
    public string addressLine1 { get; set; }
    public string addressLine2 { get; set; }
    public string aptLot { get; set; }
    public string zip { get; set; }
    public string city { get; set; }
    public string state { get; set; }

    public static List<Address> addresses = new List<Address>();

	public Address()
	{
		
	}
}