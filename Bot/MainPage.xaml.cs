using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Bot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private FileReader reader;
		private ObservableCollection<Message> all_messages;

		public MainPage()
        {
			this.InitializeComponent();
            this.reader = new FileReader();
			all_messages = new ObservableCollection<Message>(); 
			messages_list.ItemsSource = all_messages;

			input_tb.Focus(FocusState.Programmatic);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			all_messages.Add(new Message { message = input_tb.Text, sender = false });
			all_messages.Add(new Message { message = reader.Search_v1(input_tb.Text), sender = true });

			input_tb.Text = "";
		}

		private void input_tb_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == Windows.System.VirtualKey.Enter)
			{
				Button_Click(ok_b, e);
			}
		}

		private void show_all_Click(object sender, RoutedEventArgs e)
		{
			foreach (var row in reader.rows)
			{
				all_messages.Add(new Message { message = string.Join(", ", row.keywords), sender = false });
				all_messages.Add(new Message { message = row.output, sender = true });
			}
		}
	}
}
