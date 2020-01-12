using ASG.Objects;
using ASG.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASG.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddSong : Page
    {
        private AddSongService _service;
        public AddSong()
        {
            this.InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var song = new Song()
            {
                Name = Name_Text.Text,
                Singer = Singer_Text.Text,
                Author = Author_Text.Text,
                Description = Description_Text.Text,
                Thumbnail = Thumbnail_Text.Text,
                Link = Thumbnail_Text.Text
            };
            _service.Create(song);
        }
    }
}
