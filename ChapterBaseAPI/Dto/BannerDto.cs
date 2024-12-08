namespace ChapterBaseAPI.Dto
{
    public class BannerDto
    {
        private Guid _id;
        private string _title;
        private string _description;
        private string _status;
        private byte[] _image;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        public DateTime UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }
    }
}