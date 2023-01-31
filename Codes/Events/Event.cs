using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT365_A01_33836223.Events
{
    public class Event
    {
        public enum TYPE
        {
            Twitter = 0,
            Facebook,
            Photo,
            Video
        }

        private string eventID;
        private TYPE eventType;
        private Coordinates coordinates;
        private DateTime dateTime;

        private string userComment;
        private string fileAttachmentPath;

        public Coordinates GetCoords() { return coordinates; }
        public TYPE GetTypes() { return eventType; }
        public DateTime GetDateTime() { return dateTime; }
        public string GetThisTypeName() { return Enum.GetName(typeof(TYPE), eventType); }
        public string GetUserComment() { return userComment; }
        public string GetFileName() { return fileAttachmentPath; }

        public Event(string _eventID, int _eventType, double _lat, double _longi, string _userComment, DateTime _dateTime)
        {
            eventID = _eventID;
            eventType = (TYPE)_eventType;
            coordinates = new Coordinates(_lat, _longi);

            userComment = _userComment;
            dateTime = _dateTime;
        }

        public Event(string _eventID, int _eventType, double _lat, double _longi, string _filePath)
        {
            eventID = _eventID;
            eventType = (TYPE)_eventType;
            coordinates = new Coordinates(_lat, _longi);

            fileAttachmentPath = _filePath;
        }
    }


}
