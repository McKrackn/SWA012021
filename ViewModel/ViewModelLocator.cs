using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace exam.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>(true);
        }
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
    }
}
