using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DroneLander.MissionControl.Helpers
{
    public static class ControlHelper
    {
        private static ScrollViewer GetScrollViewer(DependencyObject element)
        {
            if (element is ScrollViewer)
            {
                return (ScrollViewer)element;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

        public static void ScrollIntoView(GridView gridView)
        {
            try
            {
                var sv = GetScrollViewer(gridView);
                sv.UpdateLayout();
                sv.ChangeView(0, sv.ExtentHeight, null);
            }
            catch { }

           
        }
    }
}
