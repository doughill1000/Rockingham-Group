using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;

public partial class Wepages_MapsTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //int zip;
            SqlConnection conn = Website.getSQLConnection();
            SqlCommand cmd = Website.getSQLCommand(conn);
            cmd.CommandText = "SELECT * FROM AGENCY";
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdrAgencies;
            conn.Open();
            rdrAgencies = cmd.ExecuteReader();

            string markers = "";
            string mylatlng = getLatLng(22801);
            // applicant's marker
            markers += @" var coords = new google.maps.LatLng(" + mylatlng + @");
                var image = '../Images/map_marker_home.png';
                var markerHome = new google.maps.Marker({
                    position: coords,
                    map: map,
                    title: markerHome,
                    icon: image
                }); 
                var agency_icon = '../Images/map_agency_icon.png';";
            int counter = 0;
            while (rdrAgencies.Read())
            {
                string agencyName = rdrAgencies.GetString(0);
                agencyName = agencyName.Substring(0, agencyName.IndexOf(" "));
                string streetaddress = Website.getSafeString(rdrAgencies,1);
                string city = rdrAgencies.GetString(2);
                string state = rdrAgencies.GetString(3);
                string zipCode = rdrAgencies.GetString(4);

                string latlng = getLatLng(Convert.ToInt32(zipCode));

                //agency markers (looped)
                markers += @" var contentString" + counter + @" = '<div id=""test""><p>" + agencyName + @"</p><p>" + streetaddress + @"</p><p>" + city + ", " + state + " " + zipCode + @"</p></div>';
            
                
                var iw" + counter + @" = new google.maps.InfoWindow({
                    content: contentString" + counter + @"
                });
            
                var coords = new google.maps.LatLng(" +latlng+@");
                var marker" + counter + @" = new google.maps.Marker({
                    position: coords,
                    map: map,
                    icon: agency_icon,
                    title: '" + counter + @"'
                });
                var circle" + counter+ @" = new google.maps.Circle({
                  center:coords,
                  radius:80467.2,
                  strokeColor:""#0000FF"",strokeOpacity:0.9,strokeWeight:1,fillColor:""#0000FF"",fillOpacity:0.1
                });
                circle" +counter+@".setMap(map);
                google.maps.event.addListener(marker" + counter + @", 'click', function () {
                    
                    if (infowindow) infowindow.close();
                      infowindow = new google.maps.InfoWindow({content: contentString" + counter + @"});
                      infowindow.open(map, marker" + counter + @");
                    map.position = marker" + counter + @".position;
                    
                });";
                counter++;
            }


            //string locations = "";

            string map = @"<script>var infowindow; function initialize() {
            var myLatlng = new google.maps.LatLng(" + mylatlng + ");" +
                @"var mapOptions = {
                zoom: 7,
                center: myLatlng
            };

            var map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);";

            //end for loop
            map += markers;

            map += @" }
            google.maps.event.addDomListener(window, 'load', initialize);

            
            </script>";
            jsMap.Text = map;
        }
        catch (Exception excp)
        {
            
        }
    }

    protected string getLatLng(int postalcode)
    {
        string url = "http://maps.google.com/maps/api/geocode/xml?address="+postalcode+"&sensor=false";
        WebRequest request = WebRequest.Create(url);
        using (WebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                try
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        string latitude = location["lat"].ToString();
                        string longitude = location["lng"].ToString();
                        return latitude + ", " + longitude;
                    }
                }
                catch { return "38.4661199, -78.7888860"; ; }
            }
        }
        return "38.4661199, -78.7888860";
    }

}