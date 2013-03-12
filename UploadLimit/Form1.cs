using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.GData.Client;
using Google.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.GData.YouTube;
using Google.GData.Extensions.Location;

namespace UploadLimit
{
    public partial class Form1 : Form
    {
        string key = "somegarbage";
        string appName = "SlowUpload";
        string username = "blarg";
        string password = "blarg";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                txtFile.Text = dialog.FileName;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings(appName, key, username, password);
            YouTubeRequest request = new YouTubeRequest(settings);

            Video newVideo = new Video();

            newVideo.Title = "My Test Movie";
            newVideo.Tags.Add(new MediaCategory("Autos", YouTubeNameTable.CategorySchema));
            newVideo.Keywords = "cars, funny";
            newVideo.Description = "My description";
            newVideo.YouTubeEntry.Private = false;
            newVideo.Tags.Add(new MediaCategory("mydevtag, anotherdevtag",
              YouTubeNameTable.DeveloperTagSchema));

            newVideo.YouTubeEntry.Location = new GeoRssWhere(37, -122);
            // alternatively, you could just specify a descriptive string
            // newVideo.YouTubeEntry.setYouTubeExtension("location", "Mountain View, CA");

            newVideo.YouTubeEntry.MediaSource = new MediaFileSource(txtFile.Text, "video/quicktime");
            Video createdVideo = request.Upload(newVideo);
        }
    }
}
