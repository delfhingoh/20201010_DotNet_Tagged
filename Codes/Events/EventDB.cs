using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;

namespace ICT365_A01_33836223.Events
{
    public class EventDB
    {
        private Dictionary<string, Event> theEvents = new Dictionary<string, Event>();  // 1 EventDB can have many EVENTS
        private int numOfEvents = 0;
        private string filePath = "eventDB/";
        private string thisUsername = "";

        public void CreateEventDB(string _username)
        {
            // Check and see if the file exists
            if (!File.Exists(filePath + _username + "EventDB.xml"))
            {
                // Create new file
                XDocument newFile = new XDocument(new XDeclaration("1.0", "utf-8", null));
                // Prepare NAMESPACES
                XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
                XNamespace SOAP_ENV_STYLE = "http://www.w3.org/2001/12/soap-encoding";
                XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

                // The FIRST element of the file with TWO attributes
                XElement firstElement = new XElement(SOAP_ENV + "Envelope",
                                            new XAttribute(XNamespace.Xmlns + "SOAP-ENV", SOAP_ENV),
                                            new XAttribute(SOAP_ENV + "encodingStyle", SOAP_ENV_STYLE));

                // The CHILD of the FIRST element
                XElement secondElement = new XElement(SOAP_ENV + "Body",
                                            new XAttribute(XNamespace.Xmlns + "lle", LIFELOG));

                newFile.Add(firstElement);
                firstElement.Add(secondElement);

                thisUsername = _username;
                newFile.Save(filePath + _username + "EventDB.xml");
            }
            // If the file exists
            else
            {
                XDocument thisFile = new XDocument(XDocument.Load(filePath + _username + "EventDB.xml"));
                // Prepare NAMESPACE
                XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
                XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

                // Check if there's any EVENTS from the file
                IEnumerable<XElement> events = thisFile.Element(SOAP_ENV + "Envelope").Element(SOAP_ENV + "Body").Elements(LIFELOG + "Event");
                foreach (XElement thisEvent in events) { numOfEvents++; }

                // Load the DATA from the XML FILE into a DICTIONARY
                if (numOfEvents > 0)
                {
                    // Check TYPE of EVENT
                    foreach (XElement thisEvent in events)
                    {
                        string id = thisEvent.Element(LIFELOG + "eventID").Value;
                        XElement thisType = (XElement)thisEvent.Element(LIFELOG + "eventID").NextNode;

                        if (thisType.Name == LIFELOG + "Twitter")
                            LoadTwitterEvent(id, thisType);
                        else if (thisType.Name == LIFELOG + "Facebook")
                            LoadFacebookEvent(id, thisType);
                        else if (thisType.Name == LIFELOG + "Photo")
                            LoadPhotoEvent(id, thisType);
                        else if (thisType.Name == LIFELOG + "Video")
                            LoadVideoEvent(id, thisType);
                    }
                }
            }
        }

        // ADDING DATA INTO XML FILE
        public void AddTwitterEvent(string _username, int _type, double _lat, double _longi, string _tweet, DateTime _dateTime)
        {
            // Open File
            XDocument thisFile = new XDocument(XDocument.Load(filePath + _username + "EventDB.xml"));
            XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

            // Add this EVENT into DICTIONARY
            string id = "ID" + (numOfEvents + 1);
            Event thisTweet = new Event(id, _type, _lat, _longi, _tweet, _dateTime);
            theEvents.Add(id, thisTweet);
            numOfEvents++;

            // Add this EVENT into LIST
            XElement newTweet = new XElement(LIFELOG + "Event",
                                            new XElement(LIFELOG + "eventID", id),
                                            new XElement(LIFELOG + "Twitter",
                                                    new XElement(LIFELOG + "Text", _tweet),
                                                    new XElement(LIFELOG + "Location",
                                                            new XElement(LIFELOG + "Lat", _lat),
                                                            new XElement(LIFELOG + "Longi", _longi)),
                                                    new XElement(LIFELOG + "DateTime", _dateTime.ToString())));

            thisFile.Element(SOAP_ENV + "Envelope").Element(SOAP_ENV + "Body").Add(newTweet);
            thisFile.Save(filePath + _username + "EventDB.xml");
        }

        public void AddFacebookEvent(string _username, int _type, double _lat, double _longi, string _status, DateTime _dateTime)
        {
            // Open File
            XDocument thisFile = new XDocument(XDocument.Load(filePath + _username + "EventDB.xml"));
            XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

            // Add this EVENT into DICTIONARY
            string id = "ID" + (numOfEvents + 1);
            Event thisStatus = new Event(id, _type, _lat, _longi, _status, _dateTime);
            theEvents.Add(id, thisStatus);
            numOfEvents++;

            // Add this EVENT into LIST
            XElement newStatus = new XElement(LIFELOG + "Event",
                                            new XElement(LIFELOG + "eventID", id),
                                            new XElement(LIFELOG + "Facebook",
                                                    new XElement(LIFELOG + "Text", _status),
                                                    new XElement(LIFELOG + "Location",
                                                            new XElement(LIFELOG + "Lat", _lat),
                                                            new XElement(LIFELOG + "Longi", _longi)),
                                                    new XElement(LIFELOG + "DateTime", _dateTime.ToString())));

            thisFile.Element(SOAP_ENV + "Envelope").Element(SOAP_ENV + "Body").Add(newStatus);
            thisFile.Save(filePath + _username + "EventDB.xml");
        }

        public void AddPhotoEvent(string _username, int _type, double _lat, double _longi, string _path)
        {
            // Open File
            XDocument thisFile = new XDocument(XDocument.Load(filePath + _username + "EventDB.xml"));
            XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

            // Add this EVENT into DICTIONARY
            string id = "ID" + (numOfEvents + 1);
            Event thisPhoto = new Event(id, _type, _lat, _longi, _path);
            theEvents.Add(id, thisPhoto);
            numOfEvents++;

            // Add this EVENT into LIST
            XElement newPhoto = new XElement(LIFELOG + "Event",
                                            new XElement(LIFELOG + "eventID", id),
                                            new XElement(LIFELOG + "Photo",
                                                    new XElement(LIFELOG + "FilePath", _path),
                                                    new XElement(LIFELOG + "Location",
                                                            new XElement(LIFELOG + "Lat", _lat),
                                                            new XElement(LIFELOG + "Longi", _longi))));

            thisFile.Element(SOAP_ENV + "Envelope").Element(SOAP_ENV + "Body").Add(newPhoto);
            thisFile.Save(filePath + _username + "EventDB.xml");
        }

        public void AddVideoEvent(string _username, int _type, double _lat, double _longi, string _path)
        {
            // Open File
            XDocument thisFile = new XDocument(XDocument.Load(filePath + _username + "EventDB.xml"));
            XNamespace SOAP_ENV = "http://www.w3.org/2001/12/soap-envelope";
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";

            // Add this EVENT into DICTIONARY
            string id = "ID" + (numOfEvents + 1);
            Event thisPhoto = new Event(id, _type, _lat, _longi, _path);
            theEvents.Add(id, thisPhoto);
            numOfEvents++;

            // Add this EVENT into LIST
            XElement newPhoto = new XElement(LIFELOG + "Event",
                                            new XElement(LIFELOG + "eventID", id),
                                            new XElement(LIFELOG + "Video",
                                                    new XElement(LIFELOG + "FilePath", _path),
                                                    new XElement(LIFELOG + "Location",
                                                            new XElement(LIFELOG + "Lat", _lat),
                                                            new XElement(LIFELOG + "Longi", _longi))));

            thisFile.Element(SOAP_ENV + "Envelope").Element(SOAP_ENV + "Body").Add(newPhoto);
            thisFile.Save(filePath + _username + "EventDB.xml");
        }

        // LOADING DATA FROM XML FILE
        public void LoadTwitterEvent(string _id, XElement _event)
        {
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";
            string tweet = _event.Element(LIFELOG + "Text").Value;
            double lat = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Lat").Value);
            double longi = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Longi").Value);
            DateTime dateTime = DateTime.Parse(_event.Element(LIFELOG + "DateTime").Value);

            Event thisEvent = new Event(_id, 0, lat, longi, tweet, dateTime);
            theEvents.Add(_id, thisEvent);
        }

        public void LoadFacebookEvent(string _id, XElement _event)
        {
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";
            string status = _event.Element(LIFELOG + "Text").Value;
            double lat = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Lat").Value);
            double longi = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Longi").Value);
            DateTime dateTime = DateTime.Parse(_event.Element(LIFELOG + "DateTime").Value);

            Event thisEvent = new Event(_id, 1, lat, longi, status, dateTime);
            theEvents.Add(_id, thisEvent);
        }

        public void LoadPhotoEvent(string _id, XElement _event)
        {
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";
            string pictureFilePath = _event.Element(LIFELOG + "FilePath").Value;
            double lat = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Lat").Value);
            double longi = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Longi").Value);

            Event thisEvent = new Event(_id, 2, lat, longi, pictureFilePath);
            theEvents.Add(_id, thisEvent);
        }

        public void LoadVideoEvent(string _id, XElement _event)
        {
            XNamespace LIFELOG = "http://www.xyz.org/lifelogevents";
            string videoFilePath = _event.Element(LIFELOG + "FilePath").Value;
            double lat = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Lat").Value);
            double longi = Convert.ToDouble(_event.Element(LIFELOG + "Location").Element(LIFELOG + "Longi").Value);

            Event thisEvent = new Event(_id, 3, lat, longi, videoFilePath);
            theEvents.Add(_id, thisEvent);
        }

        // GET NUMBER OF EVENTS AT THIS LOCATION
        public int GetNumOfEvents(double _lat, double _longi)
        {
            int numOfEvents = 0;
            foreach(KeyValuePair<string, Event> pair in theEvents)
            {
                if (pair.Value.GetCoords().GetLat() == _lat && pair.Value.GetCoords().GetLongi() == _longi)
                    numOfEvents++;
            }

            return numOfEvents;
        }

        public Dictionary<string, Event> GetEventDictAtThis(double _lat, double _longi)
        {
            Dictionary<string, Event> tempEventList = new Dictionary<string, Event>();
            foreach(KeyValuePair<string, Event> pair in theEvents)
            {
                if (pair.Value.GetCoords().GetLat() == _lat && pair.Value.GetCoords().GetLongi() == _longi)
                    tempEventList.Add(pair.Key, pair.Value);
            }

            return tempEventList;
        }

        public List<Event> GetEventListAtThis(double _lat, double _longi)
        {
            List<Event> tempList = new List<Event>();
            foreach (KeyValuePair<string, Event> pair in theEvents)
            {
                if (pair.Value.GetCoords().GetLat() == _lat && pair.Value.GetCoords().GetLongi() == _longi)
                    tempList.Add(pair.Value);
            }

            return tempList;
        }

        // GET ALL LOCATIONS WITH AN EVENT OR MORE (NO DUPLICATES OF LOCATION)
        public List<Coordinates> GetAllLocationCoords()
        {
            List<Coordinates> tempList = new List<Coordinates>();
            List<Coordinates> actualList = new List<Coordinates>();
            bool isSame = false;

            foreach (KeyValuePair<string, Event> pair in theEvents)
            {
                tempList.Add(pair.Value.GetCoords());
            }

            if (tempList.Count > 0)
                if (actualList.Count == 0)
                    actualList.Add(tempList[0]);

            for (int i = 0; i < tempList.Count; i++)
            {
                for(int j = 0; j < actualList.Count; j++)
                {
                    foreach(Coordinates coords in actualList)
                    {
                        isSame = false;

                        if (tempList[i].GetLat() == coords.GetLat() || tempList[i].GetLongi() == coords.GetLongi())
                        {
                            isSame = true;
                            break;
                        }
                    }

                    if (!isSame)
                    {
                        actualList.Add(tempList[i]);
                        break;
                    }
                }
            }

            return actualList;
        }
        
        public bool IsItMoreThanOneType(List<Event> _list)
        {
            // If there are more than ONE event at THIS LOCATION
            if(_list.Count > 1)
            {
                List<Event> tempList = new List<Event>();
                tempList = _list;

                for (int i = 0; i < _list.Count; i++)
                {
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        if(_list[i].GetThisTypeName() != tempList[j].GetThisTypeName())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
