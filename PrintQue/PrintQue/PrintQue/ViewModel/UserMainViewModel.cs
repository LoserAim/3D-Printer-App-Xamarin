using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintQue.ViewModel
{
    public class UserMainViewModel
    {
        public NavigationCommand NavCommand;

        public UserMainViewModel()
        {
            NavCommand = new NavigationCommand(this);
        }

        public void Navigate()
        {

        }
    }
}
