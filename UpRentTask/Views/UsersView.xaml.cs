﻿using System.Windows.Controls;

namespace UpRentTask.Views;

public partial class UsersView : UserControl
{
    public UsersView()
    {
        InitializeComponent();

        DataContext = Ioc.Default.GetService<UsersViewModel>();
    }
}